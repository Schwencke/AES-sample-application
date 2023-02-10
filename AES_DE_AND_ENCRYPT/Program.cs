using System;
using System.Security.Cryptography;
using System.Text;


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter plaintext:");
        string plaintext = Console.ReadLine();

        byte[] encrypted = Encrypt(plaintext);
        Console.WriteLine("Encrypted: " + Convert.ToBase64String(encrypted));

        string decrypted = Decrypt(encrypted);
        Console.WriteLine("Decrypted: " + decrypted);
    }

    static byte[] Encrypt(string plaintext)
    {
        byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

        using (Aes aes = Aes.Create())
        {
            aes.Key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            aes.IV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            {
                return encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
            }
        }
    }

    static string Decrypt(byte[] encrypted)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            aes.IV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            {
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}