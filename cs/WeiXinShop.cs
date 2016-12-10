using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;

namespace cheWeiXin
{
    public class WeiXinShop
    {
        #region 微信小店


        public class product_get_back
        {
            public string errcode = "";
            public string errmsg = "";
            public product product_info = new product();
        }
        /// <summary>
        /// 查询商品
        ///https://api.weixin.qq.com/merchant/get?access_token=ACCESS_TOKEN
        /// </summary>
        /// <param name="cope"></param>
        /// <param name="backtext"></param>
        /// <returns></returns>
        public static product_get_back GetProduct(ref string backtext, string pid)
        {

            string jsonStr = "{\"product_id\": \"" + pid + "\"}";
            backtext = "";
            string url = string.Format("https://api.weixin.qq.com/merchant/get?access_token={0}", API.access_token);
            Dictionary<string, string> dicback = new Dictionary<string, string>();
            backtext = "";
            string json = Http.HttpWebResponseUtility.PostHttpsString(url, jsonStr);
            backtext = json;

            //product p = getproduct(json);

            product_get_back p = new product_get_back();
            p = (product_get_back)JsonConvert.DeserializeObject(json, typeof(product_get_back));

            return p;
        }

        public class product_push_back
        {
            public string errcode = "";
            public string errmsg = "";
            public string product_id = "";
        }
        /// <summary>
        /// 添加商品
        ///https://api.weixin.qq.com/sns/oauth2/access_token?appid=APPID&secret=SECRET&code=CODE&grant_type=authorization_code
        /// </summary>
        /// <param name="cope"></param>
        /// <param name="backtext"></param>
        /// <returns></returns>
        public static product_push_back PushProduct(ref string backtext, product product)
        {

            string jsonStr = JsonConvert.SerializeObject(product);
            backtext = "";
            string url = string.Format("https://api.weixin.qq.com/merchant/create?access_token={0}", API.access_token);
            Dictionary<string, string> dicback = new Dictionary<string, string>();
            backtext = "";
            string json = Http.HttpWebResponseUtility.PostHttpsString(url, jsonStr);
            backtext = json;
            dicback = cheWeiXin.BackReader.get(json);

            product_push_back p = new product_push_back();

            if (dicback.ContainsKey(CodeName.errcode))
            {
                p.errcode = dicback[CodeName.errcode];
                p.errmsg = dicback[CodeName.errmsg];
                if (dicback.ContainsKey("product_id"))
                    p.product_id = dicback["product_id"];
            }

            return p;
        }
        public class property
        {
            public property(string id, string vid)
            {
                this.id = id;
                this.vid = vid;
            }
            public string id = "";
            public string vid = "";
        }
        public class sku_info
        {
            public string id = "";
            public List<string> vid = new List<string>();
        }
        public class main_img
        {
            public List<string> img = new List<string>();
        }
        public class detail
        {
            public string text = "";
        }
        public class attrext
        {
            public class clocation
            {
                public string country = "";
                public string province = "";
                public string city = "";
                public string address = "";
            };
            public clocation location = new clocation();
            public int isPostFree = 0;
            public int isHasReceipt = 0;
            public int isUnderGuaranty = 0;
            public int isSupportReplace = 0;
        }
        public class delivery_info
        {
            public class cexpress
            {
                public cexpress(int id, int price)
                {
                    this.id = id;
                    this.price = price;
                }
                public int id = 0;
                public int price = 0;
            };
            public List<cexpress> express = new List<cexpress>();
            public int delivery_type = 0;
            public int template_id = 0;
        }
        public class sku
        {
            public string sku_id = "";
            public int price = 0;
            public string icon_url = "";
            public string product_code = "";
            public int ori_price = 0;
            public int quantity = 0;
        }

        public class product_base
        {
            public List<string> category_id = new List<string>();
            public List<property> property = new List<property>();
            public string name = "";
            public List<sku_info> sku_info = new List<sku_info>();
            public string main_img = "";
            public List<string> img = new List<string>();
            public List<detail> detail = new List<detail>();
            public string detail_html = "";
            public int buy_limit = 0;
        }

        public class product
        {
            public string product_id = "";
            public product_base product_base = new product_base();
            public List<sku> sku_list = new List<sku>();
            public delivery_info delivery_info = new delivery_info();
            public attrext attrext = new attrext();
        }
        #endregion
    }
}
