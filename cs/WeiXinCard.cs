using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cheWeiXin
{
    public class WeiXinCard
    {
        public enum CardType
        {
            /// <summary>
            /// 团购券
            /// </summary>
            GROUPON,
            /// <summary>
            ///  现金券
            /// </summary>
            CASH,
            /// <summary>
            /// 折扣券
            /// </summary>
            DISCOUNT,
            /// <summary>
            /// 礼品券
            /// </summary>
            GIFT,
            /// <summary>
            /// 优惠券
            /// </summary>
            GENERAL_COUPON
        }

        public enum Type
        {
            DATE_TYPE_FIX_TIME_RANGE,
            DATE_TYPE_FIX_TERM
        }

        public enum Code_Type
        {
            /// <summary>
            /// 文本
            /// </summary>
            CODE_TYPE_TEXT,
            /// <summary>
            /// 一维码 
            /// </summary>
            CODE_TYPE_BARCODE,
            /// <summary>
            /// 二维码
            /// </summary>
            CODE_TYPE_QRCODE,
            /// <summary>
            /// 二维码无code显示
            /// </summary>
            CODE_TYPE_ONLY_QRCODE,
            /// <summary>
            /// 一维码无code显示
            /// </summary>
            CODE_TYPE_ONLY_BARCODE
        }

        public class Card
        {
            //public 
            public string card_id = "";
        }
        /// <summary>
        /// 折扣卷
        /// </summary>
        public class cdiscount
        {
            public base_info base_info = new base_info();
            /// <summary>
            /// 折扣券专用，表示打折额度（百分比）。填30就是七折。
            /// </summary>
            public int discount = 0;
            /// <summary>
            /// 
            /// </summary>
            public string card_type = CardType.DISCOUNT.ToString();
        }

        /// <summary>
        /// 团购卷
        /// </summary>
        public class groupon
        {
            public base_info base_info = new base_info();
            /// <summary>
            /// 团购券专用，团购详情
            /// </summary>
            public string deal_detail = "";
            /// <summary>
            /// 
            /// </summary>
            public string card_type = CardType.GROUPON.ToString();
        }

        /// <summary>
        /// 代金券
        /// </summary>
        public class cash
        {
            public base_info base_info = new base_info();
            /// <summary>
            /// 代金券专用，表示起用金额。（单位为分）
            /// </summary>
            public int least_cost = 0;
            /// <summary>
            /// 代金券专用，表示减免金额。（单位为分）
            /// </summary>
            public int reduce_cost = 0;
            /// <summary>
            /// 
            /// </summary>
            public string card_type = CardType.CASH.ToString();
        }

        /// <summary>
        /// 礼品券
        /// </summary>
        public class cgift
        {
            public base_info base_info = new base_info();
            /// <summary>
            /// 礼品券专用，填写礼品的名称。
            /// </summary>
            public string gift = "";
            /// <summary>
            /// 
            /// </summary>
            public string card_type = CardType.GIFT.ToString();
        }

        /// <summary>
        /// 优惠券
        /// </summary>
        public class general_coupon
        {
            public base_info base_info = new base_info();
            /// <summary>
            /// 优惠券专用，填写优惠详情。
            /// </summary>
            public string default_detail = "";
            /// <summary>
            /// 
            /// </summary>
            public string card_type = CardType.GENERAL_COUPON.ToString();
        }

        public class sku
        {
            /// <summary>
            /// 数量
            /// </summary>
            public int quantity = 0;
            /// <summary>
            /// 总数量（创建的时候不需要填写）
            /// </summary>
            public int total_quantity = 0;
        }

        public class date_info
        {
            /// <summary>
            /// DATE_TYPE_FIX_TIME_RANGE 表示固定日期区间，
            /// DATE_TYPE_FIX_TERM表示固定时长（自领取后按天算。
            /// </summary>
            public string type = "";
            /// <summary>
            /// type为DATE_TYPE_FIX_TIME_RANGE时专用，表示起用时间。
            /// 从1970年1月1日00:00:00至起用时间的秒数，最终需转换为字符串形态传入。
            /// （东八区时间，单位为秒）
            /// </summary>
            public uint begin_timestamp = 0;
            /// <summary>
            /// type为DATE_TYPE_FIX_TIME_RANGE时专用，表示结束时间，
            /// 建议设置为截止日期的23:59:59过期。（东八区时间，单位为秒）
            /// </summary>
            public uint end_timestamp = 0;
            /// <summary>
            /// type为DATE_TYPE_FIX_TERM时专用，表示自领取后多少天内有效，领取后当天有效填写0。（单位为天）
            /// </summary>
            public int fixed_term = 0;
            /// <summary>
            /// type为DATE_TYPE_FIX_TERM时专用，表示自领取后多少天开始生效。（单位为天）
            /// </summary>
            public int fixed_begin_term = 0;
        }

        public class base_info
        {
            //必填
            /// <summary>
            /// 卡券的商户logo，建议像素为300*300
            /// </summary>
            public string logo_url = "";
            /// <summary>
            /// Code展示类型，
            /// "CODE_TYPE_TEXT"，文本；
            /// "CODE_TYPE_BARCODE"，一维码 ；
            /// "CODE_TYPE_QRCODE"，二维码；
            /// "CODE_TYPE_ONLY_QRCODE",二维码无code显示；
            /// "CODE_TYPE_ONLY_BARCODE",一维码无code显示；
            /// </summary>
            public string code_type = "";
            /// <summary>
            /// 商户名字,字数上限为12个汉字
            /// </summary>
            public string brand_name = "";
            /// <summary>
            /// 卡券名，字数上限为9个汉字。(建议涵盖卡券属性、服务及金额)。
            /// </summary>
            public string title = "";
            /// <summary>
            /// 券名，字数上限为18个汉字。
            /// </summary>
            public string sub_title = "";
            /// <summary>
            /// 券颜色。按色彩规范标注填写Color010-Color100
            /// </summary>
            public string color = "";
            /// <summary>
            /// 卡券使用提醒，字数上限为16个汉字。
            /// </summary>
            public string notice = "";
            /// <summary>
            /// 卡券使用说明，字数上限为1024个汉字。
            /// </summary>
            public string description = "";
            /// <summary>
            /// 商品信息。
            /// </summary>
            public sku sku = new sku();
            /// <summary>
            /// 使用日期，有效期的信息。
            /// </summary>
            public date_info date_info = new date_info();
            

            //非必填字段
            /// <summary>
            /// 是否自定义Code码。填写true或false，默认为false。通常自有优惠码系统的开发者选择自定义Code码，并在卡券投放时带入Code码，详情见
            /// </summary>
            public bool use_custom_code = false;
            /// <summary>
            /// 是否指定用户领取，填写true或false。默认为false。通常指定特殊用户群体投放卡券或防止刷券时选择指定用户领取。
            /// </summary>
            public bool bind_openid = true;
            /// <summary>
            /// 客服电话。
            /// </summary>
            public string service_phone = "";
            /// <summary>
            /// 门店位置ID。调用POI门店管理接口获取门店位置ID。具备线下门店的商户为必填。
            /// </summary>
            public List<string> location_id_list = new List<string>();
            /// <summary>
            /// 第三方来源名，例如同程旅游、大众点评。
            /// </summary>
            public string source = "";
            /// <summary>
            /// 自定义跳转外链的入口名字。
            /// </summary>
            public string custom_url_name = "";
            /// <summary>
            /// 自定义跳转的URL。
            /// </summary>
            public string custom_url = "";
            /// <summary>
            /// 显示在入口右侧的提示语。
            /// </summary>
            public string custom_url_sub_title = "";
            /// <summary>
            /// 营销场景的自定义入口名称。
            /// </summary>
            public string promotion_url_name = "";
            /// <summary>
            /// 入口跳转外链的地址链接。
            /// </summary>
            public string promotion_url = "";
            /// <summary>
            /// 显示在营销入口右侧的提示语。
            /// </summary>
            public string promotion_url_sub_title = "";
            /// <summary>
            /// 每人可领券的数量限制,不填写默认为50。
            /// </summary>
            public int get_limit = 0;
            /// <summary>
            /// 卡券领取页面是否可分享。
            /// </summary>
            public bool can_share = false;
            /// <summary>
            /// 卡券是否可转赠。
            /// </summary>
            public bool can_give_friend = false;

            public string status = "";
        }

        public enum Status
        {
            /// <summary>
            /// 待审核
            /// </summary>
            CARD_STATUS_NOT_VERIFY,
            /// <summary>
            /// 审核失败
            /// </summary>
            CARD_STATUS_VERIFY_FALL,
            /// <summary>
            /// 通过审核
            /// </summary>
            CARD_STATUS_VERIFY_OK,
            /// <summary>
            /// 卡券被用户删除
            /// </summary>
            CARD_STATUS_USER_DELETE,
            /// <summary>
            /// 在公众平台投放过的卡券
            /// </summary>
            CARD_STATUS_USER_DISPATCH
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codetype">
        /// Code展示类型，
        /// "CODE_TYPE_TEXT"，文本；
        /// "CODE_TYPE_BARCODE"，一维码 ；
        /// "CODE_TYPE_QRCODE"，二维码；
        /// "CODE_TYPE_ONLY_QRCODE",二维码无code显示；
        /// "CODE_TYPE_ONLY_BARCODE",一维码无code显示；
        /// </param>
        /// <param name="logo_url">
        /// 卡券的商户logo，建议像素为300*300
        /// </param>
        /// <param name="brand_name">
        /// 商户名字,字数上限为12个汉字
        /// </param>
        /// <param name="title">
        /// 卡券名，字数上限为9个汉字。(建议涵盖卡券属性、服务及金额)。
        /// </param>
        /// <param name="sub_title">
        /// 券名，字数上限为18个汉字。
        /// </param>
        /// <param name="color">
        /// 券颜色。按色彩规范标注填写Color010-Color100
        /// </param>
        /// <param name="notice">
        /// 卡券使用提醒，字数上限为16个汉字。
        /// </param>
        /// <param name="description">
        /// 卡券使用说明，字数上限为1024个汉字。
        /// </param>
        /// <param name="sku">
        /// 商品信息。
        /// </param>
        /// <param name="quantity">
        /// 卡券库存的数量，不支持填写0，上限为100000000。
        /// </param>
        /// <param name="date_info">
        /// 使用日期，有效期的信息。
        /// </param>
        /// <param name="type">
        /// DATE_TYPE_FIX_TIME_RANGE 表示固定日期区间，
        /// DATE_TYPE_FIX_TERM表示固定时长（自领取后按天算。
        /// </param>
        /// <param name="begin_timestamp">
        /// type为DATE_TYPE_FIX_TIME_RANGE时专用，表示起用时间。
        /// 从1970年1月1日00:00:00至起用时间的秒数，最终需转换为字符串形态传入。
        /// （东八区时间，单位为秒）
        /// </param>
        /// <param name="end_timestamp">
        /// type为DATE_TYPE_FIX_TIME_RANGE时专用，表示结束时间，
        /// 建议设置为截止日期的23:59:59过期。（东八区时间，单位为秒）
        /// </param>
        /// <param name="fixed_term">
        /// type为DATE_TYPE_FIX_TERM时专用，表示自领取后多少天内有效，领取后当天有效填写0。（单位为天）
        /// </param>
        /// <param name="fixed_begin_term">
        /// type为DATE_TYPE_FIX_TERM时专用，表示自领取后多少天开始生效。（单位为天）
        /// </param>
        public static WeiXinCard.base_info iniBase_Info(Code_Type codetype, string logo_url, string brand_name, string title, string sub_title,
            string color, string notice, string description, int quantity, string date_info, Type type, uint begin_timestamp, uint end_timestamp,
            int fixed_term, int fixed_begin_term)
        {
            WeiXinCard.base_info info = new base_info();
            info.logo_url = logo_url;
            info.code_type = codetype.ToString();
            info.brand_name = brand_name;
            info.title = title;
            info.sub_title = sub_title;
            info.color = color;
            info.notice = notice;
            info.description = description;
            sku s = new sku();
            s.quantity = quantity;
            info.sku = s;

            date_info date = new WeiXinCard.date_info();
            
            date.type = type.ToString();
            date.begin_timestamp = begin_timestamp;
            date.end_timestamp = end_timestamp;
            date.fixed_term = fixed_term;
            date.fixed_begin_term = fixed_begin_term;

            info.date_info = date;

            return info;
        }

        private static _back GetBackCreate(card_push_back json_back)
        {
            _back _b = new _back();
            if (json_back.errcode.Equals("0"))
            {
                _b.ok = true;
                _b.card_id = json_back.card_id;
            }
            else
            {
                _b.ok = false;
                _b.msg = json_back.errmsg;

            }
            return _b;
        }

        public static _back TuanGou_Time_Range(Code_Type codetype, string title, string sub_title,
            string notice, string description, int quantity, string date_info, DateTime begin, DateTime end, string deal_detail)
        {
            int _begin = Misc.ConvertDateTimeInt(DateTime.Parse(begin.ToString("yyyy-MM-dd 00:00:00")));
            int _end = Misc.ConvertDateTimeInt(DateTime.Parse(end.ToString("yyyy-MM-dd 23:59:59")));

            WeiXinCard.base_info info = iniBase_Info(codetype, def_logo_url, def_brand_name, title, sub_title, def_color, notice, description,  quantity, date_info, Type.DATE_TYPE_FIX_TIME_RANGE,
                (uint)_begin, (uint)_end, 0, 0);

            card_push_back json_back = TuanGou(info, deal_detail);

            return GetBackCreate(json_back);
        }

        public static _back TuanGou_Term(Code_Type codetype, string title, string sub_title,
            string notice, string description, int quantity, string date_info, int fixed_term, int fixed_begin_term, string deal_detail)
        {
            WeiXinCard.base_info info = iniBase_Info(codetype, def_logo_url, def_brand_name, title, sub_title, def_color, notice, description, quantity, date_info, Type.DATE_TYPE_FIX_TERM,
                0, 0, fixed_term, fixed_begin_term);

            card_push_back json_back = TuanGou(info, deal_detail);

            return GetBackCreate(json_back);
        }

        public static card_push_back TuanGou(WeiXinCard.base_info info, string deal_detail)
        {
            groupon m = new groupon();
            m.base_info = info;
            m.deal_detail = deal_detail;

            string back = "";

            card_push_back json_back = Push(ref back, JsonConvert.SerializeObject(m));
            return json_back;
        }

        public static void SetCard_Group()
        {

        }


        public class card_push_back
        {
            public string errcode = "";
            public string errmsg = "";
            public string card_id = "";
        }

        public class _back
        {
            public bool ok = false;
            public string msg = "";
            public string card_id = "";
        }

        /// <summary>
        /// 创建
        ///https://api.weixin.qq.com/card/create?access_token=ACCESS_TOKEN
        /// </summary>
        /// <param name="cope"></param>
        /// <param name="backtext"></param>
        /// <returns></returns>
        public static card_push_back Push(ref string backtext, string jsonStr)
        {

            backtext = "";
            string url = string.Format("https://api.weixin.qq.com/card/create?access_token={0}", API.access_token);
            Dictionary<string, string> dicback = new Dictionary<string, string>();
            backtext = "";
            string json = Http.HttpWebResponseUtility.PostHttpsString(url, jsonStr);
            backtext = json;
            dicback = cheWeiXin.BackReader.get(json);

            card_push_back p = new card_push_back();

            if (dicback.ContainsKey(CodeName.errcode))
            {
                p.errcode = dicback[CodeName.errcode];
                p.errmsg = dicback[CodeName.errmsg];
                if (dicback.ContainsKey("card_id"))
                    p.card_id = dicback["card_id"];
            }

            return p;
        }

        public static string def_logo_url = "http://cq.xiaoche5.com/images2014/logo.png";
        public static string def_brand_name = "小车快保";
        public static string def_color = "Color030";


        public class push_back
        {
            public string errcode = "";
            public string errmsg = "";
            public bool ok = false;
        }
        #region 客服发送卡卷 开始
        //客服发送卡卷 开始
        public class _CustomSend
        {
            public _CustomSend(string openid, string cardid)
            {
                this.touser = openid;
                this.card_id = cardid;
            }
            public string touser = "";
            public string msgtype = "wxcard";
            private string cardcode = "";
            private string card_id = "";
            public _wxcard wxcard
            {
                get
                {
                    return new _wxcard(card_id, cardcode, touser);
                }
            }
        }

        public class _wxcard
        {
            private string code = "";
            private string openid = "";
            private string timestamp = "";
            public _wxcard(string carid, string code, string openid)
            {
                this.card_id = carid;
                this.code = code;
                this.openid = openid;
                this.timestamp = Misc.ConvertDateTimeInt(DateTime.Now).ToString();
            }
            public string card_id = "";
            public string card_ext
            {
                get
                {
                    Random rd = new Random();
                    string nonce_str = rd.Next(99999).ToString();
                    string keyvaluestring = card_id + code + "m7RQzjA_ljjEkt-JCoklRGsLJrzLOXYqPTypbW-L8aApcj6g2wfh1g_RctCZ6175qHO-P-eQFOwfQ7aAmr_7tA" + timestamp + nonce_str;
                    //var sign = CryptoJS.SHA1(keyvaluestring).toString();
                    keyvaluestring = Misc.SHA1_Hash(keyvaluestring);
                    //System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, FormsAuthPasswordFormat.SHA1.ToString());
                    string res = "{\"code\":\"" + code + "\",\"openid\":\"" + openid + "\",\"timestamp\":\"" + timestamp + "\",\"nonce_str\":\"" + nonce_str + "\",\"signature\":\"" + keyvaluestring + "\"}";
                    return "";
                }
            }
        }

        public class _card_ext
        {
            public string code = "";
            public string openid = "";
            public string timestamp = "";
            public string signature
            {
                get
                {
                    string keyvaluestring = "code=" + code + ",openid=" + openid + ",timestamp=" + timestamp;
                    //var sign = CryptoJS.SHA1(keyvaluestring).toString();
                    return Misc.SHA1_Hash(keyvaluestring);
                }
            }
        }

        public static push_back CustomSend(string openid, string carid, string access_token)
        {
            _CustomSend m = new _CustomSend(openid, carid);
            //m.touser = openid;
            //m.wxcard.card_id = carid;
            //m.wxcard.card_ext.openid = openid;
            //m.wxcard.card_ext.code = cardcode;
            //m.wxcard.card_ext.timestamp = Misc.ConvertDateTimeInt(DateTime.Now).ToString();
            //_News m = new _News(openid);

            string jsonStr = JsonConvert.SerializeObject(m);
            string url = string.Format(Misc.customservice, access_token);
            string json = Http.HttpWebResponseUtility.PostHttpsString(url, jsonStr);

            Dictionary<string, string> dicback = new Dictionary<string, string>();
            dicback = cheWeiXin.BackReader.get(json);

            push_back p = new push_back();

            if (dicback.ContainsKey(CodeName.errcode))
            {
                p.errcode = dicback[CodeName.errcode];
                p.errmsg = dicback[CodeName.errmsg];
                if (p.errcode.Equals("0")) p.ok = true;
            }

            return p;
        }

        #endregion

        #region
        public class _Stock
        {
            public string card_id = "";
            public int increase_stock_value = 0;
            public int reduce_stock_value = 0;
        }
        /// <summary>
        /// 添加库存
        /// </summary>
        /// <param name="carid">卡卷ID</param>
        /// <param name="access_token">access_token</param>
        /// <param name="sum">添加个数</param>
        /// <returns></returns>
        public static push_back Increase_Stock(string carid, string access_token, int sum)
        {
            _Stock m = new _Stock();
            m.card_id = carid;
            m.increase_stock_value = sum;

            string jsonStr = JsonConvert.SerializeObject(m);
            string url = string.Format(Misc.stockcard, access_token);
            string json = Http.HttpWebResponseUtility.PostHttpsString(url, jsonStr);

            Dictionary<string, string> dicback = new Dictionary<string, string>();
            dicback = cheWeiXin.BackReader.get(json);

            push_back p = new push_back();

            if (dicback.ContainsKey(CodeName.errcode))
            {
                p.errcode = dicback[CodeName.errcode];
                p.errmsg = dicback[CodeName.errmsg];
                if (p.errcode.Equals("0")) p.ok = true;
            }

            return p;
        }
        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="carid">卡卷ID</param>
        /// <param name="access_token">access_token</param>
        /// <param name="sum">减少个数</param>
        /// <returns></returns>
        public static push_back Reduce_Stock(string carid, string access_token, int sum)
        {
            _Stock m = new _Stock();
            m.card_id = carid;
            m.reduce_stock_value = sum;

            string jsonStr = JsonConvert.SerializeObject(m);
            string url = string.Format(Misc.stockcard, access_token);
            string json = Http.HttpWebResponseUtility.PostHttpsString(url, jsonStr);

            Dictionary<string, string> dicback = new Dictionary<string, string>();
            dicback = cheWeiXin.BackReader.get(json);

            push_back p = new push_back();

            if (dicback.ContainsKey(CodeName.errcode))
            {
                p.errcode = dicback[CodeName.errcode];
                p.errmsg = dicback[CodeName.errmsg];
                if (p.errcode.Equals("0")) p.ok = true;
            }

            return p;
        }

        public class _Get
        {
            public string card_id = "";
        }

        /// <summary>
        /// 优惠券
        /// </summary>
        public class _Get_Back_coupon
        {
            public int errcode = -1;
            public string errmsg = "";

            public _general_coupon card = new _general_coupon();
        }

        /// <summary>
        /// 优惠券
        /// </summary>
        public class _general_coupon
        {
            public _base general_coupon = new _base();
            /// <summary>
            /// 优惠券专用，填写优惠详情。
            /// </summary>
            public string default_detail = "";
            /// <summary>
            /// 
            /// </summary>
            public string card_type = CardType.GENERAL_COUPON.ToString();
        }


        /// <summary>
        /// 代金券
        /// </summary>
        public class _Get_Back_cash
        {
            public int errcode = -1;
            public string errmsg = "";

            public _cash card = new _cash();
        }
        /// <summary>
        /// 代金券
        /// </summary>
        public class _cash
        {
            public _base cash = new _base();
            /// <summary>
            /// 代金券专用，表示起用金额。（单位为分）
            /// </summary>
            public int least_cost = 0;
            /// <summary>
            /// 代金券专用，表示减免金额。（单位为分）
            /// </summary>
            public int reduce_cost = 0;
            /// <summary>
            /// 
            /// </summary>
            public string card_type = CardType.CASH.ToString();
        }

        /// <summary>
        /// 礼品券
        /// </summary>
        public class _gift
        {
            public _base gift = new _base();
            /// <summary>
            /// 礼品券专用，填写礼品的名称。
            /// </summary>
            public string sgift = "";
            /// <summary>
            /// 
            /// </summary>
            public string card_type = CardType.GIFT.ToString();
        }

        /// <summary>
        /// 礼品券
        /// </summary>
        public class _Get_Back_gift
        {
            public int errcode = -1;
            public string errmsg = "";

            public _gift card = new _gift();
        }

        /// <summary>
        /// 折扣卷
        /// </summary>
        public class _discount
        {
            public _base discount = new _base();
            /// <summary>
            /// 折扣券专用，表示打折额度（百分比）。填30就是七折。
            /// </summary>
            public int idiscount = 0;
            /// <summary>
            /// 
            /// </summary>
            public string card_type = CardType.DISCOUNT.ToString();
        }

        /// <summary>
        /// 折扣卷
        /// </summary>
        public class _Get_Back_discount
        {
            public int errcode = -1;
            public string errmsg = "";

            public _discount card = new _discount();
        }

        /// <summary>
        /// 团购卷
        /// </summary>
        public class _groupon
        {
            public _base groupon = new _base();
            /// <summary>
            /// 团购券专用，团购详情
            /// </summary>
            public string deal_detail = "";
            /// <summary>
            /// 
            /// </summary>
            public string card_type = CardType.GROUPON.ToString();
        }

        /// <summary>
        /// 团购卷
        /// </summary>
        public class _Get_Back_groupon
        {
            public int errcode = -1;
            public string errmsg = "";

            public _groupon card = new _groupon();
        }
        /// <summary>
        /// 基本
        /// </summary>
        public class _base
        {
            public base_info base_info = new base_info();
        }

        public class _Get_Card
        {
            public string deal_detail = "";

            /// <summary>
            /// 代金券专用，表示起用金额。（单位为分）
            /// </summary>
            public int least_cost = 0;
            /// <summary>
            /// 代金券专用，表示减免金额。（单位为分）
            /// </summary>
            public int reduce_cost = 0;

            /// <summary>
            /// 折扣券专用，表示打折额度（百分比）。填30就是七折。
            /// </summary>
            public int discount = 0;
            /// <summary>
            /// 礼品券专用，填写礼品的名称。
            /// </summary>
            public string gift = "";

            public CardType cardtype;

            public base_info base_info = new base_info();

            public string errcode = "";
            public string errmsg = "";
            public bool ok = false;

        }

        /// <summary>
        /// 查询卡卷
        /// </summary>
        /// <param name="carid">卡卷ID</param>
        /// <param name="access_token">access_token</param>
        /// <returns>_Get_Card_Back.ok = true 返回正确 _Get_Card_Back.cardtype 卡卷类型</returns>
        public static _Get_Card Get(string carid, string access_token)
        {
            string json = "";
            return Get_Debug(carid,access_token,out json);
        }
        /// <summary>
        /// 查询卡卷,调试
        /// </summary>
        /// <param name="carid">卡卷ID</param>
        /// <param name="access_token">access_token</param>
        /// <param name="_json">返回的json字符串</param>
        /// <returns>_Get_Card_Back.ok = true 返回正确 _Get_Card_Back.cardtype 卡卷类型</returns>
        public static _Get_Card Get_Debug(string carid, string access_token, out string _json)
        {
            _Get m = new _Get();
            m.card_id = carid;

            string jsonStr = JsonConvert.SerializeObject(m);
            string url = string.Format(Misc.getcard, access_token);
            string json = Http.HttpWebResponseUtility.PostHttpsString(url, jsonStr);

            _json = json;

            Dictionary<string, string> dicback = new Dictionary<string, string>();
            dicback = cheWeiXin.BackReader.get(json);

            _Get_Card getres = new _Get_Card();

            if (dicback.ContainsKey(CodeName.errcode))
            {
                getres.errcode = dicback[CodeName.errcode];
                getres.errmsg = dicback[CodeName.errmsg];
                if (getres.errcode.Equals("0"))
                {
                    getres.ok = true;


                    if (json.IndexOf(CardType.GENERAL_COUPON.ToString()) > 0)
                    {
                        //是普通卡卷
                        _Get_Back_coupon get = new _Get_Back_coupon();
                        get = (_Get_Back_coupon)JsonConvert.DeserializeObject(json, typeof(_Get_Back_coupon));

                        getres.cardtype = CardType.GENERAL_COUPON;
                        getres.base_info = get.card.general_coupon.base_info;
                        getres.deal_detail = get.card.default_detail;
                    }
                    else if (json.IndexOf(CardType.CASH.ToString()) > 0)
                    {
                        //是现金卷
                        getres.cardtype = CardType.CASH;

                        _Get_Back_cash get = new _Get_Back_cash();
                        get = (_Get_Back_cash)JsonConvert.DeserializeObject(json, typeof(_Get_Back_cash));

                        getres.base_info = get.card.cash.base_info;
                        getres.least_cost = get.card.least_cost;
                        getres.reduce_cost = get.card.reduce_cost;
                    }
                    else if (json.IndexOf(CardType.DISCOUNT.ToString()) > 0)
                    {
                        getres.cardtype = CardType.DISCOUNT;

                        _Get_Back_discount get = new _Get_Back_discount();
                        get = (_Get_Back_discount)JsonConvert.DeserializeObject(json, typeof(_Get_Back_discount));

                        getres.base_info = get.card.discount.base_info;
                        getres.discount = get.card.idiscount;
                    }
                    else if (json.IndexOf(CardType.GROUPON.ToString()) > 0)
                    {
                        getres.cardtype = CardType.GROUPON;

                        _Get_Back_groupon get = new _Get_Back_groupon();
                        get = (_Get_Back_groupon)JsonConvert.DeserializeObject(json, typeof(_Get_Back_groupon));

                        getres.base_info = get.card.groupon.base_info;
                        getres.deal_detail = get.card.deal_detail;
                    }
                    else if (json.IndexOf(CardType.GIFT.ToString()) > 0)
                    {
                        getres.cardtype = CardType.GIFT;

                        _Get_Back_gift get = new _Get_Back_gift();
                        get = (_Get_Back_gift)JsonConvert.DeserializeObject(json, typeof(_Get_Back_gift));

                        getres.base_info = get.card.gift.base_info;
                        getres.gift = get.card.sgift;
                    }
                }
            }


            return getres;
        }

        #endregion


        #region 用户卡卷
        public class _User_Get{
            public string errcode = "";
            public string errmsg = "";

            public bool ok = false;

            public List<_User_Card> card_list = new List<_User_Card>();

        }
        public class _User_Card{
            public string card_id = "";
            public string code = "";
        }

        public class _User_Get_Par
        {
            public string openid = "";
            public string card_id = "";
        }

        public static _User_Get GetUser(string openid,string card_id, string access_token){
            string json = "";
            return GetUser_Debug(openid,card_id,access_token,out json);
        }
        public static _User_Get GetUser_Debug(string openid,string card_id, string access_token,out string _json){
            _User_Get_Par m = new _User_Get_Par();
            m.card_id = card_id;
            m.openid = openid;

            string jsonStr = JsonConvert.SerializeObject(m);
            string url = string.Format(Misc.getusercards, access_token);
            string json = Http.HttpWebResponseUtility.PostHttpsString(url, jsonStr);

            _json = json;

            Dictionary<string, string> dicback = new Dictionary<string, string>();
            dicback = cheWeiXin.BackReader.get(json);

            _User_Get getres = new _User_Get();

            if (dicback.ContainsKey(CodeName.errcode))
            {
                getres.errcode = dicback[CodeName.errcode];
                getres.errmsg = dicback[CodeName.errmsg];
                if (getres.errcode.Equals("0"))
                {
                    getres.ok = true;
                    _User_Get get = new _User_Get();
                    get = (_User_Get)JsonConvert.DeserializeObject(json, typeof(_User_Get));
                }
            }


            return getres;
        }
        
        #endregion

        #region 批量查询
        
        public class _Get_Cards_Par
        {
            public int offset = 0;
            public int count = 0;
            public ArrayList status_list = new ArrayList();
        }

        public class _Get_Cards
        {
             public string errcode = "";
            public string errmsg = "";

            public bool ok = false;
            public ArrayList card_id_list = new ArrayList();
            public int total_num = 0;
        }

        public static _Get_Cards Get_List(int begin, int count, string access_token, Status status)
        {
            string json = "";
            return Get_List_Debug(begin, count, access_token, status, out json);
        }

        public static _Get_Cards Get_List_Debug(int begin,int count,string access_token, Status status,out string _json){
            _Get_Cards_Par par = new _Get_Cards_Par();
            par.offset = begin;
            par.count = count;
            par.status_list.Add(status.ToString());

            string jsonStr = JsonConvert.SerializeObject(par);
            string url = string.Format(Misc.getcards, access_token);
            string json = Http.HttpWebResponseUtility.PostHttpsString(url, jsonStr);

            _json = json;

            Dictionary<string, string> dicback = new Dictionary<string, string>();
            dicback = cheWeiXin.BackReader.get(json);

            _Get_Cards getres = new _Get_Cards();

            if (dicback.ContainsKey(CodeName.errcode))
            {
                getres.errcode = dicback[CodeName.errcode];
                getres.errmsg = dicback[CodeName.errmsg];
                if (getres.errcode.Equals("0"))
                {
                    getres.ok = true;
                    _Get_Cards get = new _Get_Cards();
                    get = (_Get_Cards)JsonConvert.DeserializeObject(json, typeof(_Get_Cards));
                    
                }
            }


            return getres;
        }
        #endregion

        #region 解码卡卷
        public class _Decrypt_Par{
            public string encrypt_code = "";
        }

        public class _Decrypt{
            public string errcode = "";
            public string errmsg = "";
            public string code = "";
            public bool ok = false;
        }

        /// <summary>
        /// 解码卡卷
        /// </summary>
        /// <param name="encrypt_code"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static _Decrypt Decrypt(string encrypt_code, string access_token)
        {
            _Decrypt_Par m = new _Decrypt_Par();
            m.encrypt_code = encrypt_code;

            string jsonStr = JsonConvert.SerializeObject(m);
            string url = string.Format(Misc.carddecrypt, access_token);
            string json = Http.HttpWebResponseUtility.PostHttpsString(url, jsonStr);

            Dictionary<string, string> dicback = new Dictionary<string, string>();
            dicback = cheWeiXin.BackReader.get(json);

            _Decrypt getres = new _Decrypt();

            if (dicback.ContainsKey(CodeName.errcode))
            {
                getres.errcode = dicback[CodeName.errcode];
                getres.errmsg = dicback[CodeName.errmsg];
                if (getres.errcode.Equals("0"))
                {

                    //_Decrypt_Par get = new _User_Get();
                    getres = (_Decrypt)JsonConvert.DeserializeObject(json, typeof(_Decrypt));

                    getres.ok = true;
                }
            }


            return getres;
        }
        #endregion

        #region 消核卡卷
        public class _Consume_Par
        {
            public string code = "";
        }
        public class _Consume
        {
            public string errcode = "";
            public string errmsg = "";
            public Card card = new Card();
            public bool ok = false;
            public string openid = "";
        }


        /// <summary>
        /// 消核卡卷
        /// </summary>
        /// <param name="code"></param>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static _Consume Consume(string code, string access_token)
        {
            _Consume_Par m = new _Consume_Par();
            m.code = code;

            string jsonStr = JsonConvert.SerializeObject(m);
            string url = string.Format(Misc.cardconsume, access_token);
            string json = Http.HttpWebResponseUtility.PostHttpsString(url, jsonStr);

            Dictionary<string, string> dicback = new Dictionary<string, string>();
            dicback = cheWeiXin.BackReader.get(json);

            _Consume getres = new _Consume();

            if (dicback.ContainsKey(CodeName.errcode))
            {
                getres.errcode = dicback[CodeName.errcode];
                getres.errmsg = dicback[CodeName.errmsg];
                if (getres.errcode.Equals("0"))
                {

                    //_Decrypt_Par get = new _User_Get();
                    getres = (_Consume)JsonConvert.DeserializeObject(json, typeof(_Consume));

                    getres.ok = true;
                }
            }


            return getres;
        
        }
        #endregion
    }

}