using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace belajarRazor.Models
{
    public class Carts
    {
        internal string _Items{get; set;}
        public double totalPrice{get; set;}
        public int userID{get; set;}
        
        [NotMapped]
        public Items Items
        {
            get{return _Items == null ? null : JsonConvert.DeserializeObject<Items>(_Items);}
            set{_Items = JsonConvert.SerializeObject(value);}
        }
    }
}