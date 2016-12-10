using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
namespace cheWeiXin
{
    /// <summary>
    /// 微信支付类
    /// </summary>
    public class WeinXinPay
    {
        public string backcode = "";
        public string backxml = "";
        public string code_url = "";

        public string post_prepay_id()
        {
            string postback = WeinXinUtility.HttpPost("https://api.mch.weixin.qq.com/pay/unifiedorder", this.Xml);
            XmlDocument xmlDoc = new XmlDocument();
            backxml = postback;
            xmlDoc.LoadXml(postback);
            if (xmlDoc.DocumentElement.SelectSingleNode("err_code") != null)
                backcode = xmlDoc.DocumentElement.SelectSingleNode("err_code").InnerText;

            if (xmlDoc.DocumentElement.SelectSingleNode("prepay_id") != null)
                prepay_id = xmlDoc.DocumentElement.SelectSingleNode("prepay_id").InnerText;

            if (xmlDoc.DocumentElement.SelectSingleNode("code_url") != null)
                code_url = xmlDoc.DocumentElement.SelectSingleNode("code_url").InnerText;

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
            JSAPI,
            NATIVE
        };

        public enum ErrCode
        {
            ORDERPAID
        };

        public bool isdebug = true;

        public string appid = "";
        public string mch_id = "";//商户号
        public string openid = "";
        public string body = "";//商品描述
        public string out_trade_no = "";//商户订单号 
        public decimal total_fee = 0;//总金额
        public string notify_url = "http://tf.cqeasy.com/ws/paytest/payback.aspx";//通知地址 

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

        public WeinXinPay(PayType type, string title, int total_fee, bool isdebug, string appid, string mchid,string key)
        {
            Random rd = new Random();
            nonce_str = rd.Next(999999).ToString();
            nonceStr = nonce_str;
            this.trade_type = type.ToString();


            this.appid = appid;
            this.mch_id = mchid;
            this.key = key;

            this.isdebug = isdebug;

            if (this.isdebug)
            {
                notify_url = "http://tf.cqeasy.com/wxpay/PayFromGZ";//通知地址
                this.total_fee = 1;
            }
            else
            {
                notify_url = "http://tf.cqeasy.com/wxpay/PayFromGZ";//通知地址 
                this.total_fee = total_fee;
            }

            this.body = title;
        }

        public WeinXinPay(PayType type, string title, int total_fee, bool isdebug, string appid, string mch_id, string key, string openid)
        {
            Random rd = new Random();
            nonce_str = rd.Next(999999).ToString();
            nonceStr = nonce_str;
            this.trade_type = type.ToString();

            this.openid = openid;
            this.appid = appid;
            this.mch_id = mch_id;
            this.key = key;

            this.isdebug = isdebug;

            if (this.isdebug)
            {
                notify_url = "http://tf.cqeasy.com/wxpay/PayFromGZ";//通知地址
                this.total_fee = 1;
            }
            else
            {
                notify_url = "http://tf.cqeasy.com/wxpay/PayFromGZ";//通知地址 
                this.total_fee = total_fee;
            }

            this.body = title;
        }

        public string prepay_id = "";
        public string time_stamp = "";
        //js 支付 参数
        public string timeStamp
        {
            get
            {
                if (time_stamp.Length == 0)
                {
                    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    time_stamp = Convert.ToInt64(ts.TotalSeconds).ToString();
                   
                }
                else
                {

                }

                return time_stamp;
            }
        }
        public string nonceStr = "";
        public string signType = "SHA1";
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

                sign = WeinXinUtility.MD5(sign).ToUpper();
                return sign;
            }
        }

        //支付签名
        public string paySignQR
        {
            get
            {
                string string1 = "";
                //string1 += "appId=" + appid + "&";
               
                //string1 += "time_stamp=" + timeStamp + "&";
                //string1 += "nonce_str=" + nonceStr + "&";


                string1 += "appid=" + appid + "&";
                string1 += "mch_id=" + mch_id + "&";
                string1 += "nonce_str=" + nonceStr + "&";
                string1 += "product_id=" + product_id + "&";
                string1 += "time_stamp=" + timeStamp + "&";
                
                

                string sign = string1 + "key=" + this.key;

                sign = WeinXinUtility.MD5(sign).ToUpper();
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

                sign = WeinXinUtility.MD5(sign).ToUpper();
                return sign;
            }
        }
    }
}