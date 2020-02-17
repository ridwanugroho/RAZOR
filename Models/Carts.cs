using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace belajarRazor.Models
{
    public class Carts
    {
        public int id{get; set;}
        public int userID{get; set;}
        public double totalPrice{get; set;}
        [JsonIgnore]
        public string _Items{get; set;}
        
        [NotMapped]
        public List<Items> Items
        {
            get{return _Items == null ? null : JsonConvert.DeserializeObject<List<Items>>(_Items);}
            set{_Items = JsonConvert.SerializeObject(value);}
        }
    }
}