using System;
using System.Collections.Generic;
using System.Text;

namespace cheWeiXin
{
    public class WeiXinAddr
    {
        public string appId = "";
        public string scope = "";
        public string signType = "";
        public string timeStamp = "";
        public string nonceStr = "";
        public string accessToken = "";
        public string url = "";

        public string addrSign
        {
            get
            {
                string keyvaluestring = "accesstoken=" + accessToken + "&appid=" + appId + "&noncestr=" + nonceStr + "&timestamp=" + timeStamp + "&url=" + url;
                //var sign = CryptoJS.SHA1(keyvaluestring).toString();
                return Misc.SHA1_Hash(keyvaluestring);
            }
        }

        public WeiXinAddr(string url, string accesstoken)
        {
            appId = "wx1c2e1ac1e113090b";
            scope = "jsapi_address";
            signType = "sha1";
            timeStamp = Misc.ConvertDateTimeInt(DateTime.Now).ToString();
            //timeStamp = "1384841012";
            Random rd = new Random();
            nonceStr = rd.Next(99999).ToString();

            this.url = url;
            this.accessToken = accesstoken;
        }



    }
}
