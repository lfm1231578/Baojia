
using System;
using System.Configuration;
using System.Text;

namespace HS.SupportComponents
{
    /// <summary>
    /// 系统追踪器
    /// 1.探测点追踪
    /// 2.错误信息追踪
    /// </summary>
    public class Tracker
    {
        /// <summary>
        /// 是否设置了跟踪类型
        /// </summary>
        private static bool ISSETTRACKTYPE;

        /// <summary>
        /// 跟踪类型
        /// </summary>
        private static TrackType TRACKTYPE = TrackType.LogFileForInfo;

        /// <summary>
        /// 锁标志
        /// </summary>
        private static readonly object LOCKFLAG = new object();

        /// <summary>
        /// 探测点追踪
        /// </summary>
        /// <param name="content"></param>
        public static void ProbeTrack(string content)
        {
            ProbeTrack(content, "系统探测点跟踪");
        }

        /// <summary>
        /// 探测点追踪
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        public static void ProbeTrack(string content, string title)
        {
            InitTrackType();
            if (TRACKTYPE != TrackType.ConsoleDisplayerForError && TRACKTYPE != TrackType.LogFileForError)
                InfoTrack(content, title);
        }

        /// <summary>
        /// 错误信息追踪
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        public static void ErrorTrack(string content, string title)
        {
            InitTrackType();
            InfoTrack(content, title);
        }

        /// <summary>
        /// 错误信息追踪
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="title"></param>
        public static void ErrorTrack(Exception ex, string title)
        {
            title = title + ":" + ex.Message;
            if (TRACKTYPE == TrackType.LogFileForError)
            {
                LogWriter.WriteError(ex, title);
            }
            else
            {
                ErrorTrack(ex.StackTrace, title);
            }
        }

        /// <summary>
        /// 信息追踪
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        private static void InfoTrack(string content, string title)
        {
            if (TRACKTYPE == TrackType.ConsoleDisplayerForError || TRACKTYPE == TrackType.ConsoleDisplayerForInfo)
                //控制台显示
                ShowInConsole(content, title);
            else if (TRACKTYPE == TrackType.LogFileForInfo) //写日志文件
                WriteLog(content, title);
        }

        /// <summary>
        /// 初始化追踪信息的追踪类型
        /// </summary>
        private static void InitTrackType()
        {
            if (!ISSETTRACKTYPE)
            {
                lock (LOCKFLAG)
                {
                    if (!ISSETTRACKTYPE)
                    {
                        ISSETTRACKTYPE = true;
                        switch (ConfigurationSettings.AppSettings["TRACKTYPE"])
                        {
                            case "ConsoleDisplayer":
                                ConsoleDisplayer.RunConsole();
                                TRACKTYPE = TrackType.ConsoleDisplayerForInfo;
                                break;
                            case "ConsoleDisplayerForError":
                                ConsoleDisplayer.RunConsole();
                                TRACKTYPE = TrackType.ConsoleDisplayerForError;
                                break;
                            case "LogFile":
                                TRACKTYPE = TrackType.LogFileForInfo;
                                break;
                            case "LogFileForError":
                                TRACKTYPE = TrackType.LogFileForError;
                                break;
                            default:
                                TRACKTYPE = TrackType.None;
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 控制台显示
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        private static void ShowInConsole(string content, string title)
        {
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine(title);
            Console.WriteLine(content);
            Console.WriteLine("-----------------------------------------------");
        }

        /// <summary>
        /// 写日志文件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        private static void WriteLog(string content, string title)
        {
            var sb = new StringBuilder();
            sb.AppendLine("+++++++++++++++++++++++++++++++++++++++++++++++");
            sb.AppendLine(content);
            sb.AppendLine("-----------------------------------------------");
            LogWriter.WriteInfo(sb.ToString(), title);
        }
    }

    /// <summary>
    /// 追踪类型
    /// </summary>
    internal enum TrackType
    {
        /// <summary>
        /// 不做任何追踪
        /// </summary>
        None,

        /// <summary>
        /// 控制台追踪所有信息
        /// </summary>
        ConsoleDisplayerForInfo,

        /// <summary>
        /// 控制台追踪错误信息
        /// </summary>
        ConsoleDisplayerForError,

        /// <summary>
        /// 日志文件追踪所有信息
        /// </summary>
        LogFileForInfo,

        /// <summary>
        /// 日志文件追踪错误信息
        /// </summary>
        LogFileForError
    }
}