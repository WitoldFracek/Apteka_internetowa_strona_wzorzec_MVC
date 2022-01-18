using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PO_Projekt.Models;

namespace PO_Projekt.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionOrder> PrescriptionOrders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductName> ProductNames { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductForm> ProductForms { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<ShippingData> ShippingData { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Seed();
        }
    }
}
