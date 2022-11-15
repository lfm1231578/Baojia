using System;

namespace HS.SupportComponents
{
    /// <summary>
    /// 值验证辅助类
    /// </summary>
    public sealed class ConvertValueHelper
    {
        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        public static byte ConvertByteValue(string value)
        {
            return ConvertByteValue(value, 0);
        }

        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        /// <param name="defaultValue">默认值</param>
        public static byte ConvertByteValue(string value, byte defaultValue)
        {
            if (!string.IsNullOrEmpty(value))
            {
                byte v;
                if (byte.TryParse(value, out v))
                    return v; // 如果转换正确，返回真实值
            }
            return defaultValue; // 否则返回默认值
        }

        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        public static short ConvertShortValue(string value)
        {
            return ConvertShortValue(value, 0);
        }

        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        /// <param name="defaultValue">默认值</param>
        public static short ConvertShortValue(string value, short defaultValue)
        {
            if (!string.IsNullOrEmpty(value))
            {
                short v;
                if (Int16.TryParse(value, out v))
                    return v; // 如果转换正确，返回真实值
            }
            return defaultValue; // 否则返回默认值
        }

        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        public static int ConvertIntValue(string value)
        {
            return ConvertIntValue(value, 0);
        }
        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        /// <param name="defaultValue">默认值</param>
        public static int ConvertIntValue(string value, int defaultValue)
        {
            if (!string.IsNullOrEmpty(value))
            {
                int v;
                if (Int32.TryParse(value, out v))
                    return v; // 如果转换正确，返回真实值
            }
            return defaultValue; // 否则返回默认值
        }

        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        public static decimal ConvertDecimalValue(string value)
        {
            return ConvertDecimalValue(value, 0);
        }

        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        /// <param name="defaultValue">默认值</param>
        public static decimal ConvertDecimalValue(string value, decimal defaultValue)
        {
            if (!string.IsNullOrEmpty(value))
            {
                decimal v;

                if (Decimal.TryParse(value, out v))
                    return v; // 如果转换正确，返回真实值
            }
            return defaultValue; // 否则返回默认值
        }

        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        public static long ConvertLongValue(string value)
        {
            return ConvertLongValue(value, 0);
        }

        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        /// <param name="defaultValue">默认值</param>
        public static long ConvertLongValue(string value, long defaultValue)
        {
            if (!string.IsNullOrEmpty(value))
            {
                long v;
                if (Int64.TryParse(value, out v))
                    return v; // 如果转换正确，返回真实值
            }
            return defaultValue; // 否则返回默认值
        }




        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        /// <param name="defaultValue">默认值</param>
        public static void ConvertDoubleValue(string value,out long d1, out string defaultValue)
        {
            d1 = 0;
            defaultValue = "";
            if (!string.IsNullOrEmpty(value))
            {
                long v;
                if (Int64.TryParse(value, out v))
                    d1 = v;
            }
            else
            {
                defaultValue = value;
            }
        }
        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        /// <param name="dateTime"></param>
        public static bool ConvertDateTimeValue(string value, out DateTime dateTime)
        {
            return ConvertDateTimeValue(value, DateTime.Now, out dateTime);
        }

        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="dateTime"></param>
        public static bool ConvertDateTimeValue(string value, DateTime defaultValue, out DateTime dateTime)
        {
            var s = new DateTime(1900, 1, 1, 0, 0, 0);
            var e = new DateTime(2078, 12, 31, 0, 0, 0);

            if (!string.IsNullOrEmpty(value))
            {
                DateTime v;
                if (DateTime.TryParse(value, out v) && v > s && v < e)
                {
                    dateTime = v;
                    return true; // 如果转换正确，返回真实值
                }
            }
            dateTime = defaultValue;
            return false; // 否则返回默认值
        }


        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        /// <param name="dateTime"></param>
        public static DateTime ConvertDateTimeValueNew(string value)
        {
            return ConvertDateTimeValueNew(value, DateTime.Now);
        }

        /// <summary>
        /// 检查验证值
        /// </summary>
        /// <param name="value">验证值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="dateTime"></param>
        public static DateTime ConvertDateTimeValueNew(string value, DateTime defaultValue)
        {
            var s = new DateTime(1900, 1, 1, 0, 0, 0);
            var e = new DateTime(2078, 12, 31, 0, 0, 0);

            if (!string.IsNullOrEmpty(value))
            {
                DateTime v;
                if (DateTime.TryParse(value, out v) && v > s && v < e)
                {

                    return v; // 如果转换正确，返回真实值
                }
            }

            return defaultValue; // 否则返回默认值
        }
    }
}