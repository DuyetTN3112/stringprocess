using System.Collections.Generic;
using System.Linq;
using StringProcessingApp.Database;
using StringProcessingApp.Interfaces;
using StringProcessingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace StringProcessingApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUserByCodename(string codename)
        {
            return _context.Users
                .Include(u => u.messages)
                .FirstOrDefault(u => u.codename == codename);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users
                .FirstOrDefault(u => u.email == email);
        }

        public bool ValidateLogin(string codename, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.codename == codename);

            return user != null && user.password == password;
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int user_id)
        {
            var user = _context.Users.Find(user_id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public IEnumerable<User> SearchUsers(string searchTerm)
        {
            return _context.Users
                .Where(u => u.name.Contains(searchTerm) ||
                    u.last_name.Contains(searchTerm) ||
                    u.email.Contains(searchTerm) ||
                    u.codename.Contains(searchTerm))
                .ToList();
        }
    }
}