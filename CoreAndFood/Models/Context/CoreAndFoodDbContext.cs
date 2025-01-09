using CoreAndFood.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CoreAndFood.Models.Context
{
    public class CoreAndFoodDbContext : DbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server =.; Database = CoreAndFoodDb; Trusted_Connection = True; MultipleActiveResultSets = true; TrustServerCertificate = True;");
        }
    }
}


