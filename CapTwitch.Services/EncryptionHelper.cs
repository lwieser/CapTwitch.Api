using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace CapTwitch.Services;

public static class EncryptionHelper
{
    public static string EncryptStringToBase64_Aes(string plainText, string salt, string password)
    {
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("PlainText");
        if (salt == null || salt.Length <= 0)
            throw new ArgumentNullException("Salt");
        if (salt.Length < 8)
            throw new ArgumentNullException("Salt must be 8 char minimum");
        if (password.Length < 8)
            throw new ArgumentNullException("Password must be 8 char minimum");
        byte[] array;
        using (Aes aes = Aes.Create())
        {
            aes.GenerateIV();
            aes.Padding = PaddingMode.PKCS7;
            byte[] iv = aes.IV;
            byte[] bytes = Encoding.ASCII.GetBytes(salt);
            aes.Key = new Rfc2898DeriveBytes(password, bytes).GetBytes(32);
            if (iv == null || iv.Length == 0)
                throw new NoNullAllowedException("IV");
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, iv);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(iv, 0, iv.Length);
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        streamWriter.Write(plainText);
                    array = memoryStream.ToArray();
                }
            }
        }
        return Convert.ToBase64String(array);
    }

    public static string DecryptStringFromBase64_Aes(
        string base64CipherText,
        string salt,
        string password)
    {
        if (base64CipherText == null || base64CipherText.Length <= 0)
            throw new ArgumentNullException(nameof(base64CipherText));
        if (salt == null || salt.Length <= 0)
            throw new ArgumentNullException("Salt");
        if (password == null || password.Length <= 0)
            throw new ArgumentNullException("Password");
        if (salt.Length < 8)
            throw new ArgumentNullException("Salt must be 8 char minimum");
        if (password.Length < 8)
            throw new ArgumentNullException("Password must be 8 char minimum");
        byte[] buffer1 = Convert.FromBase64String(base64CipherText);
        using (Aes aes = Aes.Create())
        {
            aes.Padding = PaddingMode.PKCS7;
            using (MemoryStream memoryStream = new MemoryStream(buffer1))
            {
                byte[] buffer2 = new byte[16];
                memoryStream.Read(buffer2, 0, 16);
                aes.IV = buffer2;
                byte[] bytes = Encoding.ASCII.GetBytes(salt);
                aes.Key = new Rfc2898DeriveBytes(password, bytes).GetBytes(32);
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        return streamReader.ReadToEnd();
                }
            }
        }
    }

    public class EncryptionResult
    {
        public string Result { get; set; }

        public string Iv { get; set; }
    }


    public static EncryptionResult Encrypt(string text, string key)
    {
        Aes cipher = CipherHelper.CreateCipher(key);
        ICryptoTransform encryptor = cipher.CreateEncryptor();
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        byte[] inputBuffer = bytes;
        int length = bytes.Length;
        byte[] inArray = encryptor.TransformFinalBlock(inputBuffer, 0, length);
        return new EncryptionResult()
        {
            Result = Convert.ToBase64String(inArray),
            Iv = Convert.ToBase64String(cipher.IV)
        };
    }
    public static class CipherHelper
    {
        public static Aes CreateCipher(string base64StringKey)
        {
            byte[] source = Convert.FromBase64String(base64StringKey);
            if (((IEnumerable<byte>)source).Count<byte>() != 32)
                throw new Exception("Key must be a 32 length byte array");
            Aes cipher = Aes.Create();
            cipher.Mode = CipherMode.CBC;
            cipher.Padding = PaddingMode.ISO10126;
            cipher.Key = source;
            return cipher;
        }

        public static byte[] GenerateRandomBytes(int length)
        {
            byte[] data = new byte[length];
            RandomNumberGenerator.Fill((Span<byte>)data);
            return data;
        }
    }
    public static EncryptionResult Encrypt(string text, byte[] key) => EncryptionHelper.Encrypt(text, Convert.ToBase64String(key));

    public static string Decrypt(EncryptionResult encryptionResult, byte[] key) => EncryptionHelper.Decrypt(encryptionResult.Result, encryptionResult.Iv, Convert.ToBase64String(key));

    public static string Decrypt(EncryptionResult encryptionResult, string key) => EncryptionHelper.Decrypt(encryptionResult.Result, encryptionResult.Iv, key);

    public static string Decrypt(string text, string iv, byte[] key) => EncryptionHelper.Decrypt(text, iv, Convert.ToBase64String(key));

    public static string Decrypt(string text, string iv, string key)
    {
        Aes cipher = CipherHelper.CreateCipher(key);
        cipher.IV = Convert.FromBase64String(iv);
        ICryptoTransform decryptor = cipher.CreateDecryptor();
        byte[] numArray = Convert.FromBase64String(text);
        byte[] inputBuffer = numArray;
        int length = numArray.Length;
        return Encoding.UTF8.GetString(decryptor.TransformFinalBlock(inputBuffer, 0, length));
    }
}