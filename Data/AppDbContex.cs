using Microsoft.EntityFrameworkCore;
using belajarRazor.Models;


namespace belajarRazor.Data
{
    public class AppDbContex : DbContext
    {
        public DbSet<User> User{get; set;}
        public DbSet<Barang> Barang{get; set;}
        public DbSet<Carts> Carts{get; set;}
        public DbSet<Purchases> Purchases{get; set;}
        public DbSet<TransactionDetails> TransactionDetail{get; set;}
        public DbSet<Conversation> Conversations{get; set;}
        
        public AppDbContex(DbContextOptions options) : base(options)
        {
            
        }

    }
}