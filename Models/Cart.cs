using System;
using System.Collections.Generic;

namespace belajarRazor.Models
{
    public class Cart
    {
        public int id{get; set;}
        public double totalPrice{get; set;}
        public DateTime createdAt{get; set;}
        public DateTime editedAt{get; set;}

        public virtual ICollection<Item> Items{get; set;}

        public User User{get; set;}
    }
}