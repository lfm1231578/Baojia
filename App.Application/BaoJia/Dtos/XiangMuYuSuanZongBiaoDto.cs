using SqlSugar.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Dtos
{
    /// <summary>
    /// 表1，项目预算总表（含说明）
    /// </summary>
    public class XiangMuYuSuanZongBiaoDto
    {

        public XiangMuYuSuanZongBiaoDto(CreateBaoJiaDanInput input)
        {
            ZongBingLiShu = input.LiShu;
            ZongShiXian = input.ShiXian;
            QueDingZhongXinShu = input.ZhongXinShu;
            CompanyName = input.CompanyName;
            ProjectName = input.ProjectName;
            ShiYingZheng = input.ShiYingZheng;
            SuiFang = input.SuiFang;
            NamDu = input.NanDu;

            ChaiLvFeiChengBenGuiZeBiao = new ChaiLvFeiChengBenGuiZeBiao(input);
            DanLiGuanChaFei = new DanLiGuanChaFei(input);
            DanLiJIanChaFei = new DanLiJIanChaFei(input);
            DanLiShouShiZheBuZhu = new DanLiShouShiZheBuZhu(input);
            HuiYiFeiZongE = new HuiYiFeiZongE(JiHuaShuaiXuanZhongXinShu_D, input);
            YiYuanHeTongFeiZongE = new YiYuanHeTongFeiZongE(input);
            QiTaZaFeiZongE = new QiTaZaFeiZongE(input, ChaiLvFeiChengBenGuiZeBiao);
            RenGongFeiYongZongE = new RenGongFeiYongZongE(input);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zongBingLiShu">总病例数（例）</param>
        /// <param name="zongShiXian">总时限（月）</param>
        /// <param name="queDingZhongXinShu">确定中心数</param>
        /// <param name="nandu">难度系数</param>
        public XiangMuYuSuanZongBiaoDto(int zongBingLiShu, int zongShiXian, int queDingZhongXinShu, ENanDuXiShu nandu)
        {
            ZongBingLiShu = zongBingLiShu;
            ZongShiXian = zongShiXian;
            QueDingZhongXinShu = queDingZhongXinShu;
            NamDu = nandu;

            ChaiLvFeiChengBenGuiZeBiao = new ChaiLvFeiChengBenGuiZeBiao(zongShiXian, queDingZhongXinShu);
            DanLiGuanChaFei = new DanLiGuanChaFei();
            DanLiJIanChaFei = new DanLiJIanChaFei();
            DanLiShouShiZheBuZhu = new DanLiShouShiZheBuZhu();
            HuiYiFeiZongE = new HuiYiFeiZongE(queDingZhongXinShu, JiHuaShuaiXuanZhongXinShu_D);
            YiYuanHeTongFeiZongE = new YiYuanHeTongFeiZongE(zongBingLiShu, zongShiXian, queDingZhongXinShu);
            QiTaZaFeiZongE = new QiTaZaFeiZongE(zongBingLiShu, queDingZhongXinShu);
            RenGongFeiYongZongE = new RenGongFeiYongZongE(zongBingLiShu, zongShiXian, queDingZhongXinShu, nandu);
        }


        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; } = String.Empty;

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = String.Empty;

        /// <summary>
        /// 适应症
        /// </summary>
        public string ShiYingZheng { get; set; } = String.Empty;

        /// <summary>
        /// 随访次数
        /// </summary>
        public int SuiFang { get; set; }

        /// <summary>
        /// 难度
        /// </summary>
        public ENanDuXiShu NamDu { get; set; }

        /// <summary>
        /// 预算费用总金额（RMB）
        /// </summary>
        public double TotalAmount
        {
            get
            {
                return HuiYiFeiZongE.TotalAmount + YiYuanHeTongFeiZongE.TotalAmount + QiTaZaFeiZongE.TotalAmount + RenGongFeiYongZongE.TotalAmount;
            }
        }

        /// <summary>
        /// 确定中心数
        /// </summary>
        public int QueDingZhongXinShu { get; set; }

        /// <summary>
        /// 计划筛选中心数
        /// </summary>
        public int JiHuaShuaiXuanZhongXinShu
        {
            get
            {

                return Math.Ceiling(QueDingZhongXinShu * 1.5).ObjToInt();
            }
        }

        /// <summary>
        /// 计划筛选中心数（高精度）
        /// </summary>
        protected double JiHuaShuaiXuanZhongXinShu_D
        {
            get
            {
                return (QueDingZhongXinShu * 1.5);
            }
        }


        /// <summary>
        /// 总病例数
        /// </summary>
        public int ZongBingLiShu { get; set; }

        /// <summary>
        /// 总时限（月）
        /// </summary>
        public int ZongShiXian { get; set; }

        /// <summary>
        /// 一、会议费用总额(RMB)
        /// </summary>
        public HuiYiFeiZongE HuiYiFeiZongE { get; set; }

        /// <summary>
        /// 二、医院合同费用总额(RMB)
        /// </summary>
        public YiYuanHeTongFeiZongE YiYuanHeTongFeiZongE { get; set; }

        /// <summary>
        /// 三、其它杂费总额(RMB)
        /// </summary>
        public QiTaZaFeiZongE QiTaZaFeiZongE { get; set; }

        /// <summary>
        /// 四、人工费用总额 （RMB)
        /// </summary>
        public RenGongFeiYongZongE RenGongFeiYongZongE { get; set; }

        /// <summary>
        /// 单例观察费
        /// </summary>
        public DanLiGuanChaFei DanLiGuanChaFei { get; set; }

        /// <summary>
        /// 单例检查费
        /// </summary>
        public DanLiJIanChaFei DanLiJIanChaFei { get; set; }

        /// <summary>
        /// 单例受试者补助
        /// </summary>
        public DanLiShouShiZheBuZhu DanLiShouShiZheBuZhu { get; set; }

        /// <summary>
        /// 附：差旅费成本预算规则
        /// </summary>
        public ChaiLvFeiChengBenGuiZeBiao ChaiLvFeiChengBenGuiZeBiao { get; set; }



    }

    /// <summary>
    /// 一、会议费用总额(RMB)
    /// </summary>
    public class HuiYiFeiZongE
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jiHuaShaiXuanZhongXinShu"></param>
        /// <param name="input"></param>
        public HuiYiFeiZongE(double jiHuaShaiXuanZhongXinShu, CreateBaoJiaDanInput input)
        {
            //协调会费用
            XieTiaoHui = new HuiYiFeiBase
            {

                //HuiWuFei = 3 * jiHuaShaiXuanZhongXinShu * input.XieTiaoHui.HuiWuFei,
                //JiaoTongFei = 3 * jiHuaShaiXuanZhongXinShu * input.XieTiaoHui.JiaoTongFei,
                //ZhuSuFei = 0,
                //HuiYiShiFei = jiHuaShaiXuanZhongXinShu * input.XieTiaoHui.HuiYiShiFei,
                // QiTaFei = input.XieTiaoHui.QiTaFei * 1000
                HuiWuFei = input.XieTiaoHui.HuiWuFei,
                JiaoTongFei = input.XieTiaoHui.JiaoTongFei,
                ZhuSuFei = input.XieTiaoHui.ZhuSuFei,
                HuiYiShiFei = input.XieTiaoHui.HuiYiShiFei,
                QiTaFei = input.XieTiaoHui.QiTaFei
            };

            ZongJieHui = new HuiYiFeiBase
            {

                HuiWuFei = input.ZongJieHui.HuiWuFei,
                JiaoTongFei = input.ZongJieHui.JiaoTongFei,
                ZhuSuFei = input.ZongJieHui.ZhuSuFei,
                HuiYiShiFei = input.ZongJieHui.HuiYiShiFei,
                QiTaFei = input.ZongJieHui.QiTaFei
            };

            ZhongQiHui = new HuiYiFeiBase
            {
                HuiWuFei = input.ZhongQiHui.HuiWuFei,
                JiaoTongFei = input.ZhongQiHui.JiaoTongFei,
                ZhuSuFei = input.ZhongQiHui.ZhuSuFei,
                HuiYiShiFei = input.ZhongQiHui.HuiYiShiFei,
                QiTaFei = input.ZhongQiHui.QiTaFei
            };

            ///盲态审核会费用
            MangTaiShenHeHui = new HuiYiFeiBase
            {
                //HuiWuFei = 3 * input.MangTaiShenHeHui.HuiWuFei,
                //JiaoTongFei = 2 * input.MangTaiShenHeHui.JiaoTongFei,
                //ZhuSuFei = 2 * input.MangTaiShenHeHui.ZhuSuFei,
                //HuiYiShiFei = 1 * input.MangTaiShenHeHui.HuiYiShiFei,
                //QiTaFei = 1 * input.MangTaiShenHeHui.QiTaFei
                HuiWuFei = input.MangTaiShenHeHui.HuiWuFei,
                JiaoTongFei = input.MangTaiShenHeHui.JiaoTongFei,
                ZhuSuFei = input.MangTaiShenHeHui.ZhuSuFei,
                HuiYiShiFei = input.MangTaiShenHeHui.HuiYiShiFei,
                QiTaFei = input.MangTaiShenHeHui.QiTaFei
            };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queDingZhongXinShu">确定中心数</param>
        /// <param name="jiHuaShaiXuanZhongXinShu">计划筛选中心数</param>

        public HuiYiFeiZongE(int queDingZhongXinShu, double jiHuaShaiXuanZhongXinShu)
        {

            //输入参数

            //协调会费用
            XieTiaoHui = new HuiYiFeiBase
            {

                HuiWuFei = 3 * jiHuaShaiXuanZhongXinShu * 3000,
                JiaoTongFei = 3 * jiHuaShaiXuanZhongXinShu * 1500,
                ZhuSuFei = 0,
                HuiYiShiFei = jiHuaShaiXuanZhongXinShu * 3000,
                QiTaFei = queDingZhongXinShu * 1000
            };

            ZongJieHui = new HuiYiFeiBase
            {

                HuiWuFei = 0,
                JiaoTongFei = 0,
                ZhuSuFei = 0,
                HuiYiShiFei = 0,
                QiTaFei = 0
            };

            ZhongQiHui = new HuiYiFeiBase
            {
                HuiWuFei = 0,
                JiaoTongFei = 0,
                ZhuSuFei = 0,
                HuiYiShiFei = 0,
                QiTaFei = 0
            };


            //盲态审核会费用
            MangTaiShenHeHui = new HuiYiFeiBase
            {
                HuiWuFei = 3 * 3000,
                JiaoTongFei = 2 * 1500,
                ZhuSuFei = 2 * 800,
                HuiYiShiFei = 1 * 3000,
                QiTaFei = 1 * 1000
                //HuiWuFei = (3d / 5d * queDingZhongXinShu) * 3000,
                //JiaoTongFei = (2d / 5d * queDingZhongXinShu) * 1500,
                //ZhuSuFei = (2d / 5d * queDingZhongXinShu) * 800,
                //HuiYiShiFei = (1d / 5d * queDingZhongXinShu) * 3000,
                //QiTaFei = (1d / 5d * queDingZhongXinShu) * 1000

            };
        }


        /// <summary>
        /// 协调会费用
        /// </summary>
        public HuiYiFeiBase XieTiaoHui { get; set; }

        /// <summary>
        ///总结会费用
        /// </summary>
        public HuiYiFeiBase ZongJieHui { get; set; }

        /// <summary>
        /// 中期会（或论证会）
        /// </summary>
        public HuiYiFeiBase ZhongQiHui { get; set; }

        /// <summary>
        /// 盲态审核会
        /// </summary>
        public HuiYiFeiBase MangTaiShenHeHui { get; set; }

        /// <summary>
        /// 会议费用总额
        /// </summary>
        public double TotalAmount
        {
            get
            {
                double total = 0;
                if (XieTiaoHui != null)
                {
                    total += XieTiaoHui.HeJi;
                }
                if (ZongJieHui != null)
                {
                    total += ZongJieHui.HeJi;
                }
                if (ZhongQiHui != null)
                {
                    total += ZhongQiHui.HeJi;
                }
                if (MangTaiShenHeHui != null)
                {
                    total += MangTaiShenHeHui.HeJi;
                }
                return total;
            }
        }

    }


    /// <summary>
    /// 二、医院合同费用总额(RMB)
    /// </summary>
    public class YiYuanHeTongFeiZongE
    {
        private int _zongBingLiShu;


        public YiYuanHeTongFeiZongE(CreateBaoJiaDanInput input)
        {
            _zongBingLiShu = input.LiShu;

            ZuZhangFei = new FeiYongBase
            {
                Count = input.ZuZhangFei_Count,//频数（中心或者例数）//应该也是输入参数
                Price = input.ZuZhangFei_Price,//单价
            };

            LunLiFei = new FeiYongBase
            {
                Count = 2 * input.ZhongXinShu,//频数（中心或者例数）
                Price = input.LunLiFei_Price,//单价  //应该也是输入参数
            };
            HeGeBingLiFei = new FeiYongBase
            {
                Count = input.LiShu,//频数（中心或者例数）
                Price = input.FeiXieYi + input.XieYi_RuZu + input.XieYi_ShaiXuanQi + input.ShaiXuanQiJianChaFei + input.RuZuHouJianChaFei,        //2500 + 150,//单价  //单例观察费+单例检查费
            };

            ShaiXuanBingLiFei = new FeiYongBase
            {
                Count = (int)Math.Round(input.LiShu * 0.1, MidpointRounding.AwayFromZero),//频数（中心或者例数）;四舍五入
                Price = input.XieYi_ShaiXuanQi + input.ShaiXuanQiJianChaFei     //500 + 50,//单价  //协议筛选期+筛选期检查费
            };

            QiTaFei = new FeiYongBase
            {
                Count = input.LiShu,//频数（中心或者例数）;
                Price = input.V1 + input.V2 + input.V3 + input.V4 + input.V5         //500,//单价  //单例受试者补助
            };

            JiGouGuanLiFei = new FeiYongBase
            {
                Count = 1,//频数（中心或者例数）;//应该也是输入参数
                Price = (ZuZhangFei.HeJi + LunLiFei.HeJi + HeGeBingLiFei.HeJi + ShaiXuanBingLiFei.HeJi + QiTaFei.HeJi) * 0.3d
          ,//单价  //单例受试者补助
            };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="zongBingLiShu">总病例数（例）</param>
        /// <param name="zongShiXian">总时限（月）</param>
        /// <param name="queDingZhongXinShu">确定中心数</param>
        public YiYuanHeTongFeiZongE(int zongBingLiShu, int zongShiXian, int queDingZhongXinShu)
        {
            _zongBingLiShu = zongBingLiShu;

            ZuZhangFei = new FeiYongBase
            {
                Count = 1,//频数（中心或者例数）//应该也是输入参数
                Price = 100000,//单价
            };

            LunLiFei = new FeiYongBase
            {
                Count = 2 * queDingZhongXinShu,//频数（中心或者例数）
                Price = 10000,//单价  //应该也是输入参数
            };
            HeGeBingLiFei = new FeiYongBase
            {
                Count = zongBingLiShu,//频数（中心或者例数）
                Price = 2500 + 150,//单价  //单例观察费+单例检查费
            };

            ShaiXuanBingLiFei = new FeiYongBase
            {
                Count = (int)Math.Round(zongBingLiShu * 0.1, MidpointRounding.AwayFromZero),//频数（中心或者例数）;四舍五入
                Price = 500 + 50,//单价  //协议筛选期+筛选期检查费
            };

            QiTaFei = new FeiYongBase
            {
                Count = zongBingLiShu,//频数（中心或者例数）;
                Price = 500,//单价  //单例受试者补助
            };

            JiGouGuanLiFei = new FeiYongBase
            {
                Count = 1,//频数（中心或者例数）;//应该也是输入参数
                Price = (ZuZhangFei.HeJi + LunLiFei.HeJi + HeGeBingLiFei.HeJi + ShaiXuanBingLiFei.HeJi + QiTaFei.HeJi) * 0.3d
          ,//单价  //单例受试者补助
            };
        }


        /// <summary>
        /// 医院合同费用总额
        /// </summary>
        public double TotalAmount
        {
            get
            {
                double total = 0;
                if (ZuZhangFei != null)
                {
                    total += ZuZhangFei.HeJi;
                }
                if (JiGouGuanLiFei != null)
                {
                    total += JiGouGuanLiFei.HeJi;
                }
                if (LunLiFei != null)
                {
                    total += LunLiFei.HeJi;
                }
                if (HeGeBingLiFei != null)
                {
                    total += HeGeBingLiFei.HeJi;
                }
                if (ShaiXuanBingLiFei != null)
                {
                    total += ShaiXuanBingLiFei.HeJi;
                }
                if (QiTaFei != null)
                {
                    total += QiTaFei.HeJi;
                }
                return total;
            }
        }

        /// <summary>
        /// 医院单例价格
        /// </summary>
        public double AvgPrice
        {
            get
            {
                return TotalAmount / _zongBingLiShu;
            }
        }

        /// <summary>
        /// 组长费
        /// </summary>
        /// <remarks>依据不同项目类型（或拟选机构要求）而定，结合商务计划书。</remarks>
        public FeiYongBase ZuZhangFei { get; set; }

        /// <summary>
        /// 机构管理费
        /// </summary>
        /// <remarks>依据不同项目类型（或拟选机构要求）而定，结合商务计划书与确定中心数。含机构管理、药物管理、CRC管理、质控管理、文档管理等费用。</remarks>
        public FeiYongBase JiGouGuanLiFei { get; set; }


        /// <summary>
        /// 伦理费
        /// </summary>
        /// <remarks>按10000元/次标准，结合中心数*2倍=总次数（考虑重审及结题审查）。</remarks>
        public FeiYongBase LunLiFei { get; set; }


        /// <summary>
        /// 合格病例费用
        /// </summary>
        /// <remarks>观察费和检查费(指合同签署支付医院，包括研究者劳务费、检查费、床位费、餐费等)。</remarks>
        public FeiYongBase HeGeBingLiFei { get; set; }


        /// <summary>
        /// 筛选病例费
        /// </summary>
        /// <remarks>依据不同项目的筛败工作量大小和筛选失败率来确定。预计筛败率为10%。</remarks>
        public FeiYongBase ShaiXuanBingLiFei { get; set; }


        /// <summary>
        /// 其他费用
        /// </summary>
        /// <remarks>指受试者补偿、AE随访费等费用。</remarks>
        public FeiYongBase QiTaFei { get; set; }
    }

    /// <summary>
    /// 三、其它杂费总额(RMB)
    /// </summary>
    public class QiTaZaFeiZongE
    {
        /// <summary>
        /// 差旅费计算
        /// </summary>
        /// <param name="queDingZhongXinShu"></param>
        /// <param name="PM_ChuChai_Price"></param>
        /// <param name="PM_BuChuChai_Price"></param>
        /// <param name="CRA_ChuChai_Price"></param>
        /// <param name="CRA_BuChuChai_Price"></param>
        /// <param name="Admin_ChuChai_Price"></param>
        /// <param name="Admin_BuChuChai_Price"></param>
        /// <returns></returns>
        public double chailvfei(int queDingZhongXinShu, double PM_ChuChai_Price, double PM_BuChuChai_Price, double CRA_ChuChai_Price, double CRA_BuChuChai_Price, double Admin_ChuChai_Price, double Admin_BuChuChai_Price)
        {

            //PM(出差)
            double pmChuChai = 0.4 * queDingZhongXinShu * 3 * PM_ChuChai_Price;
            //PM（不出差）
            double pmBuChuChai = 0.6 * queDingZhongXinShu * 3 * PM_BuChuChai_Price;
            //CRA（出差）
            double craChuChai = 0.4 * queDingZhongXinShu * 12 * CRA_ChuChai_Price;
            //CRA(不出差)
            double craBuChuChai = 0.6 * queDingZhongXinShu * 12 * CRA_BuChuChai_Price;
            //管理人员（出差）
            double guanliChuChai = 0.4 * queDingZhongXinShu * 2 * Admin_ChuChai_Price;
            //管理人员（不出差）
            double guanliBuChuChai = 0.6 * queDingZhongXinShu * 2 * Admin_BuChuChai_Price;

            return pmChuChai + pmBuChuChai + craChuChai + craBuChuChai + guanliChuChai + guanliBuChuChai;
        }

        public QiTaZaFeiZongE(CreateBaoJiaDanInput input, ChaiLvFeiChengBenGuiZeBiao chaiLvFeiCheng)
        {
            QiDongHuiFei = new FeiYongBase
            {
                Count = input.ZhongXinShu,
                Price = input.QiDongHuiFei_Price,//可输入参数,依据中心数确定，通常每中心2000元。
            };
            YunShuFei = new FeiYongBase
            {
                Count = input.ZhongXinShu,
                Price = input.YunShuFei_Price,//可输入参数,依据中心数确定，通常每中心2000元。需根据具体项目要求来预算。
            };

            JianChaChaiLvFei = new FeiYongBase
            {
                Count = 1,
                Price = chaiLvFeiCheng.PM_ChuChai_Amount
                + chaiLvFeiCheng.PM_BuChuChai_Amount
                + chaiLvFeiCheng.CRA_ChuChai_Amount
                + chaiLvFeiCheng.CRA_BuChuChai_Amount
                + chaiLvFeiCheng.Admin_ChuChai_Amount
                + chaiLvFeiCheng.Admin_BuChuChai_Amount
                //chailvfei(input.ZhongXinShu
                //, input.PM_ChuChai_Price
                //, input.PM_BuChuChai_Price
                //, input.CRA_ChuChai_Price
                //, input.CRA_BuChuChai_Price
                //, input.Admin_ChuChai_Price
                //, input.Admin_BuChuChai_Price
                //),//根据差旅费成本预算规则   
            };

            ShiJiHaoCaiFei = new FeiYongBase
            {
                Count = input.ShiJiHaoCaiFei_Count,//输入参数
                Price = input.ShiJiHaoCaiFei_Price,
            };

            ZhongXinShiYanShiFei = new FeiYongBase
            {
                Count = input.LiShu,
                Price = input.ZhongXinShiYanShiFei_Price,//输入参数
            };

            ShuJuTongJiFei = new FeiYongBase
            {
                Count = 1,
                Price = input.ShuJuTongJiFei_Price,//输入参数
            };
            XiTongShiYongFei = new FeiYongBase
            {
                Count = 1,
                Price = input.XiTongShiYongFei_Price,//输入参数
            };
            YinShuaFei = new FeiYongBase
            {
                Count = input.ZhongXinShu,
                Price = input.YinShuaFei_Price,//输入参数
            };
            QiTaCaiGouFei = new FeiYongBase
            {
                Count = input.QiTaCaiGouFei_Count,//输入参数
                Price = input.QiTaCaiGouFei_Price,//输入参数
            };

            ShouShiZheZhaoMuFei = new FeiYongBase
            {
                Count = input.ShouShiZheZhaoMuFei_Count,//输入参数
                Price = input.ShouShiZheZhaoMuFei_Price,//输入参数
            };

            ShengNeiJiChaFei = new FeiYongBase
            {
                Count = input.ShengNeiJiChaFei_Count,//输入参数
                Price = input.ShengNeiJiChaFei_Price,//输入参数
            };

            ShengWaiJiChaFei = new FeiYongBase
            {
                Count = input.ShengWaiJiChaFei_Count,//输入参数
                Price = input.ShengWaiJiChaFei_Price,//输入参数
            };

            SMOFei = new FeiYongBase
            {
                Count = input.LiShu,
                Price = input.SMOFei_Price,//输入参数
            };

            WeiWaiJianChaFuWuFei = new FeiYongBase
            {
                Count = input.WeiWaiJianChaFuWuFei_Count,//输入参数
                Price = input.WeiWaiJianChaFuWuFei_Price,//输入参数
            };
            YiChuanBanTianBaoFei = new FeiYongBase
            {

                Count = 1,
                Price = input.YiChuanBanTianBaoFei_Price,//输入参数

            };
            QiTaFei = new FeiYongBase
            {
                Count = input.ZhongXinShu,
                Price = input.QiTaFei_Price,
            };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zongBingLiShu">总病例数</param>
        /// <param name="queDingZhongXinShu">确定中心数</param>
        public QiTaZaFeiZongE(int zongBingLiShu, int queDingZhongXinShu)
        {
            QiDongHuiFei = new FeiYongBase
            {
                Count = queDingZhongXinShu,
                Price = 5000,//可输入参数,依据中心数确定，通常每中心2000元。
            };
            YunShuFei = new FeiYongBase
            {
                Count = queDingZhongXinShu,
                Price = 3000,//可输入参数,依据中心数确定，通常每中心2000元。需根据具体项目要求来预算。
            };

            JianChaChaiLvFei = new FeiYongBase
            {
                Count = 1,
                Price = chailvfei(queDingZhongXinShu, 3000, 500, 3000, 800, 3000, 500),//根据差旅费成本预算规则
            };

            ShiJiHaoCaiFei = new FeiYongBase
            {
                Count = 0,//输入参数
                Price = 0,
            };

            ZhongXinShiYanShiFei = new FeiYongBase
            {
                Count = zongBingLiShu,
                Price = 0,//输入参数
            };

            ShuJuTongJiFei = new FeiYongBase
            {
                Count = 1,//输入参数
                Price = 120000,//输入参数
            };
            XiTongShiYongFei = new FeiYongBase
            {
                Count = 1,
                Price = 30000,//输入参数
            };
            YinShuaFei = new FeiYongBase
            {
                Count = queDingZhongXinShu,
                Price = 3000,//输入参数
            };
            QiTaCaiGouFei = new FeiYongBase
            {
                Count = 0,//输入参数
                Price = 0,//输入参数
            };

            ShouShiZheZhaoMuFei = new FeiYongBase
            {
                Count = 0,//输入参数
                Price = 0,//输入参数
            };

            ShengNeiJiChaFei = new FeiYongBase
            {
                Count = 0,//输入参数
                Price = 0,//输入参数
            };

            ShengWaiJiChaFei = new FeiYongBase
            {
                Count = 0,//输入参数   //文档中给出的是0
                Price = 20000,//输入参数
            };

            SMOFei = new FeiYongBase
            {
                Count = zongBingLiShu,
                Price = 5000,//输入参数
            };

            WeiWaiJianChaFuWuFei = new FeiYongBase
            {
                Count = 0,//输入参数
                Price = 0,//输入参数
            };
            YiChuanBanTianBaoFei = new FeiYongBase
            {

                Count = 1,
                Price = 0,//输入参数

            };
            QiTaFei = new FeiYongBase
            {
                Count = queDingZhongXinShu,
                Price = 2000,
            };

        }

        /// <summary>
        /// 合计
        /// </summary>
        public double TotalAmount
        {
            get
            {

                double total = 0;
                if (QiDongHuiFei != null)
                {
                    total += QiDongHuiFei.HeJi;
                }
                if (YunShuFei != null)
                {
                    total += YunShuFei.HeJi;
                }
                if (JianChaChaiLvFei != null)
                {
                    total += JianChaChaiLvFei.HeJi;
                }
                if (ShiJiHaoCaiFei != null)
                {
                    total += ShiJiHaoCaiFei.HeJi;
                }
                if (ZhongXinShiYanShiFei != null)
                {
                    total += ZhongXinShiYanShiFei.HeJi;
                }
                if (ShuJuTongJiFei != null)
                {
                    total += ShuJuTongJiFei.HeJi;
                }
                if (XiTongShiYongFei != null)
                {
                    total += XiTongShiYongFei.HeJi;
                }
                if (YinShuaFei != null)
                {
                    total += YinShuaFei.HeJi;
                }
                if (QiTaCaiGouFei != null)
                {
                    total += QiTaCaiGouFei.HeJi;
                }
                if (ShouShiZheZhaoMuFei != null)
                {
                    total += ShouShiZheZhaoMuFei.HeJi;
                }
                if (ShengNeiJiChaFei != null)
                {
                    total += ShengNeiJiChaFei.HeJi;
                }
                if (ShengWaiJiChaFei != null)
                {
                    total += ShengWaiJiChaFei.HeJi;
                }
                if (SMOFei != null)
                {
                    total += SMOFei.HeJi;
                }
                if (WeiWaiJianChaFuWuFei != null)
                {
                    total += WeiWaiJianChaFuWuFei.HeJi;
                }
                if (YiChuanBanTianBaoFei != null)
                {
                    total += YiChuanBanTianBaoFei.HeJi;
                }
                if (QiTaFei != null)
                {
                    total += QiTaFei.HeJi;
                }
                return total;
            }
        }

        /// <summary>
        /// 启动会费
        /// </summary>
        /// <remarks>依据中心数确定，通常每中心2000元。</remarks>
        public FeiYongBase QiDongHuiFei { get; set; }

        /// <summary>
        /// 运输费
        /// </summary>
        /// <remarks>依据中心数确定，通常每中心2000元。需根据具体项目要求来预算。</remarks>
        public FeiYongBase YunShuFei { get; set; }

        /// <summary>
        /// 监查差旅费
        /// </summary>
        /// <remarks>见下表计算方法。</remarks>
        public FeiYongBase JianChaChaiLvFei { get; set; }

        /// <summary>
        /// 试剂、耗材费
        /// </summary>
        /// <remarks>依据合同计划书，结合项目和选择医院的情况确定。如签在医院合中，则预算在上述第二部分中。</remarks>
        public FeiYongBase ShiJiHaoCaiFei { get; set; }

        /// <summary>
        ///中心实验室
        /// </summary>
        /// <remarks>中心阅片按照800-1200/例。</remarks>
        public FeiYongBase ZhongXinShiYanShiFei { get; set; }


        /// <summary>
        /// 数据、统计费
        /// </summary>
        /// <remarks>依据合同计划书确定。</remarks>
        public FeiYongBase ShuJuTongJiFei { get; set; }

        /// <summary>
        /// 系统使用费
        /// </summary>
        /// <remarks>按需选择，EDC、IWRS、ePRO等相关系统租用费。</remarks>
        public FeiYongBase XiTongShiYongFei { get; set; }


        /// <summary>
        /// 印刷费
        /// </summary>
        /// <remarks>依据中心数确定，通常每中心3000元。</remarks>
        public FeiYongBase YinShuaFei { get; set; }


        /// <summary>
        /// 其他采购
        /// </summary>
        /// <remarks>依据合同计划书确定（根据试验需要采购的冰箱、温度计、离心机等设备）。</remarks>
        public FeiYongBase QiTaCaiGouFei { get; set; }

        /// <summary>
        /// 受试者招募费
        /// </summary>
        /// <remarks>依据合同计划书和项目实际情况进行预算，包括委外招募和研究过程协调促进等费用。</remarks>
        public FeiYongBase ShouShiZheZhaoMuFei { get; set; }

        /// <summary>
        /// 省内稽查费
        /// </summary>
        /// <remarks>根据合同计划书约定范围进行计算，省内稽查按15000元/次。</remarks>
        public FeiYongBase ShengNeiJiChaFei { get; set; }

        /// <summary>
        /// 省外稽查费
        /// </summary>
        /// <remarks>根据合同计划书约定范围进行计算，出差省外稽查按20000元/次。</remarks>
        public FeiYongBase ShengWaiJiChaFei { get; set; }

        /// <summary>
        /// SMO费用
        /// </summary>
        /// <remarks>依据合同计划书确定。</remarks>
        public FeiYongBase SMOFei { get; set; }

        /// <summary>
        /// 委外检查服务
        /// </summary>
        /// <remarks>按附表2进行测算。将预计委托外部公司的病例数进行拆分计算。</remarks>
        public FeiYongBase WeiWaiJianChaFuWuFei { get; set; }

        /// <summary>
        /// 遗传办填报
        /// </summary>
        /// <remarks>按需选择。5家中心以内3W，中心增加可视情况增加</remarks>
        public FeiYongBase YiChuanBanTianBaoFei { get; set; }

        /// <summary>
        /// 其他费用
        /// </summary>
        /// <remark>指无法确定的费用，按中心预留，小包通常每中心2000元。大包根据项目难度确定。</remark>
        public FeiYongBase QiTaFei { get; set; }
    }

    /// <summary>
    /// 四、人工费用总额 （RMB)
    /// </summary>
    public class RenGongFeiYongZongE
    {
        /// <summary>
        /// 通过难度系数获取人员管理单价
        /// </summary>
        /// <param name="nandu"></param>
        /// <returns></returns>
        private double get_Price(ENanDuXiShu nandu)
        {

            switch (nandu)
            {
                case ENanDuXiShu.简单级:
                case ENanDuXiShu.普通级:
                    return 30000;
                case ENanDuXiShu.稍难级:
                case ENanDuXiShu.困难级:
                    return 50000;
                case ENanDuXiShu.特别级:
                    return 70000;
                default:
                    return 30000;

            }
        }


        /// <summary>
        /// 表2，检查服务成本预算需序号1 单价
        /// </summary>
        /// <param name="nandu">难度</param>
        /// <returns></returns>
        private double jianChaFuWuFei1_Price(ENanDuXiShu nandu)
        {

            switch (nandu)
            {
                case ENanDuXiShu.简单级:
                case ENanDuXiShu.普通级:
                    return 5000;
                case ENanDuXiShu.稍难级:
                case ENanDuXiShu.困难级:
                case ENanDuXiShu.特别级:
                    return 8000;
                default:
                    return 5000;
            }
        }
        /// <summary>
        /// 表2，检查服务成本预算需序号1  小计
        /// </summary>
        /// <param name="nandu">难度</param>
        /// <param name="zhongXinShu">中心数</param>
        /// <returns></returns>
        private double jianChaFuWuFei1_Amount(ENanDuXiShu nandu, int zhongXinShu)
        {
            return jianChaFuWuFei1_Price(nandu) * zhongXinShu;
        }

        /// <summary>
        /// 表2，检查服务成本预算需序号2 单价
        /// </summary>
        /// <param name="nandu">难度</param>
        /// <returns></returns>
        private double jianChaFuWuFei2_Price(ENanDuXiShu nandu)
        {
            switch (nandu)
            {
                case ENanDuXiShu.简单级:
                    return 1000;
                case ENanDuXiShu.普通级:
                    return 1500;
                case ENanDuXiShu.稍难级:
                    return 3000;
                case ENanDuXiShu.困难级:
                    return 4000;
                case ENanDuXiShu.特别级:
                    return 7000;
                default:
                    return 1000;
            }
        }

        /// <summary>
        /// 表2，检查服务成本预算需序号2  小计
        /// </summary>
        /// <param name="nandu">难度</param>
        /// <param name="bingLiShu">病例数</param>
        /// <returns></returns>
        private double jianChaFuWuFei2_Amount(ENanDuXiShu nandu, int bingLiShu)
        {
            return jianChaFuWuFei2_Price(nandu) * bingLiShu;
        }


        /// <summary>
        /// 表2，检查服务成本预算需序号3 单价
        /// </summary>
        /// <param name="nandu">难度</param>
        /// <param name="bingLiShu">病例数</param>
        /// <returns></returns>
        private double jianChaFuWuFei3_Price(ENanDuXiShu nandu, int bingLiShu)
        {
            return 5000;//先默认返回5000
            switch (nandu)
            {
                case ENanDuXiShu.简单级:
                case ENanDuXiShu.普通级:

                    if (bingLiShu <= 24)
                    {

                        return 5000;
                    }
                    else
                    {

                        return 8000;
                    }
                case ENanDuXiShu.稍难级:
                case ENanDuXiShu.困难级:
                case ENanDuXiShu.特别级:
                    if (bingLiShu <= 12)
                    {

                        return 5000;
                    }
                    else
                    {

                        return 8000;
                    }
                default:
                    return 5000;
            }
        }

        /// <summary>
        ///  表2，检查服务成本预算需序号3  小计
        /// </summary>
        /// <param name="nandu">难度</param>
        /// <param name="bingLiShu">病例数</param>
        /// <param name="zhongXinShu">中心数</param>
        /// <returns></returns>
        private double jianChaFuWuFei3_Amount(ENanDuXiShu nandu, int bingLiShu, int zhongXinShu)
        {
            return jianChaFuWuFei3_Price(nandu, bingLiShu) * zhongXinShu;
        }

        /// <summary>
        /// 表2，检查服务成本预算  合计
        /// </summary>
        /// <param name="nandu"></param>
        /// <param name="bingLiShu"></param>
        /// <param name="zhongXinShu"></param>
        /// <returns></returns>
        private double getJianChaFuWuFei(ENanDuXiShu nandu, int bingLiShu, int zhongXinShu)
        {
            return jianChaFuWuFei1_Amount(nandu, zhongXinShu) + jianChaFuWuFei2_Amount(nandu, bingLiShu) + jianChaFuWuFei3_Amount(nandu, bingLiShu, zhongXinShu);
        }


        public RenGongFeiYongZongE(CreateBaoJiaDanInput input)
        {
            GuanLiRenYuanFei = new FeiYongBase
            {
                Count = 1,
                Price = get_Price(input.NanDu),//输入参数 简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。
            };

            PMFei = new FeiYongBase
            {
                Count = input.ShiXian,
                Price = input.PMFei_Price,//输入参数 依据项目总时限和项目经理平均承担项目数量，基准为8000元-15000元/月*项目总时长（月）。
            };

            PLFei = new FeiYongBase
            {
                Count = input.ShiXian,
                Price = input.PLFei_Price,//输入参数 依据项目总时限和项目备配的情况，基准为6000元-10000元/月*项目总时长（月）。
            };

            CTAFei = new FeiYongBase
            {
                Count = input.ShiXian,
                Price = input.CTAFei_Price,//输入参数  依据项目总时限和项目备配的情况，基准为3000元-5000元/月*项目总时长（月）。
            };

            CRAFei = new FeiYongBase
            {
                Count = 1,
                Price = getJianChaFuWuFei(input.NanDu, input.LiShu, input.ZhongXinShu),// 按附表2进行测算。
            };

            XieTongJianChaFei = new FeiYongBase
            {
                Count = 2 * input.ZhongXinShu,
                Price = input.XieTongJianChaFei_Price,//输入参数

            };

            YiXueZhiChi_ZhuanXieFei = new FeiYongBase
            {
                Count = 1,
                Price = get_Price(input.NanDu),//  简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。
            };

            YiXueZhiChi_JianChaFei = new FeiYongBase
            {
                Count = 1,
                Price = 10000,//每次按10000元计算。 
            };
            ZhiKongZhiChiFei = new FeiYongBase
            {

                Count = 1,
                Price = get_Price(input.NanDu),//简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。
            };

            PVZhiChiFei = new FeiYongBase
            {
                Count = input.PVZhiChiFei_Count,//输入参数
                Price = input.PVZhiChiFei_Price,//输入参数
            };
            XiangMuJiangLiFei = new FeiYongBase
            {
                Count = input.LiShu,
                Price = input.XiangMuJiangLiFei_Price,//????????????????
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="zongBingLiShu">总病例数</param>
        /// <param name="queDingZhongXinShu">确定中心数</param>
        /// <param name="zongShiXian">总时限（月）</param>
        /// <param name="nandu">难度</param>
        public RenGongFeiYongZongE(int zongBingLiShu, int zongShiXian, int queDingZhongXinShu, ENanDuXiShu nandu)
        {
            GuanLiRenYuanFei = new FeiYongBase
            {
                Count = 1,
                Price = get_Price(nandu),//输入参数 简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。
            };

            PMFei = new FeiYongBase
            {
                Count = zongShiXian,
                Price = 15000,//输入参数 依据项目总时限和项目经理平均承担项目数量，基准为8000元-15000元/月*项目总时长（月）。
            };

            PLFei = new FeiYongBase
            {
                Count = zongShiXian,
                Price = 0,//输入参数 依据项目总时限和项目备配的情况，基准为6000元-10000元/月*项目总时长（月）。
            };

            CTAFei = new FeiYongBase
            {
                Count = zongShiXian,
                Price = 5000,//输入参数  依据项目总时限和项目备配的情况，基准为3000元-5000元/月*项目总时长（月）。
            };

            CRAFei = new FeiYongBase
            {
                Count = 1,//
                Price = 150000,//输入参数 按附表2进行测算。
            };

            XieTongJianChaFei = new FeiYongBase
            {
                Count = 2 * queDingZhongXinShu,
                Price = 3000,//输入参数

            };

            YiXueZhiChi_ZhuanXieFei = new FeiYongBase
            {
                Count = 1,
                Price = get_Price(nandu),//输入参数  简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。
            };

            YiXueZhiChi_JianChaFei = new FeiYongBase
            {
                Count = 1,
                Price = 10000,//输入参数 
            };
            ZhiKongZhiChiFei = new FeiYongBase
            {

                Count = 1,
                Price = get_Price(nandu),//输入参数
            };

            PVZhiChiFei = new FeiYongBase
            {
                Count = 0,//输入参数
                Price = 0,//输入参数
            };
            XiangMuJiangLiFei = new FeiYongBase
            {
                Count = zongBingLiShu,
                Price = 0,//输入参数
            };
        }

        /// <summary>
        /// 合计
        /// </summary>
        public double TotalAmount
        {
            get
            {
                double total = 0;
                if (GuanLiRenYuanFei != null)
                {

                    total += GuanLiRenYuanFei.HeJi;
                }
                if (PMFei != null)
                {

                    total += PMFei.HeJi;
                }
                if (PLFei != null)
                {

                    total += PLFei.HeJi;
                }
                if (CTAFei != null)
                {

                    total += CTAFei.HeJi;
                }
                if (CRAFei != null)
                {

                    total += CRAFei.HeJi;
                }
                if (XieTongJianChaFei != null)
                {

                    total += XieTongJianChaFei.HeJi;
                }
                if (YiXueZhiChi_ZhuanXieFei != null)
                {

                    total += YiXueZhiChi_ZhuanXieFei.HeJi;
                }
                if (YiXueZhiChi_JianChaFei != null)
                {

                    total += YiXueZhiChi_JianChaFei.HeJi;
                }
                if (ZhiKongZhiChiFei != null)
                {

                    total += ZhiKongZhiChiFei.HeJi;
                }
                if (PVZhiChiFei != null)
                {

                    total += PVZhiChiFei.HeJi;
                }
                if (XiangMuJiangLiFei != null)
                {

                    total += XiangMuJiangLiFei.HeJi;
                }
                return total;
            }
        }
        /// <summary>
        /// 管理人员
        /// </summary>
        /// <remarks>简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。</remarks>
        public FeiYongBase GuanLiRenYuanFei { get; set; }

        /// <summary>
        /// 项目经理
        /// </summary> 
        /// <remarks>依据项目总时限和项目经理平均承担项目数量，基准为8000元-15000元/月*项目总时长（月）。</remarks>
        public FeiYongBase PMFei { get; set; }

        /// <summary>
        /// 项目组长
        /// </summary>
        /// <remarks>依据项目总时限和项目备配的情况，基准为6000元-10000元/月*项目总时长（月）。</remarks>
        public FeiYongBase PLFei { get; set; }

        /// <summary>
        /// 项目助理
        /// </summary>
        /// <remarks>依据项目总时限和项目备配的情况，基准为3000元-5000元/月*项目总时长（月）。</remarks>
        public FeiYongBase CTAFei { get; set; }

        /// <summary>
        /// 监查服务（CRA）大临床
        /// </summary>
        /// <remarks>按附表2进行测算。</remarks>
        public FeiYongBase CRAFei { get; set; }

        /// <summary>
        /// 协同监查
        /// </summary>
        /// <remarks>包括PM、LM、QC三种角色的协同访视，通常每中心按2次计算。</remarks>
        public FeiYongBase XieTongJianChaFei { get; set; }

        /// <summary>
        /// 医学支持（撰写）
        /// </summary>
        /// <remarks>简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。</remarks>
        public FeiYongBase YiXueZhiChi_ZhuanXieFei { get; set; }

        /// <summary>
        /// 医学支持（监查）
        /// </summary>
        /// <remarks>每次按10000元计算。</remarks>
        public FeiYongBase YiXueZhiChi_JianChaFei { get; set; }


        /// <summary>
        /// 质控支持
        /// </summary>
        /// <remarks>简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。</remarks>
        public FeiYongBase ZhiKongZhiChiFei { get; set; }

        /// <summary>
        /// PV支持
        /// </summary>
        /// <remarks>需要根据项目合同和计划书确定。</remarks>
        public FeiYongBase PVZhiChiFei { get; set; }

        /// <summary>
        /// 项目奖励
        /// </summary>
        /// <remarks>合同系数：＜200W，0.4；200-399W，0.5；400-599W，0.6；600-899W，0.7；900-1199W，0.8；1200-1500W，0.9。</remarks>
        public FeiYongBase XiangMuJiangLiFei { get; set; }
    }

    public class HuiYiFeiBase
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
        /// 会议室费
        /// </summary>

        public double HuiYiShiFei { get; set; }

        /// <summary>
        /// 其他费
        /// </summary>
        public double QiTaFei { get; set; }

        /// <summary>
        /// 合计
        /// </summary>
        public double HeJi { get { return HuiWuFei + JiaoTongFei + ZhuSuFei + HuiYiShiFei + QiTaFei; } }
    }

    public class FeiYongBase
    {
        /// <summary>
        /// 单价
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 频次（中心或例数）
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 合计
        /// </summary>
        public double HeJi { get { return Count * Price; } }

    }


    public class DanLiGuanChaFei
    {


        public DanLiGuanChaFei(CreateBaoJiaDanInput input)
        {

            ShaiXuanQi = input.XieYi_ShaiXuanQi;
            RuZu = input.XieYi_RuZu;
            FeiXieYi = input.FeiXieYi;
        }
        public DanLiGuanChaFei()
        {

            ShaiXuanQi = 500;
            RuZu = 1000;
            FeiXieYi = 1000;
        }

        public double TotalAmount
        {
            get
            {
                return ShaiXuanQi + RuZu + FeiXieYi;
            }
        }

        /// <summary>
        /// 筛选期
        /// </summary>
        public double ShaiXuanQi { get; set; }

        /// <summary>
        /// 入组
        /// </summary>
        public double RuZu { get; set; }

        /// <summary>
        /// 非协议
        /// </summary>
        public double FeiXieYi { get; set; }
    }


    public class DanLiJIanChaFei
    {


        public DanLiJIanChaFei(CreateBaoJiaDanInput input)
        {

            ShaiXuanQiJianChaFei = input.ShaiXuanQiJianChaFei;
            RuZuHouJIanChaFei = input.RuZuHouJianChaFei;
        }
        public DanLiJIanChaFei()
        {
            ShaiXuanQiJianChaFei = 50;
            RuZuHouJIanChaFei = 100;
        }


        /// <summary>
        /// 合计
        /// </summary>
        public double TotalAmount { get { return ShaiXuanQiJianChaFei + RuZuHouJIanChaFei; } }
        /// <summary>
        /// 筛选器检查费
        /// </summary>
        public double ShaiXuanQiJianChaFei { get; set; }

        /// <summary>
        /// 入组后检查费
        /// </summary>
        public double RuZuHouJIanChaFei { get; set; }

    }

    public class DanLiShouShiZheBuZhu
    {


        public DanLiShouShiZheBuZhu(CreateBaoJiaDanInput input)
        {
            V1 = input.V1;
            V2 = input.V2;
            V3 = input.V3;
            V4 = input.V4;
            V5 = input.V5;
            AE = input.AE;
            DanLiShouShiZheBuZhu_Price=input.DanLiShouShiZheBuZhu;

        }
        public DanLiShouShiZheBuZhu()
        {
            V1 = 100;
            V2 = 100;
            V3 = 100;
            V4 = 100;
            V5 = 100;
            AE = 0;
            DanLiShouShiZheBuZhu_Price = 500;
        }

        /// <summary>
        /// 合计
        /// </summary>
        public double TotalAmount { get { return (V1 + V2 + V3 + V4 + V5); } }

        public double AE { get; set; }

        public double V1 { get; set; }
        public double V2 { get; set; }
        public double V3 { get; set; }
        public double V4 { get; set; }
        public double V5 { get; set; }

        public double DanLiShouShiZheBuZhu_Price { get; set; }


    }

    public class ChaiLvFeiChengBenGuiZeBiao
    {

        public ChaiLvFeiChengBenGuiZeBiao(CreateBaoJiaDanInput input)
        {
            PM_ChuChai_Price = input.PM_ChuChai_Price;
            PM_BuChuChai_Price = input.PM_BuChuChai_Price;
            CRA_ChuChai_Price = input.CRA_ChuChai_Price;
            CRA_BuChuChai_Price = input.CRA_BuChuChai_Price;
            Admin_ChuChai_Price = input.Admin_ChuChai_Price;
            Admin_BuChuChai_Price = input.Admin_BuChuChai_Price;

            PM_ChuChai_ZhongXin = (int)Math.Ceiling(0.4 * input.ZhongXinShu);
            PM_BuChuChai_ZhongXin = (int)Math.Ceiling(0.6 * input.ZhongXinShu);
            CRA_ChuChai_ZhongXin = (int)Math.Ceiling(0.4 * input.ZhongXinShu);
            CRA_BuChuChai_ZhongXin = (int)Math.Ceiling(0.6 * input.ZhongXinShu);
            Admin_ChuChai_ZhongXin = (int)Math.Ceiling(0.4 * input.ZhongXinShu);
            Admin_BuChuChai_ZhongXin = (int)Math.Ceiling(0.6 * input.ZhongXinShu);

            PM_ChuChai_Count = PM_ChuChai_ZhongXin * 3;
            PM_BuChuChai_Count = PM_BuChuChai_ZhongXin * 3;
            CRA_ChuChai_Count = CRA_ChuChai_ZhongXin * input.ShiXian;
            CRA_BuChuChai_Count = CRA_BuChuChai_ZhongXin * input.ShiXian;
            Admin_ChuChai_Count = Admin_ChuChai_ZhongXin * 2;
            Admin_BuChuChai_Count = Admin_BuChuChai_ZhongXin * 2;

            //PM(出差)
            PM_ChuChai_Amount = PM_ChuChai_Count * PM_ChuChai_Price;
            //PM（不出差）
            PM_BuChuChai_Amount = PM_BuChuChai_Count * PM_BuChuChai_Price;
            //CRA（出差）
            CRA_ChuChai_Amount = CRA_ChuChai_Count * CRA_ChuChai_Price;
            //CRA(不出差)
            CRA_BuChuChai_Amount = CRA_BuChuChai_Count * CRA_BuChuChai_Price;
            //管理人员（出差）
            Admin_ChuChai_Amount = Admin_ChuChai_Count * Admin_ChuChai_Price;
            //管理人员（不出差）
            Admin_BuChuChai_Amount = Admin_BuChuChai_Count * Admin_BuChuChai_Price;
        }

        public ChaiLvFeiChengBenGuiZeBiao(int ShiXian, int ZhongXinShu)
        {
            PM_ChuChai_Price = 3000;
            PM_BuChuChai_Price = 500;
            CRA_ChuChai_Price = 3000;
            CRA_BuChuChai_Price = 800;
            Admin_ChuChai_Price = 3000;
            Admin_BuChuChai_Price = 500;

            PM_ChuChai_ZhongXin = (int)Math.Ceiling(0.4 * ZhongXinShu);
            PM_BuChuChai_ZhongXin = (int)Math.Ceiling(0.6 * ZhongXinShu);
            CRA_ChuChai_ZhongXin = (int)Math.Ceiling(0.4 * ZhongXinShu);
            CRA_BuChuChai_ZhongXin = (int)Math.Ceiling(0.6 * ZhongXinShu);
            Admin_ChuChai_ZhongXin = (int)Math.Ceiling(0.4 * ZhongXinShu);
            Admin_BuChuChai_ZhongXin = (int)Math.Ceiling(0.6 * ZhongXinShu);

            PM_ChuChai_Count = PM_ChuChai_ZhongXin * 3;
            PM_BuChuChai_Count = PM_BuChuChai_ZhongXin * 3;
            CRA_ChuChai_Count = CRA_ChuChai_ZhongXin * ShiXian;
            CRA_BuChuChai_Count = CRA_BuChuChai_ZhongXin * ShiXian;
            Admin_ChuChai_Count = Admin_ChuChai_ZhongXin * 2;
            Admin_BuChuChai_Count = Admin_BuChuChai_ZhongXin * 2;

            //PM(出差)
            PM_ChuChai_Amount = PM_ChuChai_Count * PM_ChuChai_Price;
            //PM（不出差）
            PM_BuChuChai_Amount = PM_BuChuChai_Count * PM_BuChuChai_Price;
            //CRA（出差）
            CRA_ChuChai_Amount = CRA_ChuChai_Count * CRA_ChuChai_Price;
            //CRA(不出差)
            CRA_BuChuChai_Amount = CRA_BuChuChai_Count * CRA_BuChuChai_Price;
            //管理人员（出差）
            Admin_ChuChai_Amount = Admin_ChuChai_Count * Admin_ChuChai_Price;
            //管理人员（不出差）
            Admin_BuChuChai_Amount = Admin_BuChuChai_Count * Admin_BuChuChai_Price;
        }



        /// <summary>
        /// 合计
        /// </summary>
        public double TotalAmount
        {
            get
            {
                return (
                    PM_ChuChai_Amount + PM_BuChuChai_Amount + CRA_ChuChai_Amount + CRA_BuChuChai_Amount + Admin_BuChuChai_Amount + Admin_ChuChai_Amount
                    );
            }
        }

        /// <summary>
        /// PM（出差）成本小计
        /// </summary>
        /// <remarks>PM出差，按预计启动和协同监查次数计算，每中心预计3次，平均每次3000元</remarks>
        public double PM_ChuChai_Amount { get; set; }
        /// <summary>
        /// PM（出差）单价
        /// </summary>
        /// <remarks>PM出差，按预计启动和协同监查次数计算，每中心预计3次，平均每次3000元</remarks>
        public double PM_ChuChai_Price { get; set; }
        /// <summary>
        /// PM（出差）频次/数
        /// </summary>
        /// <remarks>PM出差，按预计启动和协同监查次数计算，每中心预计3次，平均每次3000元</remarks>
        public int PM_ChuChai_Count { get; set; }

        /// <summary>
        /// PM（出差）中心数
        /// </summary>
        /// <remarks>PM出差，按预计启动和协同监查次数计算，每中心预计3次，平均每次3000元</remarks>
        public int PM_ChuChai_ZhongXin { get; set; }

        /// <summary>
        /// PM（不出差）成本小计
        /// </summary>
        /// <remarks>PM出差，按预计启动和协同监查次数计算，每中心预计3次，平均每次3000元</remarks>
        public double PM_BuChuChai_Amount { get; set; }
        /// <summary>
        /// PM（不出差）单价
        /// </summary>
        /// <remarks>PM出差，按预计启动和协同监查次数计算，每中心预计3次，平均每次3000元</remarks>
        public double PM_BuChuChai_Price { get; set; }
        /// <summary>
        /// PM（不出差）频次/数
        /// </summary>
        /// <remarks>PM出差，按预计启动和协同监查次数计算，每中心预计3次，平均每次3000元</remarks>
        public int PM_BuChuChai_Count { get; set; }

        /// <summary>
        /// PM（不出差）中心数
        /// </summary>
        /// <remarks>PM出差，按预计启动和协同监查次数计算，每中心预计3次，平均每次3000元</remarks>
        public int PM_BuChuChai_ZhongXin { get; set; }

        /// <summary>
        /// CRA（出差）成本小计
        /// </summary>
        /// <remarks>CRA出差，非出差中心约3个，非出差每月800元，频次与服务周期同步。</remarks>
        public double CRA_ChuChai_Amount { get; set; }
        /// <summary>
        ///CRA（出差）单价
        /// </summary>
        /// <remarks>CRA出差，非出差中心约3个，非出差每月800元，频次与服务周期同步。</remarks>
        public double CRA_ChuChai_Price { get; set; }
        /// <summary>
        /// CRA（出差）频次/数
        /// </summary>
        /// <remarks>CRA出差，非出差中心约3个，非出差每月800元，频次与服务周期同步。</remarks>
        public int CRA_ChuChai_Count { get; set; }

        /// <summary>
        /// CRA（出差）中心数
        /// </summary>
        /// <remarks>CRA出差，非出差中心约3个，非出差每月800元，频次与服务周期同步。</remarks>
        public int CRA_ChuChai_ZhongXin { get; set; }

        /// <summary>
        /// CRA（不出差）成本小计
        /// </summary>
        /// <remarks>CRA出差，非出差中心约3个，非出差每月800元，频次与服务周期同步。</remarks>
        public double CRA_BuChuChai_Amount { get; set; }
        /// <summary>
        ///CRA（不出差）单价
        /// </summary>
        /// <remarks>CRA出差，非出差中心约3个，非出差每月800元，频次与服务周期同步。</remarks>
        public double CRA_BuChuChai_Price { get; set; }
        /// <summary>
        /// CRA（不出差）频次/数
        /// </summary>
        /// <remarks>CRA出差，非出差中心约3个，非出差每月800元，频次与服务周期同步。</remarks>
        public int CRA_BuChuChai_Count { get; set; }

        /// <summary>
        /// CRA（不出差）中心数
        /// </summary>
        /// <remarks>CRA出差，非出差中心约3个，非出差每月800元，频次与服务周期同步。</remarks>
        public int CRA_BuChuChai_ZhongXin { get; set; }

        /// <summary>
        /// 管理人员（ 出差）成本小计
        /// </summary>
        /// <remarks>管理人员不出差，每家中心预计2次，平均每次500元</remarks>
        public double Admin_ChuChai_Amount { get; set; }
        /// <summary>
        ///管理人员（ 出差）单价
        /// </summary>
        /// <remarks>管理人员不出差，每家中心预计2次，平均每次500元</remarks>
        public double Admin_ChuChai_Price { get; set; }
        /// <summary>
        /// 管理人员（ 出差）频次/数
        /// </summary>
        /// <remarks>管理人员不出差，每家中心预计2次，平均每次500元</remarks>
        public int Admin_ChuChai_Count { get; set; }

        /// <summary>
        ///管理人员（ 出差）中心数
        /// </summary>
        /// <remarks>管理人员不出差，每家中心预计2次，平均每次500元</remarks>
        public int Admin_ChuChai_ZhongXin { get; set; }

        /// <summary>
        /// 管理人员（不出差）成本小计
        /// </summary>
        /// <remarks>管理人员不出差，每家中心预计2次，平均每次500元</remarks>
        public double Admin_BuChuChai_Amount { get; set; }
        /// <summary>
        ///管理人员（不出差）单价
        /// </summary>
        /// <remarks>管理人员不出差，每家中心预计2次，平均每次500元</remarks>
        public double Admin_BuChuChai_Price { get; set; }
        /// <summary>
        /// 管理人员（不出差）频次/数
        /// </summary>
        /// <remarks>管理人员不出差，每家中心预计2次，平均每次500元</remarks>
        public int Admin_BuChuChai_Count { get; set; }

        /// <summary>
        ///管理人员（不出差）中心数
        /// </summary>
        /// <remarks>管理人员不出差，每家中心预计2次，平均每次500元</remarks>
        public int Admin_BuChuChai_ZhongXin { get; set; }

    }
}
