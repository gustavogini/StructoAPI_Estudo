using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Structo.Application.Services.Cryptography
{
    public class PasswordEncripter
    {
        private readonly string _additionalKey;
        public PasswordEncripter(string additionalKey)
        {
            _additionalKey = additionalKey;
        }
        public string Encrypt(string password)
        {
            var newPassword = $"{password}{_additionalKey}";

            var bytes = Encoding.UTF8.GetBytes(newPassword);
            var hasBytes = SHA512.HashData(bytes);

            return StringBytes(hasBytes);
        }

        private static string StringBytes(byte[] bytes)
        {
            var stringBuilder = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                stringBuilder.Append(hex);
            }
            return stringBuilder.ToString();
        }
    
    }
}
