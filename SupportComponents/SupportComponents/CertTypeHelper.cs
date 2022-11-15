using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents
{
    /// <summary>
    /// 身份类型名称辅助类
    /// </summary>
    public class CertTypeHelper
    {
        private static Dictionary<int, string> CertTypeDic = new Dictionary<int, string>() { 
        { 111, "身份证" },{ 113, "户口簿" },{ 114, "军官证" },
        { 123, "警官证" },{ 233, "士兵证" },{ 335, "驾驶证" },
        { 414, "护照" },  { 511, "台胞证" },{ 516, "回乡证" },
        { 990, "其他证件" }};

        private static Dictionary<int, string> TSPCertTypeDic = new Dictionary<int, string>() { 
        { 111, "11" },{ 114, "90" },
        { 233, "92" },{ 335, "41" },
        { 414, "93" },{ 990, "99" }};

        public static string GetCertTypeName(int certType)
        {
            if (CertTypeDic.ContainsKey(certType))
                return CertTypeDic[certType];
            else
                return "其他证件";
        }

        /// <summary>
        /// 访客信息上传
        /// </summary>
        /// <param name="certType"></param>
        /// <returns></returns>
        public static string GetTSPCertTypeName(int certType)
        {
            if (TSPCertTypeDic.ContainsKey(certType))
                return TSPCertTypeDic[certType];
            else
                return "99";
        }
    }
}
