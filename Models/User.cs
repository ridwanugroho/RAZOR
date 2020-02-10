using System;
using System.Collections.Generic;

namespace belajarRazor.Models
{
    public class User
    {
        public int id{get; set;}
        public string username{get; set;}
        public string email{get; set;}
        public string password{get; set;}
        public int authLevel{get; set;}
        public DateTime createdAt{get; set;}
        public DateTime updatedAt{get; set;}
        public ICollection<Cart> Carts{get; set;}
    }
}