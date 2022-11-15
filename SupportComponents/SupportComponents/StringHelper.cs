using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using HS.SupportComponents.Definition;
namespace HS.SupportComponents
{
    /// <summary>
    /// 字串帮助器
    /// </summary>
    public class StringHelper
    {
        private static Regex RegexBr = new Regex(@"(\r\n)", RegexOptions.IgnoreCase);

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit)
        {
            if (!StrIsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                    return new string[] { strContent };

                return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
                return new string[0] { };
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, int count)
        {
            string[] result = new string[count];
            string[] splited = SplitString(strContent, strSplit);

            for (int i = 0; i < count; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// 过滤字符串数组中每个元素为合适的大小
        /// 当长度小于minLength时，忽略掉,-1为不限制最小长度
        /// 当长度大于maxLength时，取其前maxLength位
        /// 如果数组中有null元素，会被忽略掉
        /// </summary>
        /// <param name="minLength">单个元素最小长度</param>
        /// <param name="maxLength">单个元素最大长度</param>
        /// <returns></returns>
        public static string[] PadStringArray(string[] strArray, int minLength, int maxLength)
        {
            if (minLength > maxLength)
            {
                int t = maxLength;
                maxLength = minLength;
                minLength = t;
            }

            int iMiniStringCount = 0;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (minLength > -1 && strArray[i].Length < minLength)
                {
                    strArray[i] = null;
                    continue;
                }
                if (strArray[i].Length > maxLength)
                    strArray[i] = strArray[i].Substring(0, maxLength);

                iMiniStringCount++;
            }

            string[] result = new string[iMiniStringCount];
            for (int i = 0, j = 0; i < strArray.Length && j < result.Length; i++)
            {
                if (strArray[i] != null && strArray[i] != string.Empty)
                {
                    result[j] = strArray[i];
                    j++;
                }
            }
            return result;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <param name="maxElementLength">单个元素最大长度</param>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem, int maxElementLength)
        {
            string[] result = SplitString(strContent, strSplit);

            return ignoreRepeatItem ? DistinctStringArray(result, maxElementLength) : result;
        }

        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem, int minElementLength, int maxElementLength)
        {
            string[] result = SplitString(strContent, strSplit);

            if (ignoreRepeatItem)
            {
                result = DistinctStringArray(result);
            }
            return PadStringArray(result, minElementLength, maxElementLength);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="strContent">被分割的字符串</param>
        /// <param name="strSplit">分割符</param>
        /// <param name="ignoreRepeatItem">忽略重复项</param>
        /// <returns></returns>
        public static string[] SplitString(string strContent, string strSplit, bool ignoreRepeatItem)
        {
            return SplitString(strContent, strSplit, ignoreRepeatItem, 0);
        }

        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <param name="maxElementLength">字符串数组中单个元素的最大长度</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(string[] strArray, int maxElementLength)
        {
            Hashtable h = new Hashtable();

            foreach (string s in strArray)
            {
                string k = s;
                if (maxElementLength > 0 && k.Length > maxElementLength)
                {
                    k = k.Substring(0, maxElementLength);
                }
                h[k.Trim()] = s;
            }

            string[] result = new string[h.Count];

            h.Keys.CopyTo(result, 0);

            return result;
        }

        /// <summary>
        /// 清除字符串数组中的重复项
        /// </summary>
        /// <param name="strArray">字符串数组</param>
        /// <returns></returns>
        public static string[] DistinctStringArray(string[] strArray)
        {
            return DistinctStringArray(strArray, 0);
        }

        /// <summary>
        /// 字段串是否为Null或为""(空)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StrIsNullOrEmpty(string str)
        {
            if (str == null || str.Trim() == string.Empty)
                return true;

            return false;
        }

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>合并到的目的字符串</returns>
        public static string MergeString(string source, string target)
        {
            return MergeString(source, target, ",");
        }

        /// <summary>
        /// 合并字符
        /// </summary>
        /// <param name="source">要合并的源字符串</param>
        /// <param name="target">要被合并到的目的字符串</param>
        /// <param name="mergechar">合并符</param>
        /// <returns>并到字符串</returns>
        public static string MergeString(string source, string target, string mergechar)
        {
            if (StrIsNullOrEmpty(target))
                target = source;
            else
                target += mergechar + source;

            return target;
        }

        /// <summary>
        /// 删除最后一个字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearLastChar(string str)
        {
            return (str == "") ? "" : str.Substring(0, str.Length - 1);
        }

        /// <summary>
        /// 为脚本替换特殊字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceStrToScript(string str)
        {
            return str.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\"");
        }

        /// <summary>
        /// 格式化字节数字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatBytesStr(int bytes)
        {
            if (bytes > 1073741824)
                return ((double)(bytes / 1073741824)).ToString("0") + "G";

            if (bytes > 1048576)
                return ((double)(bytes / 1048576)).ToString("0") + "M";

            if (bytes > 1024)
                return ((double)(bytes / 1024)).ToString("0") + "K";

            return bytes.ToString() + "Bytes";
        }

        /// <summary>
        /// 进行指定的替换(脏字过滤)
        /// </summary>
        public static string StrFilter(string str, string bantext)
        {
            string text1 = "", text2 = "";
            string[] textArray1 = SplitString(bantext, "\r\n");
            for (int num1 = 0; num1 < textArray1.Length; num1++)
            {
                text1 = textArray1[num1].Substring(0, textArray1[num1].IndexOf("="));
                text2 = textArray1[num1].Substring(textArray1[num1].IndexOf("=") + 1);
                str = str.Replace(text1, text2);
            }
            return str;
        }

        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(string str)
        {
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }
        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 检测是否有危险的可能用于链接的字符串
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|游客|^Guest");
        }

        /// <summary>
        /// 清理字符串
        /// </summary>
        public static string CleanInput(string strIn)
        {
            return Regex.Replace(strIn.Trim(), @"[^\w\.@-]", "");
        }

        /// <summary>
        /// 检测是否符合email格式
        /// </summary>
        /// <param name="strEmail">要判断的email字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsValidEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
        }

        public static bool IsValidDoEmail(string strEmail)
        {
            return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 检测是否是正确的Url
        /// </summary>
        /// <param name="strUrl">要验证的Url</param>
        /// <returns>判断结果</returns>
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }

        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
        }

        public static string GetUnicodeSubString(string str, int len, string p_TailString)
        {
            string result = string.Empty;// 最终返回的结果
            int byteLen = System.Text.Encoding.Default.GetByteCount(str);// 单字节字符长度
            int charLen = str.Length;// 把字符平等对待时的字符串长度
            int byteCount = 0;// 记录读取进度
            int pos = 0;// 记录截取位置
            if (byteLen > len)
            {
                for (int i = 0; i < charLen; i++)
                {
                    if (Convert.ToInt32(str.ToCharArray()[i]) > 255)// 按中文字符计算加2
                        byteCount += 2;
                    else// 按英文字符计算加1
                        byteCount += 1;
                    if (byteCount > len)// 超出时只记下上一个有效位置
                    {
                        pos = i;
                        break;
                    }
                    else if (byteCount == len)// 记下当前位置
                    {
                        pos = i + 1;
                        break;
                    }
                }

                if (pos >= 0)
                    result = str.Substring(0, pos) + p_TailString;
            }
            else
                result = str;

            return result;
        }

        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_StartIndex">起始位置</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string myResult = p_SrcString;

            Byte[] bComments = Encoding.UTF8.GetBytes(p_SrcString);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {    //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                    //当截取的起始位置超出字段串长度时
                    if (p_StartIndex >= p_SrcString.Length)
                        return "";
                    else
                        return p_SrcString.Substring(p_StartIndex,
                                                       ((p_Length + p_StartIndex) > p_SrcString.Length) ? (p_SrcString.Length - p_StartIndex) : p_Length);
                }
            }

            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }

                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                                nFlag = 1;
                        }
                        else
                            nFlag = 0;

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                        nRealLength = p_Length + 1;

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + p_TailString;
                }
            }

            return myResult;
        }

        /// <summary>
        /// 自定义的替换字符串函数
        /// </summary>
        public static string ReplaceString(string SourceString, string SearchString, string ReplaceString, bool IsCaseInsensetive)
        {
            return Regex.Replace(SourceString, Regex.Escape(SearchString), ReplaceString, IsCaseInsensetive ? RegexOptions.IgnoreCase : RegexOptions.None);
        }

        /// <summary>
        /// 生成指定数量的html空格符号
        /// </summary>
        public static string GetSpacesString(int spacesCount)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < spacesCount; i++)
            {
                sb.Append(" &nbsp;&nbsp;");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        public static bool IsCompriseStr(string str, string stringarray, string strsplit)
        {
            if (StrIsNullOrEmpty(stringarray))
                return false;

            str = str.ToLower();
            string[] stringArray = SplitString(stringarray.ToLower(), strsplit);
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (str.IndexOf(stringArray[i]) > -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>
        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                        return i;
                }
                else if (strSearch == stringArray[i])
                    return i;
            }
            return -1;
        }


        /// <summary>
        /// 判断指定字符串在指定字符串数组中的位置
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <returns>字符串在指定字符串数组中的位置, 如不存在则返回-1</returns>		
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="strSearch">字符串</param>
        /// <param name="stringArray">字符串数组</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0;
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">字符串数组</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string[] stringarray)
        {
            return InArray(str, stringarray, false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray)
        {
            return InArray(str, SplitString(stringarray, ","), false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return InArray(str, SplitString(stringarray, strsplit), false);
        }

        /// <summary>
        /// 判断指定字符串是否属于指定字符串数组中的一个元素
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="stringarray">内部以逗号分割单词的字符串</param>
        /// <param name="strsplit">分割字符串</param>
        /// <param name="caseInsensetive">是否不区分大小写, true为不区分, false为区分</param>
        /// <returns>判断结果</returns>
        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, SplitString(stringarray, strsplit), caseInsensetive);
        }


        /// <summary>
        /// 删除字符串尾部的回车/换行/空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RTrim(string str)
        {
            for (int i = str.Length; i >= 0; i--)
            {
                if (str[i].Equals(" ") || str[i].Equals("\r") || str[i].Equals("\n"))
                {
                    str.Remove(i, 1);
                }
            }
            return str;
        }


        /// <summary>
        /// 清除给定字符串中的回车及换行符
        /// </summary>
        /// <param name="str">要清除的字符串</param>
        /// <returns>清除后返回的字符串</returns>
        public static string ClearBR(string str)
        {
            Match m = null;

            for (m = RegexBr.Match(str); m.Success; m = m.NextMatch())
            {
                str = str.Replace(m.Groups[0].ToString(), "");
            }
            return str;
        }

        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length * -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                        startIndex = startIndex - length;
                }

                if (startIndex > str.Length)
                    return "";
            }
            else
            {
                if (length < 0)
                    return "";
                else
                {
                    if (length + startIndex > 0)
                    {
                        length = length + startIndex;
                        startIndex = 0;
                    }
                    else
                        return "";
                }
            }

            if (str.Length - startIndex < length)
                length = str.Length - startIndex;

            return str.Substring(startIndex, length);
        }

        /// <summary>
        /// 从字符串的指定位置开始截取到字符串结尾的了符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }

        /// <summary>
        /// 字符串过滤
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="filterChar">要过滤的字符</param>
        /// <returns></returns>
        public static string FixString(string str, char filterChar)
        {
            string tempstr = str;
            if (tempstr.Length > 1)
            {
                if (tempstr.Substring(0, 1) == filterChar.ToString())
                {
                    tempstr = tempstr.Substring(1, tempstr.Length - 1);
                }
            }
            return tempstr;
        }

        /// <summary>
        /// 把字串转换为UTF8的byte数据
        /// </summary>
        /// <param name="utf8String"></param>
        /// <returns></returns>
        public static byte[] ConvertUTF8ToBytes(string utf8String)
        {
            byte[] rst = System.Text.UTF8Encoding.UTF8.GetBytes(utf8String);
            return rst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="utf8Bytes"></param>
        /// <returns></returns>
        public static string ConvertUTF8Bytes2String(byte[] utf8Bytes)
        {
            string utf8String = System.Text.UTF8Encoding.UTF8.GetString(utf8Bytes);
            return utf8String;
        }

        class Aaa
        {
            public DateTime Dt { get; set; }
            public string Name { get; set; }
        }


        /// <summary>
        /// 打开队列器
        /// </summary>
        static void Open()
        {
            //test...
            List<Aaa> al = new List<Aaa>();
            Aaa x = new Aaa();
            x.Name = "jjy1";
            al.Add(x);
            x = new Aaa();
            x.Name = "jjy2";
            al.Add(x);
            string xml = StringHelper.BuildEntityXml<List<Aaa>>(al);
            //test.
        }

        static StringHelper()
        {
            Open();
        }

        /// <summary>
        /// 根据对象生成XML节点
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static XmlNode CreateXmlFromObject(XmlDocument xmlDoc, object obj)
        {
            XmlElement xndRow = xmlDoc.CreateElement("ROW");

            PropertyInfo[] pInfos = obj.GetType().GetProperties();

            foreach (PropertyInfo pInfo in pInfos)
            {
                string pName = pInfo.Name;
                object pValue = pInfo.GetValue(obj, null);
                try
                {
                    XmlAttribute xndAttr = xmlDoc.CreateAttribute(pName);
                    xndAttr.Value = pValue.ToString();
                    xndRow.Attributes.Append(xndAttr);
                }
                catch (Exception ex)
                { }
            }

            return xndRow;
        }

        /// <summary>
        /// 创建实体对象的Xml片段字符串。
        /// </summary>
        /// <typeparam name="T">实体的类型。</typeparam>
        /// <param name="list">实体集合。</param>
        /// <returns>实体对象的Xml片段字符串。</returns>
        public static string BuildEntityXml<T>(T entity) where T : class
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<ROOT>");
            try
            {
                if (entity != null)
                {
                    PropertyInfo[] infos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance);
                    if (infos != null && infos.Length > 0)
                    {
                        sb.Append(string.Format("<ROW"));
                        foreach (PropertyInfo info in infos)
                        {
                            try
                            {
                                object val = typeof(T).InvokeMember(info.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance, null, entity, null);

                                // 若是DateTime对象，而且未设置值，则忽略此数据 JJY added on 2009.9.3
                                //根据时间范围来判断时间是否正常，如果不在范围内，则忽略此时间  Tan  2010-06-24
                                //if (info.ToString().StartsWith(typeof(DateTime).FullName + " "))
                                //{
                                //    DateTime thisTime = Convert.ToDateTime(val);
                                //    DateTime MaxDate = new DateTime(2078, 1, 1);
                                //    DateTime MinDate = new DateTime(1900, 1, 1);
                                //    if (DateTime.MinValue.Equals(thisTime) || thisTime >= MaxDate || thisTime <= MinDate)
                                //        continue;
                                //}

                                sb.AppendFormat(" {0}=\"{1}\"", info.Name, FixXml(ConvertToString(val)));
                            }
                            catch { }
                        }
                        ////
                        sb.Append(" />\r\n");
                    }
                }
            }
            catch (Exception ex)
            {
                //SysTrack.Tracker.ErrorTrack(ex, "创建实体的Xml错误，实体类型：" + typeof(T).ToString());
                throw ex;
            }
            sb.Append("</ROOT>\r\n");
            return sb.ToString();
        }

        /// <summary>
        /// 创建实体集合的Xml文档字符串。
        /// </summary>
        /// <typeparam name="T">实体的类型。</typeparam>
        /// <param name="list">实体集合。</param>
        /// <returns>Xml字符串。</returns>
        public static string BuildEntityXml<T>(IList<T> list) where T : class
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("<?xml version=\"1.0\" encoding=\"" + System.Text.Encoding.Default.BodyName + "\" ?>");
            sb.AppendLine("<ROOT>");

            try
            {
                if (list != null && list.Count > 0)
                {
                    PropertyInfo[] infos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance);
                    if (infos != null && infos.Length > 0)
                    {
                        foreach (T obj in list)
                        {
                            if (obj == null)
                                continue;

                            sb.Append("\t<ROW");
                            foreach (PropertyInfo info in infos)
                            {
                                try
                                {
                                    object val = typeof(T).InvokeMember(info.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance, null, obj, null);

                                    // 若是DateTime对象，而且未设置值，则忽略此数据 JJY added on 2009.9.3
                                    //根据时间范围来判断时间是否正常，如果不在范围内，则忽略此时间  Tan  2010-06-24
                                    if (info.ToString().StartsWith(typeof(DateTime).FullName + " "))
                                    {
                                        DateTime thisTime = Convert.ToDateTime(val);
                                        DateTime MaxDate = new DateTime(2078, 1, 1);
                                        DateTime MinDate = new DateTime(1900, 1, 1);
                                        if (DateTime.MinValue.Equals(thisTime) || thisTime >= MaxDate || thisTime <= MinDate)
                                            continue;
                                    }

                                    sb.AppendFormat(" {0}=\"{1}\"", info.Name, FixXml(ConvertToString(val)));
                                }
                                catch { }
                            }
                            sb.Append(" />\r\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Netbox.Pass.PassHelper.SysTrack.Tracker.ErrorTrack(ex, "创建实体集合的Xml错误，实体类型：" + typeof(T).ToString());
                throw ex;
            }
            sb.Append("</ROOT>\r\n");
            return sb.ToString();
        }

        /// <summary>
        /// 创建实体集合的Xml文档字符串。
        /// </summary>
        /// <typeparam name="T">实体的类型。</typeparam>
        /// <param name="list">实体集合。</param>
        /// <returns>Xml字符串。</returns>
        public static string BuildEntityXml<T>(IList<T> list, bool includeHeader) where T : class
        {

            StringBuilder sb = new StringBuilder();
            if (includeHeader)
                sb.AppendLine("<?xml version=\"1.0\" encoding=\"" + System.Text.Encoding.Default.BodyName + "\" ?>");

            sb.AppendLine(BuildEntityXml<T>(list));

            return sb.ToString();
        }

        /// <summary>
        /// 创建实体集合的Xml文档字符串(最小日期将不创建)。
        /// </summary>
        /// <typeparam name="T">实体的类型。</typeparam>
        /// <param name="list">实体集合。</param>
        /// <returns>Xml字符串。</returns>
        public static string BuildXmlExceptMinDate<T>(IList<T> list) where T : class
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("<?xml version=\"1.0\" encoding=\"gb2312\" ?>\r\n");
            sb.Append("<ROOT>\r\n");

            try
            {
                if (list != null && list.Count > 0)
                {
                    PropertyInfo[] infos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance);
                    if (infos != null && infos.Length > 0)
                    {
                        foreach (T obj in list)
                        {
                            sb.Append("\t<ROW");
                            foreach (PropertyInfo info in infos)
                            {
                                try
                                {
                                    object val = typeof(T).InvokeMember(info.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance, null, obj, null);
                                    if (val is DateTime && (DateTime)val == DateTime.MinValue)
                                    {
                                        continue;
                                    }
                                    sb.AppendFormat(" {0}=\"{1}\"", info.Name, FixXml(ConvertToString(val)));
                                }
                                catch (SystemException ex)
                                {

                                }
                            }
                            sb.Append(" />\r\n");
                        }
                    }
                }
            }
            catch (SystemException ex)
            { }

            sb.Append("</ROOT>\r\n");
            return sb.ToString();
        }

        /// <returns>Xml字符串。</returns>
        /// <summary>
        /// 根据描述创建实体集合的Xml文档字符串。(全体属性构造)
        /// </summary>
        /// <typeparam name="T">实体的类型</typeparam>
        /// <param name="attributeType">自定义属性类型</param>
        /// <param name="list">实体集合</param>
        /// <returns>Xml字符串</returns>
        public static string BuildXmlByAttribute<T>(Type attributeType, IList<T> list) where T : class
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("<?xml version=\"1.0\" encoding=\"gb2312\" ?>\r\n");
            sb.Append("<ROOT>\r\n");

            try
            {
                if (list != null && list.Count > 0)
                {
                    PropertyInfo[] infos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance);
                    if (infos != null && infos.Length > 0)
                    {
                        foreach (T obj in list)
                        {
                            if (obj == null)
                                continue;

                            sb.Append("\t<ROW");
                            foreach (PropertyInfo info in infos)
                            {
                                try
                                {
                                    object val = typeof(T).InvokeMember(info.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance, null, obj, null);

                                    // 若是DateTime对象，而且未设置值，则忽略此数据 JJY added on 2009.9.3
                                    //根据时间范围来判断时间是否正常，如果不在范围内，则忽略此时间  Tan  2010-06-24
                                    if (info.ToString().StartsWith(typeof(DateTime).FullName + " "))
                                    {
                                        DateTime thisTime = Convert.ToDateTime(val);
                                        DateTime MaxDate = new DateTime(2078, 1, 1);
                                        DateTime MinDate = new DateTime(1900, 1, 1);
                                        if (DateTime.MinValue.Equals(thisTime) || thisTime >= MaxDate || thisTime <= MinDate)
                                            continue;
                                    }
                                    string propertyName = info.Name;
                                    if (attributeType.BaseType == typeof(BuildXmlAttributeBase))
                                    {
                                        BuildXmlAttributeBase[] objAttribute = (BuildXmlAttributeBase[])(info.GetCustomAttributes(attributeType, false));
                                        if (objAttribute != null && objAttribute.Length == 1)
                                        {
                                            propertyName = objAttribute[0].XmlPropertyName;
                                        }
                                    }
                                    sb.AppendFormat(" {0}=\"{1}\"", propertyName, FixXml(ConvertToString(val)));
                                }
                                catch { }
                            }
                            sb.Append(" />\r\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Netbox.Pass.PassHelper.SysTrack.Tracker.ErrorTrack(ex, "根据描述创建实体集合的Xml错误，实体类型：" + typeof(T).ToString());
                throw ex;
            }

            sb.Append("</ROOT>\r\n");
            return sb.ToString();
        }

        /// <returns>Xml字符串。</returns>
        /// <summary>
        /// 根据描述创建实体集合的Xml文档字符串。(仅attributeType描述相应属性构造)
        /// </summary>
        /// <typeparam name="T">实体的类型</typeparam>
        /// <param name="attributeType">自定义属性类型</param>
        /// <param name="list">实体集合</param>
        /// <returns>Xml字符串</returns>
        public static string BuildXmlByAttributePart<T>(Type attributeType, IList<T> list) where T : class
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("<?xml version=\"1.0\" encoding=\"gb2312\" ?>\r\n");
            sb.Append("<ROOT>\r\n");

            try
            {
                if (list != null && list.Count > 0)
                {
                    PropertyInfo[] infos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance);
                    if (infos != null && infos.Length > 0)
                    {
                        foreach (T obj in list)
                        {
                            if (obj == null)
                                continue;

                            sb.Append("\t<ROW");
                            foreach (PropertyInfo info in infos)
                            {
                                try
                                {
                                    object val = typeof(T).InvokeMember(info.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance, null, obj, null);

                                    // 若是DateTime对象，而且未设置值，则忽略此数据 JJY added on 2009.9.3
                                    // 根据时间范围来判断时间是否正常，如果不在范围内，则忽略此时间  Tan  2010-06-24
                                    if (info.ToString().StartsWith(typeof(DateTime).FullName + " "))
                                    {
                                        DateTime thisTime = Convert.ToDateTime(val);
                                        DateTime MaxDate = new DateTime(2078, 1, 1);
                                        DateTime MinDate = new DateTime(1900, 1, 1);
                                        if (DateTime.MinValue.Equals(thisTime) || thisTime >= MaxDate || thisTime <= MinDate)
                                            continue;
                                    }
                                    string propertyName = string.Empty;
                                    if (attributeType.BaseType == typeof(BuildXmlAttributeBase))
                                    {
                                        BuildXmlAttributeBase[] objAttribute = (BuildXmlAttributeBase[])(info.GetCustomAttributes(attributeType, false));
                                        if (objAttribute != null && objAttribute.Length == 1)
                                        {
                                            propertyName = objAttribute[0].XmlPropertyName;
                                            if (!string.IsNullOrEmpty(propertyName))
                                                sb.AppendFormat(" {0}=\"{1}\"", propertyName, FixXml(ConvertToString(val)));
                                        }
                                    }
                                }
                                catch { }
                            }
                            sb.Append(" />\r\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Netbox.Pass.PassHelper.SysTrack.Tracker.ErrorTrack(ex, "根据描述创建实体集合的Xml错误，实体类型：" + typeof(T).ToString());
                throw ex;
            }

            sb.Append("</ROOT>\r\n");
            return sb.ToString();
        }

        /// <summary>
        /// 创建实体对象的Xml片段字符串(不闭合的开始)。
        /// </summary>
        /// <typeparam name="T">实体的类型。</typeparam>
        /// <param name="list">实体集合。</param>
        /// <returns>实体对象的Xml片段字符串。</returns>
        public static string BuildEntityXmlBegin<T>(T entity) where T : class
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                if (entity != null)
                {
                    PropertyInfo[] infos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance);
                    if (infos != null && infos.Length > 0)
                    {
                        sb.Append(string.Format("<{0}", typeof(T).Name));
                        foreach (PropertyInfo info in infos)
                        {
                            try
                            {
                                object val = typeof(T).InvokeMember(info.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance, null, entity, null);

                                // 若是DateTime对象，而且未设置值，则忽略此数据 JJY added on 2009.9.3
                                //根据时间范围来判断时间是否正常，如果不在范围内，则忽略此时间  Tan  2010-06-24
                                if (info.ToString().StartsWith(typeof(DateTime).FullName + " "))
                                {
                                    DateTime thisTime = Convert.ToDateTime(val);
                                    DateTime MaxDate = new DateTime(2078, 1, 1);
                                    DateTime MinDate = new DateTime(1900, 1, 1);
                                    if (DateTime.MinValue.Equals(thisTime) || thisTime >= MaxDate || thisTime <= MinDate)
                                        continue;
                                }

                                sb.AppendFormat(" {0}=\"{1}\"", info.Name, FixXml(ConvertToString(val)));
                            }
                            catch { }
                        }
                        sb.Append(" >\r\n");
                    }
                }
            }
            catch { }

            return sb.ToString();
        }

        /// <summary>
        /// 创建实体对象的Xml片段字符串(不闭合的结束)。
        /// </summary>
        /// <typeparam name="T">实体的类型。</typeparam>
        /// <param name="list">实体集合。</param>
        /// <returns>实体对象的Xml片段字符串。</returns>
        public static string BuildEntityXmlEnd<T>(T entity) where T : class
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                if (entity != null)
                {
                    sb.Append(string.Format("</{0}>", typeof(T).Name));
                }
            }
            catch { }

            return sb.ToString();
        }


        /// <summary>
        /// 创建值集合的Xml文档字符串。
        /// </summary>
        /// <typeparam name="T">值的类型。</typeparam>
        /// <param name="name">值的名称。</param>
        /// <param name="list">值集合。</param>
        /// <returns>Xml字符串。</returns>
        public static string BuildXml<T>(string name, IList<T> list)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("<?xml version=\"1.0\" encoding=\"gb2312\" ?>\r\n");
            sb.Append("<ROOT>\r\n");

            try
            {
                if (list != null && list.Count > 0)
                {
                    bool isValueType = typeof(T).IsValueType || typeof(T) == typeof(string);

                    foreach (T val in list)
                    {
                        if (isValueType)
                            sb.AppendFormat("\t<ROW {0}=\"{1}\" />\r\n", name, FixXml(ConvertToString(val)));
                        else
                            sb.AppendFormat("\t<ROW {0}=\"{1}\" />\r\n", name, FixXml(ConvertToString(PropertyHelper.GetPropertyValue(name, val))));
                    }
                }
            }
            catch { }

            sb.Append("</ROOT>\r\n");
            return sb.ToString();
        }

        public static string BuildXml<T>(string name, IList<T> list, bool includeHeader)
        {
            StringBuilder sb = new StringBuilder();
            if (includeHeader)
                sb.AppendLine("<?xml version=\"1.0\" encoding=\"" + System.Text.Encoding.Default.BodyName + "\" ?>");

            sb.AppendLine(BuildXml<T>(name, list));
            return sb.ToString();
        }

        /// <summary>
        /// 创建值集合的Xml文档字符串。
        /// </summary>
        /// <typeparam name="T">值的类型。</typeparam>
        /// <param name="name">值的名称，多个用","分隔。</param>
        /// <param name="list">值集合。</param>
        /// <returns>Xml字符串。</returns>
        public static string BuilderXml<T>(List<string> name, IList<T> list)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("<?xml version=\"1.0\" encoding=\"gb2312\" ?>\r\n");
            sb.Append("<ROOT>\r\n");
            //List<string> nameList = names;
            try
            {
                if (list != null && list.Count > 0)
                {
                    bool isValueType = typeof(T).IsValueType || typeof(T) == typeof(string);

                    foreach (T val in list)
                    {
                        sb.Append("\t<ROW");
                        foreach (string str in name)
                        {
                            if (isValueType)
                                sb.AppendFormat(" {0}=\"{1}\"", str, FixXml(ConvertToString(val)));
                            else
                                sb.AppendFormat(" {0}=\"{1}\"", str, FixXml(ConvertToString(PropertyHelper.GetPropertyValue(str, val))));
                        }
                        sb.Append(" />\r\n");
                    }
                }
            }
            catch { }

            sb.Append("</ROOT>\r\n");
            return sb.ToString();
        }

        /// <summary>
        /// 创建值集合的Xml文档字符串。
        /// </summary>
        /// <returns></returns>
        public static string BuildXml(string rowContent)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("<?xml version=\"1.0\" encoding=\"gb2312\" ?>\r\n");
            sb.Append("<ROOT>\r\n");
            sb.Append(rowContent);
            sb.Append("</ROOT>\r\n");
            return sb.ToString();
        }

        private static string ConvertToString(object val)
        {
            return ObjectHelper.ConvertToString(val);
        }

        public static string FixXml(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                StringBuilder info = new StringBuilder();
                foreach (char cc in s)
                {
                    int ss = (int)cc;
                    if (((ss >= 0) && (ss <= 8)) || ((ss >= 11) && (ss <= 12)) || ((ss >= 14) && (ss <= 32)))
                        info.AppendFormat(" ", ss);//&#x{0:X};
                    else
                        info.Append(cc);
                }
                return info.ToString().Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("\"", "&quot;").Replace("'", "&apos;");
            }
            else
                return "";
        }

        //public static string FixXml(string s)
        //{
        //    if (!string.IsNullOrEmpty(s))
        //        return s.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("\"", "&quot;").Replace("'", "&apos;");
        //    else
        //        return "";
        //}

        /// <summary>
        /// 比较两个指定的字符串是否相同[不区分大小写]
        /// </summary>
        /// <param name="stringA">stringA</param>
        /// <param name="stringB">stringB</param>
        /// <returns>true:相同 false:不相同</returns>
        public static bool Compare(string stringA, string stringB)
        {
            return (string.Compare(stringA, stringB, true, System.Globalization.CultureInfo.CurrentCulture) == 0);
        }

        /// <summary>
        /// String转StreamReader
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static StreamReader StringToStreamReader(string str)
        {
            return StringToStreamReader(str, "gb2312");
        }

        /// <summary>
        /// String转StreamReader
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="codeName">编码名称[如:gb2312]</param>
        /// <returns></returns>
        public static StreamReader StringToStreamReader(string str, string codeName)
        {
            MemoryStream ms = new MemoryStream(System.Text.Encoding.GetEncoding(codeName).GetBytes(str), false);
            StreamReader reader = new StreamReader(ms, System.Text.Encoding.GetEncoding(codeName));
            return reader;
        }

        /// <summary>
        /// 是否为错误的字符串或含有乱码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsErrorString(string str)
        {
            bool rst = false;
            if (IsGarbageStr(str) || IsSpecialStr(str))
                rst = true;
            return rst;
        }

        /// <summary>
        /// 是否为垃圾数据(长度少于3位、最后一位为？号或含有特殊符号)
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true:是 false:不是</returns>
        public static bool IsGarbageStr(string str)
        {
            bool rst = false;
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length < 3 || str.EndsWith("?") || str.EndsWith("？"))
                    rst = true;
            }
            return rst;
        }

        /// <summary>
        /// 是否含有特殊符号(ASCII码十进制0~31之间为特殊符号)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsSpecialStr(string str)
        {
            bool rst = false;
            int intAsciiCode = 0;
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            for (int i = 0; i < str.Length; i++)
            {
                intAsciiCode = (int)asciiEncoding.GetBytes(str)[i];
                if (intAsciiCode >= 0 && intAsciiCode <= 31)    // ASCII码十进制0~31之间为特殊符号
                {
                    rst = true;
                    break;
                }
            }
            return rst;
        }

        /// <summary>
        /// 将XMLString转换成为实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static List<T> XMLToTList<T>(string xmlString) where T : new()
        {
            List<T> list = new List<T>();

            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xmlString);//加载xml内容
            }
            catch (Exception ex)
            { }

            if (xmlDoc != null)
            { //当xml加载成功时
                XmlNodeList xndRowList = xmlDoc.SelectNodes("//ROW");

                foreach (XmlNode xndRow in xndRowList)
                {
                    T t = new T();
                    PropertyInfo[] propertyInfoList = t.GetType().GetProperties();
                    bool hasValue = false;

                    foreach (PropertyInfo pi in propertyInfoList)
                    {
                        XmlAttribute xattrValue = xndRow.Attributes[pi.Name];
                        if (xattrValue == null)
                            continue;
                        try
                        {
                            object piValue = ObjectValueParser.ChangeType(xattrValue.Value, pi.PropertyType);
                            pi.SetValue(t, piValue, null);//设置对象的值
                            hasValue = true;
                        }
                        catch (Exception ex)
                        { }
                    }

                    if (hasValue)//对象是否为有值对象
                        list.Add(t);
                }
            }

            return list;
        }

        /// <summary>
        /// 是否为重复数据(字符串之间相邻字符连续重复10次以上,则认为是重复数据)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsOverlapStr(string str)
        {
            int statCnt = 1;        // 垃圾字符统计次数
            int whileCnt = 1;       // 循环统计次数
            char PrevChar = '0';    // 上一个字符
            bool rst = false;
            foreach (char c in str)
            {
                if (whileCnt > 1 && PrevChar == c)  // 从第2个字符开始比较,如连续重复,统计数则加1
                    statCnt++;
                else
                    statCnt = 1;
                PrevChar = c;
                whileCnt++;
                if (statCnt > 10)    // 如果重复达10次以上
                    break;
            }

            if (statCnt > 10)
                rst = true;
            return rst;
        }

        /// <summary>
        /// 清空无效字符[XML文件中的合法Unicode字符范围（十六进制）Char ::= #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF]]
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string CleanInvalidXmlChars(string str)
        {
            string re = @"[^\x0D\x20-\xD7FF\xE000-\xFFFD\x10000-x10FFFF\u4e00-\u9fa5]";
            return System.Text.RegularExpressions.Regex.Replace(str, re, "");
        }
    }
}
