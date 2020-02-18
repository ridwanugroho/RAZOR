using System;

namespace belajarRazor.Models
{
    public class Sales
    {
        public Barang Item{get; set;}
        public User Buyer{get; set;}
        public string OrderID{get; set;}
        public int Quantity{get; set;}
        public DateTime TransactionTime{get; set;}
        public string TransactionStatus{get; set;}
    }
}