﻿using BusinessObjectLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class BoschStoreContext : DbContext
    {
        private readonly DbContextOptions<BoschStoreContext> _options;
        public DbContextOptions<BoschStoreContext> Options
        {
            get
            {
                return _options;
            }
        }

        public BoschStoreContext(DbContextOptions<BoschStoreContext> options) : base(options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=CLJ-C-000X9\\SQLEXPRESS; Database=BoschStore_Dev; TrustServerCertificate=True; Trusted_Connection=True;")
                .EnableSensitiveDataLogging();
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<SubcategoryEntity> Subcategories { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<WarehouseEntity> Warehouses { get; set; }
        public DbSet<CartItemEntity> CartItems { get; set; }
        public DbSet<PickupPointEntity> PickupPoints { get; set; }
        public DbSet<ProductWarehouseMapping> ProductWarehouseMapping { get; set; }
    }
}
