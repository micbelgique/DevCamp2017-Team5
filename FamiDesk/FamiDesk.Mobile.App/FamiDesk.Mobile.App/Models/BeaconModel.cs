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

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public BeaconModel()
        {
        }
        public BeaconModel(string id, string name)
        {
            Id = id;
            _name = name;
        }
    }
}
