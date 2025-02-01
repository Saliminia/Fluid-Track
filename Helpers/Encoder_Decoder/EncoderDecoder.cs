using System;

using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;

namespace DMR
{
    public class EncoderDecoder
    {
        //-------------------------------------------------------
        public static string EncodePassword(string val)
        {
            if (val.Length > 10)
                return StringCipher.Encrypt(val, val.Substring(2, 6) + "+ke8UB_@" + val.Substring(3, 2));
            else if (val.Length > 8)
                return StringCipher.Encrypt(val, val.Substring(3, 3) + "/|C,*~sq@1" + val.Substring(1, 5));
            else if (val.Length > 6)
                return StringCipher.Encrypt(val, val.Substring(1, 4) + "_6F.)n%5spM" + val.Substring(3, 1));
            else if (val.Length > 5)
                return StringCipher.Encrypt(val, val.Substring(4, 2) + "=`8@v^dl(K-4#_" + val.Substring(2, 2));
            else
                return StringCipher.Encrypt(val, "w)&_R*(%%$da2wP");
        }
        //-------------------------------------------------------
        //return success
        public static bool SaveDataSetToEncodedFile_V0001_0000(DataSet ds, string fileName)
        {
            try
            {
                if (ds == null || ds.Tables == null || ds.Tables.Count == 0)
                    return false;

                ds.RemotingFormat = SerializationFormat.Binary;

                for (int i = 0; i < ds.Tables.Count; i++)
                    ds.Tables[i].RemotingFormat = SerializationFormat.Binary;

                byte[] key = new byte[8];

                for (int i = 0; i < key.Length; i++)
                    key[i] = (byte)(((i * i) % (i + 17)) % 256);

                byte[] iv = Encoding.UTF8.GetBytes("_+m,eW21");
                byte b = 9; b = (byte)(b * b + 23);
                iv[2] = (byte)(b % 15);
                iv[6] = (byte)(b % 98);

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                // Encryption
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                using (var cryptoStream = new CryptoStream(fs, des.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    // This is where you serialize the class
                    formatter.Serialize(cryptoStream, ds);

                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        public static DataSet LoadDataSetFromEncodedFile_V0001_0000(string fileName)
        {
            try
            {
                byte[] key = new byte[8];

                for (int i = 0; i < key.Length; i++)
                    key[i] = (byte)(((i * i) % (i + 17)) % 256);

                byte[] iv = Encoding.UTF8.GetBytes("_+m,eW21");
                byte b = 9; b = (byte)(b * b + 23);
                iv[2] = (byte)(b % 15);
                iv[6] = (byte)(b % 98);

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                // Decryption
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                using (var cryptoStream = new CryptoStream(fs, des.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    // This is where you deserialize the class
                    return (DataSet)formatter.Deserialize(cryptoStream);
                }
            }
            catch (Exception)
            { 
            }

            return null;
        }
        //-------------------------------------------------------
    }
}
