using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using System.IO;

namespace HS.SupportComponents
{
    public class FileHelper
    {
        #region 压缩和解压
        /// <summary>
        /// 压缩(Zip)
        /// </summary>
        /// <param name="FullFileName"></param>
        /// <returns></returns>
        public static string Zip(string FullFileName)
        {
            lock (typeof(File))
            {
                string[] filenames = new string[2];
                filenames[0] = FullFileName; //源文件
                FileInfo info = new FileInfo(FullFileName);
                filenames[1] = info.DirectoryName + @"\" + info.Name + ".zip";
                ZipOutputStream s = new ZipOutputStream(System.IO.File.Create(filenames[1]));
                s.SetLevel(5); // 0 - store only to 9 - means best compression
                string file = filenames[0];
                FileStream fs = System.IO.File.OpenRead(file);
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
        }

        /// <summary>
        /// 压缩(Zip)
        /// </summary>
        /// <param name="FullFileName"></param>
        /// <returns></returns>
        public static byte[] Zip(byte[] data, string FileName)
        {
            lock (typeof(File))
            {
                MemoryStream mstream = new MemoryStream();
                ZipOutputStream zipOutStream = new ZipOutputStream(mstream);
                zipOutStream.SetLevel(5);
                ZipEntry ZipEntry = new ZipEntry(FileName);
                zipOutStream.PutNextEntry(ZipEntry);

                zipOutStream.Write(data, 0, data.Length);
                zipOutStream.Flush();
                zipOutStream.Close();

                byte[] result = mstream.ToArray();
                mstream.Close();

                return result;

            }
        }

        /// <summary>
        /// 压缩(GZip)
        /// </summary>
        /// <param name="FullFileName"></param>
        /// <returns></returns>
        public static string GZip(string FullFileName)
        {
            lock (typeof(File))
            {
                string gzip = FullFileName + ".gz";
                Stream s = new GZipOutputStream(System.IO.File.Create(gzip));
                FileStream fs = System.IO.File.OpenRead(FullFileName);
                byte[] writeData = new byte[fs.Length];
                fs.Read(writeData, 0, (int)fs.Length);
                s.Write(writeData, 0, writeData.Length);
                s.Close();
                fs.Close();
                return gzip;
            }
        }

        //解压(Zip)
        public static string UnZip(string FullFileName)
        {
            lock (typeof(File))
            {
                FileInfo info = new FileInfo(FullFileName);
                if (info.Extension != ".zip" || !System.IO.File.Exists(FullFileName))
                    return FullFileName;
                string path = FullFileName.Substring(0, FullFileName.Length - 4);
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                ZipInputStream s = new ZipInputStream(System.IO.File.OpenRead(FullFileName));
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
            }
        }

        //解压(GZip)
        public static string UnGZip(string FullFileName)
        {
            lock (typeof(File))
            {
                Stream s = new GZipInputStream(System.IO.File.OpenRead(FullFileName));
                string file = Path.GetDirectoryName(FullFileName) + "\\" + Path.GetFileNameWithoutExtension(FullFileName);
                FileStream fs = System.IO.File.Create(file);
                int size = 2048;
                byte[] writeData = new byte[2048];
                while (true)
                {
                    size = s.Read(writeData, 0, size);
                    if (size > 0)
                    {
                        fs.Write(writeData, 0, size);
                    }
                    else
                    {
                        break;
                    }
                }
                s.Close();
                fs.Close();
                return file;
            }
        }
        #endregion
    }
}