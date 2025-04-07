using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace BankSystem.API.Validation
{
    public class ValidationImageAttribute : ValidationAttribute
    {
        private readonly HashSet<string> _allowedExtensions;
        private readonly long? _maxSizeBytes;

        public ValidationImageAttribute(string allowedExtensions, int maxSizeMB = 3) 
        {
            _allowedExtensions = new HashSet<string>(
                allowedExtensions.Split(',')
                    .Select(e => e.Trim().ToLowerInvariant())
            );

            if (maxSizeMB > 0)
            {
                _maxSizeBytes = maxSizeMB * 1024 * 1024; 
            }
        }

        public override bool IsValid(object value)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();

                if (string.IsNullOrEmpty(extension) || !_allowedExtensions.Contains(extension))
                {
                    ErrorMessage = $"Invalid file extension. Allowed extensions are: {string.Join(", ", _allowedExtensions)}";
                    return false;
                }

                if (_maxSizeBytes.HasValue && file.Length > _maxSizeBytes.Value)
                {
                    ErrorMessage = $"File size is too large. Maximum allowed size is {_maxSizeBytes.Value / (1024 * 1024)}MB";
                    return false;
                }
            }

            return true;
        }
    }
}
