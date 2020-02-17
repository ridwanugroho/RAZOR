using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace belajarRazor.Models
{
    public class Purchases
    {
        public int Id{get; set;}
        public User User{get; set;}
        public string Address{get; set;}
        public string courir{get; set;}
        public string PaymentMethod{get; set;}
        [JsonIgnore]
        public string _ItemsDetail{get; set;}
        public TransactionDetails TransactionsDetail{get; set;}
        

        [NotMapped]
        public Carts ItemsDetail
        {
            get
            {
                return _ItemsDetail == null ? null : JsonConvert.DeserializeObject<Carts>(_ItemsDetail);
            }

            set
            {
                _ItemsDetail = JsonConvert.SerializeObject(value);
            }
        }
    }
}