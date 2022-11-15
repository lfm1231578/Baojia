using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents
{
    public class NoRecord
    {

        /// <summary>
        /// 当表格没有数据时
        /// 显示提示信息
        /// </summary>
        /// <param name="count">List的长度</param>
        /// <param name="colspan">td要跨的列数</param>
        /// <returns></returns>
        public static string NoRecords(int count,int colspan) 
        {


            if (count > 0)
            {
                return null;
            }
            else {

                return "<tr><td colspan=" + colspan + ">亲，暂时没有记录哦！</td></tr>";

            }


        }

    }
}
