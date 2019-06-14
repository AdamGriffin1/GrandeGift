using GrandeGift.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Services
{
    public static class SeedHelper
    {
        public static async Task Seed(IServiceProvider provider)//IServiceProvider serviceProvider)
        {
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


                ////add Customer role
                //if (await roleManager.FindByNameAsync("Customer") != null)
                //{
                //    await roleManager.CreateAsync(new IdentityRole("Customer"));
                //}
                ////add Admin role
                //if (await roleManager.FindByNameAsync("Admin") != null)
                //{
                //    await roleManager.CreateAsync(new IdentityRole("Admin"));
                //}
                ////add Manager role
                //if (await roleManager.FindByNameAsync("Manager") != null)
                //{
                //    await roleManager.CreateAsync(new IdentityRole("Manager"));
                //}

                //add default Admin
               // if (await userManager.FindByNameAsync("Admin") != null)
               // {
                    IdentityUser admin = new IdentityUser("Admin");
                    await userManager.CreateAsync(admin, "P@ssw0rd");
                    await userManager.AddToRoleAsync(admin, "Admin");

                //}
            }
            ////we need a service to contact the database
            //DataService<Category> categoryService = new DataService<Category>();
            //DataService<Hamper> hamperService = new DataService<Hamper>();

            ////add sample data categories
            //Category cat1 = new Category
            //{
            //    Name = "Food",
            //    Details = "Nice food for everyone"
            //};
            //categoryService.Create(cat1);

            //Category cat2 = new Category
            //{
            //    Name = "Drink",
            //    Details = "Nice drink for everyone",
            //};
            //categoryService.Create(cat2);

            //IEnumerable<Category> cats = categoryService.GetAll();
            //foreach (Category item in cats)
            //{
            //    Hamper pro1 = new Hamper
            //    {
            //        CategoryId = item.CategoryId,
            //        Name = "Product1",
            //        Details = "Product1 details",
            //        Price = 22
            //    };
            //    hamperService.Create(pro1);

            //    Hamper pro2 = new Hamper
            //    {
            //        CategoryId = item.CategoryId,
            //        Name = "Product2",
            //        Details = "Product2 details",
            //        Price = 44
            //    };
            //    hamperService.Create(pro2);
            //}

        }
    }
}
