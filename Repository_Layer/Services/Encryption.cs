using BC = BCrypt.Net.BCrypt;
using System;

namespace Repository_Layer.Services
{
    public class Encryption
    {
        public string encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty.");

            int saltLength = new Random().Next(10,13);
            string generatedSalt = BC.GenerateSalt(saltLength);

            string hashPassword = BC.HashPassword(password,generatedSalt);
            return hashPassword;
        }

        public bool matchPassword(string password, string storedHashPassword)
        {
            try
            {
                // Verify password using stored hash
                return BC.Verify(password, storedHashPassword);
            }
            catch
            {
                return false;
            }
        }
    }
}
