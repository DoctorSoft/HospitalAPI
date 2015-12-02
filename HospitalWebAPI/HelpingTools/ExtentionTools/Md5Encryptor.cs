using System.Security.Cryptography;
using System.Text;

namespace HelpingTools.ExtentionTools
{
    public class Md5Encryptor
    {
        public string GetMd5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.ASCII.GetBytes(text));

            //get hash result after compute it
            var result = md5.Hash;

            var strBuilder = new StringBuilder();
            foreach (var nextHashByte in result)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(nextHashByte.ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
