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
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;
        private SignInManager<IdentityUser> _signInManagerService;
        private RoleManager<IdentityRole> _roleManagerService;
        private IDataService<Profile> _profileDataService;

        public AccountController(UserManager<IdentityUser> managerService,
                                 SignInManager<IdentityUser> signInService,
                                 RoleManager<IdentityRole> roleService,
                                 IDataService<Profile> profileService)
        {
            _userManagerService = managerService;
            _signInManagerService = signInService;
            _roleManagerService = roleService;
            _profileDataService = profileService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            AccountLoginViewModel vm = new AccountLoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManagerService.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(vm.ReturnUrl))
                    {
                        return Redirect(vm.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManagerService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {

                IdentityUser user = new IdentityUser(vm.Username);

                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {

                    if (await _roleManagerService.FindByNameAsync("Customer") != null)
                    {
                        await _userManagerService.AddToRoleAsync(user, "Customer");
                    }

                    user = await _userManagerService.FindByNameAsync(vm.Username);
                    Profile profile = new Profile
                    {
                        UserId = user.Id,
                        FirstName = "please insert",
                        LastName = "please insert"
                    };
                    _profileDataService.Create(profile);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update()
        {
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);

            Profile profile = _profileDataService.GetSingle(p => p.UserId == user.Id);

            AccountUpdateViewModel vm = new AccountUpdateViewModel
            {
                ProfileId = profile.ProfileId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = user.Email
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AccountUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);
                Profile profile = _profileDataService.GetSingle(p => p.UserId == user.Id);

                profile.UserId = user.Id;
                profile.FirstName = vm.FirstName;
                profile.LastName = vm.LastName;
                profile.Email = vm.Email;

                _profileDataService.Update(profile);

                user.Email = vm.Email;
                await _userManagerService.UpdateAsync(user);

                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole(AccountAddRoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole(vm.Name);
                IdentityResult result = await _roleManagerService.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Administration()
        {
            return View();
        }
    }
}