using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;
using GrandeGift.Services;
using GrandeGift.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace GrandeGift.Controllers
{
    public class HamperController : Controller
    {
        private readonly IDataService<Hamper> _hamperService;
        private readonly IHostingEnvironment _hostingEnvironmentServices;
        public HamperController(IDataService<Hamper> service, IHostingEnvironment hostingService)
        {
            _hamperService = service;
            _hostingEnvironmentServices = hostingService;

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(int id)
        {
            HamperCreateViewModel vm = new HamperCreateViewModel
            {
                CategoryId = id
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(HamperCreateViewModel vm)
        {
            string uniqueFileName = null;
            if (vm.Image != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironmentServices.WebRootPath + "\\images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.Image.FileName;
                string FilePath = Path.Combine(uploadFolder, uniqueFileName);
                vm.Image.CopyTo(new FileStream(FilePath, FileMode.Create));
            }

            //map
            Hamper hamper = new Hamper
            {
                CategoryId = vm.CategoryId,
                Name = vm.Name,
                Details = vm.Details,
                Price = vm.Price,
                Image = uniqueFileName,
                IsDeleted = false
            };

            //call service
            _hamperService.Create(hamper);

            //go to category details
            return RedirectToAction("Details", "Category", new { id = vm.CategoryId });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            //call service
            Hamper hamper = _hamperService.GetSingle(p => p.HamperId == id);
            
            HamperUpdateViewModel vm = new HamperUpdateViewModel
            {
                HamperId = id,
                Name = hamper.Name,
                Details = hamper.Details,
                Price = hamper.Price,
                CategoryId = hamper.CategoryId
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(HamperUpdateViewModel vm)
        {
            string uniqueFileName = null;
            if (vm.Image != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironmentServices.WebRootPath + "\\images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.Image.FileName;
                string FilePath = Path.Combine(uploadFolder, uniqueFileName);
                vm.Image.CopyTo(new FileStream(FilePath, FileMode.Create));
            }
            //map
            Hamper updatedHamper = new Hamper
            {
                HamperId = vm.HamperId,
                CategoryId = vm.CategoryId,
                Name = vm.Name,
                Details = vm.Details,
                Price = vm.Price,
                Image = uniqueFileName
            };
            //call service
            _hamperService.Update(updatedHamper);
            //go to 
            return RedirectToAction("Details", "Category", new { id = vm.CategoryId });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Hamper hamp = _hamperService.GetSingle(c => c.HamperId == id);
            hamp.IsDeleted = true;
            _hamperService.Update(hamp);
            return RedirectToAction("Details", "Category", new { id = hamp.CategoryId });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Restore(int id)
        {
            Hamper hamp = _hamperService.GetSingle(c => c.HamperId == id);
            hamp.IsDeleted = false;
            _hamperService.Update(hamp);
            return RedirectToAction("Details", "Category", new { id = hamp.CategoryId });
        }
    }
}