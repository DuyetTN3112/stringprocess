
using System.Linq;

using System.Text.RegularExpressions;
using StringProcessingApp.Exceptions;

namespace StringProcessingApp.Helpers
{
    public static class Validator
    {
        public static bool ValidatePassword(string password, string name, string last_name, string email, string phone_number, string codename)
        {
            if (string.IsNullOrEmpty(password))
                throw new InvalidInputException("password cannot be empty.");

            if (password.Length < 8)
                throw new InvalidInputException("password must be at least 8 characters long.");

            if (!Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[!@#$%^&*()_+=\[\]{};:<>|./?,-]).+$"))
                throw new InvalidInputException("password must contain at least one uppercase letter and one special character.");

            if (password.Contains(" "))
                throw new InvalidInputException("password cannot contain spaces.");

            if (password == name || password == last_name || password == email || password == phone_number || password == codename)
                throw new InvalidInputException("password cannot be the same as your name, last name, email, phone number, or codename.");

            return true;
        }

        public static bool ValidateCodename(string codename)
        {
            if (string.IsNullOrEmpty(codename))
                throw new InvalidInputException("codename cannot be empty.");

            if (!Regex.IsMatch(codename, @"^[a-zA-Z0-9]+$"))
                throw new InvalidInputException("codename can only contain letters and numbers, no spaces or special characters.");

            return true;
        }

        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new InvalidInputException("email cannot be empty.");

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new InvalidInputException("Invalid email format.");

            return true;
        }

        public static bool ValidatePhoneNumber(string phone_number)
        {
            if (string.IsNullOrEmpty(phone_number))
                throw new InvalidInputException("Phone number cannot be empty.");

            if (!Regex.IsMatch(phone_number, @"^\d+$"))
                throw new InvalidInputException("Phone number can only contain digits.");

            return true;
        }

        public static bool ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new InvalidInputException("name cannot be empty.");

            return true;
        }

        public static bool ValidateInputString(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                throw new InvalidInputException("Input string cannot be empty.");

            if (inputString.Length > 40)
                throw new InvalidInputException("Input string must be up to 40 characters.");

            if (!inputString.All(char.IsUpper))
                throw new InvalidInputException("Input string must contain only capital letters.");

            return true;
        }

        public static bool ValidateShiftValue(string shiftValue)
        {
            if (string.IsNullOrEmpty(shiftValue))
                throw new InvalidInputException("Shift value cannot be empty.");

            if (!int.TryParse(shiftValue, out int n))
                throw new InvalidInputException("Shift value must be a number.");

            if (n < -25 || n > 25)
                throw new InvalidInputException("Shift value must be between -25 and 25.");

            return true;
        }
    }
}