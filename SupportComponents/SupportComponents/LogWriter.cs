using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace HS.SupportComponents
{
    public sealed class LogWriter
    {
        public LogWriter()
        {
        }

        /// <summary>
        /// 获取日志实体
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static LogEntry GetLogEntity(string message, string title, TraceEventType type)
        {
            LogEntry logEntity = new LogEntry();
            logEntity.Title = title;
            logEntity.TimeStamp = DateTime.Now;
            logEntity.Message = message;
            logEntity.Severity = type;
            return logEntity;
        }
        /// <summary>
        /// 输出错误日志
        /// </summary>
        /// <param name="exception"></param>
        public static void WriteError(Exception exception, string title)
        {

            LogEntry logEntity = GetLogEntity(exception.ToString(), title, TraceEventType.Error);
            Logger.Write(logEntity);
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="debugMessage"></param>
        /// <param name="title"></param>
        public static void WriteInfo(string debugMessage, string title) // JJY added on 2009.8.25
        {
            LogEntry logEntity = GetLogEntity(debugMessage, title, TraceEventType.Information);
            Logger.Write(logEntity);
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="debugMessage"></param>
        /// <param name="title"></param>
        public static void WriteSingleInfo(string debugMessage, string title) // JJY added on 2009.8.25
        {
            LogEntry logEntity = GetLogEntity(debugMessage, title, TraceEventType.Information);

            string UnitCode = string.Empty;
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["TraceUnitCode"]))
            {
                UnitCode = ConfigurationManager.AppSettings["TraceUnitCode"].ToString();    // 单场所跟踪
            }

            if ((UnitCode.Length != 0 && (debugMessage.IndexOf(UnitCode) > -1 || title.IndexOf(UnitCode) > -1)) || UnitCode == "AllUnit")
                Logger.Write(logEntity);
        }

        /// <summary>
        /// 输出调试日志
        /// </summary>
        /// <param name="debugMessage"></param>
        /// <param name="title"></param>
        public static void WriteDebug(string debugMessage, string title)
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Debug"]) && bool.Parse(ConfigurationManager.AppSettings["Debug"]))
            {
                LogEntry logEntity = new LogEntry();
                logEntity.Title = title;
                logEntity.TimeStamp = DateTime.Now;
                logEntity.Message = debugMessage;
                logEntity.Severity = TraceEventType.Information;
                WriteDebugLog(logEntity);
            }
        }

        /// <summary>
        /// 输出数据日志
        /// </summary>
        /// <param name="xmlMessage"></param>
        /// <param name="title"></param>
        public static void WriteXml(string xmlMessage, string title)
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["XmlLog"]) && bool.Parse(ConfigurationManager.AppSettings["XmlLog"]))
            {
                LogEntry logEntity = new LogEntry();
                logEntity.Title = title;
                logEntity.TimeStamp = DateTime.Now;
                logEntity.Message = xmlMessage;
                logEntity.Severity = TraceEventType.Verbose;
                Logger.Write(logEntity);
            }
        }

        /// <summary>
        /// 输出实体日志
        /// </summary>
        /// <param name="xmlMessage"></param>
        /// <param name="title"></param>
        public static void WriteEntity(string xmlMessage, string title)
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["EntityLog"]) && bool.Parse(ConfigurationManager.AppSettings["EntityLog"]))
            {
                LogEntry logEntity = new LogEntry();
                logEntity.Title = title;
                logEntity.TimeStamp = DateTime.Now;
                logEntity.Message = xmlMessage;
                logEntity.Severity = TraceEventType.Verbose;
                Logger.Write(logEntity);
            }
        }

        /// <summary>
        /// 输出调试日志
        /// </summary>
        /// <param name="iDictionary"></param>
        /// <param name="title"></param>
        public static void WriteDebug(IDictionary<string, object> iDictionary, string title)
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Debug"]) && bool.Parse(ConfigurationManager.AppSettings["Debug"]))
            {
                LogEntry logEntity = new LogEntry();
                logEntity.Title = title;
                logEntity.TimeStamp = DateTime.Now;
                logEntity.ExtendedProperties = iDictionary;
                logEntity.Severity = TraceEventType.Verbose;
                WriteDebugLog(logEntity);
            }
        }

        /// <summary>
        /// 输出调试日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="title"></param>
        public static void WriteDebug<T>(T t, string title, params Type[] typesCanBeGetted) where T : class // JJY added typesCanBeGetted on 2009.9.4
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Debug"]) && bool.Parse(ConfigurationManager.AppSettings["Debug"]))
            {
                LogEntry logEntity = new LogEntry();
                logEntity.Title = title;
                logEntity.TimeStamp = DateTime.Now;
                logEntity.Message = GetEntityProperties("    ", t, typesCanBeGetted);   // JJY changed " BuildEntity<T>(t)" to "GetEntityProperties" on 2009.9.4
                logEntity.Severity = TraceEventType.Verbose;
                WriteDebugLog(logEntity);
            }
        }


        /// <summary>
        /// 输出调试日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="title"></param>
        public static void WriteDebug<T>(IList<T> t, string title, params Type[] typesCanBeGetted) where T : class // JJY added typesCanBeGetted on 2009.9.4
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Debug"]) && bool.Parse(ConfigurationManager.AppSettings["Debug"]))
            {
                LogEntry logEntity = new LogEntry();
                logEntity.Title = title;
                logEntity.TimeStamp = DateTime.Now;
                logEntity.Message = GetEntityListProperties(t == null ? null : t.ToArray<T>(), typesCanBeGetted); // JJY replaced "BuildEntity<T>(t)" on 2009.9.4;
                logEntity.Severity = TraceEventType.Verbose;
                WriteDebugLog(logEntity);
            }
        }

        /// <summary>
        /// 输出调试日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="title"></param>
        public static void WriteDebug<T>(T[] t, string title, params Type[] typesCanBeGetted) where T : class // JJY added typesCanBeGetted on 2009.9.4
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Debug"]) && bool.Parse(ConfigurationManager.AppSettings["Debug"]))
            {
                LogEntry logEntity = new LogEntry();
                logEntity.Title = title;
                logEntity.TimeStamp = DateTime.Now;
                logEntity.Message = GetEntityListProperties(t, typesCanBeGetted); // JJY replaced "BuildEntity<T>(t)" on 2009.9.4;
                logEntity.Severity = TraceEventType.Verbose;
                WriteDebugLog(logEntity);
            }
        }

        /// <summary>
        /// 设置写日志
        /// </summary>
        /// <param name="logEntity"></param>
        private static void WriteDebugLog(LogEntry logEntity)
        {
            bool isWrote = false;
            bool isTraced = false;

            if (!isWrote && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["TraceUnitCode"]))   //跟踪场所
            {
                isTraced = true;
                string unitCode = ConfigurationManager.AppSettings["TraceUnitCode"].ToString();
                foreach (string str in unitCode.Split(';'))
                {
                    if ((logEntity.Title.IndexOf(str) > -1 || logEntity.Message.IndexOf(str) > -1) || str == "AllUnit")
                    {
                        Logger.Write(logEntity);
                        isWrote = true;
                        break;
                    }
                }
            }

            if (!isWrote && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["KeywordLog"])) //关键字
            {
                isTraced = true;
                string key = ConfigurationManager.AppSettings["KeywordLog"].ToString();
                foreach (string str in key.Split(';'))
                {
                    if (!string.IsNullOrEmpty(str) && (logEntity.Message.ToLower().IndexOf(str.ToLower()) > -1 || logEntity.Title.ToLower().IndexOf(str.ToLower()) > -1))
                    {
                        Logger.Write(logEntity);
                        isWrote = true;
                        break;
                    }
                }
            }

            if (!isWrote && !isTraced)
            {
                Logger.Write(logEntity);
            }
        }

        #region 反射获取类成员

        /// <summary>获取实体的属性（包括公共的成员变量）值。
        /// 格式：每个属性值以空格分开
        /// </summary>
        public static string GetEntityListProperties(object[] entityList, params Type[] typesCanBeGetted)// JJY added on 2009.8.24
        {
            if (entityList == null)
                return "";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < entityList.Length; i++ )
            {
                string str = GetEntityProperties("    ", entityList[i], typesCanBeGetted);
                sb.AppendLine();
                sb.Append(string.Format("        List[{0}]数据：", i));
                sb.Append(str);
            }
            return sb.ToString();
        }

        /// <summary>获取列表中所有实体的属性（包括公共的成员变量）值。
        /// 格式：每个实体的属性值都在新的一行中，属性值以空格分开
        /// </summary>
        public static string GetEntityProperties(object entity, params Type[] typesCanBeGetted)// JJY added on 2009.9.1
        {
            return GetEntityProperties("", entity, typesCanBeGetted);
        }

        private static string GetEntityProperties(string prevSpace, object entity, params Type[] typesCanBeGetted)// JJY added on 2009.8.24
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                if (entity == null)
                    return "";

                Type type = entity.GetType();
                List<MemberInfo> infos = new List<MemberInfo>();
                FieldInfo[] fiArr = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
                PropertyInfo[] piArr = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                if (fiArr != null)
                    infos.AddRange(fiArr);
                if (piArr != null)
                    infos.AddRange(piArr);

                // 设置返回的数据
                if (infos != null && infos.Count > 0)
                {
                    foreach (MemberInfo info in infos)
                    {
                        try
                        {
                            bool tagType = false;
                            object val = type.InvokeMember(info.Name, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.GetField | BindingFlags.Instance, null, entity, null);

                            // 若是目标要打印的实体类型，则递归打印该实体
                            if (typesCanBeGetted != null)
                            {
                                foreach (Type t in typesCanBeGetted)
                                {
                                    if (info.ToString().StartsWith(t.FullName + " "))
                                    {
                                        tagType = true;
                                        sb.AppendLine();
                                        sb.AppendLine(string.Format("{0}{0}{0}{1}实体数据：    {2}", prevSpace, t.Name, GetEntityProperties(prevSpace, val, typesCanBeGetted)));
                                        break;
                                    }
                                }
                            }

                            // 若不是目标要打印的实体类型，则直接打印属性值
                            if (!tagType)
                                sb.AppendFormat(" {0}=\"{1}\" ", info.Name, ConvertToString(val));
                        }
                        catch { }
                    }
                }
            }
            catch { }

            return sb.ToString();
        }

        public static string GetDictionaryInfo<T, K>(IDictionary<T, K> dictionary)
        {
            string message = string.Empty;
            int count = 0;
            if (dictionary != null)
            {
                foreach (T key in dictionary.Keys)
                {
                    count++;
                    message += string.Format("{0}={1}; ", key, dictionary[key]);
                }
            }

            return string.Format("Count:{0}, DictionaryInfo:{1}", count, message);
        }

        public static string GetDictionaryInfo<T, K>(IDictionary<T, K> dictionary, params Type[] typesCanBeGetted)
        {
            string message = string.Empty;
            int count = 0;
            if (dictionary != null)
            {
                foreach (T key in dictionary.Keys)
                {
                    count++;
                    message += string.Format("{0}={1}; ", key, GetEntityProperties("",  dictionary[key], typesCanBeGetted));
                }
            }

            return string.Format("Count:{0}, DictionaryInfo:{1}", count, message);
        }

        private static string BuildEntity<T>(T entity) where T : class
        {
            StringBuilder sb = new StringBuilder("\r\n");

            try
            {
                if (entity != null)
                {
                    PropertyInfo[] infos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance);
                    if (infos != null && infos.Length > 0)
                    {
                        foreach (PropertyInfo info in infos)
                        {
                            try
                            {
                                object val = typeof(T).InvokeMember(info.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance, null, entity, null);
                                sb.AppendFormat(" {0}=\"{1}\" ", info.Name, ConvertToString(val));
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { }

            return sb.ToString();
        }

        public static string BuildEntity<T>(IList<T> list) where T : class
        {
            StringBuilder sb = new StringBuilder("\r\n");
            try
            {
                if (list != null && list.Count > 0)
                {
                    PropertyInfo[] infos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance);
                    if (infos != null && infos.Length > 0)
                    {
                        foreach (T obj in list)
                        {
                            foreach (PropertyInfo info in infos)
                            {
                                try
                                {
                                    object val = typeof(T).InvokeMember(info.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance, null, obj, null);
                                    sb.AppendFormat(" {0}=\"{1}\"", info.Name, ConvertToString(val));
                                }
                                catch { }
                            }
                            sb.Append(" \r\n");
                        }
                    }
                }
            }
            catch { }

            return sb.ToString();
        }

        private static string ConvertToString(object val)
        {
            if (val is DateTime)
            {
                return ((DateTime)val).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (val is Enum)
            {
                return Enum.Format(val.GetType(), val, "d");
            }
            else if (val is bool)
            {
                if ((bool)val)
                    return "1";
                else
                    return "0";
            }
            else
            {
                return Convert.ToString(val);
            }
        }

        #endregion
    }
}
