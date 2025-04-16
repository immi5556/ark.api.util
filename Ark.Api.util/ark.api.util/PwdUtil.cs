using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ark.net.util
{
    public class PwdUtil
    {
        public static (bool, List<string>) ValidatePwd(string pwd)
        {
            var validator = new PasswordPolicyValidator();
            var result = validator.ValidatePassword(pwd);
            return (result.IsValid, result.Errors);
        }
    }

    public class PasswordPolicyValidator
    {
        // Configuration properties (can be set via constructor or app settings)
        public int MinimumLength { get; set; } = 8;
        public int MaximumLength { get; set; } = 64;
        public bool RequireUppercase { get; set; } = true;
        public bool RequireLowercase { get; set; } = true;
        public bool RequireDigit { get; set; } = true;
        public bool RequireSpecialCharacter { get; set; } = true;
        public int MinimumUniqueCharacters { get; set; } = 4;
        public int MaximumConsecutiveRepeatingChars { get; set; } = 2;
        public bool PreventCommonPasswords { get; set; } = true;
        public string[] CommonPasswords { get; set; } = new string[]
        {
        "password", "123456", "qwerty", "letmein", "welcome",
        "admin", "abc123", "password1", "12345678", "sunshine"
        };

        public PasswordPolicyValidator() { }

        public PasswordPolicyValidator(int minLength, int maxLength, bool requireUpper,
            bool requireLower, bool requireDigit, bool requireSpecialChar)
        {
            MinimumLength = minLength;
            MaximumLength = maxLength;
            RequireUppercase = requireUpper;
            RequireLowercase = requireLower;
            RequireDigit = requireDigit;
            RequireSpecialCharacter = requireSpecialChar;
        }

        public PasswordValidationResult ValidatePassword(string password)
        {
            var result = new PasswordValidationResult { IsValid = true };

            if (string.IsNullOrWhiteSpace(password))
            {
                result.IsValid = false;
                result.Errors.Add("Password cannot be empty");
                return result;
            }

            // Check length requirements
            if (password.Length < MinimumLength)
            {
                result.IsValid = false;
                result.Errors.Add($"Password must be at least {MinimumLength} characters long");
            }

            if (password.Length > MaximumLength)
            {
                result.IsValid = false;
                result.Errors.Add($"Password cannot exceed {MaximumLength} characters");
            }

            // Check character requirements
            if (RequireUppercase && !Regex.IsMatch(password, "[A-Z]"))
            {
                result.IsValid = false;
                result.Errors.Add("Password must contain at least one uppercase letter (A-Z)");
            }

            if (RequireLowercase && !Regex.IsMatch(password, "[a-z]"))
            {
                result.IsValid = false;
                result.Errors.Add("Password must contain at least one lowercase letter (a-z)");
            }

            if (RequireDigit && !Regex.IsMatch(password, "[0-9]"))
            {
                result.IsValid = false;
                result.Errors.Add("Password must contain at least one digit (0-9)");
            }

            if (RequireSpecialCharacter && !Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            {
                result.IsValid = false;
                result.Errors.Add("Password must contain at least one special character");
            }

            // Check for unique characters
            if (MinimumUniqueCharacters > 0)
            {
                var uniqueChars = new System.Collections.Generic.HashSet<char>(password);
                if (uniqueChars.Count < MinimumUniqueCharacters)
                {
                    result.IsValid = false;
                    result.Errors.Add($"Password must contain at least {MinimumUniqueCharacters} unique characters");
                }
            }

            // Check for consecutive repeating characters
            if (MaximumConsecutiveRepeatingChars > 0)
            {
                if (HasConsecutiveRepeatingChars(password, MaximumConsecutiveRepeatingChars + 1))
                {
                    result.IsValid = false;
                    result.Errors.Add($"Password cannot have more than {MaximumConsecutiveRepeatingChars} consecutive repeating characters");
                }
            }

            // Check against common passwords
            if (PreventCommonPasswords && CommonPasswords != null)
            {
                if (Array.Exists(CommonPasswords, p => p.Equals(password, StringComparison.OrdinalIgnoreCase)))
                {
                    result.IsValid = false;
                    result.Errors.Add("Password is too common and not allowed");
                }
            }

            return result;
        }

        private bool HasConsecutiveRepeatingChars(string input, int maxConsecutive)
        {
            if (string.IsNullOrEmpty(input) || maxConsecutive <= 1 || input.Length < maxConsecutive)
                return false;

            int consecutiveCount = 1;
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == input[i - 1])
                {
                    consecutiveCount++;
                    if (consecutiveCount >= maxConsecutive)
                        return true;
                }
                else
                {
                    consecutiveCount = 1;
                }
            }

            return false;
        }
    }

    public class PasswordValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public override string ToString()
        {
            return IsValid ? "Password is valid" : string.Join(Environment.NewLine, Errors);
        }
    }
}
