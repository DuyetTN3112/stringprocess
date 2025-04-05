using System;

namespace StringProcessingApp.Models
{
    public class Message
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string message_text { get; set; }
        public string encoded_text { get; set; }
        public int shift_value { get; set; } // Added this property
        public string input_codes { get; set; }
        public string output_codes { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public User user { get; set; }

        // Method to format the created date for display
        public string formatted_date => created_at.ToString("yyyy-MM-dd HH:mm:ss");
    }
}