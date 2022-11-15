using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using HS.SupportComponents;

namespace HS.SupportComponents.ModulePlatform
{
    public sealed class PlatformStartupHelper
    {
        private static List<IPlatformStartup> typeNameList = new List<IPlatformStartup>();
        
        /// <summary>
        /// 启动指定子模块的入口
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void Start()
        {
            List<string> list = AnalysisFile();
            foreach (string typeName in list)
            {
                try
                {
                    IPlatformStartup platform = CreateInstance(typeName);
                    platform.Start();
                    typeNameList.Add(platform);
                }
                catch (Exception ex)
                {
                    //throw ex;
                    LogWriter.WriteError(ex, "启动调度出错, 命名空间:" + typeName);
                }
            }

        }
        /// <summary>
        /// 启动指定子模块的出口
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void Stop()
        {
            try
            {
                foreach (IPlatformStartup iplatformStartup in typeNameList)
                {
                    iplatformStartup.Stop();
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                LogWriter.WriteError(ex, "关闭调度出错");
            }
        }
        /// <summary>
        /// 解析xml文件
        /// </summary>
        private static List<string> AnalysisFile()
        {
            List<string> list = new List<string>();
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PlatformStartup.xml");

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            XmlNode root = xmlDocument.DocumentElement;
            XmlNodeList platformList = root.SelectNodes("descendant::Platform");
            foreach (XmlNode platform in platformList)
            {
               list.Add( platform.Attributes["TypeName"].Value);
            }
            return list;
        }
        /// <summary>
        /// 创建指定类型的实例
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private static IPlatformStartup CreateInstance(string typeName)
        {
            Type type = Type.GetType(typeName);
            Object obj = Activator.CreateInstance(type);
            return (IPlatformStartup)obj;
        }
    }
}
