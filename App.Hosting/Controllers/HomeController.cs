using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application;
using App.Application.Blog;
using App.Application.Blog.Dtos;
using App.Application.User;
using App.Core.Entities.Blog;
using App.Core.Entities.User;
using App.Framwork.Result;
using App.Hosting.Extensions;
using App.Hosting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Ubiety.Dns.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using HS.SupportComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HS.SupportComponents;
using System.Text;
using System.Data;
using HS.SupportComponents.Base;
using System.IO;
using System.Data.SqlClient;
using System.Drawing.Printing;
using Google.Protobuf.WellKnownTypes;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Net;
using Microsoft.Graph;
using Spire.Doc;

using Spire.CompoundFile;

using Spire.Doc.Documents;

using System.IO;

using Spire.Doc.Fields;
using Newtonsoft.Json.Linq;
using SqlSugar.Extensions;
using System.Text.Json.Nodes;
using Spire.Pdf.Exporting.XPS.Schema;
using HtmlAgilityPack;
using Spire.Presentation;
using System.Xml;
using System.Reflection.PortableExecutable;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient.Memcached;
using Azure;

namespace App.Hosting.Controllers
{
    public class HomeController : WebController
    {
        private readonly IBannerService _bannerService;
        private readonly IQQUserService _qQUserService;
        private readonly ILeavemsgService _leavemsgService;
        private readonly IArticleService _articleService;
        private readonly INoticeService _noticeService;
        private readonly ITagsService _tagsService;
        private readonly IFriendLinkService _friendLinkService;
        private readonly ITimeLineService _timeLineService;

        public HomeController(IBannerService bannerService,
            IQQUserService qQUserService,
            ILeavemsgService leavemsgService,
            IArticleService articleService,
            INoticeService noticeService,
            ITagsService tagsService,
            IFriendLinkService friendLinkService,
            ITimeLineService timeLineService)
        {
            _bannerService = bannerService;
            _qQUserService = qQUserService;
            _leavemsgService = leavemsgService;
            _articleService = articleService;
            _noticeService = noticeService;
            _tagsService = tagsService;
            _friendLinkService = friendLinkService;
            _timeLineService = timeLineService;
        }

        #region 视图
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(string code, string state, string tag, string key)
        {
            var u = 9;

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.chinadrugtrials.org.cn/clinicaltrials.searchlistdetail.dhtml?_export=doc");

            //Stream postStream = new MemoryStream();
            //request.Method = "POST";
            //string boundary = "----" + DateTime.Now.Ticks.ToString("x");
            //string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";

            //request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            //request.ContentLength = postStream != null ? postStream.Length : 0;
            //request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            //request.KeepAlive = true;
            //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            //Stream requestStream = request.GetRequestStream(); 
            //var response = (HttpWebResponse)request.GetResponse();
            //var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();

            //HttpContext curContext = HttpContext.Current;
            //curContext.Response.ContentType = "application/pdf";//设置类型
            //curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            //curContext.Response.Charset = "";
            //curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename + "." + _filename[_filename.Length - 1], System.Text.Encoding.UTF8));

            //curContext.Response.AddHeader("Content-Length", remoteFileLength.ToString());
            //string _url = string.Format("http://www.chinadrugtrials.org.cn/clinicaltrials.searchlistdetail.dhtml?_export=doc");
            ////json参数
            //string jsonParam = Newtonsoft.Json.JsonConvert.SerializeObject(new
            //{
            //    id = "4a7537df8def48f3b8eabe6f6d44a9b4_p"
            //});
            //var request = (HttpWebRequest)WebRequest.Create(_url);
            //request.Method = "POST";
            //request.ContentType = "application/json;charset=UTF-8";//ContentType
            //byte[] byteData = Encoding.UTF8.GetBytes(jsonParam);
            //int length = byteData.Length;
            //request.ContentLength = length;
            //Stream writer = request.GetRequestStream();
            //writer.Write(byteData, 0, length);
            //writer.Close();
            //var response = (HttpWebResponse)request.GetResponse();
            //var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();


            //        var client = new HttpClient();
            //        var request = new HttpRequestMessage
            //        {
            //            Method = HttpMethod.Post,
            //            RequestUri = new Uri("http://www.chinadrugtrials.org.cn/clinicaltrials.searchlistdetail.dhtml?_export=doc"),
            //            Headers =
            //{
            //    { "id", "4a7537df8def48f3b8eabe6f6d44a9b4_p" },
            //},
            //            Content = new MultipartFormDataContent
            //            {
            //            },
            //        };

            //        using (var response = await client.SendAsync(request))
            //        {
            //            response.EnsureSuccessStatusCode();
            //            var body = await response.Content.ReadAsStringAsync();
            //            Console.WriteLine(body);
            //        }
            var kt = 3;
            //System.out.println(response.body());
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.chinadrugtrials.org.cn/clinicaltrials.searchlistdetail.dhtml?_export=doc");
            //request.Method = "POST";
            ////request.Accept = "text/html, application/xhtml+xml, */*";
            ////request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1599.101 Safari/537.36";
            ////request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentType = "application/json;charset=UTF-8";//ContentType"application/json";
            //string strJson = JsonConvert.SerializeObject(new
            //{
            //    id = "4a7537df8def48f3b8eabe6f6d44a9b4_p"
            //});
            //string paraUrlCoded = strJson;
            //byte[] payload;
            //payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //request.ContentLength = payload.Length;
            //Stream writer;
            //writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            //writer.Write(payload, 0, payload.Length);
            //writer.Close();
            ////HttpWebResponse response;
            //var response = (HttpWebResponse)request.GetResponse();
            //var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
            //Stream responseStream = response.GetResponseStream();
            //Stream stream1 = new FileStream("D:\\88详细信息.doc", FileMode.Create);

            //byte[] bArr = new byte[1024];
            //int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            //while (size > 0)
            //{
            //    stream1.Write(bArr, 0, size);
            //    size = responseStream.Read(bArr, 0, (int)bArr.Length);
            //}
            //stream1.Close();
            //responseStream.Close();
            var ii = 8;
            //string _url = string.Format("http://www.chinadrugtrials.org.cn/clinicaltrials.searchlistdetail.dhtml?_export=doc");
            ////json参数
            //string jsonParam = Newtonsoft.Json.JsonConvert.SerializeObject(new
            //{
            //    id = "4a7537df8def48f3b8eabe6f6d44a9b4_p"
            //});
            //var request = (HttpWebRequest)WebRequest.Create(_url);
            //request.Method = "POST";
            //request.ContentType = "application/json;charset=UTF-8";//ContentType
            ////CookieContainer cc = new CookieContainer();
            ////cc.Add(new System.Uri("https://www.yscro.com"), new Cookie("token", "242b494f-c62d-47ca-807f-c78b6ae314b4"));
            ////request.CookieContainer = cc;
            //byte[] byteData = Encoding.UTF8.GetBytes(jsonParam);
            //int length = byteData.Length;
            //request.ContentLength = length;
            //Stream writer = request.GetRequestStream();
            //writer.Write(byteData, 0, length);
            //writer.Close();
            //var response = (HttpWebResponse)request.GetResponse();
            //var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();

            //HttpWebResponse response1 = (HttpWebResponse)request.GetResponse();

            //string strWebData = string.Empty;
            //Stream responseStream = response1.GetResponseStream();
            ////创建本地文件写入流
            //Stream stream1 = new FileStream("C:\\123.doc", FileMode.Create);

            //byte[] bArr = new byte[1024];
            //int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            //while (size > 0)
            //{
            //    stream1.Write(bArr, 0, size);
            //    size = responseStream.Read(bArr, 0, (int)bArr.Length);
            //}
            //stream1.Close();
            //responseStream.Close();
            var k = 1;
            //var j = new Articleiteminfo();
            //string _url = string.Format("http://www.chinadrugtrials.org.cn/clinicaltrials.searchlist.dhtml?_export=xls");
            ////json参数
            //string jsonParam = Newtonsoft.Json.JsonConvert.SerializeObject(new
            //{
            //    id = "4a7537df8def48f3b8eabe6f6d44a9b4_p"
            //});
            //var request = (HttpWebRequest)WebRequest.Create(_url);
            //request.Method = "POST";
            //request.ContentType = "application/json;charset=UTF-8";//ContentType
            ////CookieContainer cc = new CookieContainer();
            ////cc.Add(new System.Uri("https://www.yscro.com"), new Cookie("token", "242b494f-c62d-47ca-807f-c78b6ae314b4"));
            ////request.CookieContainer = cc;
            //byte[] byteData = Encoding.UTF8.GetBytes(jsonParam);
            //int length = byteData.Length;
            //request.ContentLength = length;
            //Stream writer = request.GetRequestStream();
            //writer.Write(byteData, 0, length);
            //writer.Close();
            //var response = (HttpWebResponse)request.GetResponse();
            //var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();




            //JObject json1 = (JObject)JsonConvert.DeserializeObject(responseString);

            var c = 2;
            // Total':0Rows'
            //string jsonText = "'Total':'0,'Rows':[id:31', project:6,project_name':一卡通,name':接口测试!assignedTo':zhangsan'" +
            //     "realname':张三'estStarted':2016/11/23,realStarted':2000/01/01'status':'wait”," + id:32,project: 6project name: 一卡通name: 测试服务器调通!assignedTo: lisi + "realname':李四,estStarted':2016/11/23realStarted':2016/11/23status':wait]]"; 
            // JObject json1 = (JObject)JsonConvert.DeserializeObject(jsonText);
            // JArray array = (JArray)json1["Rows"];
            //  int i = array.Count;
            // string aa = "";
            // foreach (var jObject in array)
            // {
            //     //赋值属性
            //     aa = jObject["id"].ToString();
            // }
            string shenfen = key;
            //加载Word文档

            //Spire.Doc.Document doc = new Spire.Doc.Document();
            //doc.LoadFromFile("D:/Work/Software/EYB-备份/EYB/EYBWeb/EYBWeb/Uploads/Input.docx");

            //#region 有效
            //string _Sstring = "select hid FROM (\r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_beijing] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_tianjing] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_hebei] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_shangxi] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_neimenggu] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_liaoning] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_jiling] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_heilongjiang] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_shanghai] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_jiangshu] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_zhejiang] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_anhui] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_fujian] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_jiangxi] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_shangdong] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_henan] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_hubei] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_hunan] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_guangdong] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_guangxi] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_hainan] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_chongqi] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_sichuang] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_guizhou] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_yunnan] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_xizan] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_sanxi] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_gansu] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_qinghai] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_ningxia] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_xinjiang] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_xianggang] UNION \r\nSELECT REPLACE(REPLACE(Syurl, 'https://www.yscro.com/org/', ''),'.html','') as hid , Syname  FROM [dbo].[AirtleTable_taiwang]\r\n) AS C  ORDER BY Syname";
            //var list1 = new List<Articleiteminfo>();
            //string conStr = "server=192.168.10.28;user=sa;pwd=123456;database=Blog";//连接字符串  
            //SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            //conText.Open();//打开数据库  
            //string sqls = _Sstring;//创建统计语句  
            //SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            //SqlDataReader dr;//创建DataReader对象  
            //dr = comText.ExecuteReader();//执行查询  
            //while (dr.Read())//判断数据表中是否含有数据  
            //{
            //    var i = new Articleiteminfo();
            //    var date = dr;
            //    i.Hospitalid = date["hid"].ToString();

            //    list1.Add(i);
            //}
            //dr.Close();//关闭DataReader对象  

            //var list2 = new List<Articleiteminfo>();
            //int re = 0;
            //foreach (var i in list1)
            //{
            //    try
            //    {
            //        var hospitalid = i.Hospitalid == "" ? "9999" : i.Hospitalid;
            //        var j = new Articleiteminfo();
            //        string _url = string.Format("https://www.yscro.com/api/organization/cdeorg");
            //        //json参数
            //        string jsonParam = Newtonsoft.Json.JsonConvert.SerializeObject(new
            //        {
            //            test_range = 1,//用戶openid
            //            page = 1,
            //            limit = 10,
            //            orgid = i.Hospitalid
            //        });
            //        var request = (HttpWebRequest)WebRequest.Create(_url);
            //        request.Method = "POST";
            //        request.ContentType = "application/json;charset=UTF-8";//ContentType
            //        CookieContainer cc = new CookieContainer();
            //        cc.Add(new System.Uri("https://www.yscro.com"), new Cookie("token", "242b494f-c62d-47ca-807f-c78b6ae314b4"));
            //        request.CookieContainer = cc;
            //        byte[] byteData = Encoding.UTF8.GetBytes(jsonParam);
            //        int length = byteData.Length;
            //        request.ContentLength = length;
            //        Stream writer = request.GetRequestStream();
            //        writer.Write(byteData, 0, length);
            //        writer.Close();
            //        var response = (HttpWebResponse)request.GetResponse();
            //        var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();




            //        JObject json1 = (JObject)JsonConvert.DeserializeObject(responseString);



            //        JObject jsonObj = (JObject)JsonConvert.DeserializeObject(responseString);

            //        JObject jo = JObject.Parse(responseString);

            //        //j.Hospitalid_Cra5 
            //        if (jo["data"]["list"].Count() > 0)
            //        {
            //            JArray array = (JArray)json1["data"]["list"];
            //            int iiii = array.Count;
            //            string aa = "";
            //            foreach (var jObject in array)
            //            {

            //                var make = new Makeiteminfo();
            //                //赋值属性
            //                aa = jObject["ctr_md5_id"].ToString();
            //                string strURL = "https://www.yscro.com/cde/" + aa;
            //                //string strURL = "http://www.chinadrugtrials.org.cn/clinicaltrials.searchlistdetail.dhtml?keywords=CTR20170414";
            //                System.Net.HttpWebRequest request1;
            //                request1 = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            //                CookieContainer cc1 = new CookieContainer();
            //                cc1.Add(new System.Uri("https://www.yscro.com"), new Cookie("token", "242b494f-c62d-47ca-807f-c78b6ae314b4"));
            //                request1.CookieContainer = cc1;
            //                request1.Method = "get";
            //                System.Net.HttpWebResponse response1;
            //                response1 = (System.Net.HttpWebResponse)request1.GetResponse();
            //                System.IO.StreamReader myreader = new System.IO.StreamReader(response1.GetResponseStream(), Encoding.UTF8);
            //                string responseText1 = myreader.ReadToEnd();
            //                //if (response1.StatusCode == "OK")
            //                //{
            //                //Console.WriteLine(responseText1);
            //                var htmlDoc = new HtmlDocument();
            //                htmlDoc.LoadHtml(responseText1);

            //                var name = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='col-xs-12 col-md-10']/div[1]/div[1]/h3[1]").InnerText.Trim().ToString().Split("｜")[0].ToString();
            //                var stage = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='col-xs-12 col-md-10']/div[1]/div[1]/h3[1]").InnerText.Trim().ToString().Split("｜")[1].ToString();
            //                if (htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[1]/div[2]") != null)
            //                {
            //                    var strindext = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[1]/div[2]").InnerText;
            //                    var arrlist = strindext.Split("\n                    ");
            //                    make.JBXXdengjihao = arrlist[3].ToString();
            //                    make.JBXXxiangguandengjihao = arrlist[5].ToString();
            //                    make.JBXXchengyongming = arrlist[7].ToString();
            //                    make.JBXXyaowuleixin = arrlist[10].ToString();
            //                    make.JBXXlinchuangshoushenqing = arrlist[12].ToString();
            //                    make.JBXXshiyingzheng = arrlist[14].ToString();
            //                    make.JBXXshiyantongshutimu = arrlist[16].ToString();
            //                    make.JBXXshiyanzhuanyetimu = arrlist[18].ToString();
            //                    make.JBXXshiyanfanganbianhao = arrlist[20].ToString();
            //                    make.JBXXfanganzuijing = arrlist[22].ToString();
            //                    make.JBXXbanbenriqi = arrlist[24].ToString();
            //                    make.JBXXfanganshifou = arrlist[26].ToString();
            //                }
            //                else
            //                {
            //                    make.JBXXdengjihao = "";
            //                    make.JBXXxiangguandengjihao = "";
            //                    make.JBXXchengyongming = "";
            //                    make.JBXXyaowuleixin = "";
            //                    make.JBXXlinchuangshoushenqing = "";
            //                    make.JBXXshiyingzheng = "";
            //                    make.JBXXshiyantongshutimu = "";
            //                    make.JBXXshiyanzhuanyetimu = "";
            //                    make.JBXXshiyanfanganbianhao = "";
            //                    make.JBXXfanganzuijing = "";
            //                    make.JBXXbanbenriqi = "";
            //                    make.JBXXfanganshifou = "";
            //                }

            //                if (htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[2]/div[2]") != null)
            //                {
            //                    var strindextshenqingrenxinxi = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[2]/div[2]").InnerText;
            //                    var _strindextshenqingrenxinxi = strindextshenqingrenxinxi.Split("\n                    ");
            //                    make.SQRXXshengqingrenmingcheng = _strindextshenqingrenxinxi[3].ToString();
            //                    make.SQRXXlianxirenxingming = _strindextshenqingrenxinxi[5].ToString();
            //                    make.SQRXXlianxirenzuoji = _strindextshenqingrenxinxi[7].ToString();
            //                    make.SQRXXlianxirenshoujihao = _strindextshenqingrenxinxi[9].ToString();
            //                    make.SQRXXlianxirenemail = _strindextshenqingrenxinxi[11].ToString();
            //                    make.SQRXXlianxirenyouzhendizhi = _strindextshenqingrenxinxi[13].ToString();
            //                    make.SQRXXlianxirenyoubian = _strindextshenqingrenxinxi[15].ToString();
            //                }
            //                else
            //                {
            //                    make.SQRXXshengqingrenmingcheng = "";
            //                    make.SQRXXlianxirenxingming = "";
            //                    make.SQRXXlianxirenzuoji = "";
            //                    make.SQRXXlianxirenshoujihao = "";
            //                    make.SQRXXlianxirenemail = "";
            //                    make.SQRXXlianxirenyouzhendizhi = "";
            //                    make.SQRXXlianxirenyoubian = "";
            //                }
            //                string biaozhun1 = "";
            //                string biaozhunValue = "";

            //                string paichubiazhun = "", paichubiazhunValue = "";
            //                if (htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[3]/div[2]") != null)
            //                {
            //                    string strSyfenlie = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[3]/div[2]").InnerText;
            //                    var _strSyfenlie = strSyfenlie.Split("\n                    ");
            //                    make.LCSYXXshiyanfenlie = _strSyfenlie[4].ToString();
            //                    make.LCSYXXshiyanfenqi = _strSyfenlie[7].ToString();
            //                    make.LCSYXXshiyanmudi = _strSyfenlie[9].ToString();
            //                    make.LCSYXXshuijihua = _strSyfenlie[12].ToString();
            //                    make.LCSYXXmanfang = _strSyfenlie[15].ToString();
            //                    make.LCSYXXshiyanfangwei = _strSyfenlie[18].ToString();
            //                    make.LCSYXXshejiliexing = _strSyfenlie[21].ToString();
            //                    make.LCSYXXninaliang = _strSyfenlie[23].ToString();
            //                    make.LCSYXXxingbei = _strSyfenlie[25].ToString();
            //                    make.LCSYXXjiankuangshoushizhen = _strSyfenlie[28].ToString();

            //                    var strSyfenlieruxuanbiaozhun = strSyfenlie.Split("\n                                入选标准")[1].Split("\n                                排除标准")[0].Split("\n\t\t\t\t\t\t\t\t\t\t\t\n                                                                                \n\t\t\t\t\t\t\t\t\t\t\t");
            //                    var strSyfenliepaichubiaozhunbiaozhun = strSyfenlie.Split("\n                                入选标准")[1].Split("\n                                排除标准")[1].Split("\n\t\t\t\t\t\t\t\t\t\t\n                                                                                \n\t\t\t\t\t\t\t\t\t\t");

            //                    biaozhun1 = "";
            //                    biaozhunValue = "";
            //                    for (int ixp = 0; ixp < strSyfenlieruxuanbiaozhun.Count(); ixp++)
            //                    {
            //                        if (ixp < 21)
            //                        {
            //                            string strixp = ixp.ToString() == "0" ? "" : ixp.ToString();
            //                            biaozhun1 += ",LCSYXXruxuanbiaozhun" + strixp;
            //                            biaozhunValue += ",'" + strSyfenlieruxuanbiaozhun[ixp] + "'";
            //                        }
            //                        else { break; }

            //                    }

            //                    for (int ixp = 0; ixp < strSyfenliepaichubiaozhunbiaozhun.Count(); ixp++)
            //                    {
            //                        if (ixp < 21)
            //                        {
            //                            string strixp = ixp.ToString() == "0" ? "" : ixp.ToString();
            //                            paichubiazhun += ",LCSYXXpaichubiaozhun" + strixp;
            //                            paichubiazhunValue += ",'" + strSyfenliepaichubiaozhunbiaozhun[ixp] + "'";
            //                        }
            //                        else { break; }

            //                    }

            //                }
            //                else
            //                {
            //                    make.LCSYXXshiyanfenlie = "";
            //                    make.LCSYXXshiyanfenqi = "";
            //                    make.LCSYXXshiyanmudi = "";
            //                    make.LCSYXXshuijihua = "";
            //                    make.LCSYXXmanfang = "";
            //                    make.LCSYXXshiyanfangwei = "";
            //                    make.LCSYXXshejiliexing = "";
            //                    make.LCSYXXninaliang = "";
            //                    make.LCSYXXxingbei = "";
            //                    make.LCSYXXjiankuangshoushizhen = "";
            //                }

            //                //var strSyfenlieruxuanbiaozhun = strSyfenlie.Split("\n                                入选标准")[1].Split("\n                                排除标准")[0].Split("\n\t\t\t\t\t\t\t\t\t\t\t\n                                                                                \n\t\t\t\t\t\t\t\t\t\t\t");
            //                //var strSyfenliepaichubiaozhunbiaozhun = strSyfenlie.Split("\n                                入选标准")[1].Split("\n                                排除标准")[1].Split("\n\t\t\t\t\t\t\t\t\t\t\n                                                                                \n\t\t\t\t\t\t\t\t\t\t");

            //                //var StrSYfenzhu = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[4]/div[2]").InnerText;
            //                var strshiyanyao = "";

            //                if (htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[4]/div[2]/div[1]/div[2]/table[1]/tbody[1]/tr[1]") != null)
            //                {
            //                    strshiyanyao = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[4]/div[2]/div[1]/div[2]/table[1]/tbody[1]/tr[1]").InnerText;
            //                }
            //                else
            //                {
            //                    strshiyanyao = @"\n                                            \n                                                \n\t\t\t\t\t\t\t\t\t\t\t\t\t  \n                                            \n                                            \n                                                \n\t\t\t\t\t\t\t\t\t\t\t\t\t  ";
            //                }
            //                var _strshiyanyao = strshiyanyao.Split("\n                                            \n                                                \n\t\t\t\t\t\t\t\t\t\t\t\t\t");
            //                var strduizhaoyao = "";
            //                if (htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[4]/div[2]/div[1]/div[4]/table[1]/tbody[1]/tr[1]") != null)
            //                {
            //                    strduizhaoyao = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[4]/div[2]/div[1]/div[4]/table[1]/tbody[1]/tr[1]").InnerText;
            //                }
            //                else
            //                {
            //                    strduizhaoyao = @"\n                                            \n                                                \n\t\t\t\t\t\t\t\t\t\t\t\t\t  \n                                            \n                                            \n                                                \n\t\t\t\t\t\t\t\t\t\t\t\t\t  ";
            //                }
            //                var _strduizhaoyao = strduizhaoyao.Split("\n                                            \n                                                \n\t\t\t\t\t\t\t\t\t\t\t\t\t");

            //                var strZDZB = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[5]/div[2]").InnerText;
            //                string[] strZYZBZD;
            //                if (htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[5]/div[2]/div[1]/div[2]/table[1]/tbody[1]/tr[1]") != null)
            //                {
            //                    strZYZBZD = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[5]/div[2]/div[1]/div[2]/table[1]/tbody[1]/tr[1]").InnerText.Split("\n                                            ");
            //                }
            //                else
            //                {
            //                    strZYZBZD = "\n                                            \n                                            \n                                            \n                                            ".Split("\n                                            ");
            //                }
            //                var strCYdzd = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[5]/div[2]/div[1]/div[4]/table[1]/tbody[1]").SelectNodes("tr");
            //                var strCYdzdnode = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[5]/div[2]/div[1]/div[4]/table[1]/tbody[1]").SelectNodes("tr[1]/td");
            //                var strCYDZB = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[5]/div[2]/div[1]/div[4]/table[1]/tbody[1]/tr") != null ? htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[5]/div[2]/div[1]/div[4]/table[1]/tbody[1]/tr").InnerText : "\n                    \n                    \n                    ";
            //                var _strZDZB = strZDZB.Split("\n                    ");
            //                //var strYJZXX = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[6]/div[2]").InnerText;
            //                var arryjzxx = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[6]/div[2]/table[1]/tbody[1]").SelectNodes("tr");
            //                //var _strYJZXX = strYJZXX.Split("\n                    ");
            //                //var strGCJJJ = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[7]/div[2]").InnerText;
            //                var arrgcjj = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[7]/div[2]/table[1]/tbody[1]").SelectNodes("tr");
            //                //var _strGCJJJ = strGCJJJ.Split("\n                    ");
            //                var LLWYH = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[8]/div[2]/table[1]/tbody[1]").SelectNodes("tr");
            //                //var _LLWYH = LLWYH.Split("\n                    ");
            //                var SYZT = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[9]/div[2]/div[1]").SelectNodes("div");
            //                //var _SYZT = SYZT.Split("\n                    ");
            //                //var LCSYJG = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[10]/div[2]").InnerText;
            //                //var _LCSYJG = LCSYJG.Split("\n                    ");

            //                //make.JBXXdengjihao = arrlist[3].ToString();
            //                //make.JBXXxiangguandengjihao = arrlist[5].ToString();
            //                //make.JBXXchengyongming = arrlist[7].ToString();
            //                //make.JBXXyaowuleixin = arrlist[10].ToString();
            //                //make.JBXXlinchuangshoushenqing = arrlist[12].ToString();
            //                //make.JBXXshiyingzheng = arrlist[14].ToString();
            //                //make.JBXXshiyantongshutimu = arrlist[16].ToString();
            //                //make.JBXXshiyanzhuanyetimu = arrlist[18].ToString();
            //                //make.JBXXshiyanfanganbianhao = arrlist[20].ToString();
            //                //make.JBXXfanganzuijing = arrlist[22].ToString();
            //                //make.JBXXbanbenriqi = arrlist[24].ToString();
            //                //make.JBXXfanganshifou = arrlist[26].ToString();

            //                //make.SQRXXshengqingrenmingcheng = _strindextshenqingrenxinxi[3].ToString();
            //                //make.SQRXXlianxirenxingming = _strindextshenqingrenxinxi[5].ToString();
            //                //make.SQRXXlianxirenzuoji = _strindextshenqingrenxinxi[7].ToString();
            //                //make.SQRXXlianxirenshoujihao = _strindextshenqingrenxinxi[9].ToString();
            //                //make.SQRXXlianxirenemail = _strindextshenqingrenxinxi[11].ToString();
            //                //make.SQRXXlianxirenyouzhendizhi = _strindextshenqingrenxinxi[13].ToString();
            //                //make.SQRXXlianxirenyoubian = _strindextshenqingrenxinxi[15].ToString();

            //                //make.LCSYXXshiyanfenlie = _strSyfenlie[4].ToString();
            //                //make.LCSYXXshiyanfenqi = _strSyfenlie[7].ToString();
            //                //make.LCSYXXshiyanmudi = _strSyfenlie[9].ToString();
            //                //make.LCSYXXshuijihua = _strSyfenlie[12].ToString();
            //                //make.LCSYXXmanfang = _strSyfenlie[15].ToString();
            //                //make.LCSYXXshiyanfangwei = _strSyfenlie[18].ToString();
            //                //make.LCSYXXshejiliexing = _strSyfenlie[21].ToString();
            //                //make.LCSYXXninaliang = _strSyfenlie[23].ToString();
            //                //make.LCSYXXxingbei = _strSyfenlie[25].ToString();
            //                //make.LCSYXXjiankuangshoushizhen = _strSyfenlie[28].ToString();


            //                string strsql = @"INSERT INTO MakeTable (JBXXdengjihao
	           //                         , JBXXxiangguandengjihao
	           //                         , JBXXchengyongming
	           //                         , JBXXyaowuleixin
	           //                         , JBXXlinchuangshoushenqing
	           //                         , JBXXshiyingzheng
	           //                         , JBXXshiyantongshutimu
	           //                         , JBXXshiyanzhuanyetimu
	           //                         , JBXXshiyanfanganbianhao
	           //                         , JBXXfanganzuijing
	           //                         , JBXXbanbenriqi
	           //                         , JBXXfanganshifou
	           //                         , SQRXXshengqingrenmingcheng
	           //                         , SQRXXlianxirenxingming
	           //                         , SQRXXlianxirenzuoji
	           //                         , SQRXXlianxirenshoujihao
	           //                         , SQRXXlianxirenemail
	           //                         , SQRXXlianxirenyouzhendizhi
	           //                         , SQRXXlianxirenyoubian
	           //                         , LCSYXXshiyanfenlie
	           //                         , LCSYXXshiyanfenqi
	           //                         , LCSYXXshiyanmudi
	           //                         , LCSYXXshuijihua
	           //                         , LCSYXXmanfang
	           //                         , LCSYXXshiyanfangwei
	           //                         , LCSYXXshejiliexing
	           //                         , LCSYXXninaliang
	           //                         , LCSYXXxingbei
	           //                         , LCSYXXjiankuangshoushizhen";
            //                string slqint1 = @"INSERT INTO MakeTable (";
            //                string slqint11 = @"JBXXdengjihao
	           //                         , JBXXxiangguandengjihao
	           //                         , JBXXchengyongming
	           //                         , JBXXyaowuleixin
	           //                         , JBXXlinchuangshoushenqing
	           //                         , JBXXshiyingzheng
	           //                         , JBXXshiyantongshutimu
	           //                         , JBXXshiyanzhuanyetimu
	           //                         , JBXXshiyanfanganbianhao
	           //                         , JBXXfanganzuijing
	           //                         , JBXXbanbenriqi
	           //                         , JBXXfanganshifou
	           //                         , SQRXXshengqingrenmingcheng
	           //                         , SQRXXlianxirenxingming
	           //                         , SQRXXlianxirenzuoji
	           //                         , SQRXXlianxirenshoujihao
	           //                         , SQRXXlianxirenemail
	           //                         , SQRXXlianxirenyouzhendizhi
	           //                         , SQRXXlianxirenyoubian
	           //                         , LCSYXXshiyanfenlie
	           //                         , LCSYXXshiyanfenqi
	           //                         , LCSYXXshiyanmudi
	           //                         , LCSYXXshuijihua
	           //                         , LCSYXXmanfang
	           //                         , LCSYXXshiyanfangwei
	           //                         , LCSYXXshejiliexing
	           //                         , LCSYXXninaliang
	           //                         , LCSYXXxingbei
	           //                         , LCSYXXjiankuangshoushizhen";
            //                string slqint12 = @") VALUES ( ";
            //                //string biaozhun1 = "";
            //                string strsqlvalue = "";

            //                //string biaozhunValue = "";
            //                //for (int ixp = 0; ixp < strSyfenlieruxuanbiaozhun.Count() ; ixp++)
            //                //{
            //                //    if (ixp < 21)
            //                //    {
            //                //        string strixp = ixp.ToString() == "0" ? "": ixp.ToString();
            //                //        biaozhun1 += ",LCSYXXruxuanbiaozhun" + strixp;
            //                //        biaozhunValue += ",'" + strSyfenlieruxuanbiaozhun[ixp] + "'";
            //                //    }
            //                //    else { break; }

            //                //}
            //                strsql += biaozhun1;
            //                strsqlvalue += biaozhunValue;
            //                //string paichubiazhun = "", paichubiazhunValue ="";

            //                //for (int ixp = 0; ixp < strSyfenliepaichubiaozhunbiaozhun.Count(); ixp++)
            //                //{
            //                //    if (ixp < 21)
            //                //    {
            //                //        string strixp = ixp.ToString() == "0" ? "" : ixp.ToString();
            //                //        paichubiazhun += ",LCSYXXpaichubiaozhun" + strixp;
            //                //        paichubiazhunValue += ",'" + strSyfenliepaichubiaozhunbiaozhun[ixp] + "'";
            //                //    }
            //                //    else { break; }

            //                //}

            //                strsql += paichubiazhun;
            //                strsqlvalue += paichubiazhunValue;
            //                string zdzbcyzb = "", zdzbcyzbvalue = "", zdzbcyzb1 = "", zdzbcyzbvalue1 = "", zdzbcyzb2 = "", zdzbcyzbvalue2 = "";
            //                for (var izb = 0; izb < strCYdzd.Count(); izb++)
            //                {

            //                    if (izb < 6)
            //                    {
            //                        string strixp = izb.ToString() == "0" ? "" : izb.ToString();
            //                        int jzb = izb + 1;
            //                        zdzbcyzb += ",ZDZBCYZDZBzhibiao" + strixp;
            //                        zdzbcyzb1 += ",ZDZBCYZDZBpingjiashijian" + strixp;
            //                        zdzbcyzb2 += ",ZDZBCYZDZBzongdianzhibiaoxuanzhe" + strixp;
            //                        //zdzbcyzbvalue += ",'" + strSyfenliepaichubiaozhunbiaozhun[izb] + "'";
            //                        var OItemZB = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[5]/div[2]/div[1]/div[4]/table[1]/tbody[1]").SelectNodes("tr[" + jzb + "]/td");
            //                        zdzbcyzbvalue += ",'" + OItemZB[0].InnerText + "'";
            //                        zdzbcyzbvalue1 += ",'" + OItemZB[1].InnerText + "'";
            //                        zdzbcyzbvalue2 += ",'" + OItemZB[2].InnerText + "'";
            //                    }
            //                    else { break; }
            //                }

            //                string yj = "", yj1 = "", yj2 = "", yj3 = "", yj4 = "", yj5 = "", yj6 = "", yj7 = "", yj8 = "",
            //                    yjv = "", yjv1 = "", yjv2 = "", yjv3 = "", yjv4 = "", yjv5 = "", yjv6 = "", yjv7 = "", yjv8 = "";
            //                for (var izb = 0; izb < arryjzxx.Count(); izb++)
            //                {

            //                    if (izb < 2)
            //                    {
            //                        string strixp = izb.ToString() == "0" ? "" : izb.ToString();
            //                        int jzb = izb + 1;
            //                        yj += ",YJZXXxingming" + strixp;
            //                        yj1 += ",YJZXXxuewei" + strixp;
            //                        yj2 += ",YJZXXzhicheng" + strixp;
            //                        yj3 += ",YJZXXdianhuan" + strixp;
            //                        yj4 += ",YJZXXemail" + strixp;
            //                        yj5 += ",YJZXXyouzhenbiaoma" + strixp;
            //                        yj6 += ",YJZXXyoubian" + strixp;
            //                        yj7 += ",YJZXXdangweimingcheng" + strixp;

            //                        // var arryjzxx = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[6]/div[2]/table[1]/tbody[1]").SelectNodes("tr");
            //                        var OItemZB = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[6]/div[2]/table[1]/tbody[1]").SelectNodes("tr[" + jzb + "]/td");
            //                        yjv += ",'" + OItemZB[0].InnerText + "'";
            //                        yjv1 += ",'" + OItemZB[1].InnerText + "'";
            //                        yjv2 += ",'" + OItemZB[2].InnerText + "'";
            //                        yjv3 += ",'" + OItemZB[3].InnerText + "'";
            //                        yjv4 += ",'" + OItemZB[4].InnerText + "'";
            //                        yjv5 += ",'" + OItemZB[5].InnerText + "'";
            //                        yjv6 += ",'" + OItemZB[6].InnerText + "'";
            //                        yjv7 += ",'" + OItemZB[7].InnerText + "'";
            //                    }
            //                    else { break; }
            //                }
            //                string gc = "", gc1 = "", gc2 = "", gc3 = "", gc4 = "",
            //                       gcv = "", gcv1 = "", gcv2 = "", gcv3 = "", gcv4 = "";
            //                for (var izb = 0; izb < arrgcjj.Count(); izb++)
            //                {
            //                    if (izb < 31)
            //                    {
            //                        string strixp = izb.ToString() == "0" ? "" : izb.ToString();
            //                        int jzb = izb + 1;
            //                        gc += ",GCJJGmingcheng" + strixp;
            //                        gc1 += ",GCJJGzhuyaoyanjiuzhe" + strixp;
            //                        gc2 += ",GCJJGguojia" + strixp;
            //                        gc3 += ",GCJJGdiqu" + strixp;
            //                        gc4 += ",GCJJGchengshi" + strixp;

            //                        // var arryjzxx = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[6]/div[2]/table[1]/tbody[1]").SelectNodes("tr");
            //                        var OItemZB = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[7]/div[2]/table[1]/tbody[1]").SelectNodes("tr[" + jzb + "]/td");
            //                        gcv += ",'" + OItemZB[0].InnerText + "'";
            //                        gcv1 += ",'" + OItemZB[1].InnerText + "'";
            //                        gcv2 += ",'" + OItemZB[2].InnerText + "'";
            //                        gcv3 += ",'" + OItemZB[3].InnerText + "'";
            //                        gcv4 += ",'" + OItemZB[4].InnerText + "'";
            //                    }
            //                    else { break; }
            //                }
            //                string ll = "", ll1 = "", ll2 = "",
            //                     llv = "", llv1 = "", llv2 = "";
            //                for (var izb = 1; izb < LLWYH.Count() + 1; izb++)
            //                {
            //                    if (izb < 6)
            //                    {
            //                        string strixp = izb.ToString() == "0" ? "" : izb.ToString();
            //                        int jzb = izb;
            //                        ll += ",LLWYHmingcheng" + strixp;
            //                        ll1 += ",LLWYHshechajiruan" + strixp;
            //                        ll2 += ",LLWYHchachariqi" + strixp;

            //                        // var arryjzxx = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[6]/div[2]/table[1]/tbody[1]").SelectNodes("tr");
            //                        var OItemZB = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[8]/div[2]/table[1]/tbody[1]").SelectNodes("tr[" + jzb + "]/td");
            //                        llv += ",'" + OItemZB[0].InnerText.Trim().ToString() + "'";
            //                        llv1 += ",'" + OItemZB[1].InnerText.Trim().ToString() + "'";
            //                        llv2 += ",'" + OItemZB[2].InnerText.Trim().ToString() + "'";
            //                    }
            //                    else { break; }
            //                }
            //                string lla = ll + ll1 + ll2;
            //                string llva = llv + llv1 + llv2;
            //                string gca = gc + gc1 + gc2 + gc3 + gc4;
            //                string gcva = gcv + gcv1 + gcv2 + gcv3 + gcv4;
            //                string yja = yj + yj1 + yj2 + yj3 + yj4 + yj5 + yj6 + yj7;
            //                string yjva = yjv + yjv1 + yjv2 + yjv3 + yjv4 + yjv5 + yjv6 + yjv7;
            //                string stringZB = zdzbcyzb + zdzbcyzb1 + zdzbcyzb2;
            //                string stringzbvalue = zdzbcyzbvalue + zdzbcyzbvalue1 + zdzbcyzbvalue2;

            //                string aaa = "";
            //                string biaoben = @"
	           //                         , SYFZshiyanyaomingcheng
	           //                         , SYFZshiyanyaoyongfa
	           //                         , SYFZduizhaomyaomingcheng
	           //                         , SYFZduizhaoyaoyongfa
	           //                         , ZDZBZDZBzhibiao
	           //                         , ZDZBZDZBzhibiao1
	           //                         , ZDZBZDZBzhibiao2
	           //                         , ZDZBZDZBpingjiashijian
	           //                         , ZDZBZDZBpingjiashijian1
	           //                         , ZDZBZDZBpingjiashijian2
	           //                         , ZDZBZDZBzongdianzhibiaoxuanzhe
	           //                         , ZDZBZDZBzongdianzhibiaoxuanzhe1
	           //                         , ZDZBZDZBzongdianzhibiaoxuanzhe2";

            //                strsql += biaoben;
            //                strsql += stringZB;
            //                string strzb = @", ZDZBshujuanquanjianchaweiyuanhui
	           //                         , ZDZBweishouzhezheguomaishiyan";
            //                strsql += strzb;
            //                strsql += yja;
            //                strsql += gca;
            //                strsql += lla;
            //                string systr = strsql + @", SYZTXXshiyanzhuantai
	           //                         , SYZTXXmubiaoruzhurenshu
	           //                         , SYZTXXyiruzhulishu
	           //                         , SYZTXXshijiruzhuzonglishu
	           //                         , SYZTXXdiyilieshoushizheqianshu
	           //                         , SYZTXXdiyilieshoushizheruzhuriqi
	           //                         , SYZTXXshiyanzongzhiriqi
	           //                         , LCSYJGZYbanbenhao
	           //                         , LCSYJGZYbanbenriqi
	           //                         , Other
	           //                         , TitleName
            //                            , TitleStage) VALUES ( ";

            //                #region
            //                string strvule =
            //                               @" '" + make.JBXXdengjihao + @"'
	           //                         , ' " + make.JBXXxiangguandengjihao + @"'
	           //                         , ' " + make.JBXXchengyongming + @"'
	           //                         , ' " + make.JBXXyaowuleixin + @"'
	           //                         , ' " + make.JBXXlinchuangshoushenqing + @"'
	           //                         , ' " + make.JBXXshiyingzheng + @"'
	           //                         , ' " + make.JBXXshiyantongshutimu + @"'
	           //                         , ' " + make.JBXXshiyanzhuanyetimu + @"'
	           //                         , ' " + make.JBXXshiyanfanganbianhao + @"'
	           //                         , ' " + make.JBXXfanganzuijing + @"'
	           //                         , ' " + make.JBXXbanbenriqi + @"'
	           //                         , ' " + make.JBXXfanganshifou + @"'
	           //                         , ' " + make.SQRXXshengqingrenmingcheng + @"'
	           //                         , ' " + make.SQRXXlianxirenxingming + @"'
	           //                         , ' " + make.SQRXXlianxirenzuoji + @"'
	           //                         , ' " + make.SQRXXlianxirenshoujihao + @"'
	           //                         , ' " + make.SQRXXlianxirenemail + @"'
	           //                         , ' " + make.SQRXXlianxirenyouzhendizhi + @"'
	           //                         , ' " + make.SQRXXlianxirenyoubian + @"'
	           //                         , ' " + make.LCSYXXshiyanfenlie + @"'
	           //                         , ' " + make.LCSYXXshiyanfenqi + @"'
	           //                         , ' " + make.LCSYXXshiyanmudi + @"'
	           //                         , ' " + make.LCSYXXshuijihua + @"'
	           //                         , ' " + make.LCSYXXmanfang + @"'
	           //                         , ' " + make.LCSYXXshiyanfangwei + @"'
	           //                         , ' " + make.LCSYXXshejiliexing + @"'
	           //                         , ' " + make.LCSYXXninaliang + @"'
	           //                         , ' " + make.LCSYXXxingbei + @"'
	           //                         , ' " + make.LCSYXXjiankuangshoushizhen + @"'";
            //                string slqint13 = @" '" + make.JBXXdengjihao + @"'
	           //                         , ' " + make.JBXXxiangguandengjihao + @"'
	           //                         , ' " + make.JBXXchengyongming + @"'
	           //                         , ' " + make.JBXXyaowuleixin + @"'
	           //                         , ' " + make.JBXXlinchuangshoushenqing + @"'
	           //                         , ' " + make.JBXXshiyingzheng + @"'
	           //                         , ' " + make.JBXXshiyantongshutimu + @"'
	           //                         , ' " + make.JBXXshiyanzhuanyetimu + @"'
	           //                         , ' " + make.JBXXshiyanfanganbianhao + @"'
	           //                         , ' " + make.JBXXfanganzuijing + @"'
	           //                         , ' " + make.JBXXbanbenriqi + @"'
	           //                         , ' " + make.JBXXfanganshifou + @"'
	           //                         , ' " + make.SQRXXshengqingrenmingcheng + @"'
	           //                         , ' " + make.SQRXXlianxirenxingming + @"'
	           //                         , ' " + make.SQRXXlianxirenzuoji + @"'
	           //                         , ' " + make.SQRXXlianxirenshoujihao + @"'
	           //                         , ' " + make.SQRXXlianxirenemail + @"'
	           //                         , ' " + make.SQRXXlianxirenyouzhendizhi + @"'
	           //                         , ' " + make.SQRXXlianxirenyoubian + @"'
	           //                         , ' " + make.LCSYXXshiyanfenlie + @"'
	           //                         , ' " + make.LCSYXXshiyanfenqi + @"'
	           //                         , ' " + make.LCSYXXshiyanmudi + @"'
	           //                         , ' " + make.LCSYXXshuijihua + @"'
	           //                         , ' " + make.LCSYXXmanfang + @"'
	           //                         , ' " + make.LCSYXXshiyanfangwei + @"'
	           //                         , ' " + make.LCSYXXshejiliexing + @"'
	           //                         , ' " + make.LCSYXXninaliang + @"'
	           //                         , ' " + make.LCSYXXxingbei + @"'
	           //                         , ' " + make.LCSYXXjiankuangshoushizhen + @"'";
            //                string slqint14 = ");";
            //                #endregion
            //                strvule = strvule + strsqlvalue;

            //                string str2 = @"
            //                        , '" + _strshiyanyao[1] + @"'
            //                        , '" + _strshiyanyao[2] + @"'
            //                        , '" + _strduizhaoyao[1] + @"'
            //                        , '" + _strduizhaoyao[2] + @"'
            //                        , '" + strZYZBZD[1] + @"'
            //                        , 'ZDZBZDZBzhibiao1'
            //                        , 'ZDZBZDZBzhibiao2'
            //                        , '" + strZYZBZD[2] + @"'
            //                        , 'ZDZBZDZBpingjiashijian1'
            //                        , 'ZDZBZDZBpingjiashijian2'
            //                        , '" + strZYZBZD[3] + @"'
            //                        , 'ZDZBZDZBzongdianzhibiaoxuanzhe1'
            //                        , 'ZDZBZDZBzongdianzhibiaoxuanzhe2'";
            //                strvule += str2;

            //                strvule += stringzbvalue;
            //                string zdzbstr = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[5]/div[2]/div[1]/div[6]").InnerText;
            //                string zdzbweishoustr = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='panel panel-default mt-4']/div[5]/div[2]/div[1]/div[8]").InnerText;
            //                string stryj =
            //                         @" , '" + zdzbstr + @"'
            //                        , '" + zdzbweishoustr + @"'";
            //                strvule += stryj;
            //                strvule += yjva;
            //                strvule += gcva;
            //                strvule += llva;
            //                string SYZ = strvule + @", '" + SYZT[1].InnerText + @"'
            //                        , '" + SYZT[3].InnerText + @"'
            //                        , '" + SYZT[5].InnerText + @"'
            //                        , '" + SYZT[7].InnerText + @"'
            //                        , '" + SYZT[9].InnerText + @"'
            //                        , '" + SYZT[11].InnerText + @"'
            //                        , '" + SYZT[13].InnerText + @"'
            //                        , 'LCSYJGZYbanbenhao'
            //                        , 'LCSYJGZYbanbenriqi'
            //                        , '" + hospitalid + @"'
            //                        , '" + name + @"'
            //                        , '" + stage + @"'
            //                        );";

            //                string slq = systr + SYZ;
            //                re += 1;
            //                changeSqlData(slq);
            //                Console.WriteLine("机构id：" + hospitalid + "--->> " + re);
            //                string sqlin = slqint1 + slqint11 + slqint12 + slqint13 + slqint14;
            //                myreader.Close();
            //                //var kk = 1;
            //            }


            //            var c = 1;
            //        }
            //        list2.Add(j);
            //    }
            //    catch
            //    {
            //    }
            //}
            ////string strURL = "https://www.yscro.com/cde/6ba2cee4c301a4b8b7073fd4e4ec2a7f";
            ////string strURL = "http://www.chinadrugtrials.org.cn/clinicaltrials.searchlistdetail.dhtml?keywords=CTR20170414";
            ////System.Net.HttpWebRequest request;
            ////request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            ////CookieContainer cc = new CookieContainer();
            ////cc.Add(new System.Uri("https://www.yscro.com"), new Cookie("token", "242b494f-c62d-47ca-807f-c78b6ae314b4"));
            ////request.CookieContainer = cc;
            ////request.Method = "get";
            ////System.Net.HttpWebResponse response;
            ////response = (System.Net.HttpWebResponse)request.GetResponse();
            ////System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            ////string responseText = myreader.ReadToEnd();
            ////myreader.Close();
            //#endregion


            #region
            //var a = tag;
            var index = 1;
            var pagesize = 10;
            List<BannerInfo> list = null;
            if (code != "" && code != null)
            {
                index = Convert.ToInt32(code);
            }
            if (state != "" && state != null)
            {
                pagesize = Convert.ToInt32(state);
            }
            string str = "SELECT  Syname,Syurl,Sytag,Sytag1,Sytag2,Sytag3,Sytag4,Syxiangmu,Syphnoe, Syjiedaishijian,Syadress  from  AirtleTable_guangdong order by Topdesc desc ";
            if (tag == "1")
            {
                str = @"SELECT  Syname,Syurl,Sytag,Sytag1,Sytag2,Sytag3,Sytag4,Syxiangmu,Syphnoe, Syjiedaishijian,Syadress  from  AirtleTable 
                                    where 
                                     Sytag   like '%" + key + @"%' Or
                                    Sytag1   like '%" + key + @"%' Or
                                    Sytag2   like '%" + key + @"%' Or
                                    Sytag3   like '%" + key + @"%' Or
                                    Sytag4   like '%" + key + @"%' Or
                                    Syxiangmu   like '%" + key + @"%' Or
                                    Syphnoe   like '%" + key + @"%' Or
                                    Syjiedaishijian   like '%" + key + @"%' Or
                                    Syadress   like '%" + key + @"%' Or
                                    Dsname   like '%" + key + @"%' Or
                                    Dsshijian   like '%" + key + @"%' Or
                                    Dstag   like '%" + key + @"%' Or
                                    Dstag1   like '%" + key + @"%' Or
                                    Dstag2   like '%" + key + @"%' Or
                                    Dstag3   like '%" + key + @"%' Or
                                    Dstag4   like '%" + key + @"%' Or
                                    Dstag5   like '%" + key + @"%' Or
                                    Dstag6   like '%" + key + @"%' Or
                                    Dsphone   like '%" + key + @"%' Or
                                    Dsemais   like '%" + key + @"%' Or
                                    Dsemail   like '%" + key + @"%' Or
                                    Dsjiedaishijian   like '%" + key + @"%' Or
                                    Dsadress   like '%" + key + @"%' Or
                                    Dsliurangshu   like '%" + key + @"%' Or
                                    DJGXXshen   like '%" + key + @"%' Or
                                    DJGXXweb   like '%" + key + @"%' Or
                                    DJGXXjgwz   like '%" + key + @"%' Or
                                    DJGXXjgzzdm   like '%" + key + @"%' Or
                                    DJGXXsclxzl   like '%" + key + @"%' Or
                                    DJGXXscllzldj   like '%" + key + @"%' Or
                                    DJGXXllxllht   like '%" + key + @"%' Or
                                    DJGXXgcp   like '%" + key + @"%' Or
                                    DJGXXzkxm   like '%" + key + @"%' Or
                                    DJGXXglfc   like '%" + key + @"%' Or
                                    DJGXXlianxirenzhiwei   like '%" + key + @"%' Or
                                    DJGXXlianxiren   like '%" + key + @"%' Or
                                    DJGXXlianxirenphone   like '%" + key + @"%' Or
                                    DJGXXlianxirenemmail   like '%" + key + @"%' Or
                                    DJGXXlianxirenzhiwei1   like '%" + key + @"%' Or
                                    DJGXXlianxiren1   like '%" + key + @"%' Or
                                    DJGXXlianxirenphone1   like '%" + key + @"%' Or
                                    DJGXXlianxirenemmail1   like '%" + key + @"%' Or
                                    DJGXXjiajie   like '%" + key + @"%' Or
                                    DJGXXjiajie1   like '%" + key + @"%' Or
                                    DJGXXjiajie2   like '%" + key + @"%' Or
                                    DJGXXjiajie3   like '%" + key + @"%' Or
                                    DJGXXjiajie4   like '%" + key + @"%' Or
                                    DBAXXYWshen   like '%" + key + @"%' Or
                                    DBAXXYWbenanhao   like '%" + key + @"%' Or
                                    DBAXXYWname   like '%" + key + @"%' Or
                                    DBAXXYWjibei   like '%" + key + @"%' Or
                                    DBAXXYWlianxiren   like '%" + key + @"%' Or
                                    DBAXXYWlianxiphone   like '%" + key + @"%' Or
                                    DBAXXYWzhuantai   like '%" + key + @"%' Or
                                    DBAXXYWshijian   like '%" + key + @"%' Or
                                    DBAXXYWjcrq   like '%" + key + @"%' Or
                                    DBAXXYWjclb   like '%" + key + @"%' Or
                                    DBAXXYWjdjcjg   like '%" + key + @"%' Or
                                    DBAXXYWclqk   like '%" + key + @"%' Or
                                    DBAXXYWjcrq1   like '%" + key + @"%' Or
                                    DBAXXYWjclb1   like '%" + key + @"%' Or
                                    DBAXXYWjdjcjg1   like '%" + key + @"%' Or
                                    DBAXXYWclqk1   like '%" + key + @"%' Or
                                    DBAXXYWzymc   like '%" + key + @"%' Or
                                    DBAXXYWzyyjz   like '%" + key + @"%' Or
                                    DBAXXYWzc   like '%" + key + @"%' Or
                                    DBAXXYWzybasj   like '%" + key + @"%' Or
                                    DBAXXYWzymc1   like '%" + key + @"%' Or
                                    DBAXXYWzyyjz1   like '%" + key + @"%' Or
                                    DBAXXYWzc1   like '%" + key + @"%' Or
                                    DBAXXYWzybasj1   like '%" + key + @"%' Or
                                    DBAXXYLshen   like '%" + key + @"%' Or
                                    DBAXXYLbenanhao   like '%" + key + @"%' Or
                                    DBAXXYLname   like '%" + key + @"%' Or
                                    DBAXXYLjibei   like '%" + key + @"%' Or
                                    DBAXXYLlianxiren   like '%" + key + @"%' Or
                                    DBAXXYLlianxiphone   like '%" + key + @"%' Or
                                    DBAXXYLzhuantai   like '%" + key + @"%' Or
                                    DBAXXYLshijian   like '%" + key + @"%' Or
                                    DBAXXYLzymc   like '%" + key + @"%' Or
                                    DBAXXYLzyyjz   like '%" + key + @"%' Or
                                    DBAXXYLzc   like '%" + key + @"%' Or
                                    DLLWTHphone   like '%" + key + @"%' Or
                                    DLLWTHchuangzhen   like '%" + key + @"%' Or
                                    DLLWTHemail   like '%" + key + @"%' Or
                                    DLLWTHjiedaishijian   like '%" + key + @"%' Or
                                    DLLWTHwangzhi   like '%" + key + @"%' Or
                                    DLLWTHshen   like '%" + key + @"%' Or
                                    DLLWTHadress   like '%" + key + @"%' Or
                                    DLLWTHLXFSzhiwei   like '%" + key + @"%' Or
                                    DLLWTHLXFSmingzhi   like '%" + key + @"%' Or
                                    DLLWTHLXFSdianhua   like '%" + key + @"%' Or
                                    DLLWTHLXFSyouxiang   like '%" + key + @"%' Or
                                    DLLWTHLXFSzhiwei1   like '%" + key + @"%' Or
                                    DLLWTHLXFSmingzhi1   like '%" + key + @"%' Or
                                    DLLWTHLXFSdianhua1   like '%" + key + @"%' Or
                                    DLLWTHLXFSyouxiang1   like '%" + key + @"%' Or
                                    DLLWTHLLllzkpl   like '%" + key + @"%' Or
                                    DLLWTHLLllshxs   like '%" + key + @"%' Or
                                    DLLWTHLLllscfy   like '%" + key + @"%' Or
                                    DLLWTHLLxgzc   like '%" + key + @"%' Or
                                    DLLWTHLLzkpl   like '%" + key + @"%' Or
                                    DLLWTHLLzkpl1   like '%" + key + @"%' Or
                                    DLLWTHLLllzkpl1   like '%" + key + @"%' Or
                                    DLLWTHLLllshxs1   like '%" + key + @"%' Or
                                    DLLWTHLLllscfy1   like '%" + key + @"%' Or
                                    DLLWTHLLxgzc1   like '%" + key + @"%' Or
                                    DKSZYZHX1   like '%" + key + @"%' Or
                                    DKSZYZHX1weizhi   like '%" + key + @"%' Or
                                    DKSZYZHX1zhiwei   like '%" + key + @"%' Or
                                    DKSZYZHX1mingzi   like '%" + key + @"%' Or
                                    DKSZYZHX1dianhua   like '%" + key + @"%' Or
                                    DKSZYZHX1email   like '%" + key + @"%' Or
                                    DKSZYZHX1keshi   like '%" + key + @"%' Or
                                    DKSZYZHX1yanjiutuandui   like '%" + key + @"%' Or
                                    DLCSYqdlx   like '%" + key + @"%' Or
                                    DLCSYgcc   like '%" + key + @"%' Or
                                    DLCSYqdny   like '%" + key + @"%' Or
                                    DLCSYcjcws   like '%" + key + @"%' Or
                                    DLCSYjtlcdz   like '%" + key + @"%' Or
                                    DLCSYgsszlx   like '%" + key + @"%' Or
                                    DLCSYLXFSzhiwei   like '%" + key + @"%' Or
                                    DLCSYLXFSmingzhi   like '%" + key + @"%' Or
                                    DLCSYLXFSdianhua   like '%" + key + @"%' Or
                                    DLCSYLXFSdizhi   like '%" + key + @"%' Or
                                    DLCSYXMJY1   like '%" + key + @"%' Or
                                    DLCSYXMJY11   like '%" + key + @"%' Or
                                    DLCSYXMJY2   like '%" + key + @"%' Or
                                    DLCSYXMJY21   like '%" + key + @"%' Or
                                    DLCSYXMJY3   like '%" + key + @"%' Or
                                    DLCSYXMJY31   like '%" + key + @"%' Or
                                    DLCSYXMJY4   like '%" + key + @"%' Or
                                    DLCSYXMJY41   like '%" + key + @"%' Or
                                    DLCSYXMJY5   like '%" + key + @"%' Or
                                    DLCSYXMJY51   like '%" + key + @"%' Or
                                    DLCSYXMJY6   like '%" + key + @"%' Or
                                    DLCSYXMJY61   like '%" + key + @"%' Or
                                    DLCSYXMJY7   like '%" + key + @"%' Or
                                    DLCSYXMJY71   like '%" + key + @"%' Or
                                    DLCSYXMJY8   like '%" + key + @"%' Or
                                    DLCSYXMJY81   like '%" + key + @"%' Or
                                    DLCSYYJTD1   like '%" + key + @"%' Or
                                    DLCSYGDJS1   like '%" + key + @"%' Or
                                    DKSZYZHX2   like '%" + key + @"%' Or
                                    DKSZYZHX2weizhi   like '%" + key + @"%' Or
                                    DKSZYZHX2zhiwei   like '%" + key + @"%' Or
                                    DKSZYZHX2mingzi   like '%" + key + @"%' Or
                                    DKSZYZHX2dianhua   like '%" + key + @"%' Or
                                    DKSZYZHX2email   like '%" + key + @"%' Or
                                    DKSZYZHX2keshi   like '%" + key + @"%' Or
                                    DKSZYZHX2keshi1   like '%" + key + @"%' Or
                                    DKSZYZHX2yanjiutuandui   like '%" + key + @"%' Or
                                    DKSZYZHX2yanjiutuandui1   like '%" + key + @"%' 
                                        order by Topdesc desc";
            }
            else if (tag == "2")
            {
                //Sytag2:333@@Sytag4:888*@
                str = "SELECT  Syname,Syurl,Sytag,Sytag1,Sytag2,Sytag3,Sytag4,Syxiangmu,Syphnoe, Syjiedaishijian,Syadress  from  AirtleTable_guangdong";
                string _where = "where 1= 1";
                string _desc = "order by Topdesc desc";
                var arr = key.Split("@@");
                foreach (string i in arr)
                {
                    if (i.Contains("Syurl")) _where += "  AND Syurl   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Sytag")) _where += "  AND Sytag   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Sytag1")) _where += "  AND Sytag1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Sytag2")) _where += "  AND Sytag2   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Sytag3")) _where += "  AND Sytag3   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Sytag4")) _where += "  AND Sytag4   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Syxiangmu")) _where += "  AND Syxiangmu   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Syphnoe")) _where += "  AND Syphnoe   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Syjiedaishijian")) _where += "  AND Syjiedaishijian   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Syadress")) _where += "  AND Syadress   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsname")) _where += "  AND Dsname   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsshijian")) _where += "  AND Dsshijian   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag")) _where += "  AND Dstag   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag1")) _where += "  AND Dstag1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag2")) _where += "  AND Dstag2   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag3")) _where += "  AND Dstag3   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag4")) _where += "  AND Dstag4   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag5")) _where += "  AND Dstag5   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag6")) _where += "  AND Dstag6   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsphone")) _where += "  AND Dsphone   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsemais")) _where += "  AND Dsemais   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsemail")) _where += "  AND Dsemail   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsjiedaishijian")) _where += "  AND Dsjiedaishijian   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsadress")) _where += "  AND Dsadress   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsliurangshu")) _where += "  AND Dsliurangshu   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXshen")) _where += "  AND DJGXXshen   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXweb")) _where += "  AND DJGXXweb   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjgwz")) _where += "  AND DJGXXjgwz   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjgzzdm")) _where += "  AND DJGXXjgzzdm   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXsclxzl")) _where += "  AND DJGXXsclxzl   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXscllzldj")) _where += "  AND DJGXXscllzldj   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXllxllht")) _where += "  AND DJGXXllxllht   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXgcp")) _where += "  AND DJGXXgcp   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXzkxm")) _where += "  AND DJGXXzkxm   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXglfc")) _where += "  AND DJGXXglfc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxirenzhiwei")) _where += "  AND DJGXXlianxirenzhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxiren")) _where += "  AND DJGXXlianxiren   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxirenphone")) _where += "  AND DJGXXlianxirenphone   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxirenemmail")) _where += "  AND DJGXXlianxirenemmail   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxirenzhiwei1")) _where += "  AND DJGXXlianxirenzhiwei1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxiren1")) _where += "  AND DJGXXlianxiren1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxirenphone1")) _where += "  AND DJGXXlianxirenphone1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxirenemmail1")) _where += "  AND DJGXXlianxirenemmail1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjiajie")) _where += "  AND DJGXXjiajie   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjiajie1")) _where += "  AND DJGXXjiajie1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjiajie2")) _where += "  AND DJGXXjiajie2   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjiajie3")) _where += "  AND DJGXXjiajie3   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjiajie4")) _where += "  AND DJGXXjiajie4   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWshen")) _where += "  AND DBAXXYWshen   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWbenanhao")) _where += "  AND DBAXXYWbenanhao   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWname")) _where += "  AND DBAXXYWname   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjibei")) _where += "  AND DBAXXYWjibei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWlianxiren")) _where += "  AND DBAXXYWlianxiren   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWlianxiphone")) _where += "  AND DBAXXYWlianxiphone   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzhuantai")) _where += "  AND DBAXXYWzhuantai   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWshijian")) _where += "  AND DBAXXYWshijian   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjcrq")) _where += "  AND DBAXXYWjcrq   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjclb")) _where += "  AND DBAXXYWjclb   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjdjcjg")) _where += "  AND DBAXXYWjdjcjg   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWclqk")) _where += "  AND DBAXXYWclqk   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjcrq1")) _where += "  AND DBAXXYWjcrq1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjclb1")) _where += "  AND DBAXXYWjclb1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjdjcjg1")) _where += "  AND DBAXXYWjdjcjg1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWclqk1")) _where += "  AND DBAXXYWclqk1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzymc")) _where += "  AND DBAXXYWzymc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzyyjz")) _where += "  AND DBAXXYWzyyjz   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzc")) _where += "  AND DBAXXYWzc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzybasj")) _where += "  AND DBAXXYWzybasj   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzymc1")) _where += "  AND DBAXXYWzymc1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzyyjz1")) _where += "  AND DBAXXYWzyyjz1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzc1")) _where += "  AND DBAXXYWzc1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzybasj1")) _where += "  AND DBAXXYWzybasj1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLshen")) _where += "  AND DBAXXYLshen   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLbenanhao")) _where += "  AND DBAXXYLbenanhao   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLname")) _where += "  AND DBAXXYLname   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLjibei")) _where += "  AND DBAXXYLjibei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLlianxiren")) _where += "  AND DBAXXYLlianxiren   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLlianxiphone")) _where += "  AND DBAXXYLlianxiphone   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLzhuantai")) _where += "  AND DBAXXYLzhuantai   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLshijian")) _where += "  AND DBAXXYLshijian   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLzymc")) _where += "  AND DBAXXYLzymc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLzyyjz")) _where += "  AND DBAXXYLzyyjz   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLzc")) _where += "  AND DBAXXYLzc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHphone")) _where += "  AND DLLWTHphone   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHchuangzhen")) _where += "  AND DLLWTHchuangzhen   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHemail")) _where += "  AND DLLWTHemail   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHjiedaishijian")) _where += "  AND DLLWTHjiedaishijian   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHwangzhi")) _where += "  AND DLLWTHwangzhi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHshen")) _where += "  AND DLLWTHshen   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHadress")) _where += "  AND DLLWTHadress   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSzhiwei")) _where += "  AND DLLWTHLXFSzhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSmingzhi")) _where += "  AND DLLWTHLXFSmingzhi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSdianhua")) _where += "  AND DLLWTHLXFSdianhua   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSyouxiang")) _where += "  AND DLLWTHLXFSyouxiang   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSzhiwei1")) _where += "  AND DLLWTHLXFSzhiwei1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSmingzhi1")) _where += "  AND DLLWTHLXFSmingzhi1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSdianhua1")) _where += "  AND DLLWTHLXFSdianhua1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSyouxiang1")) _where += "  AND DLLWTHLXFSyouxiang1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLllzkpl")) _where += "  AND DLLWTHLLllzkpl   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLllshxs")) _where += "  AND DLLWTHLLllshxs   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLllscfy")) _where += "  AND DLLWTHLLllscfy   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLxgzc")) _where += "  AND DLLWTHLLxgzc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLzkpl")) _where += "  AND DLLWTHLLzkpl   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLzkpl1")) _where += "  AND DLLWTHLLzkpl1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLllzkpl1")) _where += "  AND DLLWTHLLllzkpl1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLllshxs1")) _where += "  AND DLLWTHLLllshxs1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLllscfy1")) _where += "  AND DLLWTHLLllscfy1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLxgzc1")) _where += "  AND DLLWTHLLxgzc1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1")) _where += "  AND DKSZYZHX1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1weizhi")) _where += "  AND DKSZYZHX1weizhi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1zhiwei")) _where += "  AND DKSZYZHX1zhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1mingzi")) _where += "  AND DKSZYZHX1mingzi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1dianhua")) _where += "  AND DKSZYZHX1dianhua   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1email")) _where += "  AND DKSZYZHX1email   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1keshi")) _where += "  AND DKSZYZHX1keshi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1yanjiutuandui")) _where += "  AND DKSZYZHX1yanjiutuandui   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYqdlx")) _where += "  AND DLCSYqdlx   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYgcc")) _where += "  AND DLCSYgcc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYqdny")) _where += "  AND DLCSYqdny   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYcjcws")) _where += "  AND DLCSYcjcws   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYjtlcdz")) _where += "  AND DLCSYjtlcdz   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYgsszlx")) _where += "  AND DLCSYgsszlx   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYLXFSzhiwei")) _where += "  AND DLCSYLXFSzhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYLXFSmingzhi")) _where += "  AND DLCSYLXFSmingzhi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYLXFSdianhua")) _where += "  AND DLCSYLXFSdianhua   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYLXFSdizhi")) _where += "  AND DLCSYLXFSdizhi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY1")) _where += "  AND DLCSYXMJY1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY11")) _where += "  AND DLCSYXMJY11   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY2")) _where += "  AND DLCSYXMJY2   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY21")) _where += "  AND DLCSYXMJY21   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY3")) _where += "  AND DLCSYXMJY3   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY31")) _where += "  AND DLCSYXMJY31   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY4")) _where += "  AND DLCSYXMJY4   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY41")) _where += "  AND DLCSYXMJY41   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY5")) _where += "  AND DLCSYXMJY5   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY51")) _where += "  AND DLCSYXMJY51   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY6")) _where += "  AND DLCSYXMJY6   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY61")) _where += "  AND DLCSYXMJY61   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY7")) _where += "  AND DLCSYXMJY7   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY71")) _where += "  AND DLCSYXMJY71   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY8")) _where += "  AND DLCSYXMJY8   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY81")) _where += "  AND DLCSYXMJY81   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYYJTD1")) _where += "  AND DLCSYYJTD1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYGDJS1")) _where += "  AND DLCSYGDJS1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2")) _where += "  AND DKSZYZHX2   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2weizhi")) _where += "  AND DKSZYZHX2weizhi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2zhiwei")) _where += "  AND DKSZYZHX2zhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2mingzi")) _where += "  AND DKSZYZHX2mingzi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2dianhua")) _where += "  AND DKSZYZHX2dianhua   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2email")) _where += "  AND DKSZYZHX2email   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2keshi")) _where += "  AND DKSZYZHX2keshi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2keshi1")) _where += "  AND DKSZYZHX2keshi1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2yanjiutuandui")) _where += "  AND DKSZYZHX2yanjiutuandui   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2yanjiutuandui1")) _where += "  AND DKSZYZHX2yanjiutuandui1   like '%" + i.Split(":")[1] + @"%' ";
                }
                str = str + _where + _desc;

            }
            var result = GetGoalsEntity(str);
            var data = result.Skip((index - 1) * pagesize).Take(pagesize).ToList();
            list = await _bannerService.GetListCacheAsync(null, o => o.SortCode, false);

            //var article = await _articleService.GetListCacheAsync(null, o => o.CreatorTime, false);
            ViewBag.Yixue = data;
            ViewBag.Count = result.Count();
            ViewBag.PageNum = (result.Count() - 1) / pagesize + 1;
            ViewBag.Code = index;

            return View(list);
            #endregion
        }


        /// <summary>         
        /// 获取HTML源码信息(Porschev)         
        /// </summary>         
        /// <param name="url">获取地址</param>         
        /// <returns>HTML源码</returns>         
        public string GetHtml(string url)
        {
            string str = "";
            try
            {
                Uri uri = new Uri(url);
                WebRequest wr = WebRequest.Create(uri);
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                str = sr.ReadToEnd();
            }
            catch (Exception e)
            {
            }
            return str;
        }
        /// <summary>         
        /// 通过IP得到IP所在地省市（Porschev）         
        /// </summary>         
        /// <param name="ip"></param>         
        /// <returns></returns>         
        public string GetAdrByIp(string ip)
        {
            string url = "http://www.cz88.net/ip/?ip=" + ip;
            string regStr = "(?<=<span\\s*id=\\\"cz_addr\\\">).*?(?=</span>)";

            //得到网页源码
            string html = GetHtml(url);
            Regex reg = new Regex(regStr, RegexOptions.None);
            Match ma = reg.Match(html);
            html = ma.Value;
            string[] arr = html.Split(' ');
            return arr[0];
        }
        public async Task<IActionResult> Index34(string code, string state,string tag,string key)
        {

            #region
            var a = tag;
            var index = 1;

            
            //try
            //{
            //    string hostName = Dns.GetHostName();
            //    IPHostEntry iPHostEntry = Dns.GetHostEntry(hostName);
            //    var addressV = iPHostEntry.AddressList.FirstOrDefault(q => q.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);//ip4地址
            //    if (addressV != null) {
            //        var dd = GetAdrByIp(addressV.ToString());
            //    }
            //}
            //catch (Exception ex)
            //{
            //}

            var pagesize = 10;
            List<BannerInfo> list = null;
            if (code != "" && code != null) {
                index = Convert.ToInt32(code);
            }
            if (state != "" && state != null)
            {
                pagesize = Convert.ToInt32(state);
            }
            string str = "SELECT  Syname,Syurl,Sytag,Sytag1,Sytag2,Sytag3,Sytag4,Syxiangmu,Syphnoe, Syjiedaishijian,Syadress  from  AirtleTable_guangdong order by Topdesc desc ";
            if (tag == "1")
            {
                str = @"SELECT  Syname,Syurl,Sytag,Sytag1,Sytag2,Sytag3,Sytag4,Syxiangmu,Syphnoe, Syjiedaishijian,Syadress  from  AirtleTable 
                                    where 
                                     Sytag   like '%" + key + @"%' Or
                                    Sytag1   like '%" + key + @"%' Or
                                    Sytag2   like '%" + key + @"%' Or
                                    Sytag3   like '%" + key + @"%' Or
                                    Sytag4   like '%" + key + @"%' Or
                                    Syxiangmu   like '%" + key + @"%' Or
                                    Syphnoe   like '%" + key + @"%' Or
                                    Syjiedaishijian   like '%" + key + @"%' Or
                                    Syadress   like '%" + key + @"%' Or
                                    Dsname   like '%" + key + @"%' Or
                                    Dsshijian   like '%" + key + @"%' Or
                                    Dstag   like '%" + key + @"%' Or
                                    Dstag1   like '%" + key + @"%' Or
                                    Dstag2   like '%" + key + @"%' Or
                                    Dstag3   like '%" + key + @"%' Or
                                    Dstag4   like '%" + key + @"%' Or
                                    Dstag5   like '%" + key + @"%' Or
                                    Dstag6   like '%" + key + @"%' Or
                                    Dsphone   like '%" + key + @"%' Or
                                    Dsemais   like '%" + key + @"%' Or
                                    Dsemail   like '%" + key + @"%' Or
                                    Dsjiedaishijian   like '%" + key + @"%' Or
                                    Dsadress   like '%" + key + @"%' Or
                                    Dsliurangshu   like '%" + key + @"%' Or
                                    DJGXXshen   like '%" + key + @"%' Or
                                    DJGXXweb   like '%" + key + @"%' Or
                                    DJGXXjgwz   like '%" + key + @"%' Or
                                    DJGXXjgzzdm   like '%" + key + @"%' Or
                                    DJGXXsclxzl   like '%" + key + @"%' Or
                                    DJGXXscllzldj   like '%" + key + @"%' Or
                                    DJGXXllxllht   like '%" + key + @"%' Or
                                    DJGXXgcp   like '%" + key + @"%' Or
                                    DJGXXzkxm   like '%" + key + @"%' Or
                                    DJGXXglfc   like '%" + key + @"%' Or
                                    DJGXXlianxirenzhiwei   like '%" + key + @"%' Or
                                    DJGXXlianxiren   like '%" + key + @"%' Or
                                    DJGXXlianxirenphone   like '%" + key + @"%' Or
                                    DJGXXlianxirenemmail   like '%" + key + @"%' Or
                                    DJGXXlianxirenzhiwei1   like '%" + key + @"%' Or
                                    DJGXXlianxiren1   like '%" + key + @"%' Or
                                    DJGXXlianxirenphone1   like '%" + key + @"%' Or
                                    DJGXXlianxirenemmail1   like '%" + key + @"%' Or
                                    DJGXXjiajie   like '%" + key + @"%' Or
                                    DJGXXjiajie1   like '%" + key + @"%' Or
                                    DJGXXjiajie2   like '%" + key + @"%' Or
                                    DJGXXjiajie3   like '%" + key + @"%' Or
                                    DJGXXjiajie4   like '%" + key + @"%' Or
                                    DBAXXYWshen   like '%" + key + @"%' Or
                                    DBAXXYWbenanhao   like '%" + key + @"%' Or
                                    DBAXXYWname   like '%" + key + @"%' Or
                                    DBAXXYWjibei   like '%" + key + @"%' Or
                                    DBAXXYWlianxiren   like '%" + key + @"%' Or
                                    DBAXXYWlianxiphone   like '%" + key + @"%' Or
                                    DBAXXYWzhuantai   like '%" + key + @"%' Or
                                    DBAXXYWshijian   like '%" + key + @"%' Or
                                    DBAXXYWjcrq   like '%" + key + @"%' Or
                                    DBAXXYWjclb   like '%" + key + @"%' Or
                                    DBAXXYWjdjcjg   like '%" + key + @"%' Or
                                    DBAXXYWclqk   like '%" + key + @"%' Or
                                    DBAXXYWjcrq1   like '%" + key + @"%' Or
                                    DBAXXYWjclb1   like '%" + key + @"%' Or
                                    DBAXXYWjdjcjg1   like '%" + key + @"%' Or
                                    DBAXXYWclqk1   like '%" + key + @"%' Or
                                    DBAXXYWzymc   like '%" + key + @"%' Or
                                    DBAXXYWzyyjz   like '%" + key + @"%' Or
                                    DBAXXYWzc   like '%" + key + @"%' Or
                                    DBAXXYWzybasj   like '%" + key + @"%' Or
                                    DBAXXYWzymc1   like '%" + key + @"%' Or
                                    DBAXXYWzyyjz1   like '%" + key + @"%' Or
                                    DBAXXYWzc1   like '%" + key + @"%' Or
                                    DBAXXYWzybasj1   like '%" + key + @"%' Or
                                    DBAXXYLshen   like '%" + key + @"%' Or
                                    DBAXXYLbenanhao   like '%" + key + @"%' Or
                                    DBAXXYLname   like '%" + key + @"%' Or
                                    DBAXXYLjibei   like '%" + key + @"%' Or
                                    DBAXXYLlianxiren   like '%" + key + @"%' Or
                                    DBAXXYLlianxiphone   like '%" + key + @"%' Or
                                    DBAXXYLzhuantai   like '%" + key + @"%' Or
                                    DBAXXYLshijian   like '%" + key + @"%' Or
                                    DBAXXYLzymc   like '%" + key + @"%' Or
                                    DBAXXYLzyyjz   like '%" + key + @"%' Or
                                    DBAXXYLzc   like '%" + key + @"%' Or
                                    DLLWTHphone   like '%" + key + @"%' Or
                                    DLLWTHchuangzhen   like '%" + key + @"%' Or
                                    DLLWTHemail   like '%" + key + @"%' Or
                                    DLLWTHjiedaishijian   like '%" + key + @"%' Or
                                    DLLWTHwangzhi   like '%" + key + @"%' Or
                                    DLLWTHshen   like '%" + key + @"%' Or
                                    DLLWTHadress   like '%" + key + @"%' Or
                                    DLLWTHLXFSzhiwei   like '%" + key + @"%' Or
                                    DLLWTHLXFSmingzhi   like '%" + key + @"%' Or
                                    DLLWTHLXFSdianhua   like '%" + key + @"%' Or
                                    DLLWTHLXFSyouxiang   like '%" + key + @"%' Or
                                    DLLWTHLXFSzhiwei1   like '%" + key + @"%' Or
                                    DLLWTHLXFSmingzhi1   like '%" + key + @"%' Or
                                    DLLWTHLXFSdianhua1   like '%" + key + @"%' Or
                                    DLLWTHLXFSyouxiang1   like '%" + key + @"%' Or
                                    DLLWTHLLllzkpl   like '%" + key + @"%' Or
                                    DLLWTHLLllshxs   like '%" + key + @"%' Or
                                    DLLWTHLLllscfy   like '%" + key + @"%' Or
                                    DLLWTHLLxgzc   like '%" + key + @"%' Or
                                    DLLWTHLLzkpl   like '%" + key + @"%' Or
                                    DLLWTHLLzkpl1   like '%" + key + @"%' Or
                                    DLLWTHLLllzkpl1   like '%" + key + @"%' Or
                                    DLLWTHLLllshxs1   like '%" + key + @"%' Or
                                    DLLWTHLLllscfy1   like '%" + key + @"%' Or
                                    DLLWTHLLxgzc1   like '%" + key + @"%' Or
                                    DKSZYZHX1   like '%" + key + @"%' Or
                                    DKSZYZHX1weizhi   like '%" + key + @"%' Or
                                    DKSZYZHX1zhiwei   like '%" + key + @"%' Or
                                    DKSZYZHX1mingzi   like '%" + key + @"%' Or
                                    DKSZYZHX1dianhua   like '%" + key + @"%' Or
                                    DKSZYZHX1email   like '%" + key + @"%' Or
                                    DKSZYZHX1keshi   like '%" + key + @"%' Or
                                    DKSZYZHX1yanjiutuandui   like '%" + key + @"%' Or
                                    DLCSYqdlx   like '%" + key + @"%' Or
                                    DLCSYgcc   like '%" + key + @"%' Or
                                    DLCSYqdny   like '%" + key + @"%' Or
                                    DLCSYcjcws   like '%" + key + @"%' Or
                                    DLCSYjtlcdz   like '%" + key + @"%' Or
                                    DLCSYgsszlx   like '%" + key + @"%' Or
                                    DLCSYLXFSzhiwei   like '%" + key + @"%' Or
                                    DLCSYLXFSmingzhi   like '%" + key + @"%' Or
                                    DLCSYLXFSdianhua   like '%" + key + @"%' Or
                                    DLCSYLXFSdizhi   like '%" + key + @"%' Or
                                    DLCSYXMJY1   like '%" + key + @"%' Or
                                    DLCSYXMJY11   like '%" + key + @"%' Or
                                    DLCSYXMJY2   like '%" + key + @"%' Or
                                    DLCSYXMJY21   like '%" + key + @"%' Or
                                    DLCSYXMJY3   like '%" + key + @"%' Or
                                    DLCSYXMJY31   like '%" + key + @"%' Or
                                    DLCSYXMJY4   like '%" + key + @"%' Or
                                    DLCSYXMJY41   like '%" + key + @"%' Or
                                    DLCSYXMJY5   like '%" + key + @"%' Or
                                    DLCSYXMJY51   like '%" + key + @"%' Or
                                    DLCSYXMJY6   like '%" + key + @"%' Or
                                    DLCSYXMJY61   like '%" + key + @"%' Or
                                    DLCSYXMJY7   like '%" + key + @"%' Or
                                    DLCSYXMJY71   like '%" + key + @"%' Or
                                    DLCSYXMJY8   like '%" + key + @"%' Or
                                    DLCSYXMJY81   like '%" + key + @"%' Or
                                    DLCSYYJTD1   like '%" + key + @"%' Or
                                    DLCSYGDJS1   like '%" + key + @"%' Or
                                    DKSZYZHX2   like '%" + key + @"%' Or
                                    DKSZYZHX2weizhi   like '%" + key + @"%' Or
                                    DKSZYZHX2zhiwei   like '%" + key + @"%' Or
                                    DKSZYZHX2mingzi   like '%" + key + @"%' Or
                                    DKSZYZHX2dianhua   like '%" + key + @"%' Or
                                    DKSZYZHX2email   like '%" + key + @"%' Or
                                    DKSZYZHX2keshi   like '%" + key + @"%' Or
                                    DKSZYZHX2keshi1   like '%" + key + @"%' Or
                                    DKSZYZHX2yanjiutuandui   like '%" + key + @"%' Or
                                    DKSZYZHX2yanjiutuandui1   like '%" + key + @"%' 
                                        order by Topdesc desc";
            }
            else if (tag == "2")
            { 
                //Sytag2:333@@Sytag4:888*@
                str = "SELECT  Syname,Syurl,Sytag,Sytag1,Sytag2,Sytag3,Sytag4,Syxiangmu,Syphnoe, Syjiedaishijian,Syadress  from  AirtleTable_guangdong";
                string _where = "where 1= 1";
                string _desc = "order by Topdesc desc";
                var arr = key.Split("@@");
                foreach (string i in arr) {
                    if (i.Contains("Syurl")) _where += "  AND Syurl   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Sytag")) _where += "  AND Sytag   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Sytag1")) _where += "  AND Sytag1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Sytag2")) _where += "  AND Sytag2   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Sytag3")) _where += "  AND Sytag3   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Sytag4")) _where += "  AND Sytag4   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Syxiangmu")) _where += "  AND Syxiangmu   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Syphnoe")) _where += "  AND Syphnoe   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Syjiedaishijian")) _where += "  AND Syjiedaishijian   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Syadress")) _where += "  AND Syadress   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsname")) _where += "  AND Dsname   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsshijian")) _where += "  AND Dsshijian   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag")) _where += "  AND Dstag   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag1")) _where += "  AND Dstag1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag2")) _where += "  AND Dstag2   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag3")) _where += "  AND Dstag3   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag4")) _where += "  AND Dstag4   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag5")) _where += "  AND Dstag5   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag6")) _where += "  AND Dstag6   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsphone")) _where += "  AND Dsphone   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsemais")) _where += "  AND Dsemais   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsemail")) _where += "  AND Dsemail   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsjiedaishijian")) _where += "  AND Dsjiedaishijian   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsadress")) _where += "  AND Dsadress   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dsliurangshu")) _where += "  AND Dsliurangshu   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXshen")) _where += "  AND DJGXXshen   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXweb")) _where += "  AND DJGXXweb   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjgwz")) _where += "  AND DJGXXjgwz   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjgzzdm")) _where += "  AND DJGXXjgzzdm   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXsclxzl")) _where += "  AND DJGXXsclxzl   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXscllzldj")) _where += "  AND DJGXXscllzldj   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXllxllht")) _where += "  AND DJGXXllxllht   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXgcp")) _where += "  AND DJGXXgcp   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXzkxm")) _where += "  AND DJGXXzkxm   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXglfc")) _where += "  AND DJGXXglfc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxirenzhiwei")) _where += "  AND DJGXXlianxirenzhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxiren")) _where += "  AND DJGXXlianxiren   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxirenphone")) _where += "  AND DJGXXlianxirenphone   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxirenemmail")) _where += "  AND DJGXXlianxirenemmail   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxirenzhiwei1")) _where += "  AND DJGXXlianxirenzhiwei1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxiren1")) _where += "  AND DJGXXlianxiren1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxirenphone1")) _where += "  AND DJGXXlianxirenphone1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXlianxirenemmail1")) _where += "  AND DJGXXlianxirenemmail1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjiajie")) _where += "  AND DJGXXjiajie   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjiajie1")) _where += "  AND DJGXXjiajie1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjiajie2")) _where += "  AND DJGXXjiajie2   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjiajie3")) _where += "  AND DJGXXjiajie3   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DJGXXjiajie4")) _where += "  AND DJGXXjiajie4   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWshen")) _where += "  AND DBAXXYWshen   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWbenanhao")) _where += "  AND DBAXXYWbenanhao   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWname")) _where += "  AND DBAXXYWname   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjibei")) _where += "  AND DBAXXYWjibei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWlianxiren")) _where += "  AND DBAXXYWlianxiren   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWlianxiphone")) _where += "  AND DBAXXYWlianxiphone   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzhuantai")) _where += "  AND DBAXXYWzhuantai   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWshijian")) _where += "  AND DBAXXYWshijian   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjcrq")) _where += "  AND DBAXXYWjcrq   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjclb")) _where += "  AND DBAXXYWjclb   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjdjcjg")) _where += "  AND DBAXXYWjdjcjg   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWclqk")) _where += "  AND DBAXXYWclqk   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjcrq1")) _where += "  AND DBAXXYWjcrq1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjclb1")) _where += "  AND DBAXXYWjclb1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWjdjcjg1")) _where += "  AND DBAXXYWjdjcjg1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWclqk1")) _where += "  AND DBAXXYWclqk1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzymc")) _where += "  AND DBAXXYWzymc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzyyjz")) _where += "  AND DBAXXYWzyyjz   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzc")) _where += "  AND DBAXXYWzc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzybasj")) _where += "  AND DBAXXYWzybasj   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzymc1")) _where += "  AND DBAXXYWzymc1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzyyjz1")) _where += "  AND DBAXXYWzyyjz1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzc1")) _where += "  AND DBAXXYWzc1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYWzybasj1")) _where += "  AND DBAXXYWzybasj1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLshen")) _where += "  AND DBAXXYLshen   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLbenanhao")) _where += "  AND DBAXXYLbenanhao   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLname")) _where += "  AND DBAXXYLname   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLjibei")) _where += "  AND DBAXXYLjibei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLlianxiren")) _where += "  AND DBAXXYLlianxiren   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLlianxiphone")) _where += "  AND DBAXXYLlianxiphone   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLzhuantai")) _where += "  AND DBAXXYLzhuantai   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLshijian")) _where += "  AND DBAXXYLshijian   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLzymc")) _where += "  AND DBAXXYLzymc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLzyyjz")) _where += "  AND DBAXXYLzyyjz   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DBAXXYLzc")) _where += "  AND DBAXXYLzc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHphone")) _where += "  AND DLLWTHphone   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHchuangzhen")) _where += "  AND DLLWTHchuangzhen   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHemail")) _where += "  AND DLLWTHemail   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHjiedaishijian")) _where += "  AND DLLWTHjiedaishijian   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHwangzhi")) _where += "  AND DLLWTHwangzhi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHshen")) _where += "  AND DLLWTHshen   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHadress")) _where += "  AND DLLWTHadress   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSzhiwei")) _where += "  AND DLLWTHLXFSzhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSmingzhi")) _where += "  AND DLLWTHLXFSmingzhi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSdianhua")) _where += "  AND DLLWTHLXFSdianhua   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSyouxiang")) _where += "  AND DLLWTHLXFSyouxiang   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSzhiwei1")) _where += "  AND DLLWTHLXFSzhiwei1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSmingzhi1")) _where += "  AND DLLWTHLXFSmingzhi1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSdianhua1")) _where += "  AND DLLWTHLXFSdianhua1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLXFSyouxiang1")) _where += "  AND DLLWTHLXFSyouxiang1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLllzkpl")) _where += "  AND DLLWTHLLllzkpl   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLllshxs")) _where += "  AND DLLWTHLLllshxs   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLllscfy")) _where += "  AND DLLWTHLLllscfy   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLxgzc")) _where += "  AND DLLWTHLLxgzc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLzkpl")) _where += "  AND DLLWTHLLzkpl   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLzkpl1")) _where += "  AND DLLWTHLLzkpl1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLllzkpl1")) _where += "  AND DLLWTHLLllzkpl1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLllshxs1")) _where += "  AND DLLWTHLLllshxs1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLllscfy1")) _where += "  AND DLLWTHLLllscfy1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLLWTHLLxgzc1")) _where += "  AND DLLWTHLLxgzc1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1")) _where += "  AND DKSZYZHX1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1weizhi")) _where += "  AND DKSZYZHX1weizhi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1zhiwei")) _where += "  AND DKSZYZHX1zhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1mingzi")) _where += "  AND DKSZYZHX1mingzi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1dianhua")) _where += "  AND DKSZYZHX1dianhua   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1email")) _where += "  AND DKSZYZHX1email   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1keshi")) _where += "  AND DKSZYZHX1keshi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX1yanjiutuandui")) _where += "  AND DKSZYZHX1yanjiutuandui   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYqdlx")) _where += "  AND DLCSYqdlx   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYgcc")) _where += "  AND DLCSYgcc   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYqdny")) _where += "  AND DLCSYqdny   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYcjcws")) _where += "  AND DLCSYcjcws   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYjtlcdz")) _where += "  AND DLCSYjtlcdz   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYgsszlx")) _where += "  AND DLCSYgsszlx   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYLXFSzhiwei")) _where += "  AND DLCSYLXFSzhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYLXFSmingzhi")) _where += "  AND DLCSYLXFSmingzhi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYLXFSdianhua")) _where += "  AND DLCSYLXFSdianhua   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYLXFSdizhi")) _where += "  AND DLCSYLXFSdizhi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY1")) _where += "  AND DLCSYXMJY1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY11")) _where += "  AND DLCSYXMJY11   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY2")) _where += "  AND DLCSYXMJY2   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY21")) _where += "  AND DLCSYXMJY21   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY3")) _where += "  AND DLCSYXMJY3   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY31")) _where += "  AND DLCSYXMJY31   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY4")) _where += "  AND DLCSYXMJY4   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY41")) _where += "  AND DLCSYXMJY41   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY5")) _where += "  AND DLCSYXMJY5   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY51")) _where += "  AND DLCSYXMJY51   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY6")) _where += "  AND DLCSYXMJY6   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY61")) _where += "  AND DLCSYXMJY61   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY7")) _where += "  AND DLCSYXMJY7   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY71")) _where += "  AND DLCSYXMJY71   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY8")) _where += "  AND DLCSYXMJY8   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYXMJY81")) _where += "  AND DLCSYXMJY81   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYYJTD1")) _where += "  AND DLCSYYJTD1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DLCSYGDJS1")) _where += "  AND DLCSYGDJS1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2")) _where += "  AND DKSZYZHX2   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2weizhi")) _where += "  AND DKSZYZHX2weizhi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2zhiwei")) _where += "  AND DKSZYZHX2zhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2mingzi")) _where += "  AND DKSZYZHX2mingzi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2dianhua")) _where += "  AND DKSZYZHX2dianhua   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2email")) _where += "  AND DKSZYZHX2email   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2keshi")) _where += "  AND DKSZYZHX2keshi   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2keshi1")) _where += "  AND DKSZYZHX2keshi1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2yanjiutuandui")) _where += "  AND DKSZYZHX2yanjiutuandui   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("DKSZYZHX2yanjiutuandui1")) _where += "  AND DKSZYZHX2yanjiutuandui1   like '%" + i.Split(":")[1] + @"%' ";
                }
                str = str + _where + _desc;

            }
            var result = GetGoalsEntity(str);
            var data = result.Skip((index - 1) * pagesize).Take(pagesize).ToList();
            list = await _bannerService.GetListCacheAsync(null, o => o.SortCode, false);

            //var article = await _articleService.GetListCacheAsync(null, o => o.CreatorTime, false);
            ViewBag.Yixue = data;
            ViewBag.Count = result.Count();
            ViewBag.PageNum = (result.Count() - 1) / pagesize + 1;
            ViewBag.Code = index;

            return View(list);
            #endregion
        }

        public int changeSqlData(String sql)
        {  //执行
            try
            {
                using (SqlConnection con = new SqlConnection("server=192.168.10.28;user=sa;pwd=123456;database=Blog"))
                {
                    con.Open();
                    //操作数据库的工具SqlCommand
                    SqlCommand cmd = new SqlCommand(sql, con);//(操作语句和链接工具)
                    int i = cmd.ExecuteNonQuery();//执行操作返回影响行数（）
                    con.Close();
                    return i;
                }
            }
            catch { return 11; }

        }
        private IList<Articleiteminfo> GetGoalsEntity(string sqlstr, string filter = "")
        {
            var list1 = new List<Articleiteminfo>();
            string conStr = "server=192.168.10.28;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var i = new Articleiteminfo();
                var date = dr;
                i.Syname = date["Syname"].ToString();
                i.Syurl = date["Syurl"].ToString();
                i.Sytag = date["Sytag"].ToString();
                i.Sytag1 = date["Sytag1"].ToString();
                i.Sytag2 = date["Sytag2"].ToString();
                i.Sytag3 = date["Sytag3"].ToString();
                i.Sytag4 = date["Sytag4"].ToString();
                i.Syxiangmu = date["Syxiangmu"].ToString();
                i.Syjiedaishijian = date["Syjiedaishijian"].ToString();
                i.Syphnoe = date["Syphnoe"].ToString();
                i.Syadress = date["Syadress"].ToString();

                list1.Add(i);
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }
        private Makeiteminfo GetMakeitemEntity(string sqlstr, string filter = "")
        {
            var i = new Makeiteminfo();
            string conStr = "server=192.168.10.28;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var date = dr; i.JBXXdengjihao = date["JBXXdengjihao"].ToString();
                i.JBXXxiangguandengjihao = date["JBXXxiangguandengjihao"].ToString();
                i.JBXXchengyongming = date["JBXXchengyongming"].ToString();
                i.JBXXyaowuleixin = date["JBXXyaowuleixin"].ToString();
                i.JBXXlinchuangshoushenqing = date["JBXXlinchuangshoushenqing"].ToString();
                i.JBXXshiyingzheng = date["JBXXshiyingzheng"].ToString();
                i.JBXXshiyantongshutimu = date["JBXXshiyantongshutimu"].ToString();
                i.JBXXshiyanzhuanyetimu = date["JBXXshiyanzhuanyetimu"].ToString();
                i.JBXXshiyanfanganbianhao = date["JBXXshiyanfanganbianhao"].ToString();
                i.JBXXfanganzuijing = date["JBXXfanganzuijing"].ToString();
                i.JBXXbanbenriqi = date["JBXXbanbenriqi"].ToString();
                i.JBXXfanganshifou = date["JBXXfanganshifou"].ToString();
                i.SQRXXshengqingrenmingcheng = date["SQRXXshengqingrenmingcheng"].ToString();
                i.SQRXXlianxirenxingming = date["SQRXXlianxirenxingming"].ToString();
                i.SQRXXlianxirenzuoji = date["SQRXXlianxirenzuoji"].ToString();
                i.SQRXXlianxirenshoujihao = date["SQRXXlianxirenshoujihao"].ToString();
                i.SQRXXlianxirenemail = date["SQRXXlianxirenemail"].ToString();
                i.SQRXXlianxirenyouzhendizhi = date["SQRXXlianxirenyouzhendizhi"].ToString();
                i.SQRXXlianxirenyoubian = date["SQRXXlianxirenyoubian"].ToString();
                i.LCSYXXshiyanfenlie = date["LCSYXXshiyanfenlie"].ToString();
                i.LCSYXXshiyanfenqi = date["LCSYXXshiyanfenqi"].ToString();
                i.LCSYXXshiyanmudi = date["LCSYXXshiyanmudi"].ToString();
                i.LCSYXXshuijihua = date["LCSYXXshuijihua"].ToString();
                i.LCSYXXmanfang = date["LCSYXXmanfang"].ToString();
                i.LCSYXXshiyanfangwei = date["LCSYXXshiyanfangwei"].ToString();
                i.LCSYXXshejiliexing = date["LCSYXXshejiliexing"].ToString();
                i.LCSYXXninaliang = date["LCSYXXninaliang"].ToString();
                i.LCSYXXxingbei = date["LCSYXXxingbei"].ToString();
                i.LCSYXXjiankuangshoushizhen = date["LCSYXXjiankuangshoushizhen"].ToString();
                i.LCSYXXruxuanbiaozhun = date["LCSYXXruxuanbiaozhun"].ToString();
                i.LCSYXXruxuanbiaozhun1 = date["LCSYXXruxuanbiaozhun1"].ToString();
                i.LCSYXXruxuanbiaozhun2 = date["LCSYXXruxuanbiaozhun2"].ToString();
                i.LCSYXXruxuanbiaozhun3 = date["LCSYXXruxuanbiaozhun3"].ToString();
                i.LCSYXXruxuanbiaozhun4 = date["LCSYXXruxuanbiaozhun4"].ToString();
                i.LCSYXXruxuanbiaozhun5 = date["LCSYXXruxuanbiaozhun5"].ToString();
                i.LCSYXXruxuanbiaozhun6 = date["LCSYXXruxuanbiaozhun6"].ToString();
                i.LCSYXXruxuanbiaozhun7 = date["LCSYXXruxuanbiaozhun7"].ToString();
                i.LCSYXXruxuanbiaozhun8 = date["LCSYXXruxuanbiaozhun8"].ToString();
                i.LCSYXXruxuanbiaozhun9 = date["LCSYXXruxuanbiaozhun9"].ToString();
                i.LCSYXXruxuanbiaozhun10 = date["LCSYXXruxuanbiaozhun10"].ToString();
                i.LCSYXXruxuanbiaozhun11 = date["LCSYXXruxuanbiaozhun11"].ToString();
                i.LCSYXXruxuanbiaozhun12 = date["LCSYXXruxuanbiaozhun12"].ToString();
                i.LCSYXXruxuanbiaozhun13 = date["LCSYXXruxuanbiaozhun13"].ToString();
                i.LCSYXXruxuanbiaozhun14 = date["LCSYXXruxuanbiaozhun14"].ToString();
                i.LCSYXXruxuanbiaozhun15 = date["LCSYXXruxuanbiaozhun15"].ToString();
                i.LCSYXXruxuanbiaozhun16 = date["LCSYXXruxuanbiaozhun16"].ToString();
                i.LCSYXXruxuanbiaozhun17 = date["LCSYXXruxuanbiaozhun17"].ToString();
                i.LCSYXXruxuanbiaozhun18 = date["LCSYXXruxuanbiaozhun18"].ToString();
                i.LCSYXXruxuanbiaozhun19 = date["LCSYXXruxuanbiaozhun19"].ToString();
                i.LCSYXXruxuanbiaozhun20 = date["LCSYXXruxuanbiaozhun20"].ToString();
                i.LCSYXXpaichubiaozhun = date["LCSYXXpaichubiaozhun"].ToString();
                i.LCSYXXpaichubiaozhun1 = date["LCSYXXpaichubiaozhun1"].ToString();
                i.LCSYXXpaichubiaozhun2 = date["LCSYXXpaichubiaozhun2"].ToString();
                i.LCSYXXpaichubiaozhun3 = date["LCSYXXpaichubiaozhun3"].ToString();
                i.LCSYXXpaichubiaozhun4 = date["LCSYXXpaichubiaozhun4"].ToString();
                i.LCSYXXpaichubiaozhun5 = date["LCSYXXpaichubiaozhun5"].ToString();
                i.LCSYXXpaichubiaozhun6 = date["LCSYXXpaichubiaozhun6"].ToString();
                i.LCSYXXpaichubiaozhun7 = date["LCSYXXpaichubiaozhun7"].ToString();
                i.LCSYXXpaichubiaozhun8 = date["LCSYXXpaichubiaozhun8"].ToString();
                i.LCSYXXpaichubiaozhun9 = date["LCSYXXpaichubiaozhun9"].ToString();
                i.LCSYXXpaichubiaozhun10 = date["LCSYXXpaichubiaozhun10"].ToString();
                i.LCSYXXpaichubiaozhun11 = date["LCSYXXpaichubiaozhun11"].ToString();
                i.LCSYXXpaichubiaozhun12 = date["LCSYXXpaichubiaozhun12"].ToString();
                i.LCSYXXpaichubiaozhun13 = date["LCSYXXpaichubiaozhun13"].ToString();
                i.LCSYXXpaichubiaozhun14 = date["LCSYXXpaichubiaozhun14"].ToString();
                i.LCSYXXpaichubiaozhun15 = date["LCSYXXpaichubiaozhun15"].ToString();
                i.LCSYXXpaichubiaozhun16 = date["LCSYXXpaichubiaozhun16"].ToString();
                i.LCSYXXpaichubiaozhun17 = date["LCSYXXpaichubiaozhun17"].ToString();
                i.LCSYXXpaichubiaozhun18 = date["LCSYXXpaichubiaozhun18"].ToString();
                i.LCSYXXpaichubiaozhun19 = date["LCSYXXpaichubiaozhun19"].ToString();
                i.LCSYXXpaichubiaozhun20 = date["LCSYXXpaichubiaozhun20"].ToString();
                i.SYFZshiyanyaomingcheng = date["SYFZshiyanyaomingcheng"].ToString();
                i.SYFZshiyanyaoyongfa = date["SYFZshiyanyaoyongfa"].ToString();
                i.SYFZduizhaomyaomingcheng = date["SYFZduizhaomyaomingcheng"].ToString();
                i.SYFZduizhaoyaoyongfa = date["SYFZduizhaoyaoyongfa"].ToString();
                i.ZDZBZDZBzhibiao = date["ZDZBZDZBzhibiao"].ToString();
                i.ZDZBZDZBzhibiao1 = date["ZDZBZDZBzhibiao1"].ToString();
                i.ZDZBZDZBzhibiao2 = date["ZDZBZDZBzhibiao2"].ToString();
                i.ZDZBZDZBpingjiashijian = date["ZDZBZDZBpingjiashijian"].ToString();
                i.ZDZBZDZBpingjiashijian1 = date["ZDZBZDZBpingjiashijian1"].ToString();
                i.ZDZBZDZBpingjiashijian2 = date["ZDZBZDZBpingjiashijian2"].ToString();
                i.ZDZBZDZBzongdianzhibiaoxuanzhe = date["ZDZBZDZBzongdianzhibiaoxuanzhe"].ToString();
                i.ZDZBZDZBzongdianzhibiaoxuanzhe1 = date["ZDZBZDZBzongdianzhibiaoxuanzhe1"].ToString();
                i.ZDZBZDZBzongdianzhibiaoxuanzhe2 = date["ZDZBZDZBzongdianzhibiaoxuanzhe2"].ToString();
                i.ZDZBCYZDZBzhibiao = date["ZDZBCYZDZBzhibiao"].ToString();
                i.ZDZBCYZDZBzhibiao1 = date["ZDZBCYZDZBzhibiao1"].ToString();
                i.ZDZBCYZDZBzhibiao2 = date["ZDZBCYZDZBzhibiao2"].ToString();
                i.ZDZBCYZDZBzhibiao3 = date["ZDZBCYZDZBzhibiao3"].ToString();
                i.ZDZBCYZDZBzhibiao4 = date["ZDZBCYZDZBzhibiao4"].ToString();
                i.ZDZBCYZDZBzhibiao5 = date["ZDZBCYZDZBzhibiao5"].ToString();
                i.ZDZBCYZDZBpingjiashijian = date["ZDZBCYZDZBpingjiashijian"].ToString();
                i.ZDZBCYZDZBpingjiashijian1 = date["ZDZBCYZDZBpingjiashijian1"].ToString();
                i.ZDZBCYZDZBpingjiashijian2 = date["ZDZBCYZDZBpingjiashijian2"].ToString();
                i.ZDZBCYZDZBpingjiashijian3 = date["ZDZBCYZDZBpingjiashijian3"].ToString();
                i.ZDZBCYZDZBpingjiashijian4 = date["ZDZBCYZDZBpingjiashijian4"].ToString();
                i.ZDZBCYZDZBpingjiashijian5 = date["ZDZBCYZDZBpingjiashijian5"].ToString();
                i.ZDZBCYZDZBzongdianzhibiaoxuanzhe = date["ZDZBCYZDZBzongdianzhibiaoxuanzhe"].ToString();
                i.ZDZBCYZDZBzongdianzhibiaoxuanzhe1 = date["ZDZBCYZDZBzongdianzhibiaoxuanzhe1"].ToString();
                i.ZDZBCYZDZBzongdianzhibiaoxuanzhe2 = date["ZDZBCYZDZBzongdianzhibiaoxuanzhe2"].ToString();
                i.ZDZBCYZDZBzongdianzhibiaoxuanzhe3 = date["ZDZBCYZDZBzongdianzhibiaoxuanzhe3"].ToString();
                i.ZDZBCYZDZBzongdianzhibiaoxuanzhe4 = date["ZDZBCYZDZBzongdianzhibiaoxuanzhe4"].ToString();
                i.ZDZBCYZDZBzongdianzhibiaoxuanzhe5 = date["ZDZBCYZDZBzongdianzhibiaoxuanzhe5"].ToString();
                i.ZDZBshujuanquanjianchaweiyuanhui = date["ZDZBshujuanquanjianchaweiyuanhui"].ToString();
                i.ZDZBweishouzhezheguomaishiyan = date["ZDZBweishouzhezheguomaishiyan"].ToString();
                i.YJZXXxingming = date["YJZXXxingming"].ToString();
                i.YJZXXxingming1 = date["YJZXXxingming1"].ToString();
                i.YJZXXxuewei = date["YJZXXxuewei"].ToString();
                i.YJZXXxuewei1 = date["YJZXXxuewei1"].ToString();
                i.YJZXXzhicheng = date["YJZXXzhicheng"].ToString();
                i.YJZXXzhicheng1 = date["YJZXXzhicheng1"].ToString();
                i.YJZXXdianhuan = date["YJZXXdianhuan"].ToString();
                i.YJZXXdianhuan1 = date["YJZXXdianhuan1"].ToString();
                i.YJZXXemail = date["YJZXXemail"].ToString();
                i.YJZXXemail1 = date["YJZXXemail1"].ToString();
                i.YJZXXyouzhenbiaoma = date["YJZXXyouzhenbiaoma"].ToString();
                i.YJZXXyouzhenbiaoma1 = date["YJZXXyouzhenbiaoma1"].ToString();
                i.YJZXXyoubian = date["YJZXXyoubian"].ToString();
                i.YJZXXyoubian1 = date["YJZXXyoubian1"].ToString();
                i.YJZXXdangweimingcheng = date["YJZXXdangweimingcheng"].ToString();
                i.YJZXXdangweimingcheng1 = date["YJZXXdangweimingcheng1"].ToString();
                i.GCJJGmingcheng = date["GCJJGmingcheng"].ToString();
                i.GCJJGmingcheng1 = date["GCJJGmingcheng1"].ToString();
                i.GCJJGmingcheng2 = date["GCJJGmingcheng2"].ToString();
                i.GCJJGmingcheng3 = date["GCJJGmingcheng3"].ToString();
                i.GCJJGmingcheng4 = date["GCJJGmingcheng4"].ToString();
                i.GCJJGmingcheng5 = date["GCJJGmingcheng5"].ToString();
                i.GCJJGmingcheng6 = date["GCJJGmingcheng6"].ToString();
                i.GCJJGmingcheng7 = date["GCJJGmingcheng7"].ToString();
                i.GCJJGmingcheng8 = date["GCJJGmingcheng8"].ToString();
                i.GCJJGmingcheng9 = date["GCJJGmingcheng9"].ToString();
                i.GCJJGmingcheng10 = date["GCJJGmingcheng10"].ToString();
                i.GCJJGmingcheng11 = date["GCJJGmingcheng11"].ToString();
                i.GCJJGmingcheng12 = date["GCJJGmingcheng12"].ToString();
                i.GCJJGmingcheng13 = date["GCJJGmingcheng13"].ToString();
                i.GCJJGmingcheng14 = date["GCJJGmingcheng14"].ToString();
                i.GCJJGmingcheng15 = date["GCJJGmingcheng15"].ToString();
                i.GCJJGmingcheng16 = date["GCJJGmingcheng16"].ToString();
                i.GCJJGmingcheng17 = date["GCJJGmingcheng17"].ToString();
                i.GCJJGmingcheng18 = date["GCJJGmingcheng18"].ToString();
                i.GCJJGmingcheng19 = date["GCJJGmingcheng19"].ToString();
                i.GCJJGmingcheng20 = date["GCJJGmingcheng20"].ToString();
                i.GCJJGmingcheng21 = date["GCJJGmingcheng21"].ToString();
                i.GCJJGmingcheng22 = date["GCJJGmingcheng22"].ToString();
                i.GCJJGmingcheng23 = date["GCJJGmingcheng23"].ToString();
                i.GCJJGmingcheng24 = date["GCJJGmingcheng24"].ToString();
                i.GCJJGmingcheng25 = date["GCJJGmingcheng25"].ToString();
                i.GCJJGmingcheng26 = date["GCJJGmingcheng26"].ToString();
                i.GCJJGmingcheng27 = date["GCJJGmingcheng27"].ToString();
                i.GCJJGmingcheng28 = date["GCJJGmingcheng28"].ToString();
                i.GCJJGmingcheng29 = date["GCJJGmingcheng29"].ToString();
                i.GCJJGmingcheng30 = date["GCJJGmingcheng30"].ToString();
                i.GCJJGzhuyaoyanjiuzhe = date["GCJJGzhuyaoyanjiuzhe"].ToString();
                i.GCJJGzhuyaoyanjiuzhe1 = date["GCJJGzhuyaoyanjiuzhe1"].ToString();
                i.GCJJGzhuyaoyanjiuzhe2 = date["GCJJGzhuyaoyanjiuzhe2"].ToString();
                i.GCJJGzhuyaoyanjiuzhe3 = date["GCJJGzhuyaoyanjiuzhe3"].ToString();
                i.GCJJGzhuyaoyanjiuzhe4 = date["GCJJGzhuyaoyanjiuzhe4"].ToString();
                i.GCJJGzhuyaoyanjiuzhe5 = date["GCJJGzhuyaoyanjiuzhe5"].ToString();
                i.GCJJGzhuyaoyanjiuzhe6 = date["GCJJGzhuyaoyanjiuzhe6"].ToString();
                i.GCJJGzhuyaoyanjiuzhe7 = date["GCJJGzhuyaoyanjiuzhe7"].ToString();
                i.GCJJGzhuyaoyanjiuzhe8 = date["GCJJGzhuyaoyanjiuzhe8"].ToString();
                i.GCJJGzhuyaoyanjiuzhe9 = date["GCJJGzhuyaoyanjiuzhe9"].ToString();
                i.GCJJGzhuyaoyanjiuzhe10 = date["GCJJGzhuyaoyanjiuzhe10"].ToString();
                i.GCJJGzhuyaoyanjiuzhe11 = date["GCJJGzhuyaoyanjiuzhe11"].ToString();
                i.GCJJGzhuyaoyanjiuzhe12 = date["GCJJGzhuyaoyanjiuzhe12"].ToString();
                i.GCJJGzhuyaoyanjiuzhe13 = date["GCJJGzhuyaoyanjiuzhe13"].ToString();
                i.GCJJGzhuyaoyanjiuzhe14 = date["GCJJGzhuyaoyanjiuzhe14"].ToString();
                i.GCJJGzhuyaoyanjiuzhe15 = date["GCJJGzhuyaoyanjiuzhe15"].ToString();
                i.GCJJGzhuyaoyanjiuzhe16 = date["GCJJGzhuyaoyanjiuzhe16"].ToString();
                i.GCJJGzhuyaoyanjiuzhe17 = date["GCJJGzhuyaoyanjiuzhe17"].ToString();
                i.GCJJGzhuyaoyanjiuzhe18 = date["GCJJGzhuyaoyanjiuzhe18"].ToString();
                i.GCJJGzhuyaoyanjiuzhe19 = date["GCJJGzhuyaoyanjiuzhe19"].ToString();
                i.GCJJGzhuyaoyanjiuzhe20 = date["GCJJGzhuyaoyanjiuzhe20"].ToString();
                i.GCJJGzhuyaoyanjiuzhe21 = date["GCJJGzhuyaoyanjiuzhe21"].ToString();
                i.GCJJGzhuyaoyanjiuzhe22 = date["GCJJGzhuyaoyanjiuzhe22"].ToString();
                i.GCJJGzhuyaoyanjiuzhe23 = date["GCJJGzhuyaoyanjiuzhe23"].ToString();
                i.GCJJGzhuyaoyanjiuzhe24 = date["GCJJGzhuyaoyanjiuzhe24"].ToString();
                i.GCJJGzhuyaoyanjiuzhe25 = date["GCJJGzhuyaoyanjiuzhe25"].ToString();
                i.GCJJGzhuyaoyanjiuzhe26 = date["GCJJGzhuyaoyanjiuzhe26"].ToString();
                i.GCJJGzhuyaoyanjiuzhe27 = date["GCJJGzhuyaoyanjiuzhe27"].ToString();
                i.GCJJGzhuyaoyanjiuzhe28 = date["GCJJGzhuyaoyanjiuzhe28"].ToString();
                i.GCJJGzhuyaoyanjiuzhe29 = date["GCJJGzhuyaoyanjiuzhe29"].ToString();
                i.GCJJGzhuyaoyanjiuzhe30 = date["GCJJGzhuyaoyanjiuzhe30"].ToString();
                i.GCJJGguojia = date["GCJJGguojia"].ToString();
                i.GCJJGguojia1 = date["GCJJGguojia1"].ToString();
                i.GCJJGguojia2 = date["GCJJGguojia2"].ToString();
                i.GCJJGguojia3 = date["GCJJGguojia3"].ToString();
                i.GCJJGguojia4 = date["GCJJGguojia4"].ToString();
                i.GCJJGguojia5 = date["GCJJGguojia5"].ToString();
                i.GCJJGguojia6 = date["GCJJGguojia6"].ToString();
                i.GCJJGguojia7 = date["GCJJGguojia7"].ToString();
                i.GCJJGguojia8 = date["GCJJGguojia8"].ToString();
                i.GCJJGguojia9 = date["GCJJGguojia9"].ToString();
                i.GCJJGguojia10 = date["GCJJGguojia10"].ToString();
                i.GCJJGguojia11 = date["GCJJGguojia11"].ToString();
                i.GCJJGguojia12 = date["GCJJGguojia12"].ToString();
                i.GCJJGguojia13 = date["GCJJGguojia13"].ToString();
                i.GCJJGguojia14 = date["GCJJGguojia14"].ToString();
                i.GCJJGguojia15 = date["GCJJGguojia15"].ToString();
                i.GCJJGguojia16 = date["GCJJGguojia16"].ToString();
                i.GCJJGguojia17 = date["GCJJGguojia17"].ToString();
                i.GCJJGguojia18 = date["GCJJGguojia18"].ToString();
                i.GCJJGguojia19 = date["GCJJGguojia19"].ToString();
                i.GCJJGguojia20 = date["GCJJGguojia20"].ToString();
                i.GCJJGguojia21 = date["GCJJGguojia21"].ToString();
                i.GCJJGguojia22 = date["GCJJGguojia22"].ToString();
                i.GCJJGguojia23 = date["GCJJGguojia23"].ToString();
                i.GCJJGguojia24 = date["GCJJGguojia24"].ToString();
                i.GCJJGguojia25 = date["GCJJGguojia25"].ToString();
                i.GCJJGguojia26 = date["GCJJGguojia26"].ToString();
                i.GCJJGguojia27 = date["GCJJGguojia27"].ToString();
                i.GCJJGguojia28 = date["GCJJGguojia28"].ToString();
                i.GCJJGguojia29 = date["GCJJGguojia29"].ToString();
                i.GCJJGguojia30 = date["GCJJGguojia30"].ToString();
                i.GCJJGdiqu = date["GCJJGdiqu"].ToString();
                i.GCJJGdiqu1 = date["GCJJGdiqu1"].ToString();
                i.GCJJGdiqu2 = date["GCJJGdiqu2"].ToString();
                i.GCJJGdiqu3 = date["GCJJGdiqu3"].ToString();
                i.GCJJGdiqu4 = date["GCJJGdiqu4"].ToString();
                i.GCJJGdiqu5 = date["GCJJGdiqu5"].ToString();
                i.GCJJGdiqu6 = date["GCJJGdiqu6"].ToString();
                i.GCJJGdiqu7 = date["GCJJGdiqu7"].ToString();
                i.GCJJGdiqu8 = date["GCJJGdiqu8"].ToString();
                i.GCJJGdiqu9 = date["GCJJGdiqu9"].ToString();
                i.GCJJGdiqu10 = date["GCJJGdiqu10"].ToString();
                i.GCJJGdiqu11 = date["GCJJGdiqu11"].ToString();
                i.GCJJGdiqu12 = date["GCJJGdiqu12"].ToString();
                i.GCJJGdiqu13 = date["GCJJGdiqu13"].ToString();
                i.GCJJGdiqu14 = date["GCJJGdiqu14"].ToString();
                i.GCJJGdiqu15 = date["GCJJGdiqu15"].ToString();
                i.GCJJGdiqu16 = date["GCJJGdiqu16"].ToString();
                i.GCJJGdiqu17 = date["GCJJGdiqu17"].ToString();
                i.GCJJGdiqu18 = date["GCJJGdiqu18"].ToString();
                i.GCJJGdiqu19 = date["GCJJGdiqu19"].ToString();
                i.GCJJGdiqu20 = date["GCJJGdiqu20"].ToString();
                i.GCJJGdiqu21 = date["GCJJGdiqu21"].ToString();
                i.GCJJGdiqu22 = date["GCJJGdiqu22"].ToString();
                i.GCJJGdiqu23 = date["GCJJGdiqu23"].ToString();
                i.GCJJGdiqu24 = date["GCJJGdiqu24"].ToString();
                i.GCJJGdiqu25 = date["GCJJGdiqu25"].ToString();
                i.GCJJGdiqu26 = date["GCJJGdiqu26"].ToString();
                i.GCJJGdiqu27 = date["GCJJGdiqu27"].ToString();
                i.GCJJGdiqu28 = date["GCJJGdiqu28"].ToString();
                i.GCJJGdiqu29 = date["GCJJGdiqu29"].ToString();
                i.GCJJGdiqu30 = date["GCJJGdiqu30"].ToString();
                i.GCJJGchengshi = date["GCJJGchengshi"].ToString();
                i.GCJJGchengshi1 = date["GCJJGchengshi1"].ToString();
                i.GCJJGchengshi2 = date["GCJJGchengshi2"].ToString();
                i.GCJJGchengshi3 = date["GCJJGchengshi3"].ToString();
                i.GCJJGchengshi4 = date["GCJJGchengshi4"].ToString();
                i.GCJJGchengshi5 = date["GCJJGchengshi5"].ToString();
                i.GCJJGchengshi6 = date["GCJJGchengshi6"].ToString();
                i.GCJJGchengshi7 = date["GCJJGchengshi7"].ToString();
                i.GCJJGchengshi8 = date["GCJJGchengshi8"].ToString();
                i.GCJJGchengshi9 = date["GCJJGchengshi9"].ToString();
                i.GCJJGchengshi10 = date["GCJJGchengshi10"].ToString();
                i.GCJJGchengshi11 = date["GCJJGchengshi11"].ToString();
                i.GCJJGchengshi12 = date["GCJJGchengshi12"].ToString();
                i.GCJJGchengshi13 = date["GCJJGchengshi13"].ToString();
                i.GCJJGchengshi14 = date["GCJJGchengshi14"].ToString();
                i.GCJJGchengshi15 = date["GCJJGchengshi15"].ToString();
                i.GCJJGchengshi16 = date["GCJJGchengshi16"].ToString();
                i.GCJJGchengshi17 = date["GCJJGchengshi17"].ToString();
                i.GCJJGchengshi18 = date["GCJJGchengshi18"].ToString();
                i.GCJJGchengshi19 = date["GCJJGchengshi19"].ToString();
                i.GCJJGchengshi20 = date["GCJJGchengshi20"].ToString();
                i.GCJJGchengshi21 = date["GCJJGchengshi21"].ToString();
                i.GCJJGchengshi22 = date["GCJJGchengshi22"].ToString();
                i.GCJJGchengshi23 = date["GCJJGchengshi23"].ToString();
                i.GCJJGchengshi24 = date["GCJJGchengshi24"].ToString();
                i.GCJJGchengshi25 = date["GCJJGchengshi25"].ToString();
                i.GCJJGchengshi26 = date["GCJJGchengshi26"].ToString();
                i.GCJJGchengshi27 = date["GCJJGchengshi27"].ToString();
                i.GCJJGchengshi28 = date["GCJJGchengshi28"].ToString();
                i.GCJJGchengshi29 = date["GCJJGchengshi29"].ToString();
                i.GCJJGchengshi30 = date["GCJJGchengshi30"].ToString();
                i.LLWYHmingcheng1 = date["LLWYHmingcheng1"].ToString();
                i.LLWYHmingcheng2 = date["LLWYHmingcheng2"].ToString();
                i.LLWYHmingcheng3 = date["LLWYHmingcheng3"].ToString();
                i.LLWYHmingcheng4 = date["LLWYHmingcheng4"].ToString();
                i.LLWYHmingcheng5 = date["LLWYHmingcheng5"].ToString();
                i.LLWYHshechajiruan1 = date["LLWYHshechajiruan1"].ToString();
                i.LLWYHshechajiruan2 = date["LLWYHshechajiruan2"].ToString();
                i.LLWYHshechajiruan3 = date["LLWYHshechajiruan3"].ToString();
                i.LLWYHshechajiruan4 = date["LLWYHshechajiruan4"].ToString();
                i.LLWYHshechajiruan5 = date["LLWYHshechajiruan5"].ToString();
                i.LLWYHchachariqi1 = date["LLWYHchachariqi1"].ToString();
                i.LLWYHchachariqi2 = date["LLWYHchachariqi2"].ToString();
                i.LLWYHchachariqi3 = date["LLWYHchachariqi3"].ToString();
                i.LLWYHchachariqi4 = date["LLWYHchachariqi4"].ToString();
                i.LLWYHchachariqi5 = date["LLWYHchachariqi5"].ToString();
                i.SYZTXXshiyanzhuantai = date["SYZTXXshiyanzhuantai"].ToString();
                i.SYZTXXmubiaoruzhurenshu = date["SYZTXXmubiaoruzhurenshu"].ToString();
                i.SYZTXXyiruzhulishu = date["SYZTXXyiruzhulishu"].ToString();
                i.SYZTXXshijiruzhuzonglishu = date["SYZTXXshijiruzhuzonglishu"].ToString();
                i.SYZTXXdiyilieshoushizheqianshu = date["SYZTXXdiyilieshoushizheqianshu"].ToString();
                i.SYZTXXdiyilieshoushizheruzhuriqi = date["SYZTXXdiyilieshoushizheruzhuriqi"].ToString();
                i.SYZTXXshiyanzongzhiriqi = date["SYZTXXshiyanzongzhiriqi"].ToString();
                i.LCSYJGZYbanbenhao = date["LCSYJGZYbanbenhao"].ToString();
                i.LCSYJGZYbanbenriqi = date["LCSYJGZYbanbenriqi"].ToString();
                i.Other = date["Other"].ToString();
                i.TitleName = date["TitleName"].ToString();
                i.TitleStage = date["TitleStage"].ToString();
            }
            dr.Close();//关闭DataReader对象  
            return i;
        }
        private Articleiteminfo GetGoalsitemEntity(string sqlstr, string filter = "")
        {
            var i = new Articleiteminfo();
            string conStr = "server=192.168.10.28;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var date = dr;
                i.Syurl = date["Syurl"].ToString();
                i.Sytag = date["Sytag"].ToString();
                i.Sytag1 = date["Sytag1"].ToString();
                i.Sytag2 = date["Sytag2"].ToString();
                i.Sytag3 = date["Sytag3"].ToString();
                i.Sytag4 = date["Sytag4"].ToString();
                i.Syxiangmu = date["Syxiangmu"].ToString();
                i.Syphnoe = date["Syphnoe"].ToString();
                i.Syjiedaishijian = date["Syjiedaishijian"].ToString();
                i.Syadress = date["Syadress"].ToString();
                i.Dsname = date["Dsname"].ToString();
                i.Dsshijian = date["Dsshijian"].ToString();
                i.Dstag = date["Dstag"].ToString();
                i.Dstag1 = date["Dstag1"].ToString();
                i.Dstag2 = date["Dstag2"].ToString();
                i.Dstag3 = date["Dstag3"].ToString();
                i.Dstag4 = date["Dstag4"].ToString();
                i.Dstag5 = date["Dstag5"].ToString();
                i.Dstag6 = date["Dstag6"].ToString();
                i.Dsphone = date["Dsphone"].ToString();
                i.Dsemais = date["Dsemais"].ToString();
                i.Dsemail = date["Dsemail"].ToString();
                i.Dsjiedaishijian = date["Dsjiedaishijian"].ToString();
                i.Dsadress = date["Dsadress"].ToString();
                i.Dsliurangshu = date["Dsliurangshu"].ToString();
                i.DJGXXshen = date["DJGXXshen"].ToString();
                i.DJGXXweb = date["DJGXXweb"].ToString();
                i.DJGXXjgwz = date["DJGXXjgwz"].ToString();
                i.DJGXXjgzzdm = date["DJGXXjgzzdm"].ToString();
                i.DJGXXsclxzl = date["DJGXXsclxzl"].ToString();
                i.DJGXXscllzldj = date["DJGXXscllzldj"].ToString();
                i.DJGXXllxllht = date["DJGXXllxllht"].ToString();
                i.DJGXXgcp = date["DJGXXgcp"].ToString();
                i.DJGXXzkxm = date["DJGXXzkxm"].ToString();
                i.DJGXXglfc = date["DJGXXglfc"].ToString();
                i.DJGXXlianxirenzhiwei = date["DJGXXlianxirenzhiwei"].ToString();
                i.DJGXXlianxiren = date["DJGXXlianxiren"].ToString();
                i.DJGXXlianxirenphone = date["DJGXXlianxirenphone"].ToString();
                i.DJGXXlianxirenemmail = date["DJGXXlianxirenemmail"].ToString();
                i.DJGXXlianxirenzhiwei1 = date["DJGXXlianxirenzhiwei1"].ToString();
                i.DJGXXlianxiren1 = date["DJGXXlianxiren1"].ToString();
                i.DJGXXlianxirenphone1 = date["DJGXXlianxirenphone1"].ToString();
                i.DJGXXlianxirenemmail1 = date["DJGXXlianxirenemmail1"].ToString();
                i.DJGXXjiajie = date["DJGXXjiajie"].ToString();
                i.DJGXXjiajie1 = date["DJGXXjiajie1"].ToString();
                i.DJGXXjiajie2 = date["DJGXXjiajie2"].ToString();
                i.DJGXXjiajie3 = date["DJGXXjiajie3"].ToString();
                i.DJGXXjiajie4 = date["DJGXXjiajie4"].ToString();
                i.DBAXXYWshen = date["DBAXXYWshen"].ToString();
                i.DBAXXYWbenanhao = date["DBAXXYWbenanhao"].ToString();
                i.DBAXXYWname = date["DBAXXYWname"].ToString();
                i.DBAXXYWjibei = date["DBAXXYWjibei"].ToString();
                i.DBAXXYWlianxiren = date["DBAXXYWlianxiren"].ToString();
                i.DBAXXYWlianxiphone = date["DBAXXYWlianxiphone"].ToString();
                i.DBAXXYWzhuantai = date["DBAXXYWzhuantai"].ToString();
                i.DBAXXYWshijian = date["DBAXXYWshijian"].ToString();
                i.DBAXXYWjcrq = date["DBAXXYWjcrq"].ToString();
                i.DBAXXYWjclb = date["DBAXXYWjclb"].ToString();
                i.DBAXXYWjdjcjg = date["DBAXXYWjdjcjg"].ToString();
                i.DBAXXYWclqk = date["DBAXXYWclqk"].ToString();
                i.DBAXXYWjcrq1 = date["DBAXXYWjcrq1"].ToString();
                i.DBAXXYWjclb1 = date["DBAXXYWjclb1"].ToString();
                i.DBAXXYWjdjcjg1 = date["DBAXXYWjdjcjg1"].ToString();
                i.DBAXXYWclqk1 = date["DBAXXYWclqk1"].ToString();
                i.DBAXXYWzymc = date["DBAXXYWzymc"].ToString();
                i.DBAXXYWzyyjz = date["DBAXXYWzyyjz"].ToString();
                i.DBAXXYWzc = date["DBAXXYWzc"].ToString();
                i.DBAXXYWzybasj = date["DBAXXYWzybasj"].ToString();
                i.DBAXXYWzymc1 = date["DBAXXYWzymc1"].ToString();
                i.DBAXXYWzyyjz1 = date["DBAXXYWzyyjz1"].ToString();
                i.DBAXXYWzc1 = date["DBAXXYWzc1"].ToString();
                i.DBAXXYWzybasj1 = date["DBAXXYWzybasj1"].ToString();
                i.DBAXXYLshen = date["DBAXXYLshen"].ToString();
                i.DBAXXYLbenanhao = date["DBAXXYLbenanhao"].ToString();
                i.DBAXXYLname = date["DBAXXYLname"].ToString();
                i.DBAXXYLjibei = date["DBAXXYLjibei"].ToString();
                i.DBAXXYLlianxiren = date["DBAXXYLlianxiren"].ToString();
                i.DBAXXYLlianxiphone = date["DBAXXYLlianxiphone"].ToString();
                i.DBAXXYLzhuantai = date["DBAXXYLzhuantai"].ToString();
                i.DBAXXYLshijian = date["DBAXXYLshijian"].ToString();
                i.DBAXXYLzymc = date["DBAXXYLzymc"].ToString();
                i.DBAXXYLzyyjz = date["DBAXXYLzyyjz"].ToString();
                i.DBAXXYLzc = date["DBAXXYLzc"].ToString();
                i.DLLWTHphone = date["DLLWTHphone"].ToString();
                i.DLLWTHchuangzhen = date["DLLWTHchuangzhen"].ToString();
                i.DLLWTHemail = date["DLLWTHemail"].ToString();
                i.DLLWTHjiedaishijian = date["DLLWTHjiedaishijian"].ToString();
                i.DLLWTHwangzhi = date["DLLWTHwangzhi"].ToString();
                i.DLLWTHshen = date["DLLWTHshen"].ToString();
                i.DLLWTHadress = date["DLLWTHadress"].ToString();
                i.DLLWTHLXFSzhiwei = date["DLLWTHLXFSzhiwei"].ToString();
                i.DLLWTHLXFSmingzhi = date["DLLWTHLXFSmingzhi"].ToString();
                i.DLLWTHLXFSdianhua = date["DLLWTHLXFSdianhua"].ToString();
                i.DLLWTHLXFSyouxiang = date["DLLWTHLXFSyouxiang"].ToString();
                i.DLLWTHLXFSzhiwei1 = date["DLLWTHLXFSzhiwei1"].ToString();
                i.DLLWTHLXFSmingzhi1 = date["DLLWTHLXFSmingzhi1"].ToString();
                i.DLLWTHLXFSdianhua1 = date["DLLWTHLXFSdianhua1"].ToString();
                i.DLLWTHLXFSyouxiang1 = date["DLLWTHLXFSyouxiang1"].ToString();
                i.DLLWTHLLllzkpl = date["DLLWTHLLllzkpl"].ToString();
                i.DLLWTHLLllshxs = date["DLLWTHLLllshxs"].ToString();
                i.DLLWTHLLllscfy = date["DLLWTHLLllscfy"].ToString();
                i.DLLWTHLLxgzc = date["DLLWTHLLxgzc"].ToString();
                i.DLLWTHLLzkpl = date["DLLWTHLLzkpl"].ToString();
                i.DLLWTHLLzkpl1 = date["DLLWTHLLzkpl1"].ToString();
                i.DLLWTHLLllzkpl1 = date["DLLWTHLLllzkpl1"].ToString();
                i.DLLWTHLLllshxs1 = date["DLLWTHLLllshxs1"].ToString();
                i.DLLWTHLLllscfy1 = date["DLLWTHLLllscfy1"].ToString();
                i.DLLWTHLLxgzc1 = date["DLLWTHLLxgzc1"].ToString();
                i.DKSZYZHX1 = date["DKSZYZHX1"].ToString();
                i.DKSZYZHX1weizhi = date["DKSZYZHX1weizhi"].ToString();
                i.DKSZYZHX1zhiwei = date["DKSZYZHX1zhiwei"].ToString();
                i.DKSZYZHX1mingzi = date["DKSZYZHX1mingzi"].ToString();
                i.DKSZYZHX1dianhua = date["DKSZYZHX1dianhua"].ToString();
                i.DKSZYZHX1email = date["DKSZYZHX1email"].ToString();
                i.DKSZYZHX1keshi = date["DKSZYZHX1keshi"].ToString();
                i.DKSZYZHX1yanjiutuandui = date["DKSZYZHX1yanjiutuandui"].ToString();
                i.DLCSYqdlx = date["DLCSYqdlx"].ToString();
                i.DLCSYgcc = date["DLCSYgcc"].ToString();
                i.DLCSYqdny = date["DLCSYqdny"].ToString();
                i.DLCSYcjcws = date["DLCSYcjcws"].ToString();
                i.DLCSYjtlcdz = date["DLCSYjtlcdz"].ToString();
                i.DLCSYgsszlx = date["DLCSYgsszlx"].ToString();
                i.DLCSYLXFSzhiwei = date["DLCSYLXFSzhiwei"].ToString();
                i.DLCSYLXFSmingzhi = date["DLCSYLXFSmingzhi"].ToString();
                i.DLCSYLXFSdianhua = date["DLCSYLXFSdianhua"].ToString();
                i.DLCSYLXFSdizhi = date["DLCSYLXFSdizhi"].ToString();
                i.DLCSYXMJY1 = date["DLCSYXMJY1"].ToString();
                i.DLCSYXMJY11 = date["DLCSYXMJY11"].ToString();
                i.DLCSYXMJY2 = date["DLCSYXMJY2"].ToString();
                i.DLCSYXMJY21 = date["DLCSYXMJY21"].ToString();
                i.DLCSYXMJY3 = date["DLCSYXMJY3"].ToString();
                i.DLCSYXMJY31 = date["DLCSYXMJY31"].ToString();
                i.DLCSYXMJY4 = date["DLCSYXMJY4"].ToString();
                i.DLCSYXMJY41 = date["DLCSYXMJY41"].ToString();
                i.DLCSYXMJY5 = date["DLCSYXMJY5"].ToString();
                i.DLCSYXMJY51 = date["DLCSYXMJY51"].ToString();
                i.DLCSYXMJY6 = date["DLCSYXMJY6"].ToString();
                i.DLCSYXMJY61 = date["DLCSYXMJY61"].ToString();
                i.DLCSYXMJY7 = date["DLCSYXMJY7"].ToString();
                i.DLCSYXMJY71 = date["DLCSYXMJY71"].ToString();
                i.DLCSYXMJY8 = date["DLCSYXMJY8"].ToString();
                i.DLCSYXMJY81 = date["DLCSYXMJY81"].ToString();
                i.DLCSYYJTD1 = date["DLCSYYJTD1"].ToString();
                i.DLCSYGDJS1 = date["DLCSYGDJS1"].ToString();
                i.DKSZYZHX2 = date["DKSZYZHX2"].ToString();
                i.DKSZYZHX2weizhi = date["DKSZYZHX2weizhi"].ToString();
                i.DKSZYZHX2zhiwei = date["DKSZYZHX2zhiwei"].ToString();
                i.DKSZYZHX2mingzi = date["DKSZYZHX2mingzi"].ToString();
                i.DKSZYZHX2dianhua = date["DKSZYZHX2dianhua"].ToString();
                i.DKSZYZHX2email = date["DKSZYZHX2email"].ToString();
                i.DKSZYZHX2keshi = date["DKSZYZHX2keshi"].ToString();
                i.DKSZYZHX2keshi1 = date["DKSZYZHX2keshi1"].ToString();
                i.DKSZYZHX2yanjiutuandui = date["DKSZYZHX2yanjiutuandui"].ToString();
                i.DKSZYZHX2yanjiutuandui1 = date["DKSZYZHX2yanjiutuandui1"].ToString();
                i.DKSZYZHX3 = date["DKSZYZHX3"].ToString();
                i.DKSZYZHX3weizhi = date["DKSZYZHX3weizhi"].ToString();
                i.DKSZYZHX3zhiwei = date["DKSZYZHX3zhiwei"].ToString();
                i.DKSZYZHX3mingzi = date["DKSZYZHX3mingzi"].ToString();
                i.DKSZYZHX3dianhua = date["DKSZYZHX3dianhua"].ToString();
                i.DKSZYZHX3email = date["DKSZYZHX3email"].ToString();
                i.DKSZYZHX3keshi = date["DKSZYZHX3keshi"].ToString();
                i.DKSZYZHX3keshi1 = date["DKSZYZHX3keshi1"].ToString();
                i.DKSZYZHX3yanjiutuandui = date["DKSZYZHX3yanjiutuandui"].ToString();
                i.DKSZYZHX3yanjiutuandui1 = date["DKSZYZHX3yanjiutuandui1"].ToString();

            }
            dr.Close();//关闭DataReader对象  
            return i;
        }
        /// <summary>
        /// 作品展示
        /// </summary>
        /// <returns></returns>
        public IActionResult Works()
        {
            return View();
        }

        /// <summary>
        /// 时光轴
        /// </summary>
        /// <returns></returns>
        public IActionResult TimeLine()
        {
            return View();
        }

        /// <summary>
        /// 关于
        /// </summary>
        /// <returns></returns>
        public IActionResult AboutItem(string tid, string sid = "")
        {
            var filter = tid;

            string str = @" SELECT JBXXdengjihao, JBXXxiangguandengjihao, JBXXchengyongming, JBXXyaowuleixin, JBXXlinchuangshoushenqing,
                            JBXXshiyingzheng, JBXXshiyantongshutimu, JBXXshiyanzhuanyetimu, JBXXshiyanfanganbianhao, JBXXfanganzuijing, 
                            JBXXbanbenriqi, JBXXfanganshifou, SQRXXshengqingrenmingcheng, SQRXXlianxirenxingming, SQRXXlianxirenzuoji, 
                            SQRXXlianxirenshoujihao, SQRXXlianxirenemail, SQRXXlianxirenyouzhendizhi, SQRXXlianxirenyoubian,
                             LCSYXXshiyanfenlie, LCSYXXshiyanfenqi, LCSYXXshiyanmudi, LCSYXXshuijihua, LCSYXXmanfang, 
                             LCSYXXshiyanfangwei, LCSYXXshejiliexing, LCSYXXninaliang, LCSYXXxingbei, LCSYXXjiankuangshoushizhen, 
                             LCSYXXruxuanbiaozhun, LCSYXXruxuanbiaozhun1, LCSYXXruxuanbiaozhun2, LCSYXXruxuanbiaozhun3, 
                             LCSYXXruxuanbiaozhun4, LCSYXXruxuanbiaozhun5, LCSYXXruxuanbiaozhun6, LCSYXXruxuanbiaozhun7, 
                             LCSYXXruxuanbiaozhun8, LCSYXXruxuanbiaozhun9, LCSYXXruxuanbiaozhun10, LCSYXXruxuanbiaozhun11,
                             LCSYXXruxuanbiaozhun12, LCSYXXruxuanbiaozhun13, LCSYXXruxuanbiaozhun14, LCSYXXruxuanbiaozhun15, 
                             LCSYXXruxuanbiaozhun16, LCSYXXruxuanbiaozhun17, LCSYXXruxuanbiaozhun18, LCSYXXruxuanbiaozhun19, 
                             LCSYXXruxuanbiaozhun20, LCSYXXpaichubiaozhun, LCSYXXpaichubiaozhun1, LCSYXXpaichubiaozhun2, 
                             LCSYXXpaichubiaozhun3, LCSYXXpaichubiaozhun4, LCSYXXpaichubiaozhun5, LCSYXXpaichubiaozhun6,
                             LCSYXXpaichubiaozhun7, LCSYXXpaichubiaozhun8, LCSYXXpaichubiaozhun9, LCSYXXpaichubiaozhun10,
                             LCSYXXpaichubiaozhun11, LCSYXXpaichubiaozhun12, LCSYXXpaichubiaozhun13, LCSYXXpaichubiaozhun14,
                             LCSYXXpaichubiaozhun15, LCSYXXpaichubiaozhun16, LCSYXXpaichubiaozhun17, LCSYXXpaichubiaozhun18, 
                             LCSYXXpaichubiaozhun19, LCSYXXpaichubiaozhun20, SYFZshiyanyaomingcheng, SYFZshiyanyaoyongfa, 
                             SYFZduizhaomyaomingcheng, SYFZduizhaoyaoyongfa, ZDZBZDZBzhibiao, ZDZBZDZBzhibiao1, ZDZBZDZBzhibiao2, 
                             ZDZBZDZBpingjiashijian, ZDZBZDZBpingjiashijian1, ZDZBZDZBpingjiashijian2, ZDZBZDZBzongdianzhibiaoxuanzhe, 
                             ZDZBZDZBzongdianzhibiaoxuanzhe1, ZDZBZDZBzongdianzhibiaoxuanzhe2, ZDZBCYZDZBzhibiao, ZDZBCYZDZBzhibiao1, 
                             ZDZBCYZDZBzhibiao2, ZDZBCYZDZBzhibiao3, ZDZBCYZDZBzhibiao4, ZDZBCYZDZBzhibiao5, ZDZBCYZDZBpingjiashijian,
                             ZDZBCYZDZBpingjiashijian1, ZDZBCYZDZBpingjiashijian2, ZDZBCYZDZBpingjiashijian3, ZDZBCYZDZBpingjiashijian4,
                             ZDZBCYZDZBpingjiashijian5, ZDZBCYZDZBzongdianzhibiaoxuanzhe, ZDZBCYZDZBzongdianzhibiaoxuanzhe1, 
                             ZDZBCYZDZBzongdianzhibiaoxuanzhe2, ZDZBCYZDZBzongdianzhibiaoxuanzhe3, ZDZBCYZDZBzongdianzhibiaoxuanzhe4,
                             ZDZBCYZDZBzongdianzhibiaoxuanzhe5, ZDZBshujuanquanjianchaweiyuanhui, ZDZBweishouzhezheguomaishiyan, 
                             YJZXXxingming, YJZXXxingming1, YJZXXxuewei, YJZXXxuewei1, YJZXXzhicheng, YJZXXzhicheng1, YJZXXdianhuan, 
                             YJZXXdianhuan1, YJZXXemail, YJZXXemail1, YJZXXyouzhenbiaoma, YJZXXyouzhenbiaoma1, YJZXXyoubian, YJZXXyoubian1,
                             YJZXXdangweimingcheng, YJZXXdangweimingcheng1, GCJJGmingcheng, GCJJGmingcheng1, GCJJGmingcheng2, 
                             GCJJGmingcheng3, GCJJGmingcheng4, GCJJGmingcheng5, GCJJGmingcheng6, GCJJGmingcheng7, GCJJGmingcheng8, 
                             GCJJGmingcheng9, GCJJGmingcheng10, GCJJGmingcheng11, GCJJGmingcheng12, GCJJGmingcheng13, GCJJGmingcheng14, 
                             GCJJGmingcheng15, GCJJGmingcheng16, GCJJGmingcheng17, GCJJGmingcheng18, GCJJGmingcheng19, GCJJGmingcheng20,
                             GCJJGmingcheng21, GCJJGmingcheng22, GCJJGmingcheng23, GCJJGmingcheng24, GCJJGmingcheng25, GCJJGmingcheng26,
                             GCJJGmingcheng27, GCJJGmingcheng28, GCJJGmingcheng29, GCJJGmingcheng30, GCJJGzhuyaoyanjiuzhe,
                             GCJJGzhuyaoyanjiuzhe1, GCJJGzhuyaoyanjiuzhe2, GCJJGzhuyaoyanjiuzhe3, GCJJGzhuyaoyanjiuzhe4, 
                             GCJJGzhuyaoyanjiuzhe5, GCJJGzhuyaoyanjiuzhe6, GCJJGzhuyaoyanjiuzhe7, GCJJGzhuyaoyanjiuzhe8, 
                             GCJJGzhuyaoyanjiuzhe9, GCJJGzhuyaoyanjiuzhe10, GCJJGzhuyaoyanjiuzhe11, GCJJGzhuyaoyanjiuzhe12, 
                             GCJJGzhuyaoyanjiuzhe13, GCJJGzhuyaoyanjiuzhe14, GCJJGzhuyaoyanjiuzhe15, GCJJGzhuyaoyanjiuzhe16, 
                             GCJJGzhuyaoyanjiuzhe17, GCJJGzhuyaoyanjiuzhe18, GCJJGzhuyaoyanjiuzhe19, GCJJGzhuyaoyanjiuzhe20,
                             GCJJGzhuyaoyanjiuzhe21, GCJJGzhuyaoyanjiuzhe22, GCJJGzhuyaoyanjiuzhe23, GCJJGzhuyaoyanjiuzhe24, 
                             GCJJGzhuyaoyanjiuzhe25, GCJJGzhuyaoyanjiuzhe26, GCJJGzhuyaoyanjiuzhe27, GCJJGzhuyaoyanjiuzhe28,
                             GCJJGzhuyaoyanjiuzhe29, GCJJGzhuyaoyanjiuzhe30, GCJJGguojia, GCJJGguojia1, GCJJGguojia2, 
                             GCJJGguojia3, GCJJGguojia4, GCJJGguojia5, GCJJGguojia6, GCJJGguojia7, GCJJGguojia8, GCJJGguojia9, 
                             GCJJGguojia10, GCJJGguojia11, GCJJGguojia12, GCJJGguojia13, GCJJGguojia14, GCJJGguojia15, GCJJGguojia16,
                             GCJJGguojia17, GCJJGguojia18, GCJJGguojia19, GCJJGguojia20, GCJJGguojia21, GCJJGguojia22, GCJJGguojia23,
                             GCJJGguojia24, GCJJGguojia25, GCJJGguojia26, GCJJGguojia27, GCJJGguojia28, GCJJGguojia29, GCJJGguojia30,
                             GCJJGdiqu, GCJJGdiqu1, GCJJGdiqu2, GCJJGdiqu3, GCJJGdiqu4, GCJJGdiqu5, GCJJGdiqu6, GCJJGdiqu7, GCJJGdiqu8, 
                             GCJJGdiqu9, GCJJGdiqu10, GCJJGdiqu11, GCJJGdiqu12, GCJJGdiqu13, GCJJGdiqu14, GCJJGdiqu15, GCJJGdiqu16,
                             GCJJGdiqu17, GCJJGdiqu18, GCJJGdiqu19, GCJJGdiqu20, GCJJGdiqu21, GCJJGdiqu22, GCJJGdiqu23, GCJJGdiqu24,
                             GCJJGdiqu25, GCJJGdiqu26, GCJJGdiqu27, GCJJGdiqu28, GCJJGdiqu29, GCJJGdiqu30, GCJJGchengshi, 
                             GCJJGchengshi1, GCJJGchengshi2, GCJJGchengshi3, GCJJGchengshi4, GCJJGchengshi5, GCJJGchengshi6,
                             GCJJGchengshi7, GCJJGchengshi8, GCJJGchengshi9, GCJJGchengshi10, GCJJGchengshi11, GCJJGchengshi12,
                             GCJJGchengshi13, GCJJGchengshi14, GCJJGchengshi15, GCJJGchengshi16, GCJJGchengshi17, GCJJGchengshi18, 
                             GCJJGchengshi19, GCJJGchengshi20, GCJJGchengshi21, GCJJGchengshi22, GCJJGchengshi23, 
                             GCJJGchengshi24, GCJJGchengshi25, GCJJGchengshi26, GCJJGchengshi27, GCJJGchengshi28,
                             GCJJGchengshi29, GCJJGchengshi30, LLWYHmingcheng1, LLWYHmingcheng2, LLWYHmingcheng3, LLWYHmingcheng4,
                             LLWYHmingcheng5, LLWYHshechajiruan1, LLWYHshechajiruan2, LLWYHshechajiruan3, LLWYHshechajiruan4, 
                             LLWYHshechajiruan5, LLWYHchachariqi1, LLWYHchachariqi2, LLWYHchachariqi3, LLWYHchachariqi4, 
                             LLWYHchachariqi5, SYZTXXshiyanzhuantai, SYZTXXmubiaoruzhurenshu, SYZTXXyiruzhulishu, SYZTXXshijiruzhuzonglishu,
                             SYZTXXdiyilieshoushizheqianshu, SYZTXXdiyilieshoushizheruzhuriqi, SYZTXXshiyanzongzhiriqi, LCSYJGZYbanbenhao, 
                             LCSYJGZYbanbenriqi, Other, TitleName, TitleStage, Todesc from MakeTable where JBXXdengjihao = '" + filter + "'";
            var reslut = GetMakeitemEntity(str);
            ////string url = Request.RawUrl;
            ////string url = HttpContext.Current.Request.Url.Host;
            var d = tid;
            ViewBag.Dailt = reslut;
            return View();
        }
        public IActionResult About(string tid,string sid = "")
        {
            var k = 1;
            string table = "AirtleTable";
            string _table = "AirtleTable_guangdong";
            if (sid != "" && sid != null)
            {
                table = table + "_" + sid;
            }
            else {
                table = _table;
            }
            string str = " SELECT top 1 Syname,Syurl,Sytag,Sytag1,Sytag2,Sytag3,Sytag4,Syxiangmu,Syphnoe,Syjiedaishijian,Syadress,Dsname," +
                "Dsshijian,Dstag,Dstag1,Dstag2,Dstag3,Dstag4,Dstag5,Dstag6,Dsphone,Dsemais,Dsemail,Dsjiedaishijian,Dsadress,Dsliurangshu," +
                "DJGXXshen,DJGXXweb,DJGXXjgwz,DJGXXjgzzdm,DJGXXsclxzl,DJGXXscllzldj,DJGXXllxllht,DJGXXgcp,DJGXXzkxm,DJGXXglfc," +
                "DJGXXlianxirenzhiwei,DJGXXlianxiren,DJGXXlianxirenphone,DJGXXlianxirenemmail,DJGXXlianxirenzhiwei1," +
                "DJGXXlianxiren1,DJGXXlianxirenphone1,DJGXXlianxirenemmail1,DJGXXjiajie,DJGXXjiajie1,DJGXXjiajie2," +
                "DJGXXjiajie3,DJGXXjiajie4,DBAXXYWshen,DBAXXYWbenanhao,DBAXXYWname,DBAXXYWjibei,DBAXXYWlianxiren," +
                "DBAXXYWlianxiphone,DBAXXYWzhuantai,DBAXXYWshijian,DBAXXYWjcrq,DBAXXYWjclb,DBAXXYWjdjcjg," +
                "DBAXXYWclqk,DBAXXYWjcrq1,DBAXXYWjclb1,DBAXXYWjdjcjg1,DBAXXYWclqk1,DBAXXYWzymc,DBAXXYWzyyjz," +
                "DBAXXYWzc,DBAXXYWzybasj,DBAXXYWzymc1,DBAXXYWzyyjz1,DBAXXYWzc1,DBAXXYWzybasj1,DBAXXYLshen," +
                "DBAXXYLbenanhao,DBAXXYLname,DBAXXYLjibei,DBAXXYLlianxiren,DBAXXYLlianxiphone,DBAXXYLzhuantai," +
                "DBAXXYLshijian,DBAXXYLzymc,DBAXXYLzyyjz,DBAXXYLzc,DLLWTHphone,DLLWTHchuangzhen,DLLWTHemail," +
                "DLLWTHjiedaishijian,DLLWTHwangzhi,DLLWTHshen,DLLWTHadress,DLLWTHLXFSzhiwei,DLLWTHLXFSmingzhi," +
                "DLLWTHLXFSdianhua,DLLWTHLXFSyouxiang,DLLWTHLXFSzhiwei1,DLLWTHLXFSmingzhi1,DLLWTHLXFSdianhua1," +
                "DLLWTHLXFSyouxiang1,DLLWTHLLllzkpl,DLLWTHLLllshxs,DLLWTHLLllscfy,DLLWTHLLxgzc,DLLWTHLLzkpl,DLLWTHLLzkpl1," +
                "DLLWTHLLllzkpl1,DLLWTHLLllshxs1,DLLWTHLLllscfy1,DLLWTHLLxgzc1,DKSZYZHX1,DKSZYZHX1weizhi,DKSZYZHX1zhiwei," +
                "DKSZYZHX1mingzi,DKSZYZHX1dianhua,DKSZYZHX1email,DKSZYZHX1keshi,DKSZYZHX1yanjiutuandui,DLCSYqdlx,DLCSYgcc," +
                "DLCSYqdny,DLCSYcjcws,DLCSYjtlcdz,DLCSYgsszlx,DLCSYLXFSzhiwei,DLCSYLXFSmingzhi,DLCSYLXFSdianhua,DLCSYLXFSdizhi," +
                "DLCSYXMJY1,DLCSYXMJY11,DLCSYXMJY2,DLCSYXMJY21,DLCSYXMJY3,DLCSYXMJY31,DLCSYXMJY4,DLCSYXMJY41,DLCSYXMJY5,DLCSYXMJY51," +
                "DLCSYXMJY6,DLCSYXMJY61,DLCSYXMJY7,DLCSYXMJY71,DLCSYXMJY8,DLCSYXMJY81,DLCSYYJTD1,DLCSYGDJS1,DKSZYZHX2,DKSZYZHX2weizhi," +
                "DKSZYZHX2zhiwei,DKSZYZHX2mingzi,DKSZYZHX2dianhua,DKSZYZHX2email,DKSZYZHX2keshi,DKSZYZHX2keshi1," +
                "DKSZYZHX2yanjiutuandui,DKSZYZHX3, DKSZYZHX3weizhi, DKSZYZHX3zhiwei, DKSZYZHX3mingzi, DKSZYZHX3dianhua, DKSZYZHX3email, " +
                "DKSZYZHX3keshi, DKSZYZHX3keshi1, DKSZYZHX3yanjiutuandui, DKSZYZHX3yanjiutuandui1, DKSZYZHX3jingyan, DKSZYZHX3qita, " +
                "DKSZYZHX2yanjiutuandui1 FROM "+ table + " where Syurl ='" + tid +"'";
            var reslut = GetGoalsitemEntity(str);
            //string url = Request.RawUrl;
            //string url = HttpContext.Current.Request.Url.Host;
            var d = tid;
            ViewBag.Dailt = reslut;
            return View();
        }

        /// <summary>
        /// 关于
        /// </summary>
        /// <returns></returns>
        public IActionResult AboutDetail()
        {
            return View();
        }
        #endregion


        public async Task<IActionResult> Init()
        {
            var hot = await _articleService.Hot(6);
            var notice = await _noticeService.GetListCacheAsync(null, o => o.SortCode, false);
            var tags = await _tagsService.TagsCount();
            var link = await _friendLinkService.GetListCacheAsync(null, o => o.SortCode, false);
            return Json(new { hot, notice, tags, link });
        }

        /// <summary>
        /// 时间轴
        /// </summary>
        /// <param name="pageInput"></param>
        /// <returns></returns>
        public IActionResult Line(PageInputDto pageInput)
        {
            var result = _timeLineService.GetListByPage(null, x => x.PublishDate, true, pageInput.Page, pageInput.Limit);
            var data = result;
            IEnumerable<int> years = data.Select(s => s.PublishDate.Year).Distinct().OrderByDescending(o => o);
            List<TimeLineDto> times = new List<TimeLineDto>();
            foreach (int item in years)
            {
                TimeLineDto dto = new TimeLineDto();
                dto.Year = item;
                var list = data.Where(c => c.PublishDate.Year == item);
                IEnumerable<int> months = list.Select(s => s.PublishDate.Month).Distinct().OrderBy(o => o);
                Dictionary<string, IEnumerable<LineItem>> pairs = new Dictionary<string, IEnumerable<LineItem>>();
                foreach (int m in months)
                {
                    pairs[m.ToString("D2")] = list.Where(c => c.PublishDate.Month == m).Select(s => new LineItem { Content = s.Content, Time = s.PublishDate.ToString("MM月dd日 HH:mm") }).OrderBy(o => o.Time);
                }
                dto.Items = pairs;
                times.Add(dto);
            }
            PageOutputDto<List<TimeLineDto>> d = new PageOutputDto<List<TimeLineDto>>();
            d.code = 0;
            d.count = data.Pages;
            d.data = times;
            return Json(d);
        }

        /// <summary>
        /// 留言列表
        /// </summary>
        /// <returns></returns>
        public IActionResult Msg(LeavemsgQueryInputDto dto)
        {
            return Json(_leavemsgService.MsgList(dto), "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 回复分页
        /// </summary>
        /// <param name="dto">查询model</param>
        /// <returns></returns>
        public IActionResult ReplyPage(LeavemsgQueryInputDto dto)
        {
            return Json(_leavemsgService.ReplyList(dto), "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 回复
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Reply(ReplyInputDto dto)
        {
            UnifyResult result = new UnifyResult();
            QQUserinfo user = HttpContext.GetSession<QQUserinfo>("QQ_User");
            if (user == null)
            {
                result.StatusCode = ResultCode.Unauthorized;
                result.Message = "未登录";
            }
            else
            {
                dto.UserId = user.Id;
                result = await _leavemsgService.Reply(dto);
            }
            return Json(result);
        }

        /// <summary>
        /// 留言
        /// </summary>
        /// <param name="dto">留言内容</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Comment(CommentInputDto dto)
        {
            UnifyResult result = new UnifyResult();
            QQUserinfo user = HttpContext.GetSession<QQUserinfo>("QQ_User");
            if (user == null)
            {
                result.StatusCode = ResultCode.Unauthorized;
                result.Message = "未登录";
            }
            else
            {
                dto.UserId = user.Id;
                result = await _leavemsgService.Comment(dto);
            }
            return Json(result);
        }

        /// <summary>
        /// 友情链接
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Link()
        {
            var link = await _friendLinkService.GetListCacheAsync(null, o => o.SortCode, false);
            return Json(link);
        }

        /// <summary>
        /// QQ授权登录
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Login(string code, string state)
        {
            if (string.IsNullOrWhiteSpace(code) && string.IsNullOrWhiteSpace(state))
            {
                string referer = HttpContext.Request.Headers[HeaderNames.Referer].FirstOrDefault();
                if (string.IsNullOrWhiteSpace(referer))
                {
                    referer = "/home/index";
                }
                return Json(_qQUserService.Authorize(referer));
            }
            else
            {
                var user = await _qQUserService.Login(code, state);
                string url = HttpContext.Session.GetString("lib" + state);
                if (string.IsNullOrWhiteSpace(url))
                {
                    url = "/home/index";
                }
                if (user != null)
                {
                    string json = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetString("QQ_User", json);
                }
                HttpContext.Session.Remove("lib" + state);
                return Redirect(url);
            }

        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public new IActionResult SignOut()
        {
            HttpContext.Session.Remove("QQ_User");
            return Json(new UnifyResult("已安全退出"));
        }
    }
}