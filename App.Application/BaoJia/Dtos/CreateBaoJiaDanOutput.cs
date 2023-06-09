using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Dtos
{

    public class CreateBaoJiaDanOutput
    {
        /// <summary>
        /// 表格文档路径
        /// </summary>
        public OutputFile XlsFile { get; set; }

        /// <summary>
        /// PDF文档路径
        /// </summary>
        public OutputFile PdfFile { get; set; }

        /// <summary>
        ///Word文档路径
        /// </summary>
        public OutputFile DocFile { get; set; }

        /// <summary>
        /// 试验方案
        /// </summary>
        public ShiYanFangAnDto ShiYanFangAn { get; set; }

        /// <summary>
        /// 表1，项目预算总表
        /// </summary>
        public XiangMuYuSuanZongBiaoDto XiangMuYuSuanZongBiao { get; set; }
    }

    public class OutputFile
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 下载路径
        /// </summary>

        public string Url { get; set; }
    }

    /// <summary>
    /// 试验方案
    /// </summary>
    public class ShiYanFangAnDto
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; } = String.Empty;

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = String.Empty;
        /// <summary>
        /// 总病例数
        /// </summary>
        public int ZongBingLiShu { get; set; }

        /// <summary>
        /// 总时限（月）
        /// </summary>
        public int ZongShiXian { get; set; }
        /// <summary>
        /// 确定中心数
        /// </summary>
        public int QueDingZhongXinShu { get; set; }

        /// <summary>
        /// 医疗器械临床研究费用预算清单
        /// </summary>
        public YanJiuYuSuanQingDanDto YanJiuYuSuanQingDan { get; set; }

        /// <summary>
        /// (一)临床试验服务费用
        /// </summary>
        public LinChuangShiYanFuWuFeiDto LinChuangShiYanFuWuFei { get; set; }

        /// <summary>
        /// (二)日常费用 
        /// </summary>
        public RiChangFeiDto RiChangFei { get; set; }

        /// <summary>
        /// 附表 1
        /// </summary>
        public ChaiLvFuFeiBiaoDto ChaiLvFuFeiBiao { get; set; }

        /// <summary>
        /// (三)会议费用
        /// </summary>
        public HuiYiFeiDto HuiYiFei { get; set; }

        /// <summary>
        /// (四) 临床试验研究费用
        /// </summary>
        public LinChuangShiYanYanJiuFeiDto LinChuangShiYanYanJiuFei { get; set; }

        /// <summary>
        /// (五) SMO 费用
        /// </summary>
        public SMOFeiDto SMOFei { get; set; }

    }

    /// <summary>
    /// 医疗器械临床研究费用预算清单
    /// </summary>
    public class YanJiuYuSuanQingDanDto
    {

        /// <summary>
        /// CRO 服务费用(包含数统)
        /// </summary>
        public string CRO_FuWuFei { get; set; }

        /// <summary>
        /// 日常费用(包含差旅费用)
        /// </summary>
        public string RiChangFei { get; set; }

        /// <summary>
        /// 会议费用
        /// </summary>
        public string HuiYiFei { get; set; }

        /// <summary>
        /// CRO 服务费合计(税 6%)
        /// </summary>
        public string CRO_FuWuHeJi { get; set; }

        /// <summary>
        /// 医院研究费用
        /// </summary>
        public string YiYuanYanJiuFei { get; set; }

        /// <summary>
        /// SMO 费用
        /// </summary>
        public string SMOFei { get; set; }

        /// <summary>
        /// 合计(税 6%)
        /// </summary>
        public string HanShuiHeJi { get; set; }
    }

    /// <summary>
    /// (一)临床试验服务费用
    /// </summary>
    public class LinChuangShiYanFuWuFeiDto
    {
        /// <summary>
        /// 1.筛选研究单位
        /// </summary>
        public string ShaiXuanYanJiuDanWei { get; set; }

        /// <summary>
        /// 1.筛选研究单位 子项
        /// </summary>
        public string ShaiXuanYanJiuDanWei_1 { get; set; }
        /// <summary>
        /// 2.临床试验方案设计
        /// </summary>
        public string LinChuangShiYanFangAnSheJi { get; set; }


        /// <summary>
        /// 临床试验方案设计_子项1
        /// </summary>
        public string LinChuangShiYanFangAnSheJi_1 { get; set; }

        /// <summary>
        ///3. CRO服务费用
        /// </summary>
        public string CROFuWuFei { get; set; }

        /// <summary>
        /// CRO服务费用_子项1（试验前准备 1，试验前准备 2，试验前准备 3）
        /// </summary>
        public string CROFuWuFei_1 { get; set; }

        /// <summary>
        ///  CRO服务费用_子项1（试验前准备 1，试验前准备 2，试验前准备 3）单价
        /// </summary>
        public string CROFuWuFei_1_Price { get; set; }

        /// <summary>
        /// CRO服务费用_子项2（监查访视）
        /// </summary>
        public string CROFuWuFei_2 { get; set; }


        /// <summary>
        /// CRO服务费用_子项3（试验结束访视）
        /// </summary>
        public string CROFuWuFei_3 { get; set; }

        /// <summary>
        ///  CRO服务费用_子项3（试验结束访视） 单价
        /// </summary>
        public string CROFuWuFei_3_Price { get; set; }

        /// <summary>
        /// CRO服务费用_子项4（项目管理）
        /// </summary>
        public string CROFuWuFei_4 { get; set; }

        /// <summary>
        /// CRO服务费用_子项5（试验资料的内部专家审核）
        /// </summary>
        public string CROFuWuFei_5 { get; set; }


        /// <summary>
        /// 4. 数据管理和统计分析
        /// </summary>

        public string ShuJuGuanLiHeTongJiFenXi { get; set; }

        /// <summary>
        /// 数据管理和统计分析_子项1(数据管理)
        /// </summary>
        public string ShuJuGuanLiHeTongJiFenXi_1 { get; set; }

        /// <summary>
        /// 数据管理和统计分析_子项2(统计分析)
        /// </summary>
        public string ShuJuGuanLiHeTongJiFenXi_2 { get; set; }

        /// <summary>
        /// 5. 总结
        /// </summary>
        public string ZongJie { get; set; }

        /// <summary>
        /// 总结_子项1(资料回收、归档)
        /// </summary>
        public string ZongJie_1 { get; set; }

        /// <summary>
        /// 总结_子项2(临床研究总结报告及盖章)
        /// </summary>
        public string ZongJie_2 { get; set; }

        /// <summary>
        /// 6.合计
        /// </summary>
        public string HeJi { get; set; }

        /// <summary>
        /// 7.含税合计（税率6%）
        /// </summary>
        public string HanShuiHeJi { get; set; }

    }


    /// <summary>
    /// (二)日常费用
    /// </summary>
    public class RiChangFeiDto
    {

        /// <summary>
        /// 办公费及通讯费
        /// </summary>
        public string BanGongTongXun { get; set; }

        /// <summary>
        /// 临床试验文件印刷装订费用
        /// </summary>
        public string YinShuaZhuangDing { get; set; }

        /// <summary>
        /// 差旅费
        /// </summary>
        public string ChaiLv { get; set; }

        /// <summary>
        /// 合计
        /// </summary>
        public string HeJi { get; set; }

        /// <summary>
        /// 含税合计（税率6%）
        /// </summary>
        public string HanShuiHeJi { get; set; }
    }

    /// <summary>
    /// 附表 1 差旅附表
    /// </summary>
    public class ChaiLvFuFeiBiaoDto
    {
        /// <summary>
        /// 项目经理差旅费、住宿、餐饮、话费及补贴
        /// </summary>
        public string PMChaiLvFei { get; set; }

        /// <summary>
        /// 项目经理差旅费、住宿、餐饮、话费及补贴 单价
        /// </summary>
        public string PMChaiLvFei_Price { get; set; }


        /// <summary>
        /// 监查员差旅费、住宿、餐饮、话费及补贴
        /// </summary>
        public string CRAChaiLvFei { get; set; }

        /// <summary>
        /// 监查员差旅费、住宿、餐饮、话费及补贴 单价
        /// </summary>
        public string CRAChaiLvFei_Price { get; set; }

        /// <summary>
        /// 管理人员差旅、住宿、餐饮、话费及补贴 
        /// </summary>
        public string AdminChaiLvFei { get; set; }

        /// <summary>
        /// 管理人员差旅、住宿、餐饮、话费及补贴 单价
        /// </summary>
        public string AdminChaiLvFei_Price { get; set; }

    }

    /// <summary>
    /// (三)会议费用
    /// </summary>
    public class HuiYiFeiDto
    {
        /// <summary>
        /// 临床试验方案专 家讨论会
        /// </summary>
        public string YanTaoHui { get; set; }

        /// <summary>
        /// 项目启动会
        /// </summary>

        public string QiDongHui { get; set; }

        /// <summary>
        /// 项目启动会 单价
        /// </summary> 
        public string QiDongHui_Price { get; set; }
        /// <summary>
        /// 数据审核会
        /// </summary>
        public string ShenJiHui { get; set; }
        /// <summary>
        /// 合计
        /// </summary>
        public string HeJi { get; set; }

        /// <summary>
        /// 含税合计（税率6%）
        /// </summary>
        public string HanShuiHeJi { get; set; }


    }

    /// <summary>
    /// (四) 临床试验研究费用
    /// </summary>
    public class LinChuangShiYanYanJiuFeiDto
    {

        /// <summary>
        /// 伦理审查费  数量
        /// </summary>
        public string LunLiShenCha_Count { get; set; }

        /// <summary>
        /// 伦理审查费  单价（万元）
        /// </summary>
        public string LunLiShenCha_Price { get; set; }

        /// <summary>
        /// 伦理审查费  小计（万元）
        /// </summary>
        public string LunLiShenCha_Amount { get; set; }


        /// <summary>
        /// 组长单位牵头费  数量
        /// </summary>
        public string ZuZhangDanWei_Count { get; set; }

        /// <summary>
        /// 组长单位牵头费 单价（万元）
        /// </summary>
        public string ZuZhangDanWei_Price { get; set; }

        /// <summary>
        /// 组长单位牵头费 小计（万元）
        /// </summary>
        public string ZuZhangDanWei_Amount { get; set; }


        /// <summary>
        /// 研究者费  数量
        /// </summary>
        public string YanJiuZheFei_Count { get; set; }

        /// <summary>
        /// 研究者费 单价（万元）
        /// </summary>
        public string YanJiuZheFei_Price { get; set; }

        /// <summary>
        /// 研究者费 小计（万元）
        /// </summary>
        public string YanJiuZheFei_Amount { get; set; }


        /// <summary>
        /// 受试者补贴  实验组数量
        /// </summary>
        public string ShouShiZheBuTie_Count1 { get; set; }

        /// <summary>
        /// 受试者补贴 实验组单价（万元）
        /// </summary>
        public string ShouShiZheBuTie_Price1 { get; set; }

        /// <summary>
        /// 受试者补贴 实验组小计（万元）
        /// </summary>
        public string ShouShiZheBuTie_Amount1 { get; set; }

        /// <summary>
        /// 受试者补贴  对照组数量
        /// </summary>
        public string ShouShiZheBuTie_Count2 { get; set; }

        /// <summary>
        /// 受试者补贴 对照组单价（万元）
        /// </summary>
        public string ShouShiZheBuTie_Price2 { get; set; }

        /// <summary>
        /// 受试者补贴 对照组小计（万元）
        /// </summary>
        public string ShouShiZheBuTie_Amount2 { get; set; }

        /// <summary>
        /// 受试者检查  数量（万元）
        /// </summary>
        public string ShouShiZheJianCha_Count { get; set; }
        /// <summary>
        /// 受试者检查  单价（万元）
        /// </summary>
        public string ShouShiZheJianCha_Price { get; set; }
        /// <summary>
        /// 受试者检查  合计（万元）
        /// </summary>
        public string ShouShiZheJianCha_Amount { get; set; }

        /// <summary>
        /// 研究机构管理费  数量（万元）
        /// </summary>
        public string JiGouGuanLi_Count { get; set; }
        /// <summary>
        /// 研究机构管理费  单价（万元）
        /// </summary>
        public string JiGouGuanLi_Price { get; set; }
        /// <summary>
        /// 研究机构管理费  合计（万元）
        /// </summary>
        public string JiGouGuanLi_Amount { get; set; }


        /// <summary>
        /// 保险费用  数量（万元）
        /// </summary>
        public string BaoXian_Count { get; set; }
        /// <summary>
        /// 保险费用  单价（万元）
        /// </summary>
        public string BaoXian_Price { get; set; }
        /// <summary>
        /// 保险费用  数量（万元）
        /// </summary>
        public string BaoXian_Amount { get; set; }


        /// <summary>
        /// 合计
        /// </summary>
        public string HeJi { get; set; }

        /// <summary>
        /// 含税合计（税率6%）
        /// </summary>
        public string HanShuiHeJi { get; set; }

    }

    /// <summary>
    /// (五) SMO 费用
    /// </summary>
    public class SMOFeiDto
    {
        /// <summary>
        /// CRC 服务费用  实验组数量
        /// </summary>
        public string CRC_FuWu_Count1 { get; set; }

        /// <summary>
        ///CRC 服务费用 实验组单价
        /// </summary>
        public string CRC_FuWu_Price1 { get; set; }

        /// <summary>
        /// CRC 服务费用  实验组小计（万元）
        /// </summary>
        public string CRC_FuWu_Amount1 { get; set; }

        /// <summary>
        /// CRC 服务费用  对照组数量
        /// </summary>
        public string CRC_FuWu_Count2 { get; set; }

        /// <summary>
        /// CRC 服务费用 对照组单价（万元）
        /// </summary>
        public string CRC_FuWu_Price2 { get; set; }

        /// <summary>
        /// CRC 服务费用 对照组小计（万元）
        /// </summary>
        public string CRC_FuWu_Amount2 { get; set; }

        /// <summary>
        /// SMO 供应商管理费用
        /// </summary>
        public string SMO_GongYingShangGuanLi { get; set; }

        /// <summary>
        /// 含税合计（税率6%）
        /// </summary>
        public string HanShuiHeJi { get; set; }

        /// <summary>
        /// 折合每例(含税)
        /// </summary>
        public string AVG { get; set; }

        /// <summary>
        /// 受试者数量
        /// </summary>
        public int ShouShiZhe_Count { get; set; }

        /// <summary>
        /// 中心数
        /// </summary>
        public int Center_Count { get; set; }

        /// <summary>
        /// CRC数量
        /// </summary>
        public int CRC_Count { get; set; }

        /// <summary>
        /// CRC 项目经理数量
        /// </summary>
        public int CRC_PM_Count { get; set; }
    }
}