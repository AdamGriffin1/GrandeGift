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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrandeGift.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IDataService<Order> _orderService;
        private readonly IDataService<Address> _addressService;
        private readonly UserManager<IdentityUser> _userService;
        private readonly IDataService<Profile> _ProfileService;

        public OrderController(IDataService<Order> service, IDataService<Address> addressService, 
            UserManager<IdentityUser> userService, IDataService<Profile> _profileService
            )
        {
            _orderService = service;
            _addressService = addressService;
            _userService = userService;
            _ProfileService = _profileService;
        }
        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            IdentityUser user = await _userService.FindByNameAsync(User.Identity.Name);
            Profile userProfile = _ProfileService.GetSingle(p => p.UserId == user.Id);
            IEnumerable<Address> Addresses = _addressService.GetAll().Where(p => p.ProfileId == userProfile.ProfileId);
            OrderCreateViewModel vm = new OrderCreateViewModel
            {
                Addresses = Addresses,
                HamperId = id,
                OrderDate = DateTime.Today,
                
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateViewModel vm)
        {
            IdentityUser user = await _userService.FindByNameAsync(User.Identity.Name);
            Profile userProfile = _ProfileService.GetSingle(p => p.UserId == user.Id);
            //map
            Order order = new Order
            {
                ProfileId = userProfile.ProfileId,
                HamperId = vm.HamperId,
                AddressId = vm.AddressId,
                OrderDate = vm.OrderDate,
                Quantity = vm.Quantity,
            };

            //call service
            _orderService.Create(order);

            //go to category details
            return RedirectToAction("Index", "Home");
        }
    }
}
