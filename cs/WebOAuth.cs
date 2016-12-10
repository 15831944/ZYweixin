using System;
using System.Collections.Generic;
using System.Text;

namespace cheWeiXin
{
    public class WebOAuth
    {
        /// <summary>
        /// code凭证
        /// </summary>
        public string code = "code";
        /// <summary>
        /// AppId
        /// </summary>
        public string Appid = "";
        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret = "";
        /// <summary>
        /// CallbackUrl
        /// </summary>
        public string CallbackUrl = "";
        /// <summary>
        /// Openid
        /// </summary>
        public string OpenId = "";
        /// <summary>
        /// refresh_token
        /// </summary>
        public string refresh_token = "";
        /// <summary>
        /// scope
        /// </summary>
        public string scopestr = "";
        /// <summary>
        /// expires_in
        /// </summary>
        public string expires_in = "";
        /// <summary>
        /// access_token
        /// </summary>
        public string access_token = "";

        /// <summary>
        /// 应用授权作用域
        /// </summary>
        public enum scope
        {
            /// <summary>
            /// 不弹出授权页面，直接跳转，只能获取用户openid
            /// </summary>
            snsapi_base,
            /// <summary>
            /// snsapi_userinfo弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息
            /// </summary>
            snsapi_userinfo
        }
       
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="AppId"></param>
        /// <param name="AppSecret"></param>
        /// <param name="CallbackUrl"></param>
        public WebOAuth(string AppId, string AppSecret, string CallbackUrl)
        {
            //Random rd = new Random();
            //code = rd.Next().ToString();

            this.Appid = AppId;
            this.AppSecret = AppSecret;
            this.CallbackUrl = CallbackUrl;
        }

        /// <summary>
        /// 第一步：用户同意授权，获取code
        /// </summary>
        /// <param name="cope"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public string GetAuthorizeURL(scope cope, string state)
        {
            string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + Appid + "&redirect_uri=" + CallbackUrl + "&response_type=" + code + "&scope=" + cope.ToString() + "&state=" + state + "#wechat_redirect";
            return url;
        }

        /// <summary>
        /// 第二步：通过code换取网页授权access_token
        /// 获取code后，请求以下链接获取access_token： 
        ///https://api.weixin.qq.com/sns/oauth2/access_token?appid=APPID&secret=SECRET&code=CODE&grant_type=authorization_code
        /// </summary>
        /// <param name="cope"></param>
        /// <param name="backtext"></param>
        /// <returns></returns>
        public bool GetAccessToken(string code, ref string backtext)
        {
            backtext = "";
            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code", this.Appid, this.AppSecret, code);
            Dictionary<string, string> dicback = new Dictionary<string, string>();
            return getBack(url, ref dicback, ref backtext);
        }

        private bool getBack(string url,ref Dictionary<string, string> dicback,ref string backtext){
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
                this.scopestr = dicback[CodeName.scope];
                this.access_token = dicback[CodeName.access_token];
                this.OpenId = dicback[CodeName.openid];
                this.refresh_token = dicback[CodeName.refresh_token];
                this.expires_in = dicback[CodeName.expires_in];
                return true;
            }
        }

        
    }
}
