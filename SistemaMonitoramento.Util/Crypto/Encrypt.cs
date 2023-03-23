using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace SistemaMonitoramento.Util.Crypto
{
    public class Encrypt
    {
        

        public static string DecryptString(string value)
        {
            byte[] b = Convert.FromBase64String(value);
            string decryptedConnectionString = System.Text.Encoding.ASCII.GetString(b);
            return decryptedConnectionString;
        }

        public static string EncryptString(string value)
        {
            byte[] b = System.Text.Encoding.ASCII.GetBytes(value);
            string encryptedConnectionString = Convert.ToBase64String(b);
            return encryptedConnectionString;
        }

        public static string MD5Hash(string value)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
