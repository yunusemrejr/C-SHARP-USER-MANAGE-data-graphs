using System;
using System.Text.RegularExpressions;

namespace WindowsFormsApp2.Utilities
{
    /// <summary>
    /// Provides input validation methods
    /// </summary>
    public static class InputValidator
    {
        /// <summary>
        /// Validates if a string is a valid integer
        /// </summary>
        public static bool IsValidInteger(string input, out int result)
        {
            return int.TryParse(input, out result);
        }

        /// <summary>
        /// Validates if a string is a valid float
        /// </summary>
        public static bool IsValidFloat(string input, out float result)
        {
            return float.TryParse(input, out result);
        }

        /// <summary>
        /// Validates if a string is not empty or whitespace
        /// </summary>
        public static bool IsNotEmpty(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Validates if a string has a minimum length
        /// </summary>
        public static bool HasMinLength(string input, int minLength)
        {
            return !string.IsNullOrEmpty(input) && input.Length >= minLength;
        }

        /// <summary>
        /// Validates if a string has a maximum length
        /// </summary>
        public static bool HasMaxLength(string input, int maxLength)
        {
            return string.IsNullOrEmpty(input) || input.Length <= maxLength;
        }

        /// <summary>
        /// Validates if a string contains only letters and spaces
        /// </summary>
        public static bool IsAlphabeticWithSpaces(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            
            return Regex.IsMatch(input, @"^[a-zA-Z\s]+$");
        }

        /// <summary>
        /// Validates if a number is within a range
        /// </summary>
        public static bool IsInRange(float value, float min, float max)
        {
            return value >= min && value <= max;
        }

        /// <summary>
        /// Validates if an ID is positive
        /// </summary>
        public static bool IsPositiveInteger(int value)
        {
            return value > 0;
        }
    }
}
