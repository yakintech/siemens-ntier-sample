using Microsoft.EntityFrameworkCore;
using Siemens.DAL.ORM.Entity;
using Siemens.DAL.ORM.Entity.WebUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.DAL.ORM.Context
{
    public class SiemensContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=SiemensDb;Trusted_Connection=true");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<SupplierAddress> SupplierAddresses { get; set; }

        public DbSet<WebUser> WebUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Product>()
            //     .Property(p => p.Name)
            //     .HasMaxLength(30);
                 



        }

    }
}
