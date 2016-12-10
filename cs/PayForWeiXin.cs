using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace cheWeiXin
{
    public class PayForWeiXin
    {
        
        public string backcode = "";

        public string post_prepay_id()
        {
            string postback = Http.HttpWebResponseUtility.HttpPost("https://api.mch.weixin.qq.com/pay/unifiedorder", this.Xml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(postback);
            if (xmlDoc.DocumentElement.SelectSingleNode("err_code") != null)
                backcode = xmlDoc.DocumentElement.SelectSingleNode("err_code").InnerText;

            if (xmlDoc.DocumentElement.SelectSingleNode("prepay_id") != null)
                prepay_id = xmlDoc.DocumentElement.SelectSingleNode("prepay_id").InnerText;

            return postback;
        }
        public string Xml
        {
            get
            {
                //$xml.="<".$key.">".$val."</".$key.">"; 
                string xml = "<xml>";
                if (appid.Length > 0) xml += "<appid>" + appid + "</appid>";
                if (attach.Length > 0) xml += "<attach><![CDATA[" + attach + "]]></attach>";
                if (auth_code.Length > 0) xml += "<auth_code>" + auth_code + "</auth_code>";
                if (body.Length > 0) xml += "<body><![CDATA[" + body + "]]></body>";
                if (device_info.Length > 0) xml += "<device_info>" + device_info + "</device_info>";
                if (goods_tag.Length > 0) xml += "<goods_tag>" + goods_tag + "</goods_tag>";
                //if (key.Length > 0) xml += "<appid>" + appid + "</appid>"; 
                if (mch_id.Length > 0) xml += "<mch_id>" + mch_id + "</mch_id>";
                if (nonce_str.Length > 0) xml += "<nonce_str>" + nonce_str + "</nonce_str>";
                if (notify_url.Length > 0) xml += "<notify_url>" + notify_url + "</notify_url>";
                if (openid.Length > 0) xml += "<openid>" + openid + "</openid>";
                if (out_trade_no.Length > 0) xml += "<out_trade_no>" + out_trade_no + "</out_trade_no>";
                if (product_id.Length > 0) xml += "<product_id>" + product_id + "</product_id>";
                if (sub_mch_id.Length > 0) xml += "<appid>" + appid + "</appid>";
                if (spbill_create_ip.Length > 0) xml += "<spbill_create_ip>" + spbill_create_ip + "</spbill_create_ip>";
                if (total_fee > 0) xml += "<total_fee>" + total_fee + "</total_fee>";
                if (time_start.Length > 0) xml += "<time_start>" + time_start + "</time_start>";
                if (time_expire.Length > 0) xml += "<time_expire>" + time_expire + "</time_expire>";
                xml += "<trade_type>" + trade_type + "</trade_type>";
                xml += "<sign><![CDATA[" + sign + "]]></sign>";
                xml += "</xml>";

                return xml;
            }
        }
        public enum PayType
        {
            JSAPI
        };

        public enum ErrCode
        {
            ORDERPAID
        };

        public string appid = "";
        public string mch_id = "";//商户号
        public string openid = "";
        public string body = "";//商品描述
        public string out_trade_no = "";//商户订单号 
        public decimal total_fee = 0;//总金额
        public string notify_url = "http://cq.xiaoche5.com/ws/pay/payback.aspx";//通知地址 

        public string nonce_str = "";


        public string sub_mch_id = "";//子商户号 
        public string device_info = "";//设备号 
        public string attach = "";//附加数据 
        public string time_start = "";//交易起始时间 
        public string time_expire = "";//交易结束时间 
        public string goods_tag = "";//商品标记 
        public string product_id = "";//商品 

        public string auth_code = "";
        public string key = "";//商户支付密匙
        public string spbill_create_ip = "";

        public string trade_type = "";//交易类型 

        public PayForWeiXin(PayType type)
        {
            Random rd = new Random();
            nonce_str = rd.Next(999999).ToString();
            nonceStr = rd.Next(999999).ToString();
            this.trade_type = type.ToString();
        }

        public string prepay_id = "";
        //js 支付 参数
        public string timeStamp
        {
            get
            {
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return Convert.ToInt64(ts.TotalSeconds).ToString();
            }
        }
        public string nonceStr = "";
        public string signType = "MD5";
        public string package
        {
            get
            {
                return "prepay_id=" + prepay_id;
            }
        }
        //支付签名
        public string paySign
        {
            get
            {
                string string1 = "";
                string1 += "appId=" + appid + "&";
                string1 += "nonceStr=" + nonceStr + "&";
                string1 += "package=" + package + "&";
                string1 += "signType=" + signType + "&";
                string1 += "timeStamp=" + timeStamp + "&";

                string sign = string1 + "key=" + this.key;

                sign = Misc.MD5(sign).ToUpper();
                return sign;
            }
        }
        //js 支付 参数 完

        //签名
        public string sign
        {
            get
            {
                string string1 = "";
                if (appid.Length > 0) string1 += "appid=" + appid + "&";
                if (attach.Length > 0) string1 += "attach=" + attach + "&";
                if (auth_code.Length > 0) string1 += "auth_code=" + auth_code + "&";
                if (body.Length > 0) string1 += "body=" + body + "&";
                if (device_info.Length > 0) string1 += "device_info=" + device_info + "&";
                if (goods_tag.Length > 0) string1 += "goods_tag=" + goods_tag + "&";
                //if (key.Length > 0) string1 += "key=" + key + "&";
                if (mch_id.Length > 0) string1 += "mch_id=" + mch_id + "&";
                if (nonce_str.Length > 0) string1 += "nonce_str=" + nonce_str + "&";
                if (notify_url.Length > 0) string1 += "notify_url=" + notify_url + "&";
                if (openid.Length > 0) string1 += "openid=" + openid + "&";
                if (out_trade_no.Length > 0) string1 += "out_trade_no=" + out_trade_no + "&";
                if (product_id.Length > 0) string1 += "product_id=" + product_id + "&";
                if (sub_mch_id.Length > 0) string1 += "sub_mch_id=" + sub_mch_id + "&";
                if (spbill_create_ip.Length > 0) string1 += "spbill_create_ip=" + spbill_create_ip + "&";
                if (total_fee > 0) string1 += "total_fee=" + total_fee + "&";
                if (time_start.Length > 0) string1 += "time_start=" + time_start + "&";
                if (time_expire.Length > 0) string1 += "time_expire=" + time_expire + "&";
                string1 += "trade_type=" + trade_type + "&";
                //if (trade_type.Length > 0) a += "trade_type=" + trade_type + "&";

                string sign = string1 + "key=" + this.key;

                sign = Misc.MD5(sign).ToUpper();
                return sign;
            }
        }
    }
}
