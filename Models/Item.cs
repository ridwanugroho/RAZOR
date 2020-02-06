namespace belajarRazor.Models
{
    public class Item
    {
        public int id{get; set;}
        public int qty{get; set;}
        public virtual Barang Barang{get; set;}
        
        public virtual Cart cart{get; set;}
    }
}