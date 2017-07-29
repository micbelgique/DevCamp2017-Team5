using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamiDesk.Mobile.App.Models
{
    public class BeaconModel : BaseDataObject
    {
        private string _name;
        private DateTime _discoveredDate;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public DateTime DiscoveredDate
        {
            get => _discoveredDate;
            set => SetProperty(ref _discoveredDate, value);
        }

        public BeaconModel()
        {
        }
        public BeaconModel(string id, string name)
        {
            Id = id;
            _name = name;
            DiscoveredDate = DateTime.Now;
        }
    }
}
