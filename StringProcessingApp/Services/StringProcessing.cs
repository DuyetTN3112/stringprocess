
using System.Linq;

using StringProcessingApp.Exceptions;

namespace StringProcessingApp.Services
{
    public class StringProcessing
    {
        private string _inputString;
        private int _shiftValue;

        public string InputString
        {
            get => _inputString;
            set
            {
                if (value.Length > 40 || !value.All(char.IsUpper))
                    throw new InvalidInputException("Input string must be up to 40 characters and contain only capital letters.");
                _inputString = value;
            }
        }

        public int ShiftValue
        {
            get => _shiftValue;
            set
            {
                if (value < -25 || value > 25)
                    throw new InvalidInputException("Shift value must be between -25 and 25.");
                _shiftValue = value;
            }
        }

        public StringProcessing(string inputString, int shiftValue)
        {
            InputString = inputString;
            ShiftValue = shiftValue;
        }

        // Default constructor for form initialization
        public StringProcessing()
        {
            _inputString = string.Empty;
            _shiftValue = 0;
        }

        public string Encode()
        {
            return new string(_inputString.Select(c => (char)(((c - 'A' + _shiftValue + 26) % 26) + 'A')).ToArray());
        }

        public int[] InputCode()
        {
            return _inputString.Select(c => (int)c).ToArray();
        }

        public int[] OutputCode()
        {
            return Encode().Select(c => (int)c).ToArray();
        }

        public string Sort()
        {
            return new string(_inputString.OrderBy(c => c).ToArray());
        }

        public string Print()
        {
            return Encode();
        }
        public string GetInputCodesAsString()
        {
            return string.Join(" ", InputCode());
        }

        public string GetOutputCodesAsString()
        {
            return string.Join(" ", OutputCode());
        }
    }
}