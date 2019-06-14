using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public int ProfileId { get; set; }
        public string Location { get; set; }
        public string State { get; set; }
    }
}
