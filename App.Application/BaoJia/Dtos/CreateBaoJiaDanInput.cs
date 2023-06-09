using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Dtos
{

    public class CreateBaoJiaDanInput
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary> 
        public string CompanyName { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary> 
        public string ProjectName { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        public string Other { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Yushuan { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 例数
        /// </summary> 
        public int LiShu { get; set; }

        /// <summary>
        /// 总时限（月）
        /// </summary> 
        public int ShiXian { get; set; }


        /// <summary>
        /// 确定中心数
        /// </summary> 
        public int ZhongXinShu { get; set; }

        /// <summary>
        /// 难度
        /// </summary>
        [Required]
        public ENanDuXiShu NanDu { get; set; }

        /// <summary>
        /// 适应症
        /// </summary>
        public string ShiYingZheng { get; set; }

        /// <summary>
        /// 随访
        /// </summary>
        public int SuiFang { get; set; }


        /// <summary>
        /// 协调会费用
        /// </summary>
        public HuiYiFeiInput XieTiaoHui { get; set; } = new HuiYiFeiInput();

        /// <summary>
        /// 总结会费用
        /// </summary>
        public HuiYiFeiInput ZongJieHui { get; set; } = new HuiYiFeiInput();

        /// <summary>
        /// 中期会（或论证会）
        /// </summary>
        public HuiYiFeiInput ZhongQiHui { get; set; } = new HuiYiFeiInput();

        /// <summary>
        /// 盲态审核会
        /// </summary>
        public HuiYiFeiInput MangTaiShenHeHui { get; set; } = new HuiYiFeiInput();

        /// <summary>
        /// 组长费  单价
        /// </summary>
        /// <remarks>依据不同项目类型（或拟选机构要求）而定，结合商务计划书。</remarks>
        public double ZuZhangFei_Price { get; set; }
        /// <summary>
        /// 组长费  频次（中心或例数）
        /// </summary>
        /// <remarks>依据不同项目类型（或拟选机构要求）而定，结合商务计划书。</remarks>
        public int ZuZhangFei_Count { get; set; }

        /// <summary>
        /// 机构管理费  单价 （不可修改）
        /// </summary>
        /// <remarks>依据不同项目类型（或拟选机构要求）而定，结合商务计划书。</remarks>
        public double JiGouGuanLiFei_Price { get; set; }


        /// <summary>
        /// 伦理费  单价
        /// </summary>
        /// <remarks>按10000元/次标准，结合中心数*2倍=总次数（考虑重审及结题审查）。</remarks>
        public double LunLiFei_Price { get; set; }

        /// <summary>
        /// 合格病例费 单价(不可修改)
        /// </summary>
        /// <remarks>观察费和检查费(指合同签署支付医院，包括研究者劳务费、检查费、床位费、餐费等)。</remarks>
        public double HeGeBingLiFei_Price { get; set; }
        /// <summary>
        /// 合格病例费 频次（中心或例数）(不可修改)
        /// </summary>
        /// <remarks>观察费和检查费(指合同签署支付医院，包括研究者劳务费、检查费、床位费、餐费等)。</remarks>
        public double HeGeBingLiFei_Count { get; set; }


        /// <summary>
        /// 筛选病例费用 单价(不可修改)
        /// </summary>
        /// <remarks>依据不同项目的筛败工作量大小和筛选失败率来确定。预计筛败率为10%。</remarks>
        public double ShaiXuanBingLiFei_Price { get; set; }
        /// <summary>
        /// 筛选病例费用 频次（中心或例数）(不可修改)
        /// </summary>
        /// <remarks>依据不同项目的筛败工作量大小和筛选失败率来确定。预计筛败率为10%。</remarks>
        public double ShaiXuanBingLiFei_Count { get; set; }


        /// <summary>
        /// 启动会费  单价
        /// </summary>
        /// <remarks>依据中心数确定，通常每中心2000元。</remarks>
        public double QiDongHuiFei_Price { get; set; }

        /// <summary>
        /// 运输费  单价
        /// </summary>
        /// <remarks>依据中心数确定，通常每中心2000元。需根据具体项目要求来预算。</remarks>
        public double YunShuFei_Price { get; set; }

        /// <summary>
        /// 监查差旅费 单价 （不可修改）
        /// </summary>
        public double JianChaChaiLvFei_Price { get; set; }

        /// <summary>
        /// 试剂耗材费  单价
        /// </summary>
        /// <remarks>依据合同计划书，结合项目和选择医院的情况确定。如签在医院合中，则预算在上述第二部分中。</remarks>
        public double ShiJiHaoCaiFei_Price { get; set; }

        /// <summary>
        /// 试剂耗材费  频次（中心或例数）
        /// </summary>
        /// <remark>依据合同计划书，结合项目和选择医院的情况确定。如签在医院合中，则预算在上述第二部分中。</remark>
        public int ShiJiHaoCaiFei_Count { get; set; }

        /// <summary>
        /// 中心实验室
        /// </summary>
        /// <remarks>中心阅片按照800-1200/例。</remarks>
        public double ZhongXinShiYanShiFei_Price { get; set; }


        /// <summary>
        /// 数据、统计费 单价
        /// </summary>
        /// <remarks>依据合同计划书确定。</remarks>
        public double ShuJuTongJiFei_Price { get; set; }

        /// <summary>
        /// 系统使用费  单价
        /// </summary>
        /// <remarks>按需选择，EDC、IWRS、ePRO等相关系统租用费。</remarks>
        public double XiTongShiYongFei_Price { get; set; }

        /// <summary>
        /// 印刷费  单价
        /// </summary>
        /// <remarks>依据中心数确定，通常每中心3000元。</remarks>
        public double YinShuaFei_Price { get; set; }

        /// <summary>
        /// 其他采购费 单价
        /// </summary>
        /// <remarks>依据合同计划书确定（根据试验需要采购的冰箱、温度计、离心机等设备）。</remarks>
        public double QiTaCaiGouFei_Price { get; set; }

        /// <summary>
        ///  其他采购费 频次（中心或例数）
        /// </summary>
        /// <remarks>依据合同计划书确定（根据试验需要采购的冰箱、温度计、离心机等设备）。</remarks>
        public int QiTaCaiGouFei_Count { get; set; }

        /// <summary>
        /// 受试者招募费  单价
        /// </summary>
        /// <remarks>依据合同计划书和项目实际情况进行预算，包括委外招募和研究过程协调促进等费用。</remarks>
        public double ShouShiZheZhaoMuFei_Price { get; set; }

        /// <summary>
        /// 受试者招募费  频次（中心或例数）
        /// </summary>
        /// <remarks>依据合同计划书和项目实际情况进行预算，包括委外招募和研究过程协调促进等费用。</remarks>
        public int ShouShiZheZhaoMuFei_Count { get; set; }

        /// <summary>
        /// 稽查费 省内 单价
        /// </summary>
        /// <remarks>根据合同计划书约定范围进行计算，省内稽查按15000元/次。</remarks>
        public double ShengNeiJiChaFei_Price { get; set; }

        /// <summary>
        /// 稽查费 省内 频次（中心或例数）
        /// </summary>
        /// <remarks>根据合同计划书约定范围进行计算，省内稽查按15000元/次。</remarks>
        public int ShengNeiJiChaFei_Count { get; set; }


        /// <summary>
        /// 稽查费 省外 单价
        /// </summary>
        /// <remarks>根据合同计划书约定范围进行计算，出差省外稽查按20000元/次。</remarks>
        public double ShengWaiJiChaFei_Price { get; set; }

        /// <summary>
        /// 稽查费 省外 频次（中心或例数）
        /// </summary>
        /// <remarks>根据合同计划书约定范围进行计算，出差省外稽查按20000元/次。</remarks>
        public int ShengWaiJiChaFei_Count { get; set; }


        /// <summary>
        /// SMO费用  单价
        /// </summary>
        /// <remarks>依据合同计划书确定。</remarks>
        public double SMOFei_Price { get; set; }

        /// <summary>
        /// 委外监查服务  单价
        /// </summary>
        /// <remarks>按附表2进行测算。将预计委托外部公司的病例数进行拆分计算。</remarks>
        public double WeiWaiJianChaFuWuFei_Price { get; set; }

        /// <summary>
        /// 委外监查服务  频次（中心或例数）
        /// </summary>
        public int WeiWaiJianChaFuWuFei_Count { get; set; }

        /// <summary>
        /// 遗传办填报 单价
        /// </summary>
        /// <remarks>按需选择。5家中心以内3W，中心增加可视情况增加</remarks>
        public double YiChuanBanTianBaoFei_Price { get; set; }

        /// <summary>
        /// 其他费用  单价
        /// </summary>
        /// <remarks>指无法确定的费用，按中心预留，小包通常每中心2000元。大包根据项目难度确定。</remarks>
        public double QiTaFei_Price { get; set; }

        /// <summary>
        /// 管理人员  单价       
        /// </summary>
        ///<remarks>简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。</remarks>
        public double GuanLiRenYuanFei_Price { get; set; }

        /// <summary>
        /// 项目经理（PM） 单价
        /// </summary>
        /// <remarks>依据项目总时限和项目经理平均承担项目数量，基准为8000元-15000元/月*项目总时长（月）。</remarks>
        public double PMFei_Price { get; set; }

        /// <summary>
        /// 项目组长（PL） 单价
        /// </summary>
        /// <remarks>依据项目总时限和项目备配的情况，基准为6000元-10000元/月*项目总时长（月）。</remarks>
        public double PLFei_Price { get; set; }

        /// <summary>
        /// 项目助理（CTA） 单价
        /// </summary>
        /// <remarks>依据项目总时限和项目备配的情况，基准为3000元-5000元/月*项目总时长（月）。</remarks>
        public double CTAFei_Price { get; set; }


        /// <summary>
        /// 协同监查  单价
        /// </summary>
        /// <remarks>包括PM、LM、QC三种角色的协同访视，通常每中心按2次计算。</remarks>
        public double XieTongJianChaFei_Price { get; set; }

        /// <summary>
        /// 医学支持（撰写）  单价        
        /// </summary>
        /// <remarks>简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。</remarks>
        public double YiXueZhiChiZhuanXieFei_Price { get; set; }

        /// <summary>
        ///  医学支持（监查）  单价
        /// </summary>
        /// <remarks>每次按10000元计算。</remarks>
        public double YiXueZhiChiJianChaFei_Price { get; set; }

        /// <summary>
        /// 质控支持  单价            --根据项目难度选择
        /// </summary>
        ///<remarks>简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。</remarks>
        public double ZhiKongZhiChiFei_Price { get; set; }

        /// <summary>
        /// PV支持  单价
        /// </summary>
        /// <remarks>需要根据项目合同和计划书确定。</remarks>
        public double PVZhiChiFei_Price { get; set; }


        /// <summary>
        /// PV支持  频次（中心或例数）
        /// </summary>
        /// <remarks>需要根据项目合同和计划书确定。</remarks>
        public int PVZhiChiFei_Count { get; set; }

        /// <summary>
        /// 项目奖励   单价
        /// </summary>
        /// <remarks>合同系数：＜200W，0.4；200-399W，0.5；400-599W，0.6；600-899W，0.7；900-1199W，0.8；1200-1500W，0.9。</remarks>
        public double XiangMuJiangLiFei_Price { get; set; }

        /// <summary>
        /// PM出差单价
        /// </summary>
        public double PM_ChuChai_Price { get; set; }

        /// <summary>
        /// PM出差 频次/数  （不可修改）
        /// </summary>
        public int PM_ChuChai_Count { get; set; }

        /// <summary>
        /// PM不出差单价
        /// </summary>
        public double PM_BuChuChai_Price { get; set; }

        /// <summary>
        /// PM不出差 频次/数  （不可修改）
        /// </summary>
        public double PM_BuChuChai_Count { get; set; }

        /// <summary>
        /// CRA出差单价
        /// </summary>
        public double CRA_ChuChai_Price { get; set; }

        /// <summary>
        /// CRA出差  频次/数  （不可修改）
        /// </summary>
        public double CRA_ChuChai_Count { get; set; }

        /// <summary>
        /// CRA不出差单价
        /// </summary>
        public double CRA_BuChuChai_Price { get; set; }

        /// <summary>
        /// CRA不出差单价  频次/数  （不可修改）
        /// </summary>
        public int CRA_BuChuChai_Count { get; set; }

        /// <summary>
        /// 管理出差单价
        /// </summary>
        public double Admin_ChuChai_Price { get; set; }

        /// <summary>
        /// 管理出差单价  频次/数  （不可修改）
        /// </summary>
        public int Admin_ChuChai_Count { get; set; }

        /// <summary>
        /// 管理人员不出差单价
        /// </summary>
        public double Admin_BuChuChai_Price { get; set; }

        /// <summary>
        /// 管理人员不出差单价  频次/数  （不可修改）
        /// </summary>
        public int Admin_BuChuChai_Count { get; set; }

        /// <summary>
        /// 协议（筛选期）
        /// </summary>
        public double XieYi_ShaiXuanQi { get; set; }

        /// <summary>
        /// 协议（入组）
        /// </summary>
        public double XieYi_RuZu { get; set; }

        /// <summary>
        /// 非协议
        /// </summary>
        public double FeiXieYi { get; set; }

        /// <summary>
        /// 筛选器检查费
        /// </summary>
        public double ShaiXuanQiJianChaFei { get; set; }

        /// <summary>
        /// 入组后检查费
        /// </summary>
        public double RuZuHouJianChaFei { get; set; }

        /// <summary>
        /// V1
        /// </summary>
        public double V1 { get; set; }
        /// <summary>
        /// V2
        /// </summary>
        public double V2 { get; set; }
        /// <summary>
        /// V3
        /// </summary>
        public double V3 { get; set; }
        /// <summary>
        /// V4
        /// </summary>
        public double V4 { get; set; }
        /// <summary>
        /// V5
        /// </summary>
        public double V5 { get; set; }

        /// <summary>
        /// 随访单价
        /// </summary>
        public double AE { get; set; }

        /// <summary>
        /// 单例检查费
        /// </summary>
        public string DanLiJianChaFei { get; set; }

        /// <summary>
        /// 单例受试者补助
        /// </summary>
        public double DanLiShouShiZheBuZhu { get; set; }
    }

    /// <summary>
    /// 会议费输入参数
    /// </summary>
    public class HuiYiFeiInput
    {
        /// <summary>
        /// 会务费
        /// </summary>
        public double HuiWuFei { get; set; }

        /// <summary>
        /// 交通费
        /// </summary>
        public double JiaoTongFei { get; set; }


        /// <summary>
        /// 住宿费
        /// </summary>
        public double ZhuSuFei { get; set; }
        /// <summary>
        /// 会议使用费
        /// </summary>

        public double HuiYiShiFei { get; set; }

        /// <summary>
        /// 其他费
        /// </summary>
        public double QiTaFei { get; set; }
    }

    public class UpdateBaoJiaDanInput : CreateBaoJiaDanInput
    {

    }

    /// <summary>
    /// 难度系数枚举
    /// </summary>
    public enum ENanDuXiShu
    {
        /// <summary>
        /// 简单级1.0
        /// </summary>
        [Display(Name = "简单级1.0")]
        简单级 = 0,

        /// <summary>
        /// 普通级1.14
        /// </summary>
        [Display(Name = "普通级1.14")]
        普通级 = 1,

        /// <summary>
        /// 稍难级1.3
        /// </summary>
        [Display(Name = "稍难级1.3")]
        稍难级 = 2,

        /// <summary>
        ///困难级1.54
        /// </summary>
        [Display(Name = "困难级1.54")]
        困难级 = 3,

        /// <summary>
        /// 特别级2.0
        /// </summary>
        [Display(Name = "特别级2.0")]
        特别级 = 4
    }


}
