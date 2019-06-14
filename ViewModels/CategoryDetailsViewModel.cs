﻿using GrandeGift.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class CategoryDetailsViewModel
    {
        public int Total { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; }
    }
}
