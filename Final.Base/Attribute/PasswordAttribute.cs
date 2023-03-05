﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Final.Base.Attribute
{
	public class PasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string source = value.ToString();

            // Validate pwd must be MD5 format
            if (!Regex.IsMatch(source, "^[0-9a-fA-F]{32}$", RegexOptions.Compiled))
                return new ValidationResult("Password must MD5 format.");

            return ValidationResult.Success;
        }
    }
}

