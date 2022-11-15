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
        public async Task<IActionResult> Index(string code, string state,string tag,string key)
        {

            var a = tag;
            var index = 1;
            var pagesize = 10;
            if (code != "" && code != null) {
                index = Convert.ToInt32(code);
            }
            if (state != "" && state != null)
            {
                pagesize = Convert.ToInt32(state);
            }
            string str = "SELECT  Syname,Syurl,Sytag,Sytag1,Sytag2,Sytag3,Sytag4,Syxiangmu,Syphnoe, Syjiedaishijian,Syadress  from  AirtleTable order by Topdesc desc ";
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
                str = "SELECT  Syname,Syurl,Sytag,Sytag1,Sytag2,Sytag3,Sytag4,Syxiangmu,Syphnoe, Syjiedaishijian,Syadress  from  AirtleTable";
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
            List<BannerInfo> list = await _bannerService.GetListCacheAsync(null, o => o.SortCode, false);

            //var article = await _articleService.GetListCacheAsync(null, o => o.CreatorTime, false);
            ViewBag.Yixue = data;
            ViewBag.Count = result.Count();
            ViewBag.PageNum = (result.Count() - 1) / pagesize + 1;
            ViewBag.Code = index;

            return View(list);
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
        public IActionResult About(string tid)
        {
            string str = " SELECT top 1 Syname,Syurl,Sytag,Sytag1,Sytag2,Sytag3,Sytag4,Syxiangmu,Syphnoe,Syjiedaishijian,Syadress,Dsname,Dsshijian,Dstag,Dstag1,Dstag2,Dstag3,Dstag4,Dstag5,Dstag6,Dsphone,Dsemais,Dsemail,Dsjiedaishijian,Dsadress,Dsliurangshu,DJGXXshen,DJGXXweb,DJGXXjgwz,DJGXXjgzzdm,DJGXXsclxzl,DJGXXscllzldj,DJGXXllxllht,DJGXXgcp,DJGXXzkxm,DJGXXglfc,DJGXXlianxirenzhiwei,DJGXXlianxiren,DJGXXlianxirenphone,DJGXXlianxirenemmail,DJGXXlianxirenzhiwei1,DJGXXlianxiren1,DJGXXlianxirenphone1,DJGXXlianxirenemmail1,DJGXXjiajie,DJGXXjiajie1,DJGXXjiajie2,DJGXXjiajie3,DJGXXjiajie4,DBAXXYWshen,DBAXXYWbenanhao,DBAXXYWname,DBAXXYWjibei,DBAXXYWlianxiren,DBAXXYWlianxiphone,DBAXXYWzhuantai,DBAXXYWshijian,DBAXXYWjcrq,DBAXXYWjclb,DBAXXYWjdjcjg,DBAXXYWclqk,DBAXXYWjcrq1,DBAXXYWjclb1,DBAXXYWjdjcjg1,DBAXXYWclqk1,DBAXXYWzymc,DBAXXYWzyyjz,DBAXXYWzc,DBAXXYWzybasj,DBAXXYWzymc1,DBAXXYWzyyjz1,DBAXXYWzc1,DBAXXYWzybasj1,DBAXXYLshen,DBAXXYLbenanhao,DBAXXYLname,DBAXXYLjibei,DBAXXYLlianxiren,DBAXXYLlianxiphone,DBAXXYLzhuantai,DBAXXYLshijian,DBAXXYLzymc,DBAXXYLzyyjz,DBAXXYLzc,DLLWTHphone,DLLWTHchuangzhen,DLLWTHemail,DLLWTHjiedaishijian,DLLWTHwangzhi,DLLWTHshen,DLLWTHadress,DLLWTHLXFSzhiwei,DLLWTHLXFSmingzhi,DLLWTHLXFSdianhua,DLLWTHLXFSyouxiang,DLLWTHLXFSzhiwei1,DLLWTHLXFSmingzhi1,DLLWTHLXFSdianhua1,DLLWTHLXFSyouxiang1,DLLWTHLLllzkpl,DLLWTHLLllshxs,DLLWTHLLllscfy,DLLWTHLLxgzc,DLLWTHLLzkpl,DLLWTHLLzkpl1,DLLWTHLLllzkpl1,DLLWTHLLllshxs1,DLLWTHLLllscfy1,DLLWTHLLxgzc1,DKSZYZHX1,DKSZYZHX1weizhi,DKSZYZHX1zhiwei,DKSZYZHX1mingzi,DKSZYZHX1dianhua,DKSZYZHX1email,DKSZYZHX1keshi,DKSZYZHX1yanjiutuandui,DLCSYqdlx,DLCSYgcc,DLCSYqdny,DLCSYcjcws,DLCSYjtlcdz,DLCSYgsszlx,DLCSYLXFSzhiwei,DLCSYLXFSmingzhi,DLCSYLXFSdianhua,DLCSYLXFSdizhi,DLCSYXMJY1,DLCSYXMJY11,DLCSYXMJY2,DLCSYXMJY21,DLCSYXMJY3,DLCSYXMJY31,DLCSYXMJY4,DLCSYXMJY41,DLCSYXMJY5,DLCSYXMJY51,DLCSYXMJY6,DLCSYXMJY61,DLCSYXMJY7,DLCSYXMJY71,DLCSYXMJY8,DLCSYXMJY81,DLCSYYJTD1,DLCSYGDJS1,DKSZYZHX2,DKSZYZHX2weizhi,DKSZYZHX2zhiwei,DKSZYZHX2mingzi,DKSZYZHX2dianhua,DKSZYZHX2email,DKSZYZHX2keshi,DKSZYZHX2keshi1,DKSZYZHX2yanjiutuandui,DKSZYZHX2yanjiutuandui1 FROM AirtleTable where Syurl ='" + tid +"'";
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