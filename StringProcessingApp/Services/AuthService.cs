using StringProcessingApp.Interfaces;
using StringProcessingApp.Models;
using StringProcessingApp.Exceptions;

namespace StringProcessingApp.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private static User _currentUser;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Login(string codename, string password)
        {
            if (string.IsNullOrEmpty(codename) || string.IsNullOrEmpty(password))
                throw new InvalidInputException("codename and password are required.");

            var user = _userRepository.GetUserByCodename(codename);
            if (user == null || user.password != password)
                throw new InvalidInputException("Invalid codename or password.");

            _currentUser = user;
            return true;
        }

        public static User GetCurrentUser()
        {
            return _currentUser;
        }

        public static void Logout()
        {
            _currentUser = null;
        }

        public static bool IsAdmin()
        {
            return _currentUser?.role == "Admin";
        }

        public static bool IsLoggedIn()
        {
            return _currentUser != null;
        }
    }
}