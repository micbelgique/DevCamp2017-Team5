using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamiDesk.Mobile.App.Models;
using FamiDesk.Mobile.App.Services;
using Xamarin.Forms;

namespace FamiDesk.Mobile.App.ViewModels
{
    public class AllBeaconsViewModel
    {
        private readonly BluetoothLEService _bluetoothService;
        public ObservableCollection<BeaconModel> Beacons { get; set; }

        public AllBeaconsViewModel()
        {
            _bluetoothService = DependencyService.Get<BluetoothLEService>();
            Beacons = _bluetoothService.Beacons;
        }
    }
}
