using GrandeGift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class CategorySearchViewModel
    {
        public int Total { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int CategoryId { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; }

    }
}
