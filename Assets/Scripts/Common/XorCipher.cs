using System;
using System.Text;

public static class XorCipher
{
    public static string EncryptToBase64(string plainText, string key)
    {
        byte[] p = Encoding.UTF8.GetBytes(plainText);
        byte[] k = Encoding.UTF8.GetBytes(key);
        byte[] c = XorBytes(p, k);
        return Convert.ToBase64String(c);
    }

    public static string DecryptFromBase64(string base64Cipher, string key)
    {
        byte[] c = Convert.FromBase64String(base64Cipher);
        byte[] k = Encoding.UTF8.GetBytes(key);
        byte[] p = XorBytes(c, k);
        return Encoding.UTF8.GetString(p);
    }

    static byte[] XorBytes(byte[] data, byte[] key)
    {
        byte[] outBytes = new byte[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            outBytes[i] = (byte)(data[i] ^ key[i % key.Length]);
        }
        return outBytes;
    }
}
