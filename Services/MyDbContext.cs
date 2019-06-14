using GrandeGift.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using GrandeGift.Models;

namespace GrandeGift.Services
{
    public class MyDbContext: IdentityDbContext
    {
        public DbSet<Category> CategoryTbl { get; set; }
        public DbSet<Hamper> HamperTbl { get; set; }
        public DbSet<Profile> ProfileTbl { get; set; }
        public DbSet<Address> AddressTbl { get; set; }
        public DbSet<Order> OrderTbl { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=PC10-26; Database=GrandeGiftAccounts; Trusted_Connection=True");
        }
    }
}