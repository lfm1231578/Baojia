using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Blog;
using App.Application.Blog.Dtos;
using App.Core.Entities.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Org.BouncyCastle.Utilities.Collections;
using Renci.SshNet.Security;
using Spire.Doc.Documents;
using SqlSugar;
using SqlSugar.DistributedSystem.Snowflake;
using Spire.Doc;
using System.Text;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Drawing;

namespace App.Hosting.Controllers
{
    public class ArticleController : WebController
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly ITagsService _tagsService;
        public ArticleController(IArticleService articleService,
            ICategoryService categoryService,
            ITagsService tagsService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _tagsService = tagsService;
        }
        /// <summary>
        /// 文章专栏页
        /// </summary>
        /// <param name="cid">栏目id</param>
        /// <param name="tid">标签id</param>
        /// <returns></returns>
        public IActionResult List(string cid, string tid, string code, string state, string tag, string key)
        {

            var a = tag;
            var index = 1;
            var pagesize = 10;
            if (code != "" && code != null)
            {
                index = Convert.ToInt32(code);
            }
            else
            {
            }

            if (state != "" && state != null)
            {
                pagesize = Convert.ToInt32(state);
            }

            string shenfen = "guangdong";
            if (key != null)
            {
                if (key.Split("DJGXXshen:")[1].Split("$$Dstag:")[0] != "null")
                    shenfen = key.Split("DJGXXshen:")[1].Split("$$Dstag:")[0];
            }
            string table = "AirtleTable" + "_" + shenfen;
            string where2 = "";
            string str = "SELECT  Syname,Syurl,Sytag,Sytag1,Sytag2,Sytag3,Sytag4,Syxiangmu,Syphnoe, Syjiedaishijian,Syadress  from  " + table + " order by Topdesc desc ";
            if (tag == "2")
            {
                //DJGXXshen:天津$$Dstag:1$$interest2:1$$interest3:1$$sex:国内试验$$qishu:I 期$$Dsname:null$$DLCSYgcc:null$$title:null$$title1:null$$title2:null$$title3:null$$
                str = "SELECT  Syname,Syurl,Sytag,Sytag1,Sytag2,Sytag3,Sytag4,Syxiangmu,Syphnoe, Syjiedaishijian,Syadress  from  " + table;
                string _where = " where 1= 1";
                string _desc = " order by Topdesc desc";
                var arr = key.Split("$$");
                foreach (string i in arr)
                {

                    if (i.Contains("Dsname") && i.Split(":")[1] != "null") _where += "  AND Dsname   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag") && i.Split(":")[1] != "null") _where += "  AND( Dstag   like '%" + i.Split(":")[1] + @"%' " +
                                                                                    "  OR  Dstag1   like '%" + i.Split(":")[1] + @"%' " +
                                                                                    "   OR   Dstag2   like '%" + i.Split(":")[1] + @"%' " +
                                                                                    "   OR   Dstag3   like '%" + i.Split(":")[1] + @"%' " +
                                                                                    "   OR   Dstag4   like '%" + i.Split(":")[1] + @"%' )";

                    if (i.Contains("DLCSYgcc") && i.Split(":")[1] != "null") _where += "  AND DLCSYgcc   like '%" + i.Split(":")[1] + @"%' ";


                    if (i.Contains("interest2") && i.Split(":")[1] != "null") where2 += "  AND LCSYXXshiyanfenlie   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("interest3") && i.Split(":")[1] != "null") where2 += "  AND TitleStage   like '%" + i.Split(":")[1] + @"%' ";
                    //DJGXXshen:null$$Dstag:null$$interest2:null$$interest3:null$$sex:国内试验$$qishu:I 期$$Dsname:null$$DLCSYgcc:null$$title:H$$title1:一$$title2:null$$title3:null$$
                    if (i.Contains("title") && !i.Contains("title1") && i.Split(":")[1] != "null")
                    {
                        where2 += "  AND TitleName   like '%" + i.Split(":")[1] + @"%' ";
                    }
                    if (i.Contains("title1") && i.Split(":")[1] != "null")
                    {
                        where2 += "  AND JBXXshiyingzheng   like '%" + i.Split(":")[1] + @"%' ";
                    }

                    #region



                    //if (i.Contains("Syurl") && i.Split(":")[1]  != "null") _where += "  AND Syurl   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Sytag") && i.Split(":")[1]  != "null") _where += "  AND Sytag   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Sytag1") && i.Split(":")[1]  != "null") _where += "  AND Sytag1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Sytag2") && i.Split(":")[1]  != "null") _where += "  AND Sytag2   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Sytag3") && i.Split(":")[1]  != "null") _where += "  AND Sytag3   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Sytag4") && i.Split(":")[1]  != "null") _where += "  AND Sytag4   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Syxiangmu") && i.Split(":")[1]  != "null") _where += "  AND Syxiangmu   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Syphnoe") && i.Split(":")[1]  != "null") _where += "  AND Syphnoe   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Syjiedaishijian") && i.Split(":")[1]  != "null") _where += "  AND Syjiedaishijian   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Syadress") && i.Split(":")[1]  != "null") _where += "  AND Syadress   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Dsshijian") && i.Split(":")[1]  != "null") _where += "  AND Dsshijian   like '%" + i.Split(":")[1] + @"%' ";


                    //if (i.Contains("Dstag") && i.Split(":")[1] != "null") _where += "  AND Dstag5   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Dstag") && i.Split(":")[1] != "null") _where += "  AND Dstag6   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Dsphone") && i.Split(":")[1]  != "null") _where += "  AND Dsphone   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Dsemais") && i.Split(":")[1]  != "null") _where += "  AND Dsemais   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Dsemail") && i.Split(":")[1]  != "null") _where += "  AND Dsemail   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Dsjiedaishijian") && i.Split(":")[1]  != "null") _where += "  AND Dsjiedaishijian   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Dsadress") && i.Split(":")[1]  != "null") _where += "  AND Dsadress   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Dsliurangshu") && i.Split(":")[1]  != "null") _where += "  AND Dsliurangshu   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXshen") && i.Split(":")[1] != "null") _where += "  AND DJGXXshen   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXweb") && i.Split(":")[1]  != "null") _where += "  AND DJGXXweb   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXjgwz") && i.Split(":")[1]  != "null") _where += "  AND DJGXXjgwz   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXjgzzdm") && i.Split(":")[1]  != "null") _where += "  AND DJGXXjgzzdm   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXsclxzl") && i.Split(":")[1]  != "null") _where += "  AND DJGXXsclxzl   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXscllzldj") && i.Split(":")[1]  != "null") _where += "  AND DJGXXscllzldj   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXllxllht") && i.Split(":")[1]  != "null") _where += "  AND DJGXXllxllht   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXgcp") && i.Split(":")[1]  != "null") _where += "  AND DJGXXgcp   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXzkxm") && i.Split(":")[1]  != "null") _where += "  AND DJGXXzkxm   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXglfc") && i.Split(":")[1]  != "null") _where += "  AND DJGXXglfc   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXlianxirenzhiwei") && i.Split(":")[1]  != "null") _where += "  AND DJGXXlianxirenzhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXlianxiren") && i.Split(":")[1]  != "null") _where += "  AND DJGXXlianxiren   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXlianxirenphone") && i.Split(":")[1]  != "null") _where += "  AND DJGXXlianxirenphone   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXlianxirenemmail") && i.Split(":")[1]  != "null") _where += "  AND DJGXXlianxirenemmail   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXlianxirenzhiwei1") && i.Split(":")[1]  != "null") _where += "  AND DJGXXlianxirenzhiwei1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXlianxiren1") && i.Split(":")[1]  != "null") _where += "  AND DJGXXlianxiren1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXlianxirenphone1") && i.Split(":")[1]  != "null") _where += "  AND DJGXXlianxirenphone1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXlianxirenemmail1") && i.Split(":")[1]  != "null") _where += "  AND DJGXXlianxirenemmail1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXjiajie") && i.Split(":")[1]  != "null") _where += "  AND DJGXXjiajie   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXjiajie1") && i.Split(":")[1]  != "null") _where += "  AND DJGXXjiajie1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXjiajie2") && i.Split(":")[1]  != "null") _where += "  AND DJGXXjiajie2   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXjiajie3") && i.Split(":")[1]  != "null") _where += "  AND DJGXXjiajie3   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DJGXXjiajie4") && i.Split(":")[1]  != "null") _where += "  AND DJGXXjiajie4   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWshen") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWshen   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWbenanhao") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWbenanhao   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWname") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWname   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWjibei") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWjibei   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWlianxiren") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWlianxiren   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWlianxiphone") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWlianxiphone   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWzhuantai") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWzhuantai   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWshijian") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWshijian   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWjcrq") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWjcrq   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWjclb") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWjclb   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWjdjcjg") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWjdjcjg   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWclqk") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWclqk   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWjcrq1") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWjcrq1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWjclb1") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWjclb1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWjdjcjg1") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWjdjcjg1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWclqk1") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWclqk1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWzymc") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWzymc   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWzyyjz") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWzyyjz   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWzc") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWzc   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWzybasj") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWzybasj   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWzymc1") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWzymc1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWzyyjz1") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWzyyjz1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWzc1") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWzc1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYWzybasj1") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYWzybasj1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYLshen") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYLshen   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYLbenanhao") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYLbenanhao   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYLname") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYLname   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYLjibei") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYLjibei   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYLlianxiren") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYLlianxiren   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYLlianxiphone") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYLlianxiphone   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYLzhuantai") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYLzhuantai   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYLshijian") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYLshijian   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYLzymc") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYLzymc   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYLzyyjz") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYLzyyjz   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DBAXXYLzc") && i.Split(":")[1]  != "null") _where += "  AND DBAXXYLzc   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHphone") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHphone   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHchuangzhen") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHchuangzhen   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHemail") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHemail   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHjiedaishijian") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHjiedaishijian   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHwangzhi") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHwangzhi   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHshen") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHshen   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHadress") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHadress   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLXFSzhiwei") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLXFSzhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLXFSmingzhi") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLXFSmingzhi   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLXFSdianhua") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLXFSdianhua   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLXFSyouxiang") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLXFSyouxiang   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLXFSzhiwei1") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLXFSzhiwei1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLXFSmingzhi1") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLXFSmingzhi1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLXFSdianhua1") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLXFSdianhua1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLXFSyouxiang1") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLXFSyouxiang1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLLllzkpl") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLLllzkpl   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLLllshxs") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLLllshxs   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLLllscfy") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLLllscfy   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLLxgzc") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLLxgzc   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLLzkpl") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLLzkpl   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLLzkpl1") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLLzkpl1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLLllzkpl1") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLLllzkpl1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLLllshxs1") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLLllshxs1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLLllscfy1") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLLllscfy1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLLWTHLLxgzc1") && i.Split(":")[1]  != "null") _where += "  AND DLLWTHLLxgzc1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX1") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX1weizhi") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX1weizhi   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX1zhiwei") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX1zhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX1mingzi") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX1mingzi   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX1dianhua") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX1dianhua   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX1email") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX1email   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX1keshi") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX1keshi   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX1yanjiutuandui") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX1yanjiutuandui   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYqdlx") && i.Split(":")[1]  != "null") _where += "  AND DLCSYqdlx   like '%" + i.Split(":")[1] + @"%' ";

                    //if (i.Contains("DLCSYqdny") && i.Split(":")[1]  != "null") _where += "  AND DLCSYqdny   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYcjcws") && i.Split(":")[1]  != "null") _where += "  AND DLCSYcjcws   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYjtlcdz") && i.Split(":")[1]  != "null") _where += "  AND DLCSYjtlcdz   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYgsszlx") && i.Split(":")[1]  != "null") _where += "  AND DLCSYgsszlx   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYLXFSzhiwei") && i.Split(":")[1]  != "null") _where += "  AND DLCSYLXFSzhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYLXFSmingzhi") && i.Split(":")[1]  != "null") _where += "  AND DLCSYLXFSmingzhi   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYLXFSdianhua") && i.Split(":")[1]  != "null") _where += "  AND DLCSYLXFSdianhua   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYLXFSdizhi") && i.Split(":")[1]  != "null") _where += "  AND DLCSYLXFSdizhi   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY1") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY11") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY11   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY2") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY2   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY21") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY21   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY3") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY3   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY31") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY31   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY4") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY4   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY41") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY41   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY5") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY5   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY51") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY51   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY6") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY6   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY61") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY61   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY7") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY7   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY71") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY71   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY8") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY8   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYXMJY81") && i.Split(":")[1]  != "null") _where += "  AND DLCSYXMJY81   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYYJTD1") && i.Split(":")[1]  != "null") _where += "  AND DLCSYYJTD1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DLCSYGDJS1") && i.Split(":")[1]  != "null") _where += "  AND DLCSYGDJS1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX2") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX2   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX2weizhi") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX2weizhi   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX2zhiwei") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX2zhiwei   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX2mingzi") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX2mingzi   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX2dianhua") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX2dianhua   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX2email") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX2email   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX2keshi") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX2keshi   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX2keshi1") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX2keshi1   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX2yanjiutuandui") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX2yanjiutuandui   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("DKSZYZHX2yanjiutuandui1") && i.Split(":")[1]  != "null") _where += "  AND DKSZYZHX2yanjiutuandui1   like '%" + i.Split(":")[1] + @"%' ";
                    #endregion
                }
                str = str + _where + _desc;
            }
            string name = string.Empty;
            var result = GetGoalsEntity(str);
            #region
            //string sqlzy = @"SELECT hospitalname from (SELECT   GCJJGmingcheng as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng UNION   SELECT   GCJJGmingcheng1 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng1 UNION   SELECT   GCJJGmingcheng2 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng2 UNION   SELECT   GCJJGmingcheng3 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng3 UNION   SELECT   GCJJGmingcheng4 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng4 UNION   SELECT   GCJJGmingcheng5 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng5 UNION   SELECT   GCJJGmingcheng6 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng6 UNION   SELECT   GCJJGmingcheng7 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng7 UNION   SELECT   GCJJGmingcheng8 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng8 UNION   SELECT   GCJJGmingcheng9 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng9 UNION   SELECT   GCJJGmingcheng10 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng10 UNION   SELECT   GCJJGmingcheng11 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng11 UNION   SELECT   GCJJGmingcheng12 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng12 UNION   SELECT   GCJJGmingcheng13 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng13 UNION   SELECT   GCJJGmingcheng14 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng14 UNION   SELECT   GCJJGmingcheng15 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng15 UNION   SELECT   GCJJGmingcheng16 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng16 UNION   SELECT   GCJJGmingcheng17 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng17 UNION   SELECT   GCJJGmingcheng18 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng18 UNION   SELECT   GCJJGmingcheng19 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng19 UNION   SELECT   GCJJGmingcheng20 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng20 UNION   SELECT   GCJJGmingcheng21 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng21 UNION   SELECT   GCJJGmingcheng22 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng22 UNION   SELECT   GCJJGmingcheng23 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng23 UNION   SELECT   GCJJGmingcheng24 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng24 UNION   SELECT   GCJJGmingcheng25 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng25 UNION   SELECT   GCJJGmingcheng26 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng26 UNION   SELECT   GCJJGmingcheng27 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng27 UNION   SELECT   GCJJGmingcheng28 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng28 UNION   SELECT   GCJJGmingcheng29 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng29 UNION   SELECT   GCJJGmingcheng30 as hospitalname FROM MakeTable GROUP BY  GCJJGmingcheng30 ) as t  where  hospitalname <> '' GROUP BY hospitalname";
            //var resultzy = GetGoalHospitalEntity(sqlzy, "zhaiyao");
            //IList<Articleiteminfo> newresult =new List<Articleiteminfo>();
            //foreach (var item in result) {
            //    var i = new Articleiteminfo();
            //    foreach (var items in resultzy) {
            //        if (item.Syname.ToString() == items.Syname.ToString()) {
            //            i = item;
            //        }
            //    }
            //    newresult.Add(i);
            //}
            #endregion
            var data = result.Skip((index - 1) * pagesize).Take(pagesize).ToList();
            IList<Articleiteminfo> listruslet = new List<Articleiteminfo>();
            var Stag = 0;
            if (tag != null)
            {
                foreach (var item in data)
                {
                    var Articleiteminfo = new Articleiteminfo();
                    var hn = item.Syname;
                    var stringname = "";
                    string[] arrhnnn = { hn };
                    if (hn != null)
                    {
                        if (hn.Contains("（"))
                        {
                            var arrhn = hn.Split("（");
                            var hn1 = arrhn[0];
                            var hn2 = arrhn[1];
                            if (hn2 != null)
                            {
                                if (hn2.Contains("）"))
                                {
                                    var arrhnn = hn2.Split("）");
                                    var hnn = arrhnn[0].ToString();
                                    var hnn1 = arrhnn[1];
                                    if (hnn1 == "")
                                    {
                                        if (hnn.Contains("、") || hnn.Contains("，"))
                                        {
                                            if (hnn.Contains("、"))
                                            {
                                                stringname = hn1 + "、" + hnn;
                                                arrhnnn = stringname.Split("、");
                                            }
                                            if (hnn.Contains("，"))
                                            {
                                                stringname = hn1 + "，" + hnn;
                                                arrhnnn = stringname.Split("，");
                                            }
                                        }
                                        else
                                        {
                                            stringname = hn1 + "，" + hnn;
                                            arrhnnn = stringname.Split("，");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    foreach (var ntem in arrhnnn)
                    {
                        string sqlstr = " SELECT Other, TitleName, TitleStage,JBXXshiyingzheng,JBXXshiyantongshutimu,JBXXdengjihao,LCSYXXshiyanfenlie FROM MakeTable  where  " +
                            "( GCJJGmingcheng  like  '%" + ntem.Trim().ToString().Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng1 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                            "GCJJGmingcheng2 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng3 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                            "GCJJGmingcheng4 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                            "GCJJGmingcheng5 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng6 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng7 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                            "GCJJGmingcheng8 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                            "GCJJGmingcheng9 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                            "GCJJGmingcheng10 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng11 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng12 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                            "GCJJGmingcheng13 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                            "GCJJGmingcheng14 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng15 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                            "GCJJGmingcheng16 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng17 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng18 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                            "GCJJGmingcheng19 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng20 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng21 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng22 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng23 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng24 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                            "GCJJGmingcheng25 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng26 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng27 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng28 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng29 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                            " GCJJGmingcheng30 like  '%" + ntem.Trim().ToString() + @"%' )";
                        if (where2 != "")
                        {
                            sqlstr = sqlstr + where2;
                        }
                        var zyitem = GetItemEntity(sqlstr);
                        if (zyitem != null)
                        {
                            Stag = 1;
                            item.list = zyitem;
                            item.projectcount = zyitem.Count.ToString();
                            Articleiteminfo = item;
                            listruslet.Add(Articleiteminfo);

                        }


                    }
                }
            }
            if (Stag == 0)
            {
                ViewBag.Yixue = data;
                ViewBag.Count = result.Count();
                ViewBag.PageNum = (result.Count() - 1) / pagesize + 1;

            }
            else
            {
                ViewBag.Yixue = listruslet;
                ViewBag.Count = listruslet.Count();
                ViewBag.PageNum = (listruslet.Count() - 1) / pagesize + 1;
            }
            //List<BannerInfo> list = await _bannerService.GetListCacheAsync(null, o => o.SortCode, false);
            //var article = await _articleService.GetListCacheAsync(null, o => o.CreatorTime, false);

            //ViewBag.Yixue = data;
            ViewBag.Where2 = where2;
            ViewBag.Code = index;
            ViewBag.Shenfen = shenfen;
            ViewBag.Key = key;

            string name1 = string.Empty;
            //if (!string.IsNullOrWhiteSpace(cid))
            //{
            //    name = _categoryService.FindEntity(c => c.EnabledMark && c.Id == cid)?.CategoryName;
            //}
            //if (!string.IsNullOrWhiteSpace(tid))
            //{
            //    name = _tagsService.FindEntity(c => c.EnabledMark && c.Id == tid)?.TagName;
            //}
            //ViewBag.CategoryName = "";
            return View();
        }

        public IActionResult ListPapes(string cid, string tid, string code, string state, string tag, string key, [FromServices] IWebHostEnvironment env)
        {

            var a = tag;
            var index = 1;
            var pagesize = 10;
            if (code != "" && code != null)
            {
                index = Convert.ToInt32(code);
            }
            if (state != "" && state != null)
            {
                pagesize = Convert.ToInt32(state);
            }

            string shenfen = "";
            if (key != null)
            {
                shenfen = key.Split("DJGXXshen:")[1].Split("$$Dstag:")[0];
            }
            #region



            #endregion


            string str = " ";

            string where2 = "where 1 = 1";
            string orderstr = "order by Todesc desc";
            //var result = GetItemEntityPapes(str, "zaiyao");
            var newresult = new List<Makeiteminfo>();
            string savePath = env.WebRootPath + "/sofofiter/";
            //if (shenfen == "")
            //{
            var strsql = " SELECT  title,mill,other FROM PaperPDF  WHERE postkey LIKE '%粤械注准%'   ";
            var result1 = GetItemEntityCore11(strsql);
            var result11 = result1.Skip((index - 1) * pagesize).Take(pagesize).ToList();
            foreach (var item in result11)
            {

                var i = new Makeiteminfo();
                i.JBXXdengjihao = item.other;
                i.PathName = item.other + ".pdf";

                i.Pathpapes = env.WebRootPath + "/pdf-guangdong/" + i.PathName;

                i.JBXXshiyantongshutimu = item.title;
                i.TitleName = item.mill;
                newresult.Add(i);
            }
            //} else
            //{
            //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(savePath);
            //    System.IO.FileInfo[] allFiles = dir.GetFiles(".", System.IO.SearchOption.AllDirectories);
            //    foreach (System.IO.FileInfo file in allFiles.Skip((index - 1) * pagesize).Take(pagesize).ToList())
            //    {


            //        var i = new Makeiteminfo();
            //        var filename = file.Name.Split(".")[0].Replace("详细信息", "");
            //        i.JBXXdengjihao = filename;
            //        i.PathName = file.Name;

            //        i.Pathpapes = savePath + file.Name;

            //        string name = file.Name;
            //        StringBuilder sb = new StringBuilder();
            //        Document document = new Document();

            //        document.LoadFromFile(@"/sofofiter/" + file.Name);

            //        Section section = document.Sections[0];
            //        i.JBXXshiyantongshutimu = section.Body.Tables[1].Rows[7].Cells[1].Paragraphs[0].Text.ToString();
            //        i.TitleName = section.Body.Tables[1].Rows[2].Cells[1].Paragraphs[0].Text.ToString();

            //        newresult.Add(i);
            //    }

            //}

            var data = newresult;
            ViewBag.Yixue = data;
            //ViewBag.Count = allFiles.Count();
            //ViewBag.PageNum = (allFiles.Count() - 1) / pagesize + 1;
            ViewBag.Count = result1.Count();
            ViewBag.PageNum = (result1.Count() - 1) / pagesize + 1;
            ViewBag.Code = index;
            ViewBag.Shenfen = "";
            ViewBag.Key = key;

            string name1 = string.Empty;
            return View();
        }

        private IList<Articleiteminfo1> GetItemEntityCore11(string sqlstr, string filter = "")
        {
            var list1 = new List<Articleiteminfo1>();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var i = new Articleiteminfo1();
                var date = dr;
                i.title = date["title"].ToString();
                i.mill = date["mill"].ToString();
                i.other = date["other"].ToString();
                list1.Add(i);
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }

        public IActionResult ListDetail(string cid, string tid, string code, string state, string tag, string key, string where2)
        {

            var a = tag;
            var index = 1;
            var pagesize = 10;
            if (code != "" && code != null)
            {
                index = Convert.ToInt32(code);
            }
            if (state != "" && state != null)
            {
                pagesize = Convert.ToInt32(state);
            }

            string name = string.Empty;
            IList<Makeiteminfo> zyitem = new List<Makeiteminfo>();
            IList<Makeiteminfo>[] arrzyitem = new IList<Makeiteminfo>[] { };

            var hn = "暨南大学附属第一医院（广州华侨医院）";
            if (cid != "" && cid != null)
            {
                hn = cid;
            }
            var stringname = "";
            string[] arrhnnn = { hn };
            if (hn != null)
            {
                if (hn.Contains("（"))
                {
                    var arrhn = hn.Split("（");
                    var hn1 = arrhn[0];
                    var hn2 = arrhn[1];
                    if (hn2 != null)
                    {
                        if (hn2.Contains("）"))
                        {
                            var arrhnn = hn2.Split("）");
                            var hnn = arrhnn[0].ToString();
                            var hnn1 = arrhnn[1];
                            if (hnn1 == "")
                            {
                                if (hnn.Contains("、") || hnn.Contains("，"))
                                {
                                    if (hnn.Contains("、"))
                                    {
                                        stringname = hn1 + "、" + hnn;
                                        arrhnnn = stringname.Split("、");
                                    }
                                    if (hnn.Contains("，"))
                                    {
                                        stringname = hn1 + "，" + hnn;
                                        arrhnnn = stringname.Split("，");
                                    }
                                }
                                else
                                {
                                    stringname = hn1 + "，" + hnn;
                                    arrhnnn = stringname.Split("，");
                                }
                            }
                        }
                    }
                }
            }
            int k = 0;
            foreach (var ntem in arrhnnn)
            {
                string sqlstr = " SELECT Other, TitleName, TitleStage,JBXXshiyingzheng,JBXXshiyantongshutimu,JBXXdengjihao,LCSYXXshiyanfenlie FROM MakeTable  where  " +
                    " ( GCJJGmingcheng  like  '%" + ntem.Trim().ToString().Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng1 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                    "GCJJGmingcheng2 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng3 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                    "GCJJGmingcheng4 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                    "GCJJGmingcheng5 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng6 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng7 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                    "GCJJGmingcheng8 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                    "GCJJGmingcheng9 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                    "GCJJGmingcheng10 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng11 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng12 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                    "GCJJGmingcheng13 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                    "GCJJGmingcheng14 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng15 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                    "GCJJGmingcheng16 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng17 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng18 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                    "GCJJGmingcheng19 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng20 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng21 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng22 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng23 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng24 like  '%" + ntem.Trim().ToString() + @"%' or   " +
                    "GCJJGmingcheng25 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng26 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng27 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng28 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng29 like  '%" + ntem.Trim().ToString() + @"%' or  " +
                    " GCJJGmingcheng30 like  '%" + ntem.Trim().ToString() + @"%' )";
                if (where2 != "")
                {
                    sqlstr = sqlstr + where2;
                }
                zyitem = GetItemEntity(sqlstr);
            }
            var ruslut = zyitem;// arrzyitem.Union(arrzyitem);
            var data = ruslut.Skip((index - 1) * pagesize).Take(pagesize).ToList();
            ViewBag.Yixue = data;
            ViewBag.Count = ruslut.Count();
            ViewBag.PageNum = (ruslut.Count() - 1) / pagesize + 1;
            ViewBag.Code = index;
            ViewBag.Key = key;
            ViewBag.cid = cid;
            ViewBag.Where2 = where2;
            code = "";
            return View();
        }


        public IActionResult ListItem(string cid, string tid, string code, string state, string tag, string key)
        {

            var a = tag;
            var index = 1;
            var pagesize = 10;
            if (code != "" && code != null)
            {
                index = Convert.ToInt32(code);
            }
            if (state != "" && state != null)
            {
                pagesize = Convert.ToInt32(state);
            }

            string shenfen = "";
            if (key != null)
            {
                shenfen = key.Split("DJGXXshen:")[1].Split("$$Dstag:")[0];
            }
            //string table = "AirtleTable";
            //if (shenfen != "")
            //{
            //    table = table + "_" + shenfen;
            //}
            string str = " SELECT Other, TitleName, TitleStage,JBXXshiyingzheng,JBXXshiyantongshutimu,JBXXdengjihao,LCSYXXshiyanfenlie,LCSYXXshiyanfangwei,LCSYXXshiyanfenqi FROM MakeTable ";
            string where2 = "where 1 = 1";
            string orderstr = "order by Todesc desc";
            if (key != null)
            {
                var arr = key.Split("$$");
                foreach (var item in arr)
                {

                    if (item.Contains("interest2") && item.Split(":")[1] != "null") where2 += "  AND LCSYXXshiyanfenlie   like '%" + item.Split(":")[1] + @"%' ";
                    if (item.Contains("interest3") && item.Split(":")[1] != "null") where2 += "  AND TitleStage   like '%" + item.Split(":")[1] + @"%' ";
                    if (item.Contains("title") && !item.Contains("title1") && item.Split(":")[1] != "null")
                    {
                        where2 += "  AND TitleName   like '%" + item.Split(":")[1] + @"%' ";
                    }
                    if (item.Contains("title1") && item.Split(":")[1] != "null")
                    {
                        where2 += "  AND JBXXshiyingzheng   like '%" + item.Split(":")[1] + @"%' ";
                    }
                    if (item.Contains("qishu") && item.Split(":")[1] != "null")
                    {
                        if (item.Split(":")[1] == "I 期")
                        {
                            where2 += "AND LCSYXXshiyanfenqi LIKE '%                 I期                                %' ";
                        }
                        else if (item.Split(":")[1] == "II 期")
                        {
                            where2 += "AND LCSYXXshiyanfenqi LIKE '%                 II期                                %' ";
                        }
                        else if (item.Split(":")[1] == "III 期")
                        {
                            where2 += "AND LCSYXXshiyanfenqi LIKE '%                 II期                                %' ";

                        }
                    }
                    if (item.Contains("sex") && item.Split(":")[1] != "null")
                    {
                        where2 += " AND LCSYXXshiyanfangwei  like '%" + item.Split(":")[1] + @"%'";
                    }

                }
            }
            str = str + where2 + orderstr;
            var result = GetItemEntity(str, "zaiyao");
            var data = result.Skip((index - 1) * pagesize).Take(pagesize).ToList();
            //List<BannerInfo> list = await _bannerService.GetListCacheAsync(null, o => o.SortCode, false);

            //var article = await _articleService.GetListCacheAsync(null, o => o.CreatorTime, false);
            ViewBag.Yixue = data;
            ViewBag.Count = result.Count();
            ViewBag.PageNum = (result.Count() - 1) / pagesize + 1;
            ViewBag.Code = index;
            ViewBag.Shenfen = "";
            ViewBag.Key = key;

            string name1 = string.Empty;
            return View();
        }

        private IList<Makeiteminfo> GetItemEntityPapes(string sqlstr, string filter = "")
        {
            var list1 = new List<Makeiteminfo>();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  

            while (dr.Read())//判断数据表中是否含有数据  
            {

                var i = new Makeiteminfo();
                var date = dr;
                i.Other = date["Other"].ToString();
                i.TitleName = date["TitleName"].ToString();
                i.TitleStage = date["TitleStage"].ToString();
                i.JBXXshiyingzheng = date["JBXXshiyingzheng"].ToString();
                i.JBXXshiyantongshutimu = date["JBXXshiyantongshutimu"].ToString();
                i.JBXXdengjihao = date["JBXXdengjihao"].ToString();
                i.LCSYXXshiyanfenlie = date["LCSYXXshiyanfenlie"].ToString();
                if (filter == "zaiyao")
                {
                    i.LCSYXXshiyanfenqi = date["LCSYXXshiyanfenqi"].ToString();
                    i.LCSYXXshiyanfangwei = date["LCSYXXshiyanfangwei"].ToString();
                }
                list1.Add(i);
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }

        private IList<Makeiteminfo> GetItemsEntity(string sqlstr, Articleiteminfo item)
        {
            var list1 = new List<Makeiteminfo>();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var i = new Makeiteminfo();
                var date = dr;
                i.Other = date["Other"].ToString();
                i.TitleName = date["TitleName"].ToString();
                i.TitleStage = date["TitleStage"].ToString();
                i.JBXXshiyingzheng = date["JBXXshiyingzheng"].ToString();
                i.JBXXshiyantongshutimu = date["JBXXshiyantongshutimu"].ToString();
                i.JBXXdengjihao = date["JBXXdengjihao"].ToString();
                list1.Add(i);
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }
        private IList<Makeiteminfo> GetItemEntity(string sqlstr, string filter = "")
        {
            var list1 = new List<Makeiteminfo>();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var i = new Makeiteminfo();
                var date = dr;
                i.Other = date["Other"].ToString();
                i.TitleName = date["TitleName"].ToString();
                i.TitleStage = date["TitleStage"].ToString();
                i.JBXXshiyingzheng = date["JBXXshiyingzheng"].ToString();
                i.JBXXshiyantongshutimu = date["JBXXshiyantongshutimu"].ToString();
                i.JBXXdengjihao = date["JBXXdengjihao"].ToString();
                i.LCSYXXshiyanfenlie = date["LCSYXXshiyanfenlie"].ToString();
                if (filter == "zaiyao")
                {
                    i.LCSYXXshiyanfenqi = date["LCSYXXshiyanfenqi"].ToString();
                    i.LCSYXXshiyanfangwei = date["LCSYXXshiyanfangwei"].ToString();
                }

                list1.Add(i);
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }

        private IList<Articleiteminfo> GetGoalHospitalEntity(string sqlstr, string filter = "")
        {
            var list1 = new List<Articleiteminfo>();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串  
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
                i.Syname = date["hospitalname"].ToString();

                list1.Add(i);
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }
        private IList<Articleiteminfo> GetGoalsEntity(string sqlstr, string filter = "")
        {
            var list1 = new List<Articleiteminfo>();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串  
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
        /// <summary>
        /// 首页文章
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页显示数量</param>
        /// <returns></returns>
        public IActionResult Page(ArticleQueryInputDto dto)
        {
            dto.Type = 1;
            dto.Keywords = dto.Id = string.Empty;
            dto.Sort = "IsTop desc,PublishDate desc";
            var data = _articleService.ArticleList(dto);
            if (data.count > 0)
            {
                var no = data.count * 1d / dto.Limit;
                data.count = (int)Math.Ceiling(no);
            }
            return Json(data, "yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 文章专栏列表
        /// </summary>
        /// <param name="key">关键词</param>
        /// <param name="tid">标签id</param>
        /// <param name="cid">栏目id</param>
        /// <param name="page">当前页</param>
        /// <param name="limit">每页显示的条数</param>
        /// <returns></returns>
        public IActionResult Views(string key, string tid, string cid, int page = 1, int limit = 10)
        {
            int type = 1;
            string id = "";
            id = tid ?? id;
            id = cid ?? id;
            if (!string.IsNullOrWhiteSpace(tid))
            {
                type = 2;
            }

            var dto = new ArticleQueryInputDto
            {
                Keywords = key,
                Id = id,
                Type = type,
                Limit = limit,
                Page = page,
                Sort = "IsTop desc,PublishDate desc"
            };
            var data = _articleService.ArticleList(dto);
            if (data.count > 0)
            {
                var no = data.count * 1d / limit;
                data.count = (int)Math.Ceiling(no);
            }
            return Json(data, "yyyy-MM-dd");
        }

        /// <summary>
        /// 文章详情
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns></returns>
        public async Task<IActionResult> Detail(string id)
        {
            ArticleInfo article = await _articleService.FindEntityAsync(c => c.Visible && c.Id == id);
            if (article != null)
            {
                await _articleService.UpdateAsync(f => new ArticleInfo() { ReadTimes = f.ReadTimes + 1 }, c => c.Id == id);
                article.ReadTimes += 1;
            }
            return View(article);
        }

        /// <summary>
        /// 热门文章以及分类
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Module()
        {
            var hot = await _articleService.Hot(6);
            var category = await _categoryService.GetRootCategories();
            return Json(new { hot, category });
        }
        public async Task<IActionResult> AddInfo(string name, string email, string phone, string city, string message, string other)
        {
            other = DateTime.Now.ToString();
            var rusult = string.Empty;
            if (string.IsNullOrEmpty(phone))
            {

                rusult = "手机号不能为空哦！";
                return Json(new { rusult, name, email, phone, city, message, other });

            }
            else
            {
                //if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^1[3456789]\d{9}$"))
                //{
                //    rusult = "请您输入正确的手机号！";
                //    return Json(new { rusult, name, email, phone, city, message, other });
                //}
                //else
                //{
                if (string.IsNullOrEmpty(email))
                {

                    rusult = "邮箱不能为空哦！";
                    return Json(new { rusult, name, email, phone, city, message, other });

                }
                else
                {
                    string emailStr = @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+";

                    Regex emailReg = new Regex(emailStr);
                    if (!emailReg.IsMatch(email))
                    {
                        rusult = "请您输入正确的邮箱！";
                        return Json(new { rusult, name, email, phone, city, message, other });
                    }
                    else
                    {
                        var strsql = "INSERT INTO userinfoTable(name, email, phone, city, message, other) VALUES('" +
                                       name + @"', '" + email + @"', '" + phone + @"', '" + city + @"', '" + message + @"', '" + other + @"')";
                        rusult = GetIntegralByMonth(strsql);
                        return Json(new { rusult, name, email, phone, city, message, other });
                    }
                }
                //}
            }
            //var strsql = "INSERT INTO userinfoTable(name, email, phone, city, message, other) VALUES('" +
            //               name + @"', '" + email + @"', '" + phone + @"', '" + city + @"', '" + message + @"', '" + other + @"')"; 
            //var rusult = GetIntegralByMonth(strsql);
            //return Json(new { rusult, name, email, phone, city, message, other });
        }

        public async Task<IActionResult> AddExamine(string ProjectName, string Price, string Number, string SumNum, string Type)
        {
            Type = DateTime.Now.ToString();
            var rusult = string.Empty;
                        var strsql = "INSERT INTO BaoJiaExamineTable (ProjectName, Price, Number, SumNum, Type) VALUES ('" +
                                       ProjectName + @"', '" + Price + @"', '" + Number + @"', '" + SumNum + @"', '" + Type + @"')";
                        rusult = GetIntegralByMonth(strsql);
                        return Json(new { rusult, ProjectName, Price, Number, SumNum, Type });
                  
            
        }

        public async Task<IActionResult> AddExamine1(string ProjectName, string Price, string Number, string SumNum, string Type)
        {
            Type = DateTime.Now.ToString();
            var rusult = string.Empty;
            var strsql = "INSERT INTO BaoJiaExamineTable1 (ProjectName, Price, Number, SumNum, Type) VALUES ('" +
                           ProjectName + @"', '" + Price + @"', '" + Number + @"', '" + SumNum + @"', '" + Type + @"')";
            rusult = GetIntegralByMonth(strsql);
            return Json(new { rusult, ProjectName, Price, Number, SumNum, Type });


        }
        public async Task<IActionResult> UpdateExamine(string ProjectName, string Price, string Number, string SumNum, string Type)
        {
            Type = DateTime.Now.ToString();
            string sql23 = "UPDATE TOP(1) BaoJiaExamineTable SET ProjectName='"+ ProjectName  + @"', Price='"+ Price + @"', Number='"+ Number + @"', SumNum='"+ SumNum + @"', Type='"+ Type + @"' where ProjectName='"+ ProjectName  + @"'";
            string rusult23 = GetIntegralByMonth(sql23);
            return Json(new { rusult23, ProjectName, Price, Number, SumNum, Type });


        }
        public async Task<IActionResult> UpdateExamine1(string ProjectName, string Price, string Number, string SumNum, string Type)
        {
            Type = DateTime.Now.ToString();
            string sql23 = "UPDATE TOP(1) BaoJiaExamineTable1 SET ProjectName='" + ProjectName + @"', Price='" + Price + @"', Number='" + Number + @"', SumNum='" + SumNum + @"', Type='" + Type + @"' where ProjectName='" + ProjectName + @"'";
            string rusult23 = GetIntegralByMonth(sql23);
            return Json(new { rusult23, ProjectName, Price, Number, SumNum, Type });


        }
        public async Task<IActionResult> ModuleExamine(string id, string papes, string key, string isen)
        {
            var en = "0";
            var uptitle = "";
            var downtitle = "";
            var upid = 0;
            var downid = 0;
            var _papes = "当前页papes:" + papes;
            var strsql = "";
             strsql = " SELECT ProjectName, Price, Number, SumNum, Type FROM dbo.BaoJiaExamineTable where ProjectName = '" + key + "'";

            
            var list = GetItemEntityExbyid(strsql, en);
            
            return Json(new { uptitle, upid, downtitle, downid, id, list });
        }
        public async Task<IActionResult> ModuleExamine1(string id, string papes, string key, string isen)
        {
            var en = "0";
            var uptitle = "";
            var downtitle = "";
            var upid = 0;
            var downid = 0;
            var _papes = "当前页papes:" + papes;
            var strsql = "";
            strsql = " SELECT ProjectName, Price, Number, SumNum, Type FROM dbo.BaoJiaExamineTable1 where ProjectName = '" + key + "'";


            var list = GetItemEntityExbyid(strsql, en);

            return Json(new { uptitle, upid, downtitle, downid, id, list });
        }
        public async Task<IActionResult> GetItemExEntityCore(string id, string papes, string key, string isen)
        {
            var en = "0";
            var uptitle = "";
            var downtitle = "";
            var upid = 0;
            var downid = 0;
            var _papes = "当前页papes:" + papes;
            var strsql = "";
                strsql = " SELECT ProjectName, Price, Number, SumNum, Type FROM dbo.BaoJiaExamineTable ORDER BY Type DESC";

            var list = GetItemExEntityCore(strsql, en);
            var Count = list.Count();

            return Json(new {  Count, list });
        }
        public async Task<IActionResult> GetItemExEntityCore1(string id, string papes, string key, string isen)
        {
            var en = "0";
            var uptitle = "";
            var downtitle = "";
            var upid = 0;
            var downid = 0;
            var _papes = "当前页papes:" + papes;
            var strsql = "";
            strsql = " SELECT ProjectName, Price, Number, SumNum, Type FROM dbo.BaoJiaExamineTable1 ORDER BY Type DESC";

            var list = GetItemExEntityCore(strsql, en);
            var Count = list.Count();

            return Json(new { Count, list });
        }


        public async Task<IActionResult> AddYLQXclinical(string wenxunriqi, string changping_zhongwen, string changping_kehugongshimingcheng, string changping_yingwen, string changping_gongshiwangzhi, string kehulianchengshiyanfuzheren, string lianxidianhua, string lianxiyouxinag, string lianxidizhi, string yuguchangpingjiaqian, string yuguchangpingfenlie, string yewuliexinjihuifushijianxunhao, string yewuliexinjihuifushijiandanxiang, string qiyexinxi_zhucezhijin, string qiyexinxi_qiyeyuangongrenshu, string qiyexinxi_muqianshichangzhudachangping, string qiyexinxi_nianyingyeer, string qiyexinxi_qiyexinzhi, string qiyexinxi_chenglishijian, string jichutiaojian_ischangfang, string jichutiaojian_isyingjianshebei, string cpxx_yuqiyongtu, string cpxx_changpingjieguo, string cpxx_muqianchuyunagejieduan, string cpxx_nadaojianchebaogaoshijian, string cpxx_weisongjianyujisongjianshijian, string cpxx_changpingyanfalaiyuan, string cpxx_linchangzhenduanhuozhiliaojili, string cpxx_zhiliaochangping_sheyingzhen, string cpxx_zhiliaochangping_zhiliaoshijian, string cpxx_zhiliaochangping_lianchuangpingjia, string cpxx_jiancheliechangping_jianchedemubiaorenqun, string cpxx_jiancheliechangping_jianchebuweihuojianche, string cpxx_jiancheliechangping_jianchefangfayuanli, string cpxx_jiancheliechangping_jianchezhibiao, string cpxx_jiancheliechangping_yuqiyongtu, string cpxx_yujiyingyongbenchangping, string cpxx_changpingyongyubingrenshidefeiyong, string tlcpxx_tongliechangpingmingcheng1, string tlcpxx_shengchangchangjia1, string tlcpxx_yiyuanyongyubingrenzhengduan1, string tlcpxx_tongliechangpingmingcheng2, string tlcpxx_shengchangchangjia2, string tlcpxx_yiyuanyongyubingrenzhengduan2, string tlcpxx_tongliechangpingmingcheng3, string tlcpxx_shengchangchangjia3, string tlcpxx_yiyuanyongyubingrenzhengduan3, string duilianchuanjiguodeyaoqui, string duilianchuanjiguodeyaoquineirong, string kehudefeiyongyushuan, string kehufeiji, string wenxunrenqianzhiqueren, string shangwujingliqianzhiqueren, string other1, string other2)
        {


            //var rusult = "";
            //if (string.IsNullOrEmpty(cpxx_yuqiyongtu))
            //{
            //    rusult = "您的产品信息的预期用途、主要原理或方法不能为空哦！";
            //    return Json(new { rusult, wenxunriqi, changping_zhongwen, changping_kehugongshimingcheng, changping_yingwen, changping_gongshiwangzhi, kehulianchengshiyanfuzheren, lianxidianhua, lianxiyouxinag, lianxidizhi, yuguchangpingjiaqian, yuguchangpingfenlie, yewuliexinjihuifushijianxunhao, yewuliexinjihuifushijiandanxiang, qiyexinxi_zhucezhijin, qiyexinxi_qiyeyuangongrenshu, qiyexinxi_muqianshichangzhudachangping, qiyexinxi_nianyingyeer, qiyexinxi_qiyexinzhi, qiyexinxi_chenglishijian, jichutiaojian_ischangfang, jichutiaojian_isyingjianshebei, cpxx_yuqiyongtu, cpxx_changpingjieguo, cpxx_muqianchuyunagejieduan, cpxx_nadaojianchebaogaoshijian, cpxx_weisongjianyujisongjianshijian, cpxx_changpingyanfalaiyuan, cpxx_linchangzhenduanhuozhiliaojili, cpxx_zhiliaochangping_sheyingzhen, cpxx_zhiliaochangping_zhiliaoshijian, cpxx_zhiliaochangping_lianchuangpingjia, cpxx_jiancheliechangping_jianchedemubiaorenqun, cpxx_jiancheliechangping_jianchebuweihuojianche, cpxx_jiancheliechangping_jianchefangfayuanli, cpxx_jiancheliechangping_jianchezhibiao, cpxx_jiancheliechangping_yuqiyongtu, cpxx_yujiyingyongbenchangping, cpxx_changpingyongyubingrenshidefeiyong, tlcpxx_tongliechangpingmingcheng1, tlcpxx_shengchangchangjia1, tlcpxx_yiyuanyongyubingrenzhengduan1, tlcpxx_tongliechangpingmingcheng2, tlcpxx_shengchangchangjia2, tlcpxx_yiyuanyongyubingrenzhengduan2, tlcpxx_tongliechangpingmingcheng3, tlcpxx_shengchangchangjia3, tlcpxx_yiyuanyongyubingrenzhengduan3, duilianchuanjiguodeyaoqui, duilianchuanjiguodeyaoquineirong, kehudefeiyongyushuan, kehufeiji, wenxunrenqianzhiqueren, shangwujingliqianzhiqueren, other1, other2 });


            //}
            //else { }
            other2 = DateTime.Now.ToString();
            var strsql = @"INSERT INTO YLQX_clinical ( wenxunriqi, changping_zhongwen, changping_kehugongshimingcheng, changping_yingwen, changping_gongshiwangzhi, kehulianchengshiyanfuzheren, lianxidianhua, lianxiyouxinag, lianxidizhi, yuguchangpingjiaqian, yuguchangpingfenlie, yewuliexinjihuifushijianxunhao, yewuliexinjihuifushijiandanxiang, qiyexinxi_zhucezhijin, qiyexinxi_qiyeyuangongrenshu, qiyexinxi_muqianshichangzhudachangping, qiyexinxi_nianyingyeer, qiyexinxi_qiyexinzhi, qiyexinxi_chenglishijian, jichutiaojian_ischangfang, jichutiaojian_isyingjianshebei, cpxx_yuqiyongtu, cpxx_changpingjieguo, cpxx_muqianchuyunagejieduan, cpxx_nadaojianchebaogaoshijian, cpxx_weisongjianyujisongjianshijian, cpxx_changpingyanfalaiyuan, cpxx_linchangzhenduanhuozhiliaojili, cpxx_zhiliaochangping_sheyingzhen, cpxx_zhiliaochangping_zhiliaoshijian, cpxx_zhiliaochangping_lianchuangpingjia, cpxx_jiancheliechangping_jianchedemubiaorenqun, cpxx_jiancheliechangping_jianchebuweihuojianche, cpxx_jiancheliechangping_jianchefangfayuanli, cpxx_jiancheliechangping_jianchezhibiao, cpxx_jiancheliechangping_yuqiyongtu, cpxx_yujiyingyongbenchangping, cpxx_changpingyongyubingrenshidefeiyong, tlcpxx_tongliechangpingmingcheng1, tlcpxx_shengchangchangjia1, tlcpxx_yiyuanyongyubingrenzhengduan1, tlcpxx_tongliechangpingmingcheng2, tlcpxx_shengchangchangjia2, tlcpxx_yiyuanyongyubingrenzhengduan2, tlcpxx_tongliechangpingmingcheng3, tlcpxx_shengchangchangjia3, tlcpxx_yiyuanyongyubingrenzhengduan3, duilianchuanjiguodeyaoqui, duilianchuanjiguodeyaoquineirong, kehudefeiyongyushuan, kehufeiji, wenxunrenqianzhiqueren, shangwujingliqianzhiqueren,other1,other2) VALUES ('" +
                                        wenxunriqi + @"', '" +
                                        changping_zhongwen + @"', '" +
                                        changping_kehugongshimingcheng + @"', '" +
                                        changping_yingwen + @"', '" +
                                        changping_gongshiwangzhi + @"', '" +
                                        kehulianchengshiyanfuzheren + @"', '" +
                                        lianxidianhua + @"', '" +
                                        lianxiyouxinag + @"', '" +
                                        lianxidizhi + @"', '" +
                                        yuguchangpingjiaqian + @"', '" +
                                        yuguchangpingfenlie + @"', '" +
                                        yewuliexinjihuifushijianxunhao + @"', '" +
                                        yewuliexinjihuifushijiandanxiang + @"', '" +
                                        qiyexinxi_zhucezhijin + @"', '" +
                                        qiyexinxi_qiyeyuangongrenshu + @"', '" +
                                        qiyexinxi_muqianshichangzhudachangping + @"', '" +
                                        qiyexinxi_nianyingyeer + @"', '" +
                                        qiyexinxi_qiyexinzhi + @"', '" +
                                        qiyexinxi_chenglishijian + @"', '" +
                                        jichutiaojian_ischangfang + @"', '" +
                                        jichutiaojian_isyingjianshebei + @"', '" +
                                        cpxx_yuqiyongtu + @"', '" +
                                        cpxx_changpingjieguo + @"', '" +
                                        cpxx_muqianchuyunagejieduan + @"', '" +
                                        cpxx_nadaojianchebaogaoshijian + @"', '" +
                                        cpxx_weisongjianyujisongjianshijian + @"', '" +
                                        cpxx_changpingyanfalaiyuan + @"', '" +
                                        cpxx_linchangzhenduanhuozhiliaojili + @"', '" +
                                        cpxx_zhiliaochangping_sheyingzhen + @"', '" +
                                        cpxx_zhiliaochangping_zhiliaoshijian + @"', '" +
                                        cpxx_zhiliaochangping_lianchuangpingjia + @"', '" +
                                        cpxx_jiancheliechangping_jianchedemubiaorenqun + @"', '" +
                                        cpxx_jiancheliechangping_jianchebuweihuojianche + @"', '" +
                                        cpxx_jiancheliechangping_jianchefangfayuanli + @"', '" +
                                        cpxx_jiancheliechangping_jianchezhibiao + @"', '" +
                                        cpxx_jiancheliechangping_yuqiyongtu + @"', '" +
                                        cpxx_yujiyingyongbenchangping + @"', '" +
                                        cpxx_changpingyongyubingrenshidefeiyong + @"', '" +
                                        tlcpxx_tongliechangpingmingcheng1 + @"', '" +
                                        tlcpxx_shengchangchangjia1 + @"', '" +
                                        tlcpxx_yiyuanyongyubingrenzhengduan1 + @"', '" +
                                        tlcpxx_tongliechangpingmingcheng2 + @"', '" +
                                        tlcpxx_shengchangchangjia2 + @"', '" +
                                        tlcpxx_yiyuanyongyubingrenzhengduan2 + @"', '" +
                                        tlcpxx_tongliechangpingmingcheng3 + @"', '" +
                                        tlcpxx_shengchangchangjia3 + @"', '" +
                                        tlcpxx_yiyuanyongyubingrenzhengduan3 + @"', '" +
                                        duilianchuanjiguodeyaoqui + @"', '" +
                                        duilianchuanjiguodeyaoquineirong + @"', '" +
                                        kehudefeiyongyushuan + @"', '" +
                                        kehufeiji + @"', '" +
                                        wenxunrenqianzhiqueren + @"', '" +
                                        shangwujingliqianzhiqueren + @"', '" +
                                        other1 + @"', '" +
                                        other2 + @"')";
            var rusult = GetIntegralByMonth(strsql);
            return Json(new { rusult, wenxunriqi, changping_zhongwen, changping_kehugongshimingcheng, changping_yingwen, changping_gongshiwangzhi, kehulianchengshiyanfuzheren, lianxidianhua, lianxiyouxinag, lianxidizhi, yuguchangpingjiaqian, yuguchangpingfenlie, yewuliexinjihuifushijianxunhao, yewuliexinjihuifushijiandanxiang, qiyexinxi_zhucezhijin, qiyexinxi_qiyeyuangongrenshu, qiyexinxi_muqianshichangzhudachangping, qiyexinxi_nianyingyeer, qiyexinxi_qiyexinzhi, qiyexinxi_chenglishijian, jichutiaojian_ischangfang, jichutiaojian_isyingjianshebei, cpxx_yuqiyongtu, cpxx_changpingjieguo, cpxx_muqianchuyunagejieduan, cpxx_nadaojianchebaogaoshijian, cpxx_weisongjianyujisongjianshijian, cpxx_changpingyanfalaiyuan, cpxx_linchangzhenduanhuozhiliaojili, cpxx_zhiliaochangping_sheyingzhen, cpxx_zhiliaochangping_zhiliaoshijian, cpxx_zhiliaochangping_lianchuangpingjia, cpxx_jiancheliechangping_jianchedemubiaorenqun, cpxx_jiancheliechangping_jianchebuweihuojianche, cpxx_jiancheliechangping_jianchefangfayuanli, cpxx_jiancheliechangping_jianchezhibiao, cpxx_jiancheliechangping_yuqiyongtu, cpxx_yujiyingyongbenchangping, cpxx_changpingyongyubingrenshidefeiyong, tlcpxx_tongliechangpingmingcheng1, tlcpxx_shengchangchangjia1, tlcpxx_yiyuanyongyubingrenzhengduan1, tlcpxx_tongliechangpingmingcheng2, tlcpxx_shengchangchangjia2, tlcpxx_yiyuanyongyubingrenzhengduan2, tlcpxx_tongliechangpingmingcheng3, tlcpxx_shengchangchangjia3, tlcpxx_yiyuanyongyubingrenzhengduan3, duilianchuanjiguodeyaoqui, duilianchuanjiguodeyaoquineirong, kehudefeiyongyushuan, kehufeiji, wenxunrenqianzhiqueren, shangwujingliqianzhiqueren, other1, other2 });
        }
        public async Task<IActionResult> AddIVDclinical(string wenxunriqi, string changping_zhongwen, string changping_kehugongshimingcheng, string changping_yingwen, string changping_gongshiwangzhi, string kehulianchengshiyanfuzheren, string lianxidianhua, string lianxiyouxinag, string lianxidizhi, string yuguchangpingjiaqian, string yuguchangpingfenlie, string yewuliexinjihuifushijianxunhao, string yewuliexinjihuifushijiandanxiang, string qiyexinxi_zhucezhijin, string qiyexinxi_qiyeyuangongrenshu, string qiyexinxi_muqianshichangzhudachangping, string qiyexinxi_nianyingyeer, string qiyexinxi_qiyexinzhi, string qiyexinxi_chenglishijian, string jichutiaojian_ischangfang, string jichutiaojian_isyingjianshebei, string cpxx_yuqiyongtu, string cpxx_zhuchechangpingzhucheng, string cpxx_muqianchuyunagejieduan, string cpxx_changpingyanfalaiyuan, string cpxx_isyoupeitaoyiqi, string cpxx_lianchuangyiyi, string cpxx_jianchemubiaorenqun, string cpxx_dingxinhuodingliang, string cpxx_jianchebuweihuojiancheyanben, string cpxx_yangbencaijiteshuyaoqui, string cpxx_yanbenbaochuntiaojian, string cpxx_shijihebaochuntiaojian, string cpxx_jianchefangfa, string cpxx_jianchezhibiao, string cpxx_yujiyingyongbenchangping, string cpxx_changpingyongyubingrenshidefeiyong, string tlcpxx_tongliechangpingmingcheng1, string tlcpxx_shengchangchangjia2, string tlcpxx_yiyuanyongyubingrenzhengduan2, string tlcpxx_tongliechangpingmingcheng2, string tlcpxx_shengchangchangjia3, string tlcpxx_yiyuanyongyubingrenzhengduan3, string tlcpxx_tongliechangpingmingcheng3, string tlcpxx_shengchangchangjia4, string tlcpxx_yiyuanyongyubingrenzhengduan4, string tlcpxx_tongliechangpingmingcheng4, string tlcpxx_is, string duilianchuanjiguodeyaoqui, string duilianchuanjiguodeyaoquineirong, string kehudefeiyongyushuan, string kehufeiji, string wenxunrenqianzhiqueren, string shangwujingliqianzhiqueren, string isyiqinadaozhuchuzhen, string isyiqinadaozhucebaogao, string other1, string other2)
        {
            var rusult = "";
            //if (string.IsNullOrEmpty(cpxx_yuqiyongtu))
            //{
            //    rusult = "您的产品信息的预期用途、主要原理或方法不能为空哦！";
            //    return Json(new { rusult, wenxunriqi, changping_zhongwen, changping_kehugongshimingcheng, changping_yingwen, changping_gongshiwangzhi, kehulianchengshiyanfuzheren, lianxidianhua, lianxiyouxinag, lianxidizhi, yuguchangpingjiaqian, yuguchangpingfenlie, yewuliexinjihuifushijianxunhao, yewuliexinjihuifushijiandanxiang, qiyexinxi_zhucezhijin, qiyexinxi_qiyeyuangongrenshu, qiyexinxi_muqianshichangzhudachangping, qiyexinxi_nianyingyeer, qiyexinxi_qiyexinzhi, qiyexinxi_chenglishijian, jichutiaojian_ischangfang, jichutiaojian_isyingjianshebei, cpxx_yuqiyongtu, cpxx_zhuchechangpingzhucheng, cpxx_muqianchuyunagejieduan, cpxx_changpingyanfalaiyuan, cpxx_isyoupeitaoyiqi, cpxx_lianchuangyiyi, cpxx_jianchemubiaorenqun, cpxx_dingxinhuodingliang, cpxx_jianchebuweihuojiancheyanben, cpxx_yangbencaijiteshuyaoqui, cpxx_yanbenbaochuntiaojian, cpxx_shijihebaochuntiaojian, cpxx_jianchefangfa, cpxx_jianchezhibiao, cpxx_yujiyingyongbenchangping, cpxx_changpingyongyubingrenshidefeiyong, tlcpxx_tongliechangpingmingcheng1, tlcpxx_shengchangchangjia2, tlcpxx_yiyuanyongyubingrenzhengduan2, tlcpxx_tongliechangpingmingcheng2, tlcpxx_shengchangchangjia3, tlcpxx_yiyuanyongyubingrenzhengduan3, tlcpxx_tongliechangpingmingcheng3, tlcpxx_shengchangchangjia4, tlcpxx_yiyuanyongyubingrenzhengduan4, tlcpxx_tongliechangpingmingcheng4, tlcpxx_is, duilianchuanjiguodeyaoqui, duilianchuanjiguodeyaoquineirong, kehudefeiyongyushuan, kehufeiji, wenxunrenqianzhiqueren, shangwujingliqianzhiqueren, isyiqinadaozhuchuzhen, isyiqinadaozhucebaogao, other1, other2 });

            //}
            //else { }
            //if (string.IsNullOrEmpty(cpxx_zhuchechangpingzhucheng))
            //{
            //    rusult = "您的产品信息的注册产品的组成不能为空哦！";
            //    return Json(new { rusult, wenxunriqi, changping_zhongwen, changping_kehugongshimingcheng, changping_yingwen, changping_gongshiwangzhi, kehulianchengshiyanfuzheren, lianxidianhua, lianxiyouxinag, lianxidizhi, yuguchangpingjiaqian, yuguchangpingfenlie, yewuliexinjihuifushijianxunhao, yewuliexinjihuifushijiandanxiang, qiyexinxi_zhucezhijin, qiyexinxi_qiyeyuangongrenshu, qiyexinxi_muqianshichangzhudachangping, qiyexinxi_nianyingyeer, qiyexinxi_qiyexinzhi, qiyexinxi_chenglishijian, jichutiaojian_ischangfang, jichutiaojian_isyingjianshebei, cpxx_yuqiyongtu, cpxx_zhuchechangpingzhucheng, cpxx_muqianchuyunagejieduan, cpxx_changpingyanfalaiyuan, cpxx_isyoupeitaoyiqi, cpxx_lianchuangyiyi, cpxx_jianchemubiaorenqun, cpxx_dingxinhuodingliang, cpxx_jianchebuweihuojiancheyanben, cpxx_yangbencaijiteshuyaoqui, cpxx_yanbenbaochuntiaojian, cpxx_shijihebaochuntiaojian, cpxx_jianchefangfa, cpxx_jianchezhibiao, cpxx_yujiyingyongbenchangping, cpxx_changpingyongyubingrenshidefeiyong, tlcpxx_tongliechangpingmingcheng1, tlcpxx_shengchangchangjia2, tlcpxx_yiyuanyongyubingrenzhengduan2, tlcpxx_tongliechangpingmingcheng2, tlcpxx_shengchangchangjia3, tlcpxx_yiyuanyongyubingrenzhengduan3, tlcpxx_tongliechangpingmingcheng3, tlcpxx_shengchangchangjia4, tlcpxx_yiyuanyongyubingrenzhengduan4, tlcpxx_tongliechangpingmingcheng4, tlcpxx_is, duilianchuanjiguodeyaoqui, duilianchuanjiguodeyaoquineirong, kehudefeiyongyushuan, kehufeiji, wenxunrenqianzhiqueren, shangwujingliqianzhiqueren, isyiqinadaozhuchuzhen, isyiqinadaozhucebaogao, other1, other2 });
            //}
            //else { }
            other2 = DateTime.Now.ToString();
            var strsql = @"INSERT INTO IVD_clinical ( wenxunriqi, changping_zhongwen, changping_kehugongshimingcheng, changping_yingwen, changping_gongshiwangzhi, kehulianchengshiyanfuzheren, lianxidianhua, lianxiyouxinag, lianxidizhi, yuguchangpingjiaqian, yuguchangpingfenlie, yewuliexinjihuifushijianxunhao, yewuliexinjihuifushijiandanxiang, qiyexinxi_zhucezhijin, qiyexinxi_qiyeyuangongrenshu, qiyexinxi_muqianshichangzhudachangping, qiyexinxi_nianyingyeer, qiyexinxi_qiyexinzhi, qiyexinxi_chenglishijian, jichutiaojian_ischangfang, jichutiaojian_isyingjianshebei, cpxx_yuqiyongtu, cpxx_zhuchechangpingzhucheng, cpxx_muqianchuyunagejieduan, cpxx_changpingyanfalaiyuan, cpxx_isyoupeitaoyiqi, cpxx_lianchuangyiyi, cpxx_jianchemubiaorenqun, cpxx_dingxinhuodingliang, cpxx_jianchebuweihuojiancheyanben, cpxx_yangbencaijiteshuyaoqui, cpxx_yanbenbaochuntiaojian, cpxx_shijihebaochuntiaojian, cpxx_jianchefangfa, cpxx_jianchezhibiao, cpxx_yujiyingyongbenchangping, cpxx_changpingyongyubingrenshidefeiyong, tlcpxx_tongliechangpingmingcheng1, tlcpxx_shengchangchangjia2, tlcpxx_yiyuanyongyubingrenzhengduan2, tlcpxx_tongliechangpingmingcheng2, tlcpxx_shengchangchangjia3, tlcpxx_yiyuanyongyubingrenzhengduan3, tlcpxx_tongliechangpingmingcheng3, tlcpxx_shengchangchangjia4, tlcpxx_yiyuanyongyubingrenzhengduan4, tlcpxx_tongliechangpingmingcheng4, tlcpxx_is, duilianchuanjiguodeyaoqui, duilianchuanjiguodeyaoquineirong, kehudefeiyongyushuan, kehufeiji, wenxunrenqianzhiqueren, shangwujingliqianzhiqueren,isyiqinadaozhuchuzhen,isyiqinadaozhucebaogao,other1,other2) VALUES ('" +
                            wenxunriqi + @"', '" +
                            changping_zhongwen + @"', '" +
                            changping_kehugongshimingcheng + @"', '" +
                            changping_yingwen + @"', '" +
                            changping_gongshiwangzhi + @"', '" +
                            kehulianchengshiyanfuzheren + @"', '" +
                            lianxidianhua + @"', '" +
                            lianxiyouxinag + @"', '" +
                            lianxidizhi + @"', '" +
                            yuguchangpingjiaqian + @"', '" +
                            yuguchangpingfenlie + @"', '" +
                            yewuliexinjihuifushijianxunhao + @"', '" +
                            yewuliexinjihuifushijiandanxiang + @"', '" +
                            qiyexinxi_zhucezhijin + @"', '" +
                            qiyexinxi_qiyeyuangongrenshu + @"', '" +
                            qiyexinxi_muqianshichangzhudachangping + @"', '" +
                            qiyexinxi_nianyingyeer + @"', '" +
                            qiyexinxi_qiyexinzhi + @"', '" +
                            qiyexinxi_chenglishijian + @"', '" +
                            jichutiaojian_ischangfang + @"', '" +
                            jichutiaojian_isyingjianshebei + @"', '" +
                            cpxx_yuqiyongtu + @"', '" +
                            cpxx_zhuchechangpingzhucheng + @"', '" +
                            cpxx_muqianchuyunagejieduan + @"', '" +
                            cpxx_changpingyanfalaiyuan + @"', '" +
                            cpxx_isyoupeitaoyiqi + @"', '" +
                            cpxx_lianchuangyiyi + @"', '" +
                            cpxx_jianchemubiaorenqun + @"', '" +
                            cpxx_dingxinhuodingliang + @"', '" +
                            cpxx_jianchebuweihuojiancheyanben + @"', '" +
                            cpxx_yangbencaijiteshuyaoqui + @"', '" +
                            cpxx_yanbenbaochuntiaojian + @"', '" +
                            cpxx_shijihebaochuntiaojian + @"', '" +
                            cpxx_jianchefangfa + @"', '" +
                            cpxx_jianchezhibiao + @"', '" +
                            cpxx_yujiyingyongbenchangping + @"', '" +
                            cpxx_changpingyongyubingrenshidefeiyong + @"', '" +
                            tlcpxx_tongliechangpingmingcheng1 + @"', '" +
                            tlcpxx_shengchangchangjia2 + @"', '" +
                            tlcpxx_yiyuanyongyubingrenzhengduan2 + @"', '" +
                            tlcpxx_tongliechangpingmingcheng2 + @"', '" +
                            tlcpxx_shengchangchangjia3 + @"', '" +
                            tlcpxx_yiyuanyongyubingrenzhengduan3 + @"', '" +
                            tlcpxx_tongliechangpingmingcheng3 + @"', '" +
                            tlcpxx_shengchangchangjia4 + @"', '" +
                            tlcpxx_yiyuanyongyubingrenzhengduan4 + @"', '" +
                            tlcpxx_tongliechangpingmingcheng4 + @"', '" +
                            tlcpxx_is + @"', '" +
                            duilianchuanjiguodeyaoqui + @"', '" +
                            duilianchuanjiguodeyaoquineirong + @"', '" +
                            kehudefeiyongyushuan + @"', '" +
                            kehufeiji + @"', '" +
                            wenxunrenqianzhiqueren + @"', '" +
                            shangwujingliqianzhiqueren + @"', '" +
                            isyiqinadaozhuchuzhen + @"', '" +
                            isyiqinadaozhucebaogao + @"', '" +
                            other1 + @"', '" +
                            other2 + @"')";
            rusult = GetIntegralByMonth(strsql);
            return Json(new { rusult, wenxunriqi, changping_zhongwen, changping_kehugongshimingcheng, changping_yingwen, changping_gongshiwangzhi, kehulianchengshiyanfuzheren, lianxidianhua, lianxiyouxinag, lianxidizhi, yuguchangpingjiaqian, yuguchangpingfenlie, yewuliexinjihuifushijianxunhao, yewuliexinjihuifushijiandanxiang, qiyexinxi_zhucezhijin, qiyexinxi_qiyeyuangongrenshu, qiyexinxi_muqianshichangzhudachangping, qiyexinxi_nianyingyeer, qiyexinxi_qiyexinzhi, qiyexinxi_chenglishijian, jichutiaojian_ischangfang, jichutiaojian_isyingjianshebei, cpxx_yuqiyongtu, cpxx_zhuchechangpingzhucheng, cpxx_muqianchuyunagejieduan, cpxx_changpingyanfalaiyuan, cpxx_isyoupeitaoyiqi, cpxx_lianchuangyiyi, cpxx_jianchemubiaorenqun, cpxx_dingxinhuodingliang, cpxx_jianchebuweihuojiancheyanben, cpxx_yangbencaijiteshuyaoqui, cpxx_yanbenbaochuntiaojian, cpxx_shijihebaochuntiaojian, cpxx_jianchefangfa, cpxx_jianchezhibiao, cpxx_yujiyingyongbenchangping, cpxx_changpingyongyubingrenshidefeiyong, tlcpxx_tongliechangpingmingcheng1, tlcpxx_shengchangchangjia2, tlcpxx_yiyuanyongyubingrenzhengduan2, tlcpxx_tongliechangpingmingcheng2, tlcpxx_shengchangchangjia3, tlcpxx_yiyuanyongyubingrenzhengduan3, tlcpxx_tongliechangpingmingcheng3, tlcpxx_shengchangchangjia4, tlcpxx_yiyuanyongyubingrenzhengduan4, tlcpxx_tongliechangpingmingcheng4, tlcpxx_is, duilianchuanjiguodeyaoqui, duilianchuanjiguodeyaoquineirong, kehudefeiyongyushuan, kehufeiji, wenxunrenqianzhiqueren, shangwujingliqianzhiqueren, isyiqinadaozhuchuzhen, isyiqinadaozhucebaogao, other1, other2 });
        }
        private string GetIntegralByMonth(string sqlstr, int filter = 0)
        {

            string month = "失败";
            try
            {
                string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串   
                SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
                conText.Open();//打开数据库  
                string sqls = sqlstr;//创建统计语句  
                SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
                SqlDataReader dr;//创建DataReader对象  
                dr = comText.ExecuteReader();//执行查询  
                //while (dr.Read())//判断数据表中是否含有数据  
                //{
                //    var date = dr;
                //    if (filter == 0)
                //    {
                //        month = date["IntegralByMonth"].ToString();//到店次数
                //    } 
                //    else
                //    {
                //        month = date["AttributeValue"].ToString();//其他       
                //    }
                //}
                month = "成功";
                dr.Close();//关闭DataReader对象  
            }
            catch
            {

            }
            return month;

        }
        public async Task<IActionResult> ModuleNew(string id, string papes, string cid, string isen)
        {
            var en = "0";
            if (isen != null && isen != "")
            {
                en = isen;
            }
            int ids = 2287;
            if (id != null && id != "")
            {
                ids = Convert.ToInt32(id);
            }
            var uptitle = "";
            var downtitle = "";
            var upid = 0;
            var downid = 0;
            var _papes = "当前页papes:" + papes;
            var strsql = " SELECT id, title, brief, coverImage, contentBody, typeId, sort, isPub, isDel, pv, createTime, updateTime, modular, entitle, enbrief, encontentBody, encreateTime FROM CoreCmsNews where id =" + ids;
            var list = GetItemEntityCorebyid(strsql, en);
            if (ids > 2276)
            {
                upid = ids - 1;
                var uplist = "SELECT id, title, brief, coverImage, contentBody, typeId, sort, isPub, isDel, pv, createTime, updateTime, modular, entitle, enbrief, encontentBody, encreateTime FROM CoreCmsNews where id =" + upid;
                var updata = GetItemEntityCorebyid(uplist, en);
                uptitle = updata.title;
            }
            if (ids < 2312)
            {
                downid = ids + 1;
                var downlist = "SELECT id, title, brief, coverImage, contentBody, typeId, sort, isPub, isDel, pv, createTime, updateTime, modular, entitle, enbrief, encontentBody, encreateTime FROM CoreCmsNews where id =" + downid;
                var _id = "id:" + id;
                var downdata = GetItemEntityCorebyid(downlist, en);

                downtitle = downdata.title;

            }
            if (en == "0")
            { }
            else
            {
                if (list.createTime != null)
                    list.createTime = Convert.ToDateTime(list.createTime).ToString("dd-MMM-yyyy", new System.Globalization.CultureInfo("en-US"));
            }
            return Json(new { uptitle, upid, downtitle, downid, id, list });
        }
        private ArticleExDto GetItemEntityExbyid(string sqlstr, string filter = "")
        {
            var i = new ArticleExDto();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  

            while (dr.Read())//判断数据表中是否含有数据  
            {
                var date = dr;

                i.Title = date["ProjectName"].ToString();
                i.Summary = date["Price"].ToString();
                i.Source = date["Number"].ToString();
                i.SourceLink = date["SumNum"].ToString();
                i.Author = date["Type"].ToString();
            }
            dr.Close();//关闭DataReader对象  
            return i;
        }
        private IList<ArticleExDto> GetItemExEntityCore(string sqlstr, string filter = "")
        {
            var list1 = new List<ArticleExDto>();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var i = new ArticleExDto();
                var date = dr;

                i.Title = date["ProjectName"].ToString();
                i.Summary = date["Price"].ToString();
                i.Source = date["Number"].ToString();
                i.SourceLink = date["SumNum"].ToString();
                i.Author = date["Type"].ToString();


                list1.Add(i);
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }
        private CoreCmsNews GetItemEntityCorebyid(string sqlstr, string filter = "")
        {
            var i = new CoreCmsNews();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=CoreShopProfessional";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  

            while (dr.Read())//判断数据表中是否含有数据  
            {
                var date = dr;
                i.id = Convert.ToInt32(date["id"]);
                i.createTime = Convert.ToDateTime(date["createTime"].ToString() == null ? DateTime.Now.ToString() : date["createTime"].ToString()).ToLongDateString();
                if (filter == "0")
                {
                    i.title = date["title"].ToString();
                    i.brief = date["brief"].ToString();
                    i.contentBody = date["contentBody"].ToString();
                    i.createTime = Convert.ToDateTime(date["createTime"].ToString() == null ? DateTime.Now.ToString() : date["createTime"].ToString()).ToLongDateString();
                }
                else if (filter == "1")
                {
                    i.title = date["entitle"].ToString();
                    i.brief = date["enbrief"].ToString();
                    i.contentBody = date["encontentBody"].ToString();
                    //i.createTime = Convert.ToDateTime(date["encreateTime"].ToString() == null ? DateTime.Now.ToString() : date["encreateTime"].ToString()).ToLongDateString();
                }

                i.coverImage = date["coverImage"].ToString();
                i.typeId = Convert.ToInt32(date["typeId"]);
                i.sort = Convert.ToInt32(date["sort"]);
            }
            dr.Close();//关闭DataReader对象  
            return i;
        }
        public async Task<IActionResult> ModuleNewlist1(string key, string papes, string cid, string isen)
        {
            var en = "0";
            if (isen != null && isen != "")
            {
                en = isen;
            }
            int p = 1;
            if (papes != null && papes != "")
            {
                p = Convert.ToInt32(papes.ToString());
            }
            //var _papes = "当前页papes:" + papes;
            var where = "   where 1 = 1";
            if (key != null && key != "")
            {
                if (en == "0")
                {
                    where = where + "and  ( title like '%" + key + @"%' or  contentBody like '%" + key + @"%')";
                }
                else
                {
                    where = where + "and  ( entitle like '%" + key + @"%' or  encontentBody like '%" + key + @"%')";
                }
            }
            if (cid != null && cid != "")
            {
                where = where + " and typeId = " + Convert.ToInt32(cid.ToString());
            }
            //var _cid = "1 = 法规 ；2=新闻；3：警戒: cid的值为：" + cid;
            var strsql = "SELECT id, title, brief, coverImage, contentBody, typeId, sort, isPub, isDel, pv, createTime, updateTime, modular, entitle, enbrief, encontentBody, encreateTime FROM CoreCmsNews" + where + " order by sort desc";
            var list = GetItemEntityCore(strsql, en);
            var count = list.Count.ToString();
            //var _key = "查询条件key:" + key;
            list = list.Skip((p - 1) * 5).Take(5).ToList();
            foreach (var item in list)
            {
                if (en == "0")
                { }
                else
                {
                    if (item.createTime != null)
                        item.createTime = Convert.ToDateTime(item.createTime).ToString("dd-MMM-yyyy", new System.Globalization.CultureInfo("en-US"));
                }

            }
            //var hot = await _articleService.Hot(6);
            //var category = await _categoryService.GetRootCategories();
            return Json(new { strsql, key, papes, cid, count, list });
        }

        public async Task<IActionResult> ModuleNewlist2(string key, string papes, string cid, string isen)
        {
            var strsql = "SELECT 技术审评报告名称,文档链接 FROM Sheet1 WHERE  文档链接 !=''";
            var result1 = GetItemEntityCore1(strsql);
            return Json(new { result1 });
        }

        public async Task<IActionResult> ModuleRePortlist(string key, string papes, string pape, string cid, string isen) //
        {
            var en = "0";
            if (isen != null && isen != "")
            {
                en = isen;
            }
            int p = 1;
            if (papes != null && papes != "")
            {
                p = Convert.ToInt32(papes.ToString());
            }
            int p2 = 10;
            if (pape != null && pape != "")
            {
                p2 = Convert.ToInt32(pape.ToString());
            }
            var where = "   where 1 = 1 ";
            if (key != null && key != "")
            {
                key = key.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "").Replace("'", "").Trim();
                where = where + "and ( 技术审评报告名称 like '%" + key + @"%'" + @" or 详细内容1 like '%" + key + @"%' "
                                                                                + " or 详细内容2 like  '%" + key + @"%' "
                                                                                + " or 详细内容3 like  '%" + key + @"%' "
                                                                                + " or 详细内容4 like '%" + key + @"%' "
                                                                                + " or 详细内容5 like  '%" + key + @"%' "
                                                                                + " or 详细内容6 like '%" + key + @"%' "
                                                                                + " or 详细内容7 like  '%" + key + @"%' "
                                                                                + " or 详细内容8 like  '%" + key + @"%' "
                                                                                + " or 详细内容9 like  '%" + key + @"%' "
                                                                                + " or 详细内容10 like  '%" + key + @"%' "
                                                                                + " or 详细内容10 like  '%" + key + @"%' "
                                                                                + " or 详细内容11 like  '%" + key + @"%' "
                                                                                + " or 详细内容12 like  '%" + key + @"%' "
                                                                                + " or 详细内容13 like  '%" + key + @"%' "
                                                                                + " or 详细内容14 like  '%" + key + @"%' "
                                                                                + @" )";
            }
            var strsql = "SELECT * FROM Sheet1" + where + " ORDER BY 发布时间 DESC ";
            var list = GetItemEntityCore14(strsql);
            var count = list.Count.ToString();
            list = list.Skip((p - 1) * p2).Take(p2).ToList();
            return Json(new { key, papes, count, list });
        }


        public async Task<IActionResult> ModuleReGZELQXlist2222(string key, string papes, string pape, string cid, string isen, string postkey, string title, string mill, string other) //
        {
            var en = "0";
            if (isen != null && isen != "")
            {
                en = isen;
            }
            int p = 1;
            if (papes != null && papes != "")
            {
                p = Convert.ToInt32(papes.ToString());
            }
            int p2 = 10;
            if (pape != null && pape != "")
            {
                p2 = Convert.ToInt32(pape.ToString());
            }
            var where = "   where 1 = 1 ";

            if (key != null && key != "")
            {
                key = key.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "").Replace("'", "").Trim();
                var where3 = "and ( postkey like '%" + postkey + @"%'"
                                    + title != "" ? @" or title like '%" + title + @"%' " : ""
                                    + mill != "" ? @" or mill like '%" + mill + @"%' " : ""
                                    + other != "" ? @" or other like '%" + other + @"%' " : "";
                var where4 = "and ( postkey like '%" + postkey + @"%'"
                                    + title != "" ? @" and title like '%" + title + @"%' " : ""
                                    + mill != "" ? @" and mill like '%" + mill + @"%' " : ""
                                    + other != "" ? @" and other like '%" + other + @"%' " : "";
                var where1 = "and ( postkey like '%" + key + @"%'" + @" or title like '%" + key + @"%' "
                                                                                + @" or mill like '%" + key + @"%' "
                                                                                + @" or other like '%" + key + @"%' ";
                var where2 = @" or 详细内容1 like '%" + key + @"%' "
                                                                                + " or 详细内容2 like  '%" + key + @"%' "
                                                                                + " or 详细内容3 like  '%" + key + @"%' "
                                                                                + " or 详细内容4 like '%" + key + @"%' "
                                                                                + " or 详细内容5 like  '%" + key + @"%' "
                                                                                + " or 详细内容6 like '%" + key + @"%' "
                                                                                + " or 详细内容7 like  '%" + key + @"%' "
                                                                                + " or 详细内容8 like  '%" + key + @"%' "
                                                                                + " or 详细内容9 like  '%" + key + @"%' "
                                                                                + " or 详细内容10 like  '%" + key + @"%' "
                                                                                + " or 详细内容10 like  '%" + key + @"%' "
                                                                                + " or 详细内容11 like  '%" + key + @"%' "
                                                                                + " or 详细内容12 like  '%" + key + @"%' "
                                                                                + " or 详细内容13 like  '%" + key + @"%' "
                                                                                + " or 详细内容14 like  '%" + key + @"%' "
                                                                                + " or 详细内容15 like  '%" + key + @"%' "
                                                                                + " or 详细内容16 like  '%" + key + @"%' "
                                                                                + " or 详细内容17 like  '%" + key + @"%' "
                                                                                + " or 详细内容18 like  '%" + key + @"%' "
                                                                                + " or 详细内容19 like  '%" + key + @"%' "
                                                                                + " or 详细内容20 like  '%" + key + @"%' "
                                                                                + " or 详细内容21 like  '%" + key + @"%' "
                                                                                + " or 详细内容22 like  '%" + key + @"%' "
                                                                                + " or 详细内容23 like  '%" + key + @"%' "
                                                                                + " or 详细内容24 like  '%" + key + @"%' "
                                                                                + " or 详细内容25 like  '%" + key + @"%' "
                                                                                + " or 详细内容26 like  '%" + key + @"%' "
                                                                                + " or 详细内容27 like  '%" + key + @"%' "
                                                                                + " or 详细内容28 like  '%" + key + @"%' "
                                                                                + " or 详细内容29 like  '%" + key + @"%' "
                                                                                + " or 详细内容30 like  '%" + key + @"%' "
                                                                                + " or 详细内容31 like  '%" + key + @"%' "
                                                                                + " or 详细内容32 like  '%" + key + @"%' "
                                                                                + " or 详细内容33 like  '%" + key + @"%' "
                                                                                + " or 详细内容34 like  '%" + key + @"%' "
                                                                                + " or 详细内容35 like  '%" + key + @"%' "
                                                                                + " or 详细内容36 like  '%" + key + @"%' "
                                                                                + " or 详细内容37 like  '%" + key + @"%' "
                                                                                + " or 详细内容38 like  '%" + key + @"%' "
                                                                                + " or 详细内容39 like  '%" + key + @"%' "
                                                                                + " or 详细内容40 like  '%" + key + @"%' "
                                                                                + " or 详细内容41 like  '%" + key + @"%' "
                                                                                + " or 详细内容42 like  '%" + key + @"%' "
                                                                                + " or 详细内容43 like  '%" + key + @"%' "
                                                                                + " or 详细内容44 like  '%" + key + @"%' "
                                                                                + " or 详细内容45 like  '%" + key + @"%' "
                                                                                + " or 详细内容46 like  '%" + key + @"%' "
                                                                                + " or 详细内容47 like  '%" + key + @"%' "
                                                                                + " or 详细内容48 like  '%" + key + @"%' "
                                                                                + " or 详细内容49 like  '%" + key + @"%' "
                                                                                + " or 详细内容50 like  '%" + key + @"%' "
                                                                                + @" )";
                if (cid == "1")
                {
                    where += where1 + where2;
                }
                else if (cid == "0")
                {
                    where += ")";
                }
                else if (cid == "3")
                {
                    where += where3 + ")";

                }
                else if (cid == "4")
                {
                    where += where4 + ")";

                }
            }
            where += " and  详细内容1 <> ''";
            var strsql = "SELECT * FROM PaperPDF" + where;// + " ORDER BY 发布时间 DESC ";

            var list = GetItemEntityCore15(strsql);
            var count = list.Count.ToString();
            list = list.Skip((p - 1) * p2).Take(p2).ToList();
            return Json(new { key, papes, count, list });
        }


        public async Task<IActionResult> ModuleFGWJGlist(string key, string papes, string pape, string cid, string isen, string isenType, string postkey, string title, string mill, string other) //
        {
            //other = "法规文件名称like'%你好%'andMMPA发布名称like'%我好%'andMMPA发布名称like'%%'and官网链接like'%%'and发布时间like'%%'and索引号like'%%'and";
            var en = "0";
            var startime = DateTime.Now;
            var endime = DateTime.Now;
            if (isen != null && isen != "")
            {
                startime = Convert.ToDateTime(isen.Split(',')[0]);
                endime = Convert.ToDateTime(isen.Split(',')[1]);
                //isen = Convert.ToDateTime(isen).ToLongDateString().ToString();
                //en = isen;
            }
            int p = 1;
            if (papes != null && papes != "")
            {
                p = Convert.ToInt32(papes.ToString());
            }
            int p2 = 10;
            if (pape != null && pape != "")
            {
                p2 = Convert.ToInt32(pape.ToString());
            }
            var where = "   where 1 = 1 ";

            if (key != null && key != "")
            {
                key = key.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "").Replace("'", "").Trim();
                where = where + "and ( 法规文件名称 like '%" + key + @"%'" + @" or NMPA发布名称 like '%" + key + @"%' " + @" or 标题  like '%" + key + @"%' ";
                var where2 = @" or 内容 like '%" + key + @"%' " + @" )";
                if (cid == "0")
                {

                    where += ")";
                }
                else if (cid == "1")
                {
                    where += where2;
                }
                else if (cid == "2")
                {
                    if (other != "" && other != null)
                    {
                    }
                    else
                    {
                        other = " 1 = 1  ";
                    }
                    where = "  where " + other + "";
                    if (isen != null && isen != "")
                    {
                        if (isenType == "NOT LIKE")
                        {

                            where += "and" + "( 时间 < '" + startime + @"' or  时间 > '" + endime + @"' )";
                        }
                        else
                        {

                            where += isenType + "( 时间 > '" + startime + @"' and  时间 < '" + endime + @"' )";
                        }

                    }
                }
            }

            where += " and  内容 <> ''";

            var strsql = "SELECT * FROM FGWJ" + where;// + " ORDER BY 发布时间 DESC ";

            var list = GetItemEntityCore156(strsql);
            var count = list.Count.ToString();
            list = list.Skip((p - 1) * p2).Take(p2).ToList();
            return Json(new { key, strsql, other, papes, count, list });
        }
        public async Task<IActionResult> ModuleReGZELQXlist(string key, string papes, string pape, string cid, string isen) //
        {
            var en = "0";
            if (isen != null && isen != "")
            {
                en = isen;
            }
            int p = 1;
            if (papes != null && papes != "")
            {
                p = Convert.ToInt32(papes.ToString());
            }
            int p2 = 10;
            if (pape != null && pape != "")
            {
                p2 = Convert.ToInt32(pape.ToString());
            }
            var where = "   where 1 = 1 ";

            if (key != null && key != "")
            {
                key = key.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "").Replace("'", "").Trim();
                where = where + "and ( postkey like '%" + key + @"%'" + @" or title like '%" + key + @"%' "
                                                                                + @" or mill like '%" + key + @"%' "
                                                                                + @" or other like '%" + key + @"%' ";
                var where2 = @" or 详细内容1 like '%" + key + @"%' "
                                                                                + " or 详细内容2 like  '%" + key + @"%' "
                                                                                + " or 详细内容3 like  '%" + key + @"%' "
                                                                                + " or 详细内容4 like '%" + key + @"%' "
                                                                                + " or 详细内容5 like  '%" + key + @"%' "
                                                                                + " or 详细内容6 like '%" + key + @"%' "
                                                                                + " or 详细内容7 like  '%" + key + @"%' "
                                                                                + " or 详细内容8 like  '%" + key + @"%' "
                                                                                + " or 详细内容9 like  '%" + key + @"%' "
                                                                                + " or 详细内容10 like  '%" + key + @"%' "
                                                                                + " or 详细内容10 like  '%" + key + @"%' "
                                                                                + " or 详细内容11 like  '%" + key + @"%' "
                                                                                + " or 详细内容12 like  '%" + key + @"%' "
                                                                                + " or 详细内容13 like  '%" + key + @"%' "
                                                                                + " or 详细内容14 like  '%" + key + @"%' "
                                                                                + " or 详细内容15 like  '%" + key + @"%' "
                                                                                + " or 详细内容16 like  '%" + key + @"%' "
                                                                                + " or 详细内容17 like  '%" + key + @"%' "
                                                                                + " or 详细内容18 like  '%" + key + @"%' "
                                                                                + " or 详细内容19 like  '%" + key + @"%' "
                                                                                + " or 详细内容20 like  '%" + key + @"%' "
                                                                                + " or 详细内容21 like  '%" + key + @"%' "
                                                                                + " or 详细内容22 like  '%" + key + @"%' "
                                                                                + " or 详细内容23 like  '%" + key + @"%' "
                                                                                + " or 详细内容24 like  '%" + key + @"%' "
                                                                                + " or 详细内容25 like  '%" + key + @"%' "
                                                                                + " or 详细内容26 like  '%" + key + @"%' "
                                                                                + " or 详细内容27 like  '%" + key + @"%' "
                                                                                + " or 详细内容28 like  '%" + key + @"%' "
                                                                                + " or 详细内容29 like  '%" + key + @"%' "
                                                                                + " or 详细内容30 like  '%" + key + @"%' "
                                                                                + " or 详细内容31 like  '%" + key + @"%' "
                                                                                + " or 详细内容32 like  '%" + key + @"%' "
                                                                                + " or 详细内容33 like  '%" + key + @"%' "
                                                                                + " or 详细内容34 like  '%" + key + @"%' "
                                                                                + " or 详细内容35 like  '%" + key + @"%' "
                                                                                + " or 详细内容36 like  '%" + key + @"%' "
                                                                                + " or 详细内容37 like  '%" + key + @"%' "
                                                                                + " or 详细内容38 like  '%" + key + @"%' "
                                                                                + " or 详细内容39 like  '%" + key + @"%' "
                                                                                + " or 详细内容40 like  '%" + key + @"%' "
                                                                                + " or 详细内容41 like  '%" + key + @"%' "
                                                                                + " or 详细内容42 like  '%" + key + @"%' "
                                                                                + " or 详细内容43 like  '%" + key + @"%' "
                                                                                + " or 详细内容44 like  '%" + key + @"%' "
                                                                                + " or 详细内容45 like  '%" + key + @"%' "
                                                                                + " or 详细内容46 like  '%" + key + @"%' "
                                                                                + " or 详细内容47 like  '%" + key + @"%' "
                                                                                + " or 详细内容48 like  '%" + key + @"%' "
                                                                                + " or 详细内容49 like  '%" + key + @"%' "
                                                                                + " or 详细内容50 like  '%" + key + @"%' "
                                                                                + @" )";
                if (cid == "1")
                {
                    where += where2;
                }
                else
                {
                    where += ")";
                }
            }
            where += " and  详细内容1 <> ''";
            var strsql = "SELECT * FROM PaperPDF" + where;// + " ORDER BY 发布时间 DESC ";

            var list = GetItemEntityCore15(strsql);
            var count = list.Count.ToString();
            list = list.Skip((p - 1) * p2).Take(p2).ToList();
            return Json(new { key, papes, count, list });
        }
        public async Task<string> JiutaiChatGPTnew(string key)
        {
            var resluts = "imag";
            string _url = string.Format("http://www.gpt-chatgpt.com/index/talk/text");

            //json参数
            string jsonParam = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                talk_id = "8993",
                text = key

            });
            var valuestr = key;
            var request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";//ContentType
            request.Headers.Add("Cookie", "user_token=a6c07a7195be7fe11f45263aac216c12; tuid=6338");
            byte[] byteData = Encoding.UTF8.GetBytes(jsonParam);
            int length = byteData.Length;
            request.ContentLength = length;
            Stream writer = request.GetRequestStream();
            writer.Write(byteData, 0, length);
            writer.Close();
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();

            JObject jo = JObject.Parse(responseString);
            string zone = jo["data"]["ai"][0]["value"].ToString(); ;
            return zone;
        }
        public async Task<string> JiutaiChatgpt456(string key)
        {
            var resluts = "imag";
            string _url = string.Format("https://free-api.cveoy.top/v3/completions");

            var valuestr = key;
            //key = "你好!";
            string jsonParam = "{\"prompt\":\"" + key + "\"}";
            var request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";//ContentType
                                                                   //request.Headers.Add("Cookie", "_ga=GA1.1.1405402686.1680744018; _clck=1hpgfw2|1|faj|0; _ga_DR0DKGM5XH=GS1.1.1680758052.3.1.1680758054.0.0.0; csrfToken=WqSRImavOvSr_Gb1wCEozjws; _clsk=k7wi4|1680758248512|3|1|r.clarity.ms/collect");
                                                                   //request.Headers.Add("x-csrf-token", "WqSRImavOvSr_Gb1wCEozjws");            //request.Headers.Add("Cookie", "_ga=GA1.1.1405402686.1680744018; _clck=1hpgfw2|1|faj|0; _ga_DR0DKGM5XH=GS1.1.1680758052.3.1.1680758054.0.0.0; csrfToken=WqSRImavOvSr_Gb1wCEozjws; _clsk=k7wi4|1680758248512|3|1|r.clarity.ms/collect");
            int str = 1;
            int str2 = 1;

            string sql22 = "SELECT Code2 FROM NewTableNum";
            str2 = Convert.ToInt32(Getcode2(sql22));
            if (str2 == 9)
            {
                str2 = 1;
                string sql = "SELECT Code FROM NewTableNum";
                str = Convert.ToInt32(Getcode(sql));
                if (str == 29)
                {
                    str = 1;
                }
                else
                {
                    str++;
                }
                string sql2 = "UPDATE TOP(1) NewTableNum SET Code=" + str;
                string rusult2 = GetIntegralByMonth(sql2);
            }
            else
            {
                str2++;
            }
            string sql23 = "UPDATE TOP(1) NewTableNum SET Code2=" + str2;
            string rusult23 = GetIntegralByMonth(sql23);


            string ori = "https://ai" + str + ".chagpt.fun";
            request.Headers.Add("origin", ori);
            //request.Headers.Add("Cookie", "BIDUPSID=7891B647FEF86D67510769E8D37EA33E; BAIDUID=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; PSTM=1658661266; BDUSS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; BDUSS_BFESS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; MCITY=-257%3A; Hm_lvt_01e907653ac089993ee83ed00ef9c2f3=1679276132; BDSFRCVID=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; H_BDCLCKID_SF=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; BDORZ=B490B5EBF6F3CD402E515D22BCDA1598; BA_HECTOR=a10h8l25a120ag8ka08080c81i1i5k51n; delPer=0; PSINO=6; ZFY=xeG8XGnodAhNOH37maKvpHgvVS4yWCQShEruX4R6o2w:C; BAIDUID_BFESS=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; BDSFRCVID_BFESS=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; H_BDCLCKID_SF_BFESS=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; H_PS_PSSID=38185_36558_38410_38349_38307_37862_38174_38290_37922_38425_38314_38383_38285_26350_38420_38283_37881; __bid_n=186fca84a5b9216b5752e1; XFI=bf261800-c796-11ed-9e7c-993a1e2bd1b9; Hm_lpvt_01e907653ac089993ee83ed00ef9c2f3=1679368579; ab_sr=1.0.1_N2YyOTA1NTAyMjI2NmViYzQ3NjMxZjg3OTE4ZTQ3ODg1NGJiNDgxOWZhZjAyYjU5YTk4NDIyMmZlZmI4NGFiYTA5MDg1YzZiOTdiMGNkNzc5NjZlOGI5ZTBiYTY5ZjBkZmZjNTE1NjhkMzExMmRjZGU1MzEyYmNjOTVjYTBiMDhhNjgwMjA0YmQxMjM5M2VjMjRhMGE4ZWZiNmNhZTVkYzViYjMxNGFhYzE4ZDJkMzNkZDBiMmIyMmZlYjI0MmQz; XFCS=EED03EFAACD1A34C9A95B676A5742E986961084A1D66E5C82F7FDBF66FFF6525; XFT=YIBAwYbCQVTX/hBmbFSk3pokc8vmvt9MmWTTIO60MVk=");
            //request.Headers.Add("Acs-Token", "1679288415966_1679369409945_H2S6+gSGuHdouzSnuYXZt9m6M5kauNTIZXpHl6c5/p+ky0VMWplii+uwH2E+JsD1c0Z2Wu58VGKzVUJYrqlyfr/b7NEqKCVQUbNp47RIRZtwfSNLueUDK/pidv5MvU8cJe8LFzjfAuJ1t+2ujjkn403zZnT+V+6QsuO1P4Mpk3YM+z/ulNVN6hCOdG17KnB9alyB7XcceKCBmghYIbFrICf40Lw10FUqxtGo2QHUgCYOMC/Ev3baG6pNIMrjYdaiGFFjug9Kx7sAS7k8dfjADk7H3W80kuq4d6/AyXELYQ+hylZLQVgzyqa9lDBsd1C26fl102rgY47dLfkjwOpfIw==");
            //CookieContainer cc = new CookieContainer();
            //cc.Add(new System.Uri("https://yiyan.baidu.com"), new Cookie("Cookie", "BIDUPSID=7891B647FEF86D67510769E8D37EA33E; BAIDUID=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; PSTM=1658661266; BDUSS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; BDUSS_BFESS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; MCITY=-257%3A; Hm_lvt_01e907653ac089993ee83ed00ef9c2f3=1679276132; BDORZ=B490B5EBF6F3CD402E515D22BCDA1598; BA_HECTOR=8h8k002h050g01048k8g840s1i1i3g31n; BDRCVFR[feWj1Vr5u3D]=I67x6TjHwwYf0; delPer=0; PSINO=6; BAIDUID_BFESS=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; ZFY=xeG8XGnodAhNOH37maKvpHgvVS4yWCQShEruX4R6o2w:C; BCLID=4201984001025888909; BCLID_BFESS=4201984001025888909; BDSFRCVID=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; BDSFRCVID_BFESS=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; H_BDCLCKID_SF=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; H_BDCLCKID_SF_BFESS=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; XFI=d8cdfac0-c78c-11ed-9363-11fb77acfb22; Hm_lpvt_01e907653ac089993ee83ed00ef9c2f3=1679364327; __bid_n=186fca84a5b9216b5752e1; ab_sr=1.0.1_MDhlMzhhZjkyZDJiYWNlOGRhMjdkNjQ1YzczYjkzNDA0YzAyODQ1ZDhhNjNmMDAzZTZkMDM2ZmVhNGEzMzUwZTcwOTc3ODlmMWU0NjgzYzY1OWMyOWE2YTYyMjcxMDA3N2IwNTBkZTEzZGJiMTc4MjM4YjkzOTEwMmFmYjlhNzI4NzA5ZjY4ZjQxMmFiYWJjNDRiN2NjM2ZjNTBlNjQyNGQ0NTFiZGNlYmVkZjc0MWRmMDg5MWJiZTI5ODlhNzlm; XFCS=D4DA282258F7335484898A2AF69DC41EA15E3806075C8AAFEA6DDBE278479D6E; XFT=s25AHfKy5Z9kcvgUrcryF3aCf5jg21c06GlfhkXUHWw=; BDRCVFR[VXHUG3ZuJnT]=mk3SLVN4HKm; H_PS_PSSID=26350"));
            //cc.Add(new System.Uri("https://yiyan.baidu.com"), new Cookie("token", "1679288415966_1679364661713_iUtUdtoyITmXeBfte7MCnnK8oI/JVoX8qSHD7dEk/qtqgG5DD1JKOgEqQGD+msq4GuE05P//gtrrZjQxk0dwRuFPCSdlrfU9duyrDWkp0PeyFljeboqFVso55q4it+VrbQPUW34pdj80wLEIty8YrBGL7NvlqnmDTly+w/my5pvhAFPr4YD7RzY/xMP//Agyxi/wMj8bJP8rcozzEmWbaLOxslIZPgO5PFN/KBVN/tj9ZYJJDt5xvYk8ZwHHFgA4039fco4g20z32qhEs5JEM4Jtwuyyd7Jas0esf6GC36Af+FGzKL/hMbd3kFS3giu0DNLBz4CEY01UoqqVCSkBgA=="));
            //request.CookieContainer = cc;
            byte[] byteData = Encoding.UTF8.GetBytes(jsonParam);
            int length = byteData.Length;
            request.ContentLength = length;
            Stream writer = request.GetRequestStream();
            writer.Write(byteData, 0, length);
            writer.Close();
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();

            return responseString;
        }
        private string Getcode(string sqlstr, string filter = "")
        {
            var list1 = "";
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var date = dr;
                list1 = date["Code"].ToString();
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }
        private string Getcode2(string sqlstr, string filter = "")
        {
            var list1 = "";
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var date = dr;
                list1 = date["Code2"].ToString();
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }
        public async Task<string> JiutaiChatgpt123(string key)
        {
            var resluts = "imag";
            string _url = string.Format("https://svip-api.cveoy.top/v3/completions");

            var valuestr = key;
            string jsonParam = "{\"prompt\":\"" + key + "\",\"keys\":\"sk-ba969e20efa047258a7e10e32cde86ce\"}";
            //string jsonParam = "{\"prompt\":\"" + key + "\",\"keys\":\"sk-e0ed097f56c540af9b2436983f1601a0\"}";
            //string jsonParam = "{\"prompt\":\""+ key + "\",\"keys\":\"sk-2d81d9aaf29a4ec287647b4fc514b893\"}";
            var request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";//ContentType
            //request.Headers.Add("Cookie", "_ga=GA1.1.1405402686.1680744018; _clck=1hpgfw2|1|faj|0; _ga_DR0DKGM5XH=GS1.1.1680758052.3.1.1680758054.0.0.0; csrfToken=WqSRImavOvSr_Gb1wCEozjws; _clsk=k7wi4|1680758248512|3|1|r.clarity.ms/collect");
            //request.Headers.Add("x-csrf-token", "WqSRImavOvSr_Gb1wCEozjws");            //request.Headers.Add("Cookie", "_ga=GA1.1.1405402686.1680744018; _clck=1hpgfw2|1|faj|0; _ga_DR0DKGM5XH=GS1.1.1680758052.3.1.1680758054.0.0.0; csrfToken=WqSRImavOvSr_Gb1wCEozjws; _clsk=k7wi4|1680758248512|3|1|r.clarity.ms/collect");
            //request.Headers.Add("x-csrf-token", "WqSRImavOvSr_Gb1wCEozjws");
            //request.Headers.Add("Cookie", "BIDUPSID=7891B647FEF86D67510769E8D37EA33E; BAIDUID=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; PSTM=1658661266; BDUSS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; BDUSS_BFESS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; MCITY=-257%3A; Hm_lvt_01e907653ac089993ee83ed00ef9c2f3=1679276132; BDSFRCVID=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; H_BDCLCKID_SF=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; BDORZ=B490B5EBF6F3CD402E515D22BCDA1598; BA_HECTOR=a10h8l25a120ag8ka08080c81i1i5k51n; delPer=0; PSINO=6; ZFY=xeG8XGnodAhNOH37maKvpHgvVS4yWCQShEruX4R6o2w:C; BAIDUID_BFESS=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; BDSFRCVID_BFESS=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; H_BDCLCKID_SF_BFESS=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; H_PS_PSSID=38185_36558_38410_38349_38307_37862_38174_38290_37922_38425_38314_38383_38285_26350_38420_38283_37881; __bid_n=186fca84a5b9216b5752e1; XFI=bf261800-c796-11ed-9e7c-993a1e2bd1b9; Hm_lpvt_01e907653ac089993ee83ed00ef9c2f3=1679368579; ab_sr=1.0.1_N2YyOTA1NTAyMjI2NmViYzQ3NjMxZjg3OTE4ZTQ3ODg1NGJiNDgxOWZhZjAyYjU5YTk4NDIyMmZlZmI4NGFiYTA5MDg1YzZiOTdiMGNkNzc5NjZlOGI5ZTBiYTY5ZjBkZmZjNTE1NjhkMzExMmRjZGU1MzEyYmNjOTVjYTBiMDhhNjgwMjA0YmQxMjM5M2VjMjRhMGE4ZWZiNmNhZTVkYzViYjMxNGFhYzE4ZDJkMzNkZDBiMmIyMmZlYjI0MmQz; XFCS=EED03EFAACD1A34C9A95B676A5742E986961084A1D66E5C82F7FDBF66FFF6525; XFT=YIBAwYbCQVTX/hBmbFSk3pokc8vmvt9MmWTTIO60MVk=");
            //request.Headers.Add("Acs-Token", "1679288415966_1679369409945_H2S6+gSGuHdouzSnuYXZt9m6M5kauNTIZXpHl6c5/p+ky0VMWplii+uwH2E+JsD1c0Z2Wu58VGKzVUJYrqlyfr/b7NEqKCVQUbNp47RIRZtwfSNLueUDK/pidv5MvU8cJe8LFzjfAuJ1t+2ujjkn403zZnT+V+6QsuO1P4Mpk3YM+z/ulNVN6hCOdG17KnB9alyB7XcceKCBmghYIbFrICf40Lw10FUqxtGo2QHUgCYOMC/Ev3baG6pNIMrjYdaiGFFjug9Kx7sAS7k8dfjADk7H3W80kuq4d6/AyXELYQ+hylZLQVgzyqa9lDBsd1C26fl102rgY47dLfkjwOpfIw==");
            //CookieContainer cc = new CookieContainer();
            //cc.Add(new System.Uri("https://yiyan.baidu.com"), new Cookie("Cookie", "BIDUPSID=7891B647FEF86D67510769E8D37EA33E; BAIDUID=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; PSTM=1658661266; BDUSS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; BDUSS_BFESS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; MCITY=-257%3A; Hm_lvt_01e907653ac089993ee83ed00ef9c2f3=1679276132; BDORZ=B490B5EBF6F3CD402E515D22BCDA1598; BA_HECTOR=8h8k002h050g01048k8g840s1i1i3g31n; BDRCVFR[feWj1Vr5u3D]=I67x6TjHwwYf0; delPer=0; PSINO=6; BAIDUID_BFESS=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; ZFY=xeG8XGnodAhNOH37maKvpHgvVS4yWCQShEruX4R6o2w:C; BCLID=4201984001025888909; BCLID_BFESS=4201984001025888909; BDSFRCVID=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; BDSFRCVID_BFESS=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; H_BDCLCKID_SF=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; H_BDCLCKID_SF_BFESS=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; XFI=d8cdfac0-c78c-11ed-9363-11fb77acfb22; Hm_lpvt_01e907653ac089993ee83ed00ef9c2f3=1679364327; __bid_n=186fca84a5b9216b5752e1; ab_sr=1.0.1_MDhlMzhhZjkyZDJiYWNlOGRhMjdkNjQ1YzczYjkzNDA0YzAyODQ1ZDhhNjNmMDAzZTZkMDM2ZmVhNGEzMzUwZTcwOTc3ODlmMWU0NjgzYzY1OWMyOWE2YTYyMjcxMDA3N2IwNTBkZTEzZGJiMTc4MjM4YjkzOTEwMmFmYjlhNzI4NzA5ZjY4ZjQxMmFiYWJjNDRiN2NjM2ZjNTBlNjQyNGQ0NTFiZGNlYmVkZjc0MWRmMDg5MWJiZTI5ODlhNzlm; XFCS=D4DA282258F7335484898A2AF69DC41EA15E3806075C8AAFEA6DDBE278479D6E; XFT=s25AHfKy5Z9kcvgUrcryF3aCf5jg21c06GlfhkXUHWw=; BDRCVFR[VXHUG3ZuJnT]=mk3SLVN4HKm; H_PS_PSSID=26350"));
            //cc.Add(new System.Uri("https://yiyan.baidu.com"), new Cookie("token", "1679288415966_1679364661713_iUtUdtoyITmXeBfte7MCnnK8oI/JVoX8qSHD7dEk/qtqgG5DD1JKOgEqQGD+msq4GuE05P//gtrrZjQxk0dwRuFPCSdlrfU9duyrDWkp0PeyFljeboqFVso55q4it+VrbQPUW34pdj80wLEIty8YrBGL7NvlqnmDTly+w/my5pvhAFPr4YD7RzY/xMP//Agyxi/wMj8bJP8rcozzEmWbaLOxslIZPgO5PFN/KBVN/tj9ZYJJDt5xvYk8ZwHHFgA4039fco4g20z32qhEs5JEM4Jtwuyyd7Jas0esf6GC36Af+FGzKL/hMbd3kFS3giu0DNLBz4CEY01UoqqVCSkBgA=="));
            //request.CookieContainer = cc;
            byte[] byteData = Encoding.UTF8.GetBytes(jsonParam);
            int length = byteData.Length;
            request.ContentLength = length;
            Stream writer = request.GetRequestStream();
            writer.Write(byteData, 0, length);
            writer.Close();
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();

            return responseString;
        }
        public async Task<string> JiutaiChatgpt(string key)
        {
            var resluts = "imag";
            string _url = string.Format("https://gpttalk.live/api/chat-process");

            //System.Threading.Thread.Sleep(1 * 2 * 1000); //休眠30秒
            //json参数
            //string jsonParam = Newtonsoft.Json.JsonConvert.SerializeObject(new
            //{
            //    chatId = "15187477",
            //    parentChatId = "15187475",
            //    sentenceId =0,
            //    stop = 0,
            //    timestamp = 1679368929167,
            //    deviceType = "pc",
            //    sign = "1679288415966_1679368029164_13iqOlPG28VuKkzRh9alRF0wVQ8WjvSsu2iuGokzD/ujqvrj/wmAD6A2a0hENZFH7YCZlX+DeNZQGooNjQW2zNPoR6TeAN9MD/GVfzUy8OJ081yg0f5dk8yFT7IPiBGAGLWozEdQQCEyXN20sR3gtYHrgniwxx5YFr69v+8WLBE77iFx7/dsJDjFi3tv78MFVWxgO5wywWJdv1kqWVQJqzwCqcwJ1wW2N5VngvNKyIy9r2953aEzK60VCc1TExjLY/bm+Pyu+v8uqMjGZZMlK4O93B3hTw+26raJkXxazDabx8b6s7adC9ymYANKqaWKwjx66+GGso2qaq9OHN2acg=="

            //});
            var valuestr = key;
            string jsonParam = "{\"prompt\":\"" + key + "\",\"options\":{\"parentMessageId\":\"chatcmpl-72AuDhmT57dbpvS9gmDOvuryWgsVU\"},\"systemMessage\":\"You are ChatGPT, a large language model trained by OpenAI. Follow the user's instructions carefully. Respond using markdown.\"}";
            var request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";//ContentType
            request.Headers.Add("Cookie", "_ga=GA1.1.1405402686.1680744018; _clck=1hpgfw2|1|faj|0; _ga_DR0DKGM5XH=GS1.1.1680758052.3.1.1680758054.0.0.0; csrfToken=WqSRImavOvSr_Gb1wCEozjws; _clsk=k7wi4|1680758248512|3|1|r.clarity.ms/collect");
            request.Headers.Add("x-csrf-token", "WqSRImavOvSr_Gb1wCEozjws");
            //request.Headers.Add("Cookie", "BIDUPSID=7891B647FEF86D67510769E8D37EA33E; BAIDUID=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; PSTM=1658661266; BDUSS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; BDUSS_BFESS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; MCITY=-257%3A; Hm_lvt_01e907653ac089993ee83ed00ef9c2f3=1679276132; BDSFRCVID=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; H_BDCLCKID_SF=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; BDORZ=B490B5EBF6F3CD402E515D22BCDA1598; BA_HECTOR=a10h8l25a120ag8ka08080c81i1i5k51n; delPer=0; PSINO=6; ZFY=xeG8XGnodAhNOH37maKvpHgvVS4yWCQShEruX4R6o2w:C; BAIDUID_BFESS=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; BDSFRCVID_BFESS=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; H_BDCLCKID_SF_BFESS=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; H_PS_PSSID=38185_36558_38410_38349_38307_37862_38174_38290_37922_38425_38314_38383_38285_26350_38420_38283_37881; __bid_n=186fca84a5b9216b5752e1; XFI=bf261800-c796-11ed-9e7c-993a1e2bd1b9; Hm_lpvt_01e907653ac089993ee83ed00ef9c2f3=1679368579; ab_sr=1.0.1_N2YyOTA1NTAyMjI2NmViYzQ3NjMxZjg3OTE4ZTQ3ODg1NGJiNDgxOWZhZjAyYjU5YTk4NDIyMmZlZmI4NGFiYTA5MDg1YzZiOTdiMGNkNzc5NjZlOGI5ZTBiYTY5ZjBkZmZjNTE1NjhkMzExMmRjZGU1MzEyYmNjOTVjYTBiMDhhNjgwMjA0YmQxMjM5M2VjMjRhMGE4ZWZiNmNhZTVkYzViYjMxNGFhYzE4ZDJkMzNkZDBiMmIyMmZlYjI0MmQz; XFCS=EED03EFAACD1A34C9A95B676A5742E986961084A1D66E5C82F7FDBF66FFF6525; XFT=YIBAwYbCQVTX/hBmbFSk3pokc8vmvt9MmWTTIO60MVk=");
            //request.Headers.Add("Acs-Token", "1679288415966_1679369409945_H2S6+gSGuHdouzSnuYXZt9m6M5kauNTIZXpHl6c5/p+ky0VMWplii+uwH2E+JsD1c0Z2Wu58VGKzVUJYrqlyfr/b7NEqKCVQUbNp47RIRZtwfSNLueUDK/pidv5MvU8cJe8LFzjfAuJ1t+2ujjkn403zZnT+V+6QsuO1P4Mpk3YM+z/ulNVN6hCOdG17KnB9alyB7XcceKCBmghYIbFrICf40Lw10FUqxtGo2QHUgCYOMC/Ev3baG6pNIMrjYdaiGFFjug9Kx7sAS7k8dfjADk7H3W80kuq4d6/AyXELYQ+hylZLQVgzyqa9lDBsd1C26fl102rgY47dLfkjwOpfIw==");
            //CookieContainer cc = new CookieContainer();
            //cc.Add(new System.Uri("https://yiyan.baidu.com"), new Cookie("Cookie", "BIDUPSID=7891B647FEF86D67510769E8D37EA33E; BAIDUID=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; PSTM=1658661266; BDUSS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; BDUSS_BFESS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; MCITY=-257%3A; Hm_lvt_01e907653ac089993ee83ed00ef9c2f3=1679276132; BDORZ=B490B5EBF6F3CD402E515D22BCDA1598; BA_HECTOR=8h8k002h050g01048k8g840s1i1i3g31n; BDRCVFR[feWj1Vr5u3D]=I67x6TjHwwYf0; delPer=0; PSINO=6; BAIDUID_BFESS=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; ZFY=xeG8XGnodAhNOH37maKvpHgvVS4yWCQShEruX4R6o2w:C; BCLID=4201984001025888909; BCLID_BFESS=4201984001025888909; BDSFRCVID=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; BDSFRCVID_BFESS=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; H_BDCLCKID_SF=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; H_BDCLCKID_SF_BFESS=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; XFI=d8cdfac0-c78c-11ed-9363-11fb77acfb22; Hm_lpvt_01e907653ac089993ee83ed00ef9c2f3=1679364327; __bid_n=186fca84a5b9216b5752e1; ab_sr=1.0.1_MDhlMzhhZjkyZDJiYWNlOGRhMjdkNjQ1YzczYjkzNDA0YzAyODQ1ZDhhNjNmMDAzZTZkMDM2ZmVhNGEzMzUwZTcwOTc3ODlmMWU0NjgzYzY1OWMyOWE2YTYyMjcxMDA3N2IwNTBkZTEzZGJiMTc4MjM4YjkzOTEwMmFmYjlhNzI4NzA5ZjY4ZjQxMmFiYWJjNDRiN2NjM2ZjNTBlNjQyNGQ0NTFiZGNlYmVkZjc0MWRmMDg5MWJiZTI5ODlhNzlm; XFCS=D4DA282258F7335484898A2AF69DC41EA15E3806075C8AAFEA6DDBE278479D6E; XFT=s25AHfKy5Z9kcvgUrcryF3aCf5jg21c06GlfhkXUHWw=; BDRCVFR[VXHUG3ZuJnT]=mk3SLVN4HKm; H_PS_PSSID=26350"));
            //cc.Add(new System.Uri("https://yiyan.baidu.com"), new Cookie("token", "1679288415966_1679364661713_iUtUdtoyITmXeBfte7MCnnK8oI/JVoX8qSHD7dEk/qtqgG5DD1JKOgEqQGD+msq4GuE05P//gtrrZjQxk0dwRuFPCSdlrfU9duyrDWkp0PeyFljeboqFVso55q4it+VrbQPUW34pdj80wLEIty8YrBGL7NvlqnmDTly+w/my5pvhAFPr4YD7RzY/xMP//Agyxi/wMj8bJP8rcozzEmWbaLOxslIZPgO5PFN/KBVN/tj9ZYJJDt5xvYk8ZwHHFgA4039fco4g20z32qhEs5JEM4Jtwuyyd7Jas0esf6GC36Af+FGzKL/hMbd3kFS3giu0DNLBz4CEY01UoqqVCSkBgA=="));
            //request.CookieContainer = cc;
            byte[] byteData = Encoding.UTF8.GetBytes(jsonParam);
            int length = byteData.Length;
            request.ContentLength = length;
            Stream writer = request.GetRequestStream();
            writer.Write(byteData, 0, length);
            writer.Close();
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();

            //JObject jo = JObject.Parse(responseString);
            //string zone = jo["Data"]["id"].ToString();

            //System.Threading.Thread.Sleep(1 * 5 * 1000); //休眠30秒
            //string _url2 = string.Format("https://modelscope.cn/api/v1/models/damo/cv_diffusion_text-to-image-synthesis/widgets/query");
            //string jsonParam2 = "{\"id\":\"" + zone + "\"}";
            //var request2 = (HttpWebRequest)WebRequest.Create(_url2);
            //request2.Method = "POST";
            //request2.ContentType = "application/json;charset=UTF-8";//ContentType
            //request2.Headers.Add("cookie", "uuid_tt_dd=11_27076939590-1679470980362-017728; dc_session_id=11_1679470980362.638130; c_segment=0; c_page_id=default; cna=ffxkG9VIjgkCAXBeH6SxVDo+; xlly_s=1; dp_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpZCI6MTkwNzAwNywiZXhwIjoxNjgwMDc1Nzg5LCJpYXQiOjE2Nzk0NzA5ODksInVzZXJuYW1lIjoid2VpeGluXzM5MjY5NzQwIn0.RV9_FW84VdXJM0roKBuzUwglc5DrpFdRA687nRC1gRA; UserName=weixin_39269740; UN=weixin_39269740; c_ref=https%3A//community.modelscope.cn/; c_first_ref=community.modelscope.cn; c_pref=https%3A//community.modelscope.cn/; c_first_page=https%3A//community.modelscope.cn/%3F; c_dsid=11_1679470991475.861447; dc_tos=rrww3z; log_Id_pv=3; _samesite_flag_=true; cookie2=1d1928d7774c4ae1fcdc9391baf7953c; t=cb3738081446cb99b5a4dbf3415aa368; _tb_token_=75ea6a93eb370; csg=ed524eae; m_session_id=d12d527f-624f-40ae-8cf6-bcb76e634ef1; h_uid=2215618637015; log_Id_view=7; log_Id_click=3; isg=BGZmIhFXPusWM-pPMfirKMXVt9zoR6oBS9jq6lA9Pw6w01LtDNDWEb_tL8_f-6IZ");
            //byte[] byteData2 = Encoding.UTF8.GetBytes(jsonParam2);
            //int length2 = byteData2.Length;
            //request2.ContentLength = length2;
            //Stream writer2 = request2.GetRequestStream();
            //writer2.Write(byteData2, 0, length2);
            //writer2.Close();
            //var response2 = (HttpWebResponse)request2.GetResponse();
            //var responseString2 = new StreamReader(response2.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
            //JObject jooo = (JObject)JsonConvert.DeserializeObject(responseString2);
            //string zoneo = jooo["Data"]["data"]["output_imgs"][0].ToString();
            //string zone1 = jooo["Data"]["data"]["output_imgs"][1].ToString();
            //WebRequest imgRequest0 = WebRequest.Create(zoneo);
            //WebRequest imgRequest1 = WebRequest.Create(zone1);
            //System.Drawing.Image downImage = System.Drawing.Image.FromStream(imgRequest0.GetResponse().GetResponseStream());
            //System.Drawing.Image downImage1 = System.Drawing.Image.FromStream(imgRequest1.GetResponse().GetResponseStream());
            //string deerory = string.Format(@"D:\web\web6\wwwroot\web\img\{0}\", DateTime.Now.ToString("yyyy-MM-dd"));
            //string deeroryrr = string.Format(@"web\img\{0}\", DateTime.Now.ToString("yyyy-MM-dd"));
            //string fileName = string.Format("{0}.png", DateTime.Now.ToString("HHmmssffff"));
            //string fileName1 = string.Format("{0}.png", DateTime.Now.ToString("HHmmssffff") + "1");
            //if (!System.IO.Directory.Exists(deerory))
            //{
            //    System.IO.Directory.CreateDirectory(deerory);
            //}
            //downImage.Save(deerory + fileName);
            //downImage.Dispose();
            //downImage1.Save(deerory + fileName1);
            //downImage1.Dispose();

            //var m0 = deeroryrr + fileName;
            //var m1 = deeroryrr + fileName1;

            //resluts = m0 + "&" + m1;

            return responseString;
        }
        public Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }
        public async Task<string> JiutaiImag(string key)
        {
            var resluts = "imag";
            string _url = string.Format("https://modelscope.cn/api/v1/models/damo/cv_diffusion_text-to-image-synthesis/widgets/submit");

            System.Threading.Thread.Sleep(1 * 2 * 1000); //休眠30秒
            //json参数
            //string jsonParam = Newtonsoft.Json.JsonConvert.SerializeObject(new
            //{
            //    chatId = "15187477",
            //    parentChatId = "15187475",
            //    sentenceId =0,
            //    stop = 0,
            //    timestamp = 1679368929167,
            //    deviceType = "pc",
            //    sign = "1679288415966_1679368029164_13iqOlPG28VuKkzRh9alRF0wVQ8WjvSsu2iuGokzD/ujqvrj/wmAD6A2a0hENZFH7YCZlX+DeNZQGooNjQW2zNPoR6TeAN9MD/GVfzUy8OJ081yg0f5dk8yFT7IPiBGAGLWozEdQQCEyXN20sR3gtYHrgniwxx5YFr69v+8WLBE77iFx7/dsJDjFi3tv78MFVWxgO5wywWJdv1kqWVQJqzwCqcwJ1wW2N5VngvNKyIy9r2953aEzK60VCc1TExjLY/bm+Pyu+v8uqMjGZZMlK4O93B3hTw+26raJkXxazDabx8b6s7adC9ymYANKqaWKwjx66+GGso2qaq9OHN2acg=="

            //});
            var valuestr = key;
            string jsonParam = "\r\n{\"task\":\"text-to-image-synthesis\",\"inputs\":[\"" + valuestr + "\"],\"parameters\":{\"tokenizer\":\"xglm\",\"batch_size\":2},\"urlPaths\":{\"inUrls\":[{\"value\":\"" + valuestr + "\",\"type\":\"text\",\"displayProps\":{\"size\":\"small\"},\"displayType\":\"TextArea\",\"name\":\"text\",\"title\":\"输入prompt\",\"validator\":{\"max_words\":\"75\"}}],\"outUrls\":[{\"fileType\":\"png-list\",\"outputKey\":\"output_imgs\"}]}}";
            var request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";//ContentType
            request.Headers.Add("cookie", "uuid_tt_dd=11_27076939590-1679470980362-017728; dc_session_id=11_1679470980362.638130; c_segment=0; c_page_id=default; cna=ffxkG9VIjgkCAXBeH6SxVDo+; xlly_s=1; dp_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpZCI6MTkwNzAwNywiZXhwIjoxNjgwMDc1Nzg5LCJpYXQiOjE2Nzk0NzA5ODksInVzZXJuYW1lIjoid2VpeGluXzM5MjY5NzQwIn0.RV9_FW84VdXJM0roKBuzUwglc5DrpFdRA687nRC1gRA; UserName=weixin_39269740; UN=weixin_39269740; c_ref=https%3A//community.modelscope.cn/; c_first_ref=community.modelscope.cn; c_pref=https%3A//community.modelscope.cn/; c_first_page=https%3A//community.modelscope.cn/%3F; c_dsid=11_1679470991475.861447; dc_tos=rrww3z; log_Id_pv=3; _samesite_flag_=true; cookie2=1d1928d7774c4ae1fcdc9391baf7953c; t=cb3738081446cb99b5a4dbf3415aa368; _tb_token_=75ea6a93eb370; csg=ed524eae; m_session_id=d12d527f-624f-40ae-8cf6-bcb76e634ef1; h_uid=2215618637015; log_Id_view=7; log_Id_click=3; isg=BNHR29IOUfrlB73ukpEUmXac4N1rPkWw2OHdS7NIDB9jWqKsT4l7gIf8_C680t3o");
            //request.Headers.Add("Cookie", "BIDUPSID=7891B647FEF86D67510769E8D37EA33E; BAIDUID=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; PSTM=1658661266; BDUSS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; BDUSS_BFESS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; MCITY=-257%3A; Hm_lvt_01e907653ac089993ee83ed00ef9c2f3=1679276132; BDSFRCVID=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; H_BDCLCKID_SF=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; BDORZ=B490B5EBF6F3CD402E515D22BCDA1598; BA_HECTOR=a10h8l25a120ag8ka08080c81i1i5k51n; delPer=0; PSINO=6; ZFY=xeG8XGnodAhNOH37maKvpHgvVS4yWCQShEruX4R6o2w:C; BAIDUID_BFESS=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; BDSFRCVID_BFESS=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; H_BDCLCKID_SF_BFESS=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; H_PS_PSSID=38185_36558_38410_38349_38307_37862_38174_38290_37922_38425_38314_38383_38285_26350_38420_38283_37881; __bid_n=186fca84a5b9216b5752e1; XFI=bf261800-c796-11ed-9e7c-993a1e2bd1b9; Hm_lpvt_01e907653ac089993ee83ed00ef9c2f3=1679368579; ab_sr=1.0.1_N2YyOTA1NTAyMjI2NmViYzQ3NjMxZjg3OTE4ZTQ3ODg1NGJiNDgxOWZhZjAyYjU5YTk4NDIyMmZlZmI4NGFiYTA5MDg1YzZiOTdiMGNkNzc5NjZlOGI5ZTBiYTY5ZjBkZmZjNTE1NjhkMzExMmRjZGU1MzEyYmNjOTVjYTBiMDhhNjgwMjA0YmQxMjM5M2VjMjRhMGE4ZWZiNmNhZTVkYzViYjMxNGFhYzE4ZDJkMzNkZDBiMmIyMmZlYjI0MmQz; XFCS=EED03EFAACD1A34C9A95B676A5742E986961084A1D66E5C82F7FDBF66FFF6525; XFT=YIBAwYbCQVTX/hBmbFSk3pokc8vmvt9MmWTTIO60MVk=");
            //request.Headers.Add("Acs-Token", "1679288415966_1679369409945_H2S6+gSGuHdouzSnuYXZt9m6M5kauNTIZXpHl6c5/p+ky0VMWplii+uwH2E+JsD1c0Z2Wu58VGKzVUJYrqlyfr/b7NEqKCVQUbNp47RIRZtwfSNLueUDK/pidv5MvU8cJe8LFzjfAuJ1t+2ujjkn403zZnT+V+6QsuO1P4Mpk3YM+z/ulNVN6hCOdG17KnB9alyB7XcceKCBmghYIbFrICf40Lw10FUqxtGo2QHUgCYOMC/Ev3baG6pNIMrjYdaiGFFjug9Kx7sAS7k8dfjADk7H3W80kuq4d6/AyXELYQ+hylZLQVgzyqa9lDBsd1C26fl102rgY47dLfkjwOpfIw==");
            //CookieContainer cc = new CookieContainer();
            //cc.Add(new System.Uri("https://yiyan.baidu.com"), new Cookie("Cookie", "BIDUPSID=7891B647FEF86D67510769E8D37EA33E; BAIDUID=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; PSTM=1658661266; BDUSS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; BDUSS_BFESS=JXYVktblhaVTlGZkcxMFlPNGg2aG01MVZ0RDgyY1dXSEYxWTI4R35RUzBCalprSUFBQUFBJCQAAAAAAAAAAAEAAAB-4P9EbHVvZmFtaW5nNAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAALR5DmS0eQ5kbG; MCITY=-257%3A; Hm_lvt_01e907653ac089993ee83ed00ef9c2f3=1679276132; BDORZ=B490B5EBF6F3CD402E515D22BCDA1598; BA_HECTOR=8h8k002h050g01048k8g840s1i1i3g31n; BDRCVFR[feWj1Vr5u3D]=I67x6TjHwwYf0; delPer=0; PSINO=6; BAIDUID_BFESS=F0147F17F26EC14E47BCF2AEFFE12921:FG=1; ZFY=xeG8XGnodAhNOH37maKvpHgvVS4yWCQShEruX4R6o2w:C; BCLID=4201984001025888909; BCLID_BFESS=4201984001025888909; BDSFRCVID=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; BDSFRCVID_BFESS=MSkOJeC62GHnemvfyjuGhfuCPvJiZXjTH6aoT49yHx85QsgAUu5oEG0P0U8g0KuM5NlwogKK3gOTH4DF_2uxOjjg8UtVJeC6EG0Ptf8g0f5; H_BDCLCKID_SF=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; H_BDCLCKID_SF_BFESS=JnI8oCD2JIK3Hnnp5-J25KCQbMDDJT8XbD5jVM7Dbl7keq8CD6jIW4KuhxjIKTjJ2Ict5MJ8WCQsKqc2y5jHhUL4yfTv2-T--27phlvRfnjpsIJMhxFWbT8UQ4cT04r0aKviaKOjBMb1MhbDBT5h2M4qMxtOLR3pWDTm_q5TtUJMeCnTD-Dhe6jWDH8HJ6-jfKQtW4thHDKaJbTp24QEq4tHeNt8-URZ5m7LaUoOtbnEHRQwjjQbM-K0Ln3MBMPj52OnanRsaqo6ED5c5RjbLpFm346-35543bRTLnLy5KJvfJoD3h3ChP-UyN3MWh37Je3lMKoaMp78jR093JO4y4Ldj4oxJpOJ5JbMopCafD8bhDtGjTt-en-W5gTBa4T8bC_X3buQtII28pcNLTDKjx4eD-6BKJcvWJn30t5nLPL-MxoKKlO1j4_eQh642-Lq2Kvr0nT7atTvhl5jDh0K25ksD-4JW45AWHby0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qhja0tJT0ttJ-sL-35HJj2jtJvKn8_-P6MXGjCWMT-0bFH5xJgWx7ke4Te55osbRLd0h3eQpRxJan7_JjOQI-BJqn9e5oGKP-b54tOaUQxtN4j2CnjtpvP8D58hbJobUPUDUJ9LUkJ3gcdot5yBbc8eIna5hjkbfJBQttjQn3hfIkj0DKLJIthhK-GD6A35n-WqxQjaROy-5PX3buQBxOO8pcNLTDKQUkHbfcqKJcBbmv30t5nLPQkjfcyMlO1j4_e3a88hxvOfnTIQb3pQ4KhDp5jDh023jksD-RtW6QwaTvy0hvctb3cShPmQMjrDRLbXU6BK5vPbNcZ0l8K3l02V-bIe-t2b6Qh-p52f6KetJFf3e; XFI=d8cdfac0-c78c-11ed-9363-11fb77acfb22; Hm_lpvt_01e907653ac089993ee83ed00ef9c2f3=1679364327; __bid_n=186fca84a5b9216b5752e1; ab_sr=1.0.1_MDhlMzhhZjkyZDJiYWNlOGRhMjdkNjQ1YzczYjkzNDA0YzAyODQ1ZDhhNjNmMDAzZTZkMDM2ZmVhNGEzMzUwZTcwOTc3ODlmMWU0NjgzYzY1OWMyOWE2YTYyMjcxMDA3N2IwNTBkZTEzZGJiMTc4MjM4YjkzOTEwMmFmYjlhNzI4NzA5ZjY4ZjQxMmFiYWJjNDRiN2NjM2ZjNTBlNjQyNGQ0NTFiZGNlYmVkZjc0MWRmMDg5MWJiZTI5ODlhNzlm; XFCS=D4DA282258F7335484898A2AF69DC41EA15E3806075C8AAFEA6DDBE278479D6E; XFT=s25AHfKy5Z9kcvgUrcryF3aCf5jg21c06GlfhkXUHWw=; BDRCVFR[VXHUG3ZuJnT]=mk3SLVN4HKm; H_PS_PSSID=26350"));
            //cc.Add(new System.Uri("https://yiyan.baidu.com"), new Cookie("token", "1679288415966_1679364661713_iUtUdtoyITmXeBfte7MCnnK8oI/JVoX8qSHD7dEk/qtqgG5DD1JKOgEqQGD+msq4GuE05P//gtrrZjQxk0dwRuFPCSdlrfU9duyrDWkp0PeyFljeboqFVso55q4it+VrbQPUW34pdj80wLEIty8YrBGL7NvlqnmDTly+w/my5pvhAFPr4YD7RzY/xMP//Agyxi/wMj8bJP8rcozzEmWbaLOxslIZPgO5PFN/KBVN/tj9ZYJJDt5xvYk8ZwHHFgA4039fco4g20z32qhEs5JEM4Jtwuyyd7Jas0esf6GC36Af+FGzKL/hMbd3kFS3giu0DNLBz4CEY01UoqqVCSkBgA=="));
            //request.CookieContainer = cc;
            byte[] byteData = Encoding.UTF8.GetBytes(jsonParam);
            int length = byteData.Length;
            request.ContentLength = length;
            Stream writer = request.GetRequestStream();
            writer.Write(byteData, 0, length);
            writer.Close();
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();

            JObject jo = JObject.Parse(responseString);
            JObject joo = (JObject)JsonConvert.DeserializeObject(responseString);
            string zone = jo["Data"]["id"].ToString();

            System.Threading.Thread.Sleep(1 * 20 * 1000); //休眠30秒 
            string _url2 = string.Format("https://modelscope.cn/api/v1/models/damo/cv_diffusion_text-to-image-synthesis/widgets/query");
            string jsonParam2 = "{\"id\":\"" + zone + "\"}";
            var request2 = (HttpWebRequest)WebRequest.Create(_url2);
            request2.Method = "POST";
            request2.ContentType = "application/json;charset=UTF-8";//ContentType
            request2.Headers.Add("cookie", "uuid_tt_dd=11_27076939590-1679470980362-017728; dc_session_id=11_1679470980362.638130; c_segment=0; c_page_id=default; cna=ffxkG9VIjgkCAXBeH6SxVDo+; xlly_s=1; dp_token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpZCI6MTkwNzAwNywiZXhwIjoxNjgwMDc1Nzg5LCJpYXQiOjE2Nzk0NzA5ODksInVzZXJuYW1lIjoid2VpeGluXzM5MjY5NzQwIn0.RV9_FW84VdXJM0roKBuzUwglc5DrpFdRA687nRC1gRA; UserName=weixin_39269740; UN=weixin_39269740; c_ref=https%3A//community.modelscope.cn/; c_first_ref=community.modelscope.cn; c_pref=https%3A//community.modelscope.cn/; c_first_page=https%3A//community.modelscope.cn/%3F; c_dsid=11_1679470991475.861447; dc_tos=rrww3z; log_Id_pv=3; _samesite_flag_=true; cookie2=1d1928d7774c4ae1fcdc9391baf7953c; t=cb3738081446cb99b5a4dbf3415aa368; _tb_token_=75ea6a93eb370; csg=ed524eae; m_session_id=d12d527f-624f-40ae-8cf6-bcb76e634ef1; h_uid=2215618637015; log_Id_view=7; log_Id_click=3; isg=BGZmIhFXPusWM-pPMfirKMXVt9zoR6oBS9jq6lA9Pw6w01LtDNDWEb_tL8_f-6IZ");
            byte[] byteData2 = Encoding.UTF8.GetBytes(jsonParam2);
            int length2 = byteData2.Length;
            request2.ContentLength = length2;
            Stream writer2 = request2.GetRequestStream();
            writer2.Write(byteData2, 0, length2);
            writer2.Close();
            var response2 = (HttpWebResponse)request2.GetResponse();
            var responseString2 = new StreamReader(response2.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
            JObject jooo = (JObject)JsonConvert.DeserializeObject(responseString2);
            string zoneo = jooo["Data"]["data"]["output_imgs"][0].ToString();
            string zone1 = jooo["Data"]["data"]["output_imgs"][1].ToString();
            WebRequest imgRequest0 = WebRequest.Create(zoneo);
            WebRequest imgRequest1 = WebRequest.Create(zone1);
            System.Drawing.Image downImage = System.Drawing.Image.FromStream(imgRequest0.GetResponse().GetResponseStream());
            System.Drawing.Image downImage1 = System.Drawing.Image.FromStream(imgRequest1.GetResponse().GetResponseStream());
            string deerory = string.Format(@"D:\web\web6\wwwroot\web\img\{0}\", DateTime.Now.ToString("yyyy-MM-dd"));
            string deeroryrr = string.Format(@"web\img\{0}\", DateTime.Now.ToString("yyyy-MM-dd"));
            string fileName = string.Format("{0}.png", DateTime.Now.ToString("HHmmssffff"));
            string fileName1 = string.Format("{0}.png", DateTime.Now.ToString("HHmmssffff") + "1");
            if (!System.IO.Directory.Exists(deerory))
            {
                System.IO.Directory.CreateDirectory(deerory);
            }

            downImage.Save(deerory + fileName);
            downImage.Dispose();
            Bitmap source = new Bitmap(deerory + fileName);
            Rectangle section = new Rectangle(new Point(1, 1), new Size(1363, 1240));
            Bitmap CroppedImage = CropImage(source, section);
            string str = deerory + "jt" + fileName;
            CroppedImage.Save(@str);

            downImage1.Save(deerory + fileName1);
            downImage1.Dispose();
            Bitmap source1 = new Bitmap(deerory + fileName1);
            Rectangle section1 = new Rectangle(new Point(1, 1), new Size(1363, 1240));
            Bitmap CroppedImage1 = CropImage(source1, section1);
            CroppedImage1.Save(deerory + "jt" + fileName1);

            var m0 = deeroryrr + "jt" + fileName;
            var m1 = deeroryrr + "jt" + fileName1;

            resluts = m0 + "&" + m1;
            //resluts = "";

            return resluts;
        }
        private IList<Articleiteminfo1> GetItemEntityCore1(string sqlstr, string filter = "")
        {
            var list1 = new List<Articleiteminfo1>();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Ymsoft";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var i = new Articleiteminfo1();
                var date = dr;
                i.Title = date["文档链接"].ToString();
                i.projectcount = date["技术审评报告名称"].ToString();
                list1.Add(i);
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }
        private IList<Articleiteminfo1> GetItemEntityCore156(string sqlstr, string filter = "")
        {
            var list1 = new List<Articleiteminfo1>();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var i = new Articleiteminfo1();
                var date = dr;
                i.Title = date["类型"].ToString();
                i.projectcount = date["法规文件名称"].ToString();
                i.mill = date["NMPA发布名称"].ToString();
                i.title = date["官网链接"].ToString();
                i.Id = date["发布时间"].ToString();
                i.other = date["索引号"].ToString();
                list1.Add(i);
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }
        private IList<Articleiteminfo1> GetItemEntityCore15(string sqlstr, string filter = "")
        {
            var list1 = new List<Articleiteminfo1>();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var i = new Articleiteminfo1();
                var date = dr;
                i.Title = date["title"].ToString();
                i.projectcount = date["mill"].ToString();
                i.mill = date["other"].ToString();
                i.other = date["postkeyurl"].ToString();
                list1.Add(i);
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }
        private IList<Articleiteminfo1> GetItemEntityCore14(string sqlstr, string filter = "")
        {
            var list1 = new List<Articleiteminfo1>();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Ymsoft";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var i = new Articleiteminfo1();
                var date = dr;
                i.Title = date["技术审评报告名称"].ToString();
                i.projectcount = Convert.ToDateTime(date["发布时间"]).ToString("yyyy-MM-dd");
                i.mill = date["文档链接"].ToString();

                list1.Add(i);
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }
        private IList<CoreCmsNews> GetItemEntityCore(string sqlstr, string filter = "")
        {
            var list1 = new List<CoreCmsNews>();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=CoreShopProfessional";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var i = new CoreCmsNews();
                var date = dr;
                i.id = Convert.ToInt32(date["id"]);

                i.createTime = Convert.ToDateTime(date["createTime"].ToString() == null ? DateTime.Now.ToString() : date["createTime"].ToString()).ToLongDateString();
                if (filter == "0")
                {
                    i.title = date["title"].ToString();
                    i.brief = date["brief"].ToString();
                    i.contentBody = date["contentBody"].ToString();
                    i.createTime = Convert.ToDateTime(date["createTime"].ToString() == null ? DateTime.Now.ToString() : date["createTime"].ToString()).ToLongDateString();
                }
                else if (filter == "1")
                {
                    i.title = date["entitle"].ToString();
                    i.brief = date["enbrief"].ToString();
                    i.contentBody = date["encontentBody"].ToString();

                }
                i.coverImage = date["coverImage"].ToString();
                i.typeId = Convert.ToInt32(date["typeId"]);
                i.sort = Convert.ToInt32(date["sort"]);


                list1.Add(i);
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> DetailModule()
        {
            var hot = await _articleService.Hot(6);
            var category = await _categoryService.GetRootCategories();
            var random = await _articleService.Random();
            return Json(new { hot, category, random });
        }
    }
}