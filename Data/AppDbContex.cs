using Microsoft.EntityFrameworkCore;
using belajarRazor.Models;


namespace belajarRazor.Data
{
    public class AppDbContex : DbContext
    {
        public DbSet<User> User{get; set;}
        public DbSet<Barang> Barang{get; set;}
        public DbSet<Cart> Cart{get; set;}
        public DbSet<Item> Items{get; set;}
        
        public AppDbContex(DbContextOptions options) : base(options)
        {

        }
    }
}