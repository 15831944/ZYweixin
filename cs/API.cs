using System;
using System.Collections.Generic;
using System.Text;
using Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Collections;

namespace cheWeiXin
{
    public class API
    {
        public static string access_token = "";
        public static string expires_in = "";
        public static string ticket = "";
        /// <summary>
        /// 请求以下链接获取access_token： 
        ///https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET
        ///https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET
        /// </summary>
        /// <param name="cope"></param>
        /// <param name="backtext"></param>
        /// <returns></returns>
        public static bool GetToken(ref string backtext, string Appid, string AppSecret)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", Appid, AppSecret);
            Dictionary<string, string> dicback = new Dictionary<string, string>();
            return getBack2(url, ref dicback, ref backtext);
        }

        /// <summary>
        /// https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=ACCESS_TOKEN&type=jsapi
        /// </summary>
        /// <param name="backtext"></param>
        /// <param name="Appid"></param>
        /// <param name="AppSecret"></param>
        /// <returns></returns>
        public static bool GetJsapiTicket(ref string backtext,string token)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", token);
            Dictionary<string, string> dicback = new Dictionary<string, string>();
            return getBack3(url, ref dicback, ref backtext);
        }

        private static bool getBack2(string url, ref Dictionary<string, string> dicback, ref string backtext)
        {
            backtext = "";
            string json = Http.HttpWebResponseUtility.GetHttps(url);
            backtext = json;
            dicback = cheWeiXin.BackReader.get(json);
            if (dicback.ContainsKey(CodeName.errcode))
            {
                backtext = string.Format("错误号{0},说明{1}", dicback[CodeName.errcode], dicback[CodeName.errmsg]);
                return false;
            }
            else
            {
                access_token = dicback[CodeName.access_token];
                expires_in = dicback[CodeName.expires_in];
                return true;
            }
        }

        private static bool getBack3(string url, ref Dictionary<string, string> dicback, ref string backtext)
        {
            backtext = "";
            string json = Http.HttpWebResponseUtility.GetHttps(url);
            backtext = json;
            dicback = cheWeiXin.BackReader.get(json);
            if (dicback.ContainsKey(CodeName.errcode))
            {
                if (dicback[CodeName.errcode].Equals("0"))
                {
                    ticket = dicback[CodeName.ticket];
                    expires_in = dicback[CodeName.expires_in];
                    return true;
                }
                else
                {
                    backtext = string.Format("错误号{0},说明{1}", dicback[CodeName.errcode], dicback[CodeName.errmsg]);
                    return false;
                }
            }
            else
            {
                
                return false;
            }
        }

        public API(string access_token)
        {
            API.access_token = access_token;
        }

        private static bool isOk(string json)
        {
            //errcode
            if (json.IndexOf("{\"errcode\":") >= 0)
            {
                return false;
            }
            return true;
        }

        private static bool isOkMsg(string json)
        {
            JObject javascript = (JObject)JsonConvert.DeserializeObject(json);
            if (javascript[CodeName.errcode].Equals("0"))
            {
                return true;
            }
            return false;
        }

        private static JObject GetJson(string json)
        {
            JObject javascript = (JObject)JsonConvert.DeserializeObject(json);
            return javascript;
        }

        private static bool getBack(string url, ref Dictionary<string, string> dicback, ref string backtext)
        {
            backtext = "";
            dicback = cheWeiXin.BackReader.get(Http.HttpWebResponseUtility.GetHttps(url));
            if (dicback.ContainsKey(CodeName.errcode))
            {
                backtext = string.Format("错误号{0},说明{1}", dicback[CodeName.errcode], dicback[CodeName.errmsg]);
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool getBackPost(string url, ref Dictionary<string, string> dicback, ref string backtext, Dictionary<string, string> pars)
        {
            backtext = "";
            dicback = cheWeiXin.BackReader.get(Http.HttpWebResponseUtility.PostHttps(url, pars));
            if (dicback.ContainsKey(CodeName.errcode))
            {
                backtext = string.Format("错误号{0},说明{1}", dicback[CodeName.errcode], dicback[CodeName.errmsg]);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 获取用户信息基本信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfo(string openid, string access_token)
        {
            //http请求方式: GET
            //https://api.weixin.qq.com/cgi-bin/user/info?access_token=ACCESS_TOKEN&openid=OPENID
            string get = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}", access_token, openid);

            Dictionary<string, string> dicback = new Dictionary<string, string>();
            string backtext = "";
            UserInfo user = new UserInfo();
            if (getBack(get, ref dicback, ref backtext))
            {
                user.City = dicback[CodeName.city];
                user.Country = dicback[CodeName.country];
                user.Headimgurl = dicback[CodeName.headimgurl];
                user.Nickname = dicback[CodeName.nickname];
                user.OpenId = dicback[CodeName.openid];
                //user.Privilege = dicback[CodeName.privilege];
                user.Province = dicback[CodeName.province];
                user.Sex = dicback[CodeName.sex];
                user.Language = dicback[CodeName.language];
                user.Subscribe = dicback[CodeName.subscribe];
                user.Subscribe_time = dicback[CodeName.subscribe_time];
                return user;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 网页授权后拉取用户信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static UserInfo GetUser(string openid, string access_token, out string backtext)
        {
            //http：GET（请使用https协议）
            //https://api.weixin.qq.com/sns/userinfo?access_token=ACCESS_TOKEN&openid=OPENID
            string get = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}", access_token, openid);

            Dictionary<string, string> dicback = new Dictionary<string, string>();
            backtext = "";
            UserInfo user = new UserInfo();
            if (getBack(get, ref dicback, ref backtext))
            {
                user.City = dicback[CodeName.city];
                user.Country = dicback[CodeName.country];
                user.Headimgurl = dicback[CodeName.headimgurl];
                user.Nickname = dicback[CodeName.nickname];
                user.OpenId = dicback[CodeName.openid];
                user.Privilege = dicback[CodeName.privilege];
                user.Province = dicback[CodeName.province];
                user.Sex = dicback[CodeName.sex];
                return user;
            }
            else
            {
                return null;
            }
        }

        private static string GetBackErro(string json)
        {
            JObject j = GetJson(json);
            string backinfo = string.Format("错误号：{0}，说明{1}", j[CodeName.errcode], j[CodeName.errmsg]);
            return backinfo;
        }

        #region 获取关注者列表
        /// <summary>
        /// 获取关注者列表
        /// 每次拉取10000个，下一次需要把上一次返回的next_openid
        /// 作为参数传入,返回的next_openid为空字符时表示没有了
        /// </summary>
        /// <param name="next_openid">开始获取关注用户openid,第一次为“”</param>
        /// <param name="backinfo">输出的错误信息</param>
        /// <param name="access_token">access_token</param>
        /// <returns>返回关注的对象</returns>
        public static Like GetLike(string next_openid, ref string backinfo, string access_token)
        {
            //http请求方式: GET（请使用https协议）
            //https://api.weixin.qq.com/cgi-bin/user/get?access_token=ACCESS_TOKEN&next_openid=NEXT_OPENID

            string get = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}", access_token, next_openid);
            Dictionary<string, string> pars = new Dictionary<string, string>();

            string json = Http.HttpWebResponseUtility.GetHttps(get);
            backinfo = "";
            if (isOk(json))
            {
                Like l = new Like();

                JObject javascript = (JObject)JsonConvert.DeserializeObject(json);
                l.Count = int.Parse(javascript[CodeName.count].ToString());
                l.Next_openid = javascript[CodeName.next_openid].ToString();
                l.Total = int.Parse(javascript[CodeName.total].ToString());

                JArray jary = (JArray)javascript[CodeName.data];
                JArray jaryls = (JArray)jary[0];

                ArrayList datas = new ArrayList();
                for (int j = 0; j < jaryls.Count; j++)
                {
                    datas.Add(jaryls[j].ToString());
                }
                return l;
            }
            else
            {
                backinfo = GetBackErro(json);
            }

            return null;

        }
        #endregion

        #region 分组
        /// <summary>
        /// 修改分组
        /// </summary>
        /// <param name="groupdid">分组id，由微信分配</param>
        /// <param name="openid">openid</param>
        /// <param name="to_groupid">分组id，由微信分配的ID</param>
        /// <param name="backinfo">返回的错误信息</param>
        /// <param name="access_token">access_token</param>
        /// <returns>成功 失败</returns>
        public static bool MoveUserToGroup(string openid, int to_groupid, ref string backinfo, string access_token)
        {
            //http请求方式: POST（请使用https协议）
            //https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token=ACCESS_TOKEN
            //POST数据格式：json
            //POST数据例子：{"openid":"oDF3iYx0ro3_7jD4HFRDfrjdCM58","to_groupid":108}

            string get = string.Format("https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}", access_token);
            Dictionary<string, string> pars = new Dictionary<string, string>();

            string data = "{\"openid\":\"" + openid + "\",\"to_groupid\":" + to_groupid + "}";
            pars.Add("data", data);

            string json = Http.HttpWebResponseUtility.PostHttps(get, pars);
            backinfo = "";
            if (isOkMsg(json))
            {
                return true;
            }
            else
            {
                backinfo = GetBackErro(json);
            }

            return false;
        }
        /// <summary>
        /// 修改分组
        /// </summary>
        /// <param name="groupdid">分组id，由微信分配</param>
        /// <param name="name">分组名字（30个字符以内,过长会截断）</param>
        /// <param name="backinfo">返回的错误信息</param>
        /// <param name="access_token">access_token</param>
        /// <returns>成功 失败</returns>
        public static bool UpdateGroup(int groupdid, string name, ref string backinfo, string access_token)
        {
            //http请求方式: POST（请使用https协议）
            //https://api.weixin.qq.com/cgi-bin/groups/update?access_token=ACCESS_TOKEN
            //POST数据格式：json
            //POST数据例子：{"group":{"id":108,"name":"test2_modify2"}}
            string get = string.Format("https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}", access_token);
            Dictionary<string, string> pars = new Dictionary<string, string>();

            if (name.Length > 30) name = name.Substring(0, 30);
            string data = "{\"group\":{\"id\":" + groupdid + ",\"name\":\"" + name + "\"}}";
            pars.Add("data", data);

            string json = Http.HttpWebResponseUtility.PostHttps(get, pars);
            backinfo = "";
            if (isOkMsg(json))
            {
                return true;
            }
            else
            {
                backinfo = GetBackErro(json);
            }

            return false;
        }

        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="name">分组名字（30个字符以内,过长会截断）</param>
        /// <returns>Group对象，带返回的ID</returns>
        public static Group AddGroup(string name, ref string backinfo, string access_token)
        {
            ///http请求方式: POST（请使用https协议）
            //https://api.weixin.qq.com/cgi-bin/groups/create?access_token=ACCESS_TOKEN
            //POST数据格式：json
            //POST数据例子：{"group":{"name":"test"}}
            string get = string.Format("https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}", access_token);
            Dictionary<string, string> pars = new Dictionary<string, string>();

            if (name.Length > 30) name = name.Substring(0, 30);
            string data = "{\"group\":{\"name\":\"" + name + "\"}}";
            pars.Add("data", data);

            string json = Http.HttpWebResponseUtility.PostHttps(get, pars);
            backinfo = "";
            if (isOk(json))
            {
                Group g = new Group();

                JObject javascript = (JObject)JsonConvert.DeserializeObject(json);
                JArray jary = (JArray)javascript[CodeName.group];

                for (int j = 0; j < jary.Count; j++)
                {
                    JObject jsonbj = (JObject)json[j];

                    g.Count = int.Parse(jsonbj[CodeName.count].ToString());
                    g.Id = int.Parse(jsonbj[CodeName.id].ToString());
                    g.Name = jsonbj[CodeName.name].ToString();


                }

                return g;
            }
            else
            {
                backinfo = GetBackErro(json);
            }

            return null;
        }

        /// <summary>
        /// 修改分组
        /// </summary>
        /// <param name="name">分组名称</param>
        /// <returns>Group对象，带返回的ID</returns>
        public static Group UpdateGroup(string name, ref string backinfo, string access_token)
        {
            ///http请求方式: POST（请使用https协议）
            //https://api.weixin.qq.com/cgi-bin/groups/create?access_token=ACCESS_TOKEN
            //POST数据格式：json
            //POST数据例子：{"group":{"name":"test"}}
            string get = string.Format("https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}", access_token);
            Dictionary<string, string> pars = new Dictionary<string, string>();

            string data = "{\"group\":{\"name\":\"" + name + "\"}}";
            pars.Add("data", data);

            string json = Http.HttpWebResponseUtility.PostHttps(get, pars);
            backinfo = "";
            if (isOk(json))
            {
                Group g = new Group();

                JObject javascript = (JObject)JsonConvert.DeserializeObject(json);
                JArray jary = (JArray)javascript[CodeName.group];

                for (int j = 0; j < jary.Count; j++)
                {
                    JObject jsonbj = (JObject)json[j];

                    g.Count = int.Parse(jsonbj[CodeName.count].ToString());
                    g.Id = int.Parse(jsonbj[CodeName.id].ToString());
                    g.Name = jsonbj[CodeName.name].ToString();


                }

                return g;
            }
            else
            {
                backinfo = GetBackErro(json);
            }

            return null;
        }
        /// <summary>
        /// 获得分组
        /// </summary>
        /// <param name="backinfo">返回错误值</param>
        /// <returns>分组集合</returns>
        public static List<Group> GetGroup(ref string backinfo, string access_token)
        {
            //http请求方式: GET（请使用https协议）
            //https://api.weixin.qq.com/cgi-bin/groups/get?access_token=ACCESS_TOKEN
            string get = string.Format("https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}", access_token);

            string json = Http.HttpWebResponseUtility.GetHttps(get);
            backinfo = "";
            if (isOk(json))
            {
                List<Group> groups = new List<Group>();

                JObject javascript = (JObject)JsonConvert.DeserializeObject(json);
                JArray jary = (JArray)javascript[CodeName.groups];

                for (int j = 0; j < jary.Count; j++)
                {
                    JObject jsonbj = (JObject)json[j];
                    Group g = new Group();
                    g.Count = int.Parse(jsonbj[CodeName.count].ToString());
                    g.Id = int.Parse(jsonbj[CodeName.id].ToString());
                    g.Name = jsonbj[CodeName.name].ToString();

                    groups.Add(g);
                }

                return groups;
            }
            else
            {
                backinfo = GetBackErro(json);
            }

            return null;
        }
        #endregion

        #region 二维码
        /// <summary>
        /// 发送消息类型
        /// </summary>
        public enum TicketType
        {
            QR_SCENE,
            QR_LIMIT_SCENE
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="action_info">二维码详细信息</param>
        /// <param name="scene_id">场景值ID，临时二维码时为32位整型，永久二维码时最大值为1000</param>
        /// <param name="TicketType">二维码类型，QR_SCENE为临时,QR_LIMIT_SCENE为永久</param>
        /// <returns>图片url</returns>
        public static string GetQRCodeImage(string action_info, int scene_id, TicketType TicketType, string access_token)
        {
            string ticket = GetTicket(action_info, scene_id, TicketType, access_token);
            if (ticket.Length > 0)
                return GetQRCodeImageFromTicket(HttpContext.Current.Server.UrlEncode(ticket));
            return "";
        }

        /// <summary>
        /// 根据票据获得二维码
        /// </summary>
        /// <param name="ticket">TICKET记得进行UrlEncode</param>
        /// <returns>图片url</returns>
        public static string GetQRCodeImageFromTicket(string ticket)
        {
            /**
            HTTP GET请求（请使用https协议）
            https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=TICKET
            提醒：TICKET记得进行UrlEncode
             * **/
            string url = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + ticket;
            return url;
        }
        /// <summary>
        /// 获得票据
        /// </summary>
        /// <param name="action_info">二维码详细信息</param>
        /// <param name="scene_id">场景值ID，临时二维码时为32位整型，永久二维码时最大值为1000</param>
        /// <param name="TicketType">二维码类型，QR_SCENE为临时,QR_LIMIT_SCENE为永久</param>
        /// <returns>票据凭证</returns>
        public static string GetTicket(string action_info, int scene_id, TicketType TicketType, string access_token)
        {
            /**
            临时二维码请求说明

            http请求方式: POST
            URL: https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=TOKEN
            POST数据格式：json
            POST数据例子：{"expire_seconds": 1800, "action_name": "QR_SCENE", "action_info": {"scene": {"scene_id": 123}}}
            永久二维码请求说明

            http请求方式: POST
            URL: https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=TOKEN
            POST数据格式：json
            POST数据例子：{"action_name": "QR_LIMIT_SCENE", "action_info": {"scene": {"scene_id": 123}}}
            **/
            string get = string.Format("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}", access_token);

            Dictionary<string, string> dicback = new Dictionary<string, string>();
            Dictionary<string, string> pars = new Dictionary<string, string>();

            string data = "{\"action_name\": \"" + TicketType.ToString() + "\", \"" + action_info + "\": {\"scene\": {\"scene_id\": " + scene_id.ToString() + "}}}";
            pars.Add("data", "");
            string backtext = "";

            if (getBackPost(get, ref dicback, ref backtext, pars))
            {
                string ticket = dicback[CodeName.ticket].ToString();
                string expire_seconds = dicback[CodeName.expire_seconds].ToString();
                return ticket;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 客服功能
        /// <summary>
        /// 发送消息类型
        /// </summary>
        public enum PushMessageType
        {
            text,
            image,
            voice,
            video,
            music,
            news
        }
        /// <summary>
        /// 发送文本
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string PushText(string openid, string content, string access_token)
        {
            Dictionary<string, string> myDic = new Dictionary<string, string>();
            myDic.Add("content", content);

            return PushMessage(openid, SetPushMessageJosn(openid, PushMessageType.text, myDic), access_token);
        }
        /// <summary>
        /// 发送图片
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="MEDIA_ID">发送的图片的媒体ID</param>
        /// <returns></returns>
        public static string PushImage(string openid, string MEDIA_ID, string access_token)
        {
            Dictionary<string, string> myDic = new Dictionary<string, string>();
            myDic.Add("media_id", MEDIA_ID);

            return PushMessage(openid, SetPushMessageJosn(openid, PushMessageType.image, myDic), access_token);
        }
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="MEDIA_ID">发送的语音的媒体ID</param>
        /// <returns></returns>
        public static string PushVoice(string openid, string MEDIA_ID, string access_token)
        {
            Dictionary<string, string> myDic = new Dictionary<string, string>();
            myDic.Add("media_id", MEDIA_ID);

            return PushMessage(openid, SetPushMessageJosn(openid, PushMessageType.voice, myDic), access_token);
        }
        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="MEDIA_ID">媒体ID</param>
        /// <param name="THUMB_MEDIA_ID">缩略图媒体ID</param>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static string PushVideo(string openid, string media_id, string thumb_media_id, string title, string description, string access_token)
        {
            Dictionary<string, string> myDic = new Dictionary<string, string>();
            myDic.Add("media_id", media_id);
            myDic.Add("thumb_media_id", thumb_media_id);
            myDic.Add("title", title);
            myDic.Add("description", description);

            return PushMessage(openid, SetPushMessageJosn(openid, PushMessageType.video, myDic), access_token);
        }
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="musicurl">音乐链接</param>
        /// <param name="hqmusicurl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="thumb_media_id">缩略图的媒体ID</param>
        /// <param name="title">音乐标题</param>
        /// <param name="description">音乐描述</param>
        /// <returns></returns>
        public static string PushMusic(string openid, string musicurl, string hqmusicurl, string thumb_media_id, string title, string description, string access_token)
        {
            Dictionary<string, string> myDic = new Dictionary<string, string>();
            myDic.Add("musicurl", musicurl);
            myDic.Add("hqmusicurl", hqmusicurl);
            myDic.Add("thumb_media_id", thumb_media_id);
            myDic.Add("title", title);
            myDic.Add("description", description);

            return PushMessage(openid, SetPushMessageJosn(openid, PushMessageType.music, myDic), access_token);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        /// <param name="picurl">图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80</param>
        /// <param name="url">点击后跳转的链接</param>
        /// <returns></returns>
        public static string PushNews(string openid, string title, string description, string picurl, string url, string access_token)
        {
            Dictionary<string, string> myDic = new Dictionary<string, string>();
            myDic.Add("title", title);
            myDic.Add("description", description);
            myDic.Add("picurl", picurl);
            myDic.Add("url", url);

            return PushMessage(openid, SetPushMessageJosn(openid, PushMessageType.news, myDic), access_token);
        }

        /// <summary>
        /// 设置发送的json
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="type"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        private static string SetPushMessageJosn(string openid, PushMessageType type, Dictionary<string, string> dic)
        {

            string json = "{{" +
                "\"touser\":\"" + openid + "\"," +
                "\"msgtype\":\"" + type.ToString() + "\"," +
                "\"" + type.ToString() + "\":" +
                "{{{0}}}" +
            "}}";

            int len = dic.Count;
            StringBuilder pars = new StringBuilder();
            foreach (KeyValuePair<string, string> a in dic)
            {
                pars.AppendFormat("\"{0}\":\"{1}\",", a.Key, a.Value);
            }

            pars.Remove(pars.Length - 1, 1);

            switch (type)
            {
                case PushMessageType.news:
                    {
                        json = string.Format(json, "\"articles\": [{" + pars + "}]");
                    } break;
                default: json = string.Format(json, pars); break;
            }

            return json;
        }

        private static string PushMessage(string openid, string json, string access_token)
        {
            //http请求方式: POST
            //https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
            string get = string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token);

            string back = HttpWebResponseUtility.PostHttps(get, null);
            return back;
        }
        #endregion


    
    }
}
