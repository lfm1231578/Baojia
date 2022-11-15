using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Drawing;
namespace HS.SupportComponents
{
    public class ImageHelper
    {
        //public static string gif = "image/gif";
        //public static string jpg = "image/pjpeg";
        //public static string png = "image/x-png";
        Page pag = new Page();
        string[] Imgtype = { "image/gif", "image/pjpeg", "image/x-png" };
        public static string thisYear = DateTime.Now.Year.ToString();
        public static string thisMonth = DateTime.Now.Month.ToString();
        public static string thisDay = DateTime.Now.Day.ToString();
        string strDate = thisYear + thisMonth + thisDay + DateTime.Now.Hour.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();

        /// <summary>
        /// 保存上传的文件返回true为保存成功Massage为文件路径,返回false为保存失败Massage错误信息
        /// </summary>
        /// <param name="ProducFile"></param>
        /// <param name="size">允许上传的最大值(以字节为单位)</param>
        /// <param name="addurl">存储路径</param>
        /// <param name="Massage">存储信息</param>
        /// <returns></returns>
        public bool UpImg(System.Web.UI.WebControls.FileUpload ProducFile, int size, string addurl, out string Massage)
        {
            string mag;
            bool su = false;
            int i = ThrowImg(ProducFile, size, out mag);
            if (i == 1)
            {
                //新文件名
                string NewFileName = strDate + ProducFile.FileName.Replace("~", "").Replace("|", "").Replace("&", "");
                string url = addurl;
                string PddUrl = pag.Server.MapPath(url);
                try
                {
                    if (!Directory.Exists(PddUrl))
                    {
                        Directory.CreateDirectory(PddUrl);
                    }
                    if (File.Exists(pag.Server.MapPath(url + NewFileName)) == false)
                    {
                        ProducFile.SaveAs(pag.Server.MapPath(url + NewFileName));
                        Massage = url + NewFileName;
                        su = true;
                    }
                    else
                    {
                        Massage = "上传文件名已经存在,请重新上传!";
                    }

                }
                catch
                {
                    Massage = "文件上传时程序出现异常,请重新上传!";
                }
            }
            else
            {
                Massage = mag;
            }
            return su;

        }


        /// <summary>
        /// 上传图片关生成 缩略图
        /// </summary>
        /// <param name="ProducFile">控件</param>
        /// <param name="size">允许上传的最大值(以字节为单位)</param>
        /// <param name="addurl">存储路径</param>
        /// <param name="frontName">保存文件前缀名集合</param>
        /// <param name="ImgWid">,宽度集合</param>
        /// <param name="ImgHid">高度集合</param>
        /// <param name="Massage">返回上传信息</param>
        /// <returns></returns>
        public bool UpImgAndBreviary(System.Web.UI.WebControls.FileUpload ProducFile, int size, string addurl, string[] frontName, int[] ImgWid, int[] ImgHid, out string Massage)
        {
            string mag;
            bool key = false;
            int i = ThrowImg(ProducFile, size, out mag);
            if (i != 1)
            {
                Massage = mag;
                key = false;


            }
            else if (frontName.Length <= 0 || frontName.Length != ImgWid.Length || frontName.Length != ImgHid.Length)
            {
                Massage = "缩略图信息不相符";
                key = false;
            }
            else
            {
                //新文件名
                string NewFileName = "primary_" + strDate + ProducFile.FileName.Replace("primary_", "");
                //存储路径
                string url = addurl + thisYear + "/" + thisMonth + "/" + thisDay;
                //转为服务器的绝对路径
                string PddUrl = pag.Server.MapPath(url);

                try
                {
                    //判断路径目录是否存在
                    if (!Directory.Exists(PddUrl))
                    {
                        //创建目录
                        Directory.CreateDirectory(PddUrl);
                        if (File.Exists(pag.Server.MapPath(url + "/" + NewFileName)) == false)
                        {
                            //保存文件
                            ProducFile.SaveAs(pag.Server.MapPath(url + "/" + NewFileName));
                            //返回文件地址
                            Massage = url + "/" + NewFileName;

                            //生成缩略图
                            for (int j = 0; j < frontName.Length; j++)
                            {
                                BreviaryImg(Massage, Massage.Replace("primary_", frontName[j]), ImgWid[j], ImgHid[j]);
                            }
                            key = true;

                            //生成两张缩略图,保存名是在原图名前加了一个middle_,small_。
                            //newwidth1 = 200;
                            //newheight1 = 200;
                            //newwidth2 = 60;
                            //newheight2 = 60;
                            //BreviaryImg(Massage, Massage.Replace("primary_", "middle_"), newwidth1, newheight1);
                            //BreviaryImg(Massage, Massage.Replace("primary_", "small_"), newwidth2, newheight2);                       

                        }
                        else
                        {
                            Massage = "上传文件重名,请重新上传!";
                            key = false;
                        }
                    }
                    else
                    {
                        if (File.Exists(pag.Server.MapPath(url + "/" + NewFileName)) == false)
                        {
                            //保存文件
                            ProducFile.SaveAs(pag.Server.MapPath(url + "/" + NewFileName));
                            //返回保存地址
                            Massage = url + "/" + NewFileName;

                            //生成缩略图。
                            for (int j = 0; j < frontName.Length; j++)
                            {
                                BreviaryImg(Massage, Massage.Replace("primary_", frontName[j]), ImgWid[j], ImgHid[j]);
                            }
                            key = true;

                        }
                        else
                        {
                            Massage = "上传文件名已经存在,请重新上传!";
                            key = false;
                        }
                    }
                }
                catch
                {
                    Massage = "文件上传时出现程序错误,请重新上传!";
                    key = false;
                }

            }

            return key;

        }


        /// <summary>
        /// 检测上传文件
        /// </summary>
        /// <param name="ProducFile"></param>
        /// <param name="Size">允许上传最大值</param>
        /// <param name="Massage">检测信息</param>
        /// <returns></returns>    
        private int ThrowImg(System.Web.UI.WebControls.FileUpload ProducFile, int Size, out string Massage)
        {
            int i = 0;

            Boolean ImgOK = false;
            if (ProducFile.HasFile)
            {
                //图片类型集
                string[] ImgExtension = { ".gif", ".jpg", ".jpeg", ".png" };
                //文件类型
                string FileType = ProducFile.PostedFile.ContentType;
                //文件扩展名
                string FileExType = Path.GetExtension(ProducFile.FileName).ToLower();
                //匹配类型
                for (int k = 0; k < ImgExtension.Length; k++)
                {
                    if (FileExType == ImgExtension[k])
                    {
                        foreach (string type in Imgtype)
                        {
                            if (FileType == type)
                            {
                                ImgOK = true;
                                break;
                            }
                        }
                    }
                }
                if (ImgOK == true)
                {
                    //获取文件大小
                    int FileSize = ProducFile.PostedFile.ContentLength;
                    if (FileSize < Size)
                    {
                        i = 1;
                        Massage = "";
                    }
                    else
                    {
                        Massage = "上传文件过大!";
                        i = 0;
                    }
                }
                else
                {
                    Massage = "上传的文件的格式不合法!";
                    i = 0;
                }
            }
            else
            {
                Massage = "请选择正确的文件路径!";
                i = 0;
            }
            return i;

        }


        /// <summary>
        /// 生成缩缩略图
        /// </summary>
        /// <param name="imagePath">原图路径</param>
        /// <param name="savePath">缩略图存储路径</param>
        /// <param name="wid">缩略图宽</param>
        /// <param name="hid">缩略图高</param>
        private void BreviaryImg(string imagePath, string savePath, int wid, int hid)
        {
            string originalImagePath = pag.Server.MapPath(imagePath);
            string thumbnailPath = pag.Server.MapPath(savePath);
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            //int towidth = wid;
            //int toheight = hid;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(wid, hid);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, wid, hid), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);

            try
            {

                //以原格式保存缩略图
                bitmap.Save(thumbnailPath, originalImage.RawFormat);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalImagePath"></param>
        /// <param name="thumbnailPath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mode">"HW"://指定高宽缩放（可能变形）
        /// "W"://指定宽，高按比例  
        /// "H"://指定高，宽按比例
        /// "Cut"://指定高宽裁减（不变形）</param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage =Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }


        public static Image RetrunImage(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                return bitmap;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }


    }

}

