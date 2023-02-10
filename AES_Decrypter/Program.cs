using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        if (!File.Exists("key.keyzor")) 
        {
            Console.WriteLine("No decryption key was found, make sure you have a file named key.keyzor in the same directory as this program.");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0); 
        }
        if (!File.Exists("encrypted.crypzor")) 
        {
            Console.WriteLine("No encrypted file was found, make sure you have a file named encrypted.crypzor in the same directory as this program.");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        string decrypted = Decrypt();
        Console.WriteLine("Decrypted: " + decrypted);
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    static string Decrypt()
    {

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


                using (FileStream encryptedFileStream = File.OpenRead("encrypted.crypzor"))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(encryptedFileStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cryptoStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}