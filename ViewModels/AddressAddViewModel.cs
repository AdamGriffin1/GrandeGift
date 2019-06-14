using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class AddressAddViewModel
    {
        public int ProfileId { get; set; }
        public int AddressId { get; set; }
        public string Location { get; set; }
        public string State { get; set; }
    }
}
