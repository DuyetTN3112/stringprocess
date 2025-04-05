using System.Collections.Generic;
using System.Linq;
using StringProcessingApp.Database;
using StringProcessingApp.Interfaces;
using StringProcessingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace StringProcessingApp.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DatabaseContext _context;

        public MessageRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        public IEnumerable<Message> GetMessagesByuser_id(int user_id)
        {
            return _context.Messages
                .Include(m => m.user)
                .Where(m => m.user_id == user_id)
                .ToList();
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return _context.Messages
                .Include(m => m.user)
                .ToList();
        }

        public IEnumerable<Message> GetMessagesSortedByDate(bool ascending)
        {
            return ascending
                ? _context.Messages.Include(m => m.user).OrderBy(m => m.created_at).ToList()
                : _context.Messages.Include(m => m.user).OrderByDescending(m => m.created_at).ToList();
        }

        public IEnumerable<Message> GetMessagesSortedAlphabetically(bool ascending)
        {
            return ascending
                ? _context.Messages.Include(m => m.user).OrderBy(m => m.message_text).ToList()
                : _context.Messages.Include(m => m.user).OrderByDescending(m => m.message_text).ToList();
        }

        public IEnumerable<Message> SearchMessages(string searchTerm)
        {
            return _context.Messages
                .Include(m => m.user)
                .Where(m => m.message_text.Contains(searchTerm) ||
                    m.encoded_text.Contains(searchTerm))
                .ToList();
        }
    }
}