using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents
{
    /// <summary>
    /// CCIC辅助类
    /// </summary>
    public class CCICHeler
    {
        /// <summary>
        /// 转换CCIC格式[以空格做分隔符转换成以逗号分隔的]
        /// </summary>
        /// <param name="line">一行内容</param>
        /// <returns></returns>
        public static string CCICConvert(string line)
        {
            // 例:6103231051988060034	刘占林		1	1965/07/28	1965/07/28	610323650728681	610323	雍川镇麦禾营村二组	610323	雍川镇麦禾营村二组					0	6103231051988060034	050200		01	00	00	0	610323	吕玉岐	09178330173
            StringBuilder sb = new StringBuilder();
            string sTmp1 = "";// 临时比较字符串
            string sTmp2 = "";// 临时比较字符串

            int idxMax = line.Trim().Length;

            int iSignCnt = 0;  // ","号累加次数,超过2次就跳出循环
            for (int i = 0; i < line.Length; i++)
            {
                sTmp1 = line.Substring(i, 1);

                if (idxMax > i + 1)
                    sTmp2 = line.Substring(i + 1, 1);

                if (sTmp1 == "\t")
                    sTmp1 = " ";
                if (sTmp2 == "\t")
                    sTmp2 = " ";

                //如果2个字符不为空[1,1]则累加或第1个字符不为空[1, ]则累加
                if ((sTmp1 != " " && sTmp2 != " ") || (sTmp1 != " " && sTmp2 == " "))
                    sb.Append(sTmp1);
                if (sTmp1 == " " && sTmp2 == " ")
                    continue;

                //如果第2个字符不为空[ ,1]则加入","号
                if (sTmp1 == " " && sTmp2 != " ")
                {
                    sb.Append(",");
                    iSignCnt++;
                }
            }
            return sb.ToString();
        }
    }
}
