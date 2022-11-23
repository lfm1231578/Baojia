using System;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Blog;
using App.Application.Blog.Dtos;
using App.Core.Entities.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SqlSugar;

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
            if (state != "" && state != null)
            {
                pagesize = Convert.ToInt32(state);
            }

            string shenfen = "";
            if (key != null)
            {
                shenfen = key.Split("DJGXXshen:")[1].Split("$$Dstag:")[0];
            }
            string table = "AirtleTable";
            if (shenfen != "")
            {
                table = table + "_" + shenfen;
            }
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
                    if (i.Contains("Dsname") && i.Split(":")[1] != "null") _where += "  AND Dsname   like '%" + i.Split(":")[1] + @"%' ";
                    //if (i.Contains("Dsshijian") && i.Split(":")[1]  != "null") _where += "  AND Dsshijian   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag") && i.Split(":")[1] != "null") _where += "  AND Dstag   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag") && i.Split(":")[1] != "null") _where += "  AND Dstag1   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag") && i.Split(":")[1] != "null") _where += "  AND Dstag2   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag") && i.Split(":")[1] != "null") _where += "  AND Dstag3   like '%" + i.Split(":")[1] + @"%' ";
                    if (i.Contains("Dstag") && i.Split(":")[1] != "null") _where += "  AND Dstag4   like '%" + i.Split(":")[1] + @"%' ";
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
                    if (i.Contains("DLCSYgcc") && i.Split(":")[1] != "null") _where += "  AND DLCSYgcc   like '%" + i.Split(":")[1] + @"%' ";

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
                }
                str = str + _where + _desc;
            }
            string name = string.Empty;
            var result = GetGoalsEntity(str);
            var data = result.Skip((index - 1) * pagesize).Take(pagesize).ToList();
            //List<BannerInfo> list = await _bannerService.GetListCacheAsync(null, o => o.SortCode, false);

            //var article = await _articleService.GetListCacheAsync(null, o => o.CreatorTime, false);
            ViewBag.Yixue = data;
            ViewBag.Count = result.Count();
            ViewBag.PageNum = (result.Count() - 1) / pagesize + 1;
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
            string str = " SELECT Other, TitleName, TitleStage,JBXXshiyingzheng,JBXXshiyantongshutimu,JBXXdengjihao FROM MakeTable order by Todesc desc ";
        
            var result = GetItemEntity(str);
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

        private IList<Makeiteminfo> GetItemEntity(string sqlstr, string filter = "")
        {
            var list1 = new List<Makeiteminfo>();
            string conStr = "server=192.168.10.28;user=sa;pwd=123456;database=Blog";//连接字符串  
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