using System;
using System.Collections.Generic;
using System.Text;

namespace cheWeiXin
{
    public class Group
    {
        int id = 0;
        /// <summary>
        /// 分组ID
        /// </summary>
        public int Id{
            set{
                this.id = value;
            }
            get{
                return this.id;
            }
        }

        string name = "";
        /// <summary>
        /// 分组名称
        /// </summary>
        public string Name{
            set{
                this.name = value;
            }
            get{
                return this.name;
            }
        }

        int count = 0;
        /// <summary>
        /// 分组包含用户个数
        /// </summary>
        public int Count{
            set{
                this.count = value;
            }
            get{
                return this.count;
            }
        }
    }
}
