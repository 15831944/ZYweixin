using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;

namespace cheWeiXin
{
    public class Template
    {
        public static void Add(string accesstoken)
        {

        }

        public class Send_J
        {
            public string touser = "";
            public string template_id = "";
            public string url = "";
            public string data = "[data]";
        }

        public class Send_Back
        {
            public string errcode = "";
            public string errmsg = "";
            public long msgid = 0;
        }
        public static Send_Back Send(string accesstoken, Send_J sd, string datajson)
        {
            //sd.data = datajson;
            string send = JsonConvert.SerializeObject(sd);
            send = send.Replace("\"[data]\"", datajson);
            string postback = Http.HttpWebResponseUtility.HttpPost("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + accesstoken, send);
            Send_Back p = new Send_Back();
            p = (Send_Back)JsonConvert.DeserializeObject(postback, typeof(Send_Back));
            return p;
        }
    }
}
