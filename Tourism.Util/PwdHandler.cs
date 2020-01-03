using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Tourism.Util
{
    public class PwdHandler
    {
        /// <summary>
        /// MD532位加密方式
        /// </summary>
        /// <param name="str">用户原始密码</param>
        /// <returns></returns>
        public static string MD5EncryptTo32(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("x2");

            }
            return pwd;
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="content">加密内容</param>
        /// <param name="key">公钥</param>
        /// <returns></returns>
        public static string DESEncode(string content, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(content);

            //建立加密对象的密钥和偏移量    
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法    
            //使得输入密码必须输入英文文本    
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="content">加密内容</param>
        /// <param name="key">公钥</param>
        /// <returns></returns>
        public static string DESDecode(string content, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[content.Length / 2];
            for (int x = 0; x < content.Length / 2; x++)
            {
                int i = (Convert.ToInt32(content.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
    }
}
