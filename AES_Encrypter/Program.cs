using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        if (!File.Exists("key.keyzor"))
        {
            Console.WriteLine("A encryption key was not found, ensure that you have a key.keyzor file in the same directory as this program.");
            Console.WriteLine("Do you want me to generate a key for you? (y/n)");
            string answer = Console.ReadLine();
            if (answer == "y")
            {
                Console.WriteLine("Generating key...");
                CreateAESKey();
                Console.WriteLine("Key generated");
            }
            else
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
        
        Start:
            Console.WriteLine("Enter plaintext:");
            string plaintext = Console.ReadLine();
            if (plaintext == null || plaintext == String.Empty)
            {
            Console.WriteLine("Please enter a message to encrypt.");
            goto Start;
            }
            byte[] encrypted = Encrypt(plaintext);
            Console.WriteLine("Your file was encrypted: " + Convert.ToBase64String(encrypted));
            try
            {
            var fileName = "encrypted";
            var fileExtension = ".crypzor";
                File.WriteAllBytes("encrypted.crypzor", encrypted);
                Console.WriteLine($"The encrypted file was stored in the file {fileName}{fileExtension}");
                Console.WriteLine("Press any key to exit.");
                Console.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine("The file was encrypted, but not saved. Something went wrong while saving the file.");
                Console.WriteLine("System message: " + e.Message);
            }
        

       

  
    }

    static byte[] Encrypt(string plaintext)
    {
        byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

        using (FileStream keyFileStream = File.OpenRead("key.keyzor"))
        {
            byte[] key = new byte[16];
            int keyLength = keyFileStream.Read(key, 0, key.Length);

            byte[] iv = new byte[16];
            int ivLength = keyFileStream.Read(iv, 0, iv.Length);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    return encryptor.TransformFinalBlock(plaintextBytes, 0, plaintextBytes.Length);
                }
            }
        }
    }

    static byte[] CreateAESKey() 
    {

        using (Aes aes = Aes.Create())
        {
            aes.Key = new byte[] { 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67 };
            aes.IV = new byte[] { 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163 };

            using (FileStream keyFileStream = File.Create("key.keyzor"))
            {
                keyFileStream.Write(aes.Key, 0, aes.Key.Length);
                keyFileStream.Write(aes.IV, 0, aes.IV.Length);
            }
            Console.WriteLine("A key was generate forKey: " + Convert.ToBase64String(aes.Key));
            return aes.Key;
        }

    }
}