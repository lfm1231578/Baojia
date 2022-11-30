using System;
using System.Collections.Generic;
using App.Core.Data;
using App.Core.Share;
using SqlSugar;

namespace App.Core.Entities.Blog
{
    /// <summary>
    /// 记录文章信息
    /// </summary>
    [Serializable]
    public class Articleiteminfo : Entity<string>, ISoftDelete
    {
        public IList<Makeiteminfo> list { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        public string projectcount { get; set; }

        public string hospitalname { get; set; }
        public string Hospitalid_Cra5 { get; set; }
        public string Hospitalid { get; set; }
        
        /// <summary>
        /// 标题
        /// </summary>
        public string Fiter { get; set; }
        public string DKSZYZHX1keshi1 { get; set; }
        public string DKSZYZHX1yanjiutuandui1 { get; set; }
        public string DKSZYZHX2keshi1 { get; set; }
        public string DKSZYZHX2yanjiutuandui1 { get; set; }
        public string DKSZYZHX3keshi1 { get; set; }
        public string DKSZYZHX3yanjiutuandui1 { get; set; }
        public string DJGXXjiajie1 { get; set; }
        public string DJGXXjiajie2 { get; set; }
        public string DJGXXjiajie3 { get; set; }
        public string DJGXXjiajie4 { get; set; }
        public string DJGXXjiajie5 { get; set; }

        public int Topdesc { get; set; }
        public string DLLWTHLLzkpl { get; set; }
        public string DLLWTHLLzkpl1 { get; set; }
        public string DLCSYqdny { get; set; }
        public string DLCSYgsszlx { get; set; }
        public string DLLWTHjiedaishijian { get; set; }
        public string DLLWTHwangzhi { get; set; }
        public string DJGXXlianxirenzhiwei1 { get; set; }
        public string DJGXXlianxiren1 { get; set; }
        public string DJGXXlianxirenphone1 { get; set; }
        public string DJGXXlianxirenemmail1 { get; set; }
        public string DJGXXlianxirenzhiwei2 { get; set; }
        public string DJGXXlianxiren2 { get; set; }
        public string DJGXXlianxirenphone2 { get; set; }
        public string DJGXXlianxirenemmail2 { get; set; }
        public string DJGXXllxllht { get; set; }

        public string Syname { get; set; }

        public string Syurl { get; set; }
        public string Sytag { get; set; }
        public string Sytag1 { get; set; }
        public string Sytag2 { get; set; }
        public string Sytag3 { get; set; }
        public string Sytag4 { get; set; }
        public string Syxiangmu { get; set; }
        public string Syjiedaishijian { get; set; }
        public string Syphnoe { get; set; }
        public string Syadress { get; set; }
        public string Dsname { get; set; }
        public string Dsshijian { get; set; }
        public string DJGXXjgwz { get; set; }
        public string DJGXXjgzzdm { get; set; }
        public string DJGXXsclxzl { get; set; }
        public string DJGXXscllzldj { get; set; }
        public string DJGXXzkxm { get; set; }
        public string DJGXXglfc { get; set; }
        public string Dstag { get; set; }
        public string Dstag1 { get; set; }
        public string Dstag2 { get; set; }
        public string Dstag3 { get; set; }
        public string Dstag4 { get; set; }
        public string Dstag5 { get; set; }
        public string Dstag6 { get; set; }
        public string Dsphone { get; set; }
        public string Dsemais { get; set; }
        public string Dsemail { get; set; }
        public string Dsjiedaishijian { get; set; }
        public string Dsadress { get; set; }
        public string Dsliurangshu { get; set; }
        public string DJGXXshen { get; set; }
        public string DJGXXweb { get; set; }
        public string DJGXXgcp { get; set; }
        public string DJGXXlianxirenzhiwei { get; set; }
        public string DJGXXlianxiren { get; set; }
        public string DJGXXlianxirenphone { get; set; }
        public string DJGXXlianxirenemmail { get; set; }
        public string DJGXXjiajie { get; set; }
        public string DBAXXYWshen { get; set; }
        public string DBAXXYWbenanhao { get; set; }
        public string DBAXXYWname { get; set; }
        public string DBAXXYWjibei { get; set; }
        public string DBAXXYWlianxiren { get; set; }
        public string DBAXXYWlianxiphone { get; set; }
        public string DBAXXYWzhuantai { get; set; }
        public string DBAXXYWshijian { get; set; }
        public string DBAXXYWjcrq { get; set; }
        public string DBAXXYWjclb { get; set; }
        public string DBAXXYWjdjcjg { get; set; }
        public string DBAXXYWclqk { get; set; }
        public string DBAXXYWjcrq1 { get; set; }
        public string DBAXXYWjclb1 { get; set; }
        public string DBAXXYWjdjcjg1 { get; set; }
        public string DBAXXYWclqk1 { get; set; }
        public string DBAXXYWzymc { get; set; }
        public string DBAXXYWzyyjz { get; set; }
        public string DBAXXYWzc { get; set; }
        public string DBAXXYWzybasj { get; set; }
        public string DBAXXYWzymc1 { get; set; }
        public string DBAXXYWzyyjz1 { get; set; }
        public string DBAXXYWzc1 { get; set; }
        public string DBAXXYWzybasj1 { get; set; }
        public string DBAXXYLshen { get; set; }
        public string DBAXXYLbenanhao { get; set; }
        public string DBAXXYLname { get; set; }
        public string DBAXXYLjibei { get; set; }
        public string DBAXXYLlianxiren { get; set; }
        public string DBAXXYLlianxiphone { get; set; }
        public string DBAXXYLzhuantai { get; set; }
        public string DBAXXYLshijian { get; set; }
        public string DBAXXYLzymc { get; set; }
        public string DBAXXYLzyyjz { get; set; }
        public string DBAXXYLzc { get; set; }
        public string DBAXXYLzybasj { get; set; }
        public string DBAXXYLzymc1 { get; set; }
        public string DBAXXYLzyyjz1 { get; set; }
        public string DBAXXYLzc1 { get; set; }
        public string DBAXXYLzybasj1 { get; set; }
        public string DLLWTHphone { get; set; }
        public string DLLWTHchuangzhen { get; set; }
        public string DLLWTHemail { get; set; }
        public string DLLWTHshen { get; set; }
        public string DLLWTHadress { get; set; }
        public string DLLWTHLXFSzhiwei { get; set; }
        public string DLLWTHLXFSmingzhi { get; set; }
        public string DLLWTHLXFSdianhua { get; set; }
        public string DLLWTHLXFSyouxiang { get; set; }
        public string DLLWTHLXFSzhiwei1 { get; set; }
        public string DLLWTHLXFSmingzhi1 { get; set; }
        public string DLLWTHLXFSdianhua1 { get; set; }
        public string DLLWTHLXFSyouxiang1 { get; set; }
        public string DLLWTHLLllzkpl { get; set; }
        public string DLLWTHLLllshxs { get; set; }
        public string DLLWTHLLllscfy { get; set; }
        public string DLLWTHLLxgzc { get; set; }
        public string DLLWTHLLllzkpl1 { get; set; }
        public string DLLWTHLLllshxs1 { get; set; }
        public string DLLWTHLLllscfy1 { get; set; }
        public string DLLWTHLLxgzc1 { get; set; }
        public string DKSZYZHX1zhiwei { get; set; }
        public string DKSZYZHX1mingzi { get; set; }
        public string DKSZYZHX1dianhua { get; set; }
        public string DKSZYZHX1email { get; set; }
        public string DKSZYZHX2zhiwei { get; set; }
        public string DKSZYZHX2mingzi { get; set; }
        public string DKSZYZHX2dianhua { get; set; }
        public string DKSZYZHX2email { get; set; }
        public string DKSZYZHX3zhiwei { get; set; }
        public string DKSZYZHX3mingzi { get; set; }
        public string DKSZYZHX3dianhua { get; set; }
        public string DKSZYZHX3email { get; set; }
        public string DKSZYZHX1 { get; set; }
        public string DKSZYZHX1weizhi { get; set; }
        public string DKSZYZHX1jingyan { get; set; }
        public string DKSZYZHX1keshi { get; set; }
        public string DKSZYZHX1yanjiutuandui { get; set; }
        public string DKSZYZHX1qita { get; set; }
        public string DKSZYZHX2 { get; set; }
        public string DKSZYZHX2weizhi { get; set; }
        public string DKSZYZHX2jingyan { get; set; }
        public string DKSZYZHX2keshi { get; set; }
        public string DKSZYZHX2yanjiutuandui { get; set; }
        public string DKSZYZHX2qita { get; set; }
        public string DKSZYZHX3 { get; set; }
        public string DKSZYZHX3weizhi { get; set; }
        public string DKSZYZHX3jingyan { get; set; }
        public string DKSZYZHX3keshi { get; set; }
        public string DKSZYZHX3yanjiutuandui { get; set; }
        public string DKSZYZHX3qita { get; set; }
        public string DLCSYqdlx { get; set; }
        public string DLCSYgcc { get; set; }
        public string DLCSYcjcws { get; set; }
        public string DLCSYjtlcdz { get; set; }
        public string DLCSYLXFSzhiwei { get; set; }
        public string DLCSYLXFSmingzhi { get; set; }
        public string DLCSYLXFSdianhua { get; set; }
        public string DLCSYLXFSdizhi { get; set; }
        public string DLCSYHJSS1 { get; set; }
        public string DLCSYHJSS2 { get; set; }
        public string DLCSYXMJY1 { get; set; }
        public string DLCSYXMJY11 { get; set; }
        public string DLCSYXMJY2 { get; set; }
        public string DLCSYXMJY21 { get; set; }
        public string DLCSYXMJY3 { get; set; }
        public string DLCSYXMJY31 { get; set; }
        public string DLCSYXMJY4 { get; set; }
        public string DLCSYXMJY41 { get; set; }
        public string DLCSYXMJY5 { get; set; }
        public string DLCSYXMJY51 { get; set; }
        public string DLCSYXMJY6 { get; set; }
        public string DLCSYXMJY61 { get; set; }
        public string DLCSYXMJY7 { get; set; }
        public string DLCSYXMJY71 { get; set; }
        public string DLCSYXMJY8 { get; set; }
        public string DLCSYXMJY81 { get; set; }
        public string DLCSYYJTD1 { get; set; }
        public string DLCSYYJTD2 { get; set; }
        public string DLCSYGDJS1 { get; set; }
        public string DLCSYGDJS2 { get; set; }


        /// <summary>
        /// 创作类型：0-原创；1-转载
        /// </summary>
        public CreativeType CreativeType { get; set; }

        /// <summary>
        /// 文章来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 来源链接
        /// </summary>
        public string SourceLink { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 内容摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime PublishDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否置顶
        /// </summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool DeleteMark { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadTimes { get; set; } = 0;

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatorTime { get; set; } = DateTime.Now;
    }
}
