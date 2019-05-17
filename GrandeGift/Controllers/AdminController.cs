using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;
using GrandeGift.Models.AdminViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GrandeGift.Controllers
{

    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("admin/{userId}/roles")]
        public async Task<IActionResult> UserRoles(string userId)
        {
            // Find the user by their id
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            // Get the roles for that user
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            // Get the list of all roles
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();

            // Build the select list for the select element's options
            SelectList selectList = new SelectList(roles, "Name", "Name");

            // Build the view model
            UserRolesViewModel model = new UserRolesViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRoles = userRoles,
                Roles = selectList
            };

            // Pass it to the view
            return View(model);
        }
    }

}