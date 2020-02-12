using Microsoft.EntityFrameworkCore;
using belajarRazor.Models;


namespace belajarRazor.Data
{
    public class AppDbContex : DbContext
    {
        public DbSet<User> User{get; set;}
        public DbSet<Barang> Barang{get; set;}
        public DbSet<Carts> Carts{get; set;}
        
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Carts>(entity=>
            {
                entity.HasNoKey();
            });
        }
        
        public AppDbContex(DbContextOptions options) : base(options)
        {
            
        }

    }
}