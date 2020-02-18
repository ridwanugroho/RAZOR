using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace belajarRazor.Models
{
    public class TransactionDetails
    {
        [JsonIgnore]
        public int id{get; set;}

        public string status_code{get; set;} = "NULL";
        public string status_message{get; set;} = "NULL";
        public string transaction_id{get; set;} = "NULL";
        public string order_id{get; set;} = "NULL";
        public string merchant_id{get; set;} = "NULL";
        public string gross_amount{get; set;} = "NULL";
        public string currency{get; set;} = "NULL";
        public string payment_type{get; set;} = "NULL";
        public DateTime transaction_time{get; set;} = DateTime.Now;
        public string transaction_status{get; set;} = "NULL";
        public string fraud_status{get; set;} = "NULL";
        [JsonIgnore]
        public string _va_numbers{get; set;} = "[{\"bank\":\"NULL\",\"va_number\":\"NULL\"}]";
        [JsonIgnore]
        public string _actions{get; set;}

        [NotMapped]
        public List<Actions> actions
        {
            get{return _actions == null ? null : JsonConvert.DeserializeObject<List<Actions>>(_actions);}
            set{_actions = JsonConvert.SerializeObject(value);}
        }

        [NotMapped]
        public List<Virtual> va_numbers
        {
            get{return _va_numbers == null ? null : JsonConvert.DeserializeObject<List<Virtual>>(_va_numbers);}
            set{_va_numbers = JsonConvert.SerializeObject(value);}
        }
    }

    public class Virtual
    {
        public string bank{get; set;} = "NULL";
        public string va_number{get; set;} = "NULL";
    }

    public class Actions
    {
        public string name{get; set;} = "NULL";
        public string method{get; set;} = "NULL";
        public string url{get; set;} = "NULL";
    }
}