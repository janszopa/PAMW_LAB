using Microsoft.EntityFrameworkCore;
using Domain.Models;
using System.ComponentModel;

namespace MyRestApi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Order> Orders { get; set; }


         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category) 
                .WithMany(c => c.Products) 
                .HasForeignKey(p => p.CategoryId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);


            modelBuilder.Entity<User>()
                .HasOne(u => u.ContactInfo) 
                .WithOne(ci => ci.User) 
                .HasForeignKey<ContactInfo>(ci => ci.UserId); 

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<ContactInfo>()
                .Property(ci => ci.Email)
                .IsRequired()
                .HasMaxLength(150);


            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithMany(o => o.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderProduct", // Nazwa tabeli pośredniej
                    j => j.HasOne<Order>().WithMany().HasForeignKey("OrderId"),
                    j => j.HasOne<Product>().WithMany().HasForeignKey("ProductId"),
                    j =>
                    {
                        j.HasKey("OrderId", "ProductId"); // Klucz główny tabeli pośredniej
                    });
        }
    }
}