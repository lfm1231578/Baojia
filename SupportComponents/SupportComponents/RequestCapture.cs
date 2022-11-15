using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HS.SupportComponents
{
    /// <summary>
    /// 请求捕获 用于获取用户请求的来源
    /// </summary>
    public class RequestCapture
    {
        /// <summary>
        /// 判断是否是搜索引擎
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsSpider(HttpRequest request)
        {
            //google蜘蛛： googlebot
            //百度蜘蛛：baiduspider
            //yahoo蜘蛛：slurp
            //soso蜘蛛：sosospider
            //msn蜘蛛：msnbot
            //有道蜘蛛：YodaoBot和OutfoxBot
            //搜狗蜘蛛：sougouspider​​
            List<string> listSpider = new List<string>();
            listSpider.AddRange(new string[] {"spider","baiduspider", "googlebot","sogou","sougouspider","sosospider","yahoo", "yodaobot"
            ,"slurp", "sosoimagespider", "msnbot","msnbot","gigabot"
             ,"outfoxbot","webalta"
        });
            for (int i = 0; i < listSpider.Count; i++)
            {
                if (string.IsNullOrEmpty(request.UserAgent) && request.UserAgent.ToLower().IndexOf(listSpider[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
