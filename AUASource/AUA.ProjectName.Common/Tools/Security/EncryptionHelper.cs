using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using AUA.ProjectName.Common.Tools.Config.JsonSetting;

namespace AUA.ProjectName.Common.Tools.Security
{
    public sealed class EncryptionHelper
    {
        public static string AesEncryptKeyId(string clearId, long userId)
        {
            var encryptionKey = userId + AppSetting.BaseEncryptionKeyId;

            return AesEncryptString(clearId, encryptionKey);
        }

        public static string AesDecryptKeyId(string cipherId, long userId)
        {
            var encryptionKey = userId + AppSetting.BaseEncryptionKeyId;


            return AesDecryptString(cipherId, encryptionKey);
        }

        public static string AesEncryptString(string clearText)
        {
            var encryptionKey = AppSetting.DataEncryptionKey;

            return AesEncryptString(clearText, encryptionKey);
        }

        public static string AesEncryptString(string clearText, string encryptionKey)
        {

            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            string encryptText;

            using (var encryptor = Aes.Create())
            {


                var pdb = new Rfc2898DeriveBytes(
                    encryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                if (encryptor == null)
                    return string.Empty;


                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);


                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    encryptText = Convert.ToBase64String(ms.ToArray());

                }
            }

            return encryptText;
        }

        public static string AesDecryptString(string cipherText)
        {
            var encryptionKey = AppSetting.DataEncryptionKey;

            try
            {
                return AesDecryptString(cipherText, encryptionKey);
            }
            catch
            {
                return string.Empty;

            }

        }

        public static string AesDecryptString(string cipherText, string encryptionKey)
        {

            try
            {
                var cipherBytes = Convert.FromBase64String(cipherText);


                using var encryptor = Aes.Create();
                var pdb = new Rfc2898DeriveBytes(
                    encryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });


                if (encryptor == null)
                    return string.Empty;


                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using var ms = new MemoryStream();
                using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }

                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
            catch
            {
                return string.Empty;
            }

            return cipherText;

        }

        public static string GetSha256Hash(string input)
        {
            using var hashAlgorithm = new SHA256CryptoServiceProvider();
            var byteValue = Encoding.UTF8.GetBytes(input);
            var byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static Guid CreateCryptographicallySecureGuid()
        {
            var rand = RandomNumberGenerator.Create();

            var bytes = new byte[16];
            rand.GetBytes(bytes);
            return new Guid(bytes);
        }


        public static string GetSha512Hash(string text)
        {

            return BitConverter
                .ToString(new SHA512CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(text)))
                .Replace("-", string.Empty)
                .ToUpper();
        }

        public static string EncryptTripleDes(string toEncrypt)
        {
            return EncryptTripleDes(toEncrypt, AppSetting.DataEncryptionKey);
        }

        public static string EncryptTripleDes(string toEncrypt, string securityKey)
        {
            byte[] keyArray;
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            var hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(securityKey));

            hashmd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateEncryptor();

            var resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);

            tdes.Clear();

            return CorrectBase64UrlEncode(resultArray);
        }

        public static string DecryptTripleDes(string cipherString)
        {
            return DecryptTriple(cipherString, AppSetting.DataEncryptionKey);
        }

        public static string DecryptTriple(string cipherString, string securityKey)
        {
            var toEncryptArray = CorrectBase64UrlDecode(cipherString);
            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(securityKey));

            hashmd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();

            return Encoding.UTF8.GetString(resultArray);
        }

        private static string CorrectBase64UrlEncode(byte[] arg)
        {
            var value = Convert.ToBase64String(arg);

            return value.Split('=')[0]
                         .Replace('+', '-')
                         .Replace('/', '_'); 
        }

        private static byte[] CorrectBase64UrlDecode(string arg)
        {
            var value = arg;
            value = value.Replace('-', '+'); 
            value = value.Replace('_', '/'); 

            switch (value.Length % 4) 
            {
                case 0: break; 
                case 2: value += "=="; break; 
                case 3: value += "="; break; 
                default:
                    throw new Exception(
             "Illegal base64url string!");
            }

            return Convert.FromBase64String(value);
        }
    }
}
