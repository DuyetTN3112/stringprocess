
using System.Collections.Generic;

using StringProcessingApp.Models;

namespace StringProcessingApp.Interfaces
{
    public interface IUserRepository
    {
        // Add a new user
        void AddUser(User user);

        // Get user by codename
        User GetUserByCodename(string codename);

        // Get user by email
        User GetUserByEmail(string email);

        // Validate login credentials
        bool ValidateLogin(string codename, string password);

        // Update user
        void UpdateUser(User user);

        // Delete user by ID
        void DeleteUser(int user_id);

        // Get all users
        IEnumerable<User> GetAllUsers();

        // Search users
        IEnumerable<User> SearchUsers(string searchTerm);
    }
}