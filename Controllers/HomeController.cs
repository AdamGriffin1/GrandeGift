using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;
using GrandeGift.Services;
using GrandeGift.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GrandeGift.Controllers
{
    public class HomeController : Controller
    {
        private IDataService<Category> _categorydataService;
        public HomeController(IDataService<Category> catService)
        {
            _categorydataService = catService;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> cats = _categorydataService.GetAll();
            //creat vm
            HomeIndexViewModel vm = new HomeIndexViewModel
            {
                Total = cats.Count(),
                Categories = cats
            };
            return View(vm);
        }
    }
}