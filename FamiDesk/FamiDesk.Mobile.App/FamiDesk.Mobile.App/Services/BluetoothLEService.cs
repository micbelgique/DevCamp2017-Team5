using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FamiDesk.Mobile.App.Messages;
using FamiDesk.Mobile.App.Models;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Xamarin.Forms;

namespace FamiDesk.Mobile.App.Services
{
    public class BluetoothLEService
    {
        public event Action<string> DebugInfo;

        private object lockObject = new object();

        public ObservableCollection<BeaconModel> Beacons { get; set; } = new ObservableCollection<BeaconModel>();
        private readonly IBluetoothLE _ble;
        private readonly IAdapter _adapter;
        private IDataStore<Person> _personDataStore;
        private IDataStore<EventInfo> _eventDataStore;
        private bool _scanning;
        private Task _scanningTask;

        public BluetoothLEService()
        {
            _ble = CrossBluetoothLE.Current;
            _adapter = CrossBluetoothLE.Current.Adapter;
            _adapter.DeviceDiscovered += OnAdapterOnDeviceDiscovered;
            _adapter.ScanTimeoutElapsed += OnAdapter_ScanTimeoutElapsed;
            _adapter.ScanTimeout = 10_000;
            _personDataStore = DependencyService.Get<IDataStore<Person>>();
            _eventDataStore = DependencyService.Get<IDataStore<EventInfo>>();
        }

        public async Task ScanTask(CancellationToken token)
        {

            try
            {
                if (_scanningTask == null || _scanningTask.IsCompleted || _scanningTask.IsCanceled ||
                    _scanningTask.IsFaulted)
                {
                    _scanningTask = Task.Run(async () =>
                    {
                        _scanning = true;
                        while (_scanning)
                        {
                            token.ThrowIfCancellationRequested();
                            await Task.Delay(250, token);
                            DebugInfo?.Invoke($"Clear out of range beacons");
                            await ClearOutOfRangeBeacon();
                            DebugInfo?.Invoke($"Start scanning ... ({DateTime.Now:T})");
                            await _adapter.StartScanningForDevicesAsync(cancellationToken: token);
                        }
                    }, token);
                    await _scanningTask;
                }
                else
                {
                    _scanning = true;
                }
            }
            catch (Exception e)
            {
                DebugInfo?.Invoke(e.Message);
                await ScanTask(new CancellationToken());
            }
        }

        private Task ClearOutOfRangeBeacon()
        {
            return Task.Run(async () =>
            {
                try
                {
                    var toRemove = Beacons.Where(b => (DateTime.Now - b.LastDiscoveredDate).TotalSeconds > 30).ToList();

                    lock (lockObject)
                    {
                        foreach (var beacon in toRemove)
                        {
                            Beacons.Remove(beacon);
                        }
                    }

                    //notify check-out
                    foreach (var beacon in toRemove)
                    {
                        var persons = await _personDataStore.GetItemsAsync();
                        var eventInfos = await _eventDataStore.GetItemsAsync();
                        var person = persons.FirstOrDefault(p => p.BeaconId?.ToUpper() == beacon.Id.ToString().ToUpper());
                        if (person != null)
                        {
                            var userEvents = eventInfos
                                .Where(ev => ev.PersonId == person.Id && ev.UserId == App.CurrentUserId)
                                .OrderByDescending(d => d.Date).ToList();
                            var lastEvent = userEvents.FirstOrDefault();
                            bool isIn = lastEvent.Type.ToUpper() == "CHECKIN";

                            if (isIn)
                            {
                                MessagingCenter.Send(this, "NotificationMessage",
                                    new NotificationMessage($"Dite au revoir à {person.FirstName} !",
                                        "Procéder au check-Out ?",
                                        new KeyValuePair<string, string>("Id", person.Id)));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                }
            });
        }

        private async void OnAdapter_ScanTimeoutElapsed(object sender, System.EventArgs e)
        {
            DebugInfo?.Invoke($"Start Timeout !");
            await ScanTask(new CancellationToken());
        }

        private async void OnAdapterOnDeviceDiscovered(object sender, DeviceEventArgs e)
        {
            try
            {
                //beacon found !
                if (e.Device?.Name?.ToUpper().Contains("KONTAKT") == true)
                {
                    //check if the beacon has been already discovered
                    var beacon = Beacons.FirstOrDefault(b => b.Id.ToUpper() == e.Device.Id.ToString().ToUpper());
                    if (beacon == null)
                    {
                        //Add the beacon on the list
                        lock (lockObject)
                        {
                            Beacons.Add(new BeaconModel(e.Device.Id.ToString(), e.Device.Name));
                        }

                        //check if one person match the beacon id
                        var persons = await _personDataStore.GetItemsAsync();
                        var eventInfos = await _eventDataStore.GetItemsAsync();
                        var person = persons.FirstOrDefault(p => p.BeaconId?.ToUpper() == e.Device.Id.ToString().ToUpper());
                        if (person != null)
                        {
                            var userEvents = eventInfos
                                .Where(ev => ev.PersonId == person.Id && ev.UserId == App.CurrentUserId)
                                .OrderByDescending(d => d.Date).ToList();
                            var lastEvent = userEvents.FirstOrDefault();
                            bool isIn = lastEvent.Type.ToUpper() == "CHECKIN";

                            if (isIn == false)
                            {
                                MessagingCenter.Send(this, "NotificationMessage",
                                    new NotificationMessage($"Bienvenu chez {person.FirstName} !",
                                        "Procéder au check-in ?",
                                        new KeyValuePair<string, string>("Id", person.Id)));
                            }
                        }
                    }
                    else
                    {
                        //Update the last discoveredDate of the beacon
                        beacon.LastDiscoveredDate = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}