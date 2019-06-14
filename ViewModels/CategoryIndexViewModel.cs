using GrandeGift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class CategoryIndexViewModel
    {
        public int Total { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
