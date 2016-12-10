using System;
using System.Collections.Generic;

using System.Text;

namespace cheWeiXin
{
    public class UserInfo
    {
        private string openid = "";
        /// <summary>
        /// openid
        /// </summary>
        public string OpenId
        {
            get
            {
                return openid;
            }
            set
            {
                openid = value;
            }
        }

        private string nickname = "";
        /// <summary>
        /// nickname
        /// </summary>
        public string Nickname
        {
            get
            {
                return nickname;
            }
            set
            {
                nickname = value;
            }
        }

        private string sex = "";
        /// <summary>
        /// sex
        /// </summary>
        public string Sex
        {
            get
            {
                return sex;
            }
            set
            {
                sex = value;
            }
        }

        private string province = "";
        /// <summary>
        /// province
        /// </summary>
        public string Province
        {
            get
            {
                return province;
            }
            set
            {
                province = value;
            }
        }

        private string city = "";
        /// <summary>
        /// city
        /// </summary>
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        private string country = "";
        /// <summary>
        /// country
        /// </summary>
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }

        private string headimgurl = "";
        /// <summary>
        /// headimgurl
        /// </summary>
        public string Headimgurl
        {
            get
            {
                return headimgurl;
            }
            set
            {
                headimgurl = value;
            }
        }

        private string privilege = "";
        /// <summary>
        /// privilege
        /// </summary>
        public string Privilege
        {
            get
            {
                return privilege;
            }
            set
            {
                privilege = value;
            }
        }

        private string subscribe = "";
        /// <summary>
        /// subscribe
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public string Subscribe
        {
            get
            {
                return subscribe;
            }
            set
            {
                subscribe = value;
            }
        }

        private string language = "";
        /// <summary>
        /// language
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
            }
        }

        private string subscribe_time = "";
        /// <summary>
        /// subscribe_time
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public string Subscribe_time
        {
            get
            {
                return subscribe_time;
            }
            set
            {
                subscribe_time = value;
            }
        }
    }
}
