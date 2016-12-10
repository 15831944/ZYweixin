using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace cheWeiXin
{
    public class Like
    {
        int total = 0;
        /// <summary>
        /// 关注该公众账号的总用户数
        /// </summary>
        public int Total
        {
            set
            {
                this.total = value;
            }
            get
            {
                return this.total;
            }
        }

        int count = 0;
        /// <summary>
        /// 拉取的OPENID个数，最大值为10000
        /// </summary>
        public int Count
        {
            set
            {
                this.count = value;
            }
            get
            {
                return this.count;
            }
        }

        string next_openid = "";
        /// <summary>
        /// 拉取列表的后一个用户的OPENID
        /// </summary>
        public string Next_openid
        {
            set
            {
                this.next_openid = value;
            }
            get
            {
                return this.next_openid;
            }
        }

        ArrayList datas;
        /// <summary>
        /// 列表数据，OPENID的列表
        /// </summary>
        public ArrayList Datas
        {
            set
            {
                this.datas = value;
            }
            get
            {
                return this.datas;
            }
        }
    }
}
