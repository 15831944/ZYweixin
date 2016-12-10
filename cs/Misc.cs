using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace cheWeiXin
{
    public class Misc
    {
        public static string customservice = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";
        /// <summary>
        /// 修改卡卷库存
        /// </summary>
        public static string stockcard = "https://api.weixin.qq.com/card/modifystock?access_token={0}";
        /// <summary>
        /// 查询卡卷
        /// </summary>
        public static string getcard = "https://api.weixin.qq.com/card/get?access_token={0}";
        /// <summary>
        /// 查询用户卡卷
        /// </summary>
        public static string getusercards = "https://api.weixin.qq.com/card/user/getcardlist?access_token={0}";
        /// <summary>
        /// 批量查询用户卡卷
        /// </summary>
        public static string getcards = "https://api.weixin.qq.com/card/batchget?access_token={0}";
        /// <summary>
        /// 解码卡卷
        /// </summary>
        public static string carddecrypt = "https://api.weixin.qq.com/card/code/decrypt?access_token={0}";

        /// <summary>
        /// 消核卡卷
        /// </summary>
        public static string cardconsume = "https://api.weixin.qq.com/card/code/consume?access_token={0}";

        //SHA1
        static public string SHA1_Hash(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "");
            return str_sha1_out;

            //System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str_sha1_in, FormsAuthPasswordFormat.SHA1.ToString());
        }

        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        public static string MD5(string myString)
        {
            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(myString);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
                cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash = cryptHandler.ComputeHash(textBytes);
                string ret = "";
                foreach (byte a in hash)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("x");
                    else
                        ret += a.ToString("x");
                }
                return ret;
            }
            catch
            {
                throw;
            }
        }


    }
}
