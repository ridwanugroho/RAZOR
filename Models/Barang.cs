using System;


namespace belajarRazor.Models
{
    public class Barang
    {
        public int id{get; set;}
        public string name{get; set;}
        public string desc{get; set;}
        public string img_url{get; set;}
        public double price{get; set;}
        public int rating{get; set;}
        public DateTime createdAt{get; set;}
        public DateTime editedAt{get; set;}
    }
}