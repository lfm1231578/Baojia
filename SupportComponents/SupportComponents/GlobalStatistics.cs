using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HS.SupportComponents
{
    /// <summary>
    /// GlobalStatistics 的摘要说明。
    /// </summary>
    public class GlobalStatistics
    {
        public GlobalStatistics()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 已发送的消息统计
        public static void AddOneMessage()
        {
            Interlocked.Increment(ref totalMessage);
        }
        public static long TotalMessage
        {
            get { return totalMessage; }
        }
        private static long totalMessage = 0;
        #endregion
    }
}
