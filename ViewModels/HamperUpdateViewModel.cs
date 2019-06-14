using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class HamperUpdateViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }
        public int HamperId { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public IFormFile Image { get; set; }
    }
}
