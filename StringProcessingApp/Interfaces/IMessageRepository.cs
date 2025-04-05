
using System.Collections.Generic;

using StringProcessingApp.Models;

namespace StringProcessingApp.Interfaces
{
    public interface IMessageRepository
    {
        // Add a new message
        void AddMessage(Message message);

        // Get messages by user ID
        IEnumerable<Message> GetMessagesByuser_id(int user_id);

        // Get all messages
        IEnumerable<Message> GetAllMessages();

        // Get messages sorted by date
        IEnumerable<Message> GetMessagesSortedByDate(bool ascending);

        // Get messages sorted alphabetically
        IEnumerable<Message> GetMessagesSortedAlphabetically(bool ascending);

        // Search messages
        IEnumerable<Message> SearchMessages(string searchTerm);
    }
}