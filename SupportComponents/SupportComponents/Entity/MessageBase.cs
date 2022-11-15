using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents
{
    [Serializable]
   public abstract class MessageBase
    {
       public MessageBase()
       {
           this.CreateTime = DateTime.Now;
       }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType
        { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public DateTime CreateTime
        { get; set; }

        /// <summary>
        /// 数据内容
        /// </summary>
        public object DataContent
        { get; set; }
    }
}
