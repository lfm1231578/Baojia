
using System;
using System.Configuration;
using System.Text;

namespace HS.SupportComponents
{
    /// <summary>
    /// ϵͳ׷����
    /// 1.̽���׷��
    /// 2.������Ϣ׷��
    /// </summary>
    public class Tracker
    {
        /// <summary>
        /// �Ƿ������˸�������
        /// </summary>
        private static bool ISSETTRACKTYPE;

        /// <summary>
        /// ��������
        /// </summary>
        private static TrackType TRACKTYPE = TrackType.LogFileForInfo;

        /// <summary>
        /// ����־
        /// </summary>
        private static readonly object LOCKFLAG = new object();

        /// <summary>
        /// ̽���׷��
        /// </summary>
        /// <param name="content"></param>
        public static void ProbeTrack(string content)
        {
            ProbeTrack(content, "ϵͳ̽������");
        }

        /// <summary>
        /// ̽���׷��
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
        /// ������Ϣ׷��
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        public static void ErrorTrack(string content, string title)
        {
            InitTrackType();
            InfoTrack(content, title);
        }

        /// <summary>
        /// ������Ϣ׷��
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
        /// ��Ϣ׷��
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        private static void InfoTrack(string content, string title)
        {
            if (TRACKTYPE == TrackType.ConsoleDisplayerForError || TRACKTYPE == TrackType.ConsoleDisplayerForInfo)
                //����̨��ʾ
                ShowInConsole(content, title);
            else if (TRACKTYPE == TrackType.LogFileForInfo) //д��־�ļ�
                WriteLog(content, title);
        }

        /// <summary>
        /// ��ʼ��׷����Ϣ��׷������
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
        /// ����̨��ʾ
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
        /// д��־�ļ�
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
    /// ׷������
    /// </summary>
    internal enum TrackType
    {
        /// <summary>
        /// �����κ�׷��
        /// </summary>
        None,

        /// <summary>
        /// ����̨׷��������Ϣ
        /// </summary>
        ConsoleDisplayerForInfo,

        /// <summary>
        /// ����̨׷�ٴ�����Ϣ
        /// </summary>
        ConsoleDisplayerForError,

        /// <summary>
        /// ��־�ļ�׷��������Ϣ
        /// </summary>
        LogFileForInfo,

        /// <summary>
        /// ��־�ļ�׷�ٴ�����Ϣ
        /// </summary>
        LogFileForError
    }
}