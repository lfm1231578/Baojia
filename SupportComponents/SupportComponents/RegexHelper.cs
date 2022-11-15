using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HS.SupportComponents
{
    /// <summary>
    /// 正则表达式处理的辅助类
    /// </summary>
    public sealed class RegexHelper
    {
        private static Regex regNumber = new Regex(@"^[-]?\d+[.]?\d*$"); // 验证字符串是否为数字
        private static Regex regEmail = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"); // 验证字符串是否为Email
        private static Regex regChinese = new Regex(@"[\u4e00-\u9fa5]"); // 验证字符串是否为中文
        private static Regex regMac = new Regex(@"^([0-9a-fA-F]{2})(([/\s:-][0-9a-fA-F]{2}){5})$"); // 验证字符串是否为MAC地址
        private static Regex regPhone = null;   // 获取电话号码集合的正则表达式
        private static Regex regCertificate = null;   //获取聊天内容的证件号码正则表达式
        private static Regex regCertificateNo = new Regex(@"^(\d{18,18}|\d{15,15}|\d{17,17}x|\d{17,17}X)$"); //验证18位和15位身份证
        private static Regex regIP = new Regex(@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"); // 验证字符串是否为IP
        private static Regex regTime = null;   // 验证字符串是否为时间型
        private static Regex regDate = null;   // 验证字符串是否为日期型
        private static Regex regDateTime = null;   // 验证字符串是否为日期+时间型
        private static Regex isPhone = new Regex(@"^(1)[0-9]{10}$");//验证是否是手机号
        static RegexHelper()
        {
            if (regPhone == null)
                regPhone = new Regex(@"((?:[+( ]*61[-) ]*)?(?:\(? *0?\)? *(\d) ?[-)]?\s*)?(?:\d{5,}(?:[ -]\d+)*|\d{1,4}(?:[ -]\d+)+))", RegexOptions.Compiled);

            if (regCertificate == null)
                regCertificate = new Regex(@"\d{18}|(\d{17}[X])|(\d{17}[x])|\d{15}", RegexOptions.Compiled);

            if (regTime == null)
                regTime = new Regex(@"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$", RegexOptions.Compiled);

            if (regDate == null)
                regDate = new Regex(@"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$", RegexOptions.Compiled);

            if (regDateTime == null)
                regDateTime = new Regex(@"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ", RegexOptions.Compiled);
        }

        #region 字符串验证方法
        /// <summary>
        /// 验证字符串是否为数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string str)
        {
            return regNumber.IsMatch(str);
        }
        /// <summary>
        /// 验证字符串是否为手机号
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static bool IsPhone(string str)
        {
            return isPhone.IsMatch(str);
        }
        /// <summary>
        /// 验证字符串是否为IP
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static bool IsIP(string str)
        {
            return regIP.IsMatch(str);
        }

        /// <summary>
        /// 验证字符串是否为Email
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string str)
        {
            return regEmail.IsMatch(str);
        }

        /// <summary>
        /// 验证字符串是否为中文
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static bool IsChinese(string str)
        {
            return !regChinese.IsMatch(str);
        }

        /// <summary>
        /// 验证字符串是否为MAC地址
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static bool IsMac(string str)
        {
            return regMac.IsMatch(str);
        }

        /// <summary>
        /// 验证字符串是否为身份证18位和15位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsCertificate(string str)
        {
            return regCertificateNo.IsMatch(str);
        }

        /// <summary>          
        /// 是否为时间型字符串          
        /// </summary>          
        /// <param name="source">时间字符串(15:00:00)</param>          
        /// <returns></returns>  
        public static bool IsTime(string str)
        {
            return regTime.IsMatch(str);
        }

        /// <summary>          
        /// 是否为日期型字符串          
        /// </summary>          
        /// <param name="StrSource">日期字符串(2008-05-08)</param>          
        /// <returns></returns>  
        public static bool IsDate(string str)
        {
            return regDate.IsMatch(str);
        }

        /// <summary>          
        /// 是否为日期+时间型字符串          
        /// </summary>          
        /// <param name="source"></param>          
        /// <returns></returns>  
        public static bool IsDateTime(string str)
        {
            return regDateTime.IsMatch(str);
        }
        #endregion

        /// <summary>
        /// 获取字符串中的电话号码
        /// </summary>
        /// <param name="Content">字符串</param>
        /// <returns></returns>
        public static List<string> GetPhone(string Content)
        {
            List<string> listPhone = new List<string>();
            foreach (Match m in regPhone.Matches(Content))
            {
                if (m.Value.Length > 30)
                    continue;

                if (!listPhone.Contains(m.Value))
                    listPhone.Add(m.Value.Trim());
            }
            return listPhone;
        }

        /// <summary>
        /// 获取证件号码集合
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static List<string> GetCertificate(string Content)
        {
            List<string> listID = new List<string>();
            string ID = string.Empty;
            foreach (Match m in regCertificate.Matches(Content))
            {
                ID = m.Value.Trim();
                if ((ID.Length == 15 || ID.Length == 18) && !listID.Contains(ID))
                    listID.Add(ID);
            }
            return listID;
        }
    }
}