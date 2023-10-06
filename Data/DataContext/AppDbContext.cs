using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataContext
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Category>().ToTable("categories", schema: "stock");
			modelBuilder.Entity<Product>().ToTable("products", schema: "stock");
			modelBuilder.Entity<Vendor>().ToTable("vendors", schema: "stock");


			modelBuilder.Entity<Product>()
		   .HasOne(p => p.Category)
		   .WithMany(v => v.Products)
		   .HasForeignKey(p => p.CategoryId);
		}
	}
}
