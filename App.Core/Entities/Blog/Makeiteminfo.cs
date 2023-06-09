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
    public class Makeiteminfo : Entity<string>, ISoftDelete
    {
        /// <summary>
        /// 标题
        /// </summary>
        /// 

        public string PathName { get; set; }
        public string Pathpapes { get; set; }
        public string Other { get; set; }
        public string TitleName { get; set; }
        public string JBXXdengjihao { get; set; }

        public string TitleStage { get; set; }
        public string JBXXxiangguandengjihao { get; set; }
        public string JBXXchengyongming { get; set; }
        public string JBXXyaowuleixin { get; set; }
        public string JBXXlinchuangshoushenqing { get; set; }
        public string JBXXshiyingzheng { get; set; }
        public string JBXXshiyantongshutimu { get; set; }
        public string JBXXshiyanzhuanyetimu { get; set; }
        public string JBXXshiyanfanganbianhao { get; set; }
        public string JBXXfanganzuijing { get; set; }
        public string JBXXbanbenriqi { get; set; }
        public string JBXXfanganshifou { get; set; }
        public string SQRXXshengqingrenmingcheng { get; set; }
        public string SQRXXlianxirenxingming { get; set; }
        public string SQRXXlianxirenzuoji { get; set; }
        public string SQRXXlianxirenshoujihao { get; set; }
        public string SQRXXlianxirenemail { get; set; }
        public string SQRXXlianxirenyouzhendizhi { get; set; }
        public string SQRXXlianxirenyoubian { get; set; }
        public string LCSYXXshiyanfenlie { get; set; }
        public string LCSYXXshiyanfenqi { get; set; }
        public string LCSYXXshiyanmudi { get; set; }
        public string LCSYXXshuijihua { get; set; }
        public string LCSYXXmanfang { get; set; }
        public string LCSYXXshiyanfangwei { get; set; }
        public string LCSYXXshejiliexing { get; set; }
        public string LCSYXXninaliang { get; set; }
        public string LCSYXXxingbei { get; set; }
        public string LCSYXXjiankuangshoushizhen { get; set; }
        public string LCSYXXruxuanbiaozhun { get; set; }
        public string LCSYXXruxuanbiaozhun1 { get; set; }
        public string LCSYXXruxuanbiaozhun2 { get; set; }
        public string LCSYXXruxuanbiaozhun3 { get; set; }
        public string LCSYXXruxuanbiaozhun4 { get; set; }
        public string LCSYXXruxuanbiaozhun5 { get; set; }
        public string LCSYXXruxuanbiaozhun6 { get; set; }
        public string LCSYXXruxuanbiaozhun7 { get; set; }
        public string LCSYXXruxuanbiaozhun8 { get; set; }
        public string LCSYXXruxuanbiaozhun9 { get; set; }
        public string LCSYXXruxuanbiaozhun10 { get; set; }
        public string LCSYXXruxuanbiaozhun11 { get; set; }
        public string LCSYXXruxuanbiaozhun12 { get; set; }
        public string LCSYXXruxuanbiaozhun13 { get; set; }
        public string LCSYXXruxuanbiaozhun14 { get; set; }
        public string LCSYXXruxuanbiaozhun15 { get; set; }
        public string LCSYXXruxuanbiaozhun16 { get; set; }
        public string LCSYXXruxuanbiaozhun17 { get; set; }
        public string LCSYXXruxuanbiaozhun18 { get; set; }
        public string LCSYXXruxuanbiaozhun19 { get; set; }
        public string LCSYXXruxuanbiaozhun20 { get; set; }
        public string LCSYXXpaichubiaozhun { get; set; }
        public string LCSYXXpaichubiaozhun1 { get; set; }
        public string LCSYXXpaichubiaozhun2 { get; set; }
        public string LCSYXXpaichubiaozhun3 { get; set; }
        public string LCSYXXpaichubiaozhun4 { get; set; }
        public string LCSYXXpaichubiaozhun5 { get; set; }
        public string LCSYXXpaichubiaozhun6 { get; set; }
        public string LCSYXXpaichubiaozhun7 { get; set; }
        public string LCSYXXpaichubiaozhun8 { get; set; }
        public string LCSYXXpaichubiaozhun9 { get; set; }
        public string LCSYXXpaichubiaozhun10 { get; set; }
        public string LCSYXXpaichubiaozhun11 { get; set; }
        public string LCSYXXpaichubiaozhun12 { get; set; }
        public string LCSYXXpaichubiaozhun13 { get; set; }
        public string LCSYXXpaichubiaozhun14 { get; set; }
        public string LCSYXXpaichubiaozhun15 { get; set; }
        public string LCSYXXpaichubiaozhun16 { get; set; }
        public string LCSYXXpaichubiaozhun17 { get; set; }
        public string LCSYXXpaichubiaozhun18 { get; set; }
        public string LCSYXXpaichubiaozhun19 { get; set; }
        public string LCSYXXpaichubiaozhun20 { get; set; }
        public string SYFZshiyanyaomingcheng { get; set; }
        public string SYFZshiyanyaoyongfa { get; set; }
        public string SYFZduizhaomyaomingcheng { get; set; }
        public string SYFZduizhaoyaoyongfa { get; set; }
        public string ZDZBZDZBzhibiao { get; set; }
        public string ZDZBZDZBzhibiao1 { get; set; }
        public string ZDZBZDZBzhibiao2 { get; set; }
        public string ZDZBZDZBpingjiashijian { get; set; }
        public string ZDZBZDZBpingjiashijian1 { get; set; }
        public string ZDZBZDZBpingjiashijian2 { get; set; }
        public string ZDZBZDZBzongdianzhibiaoxuanzhe { get; set; }
        public string ZDZBZDZBzongdianzhibiaoxuanzhe1 { get; set; }
        public string ZDZBZDZBzongdianzhibiaoxuanzhe2 { get; set; }
        public string ZDZBCYZDZBzhibiao { get; set; }
        public string ZDZBCYZDZBzhibiao1 { get; set; }
        public string ZDZBCYZDZBzhibiao2 { get; set; }
        public string ZDZBCYZDZBzhibiao3 { get; set; }
        public string ZDZBCYZDZBzhibiao4 { get; set; }
        public string ZDZBCYZDZBzhibiao5 { get; set; }
        public string ZDZBCYZDZBpingjiashijian { get; set; }
        public string ZDZBCYZDZBpingjiashijian1 { get; set; }
        public string ZDZBCYZDZBpingjiashijian2 { get; set; }
        public string ZDZBCYZDZBpingjiashijian3 { get; set; }
        public string ZDZBCYZDZBpingjiashijian4 { get; set; }
        public string ZDZBCYZDZBpingjiashijian5 { get; set; }
        public string ZDZBCYZDZBzongdianzhibiaoxuanzhe { get; set; }
        public string ZDZBCYZDZBzongdianzhibiaoxuanzhe1 { get; set; }
        public string ZDZBCYZDZBzongdianzhibiaoxuanzhe2 { get; set; }
        public string ZDZBCYZDZBzongdianzhibiaoxuanzhe3 { get; set; }
        public string ZDZBCYZDZBzongdianzhibiaoxuanzhe4 { get; set; }
        public string ZDZBCYZDZBzongdianzhibiaoxuanzhe5 { get; set; }
        public string ZDZBshujuanquanjianchaweiyuanhui { get; set; }
        public string ZDZBweishouzhezheguomaishiyan { get; set; }
        public string YJZXXxingming { get; set; }
        public string YJZXXxingming1 { get; set; }
        public string YJZXXxuewei { get; set; }
        public string YJZXXxuewei1 { get; set; }
        public string YJZXXzhicheng { get; set; }
        public string YJZXXzhicheng1 { get; set; }
        public string YJZXXdianhuan { get; set; }
        public string YJZXXdianhuan1 { get; set; }
        public string YJZXXemail { get; set; }
        public string YJZXXemail1 { get; set; }
        public string YJZXXyouzhenbiaoma { get; set; }
        public string YJZXXyouzhenbiaoma1 { get; set; }
        public string YJZXXyoubian { get; set; }
        public string YJZXXyoubian1 { get; set; }
        public string YJZXXdangweimingcheng { get; set; }
        public string YJZXXdangweimingcheng1 { get; set; }
        public string GCJJGmingcheng { get; set; }
        public string GCJJGmingcheng1 { get; set; }
        public string GCJJGmingcheng2 { get; set; }
        public string GCJJGmingcheng3 { get; set; }
        public string GCJJGmingcheng4 { get; set; }
        public string GCJJGmingcheng5 { get; set; }
        public string GCJJGmingcheng6 { get; set; }
        public string GCJJGmingcheng7 { get; set; }
        public string GCJJGmingcheng8 { get; set; }
        public string GCJJGmingcheng9 { get; set; }
        public string GCJJGmingcheng10 { get; set; }
        public string GCJJGmingcheng11 { get; set; }
        public string GCJJGmingcheng12 { get; set; }
        public string GCJJGmingcheng13 { get; set; }
        public string GCJJGmingcheng14 { get; set; }
        public string GCJJGmingcheng15 { get; set; }
        public string GCJJGmingcheng16 { get; set; }
        public string GCJJGmingcheng17 { get; set; }
        public string GCJJGmingcheng18 { get; set; }
        public string GCJJGmingcheng19 { get; set; }
        public string GCJJGmingcheng20 { get; set; }
        public string GCJJGmingcheng21 { get; set; }
        public string GCJJGmingcheng22 { get; set; }
        public string GCJJGmingcheng23 { get; set; }
        public string GCJJGmingcheng24 { get; set; }
        public string GCJJGmingcheng25 { get; set; }
        public string GCJJGmingcheng26 { get; set; }
        public string GCJJGmingcheng27 { get; set; }
        public string GCJJGmingcheng28 { get; set; }
        public string GCJJGmingcheng29 { get; set; }
        public string GCJJGmingcheng30 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe { get; set; }
        public string GCJJGzhuyaoyanjiuzhe1 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe2 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe3 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe4 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe5 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe6 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe7 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe8 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe9 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe10 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe11 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe12 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe13 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe14 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe15 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe16 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe17 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe18 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe19 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe20 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe21 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe22 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe23 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe24 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe25 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe26 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe27 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe28 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe29 { get; set; }
        public string GCJJGzhuyaoyanjiuzhe30 { get; set; }
        public string GCJJGguojia { get; set; }
        public string GCJJGguojia1 { get; set; }
        public string GCJJGguojia2 { get; set; }
        public string GCJJGguojia3 { get; set; }
        public string GCJJGguojia4 { get; set; }
        public string GCJJGguojia5 { get; set; }
        public string GCJJGguojia6 { get; set; }
        public string GCJJGguojia7 { get; set; }
        public string GCJJGguojia8 { get; set; }
        public string GCJJGguojia9 { get; set; }
        public string GCJJGguojia10 { get; set; }
        public string GCJJGguojia11 { get; set; }
        public string GCJJGguojia12 { get; set; }
        public string GCJJGguojia13 { get; set; }
        public string GCJJGguojia14 { get; set; }
        public string GCJJGguojia15 { get; set; }
        public string GCJJGguojia16 { get; set; }
        public string GCJJGguojia17 { get; set; }
        public string GCJJGguojia18 { get; set; }
        public string GCJJGguojia19 { get; set; }
        public string GCJJGguojia20 { get; set; }
        public string GCJJGguojia21 { get; set; }
        public string GCJJGguojia22 { get; set; }
        public string GCJJGguojia23 { get; set; }
        public string GCJJGguojia24 { get; set; }
        public string GCJJGguojia25 { get; set; }
        public string GCJJGguojia26 { get; set; }
        public string GCJJGguojia27 { get; set; }
        public string GCJJGguojia28 { get; set; }
        public string GCJJGguojia29 { get; set; }
        public string GCJJGguojia30 { get; set; }
        public string GCJJGdiqu { get; set; }
        public string GCJJGdiqu1 { get; set; }
        public string GCJJGdiqu2 { get; set; }
        public string GCJJGdiqu3 { get; set; }
        public string GCJJGdiqu4 { get; set; }
        public string GCJJGdiqu5 { get; set; }
        public string GCJJGdiqu6 { get; set; }
        public string GCJJGdiqu7 { get; set; }
        public string GCJJGdiqu8 { get; set; }
        public string GCJJGdiqu9 { get; set; }
        public string GCJJGdiqu10 { get; set; }
        public string GCJJGdiqu11 { get; set; }
        public string GCJJGdiqu12 { get; set; }
        public string GCJJGdiqu13 { get; set; }
        public string GCJJGdiqu14 { get; set; }
        public string GCJJGdiqu15 { get; set; }
        public string GCJJGdiqu16 { get; set; }
        public string GCJJGdiqu17 { get; set; }
        public string GCJJGdiqu18 { get; set; }
        public string GCJJGdiqu19 { get; set; }
        public string GCJJGdiqu20 { get; set; }
        public string GCJJGdiqu21 { get; set; }
        public string GCJJGdiqu22 { get; set; }
        public string GCJJGdiqu23 { get; set; }
        public string GCJJGdiqu24 { get; set; }
        public string GCJJGdiqu25 { get; set; }
        public string GCJJGdiqu26 { get; set; }
        public string GCJJGdiqu27 { get; set; }
        public string GCJJGdiqu28 { get; set; }
        public string GCJJGdiqu29 { get; set; }
        public string GCJJGdiqu30 { get; set; }
        public string GCJJGchengshi { get; set; }
        public string GCJJGchengshi1 { get; set; }
        public string GCJJGchengshi2 { get; set; }
        public string GCJJGchengshi3 { get; set; }
        public string GCJJGchengshi4 { get; set; }
        public string GCJJGchengshi5 { get; set; }
        public string GCJJGchengshi6 { get; set; }
        public string GCJJGchengshi7 { get; set; }
        public string GCJJGchengshi8 { get; set; }
        public string GCJJGchengshi9 { get; set; }
        public string GCJJGchengshi10 { get; set; }
        public string GCJJGchengshi11 { get; set; }
        public string GCJJGchengshi12 { get; set; }
        public string GCJJGchengshi13 { get; set; }
        public string GCJJGchengshi14 { get; set; }
        public string GCJJGchengshi15 { get; set; }
        public string GCJJGchengshi16 { get; set; }
        public string GCJJGchengshi17 { get; set; }
        public string GCJJGchengshi18 { get; set; }
        public string GCJJGchengshi19 { get; set; }
        public string GCJJGchengshi20 { get; set; }
        public string GCJJGchengshi21 { get; set; }
        public string GCJJGchengshi22 { get; set; }
        public string GCJJGchengshi23 { get; set; }
        public string GCJJGchengshi24 { get; set; }
        public string GCJJGchengshi25 { get; set; }
        public string GCJJGchengshi26 { get; set; }
        public string GCJJGchengshi27 { get; set; }
        public string GCJJGchengshi28 { get; set; }
        public string GCJJGchengshi29 { get; set; }
        public string GCJJGchengshi30 { get; set; }
        public string LLWYHmingcheng1 { get; set; }
        public string LLWYHmingcheng2 { get; set; }
        public string LLWYHmingcheng3 { get; set; }
        public string LLWYHmingcheng4 { get; set; }
        public string LLWYHmingcheng5 { get; set; }
        public string LLWYHshechajiruan1 { get; set; }
        public string LLWYHshechajiruan2 { get; set; }
        public string LLWYHshechajiruan3 { get; set; }
        public string LLWYHshechajiruan4 { get; set; }
        public string LLWYHshechajiruan5 { get; set; }
        public string LLWYHchachariqi1 { get; set; }
        public string LLWYHchachariqi2 { get; set; }
        public string LLWYHchachariqi3 { get; set; }
        public string LLWYHchachariqi4 { get; set; }
        public string LLWYHchachariqi5 { get; set; }
        public string SYZTXXshiyanzhuantai { get; set; }
        public string SYZTXXmubiaoruzhurenshu { get; set; }
        public string SYZTXXyiruzhulishu { get; set; }
        public string SYZTXXshijiruzhuzonglishu { get; set; }
        public string SYZTXXdiyilieshoushizheqianshu { get; set; }
        public string SYZTXXdiyilieshoushizheruzhuriqi { get; set; }
        public string SYZTXXshiyanzongzhiriqi { get; set; }
        public string LCSYJGZYbanbenhao { get; set; }
        public string LCSYJGZYbanbenriqi { get; set; }



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
