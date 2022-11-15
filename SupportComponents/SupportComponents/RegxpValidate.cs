using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace HS.SupportComponents
{
    public class RegxpValidate
    {


        private static Regex RegNumber = new Regex("^[0-9]+$");   //正整数

        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$"); //正整数或负整数

        private static Regex RegDecimal = new Regex("^(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*))$");//正整数或正小数

        //		private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //正或负的小数型  等价于^[+-]?\d+[.]?\d+$

        private static Regex RegNumericSign = new Regex(@"^[+-]?\d+(\.\d+)?$"); //正负(整数或小数)型

        private static Regex RegEmail = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 

        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");

        private static Regex RegMobile = new Regex(@"^(13[0-9]|158|159|188|189)\d{8}$");

        private static Regex RegTel1 = new Regex("^[0-9]{7,8}$");     //固定电话(7或8位不带分机号和区号)

        private static Regex RegTel2 = new Regex("^[0-9]{3,4}-[0-9]{7,8}$");//带(3或4位)区号

        private static Regex RegTel3 = new Regex("^[0-9]{7,8}-[0-9]{3,6}$");//带3到6位分机号

        private static Regex RegTel4 = new Regex("^[0-9]{3,4}-[0-9]{7,8}-[0-9]{3,6}$");//带区号和分机号

        private static Regex RegPostCode = new Regex(@"\d{6}");


        #region 数字字符串检查

        /// <summary>
        /// 检查Request查询字符串的键值，是否是数字，最大长度限制
        /// </summary>
        /// <param name="req">Request</param>
        /// <param name="inputKey">Request的键值</param>
        /// <param name="maxLen">最大长度</param>
        /// <returns>返回Request查询字符串</returns>
        public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
        {
            string retVal = string.Empty;
            if (inputKey != null && inputKey != string.Empty)
            {
                retVal = req.QueryString[inputKey];
                if (null == retVal)
                    retVal = req.Form[inputKey];
                if (null != retVal)
                {
                    retVal = SqlText(retVal, maxLen);
                    if (!IsNumber(retVal))
                        retVal = string.Empty;
                }
            }
            if (retVal == null)
                retVal = string.Empty;
            return retVal;
        }
        /// <summary>
        /// 是否数字字符串
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string inputData)
        {
            return RegNumber.Match(inputData).Success;
        }
        /// <summary>
        /// 是否数字字符串可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            return RegNumberSign.Match(inputData).Success;
        }
        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            foreach (char cha in inputData)
            {
                if (char.IsLetter(cha))
                {
                    return false;
                }
            }

            if (inputData.IndexOf("-") >= 0)
            {
                return false;
            }
            return RegDecimal.Match(inputData).Success;
        }

        /// <summary>
        /// 是否是浮点数可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        //		public static bool IsDecimalSign(string inputData)
        //		{
        //			return RegDecimalSign.Match(inputData).Success;
        //		}

        /// <summary>
        /// 整数和浮点混合型(可带正负号)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            return RegNumericSign.Match(str).Success;
        }


        #endregion

        #region　电话邮编检测

        /// <summary>
        /// 是否国内电话号码
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsTel(string inputData)
        {
            return (RegTel1.Match(inputData).Success || RegTel2.Match(inputData).Success || RegTel3.Match(inputData).Success || RegTel4.Match(inputData).Success);
        }

        /// <summary>
        /// 是否正确邮编
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsPostCode(string inputData)
        {
            return RegPostCode.Match(inputData).Success;
        }

        /// <summary>
        /// 是否正确手机号码
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsMobile(string inputData)
        {
            return RegMobile.Match(inputData).Success;
        }
        #endregion

        #region 中文检测


        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns>含中文则返回true,否则返回false</returns>
        public static bool IsHasCHZN(string inputData)
        {
            return RegCHZN.Match(inputData).Success;
        }
        #endregion

        #region 邮件地址
        /// <summary>
        /// 是否是浮点数可带正负号

        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string inputData)
        {
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 其他
        /// <summary>
        /// 检查字符串最大长度，返回指定长度的串
        /// </summary>
        /// <param name="sqlInput">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>     
        public static string SqlText(string sqlInput, int maxLength)
        {
            if (sqlInput != null && sqlInput != string.Empty)
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength)//按最大长度截取字符串
                    sqlInput = sqlInput.Substring(0, maxLength);
            }
            return sqlInput;
        }
        /// <summary>
        /// 字符串编码
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static string HtmlEncode(string inputData)
        {
            return HttpUtility.HtmlEncode(inputData);
        }
        /// <summary>
        /// 设置Label显示Encode的字符串
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="txtInput"></param>
        public static void SetLabel(Label lbl, string txtInput)
        {
            lbl.Text = HtmlEncode(txtInput);
        }
        public static void SetLabel(Label lbl, object inputObj)
        {
            SetLabel(lbl, inputObj.ToString());
        }
        public static bool isDateTime(string strInput)
        {
            bool flag = true;
            try
            {
                DateTime DT = Convert.ToDateTime(strInput);
            }
            catch
            {
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 日期验证
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime1(string strTime)
        {
            //YYY-MM-DD 闰年和2月等的情况都考虑进去了
            string regstr = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(regstr);
            return reg1.IsMatch(strTime);

        }
        #endregion

        #region "检测是否超过输入最大数(包括中文)"
        /// <summary>
        /// 判断输入的字符是否超出最大数包括中文
        /// </summary>
        /// <param name="strText"></param>
        /// <param name="Alength"></param>
        /// <returns></returns>
        public static bool GetTextLength(string strText, int Alength)
        {
            string strTb = "";
            string strTest = "";
            strTb = strText;
            int t = 0;
            for (int i = 0; i < strTb.Length; i++)
            {

                strTest = strTb[i].ToString();
                bool yn = Regex.IsMatch(strTest, @"[\u4e00-\u9fa5]+");
                if (yn)
                {
                    t = t + 2;
                }
                else
                {
                    t = t + 1;
                }
            }
            if (t > Alength)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region 判断用户名或密码是否有非常字符并进行提示
        /// <summary>
        /// 判断用户名或密码是否有非常字符并进行提示
        /// </summary>
        /// <param name="cUserNo"></param>
        /// <returns></returns>
        public static int checkUserNo(string cUserNo) //用户名是否正确'",:*?/|\><
        {
            if (cUserNo.IndexOf("'") != -1)
            {
                return 1;
            }
            if (cUserNo.IndexOf("\"") != -1)
            {
                return 1;
            }
            if (cUserNo.IndexOf(",") != -1)
            {
                return 1;
            }
            if (cUserNo.IndexOf(":") != -1)
            {
                return 1;
            }
            if (cUserNo.IndexOf("*") != -1)
            {
                return 1;
            }
            if (cUserNo.IndexOf("?") != -1)
            {
                return 1;
            }
            if (cUserNo.IndexOf("/") != -1)
            {
                return 1;
            }
            if (cUserNo.IndexOf("|") != -1)
            {
                return 1;
            }
            if (cUserNo.IndexOf("\\") != -1)
            {
                return 1;
            }
            if (cUserNo.IndexOf(">") != -1)
            {
                return 1;
            }
            if (cUserNo.IndexOf("<") != -1)
            {
                return 1;
            }
            return 0;
        }
        #endregion

        #region
        /// <summary>
        /// 验证密码是否是字母与数字的组合
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool CheckPwd(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[0-9]+$");
            System.Text.RegularExpressions.Regex reg2 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z]+$");
            if (reg1.IsMatch(str) && reg2.IsMatch(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
