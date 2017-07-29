using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FamiDesk.Mobile.App.Models;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Xamarin.Forms;

namespace FamiDesk.Mobile.App.Services
{
    public class BluetoothLEService
    {
        public ObservableCollection<BeaconModel> Beacons { get; set; } = new ObservableCollection<BeaconModel>();
        private IBluetoothLE _ble;
        private IAdapter _adapter;
        private bool _scanning;
        private Task _scanningTask;

        public BluetoothLEService()
        {
            _ble = CrossBluetoothLE.Current;
            _adapter = CrossBluetoothLE.Current.Adapter;
            _adapter.DeviceDiscovered += OnAdapterOnDeviceDiscovered;
            _adapter.ScanTimeoutElapsed += OnAdapter_ScanTimeoutElapsed;
            _adapter.ScanTimeout = 10_000;
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
            }
        }

        private async void OnAdapter_ScanTimeoutElapsed(object sender, System.EventArgs e)
        {
            await ScanTask(new CancellationToken());
        }

        private void OnAdapterOnDeviceDiscovered(object sender, DeviceEventArgs e)
        {
            //beacon found !
            if (e.Device?.Name?.ToUpper().Contains("KONTAKT") == true)
            {
                if (Beacons.Any(b => b.Id.ToUpper() == e.Device.Id.ToString().ToUpper()) == false)
                {
                    Beacons.Add(new BeaconModel(e.Device.Id.ToString(), e.Device.Name));
                    var notifService = DependencyService.Get<INotificationService>();
                    notifService.Push("Beacon found!", $"Id: {e.Device.Id}");
                }
            }

            //Device.BeginInvokeOnMainThread(() => {
            //    MessagingCenter.Send<TickedMessage>(message, "TickedMessage");
            //});
        }
    }
}