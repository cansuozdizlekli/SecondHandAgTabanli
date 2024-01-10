using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                builder.Append(hashedBytes[i].ToString());
            }

            return builder.ToString();
        }
    }

    public static bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        // Hash the user input
        string hashedInputPassword = HashPassword(inputPassword);

        // Compare the hashed input with the stored hash
        return string.Equals(hashedInputPassword, hashedPassword);
    }

}

