using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;
using GrandeGift.Services;
using GrandeGift.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers
{
    public class AddressController : Controller
    {
        private IDataService<Address> _addressService;

        public AddressController(IDataService<Address> addService)
        {
            _addressService = addService;
        }

        public IActionResult Index(int id)
        {
            IEnumerable<Address> adds = _addressService.GetAll().Where(a => a.ProfileId == id);
            //creat vm
            AddressIndexViewModel vm = new AddressIndexViewModel
            {
                Addresses = adds,
                ProfileId = id
            };
            return View(vm);
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Add(int id)
        {

            AddressAddViewModel vm = new AddressAddViewModel
            {
                ProfileId = id
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(AddressAddViewModel vm)
        {
            //map
            Address add = new Address
            {
                ProfileId = vm.ProfileId,
                Location = vm.Location,
                State = vm.State
            };

            //call service
            _addressService.Create(add);

            //go to category details
            return RedirectToAction("Index", "Address", new { id = vm.ProfileId });
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            //call service
            Address add = _addressService.GetSingle(p => p.AddressId == id);

            AddressUpdateViewModel vm = new AddressUpdateViewModel
            {
                AddressId = add.AddressId,
                ProfileId = add.ProfileId,
                Location = add.Location,
                State = add.State
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Update(AddressUpdateViewModel vm)
        {
            //map
            Address updatedAddress = new Address
            {
                AddressId=vm.AddressId,
                ProfileId=vm.ProfileId,
                Location=vm.Location,
                State=vm.State
            };
            //call service
            _addressService.Update(updatedAddress);
            //go to 
            return View(vm);
        }

        public IActionResult Delete(int id)
        {
            Address add = _addressService.GetSingle(c => c.AddressId == id);
            _addressService.Delete(add);
            return RedirectToAction("Index", "Address", new { id = add.ProfileId });
        }
    }
}
