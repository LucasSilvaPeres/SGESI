using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Serilog;

namespace Interface.Helpers
{
    public class HashHelper
    {
        public string CreateHash(string value, string salt)
        {
            try
            {
                var valueBytes = KeyDerivation.Pbkdf2(
                                    password: value,
                                    salt: Encoding.UTF8.GetBytes(salt),
                                    prf: KeyDerivationPrf.HMACSHA512,
                                    iterationCount: 10000,
                                    numBytesRequested: 256 / 8);

                return Convert.ToBase64String(valueBytes);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao criar um hash");
                throw;
            }
        }

        public bool Validate(string value, string salt, string hash)
        {
            try
            {
                return CreateHash(value, salt) == hash;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao validar uma senha");
                throw;
            }
        }

        public string CreateSalt()
        {
            try
            {
                byte[] randomBytes = new byte[128 / 8];
                using (var generator = RandomNumberGenerator.Create())
                {
                    generator.GetBytes(randomBytes);
                    return Convert.ToBase64String(randomBytes);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro ao criar um salt");
                throw;
            }
        }
    }
}
