using System;
using System.Collections.Generic;


namespace StringProcessingApp.Models
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string codename { get; set; }
        public string password { get; set; }
        public string role { get; set; } = "User";
        public DateTime created_at { get; set; } = DateTime.Now;
        public ICollection<Message> messages { get; set; }

        public User()
        {
            messages = new List<Message>();
        }
    }
}