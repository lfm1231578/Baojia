using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace HS.SupportComponents
{
    /// <summary>
    /// IP处理辅助类
    /// </summary>
    public sealed class IPHelper : System.Web.UI.Page
    {
        private const string _IP = "000";
        public IPHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static bool IsIPSect(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");
        }

        /// <summary>
        /// 转换成三位IP地址[ Old:192.168.1.1 New:192.168.001.001 ]
        /// </summary>
        /// <param name="IP">IP地址</param>
        /// <returns></returns>
        public static string ToIP(string IP)
        {
            /*
			StringBuilder ip =  new StringBuilder();
			string []ips = IP.Split('.');
			if ( ips.Length == 4) 
			{
				for(int i=0;i<ips.Length;i++ )
				{
					if ( i> 0)
						ip.Append( "." );
					ips[i] = _IP.Substring( 0, _IP.Length - ips[i].Length) + ips[i];
					ip.Append( ips[i] );
				}
			}
			else
				ip.Append( IP );
			return ip.ToString();
            */
            return IP;
        }

        /// <summary>
        /// 是否为LAN IP
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns></returns>
        public static bool IsLANIP(IPAddress ip)
        {
            string[] split = ip.ToString().Split('.');
            // 10.0.0.0   255.0.0.0
            if ((int.Parse(split[0]) & 255) == 10)
                return true;
            // 192.168.0.0  255.255.0.0
            if (((int.Parse(split[0]) & 255) == 192) && ((int.Parse(split[1]) & 255) == 168))
                return true;
            // 172.16.0.0  255.240.0.0
            if (((int.Parse(split[0]) & 255) == 172) && ((int.Parse(split[1]) & 240) == 16))
                return true;
            return false;
        }

        public static long ConvertIPToLong(string IP)
        {
            string[] node = IP.Split('.');
            if (node.Length != 4)
                return 0;

            for (int i = 0; i < 4; i++)
            {
                if (!IsInt(node[i]))
                    return 0;
            }
            long irst = Convert.ToInt64(node[0]) << 24;
            irst += Convert.ToInt64(node[1]) << 16;
            irst += Convert.ToInt64(node[2]) << 8;
            irst += Convert.ToInt64(node[3]);
            return irst;
        }

        /// <summary>
        /// 判断字符串是否数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static bool IsInt(string str)
        {
            if (str == null)
                return false;
            str = str.Trim();
            if (str.Length == 0)
                return false;

            foreach (char ch in str.ToCharArray())
            {
                if (char.IsNumber(ch) == false)
                    return false;
            }
            return true;
        }

        public static string ToLongIP(string ip)
        {
            string[] ips = ip.Split('.');
            ip = "";
            if (ips.Length == 4)
            {
                foreach (string subIP in ips)
                {
                    ip = string.Format("{0}{1}", string.IsNullOrEmpty(ip) ? "" : ip + ".", subIP.PadLeft(3, '0'));
                }
            }
            return ip;
        }

        public static string[] GetIPAdd(string IP)
        {
            string[] adds = null;
                string CurrentPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string filePath  = CurrentPath + @"/qqwry.dat";
            IPSearch cnip = new IPSearch(filePath);
            IPEntity ip = cnip.Query(IP);  //查询一个IP地址
            //Response.Write(ip.IP + ":" + ip.Country + ":" + ip.Local);//118.123.14.88:四川省绵阳市:电信IDC机房(科技城机房)
            if (ip.Country.Contains("省"))
            {
                adds = ip.Country.Split('省');
            }
            else
            {
                adds = new string[2];
                adds[0] = ip.Country;
                adds[1] = "本地";
            }
            return adds;
        }
    }
}
