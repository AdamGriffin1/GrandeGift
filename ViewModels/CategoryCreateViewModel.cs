﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = "Please enter a name")]
        [Display(Name = "Category Name")]
        [MaxLength(60, ErrorMessage = "Name length less than 60")]
        [MinLength(3, ErrorMessage = "Name grater than 3")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Details { get; set; }
    }
}
