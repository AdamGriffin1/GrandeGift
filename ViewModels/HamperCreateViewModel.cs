using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GrandeGift.ViewModels
{
    public class HamperCreateViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public IFormFile Image { get; set; }
    }
}
