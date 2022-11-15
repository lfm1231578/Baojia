using System;
using System.Text;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;

using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

namespace HS.SupportComponents
{
    /// <summary>
    /// 压缩处理辅助类
    /// </summary>
    public sealed class ZipHelper
    {
        #region FTP有关路径
        private static string ftp_Path = string.Empty;
        public static string FTP_Path
        {
            set
            {
                ftp_Path = value;
                Directory.CreateDirectory(ftp_Path);
            }
            get
            {
                return ftp_Path;
            }
        }
        private static string ftp_Error = string.Empty;
        public static string FTP_Error
        {
            set
            {
                ftp_Error = value;
                Directory.CreateDirectory(ftp_Error);
            }
            get
            {
                return ftp_Error;
            }
        }
        #endregion

        private const string extend = "XXX";
        private const string xmlExtension = ".xml";
        private const string gzipExtension = ".gz";
        private const string zipExtension = ".zip";
        private const string eddExtension = ".edd";
        private const int MAX_LENGTH = 4 * 1024 * 1024;

        public enum ZFileType { Video = 0, Picture = 1 };
        /*文件名定义
         * AABBBBBBBBBBBBBBCCCDDDDDDDDDDDDDDDDDEEE.FFF.GGG
         */
        /// <summary>
        /// 生成FTP定义文件名称
        /// </summary>
        /// <param name="GuildID">场所类型</param>
        /// <param name="UnitCode">场所编码</param>
        /// <param name="TableNameID">数据项标识</param>
        /// <returns>生成的文件名称</returns>
        public static string GetFileName(int GuildID, string UnitCode, string TableNameID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("00"); //A：为2位长度数字编码的行业类别
            string UnitTemp = "00000000000000";
            sb.Append(UnitCode + UnitTemp.Substring(0, UnitTemp.Length - UnitCode.Length));//B：为14位长度的场所编码
            sb.Append(TableNameID); //C：为3位数字编码的文件处理识别
            sb.Append(DateTime.Now.ToString("yyyyMMddHHmmssfff")); //D：文件生成日期时间截
            //sb.Append( extend ); //E：为扩展定义
            sb.Append(xmlExtension); //F：原文件格式后缀名
            return sb.ToString();
        }

        /// <summary>
        /// 生成FTP定义文件名称
        /// </summary>
        /// <param name="GuildID">场所类型</param>
        /// <param name="UnitCode">场所编码</param>
        /// <param name="TableNameID">数据项标识</param>
        /// <param name="extension">扩展名</param>
        /// <returns>生成的文件名称</returns>
        public static string GetFileName(int GuildID, string UnitCode, string TableNameID, string extension)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("00"); //A：为2位长度数字编码的行业类别
            string UnitTemp = "00000000000000";
            sb.Append(UnitCode + UnitTemp.Substring(0, UnitTemp.Length - UnitCode.Length));//B：为14位长度的场所编码
            sb.Append(TableNameID); //C：为3位数字编码的文件处理识别
            sb.Append(DateTime.Now.ToString("yyyyMMddHHmmssfff")); //D：文件生成日期时间截
            //sb.Append( extend ); //E：为扩展定义
            sb.Append(extension); //F：原文件格式后缀名
            return sb.ToString();
        }

        /// <summary>
        /// 生成FTP定义文件名称 MFang 2007-11-28 增加自定义文件名 [文件名].[自定义字符串].[后缀]
        /// </summary>
        /// <param name="GuildID">场所类型</param>
        /// <param name="UnitCode">场所编码</param>
        /// <param name="TableNameID">数据项标识</param>
        /// <param name="extension">扩展名</param>
        /// <returns>生成的文件名称</returns>
        public static string GetFileName(int GuildID, string UnitCode, string TableNameID, string extension, string extensionStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("00"); //A：为2位长度数字编码的行业类别
            string UnitTemp = "00000000000000";
            sb.Append(UnitCode + UnitTemp.Substring(0, UnitTemp.Length - UnitCode.Length));//B：为14位长度的场所编码
            sb.Append(TableNameID); //C：为3位数字编码的文件处理识别
            sb.Append(DateTime.Now.ToString("yyyyMMddHHmmssfff")); //D：文件生成日期时间截
            //sb.Append( extend ); //E：为扩展定义
            sb.Append(extensionStr);
            sb.Append(extension); //F：原文件格式后缀名
            return sb.ToString();
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="filePath">文件完全路径</param>
        public static bool CompressFile(string filePath)
        {
            FileStream ins = File.OpenRead(filePath);
            FileInfo info = new FileInfo(filePath);
            FileStream outs = File.Create(info.DirectoryName + @"\" + info.Name.Replace(info.Extension, string.Empty) + ".zip");
            ZipOutputStream s = new ZipOutputStream(outs);
            s.SetLevel(5);
            ZipEntry entry = new ZipEntry(info.Name);
            s.PutNextEntry(entry);
            byte[] buffer = new byte[ins.Length];
            ins.Read(buffer, 0, buffer.Length);
            s.Write(buffer, 0, buffer.Length);
            s.Finish();
            s.Close();
            outs.Close();
            ins.Close();
            return true;
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="filePath">日志目录路径</param>
        /// <param name="searchPattern">搜索的字符串(如:*.log)</param>
        public static List<string> GetFiles(string path, string searchPattern)
        {
            return GetFiles(path, searchPattern,0);
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="filePath">日志目录路径</param>
        /// <param name="searchPattern">搜索的字符串(如:*.log)</param>
        /// <param name="day">扫描n天前的文件</param>
        public static List<string> GetFiles(string path, string searchPattern,int day)
        {
            List<string> listLogFile = new List<string>();
            DirectoryInfo thisOne = new DirectoryInfo(path);
            DateTime dtFile;
            foreach (FileInfo fileInfo in thisOne.GetFiles(searchPattern))
            {
                dtFile = fileInfo.LastWriteTime.AddDays(day);
                if (dtFile < DateTime.Now)
                    listLogFile.Add(fileInfo.FullName);
            }
            return listLogFile;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件完全路径</param>
        public static bool DeleteFile(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            if (info.Exists)
                info.Delete();
            return true;
        }

        /// <summary>
        /// 根据文件名取数据项标识
        /// </summary>
        /// <param name="FileName">上传来的文件名称</param>
        /// <returns>数据项标识</returns>
        private static string GetFileIDByFileName(string FileName)
        {
            if (FileName.Length < 20)
                return string.Empty;
            return FileName.Substring(16, 3);
        }

        /// <summary>
        /// 压缩(Zip)
        /// </summary>
        /// <param name="FullFileName">源文件名</param>
        /// <returns>压缩后文件名</returns>
        public static string Zip(string FullFileName)
        {
            string[] filenames = new string[2];
            filenames[0] = FullFileName; //源文件
            FileInfo info = new FileInfo(FullFileName);
            filenames[1] = info.DirectoryName + @"\" + info.Name + ".zip";
            ZipOutputStream s = new ZipOutputStream(File.Create(filenames[1]));
            s.SetLevel(5); // 0 - store only to 9 - means best compression
            string file = filenames[0];
            FileStream fs = File.OpenRead(file);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            ZipEntry entry = new ZipEntry(info.Name);
            s.PutNextEntry(entry);
            s.Write(buffer, 0, buffer.Length);
            s.Finish();
            s.Close();
            fs.Close();
            return filenames[1];
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="obj">源压缩对象</param>
        /// <returns>压缩后的对象</returns>
        public static object Zip(object obj)
        {
            MemoryStream ms = new MemoryStream();
            DeflateStream zip = new DeflateStream(ms, CompressionMode.Compress, true);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(zip, obj);
            zip.Close();
            return (object)ms.ToArray();
        }

        /// <summary>
        /// 压缩(GZip)
        /// </summary>
        /// <param name="FullFileName">源文件名</param>
        /// <returns>压缩后文件名</returns>
        public static string GZip(string FullFileName)
        {
            string gzip = FullFileName + ".gz";
            Stream s = new GZipOutputStream(File.Create(gzip));
            FileStream fs = File.OpenRead(FullFileName);
            byte[] writeData = new byte[fs.Length];
            fs.Read(writeData, 0, (int)fs.Length);
            s.Write(writeData, 0, writeData.Length);
            s.Close();
            fs.Close();
            return gzip;
        }

        /// <summary>
        /// 解压(Zip)
        /// </summary>
        /// <param name="FullFileName">源文件名</param>
        /// <returns>解压后文件名</returns>
        public static string UnZip(string FullFileName)
        {
            /*
            FileInfo info = new FileInfo(FullFileName);
            if (info.Extension != ".zip" || !File.Exists(FullFileName))
                return FullFileName;
            string path = FullFileName.Substring(0, FullFileName.Length - 4);
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

            ZipInputStream s = new ZipInputStream(File.OpenRead(FullFileName));
            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                int size = 2048;
                byte[] data = new byte[2048];
                while (true)
                {
                    size = s.Read(data, 0, data.Length);
                    if (size > 0)
                    {
                        fs.Write(data, 0, data.Length);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            fs.Close();
            s.Close();
            return path;
            */
            
            ZipInputStream zipStream = new ZipInputStream(File.Open(FullFileName, FileMode.Open));
            ZipEntry entry = zipStream.GetNextEntry();
            string path = FullFileName.Substring(0, FullFileName.Length - 4);
            while (entry != null)
            {
                if (!entry.IsFile)
                    continue;

                FileStream writer = File.Create(path); //解压后的文件          
                int bufferSize = 2048; //缓冲区大小     
                int readCount = 0; //读入缓冲区的实际字节     
                byte[] buffer = new byte[bufferSize];
                readCount = zipStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    writer.Write(buffer, 0, readCount);
                    readCount = zipStream.Read(buffer, 0, bufferSize);
                }
                writer.Close();
                writer.Dispose();
                entry = zipStream.GetNextEntry();
            }
            zipStream.Close();
            zipStream.Dispose();
            return path;
        }
        /// <summary>
        /// 解压(Zip)
        /// </summary>
        /// <param name="FullFileName">源文件名</param>
        /// <returns>解压后文件名</returns>
        public static string UnZip2(string FullFileName)
        {
            string result = string.Empty;
            ZipInputStream zipStream = new ZipInputStream(File.Open(FullFileName, FileMode.Open));
            ZipEntry entry = zipStream.GetNextEntry();
            string dirPath = FullFileName.Substring(0, FullFileName.LastIndexOf("\\"));
            while (entry != null)
            {
                if (!entry.IsFile)
                    continue;

                string fileName = entry.Name;
                string extension = fileName.Substring(fileName.LastIndexOf("."));
                string filePath = FullFileName.Substring(0, FullFileName.Length - 4) + extension;
                result = filePath;

                FileStream writer = File.Create(filePath); //解压后的文件          
                int bufferSize = 2048; //缓冲区大小     
                int readCount = 0; //读入缓冲区的实际字节     
                byte[] buffer = new byte[bufferSize];
                readCount = zipStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    writer.Write(buffer, 0, readCount);
                    readCount = zipStream.Read(buffer, 0, bufferSize);
                }

                writer.Close();
                writer.Dispose();

                entry = zipStream.GetNextEntry();
            }
            zipStream.Close();
            zipStream.Dispose();

            return result;
        }


        /// <summary>
        /// 解压缩后对象
        /// </summary>
        /// <param name="obj">压缩前对象</param>
        /// <returns></returns>
        public static object UpZip(object obj)
        {
            byte[] ary = (byte[])obj;
            MemoryStream ms = new MemoryStream(ary);
            DeflateStream UnZip = new DeflateStream(ms, CompressionMode.Decompress);

            BinaryFormatter serializer = new BinaryFormatter();
            object objTemp = serializer.Deserialize(UnZip);
            UnZip.Close();
            ms.Close();
            return objTemp;
        }

        /// <summary>
        /// 解压(GZip)
        /// </summary>
        /// <param name="FullFileName">源文件名</param>
        /// <returns>解压后文件名</returns>
        public static string UnGZip(string FullFileName)
        {
            string file = Path.GetDirectoryName(FullFileName) + "\\" + Path.GetFileNameWithoutExtension(FullFileName);
            Stream s = null;
            FileStream fs = null;
            try
            {
                s = new GZipInputStream(File.OpenRead(FullFileName));
                fs = File.Create(file);
                int size = 2048;
                byte[] writeData = new byte[2048];
                while (true)
                {
                    size = s.Read(writeData, 0, size);
                    if (size > 0)
                    {
                        fs.Write(writeData, 0, size);
                        if (fs.Position > MAX_LENGTH)
                        {
                            //ApplicationLog.WriteError(new Exception("接收到文件:[" + FullFileName + "],解压超出4M,终止解压"), "解压文件", 0);
                            if (File.Exists(file))
                            {
                                fs.Close();
                                File.Delete(file);
                            }
                            string error = ZipHelper.FTP_Error + "\\" + Path.GetFileName(FullFileName);
                            if (File.Exists(FullFileName) && !File.Exists(error)) //把方式转移目录 
                            {
                                s.Close();
                                File.Move(FullFileName, error);
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            finally
            {
                if (s != null)
                    s.Close();
                if (fs != null)
                    fs.Close();
            }
            return file;
        }

        /// <summary>
        /// 解压(Tar)
        /// </summary>
        /// <param name="FullFileName">源文件名</param>
        /// <param name="TargetDir">目的目录</param>
        /// <returns></returns>
        public static bool UnTar(string FullFileName, string TargetDir)
        {
            try
            {
                TarInputStream input = new TarInputStream(File.OpenRead(FullFileName));
                TarEntry entry = null;
                if (input == null)
                    return false;
                using (input)
                {
                    while ((entry = input.GetNextEntry()) != null)
                    {
                        if (entry.IsDirectory)
                            continue;
                        string file = TargetDir + "\\" + entry.Name;
                        Directory.CreateDirectory(Path.GetDirectoryName(file));
                        byte[] writeData = new byte[2048];
                        using (FileStream fs = File.Create(file))
                        {
                            int x = 0;
                            while ((x = input.Read(writeData, 0, 2048)) > 0)
                            {
                                fs.Write(writeData, 0, x);
                            };
                            //fs.Close()
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}