using CoreBridge.Models;
using Microsoft.EntityFrameworkCore;

using System;

namespace CoreBridge.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Products> Products { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }

    }
}
