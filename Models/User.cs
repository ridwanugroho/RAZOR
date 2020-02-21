using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace belajarRazor.Models
{
    public class User
    {
        public int id{get; set;}
        public string username{get; set;}
        public string email{get; set;}
        [JsonIgnore]
        public string password{get; set;}
        public int authLevel{get; set;}
        [JsonIgnore]
        public DateTime createdAt{get; set;}
        [JsonIgnore]
        public DateTime updatedAt{get; set;}
        // public List<Carts> Carts{get; set;}
    }
}