using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents
{
    /// <summary>
    /// 身份证件处理辅助类
    /// </summary>
    public sealed class CertificateHelper
    {
        /// <summary>
        /// 其它证件号码
        /// </summary>
        public const string OtherID = "999999";

        /// <summary>
        /// 获取真实身份ID
        /// </summary>
        /// <param name="CertType">证件类型</param>
        /// <param name="CertID">证件号码</param>
        /// <returns></returns>
        public static long GetHyperID(string CertType, string CertID)
        {
            CertType = string.IsNullOrEmpty(CertType) ? string.Empty : CertType.Trim();
            CertID = string.IsNullOrEmpty(CertID) ? string.Empty : CertID.Trim();

            if (CertType.Trim() == "111" && CertID.Trim().Length == 15)
                CertID = CertificateHelper.PersonID15To18(CertID.ToUpper());

            if (CertType.Trim() == "111")
                CertID = CertID.ToUpper();

            return (long)CRC64Helper.StringCRC(CertType + CertID.ToUpper());
        }

        /// <summary>
        /// 身份证号码15位转18位 
        /// </summary>
        /// <param name="CardID">证件号码</param>
        /// <returns></returns>
        public static string PersonID15To18(string CardID)
        {
            if (CardID.Trim().Length != 15)
                return null;

            CardID = CardID.Trim();

            char[] perIDSrc = CardID.ToCharArray();
            int iS = 0;

            int[] iW = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 }; // 加权因子常数 
            char[] LastCode = "10X98765432".ToCharArray();	// 校验码常数 
            //新身份证号 
            //char[] perIDNew = new char[19]; 
            char[] perIDNew = new char[18];

            for (int i = 0; i < 6; i++)
            {
                perIDNew[i] = perIDSrc[i];
            }

            //填在第6位及第7位上填上‘1’，‘9’两个数字 
            perIDNew[6] = '1';
            perIDNew[7] = '9';

            for (int i = 8; i < 17; i++)
            {
                perIDNew[i] = perIDSrc[i - 2];
            }

            //进行加权求和 
            for (int i = 0; i < 17; i++)
            {
                iS += (perIDNew[i] - '0') * iW[i];
                #region 注释
                //  --------------------------------------------------------- 
                // | 对于perIDNew[i]-'0'解释一下：                           |
                // | perIDNew[i]->ASCII码，取得它的值实际是十进制数；        |
                // | '0' ->ASCII码，同上；                                   |
                // | perIDNew[i]-'0' -> 得到具体的十进制数值；               |
                // | 对于这里面的为什么会进行转换，具体去看C++PRIMER，呵呵。 |
                //  ---------------------------------------------------------
                #endregion
            }

            //取模运算，得到模值 
            int iY = iS % 11;
            //从LastCode中取得以模为索引号的值，加到身份证的最后一位，即为新身份证号。 
            perIDNew[17] = LastCode[iY];
            //加上结束符 for c++
            //perIDNew[18] = '\0'; 

            StringBuilder sb = new StringBuilder();
            sb.Append(perIDNew);
            return sb.ToString();
        }

        /// <summary>
        /// 身份证号码18位转15位 
        /// </summary>
        /// <param name="CardID">证件号码</param>
        /// <returns></returns>
        public static string PersonID18To15(string CardID)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < CardID.Length; i++)
            {
                if (i != 6 && i != 7 && i != 17)
                {
                    sb.Append(CardID[i]);
                }
            }
            return sb.ToString();
            //sb.Append();
        }

        /// <summary>
        /// 对身份证最后一位验证码进行校验
        /// </summary>
        /// <param name="perIDSrc"></param>
        /// <returns></returns>
        public static bool CheckPersonID18(char[] perIDSrc)
        {
            if (perIDSrc.Length != 18)
                return false;
            int iS = 0;
            int[] iW = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            char[] LastCode = "10X98765432".ToCharArray();

            for (int i = 0; i < 17; i++)
            {
                iS += (int)(perIDSrc[i] - '0') * iW[i];
            }

            int iY = iS % 11;
            if (perIDSrc[17] != LastCode[iY])
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取身份证上的出生日期[格式:YYYYMMDD]
        /// </summary>
        /// <param name="CertID">18位的证件号码</param>
        /// <returns>出生日期</returns>
        public static string GetBirthday(string CertID)
        {
            string Birthday = string.Empty;
            try
            {
                int yyyy = Convert.ToInt32(CertID.Substring(6, 4));
                int mm = Convert.ToInt32(CertID.Substring(10, 2));
                int dd = Convert.ToInt32(CertID.Substring(12, 2));
                if ((yyyy > 1700 && yyyy < 2099) && (mm > 0 && mm < 13) && (dd > 0 && dd < 32))
                    Birthday = CertID.Substring(6, 4) + CertID.Substring(10, 2) + CertID.Substring(12, 2);
            }
            catch
            {
                Birthday = string.Empty;
            }
            return Birthday;
        }

        /// <summary>
        /// 获取证件类型
        /// </summary>
        /// <param name="CertType">证件类型</param>
        /// <returns></returns>
        public static string GetCertType(string CertType)
        {
            string type = "990";
            if (!string.IsNullOrEmpty(CertType) && CertType.Length == 3 && RegexHelper.IsNumber(CertType))
                type = CertType;
            return type;
        }

        /// <summary>
        /// 获取证件号码
        /// </summary>
        /// <param name="CertType">证件类型</param>
        /// <param name="CertID">证件号码</param>
        /// <returns>处理过的证件号码</returns>
        public static string GetCertID(string CertType, string CertID)
        {
            #region 个别字段处理
            // 因为自动开户所产生的顾客记录都没有证件类型和证件号，所以默认为990和999999，以免增加真实身份失败
            // 证件类型长度由2位升到3位，如果为2位长度的默认为990其他证件
            if (string.IsNullOrEmpty(CertType) || CertType.Length != 3)
                CertType = "990";
            if (string.IsNullOrEmpty(CertID) || CertType.Length != 3)
                CertID = "999999";

            if (CertType == "111" && CertID.Length == 15)
                CertID = PersonID15To18(CertID);	// 15位转18位[针对身份证]
            if (CertType == "111")
                CertID = CertID.ToUpper();
            return CertID;
            #endregion
        }

        /// <summary>
        /// 证件类型
        /// </summary>
        public enum enumCertType
        {
            /// <summary>
            /// 身份证
            /// </summary>
            Identification = 111,
            /// <summary>
            /// 驾驶证
            /// </summary>
            DrivingLicense = 335,
            /// <summary>
            /// 军官证
            /// </summary>
            MilitaryOfficer = 114,
            /// <summary>
            /// 警官证
            /// </summary>
            PoliceOffcer = 123,
            /// <summary>
            /// 士兵证
            /// </summary>
            Soldier = 233,
            /// <summary>
            /// 户口簿
            /// </summary>
            ResidenceBooklet = 113,
            /// <summary>
            /// 护照
            /// </summary>
            Passport = 414,
            /// <summary>
            /// 台胞证
            /// </summary>
            TaiwaneseCompatriot = 511,
            /// <summary>
            /// 回乡证
            /// </summary>
            HomeVisitPermit = 516,
            /// <summary>
            /// 其他证件
            /// </summary>
            Other = 990
        }

        static string[,] CertList = new string[9, 3]
              {
                   { "11", "111", "身份证" },
                   { "15", "335", "驾驶证" },
                   { "90", "114", "军官证" },
                   { "91", "123", "警官证" },
                   { "92", "233", "士兵证" },
                   { "93", "113", "户口簿" },
                   { "94", "414", "护照" },
                   { "95", "511", "台胞证" },
                   { "96", "516", "回乡证" }
                   // { "99", "其他证件" }
              };

        public static string ToOldCert(string certType)
        {
            for (int i = 0; i < CertList.Length / 3; i++)
            {
                if (CertList[i, 1] == certType)
                    return CertList[i, 0];
            }
            return "99";
        }

        public static string ToNewCert(string certType)
        {
            for (int i = 0; i < CertList.Length / 3; i++)
            {
                if (CertList[i, 0] == certType)
                    return CertList[i, 1];
            }
            return "990";
        }
    }
}