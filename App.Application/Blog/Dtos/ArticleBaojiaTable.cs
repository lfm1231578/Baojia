using System;
using System.Collections.Generic;

namespace App.Application.Blog.Dtos
{
    [Serializable]
    public class ArticleBaojiaTable : EntityDto<string>
    {
        public string CompanyName { get; set; }
        public string ProjectName { get; set; }
        public string Yushuan { get; set; }
        public string Other { get; set; }
        public string Type { get; set; }
        public string LiShu { get; set; }
        public string ShiXian { get; set; }
        public string ZhongXinShu { get; set; }
        public string NanDu { get; set; }
        public string ShiYingZheng { get; set; }
        public string SuiFang { get; set; }
        public string XieTiaoHui_HuiWuFei { get; set; }
        public string XieTiaoHui_JiaoTongFei { get; set; }
        public string XieTiaoHui_ZhuSuFei { get; set; }
        public string XieTiaoHui_HuiYiShiFei { get; set; }
        public string XieTiaoHui_QiTaFei { get; set; }
        public string ZongJieHui_HuiWuFei { get; set; }
        public string ZongJieHui_JiaoTongFei { get; set; }
        public string ZongJieHui_ZhuSuFei { get; set; }
        public string ZongJieHui_HuiYiShiFei { get; set; }
        public string ZongJieHui_QiTaFei { get; set; }
        public string ZhongQiHui_HuiWuFei { get; set; }
        public string ZhongQiHui_JiaoTongFei { get; set; }
        public string ZhongQiHui_ZhuSuFei { get; set; }
        public string ZhongQiHui_HuiYiShiFei{ get; set; }
        public string ZhongQiHui_QiTaFei { get; set; }
        public string MangTaiShenHeHui_HuiWuFei { get; set; }
        public string MangTaiShenHeHui_JiaoTongFei { get; set; }
        public string MangTaiShenHeHui_ZhuSuFei { get; set; }
        public string MangTaiShenHeHui_HuiYiShiFei { get; set; }
        public string MangTaiShenHeHui_QiTaFei { get; set; }
        public string ZuZhangFei_Price { get; set; }
        public string ZuZhangFei_Count { get; set; }
        public string LunLiFei_Price { get; set; }
        public string QiDongHuiFei_Price { get; set; }
        public string YunShuFei_Price { get; set; }
        public string ShiJiHaoCaiFei_Price { get; set; }
        public string ShiJiHaoCaiFei_Count { get; set; }
        public string ZhongXinShiYanShiFei_Price { get; set; }
        public string ShuJuTongJiFei_Price { get; set; }
        public string XiTongShiYongFei_Price { get; set; }
        public string YinShuaFei_Price { get; set; }
        public string QiTaCaiGouFei_Price { get; set; }
        public string QiTaCaiGouFei_Count { get; set; }
        public string ShouShiZheZhaoMuFei_Price { get; set; }
        public string ShouShiZheZhaoMuFei_Count { get; set; }
        public string ShengNeiJiChaFei_Price { get; set; }
        public string ShengNeiJiChaFei_Count { get; set; }
        public string ShengWaiJiChaFei_Price { get; set; }
        public string ShengWaiJiChaFei_Count { get; set; }
        public string SMOFei_Price { get; set; }
        public string WeiWaiJianChaFuWuFei_Price { get; set; }
        public string WeiWaiJianChaFuWuFei_Count { get; set; }
        public string YiChuanBanTianBaoFei_Price { get; set; }
        public string QiTaFei_Price { get; set; }
        public string GuanLiRenYuanFei_Price { get; set; }
        public string PMFei_Price { get; set; }
        public string PLFei_Price { get; set; }
        public string CTAFei_Price { get; set; }
        public string XieTongJianChaFei_Price { get; set; }
        public string YiXueZhiChiZhuanXieFei_Price { get; set; }
        public string YiXueZhiChiJianChaFei_Price { get; set; }
        public string ZhiKongZhiChiFei_Price { get; set; }
        public string PVZhiChiFei_Price { get; set; }
        public string PVZhiChiFei_Count { get; set; }
        public string XiangMuJiangLiFei_Price { get; set; }
        public string PM_ChuChai_Price { get; set; }
        public string PM_BuChuChai_Price { get; set; }
        public string CRA_ChuChai_Price { get; set; }
        public string CRA_BuChuChai_Price { get; set; }
        public string Admin_ChuChai_Price { get; set; }
        public string Admin_BuChuChai_Price { get; set; }
        public string XieYi_ShaiXuanQi { get; set; }
        public string XieYi_RuZu { get; set; }
        public string FeiXieYi { get; set; }
        public string ShaiXuanQiJianChaFei { get; set; }
        public string RuZuHouJianChaFei { get; set; }
        public string V1 { get; set; }
        public string V2 { get; set; }
        public string V3 { get; set; }
        public string V4 { get; set; }
        public string V5 { get; set; } 
        public string CreateTime { get; set; }
        public string AE { get; set; }

        public string DanLiJianChaFei { get; set; }

        public string DanLiShouShiZheBuZhu { get; set; }
    }
}