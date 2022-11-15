using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Globalization;
using System.IO;
using EnterpriseDT.Net.Ftp;
using System.Configuration;
namespace HS.SupportComponents
{
    /// <summary>
    /// 上传和下载
    /// </summary>
    public class UploadAndDown : Controller
    {
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        public string BufferToDisk()
        {

            var path = Server.MapPath("~/Uploads") + DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo) + "/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (string file in Request.Files)
            {
                var fileBase = Request.Files[file];
                try
                {
                    if (fileBase.ContentLength > 0)
                    {
                        var fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + fileBase.FileName.Substring(fileBase.FileName.LastIndexOf("."));
                        fileBase.SaveAs(path + "/" + fileName);
                        return fileName;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return "";
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ActionResult DownFile(string fileName)
        {
            var filePath = Server.MapPath("~/Uploads");
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = "application/octet-stream";

            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileName));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            return new EmptyResult();

        }

        //上传
        public static void UploadFile(string fullName, string Name)
        {
            //上传
            FTPConnection connection = OpenConnection();
            try
            {
                if (connection != null)
                {
                    try
                    {

                        connection.ConnectMode = FTPConnectMode.PASV;    //设置为被动模式.
                        connection.TransferType = FTPTransferType.BINARY;    //传输类型为ASCII
                        connection.UploadFile(fullName, Name);
                    }
                    catch (Exception ex) { LogWriter.WriteError(ex, "文件上传"); }

                }
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private static FTPConnection OpenConnection()
        {

            FTPConnection connection = new FTPConnection();
            try
            {
                connection.ServerAddress = ConfigurationManager.AppSettings["PVHost"];
                connection.UserName = ConfigurationManager.AppSettings["FtpUserName"];
                connection.Password = ConfigurationManager.AppSettings["FtpPassword"];
                connection.ServerPort = ConvertValueHelper.ConvertIntValue(ConfigurationManager.AppSettings["PVPort"]);
                connection.TransferType = EnterpriseDT.Net.Ftp.FTPTransferType.BINARY;
                connection.ConnectMode = EnterpriseDT.Net.Ftp.FTPConnectMode.PASV;
                connection.Timeout = 5 * 60 * 1000;
                connection.Connect();
                return connection;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}
