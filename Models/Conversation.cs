using System;
using System.ComponentModel.DataAnnotations;

namespace belajarRazor.Models
{
    public class Conversation
    {
        [Key]
        public Guid uuid{get; set;}
        public User From{get; set;}
        public User To{get; set;}
        public string Message{get; set;}
        public DateTime SentTime{get; set;}
        public DateTime ? Read{get; set;}

    }
}