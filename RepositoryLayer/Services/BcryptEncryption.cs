using System;
using System.Collections.Generic;
using System.Text;
using Pk = BCrypt.Net.BCrypt;

namespace RepositoryLayer.Services
{
    public class BcryptEncryption
    {
        public string HashPassGenerator(string input)
        {
            if (input == null)
            {
                throw new Exception("Please Enter Password");

            }

            int Salt = new Random().Next(10, 14);

            string SaltGenerated = Pk.GenerateSalt(Salt);
            string HashedPassword = Pk.HashPassword(input, SaltGenerated);
            return HashedPassword;



        }

        public bool MatchPass(string userpass, string hashpass)
        {
            try
            {
                return Pk.Verify(userpass, hashpass);
            }
            catch
            {
                return false;
            }
        }
    }
}
