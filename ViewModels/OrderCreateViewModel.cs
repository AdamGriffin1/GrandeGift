﻿using GrandeGift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class OrderCreateViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public int AddressId { get; set; }
        public int HamperId { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
    }
}
