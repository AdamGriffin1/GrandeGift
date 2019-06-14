using GrandeGift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class AddressIndexViewModel
    {
        public int AddressId { get; set; }
        public int ProfileId { get; set; }
        public IEnumerable<Address> Addresses { get; set; }

    }
}
