using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MiCore2d
{
	public class EncryptUtil
	{
		/// <summary>
		/// Constructor
		/// </summary>
		private EncryptUtil()
		{
		}

        /// <summary>
        /// EncryptString.
        /// </summary>
        /// <param name="text">plain text</param>
        /// <param name="iv">initialization vector</param>
        /// <param name="key">encrypt key</param>
        /// <returns>encrypted string(base64)</returns>
        public static string EncryptString(string text, string iv, string key)
		{
			using (Aes aes = Aes.Create())
			{
				aes.BlockSize = 128;
				aes.KeySize = 256;
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;

				aes.IV = Encoding.UTF8.GetBytes(iv);
				aes.Key = Encoding.UTF8.GetBytes(key);

				ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
				byte[] encrypted;

				using (MemoryStream stream = new MemoryStream())
				{
					using (CryptoStream ctStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
					{
						using(StreamWriter sw = new StreamWriter(ctStream))
						{
							sw.Write(text);
						}
						encrypted = stream.ToArray();
					}
				}
				return System.Convert.ToBase64String(encrypted);
			}
		}

        /// <summary>
        /// DecryptString
        /// </summary>
        /// <param name="cipher">encrypted text(Base64)</param>
        /// <param name="iv">initialization vector</param>
        /// <param name="key">encrypt key</param>
        /// <returns>plain text</returns>
        public static string DecryptString(string cipher, string iv, string key)
		{
            using (Aes aes = Aes.Create())
            {
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                aes.IV = Encoding.UTF8.GetBytes(iv);
                aes.Key = Encoding.UTF8.GetBytes(key);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

				string? plain;
				using (MemoryStream stream = new MemoryStream(System.Convert.FromBase64String(cipher)))
				{
                    using (CryptoStream ctStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(ctStream))
                        {
                            plain = sr.ReadLine();
                        }
                    }
                }
				return plain!;
            }
        }

        /// <summary>
        /// DecryptFile.
        /// </summary>
        /// <param name="path">file path</param>
        /// <param name="iv">initialization vector</param>
        /// <param name="key">encrypt key</param>
        /// <returns>Stream(MemoryStream)</returns>
		public static Stream DecryptFile(string path, string iv, string key)
		{
            using (Aes aes = Aes.Create())
            {
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                aes.IV = Encoding.UTF8.GetBytes(iv);
                aes.Key = Encoding.UTF8.GetBytes(key);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (Stream fstream = File.OpenRead(path))
				{
                    using (CryptoStream ctStream = new CryptoStream(fstream, decryptor, CryptoStreamMode.Read))
					{
						MemoryStream stream = new MemoryStream();
						ctStream.CopyTo(stream);
						return stream;
					}

                }
            }
        }

        public static void Test()
        {
            //IV(16 charactor words)
            string iv = "f0321tkmw5E0jb8h";
            //Key(32 charactor words)
            string key = "OS6ynCOaSdRODItHeJ4yPkSd9V7147J3";

            string plainText = "Hello World";

            string encrypedText = EncryptString(plainText, iv, key);
            Log.Debug(encrypedText);
            string decryptText = DecryptString(encrypedText, iv, key);
            Log.Debug(decryptText);
        }
	}
}

