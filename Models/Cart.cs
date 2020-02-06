using System.Collections.Generic;

namespace belajarRazor.Models
{
    public class Cart
    {
        public int id{get; set;}
        public double totalPrice{get; set;}

        public virtual ICollection<Item> Items{get; set;}
    }
}