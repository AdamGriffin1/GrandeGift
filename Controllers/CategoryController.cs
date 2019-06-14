using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;
using GrandeGift.Services;
using GrandeGift.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrandeGift.Controllers
{
    public class CategoryController : Controller
    {
        private IDataService<Category> _categoryService;
        private IDataService<Hamper> _hamperService;

        public CategoryController(IDataService<Category> catService, 
            IDataService<Hamper> hamService
            )
        {
            _categoryService = catService;
            _hamperService = hamService;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> cats = _categoryService.GetAll();
            //creat vm
            CategoryIndexViewModel vm = new CategoryIndexViewModel
            {
                Total = cats.Count(),
                Categories = cats
            };
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CategoryCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Category existingCategory = _categoryService.GetSingle(c => c.Name == vm.Name);
                if (existingCategory == null)
                {
                    Category cat = new Category
                    {
                        Name = vm.Name,
                        Details = vm.Details
                    };

                    _categoryService.Create(cat);

                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ViewBag.MyMessage = "Category name exists. please change the name";
                    return View(vm);
                }
            }
            else
            {
                return View(vm);
            }
        }
        public IActionResult Details(int id)
        {
            Category cat = _categoryService.GetSingle(c => c.CategoryId == id);
            IEnumerable<Hamper> hamperList = _hamperService.Query(p => p.CategoryId == id);
            CategoryDetailsViewModel vm = new CategoryDetailsViewModel
            {
                Total = hamperList.Count(),
                Details = cat.Details,
                Name = cat.Name,
                Hampers = hamperList.ToList(),
                CategoryId = cat.CategoryId
            };

            //pass to view
            return View(vm);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Category cat = _categoryService.GetSingle(c => c.CategoryId == id);
            _categoryService.Delete(cat);
            return RedirectToAction("Index", "Category");
        }

        [HttpGet]
        public IActionResult Search()
        {
            var vm = new CategorySearchViewModel();
            var categories = _categoryService.GetAll();
            vm.Categories = categories;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Search(CategorySearchViewModel vm)
        {
            IEnumerable<Hamper> hampers = _hamperService.GetAll().Where(c => c.CategoryId == vm.CategoryId);
            if (vm.MaxPrice > 0)
            {
                hampers = hampers.Where(h => h.Price < vm.MaxPrice);
            }
            if (vm.MinPrice > 0)
            {
                hampers = hampers.Where(h => h.Price > vm.MinPrice);
            }
            vm.Hampers = hampers;
            var categories = _categoryService.GetAll();
            vm.Categories = categories;

            return View(vm);
        }
    }
}