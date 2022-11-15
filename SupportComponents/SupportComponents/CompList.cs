using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents
{
    public class CompList
    {

        /// <summary>
        /// 比较集合元素大小
        /// 返回最大值
        /// </summary>
        /// <returns></returns>
        public static int CompToList(List<double> list)
        {
            var max = list[0];
            for(int i = 0;i<list.Count;i++){

                max = list[i] > max ? list[i] : max;

            }

            return Convert.ToInt32(max);
        }

        /// <summary>
        /// 假期类型
        /// </summary>
        /// <returns></returns>
        public static System.Collections.Hashtable LeaveType()
        {

            System.Collections.Hashtable types = new System.Collections.Hashtable();
            types["病假"] = 1;
            types["事假"] = 2;
            types["产假"] = 3;
            types["公假"] = 4;
            types["婚假"] = 5;
            types["丧假"] = 6;
            types["出差"] = 7;
            types["调休"] = 8;
            return types;
        }
        /// <summary>
        /// 假期时长
        /// </summary>
        /// <returns></returns>
        public static List<double> TimeLong()
        {
            List<double> Long = new List<double>();
            Long.Add(0.5);
            Long.Add(1.0);
            Long.Add(1.5);
            Long.Add(2.0);
            Long.Add(2.5);
            Long.Add(3.0);
            Long.Add(7.0);
            Long.Add(15.0);
            return Long;
        }

    }
}
