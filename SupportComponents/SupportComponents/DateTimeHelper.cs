using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HS.SupportComponents
{
    public sealed class DateTimeHelper
    {
        /// <summary>
        /// 返回DateTime的格林尼治标准时间(Greenwich Mean Time)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static uint ToGMT(DateTime dateTime)
        {
            // GMT是以1970年1月1日0时0分为基准的秒数。中国的时区为+8。
            TimeSpan ts = dateTime - new DateTime(1970, 1, 1, 8, 0, 0);
            return (uint)ts.TotalSeconds;
        }

        public static DateTime GMTToDateTime(Int32 GMT)
        {
            DateTime dt = new DateTime(1970, 1, 1, 8, 0, 0);
            dt.AddSeconds(GMT);
            return dt;
        }

        /// <summary>
        /// 将指定的日期类型转换成32位有字符整数[格式:yyyyMMdd]
        /// </summary>
        /// <param name="dt">转换的日期</param>
        /// <returns>转换后的32位的有字符整数</returns>
        public static int ToInt32(DateTime dt)
        {
            int Year = dt.Year;
            int Month = dt.Month;
            int Day = dt.Day;
            return ((Year * 10000) + (Month * 100) + Day);
        }

        /// <summary>
        /// 将指定的32位有字符整数转换成日期类型[格式:yyyy-MM-dd]
        /// </summary>
        /// <param name="dt">转换的32位有字符整数</param>
        /// <returns>转换后的日期</returns>
        public static DateTime ToDateTime(int dt)
        {
            if (dt.ToString().Length >= 8)
            {
                int Year = Convert.ToInt32(dt / 10000);
                int Month = Convert.ToInt32((dt - Year * 10000) / 100);
                int Day = Convert.ToInt32(dt - (Year * 10000) - (Month * 100));
                return (new DateTime(Year, Month, Day));
            }
            return DateTime.MinValue;
        }

        /// <summary>
        /// 转换为指定的日期类型[格式:yyyy-MM-dd HH:mm:ss]
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToString(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 由公安标准时间转换成正常时间
        /// </summary>
        /// <param name="DateTime"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(long DateTime)
        {
            return ToDateTime(DateTime.ToString());
        }

        /// <summary>
        /// 由公安标准时间转换成正常时间
        /// </summary>
        /// <param name="DateTime"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string strDateTime)
        {
            if (strDateTime.Trim().Length == 0 || !IsDatetimeString(strDateTime))
                return DateTime.Now;

            int year = int.Parse(strDateTime.Substring(0, 4));
            int month = int.Parse(strDateTime.Substring(4, 2));
            int day = int.Parse(strDateTime.Substring(6, 2));
            int hour = int.Parse(strDateTime.Substring(8, 2));
            int minute = int.Parse(strDateTime.Substring(10, 2));
            int second = int.Parse(strDateTime.Substring(12, 2));
            return new DateTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// 检查日期时间字符串是否合法
        /// </summary>
        /// <param name="strDatetime">日期时间字符串</param>
        /// <returns>true表示合法，false表示不合法</returns>
        public static bool IsDatetimeString(string strDatetime)
        {
            if (strDatetime.Length != 14)
                return false;

            string strDate = strDatetime.Substring(0, 8);
            string strTime = strDatetime.Substring(8, 6);

            return (CheckDateString(strDate) && IsTimeString(strTime));
        }

        /// <summary>
        /// 检查日期字符串是否合法
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static bool CheckDateString(string strDate)
        {
            if (strDate.Length != 8)
                return false;

            foreach (char ch in strDate)
            {
                if (!Char.IsDigit(ch))
                    return false;
            }

            int year = int.Parse(strDate.Substring(0, 4));
            int month = int.Parse(strDate.Substring(4, 2));
            int day = int.Parse(strDate.Substring(6, 2));

            if (year < 1900 || year > 2078 || month > 12 || day > 31)
                return false;

            return true;
        }

        /// <summary>
        /// 检查时间字符串是否合法
        /// </summary>
        /// <param name="strTime"></param>
        /// <returns></returns>
        public static bool IsTimeString(string strTime)
        {
            if (strTime.Length != 6)
                return false;

            foreach (char ch in strTime)
            {
                if (!Char.IsDigit(ch))
                    return false;
            }

            int hour = int.Parse(strTime.Substring(0, 2));
            int min = int.Parse(strTime.Substring(2, 2));
            int sec = int.Parse(strTime.Substring(4, 2));

            if (hour > 24 || min > 60 || sec > 60)
                return false;

            return true;
        }

        /// <summary>
        /// 获取绝对秒数[8位十六进制](默认为当前时间与1970年1月1日0:0:0之间的秒数)
        /// </summary>
        /// <returns></returns>
        public static string GetAbSecondToHex()
        {
            return GetAbSecondToHex(DateTime.Parse("1970-01-01 00:00:00"), DateTime.Now);
        }

        /// <summary>
        /// 获取绝对秒数[8位十六进制]
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static string GetAbSecondToHex(DateTime beginTime, DateTime endTime)
        {
            int digit = 8;  // 8位十六进制数字
            int fillcnt = 0;    // 补0的数量

            string value = GetAbSecond(beginTime, endTime);
            value = NumberHelper.Ten2Hex(value);

            if (value.Length < 8)
            {
                fillcnt = digit - value.Length;
                for (int i = 0; i < fillcnt; i++)
                    value = "0" + value;
            }
            return value.ToUpper();
        }

        /// <summary>
        /// 获取绝对秒数[十进制](默认为当前时间与1970年1月1日0:0:0之间的秒数)
        /// </summary>
        /// <returns></returns>
        public static string GetAbSecond()
        {
            return GetAbSecondToHex(DateTime.Parse("1970-01-01 00:00:00"), DateTime.Now);
        }

        /// <summary>
        /// 将YY-MM-DD AM/PM HH:mm:ss格式转换为正常时间格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetTimeHaveStr(string time)
        {
            string Times = "";
            if (time != "")
            {

                string[] str = Regex.Split(time, " ", RegexOptions.IgnoreCase);
                string i = str[0];
                string j = str[1];
                string k = str[2];
                Times = str[1] == "AM" ? str[0] + " " + str[2] : str[0] + " " + Convert.ToDateTime(str[2]).AddHours(12).ToString();
            }
            return Times;

        }


        /// <summary>
        /// 获取绝对秒数[十进制]
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static string GetAbSecond(DateTime beginTime, DateTime endTime)
        {
            TimeSpan span = new TimeSpan(endTime.Ticks);
            TimeSpan ts = new TimeSpan(beginTime.Ticks);
            string value = Convert.ToInt32(span.Subtract(ts).TotalSeconds).ToString();
            return value;
        }

        /// <summary>
        /// 将8位日期型整型数据转换为日期字符串数据
        /// </summary>
        /// <param name="date">整型日期</param>
        /// <param name="chnType">是否以中文年月日输出</param>
        /// <returns></returns>
        public static string FormatDate(int date, bool chnType)
        {
            string dateStr = date.ToString();

            if (date <= 0 || dateStr.Length != 8)
                return dateStr;

            if (chnType)
                return dateStr.Substring(0, 4) + "年" + dateStr.Substring(4, 2) + "月" + dateStr.Substring(6) + "日";
            return dateStr.Substring(0, 4) + "-" + dateStr.Substring(4, 2) + "-" + dateStr.Substring(6);
        }

        public static string FormatDate(int date)
        {
            return FormatDate(date, false);
        }

        public static string AdDeTime(int times)
        {
            return (DateTime.Now).AddMinutes(times).ToString();
        }

        /// <summary>
        /// 判断字符串是否是yy-mm-dd字符串
        /// </summary>
        /// <param name="str">待判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsDateString(string str)
        {
            return Regex.IsMatch(str, @"(\d{4})-(\d{1,2})-(\d{1,2})");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }

        /// <summary>
        /// 返回标准日期格式string
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回指定日期格式
        /// </summary>
        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
                return replacestr;

            if (datetimestr.Equals(""))
                return replacestr;

            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;
        }


        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回相对于当前时间的相对天数
        /// </summary>
        public static string GetDateTime(int relativeday)
        {
            return DateTime.Now.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        /// <summary>
        /// 返回标准时间 
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
                return fDateTime;
            DateTime time = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            if (DateTime.TryParse(fDateTime, out time))
                return time.ToString(formatStr);
            else
                return "N/A";
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH:mm:ss
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd
        /// </sumary>
        public static string GetStandardDate(string fDate)
        {
            return GetStandardDateTime(fDate, "yyyy-MM-dd");
        }

        /// <summary>
        /// 根据阿拉伯数字返回月份的名称(可更改为某种语言)
        /// </summary>	
        public static string[] Monthes
        {
            get
            {
                return new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            }
        }
        /// <summary>
        /// 通过月份.年份获取本月有几天
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int GetAllDayByMonth(int year, int month)
        {
            year = DateTime.Parse(year + "-" + month + "-" + "01").Year;
            month = DateTime.Parse(year + "-" + month + "-" + "01").Month;
            int days = 0;
            //得到当前月的天数
            switch (month)
            {
                case 1:
                    days = 31;
                    break;
                case 2:
                    if (DateTime.IsLeapYear(year))
                    {
                        //闰年二月为29天
                        days = 29;
                    }
                    else
                    {
                        //不是闰年，二月为28天
                        days = 28;
                    }
                    break;
                case 3:
                    days = 31;
                    break;
                case 4:
                    days = 30;
                    break;
                case 5:
                    days = 31;
                    break;
                case 6:
                    days = 30;
                    break;
                case 7:
                    days = 31;
                    break;
                case 8:
                    days = 31;
                    break;
                case 9:
                    days = 30;
                    break;
                case 10:
                    days = 31;
                    break;
                case 11:
                    days = 30;
                    break;
                case 12:
                    days = 31;
                    break;
            }
            return days;
        }
        /// <summary>
        /// 取指定日期是一年中的第几周
        /// </summary>
        /// <param name="dtime">给定的日期</param>
        /// <returns>数字 一年中的第几周</returns>
        public static int WeekOfYear(DateTime dtime)
        {
            try
            {
                //确定此时间在一年中的位置
                int dayOfYear = dtime.DayOfYear;
                //当年第一天
                DateTime tempDate = new DateTime(dtime.Year, 1, 1);
                //确定当年第一天
                int tempDayOfWeek = (int)tempDate.DayOfWeek;
                tempDayOfWeek = tempDayOfWeek == 0 ? 7 : tempDayOfWeek;
                ////确定星期几
                int index = (int)dtime.DayOfWeek;
                index = index == 0 ? 7 : index;

                //当前周的范围
                DateTime retStartDay = dtime.AddDays(-(index - 1));
                DateTime retEndDay = dtime.AddDays(6 - index);

                //确定当前是第几周
                int weekIndex = (int)Math.Ceiling(((double)dayOfYear + tempDayOfWeek - 1) / 7);

                if (retStartDay.Year < retEndDay.Year)
                {
                    weekIndex = 1;
                }

                return weekIndex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>;
        /// 求某年有多少周
        /// </summary>;
        /// <param name="dtime">;</param>;
        /// <returns>;</returns>;
        public static int GetWeekOfYear(DateTime dtime)
        {
            int countDay = DateTime.Parse(dtime.Year + "-12-31").DayOfYear;
            int countWeek = countDay / 7;
            return countWeek;
        }


        //根据年月日获得星期几
        public static string CaculateWeekDay(int month, int day)
        {
            string weekstr = string.Empty;
            int year = DateTime.Now.Year;
            //把一月和二月看成是上一年的十三月和十四月
            if (month == 1) { month = 13; year--; }
            if (month == 2) { month = 14; year--; }
            int week = (day + 2 * month + 3 * (month + 1) / 5 + year + year / 4 - year / 100 + year / 400) % 7;
            switch (week)
            {
                case 0: weekstr = "1"; break;
                case 1: weekstr = "2"; break;
                case 2: weekstr = "3"; break;
                case 3: weekstr = "4"; break;
                case 4: weekstr = "5"; break;
                case 5: weekstr = "6"; break;
                case 6: weekstr = "7"; break;
            }
            return weekstr;
        }

        /// <summary>;
        /// 根据2个时间段获得相应的周数
        /// </summary>;
        /// <param name="beginDate">;</param>;
        /// <param name="endDate">;</param>;
        /// <returns>;</returns>;
        public static int WeekOfDate(DateTime beginDate, DateTime endDate)
        {
            TimeSpan ts1 = new TimeSpan(beginDate.Ticks);
            TimeSpan ts2 = new TimeSpan(endDate.Ticks);
            TimeSpan ts = ts2.Subtract(ts1).Duration();
            //int weeks = ts.Days / 7;
            double weeks = (double)ts.Days / (double)7;
           int result = (int)(Math.Ceiling(weeks));
           return result;
        }
            /// <summary>;
        /// 根据根据开始时间确定周的范围
        /// </summary>;
        /// <param name="beginDate">;</param>;
        /// <param name="endDate">;</param>;
        /// <returns>;</returns>;
        public static string RangWeekOfDate(DateTime beginDate, DateTime endDate,out DateTime retStartDay, out DateTime retEndDay)
        {
            ////确定星期几
            int startindex = (int)beginDate.DayOfWeek;
           // startindex = startindex == 0 ;//? 7 : startindex;
            //当前周的范围
             retStartDay = beginDate.AddDays(-(startindex - 1));
             retEndDay = beginDate.AddDays(7 - startindex);

            //确定此时间在一年中的位置
            int dayOfYear = beginDate.DayOfYear;
            //当年第一天
            DateTime tempDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day);
            //最后一天
            DateTime tempendDate = new DateTime(endDate.Year, endDate.Month, endDate.Day);
            int tempDayOfWeek = (int)tempDate.DayOfWeek;
            tempDayOfWeek = tempDayOfWeek == 0 ? 7 : tempDayOfWeek;

            //确定当前是第几周
            int weekIndex = (int)Math.Ceiling(((double)dayOfYear + tempDayOfWeek - 1) / 7);
            return retStartDay.ToString("MM-dd") +"~"+ retEndDay.ToString("MM-dd");
        }
        /// <summary>;
        /// 根据根据开始时间确定第几周
        /// </summary>;
        /// <param name="beginDate">;</param>;
        /// <param name="endDate">;</param>;
        /// <returns>;</returns>;
        public static int RangWeekOfDateTimes(DateTime beginDate, DateTime endDate)
        {
            //确定此时间在一年中的位置
            int dayOfYear = beginDate.DayOfYear;
            //当年第一天
            DateTime tempDate = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day);
            //最后一天
            DateTime tempendDate = new DateTime(endDate.Year, endDate.Month, endDate.Day);
            int tempDayOfWeek = (int)tempDate.DayOfWeek;
            tempDayOfWeek = tempDayOfWeek == 0 ? 7 : tempDayOfWeek;

            //确定当前是第几周
            int weekIndex = (int)Math.Ceiling(((double)dayOfYear + tempDayOfWeek - 1) / 7);
            return weekIndex;
        }
        /// <summary>;
        /// 根据起始时间，获取第几周
        /// </summary>;
        /// <param name="dtime">;当前时间</param>;
        /// <returns>;</returns>;
        public static int WeekOfTermDate(DateTime dtime)
        {
            string datetime = DateTime.Now.ToString("yyyy-01-01");

            TimeSpan ts1 = new TimeSpan(dtime.Ticks);
            TimeSpan ts2 = new TimeSpan(Convert.ToDateTime(datetime).Ticks);
            TimeSpan ts = ts2.Subtract(ts1).Duration();

            //确定此时间在一年中的位置
            int dayOfYear = ts.Days;
            //当年第一天
            DateTime tempDate = new DateTime(Convert.ToDateTime(datetime).Year, Convert.ToDateTime(datetime).Month, Convert.ToDateTime(datetime).Day);

            int tempDayOfWeek = (int)tempDate.DayOfWeek;
            tempDayOfWeek = tempDayOfWeek == 0 ? 7 : tempDayOfWeek;
            ////确定星期几
            int index = (int)dtime.DayOfWeek;
            index = index == 0 ? 7 : index;

            //当前周的范围
            DateTime retStartDay = dtime.AddDays(-(index - 1));
            DateTime retEndDay = dtime.AddDays(7 - index);

            //确定当前是第几周
            int weekIndex = (int)Math.Ceiling(((double)dayOfYear + tempDayOfWeek) / 7);
            return weekIndex;
        }

        /// <summary>;
        /// 根据周，星期获得具体年月日
        /// </summary>;
        /// <param name="week">;第几周</param>;
        /// <param name="day">;星期几</param>;
        /// <returns>;</returns>;
        public static DateTime DateTimeByWeekAndDay(int week, int day)
        {
            DateTime someTime = Convert.ToDateTime("2011-3-1");

            int i = someTime.DayOfWeek - DayOfWeek.Monday;
            if (i == -1) i = 6;// i值 >; = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);

            //获取第N周的星期一
            someTime = someTime.Subtract(ts).AddDays((week - 1) * 7);
            //获得星期几
            someTime = someTime.AddDays(day - 1);
            return someTime;
        }
        /// <summary>
        /// 已知1970年一月一号零时到某时间的毫秒数,计算出该时间的具体时间戳
        /// </summary>
        /// <param name="TickCount">毫秒数</param>
        /// <returns></returns>
        public static DateTime ConvertDateTimeFromTick(double tickCount)
        {
            DateTime dt1970 = new DateTime(1970, 1, 1);
            return dt1970.AddMilliseconds(tickCount).ToLocalTime();
        }
        /// <summary>
        /// 秒数转日期
        /// </summary>
        /// <param name="Value">秒数</param>
        /// <returns>日期</returns>
        public static DateTime GetGMTDateTime(int Value)
        {
            //秒数转时间日期
            //GMT时间从2000年1月1日开始，先把它作为赋为初值
            long Year = 2000, Month = 1, Day = 1;
            long Hour = 0, Min = 0, Sec = 0;
            //临时变量
            long iYear = 0, iDay = 0;
            long iHour = 0, iMin = 0, iSec = 0;
            //计算文件创建的年份
            iYear = Value / (365 * 24 * 60 * 60);
            Year = Year + iYear;
            //计算文件除创建整年份以外还有多少天
            iDay = (Value % (365 * 24 * 60 * 60)) / (24 * 60 * 60);
            //把闰年的年份数计算出来
            int RInt = 0;
            for (int i = 2000; i < Year; i++)
            {
                if ((i % 4) == 0)
                    RInt = RInt + 1;
            }
            //计算文件创建的时间(几时)
            iHour = ((Value % (365 * 24 * 60 * 60)) % (24 * 60 * 60)) / (60 * 60);
            Hour = Hour + iHour;
            //计算文件创建的时间(几分)
            iMin = (((Value % (365 * 24 * 60 * 60)) % (24 * 60 * 60)) % (60 * 60)) / 60;
            Min = Min + iMin;
            //计算文件创建的时间(几秒)
            iSec = (((Value % (365 * 24 * 60 * 60)) % (24 * 60 * 60)) % (60 * 60)) % 60;
            Sec = Sec + iSec;
            DateTime t = new DateTime((int)Year, (int)Month, (int)Day, (int)Hour, (int)Min, (int)Sec);
            DateTime t1 = t.AddDays(iDay - RInt);
            return t1;
        }



        /// <summary>
        /// 日期转换为工作日时长（通过开始时间和结束时间将日期转化为工作日）
        /// </summary>
        /// <param name="stime">开始时间</param>
        /// <param name="etime">结束时间</param>
        /// <returns></returns>
        public static string Leavetransform(DateTime stime, DateTime etime)
        {
            string workday = "";
            int dateDiff = 0;//获取时间天数间隔
            TimeSpan st = new TimeSpan(stime.Ticks);
            TimeSpan et = new TimeSpan(etime.Ticks);
            TimeSpan timelong = et.Subtract(st).Duration();
            dateDiff = Convert.ToInt32(timelong.Days);

            if (stime.Hour > 12 && timelong.Hours > 16)
            {
                workday = timelong.Days.ToString() + "天" + Convert.ToInt32(timelong.Hours - 15) + "小时";
            }
            else if (stime.Hour < 12 && timelong.Hours > 16)
            {
                workday = timelong.Days.ToString() + "天" + Convert.ToInt32(timelong.Hours - 16) + "小时";
            }
            else if (stime.Hour <= 12 && etime.Hour >= 12)
            {
                workday = timelong.Days.ToString() + "天" + (Convert.ToInt32(timelong.Hours) - 1).ToString() + "小时";
            }
            else
            {
                workday = timelong.Days.ToString() + "天" + timelong.Hours.ToString() + "小时";
            }

            //workday = dateDiff * 8 + Convert.ToInt32(timelong.Hours) + "小时";



            return workday;
        }
        /// <summary>
        /// 取得某月的第一天
        /// </summary>
        /// <param name="datetime">要取得月份第一天的时间</param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day);
        }
        /**/
        /// <summary>
        /// 取得某月的最后一天
        /// </summary>
        /// <param name="datetime">要取得月份最后一天的时间</param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
        }

        /**/
        /// <summary>
        /// 取得上个月第一天
        /// </summary>
        /// <param name="datetime">要取得上个月第一天的当前时间</param>
        /// <returns></returns>
        public static DateTime FirstDayOfPreviousMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(-1);
        }

        /**/
        /// <summary>
        /// 取得上个月的最后一天
        /// </summary>
        /// <param name="datetime">要取得上个月最后一天的当前时间</param>
        /// <returns></returns>
        public static DateTime LastDayOfPrdviousMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddDays(-1);
        }
        /// <summary>
        /// 得到本周第一天(以星期一为第一天)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }
    }
}
