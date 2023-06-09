//using App.Application.Dtos;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks; 
//using Spire.Doc.Documents;
//using Spire.Pdf.Graphics;
//using Spire.Pdf;
//using Spire.Xls;
//using Spire.Xls.Core;
//using System;
//using System.Collections.Generic;
//using System.DirectoryServices.Protocols;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
//using Document = iTextSharp.text.Document;
//using PageSize = iTextSharp.text.PageSize;


//namespace App.Application
//{ 

//        public class BaoJiaService : IBaoJiaService
//        {

//            public CreateBaoJiaDanInput Init(InitInput input)
//            {
//                XiangMuYuSuanZongBiaoDto XiangMuYuSuanZong = new XiangMuYuSuanZongBiaoDto(input.LiShu, input.ShiXian, input.ZhongXinShu, input.NanDu);
//                var output = new CreateBaoJiaDanInput()
//                {
//                    CompanyName = XiangMuYuSuanZong.CompanyName,
//                    CTAFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.CTAFei.Price,
//                    GuanLiRenYuanFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.GuanLiRenYuanFei.Price,
//                    LiShu = XiangMuYuSuanZong.ZongBingLiShu,
//                    LunLiFei_Price = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.LunLiFei.Price,
//                    MangTaiShenHeHui = new HuiYiFeiInput
//                    {
//                        HuiWuFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.HuiWuFei,
//                        HuiYiShiFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.HuiYiShiFei,
//                        JiaoTongFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.JiaoTongFei,
//                        QiTaFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.QiTaFei,
//                        ZhuSuFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.ZhuSuFei,
//                    },
//                    NanDu = input.NanDu,
//                    PLFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.PLFei.Price,
//                    PMFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.PMFei.Price,
//                    ProjectName = XiangMuYuSuanZong.ProjectName,
//                    PVZhiChiFei_Count = XiangMuYuSuanZong.RenGongFeiYongZongE.PVZhiChiFei.Count,
//                    PVZhiChiFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.PVZhiChiFei.Price,
//                    QiDongHuiFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.QiDongHuiFei.Price,
//                    QiTaCaiGouFei_Count = XiangMuYuSuanZong.QiTaZaFeiZongE.QiTaCaiGouFei.Count,
//                    QiTaCaiGouFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.QiTaCaiGouFei.Price,
//                    QiTaFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.QiTaFei.Price,
//                    ShengNeiJiChaFei_Count = XiangMuYuSuanZong.QiTaZaFeiZongE.ShengNeiJiChaFei.Count,
//                    ShengNeiJiChaFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.ShengNeiJiChaFei.Price,
//                    ShengWaiJiChaFei_Count = XiangMuYuSuanZong.QiTaZaFeiZongE.ShengWaiJiChaFei.Count,
//                    ShengWaiJiChaFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.ShengWaiJiChaFei.Price,
//                    ShiJiHaoCaiFei_Count = XiangMuYuSuanZong.QiTaZaFeiZongE.ShiJiHaoCaiFei.Count,
//                    ShiJiHaoCaiFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.ShiJiHaoCaiFei.Price,
//                    ShiXian = input.ShiXian,
//                    ShiYingZheng = XiangMuYuSuanZong.ShiYingZheng,
//                    ShouShiZheZhaoMuFei_Count = XiangMuYuSuanZong.QiTaZaFeiZongE.ShouShiZheZhaoMuFei.Count,
//                    ShouShiZheZhaoMuFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.ShouShiZheZhaoMuFei.Price,
//                    ShuJuTongJiFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.ShuJuTongJiFei.Price,
//                    SMOFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.SMOFei.Price,
//                    SuiFang = XiangMuYuSuanZong.SuiFang,
//                    Type = 0,
//                    WeiWaiJianChaFuWuFei_Count = XiangMuYuSuanZong.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.Count,
//                    WeiWaiJianChaFuWuFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.Price,
//                    XiangMuJiangLiFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.XiangMuJiangLiFei.Price,
//                    XieTiaoHui = new HuiYiFeiInput
//                    {
//                        HuiWuFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.HuiWuFei,
//                        HuiYiShiFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.HuiYiShiFei,
//                        JiaoTongFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.JiaoTongFei,
//                        QiTaFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.QiTaFei,
//                        ZhuSuFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.ZhuSuFei,
//                    },
//                    XieTongJianChaFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.XieTongJianChaFei.Price,
//                    XiTongShiYongFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.XiTongShiYongFei.Price,
//                    YiChuanBanTianBaoFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.YiChuanBanTianBaoFei.Price,
//                    YinShuaFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.YinShuaFei.Price,
//                    YiXueZhiChiJianChaFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.YiXueZhiChi_JianChaFei.Price,
//                    YiXueZhiChiZhuanXieFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.YiXueZhiChi_ZhuanXieFei.Price,
//                    YunShuFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.YunShuFei.Price,
//                    ZhiKongZhiChiFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.ZhiKongZhiChiFei.Price,
//                    ZhongQiHui = new HuiYiFeiInput
//                    {
//                        HuiWuFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.HuiWuFei,
//                        HuiYiShiFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.HuiYiShiFei,
//                        JiaoTongFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.JiaoTongFei,
//                        QiTaFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.QiTaFei,
//                        ZhuSuFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.ZhuSuFei,

//                    },
//                    ZhongXinShiYanShiFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.ZhongXinShiYanShiFei.Price,
//                    ZhongXinShu = XiangMuYuSuanZong.QueDingZhongXinShu,
//                    ZongJieHui = new HuiYiFeiInput
//                    {
//                        HuiWuFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.HuiWuFei,
//                        HuiYiShiFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.HuiYiShiFei,
//                        JiaoTongFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.JiaoTongFei,
//                        QiTaFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.QiTaFei,
//                        ZhuSuFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.ZhuSuFei,
//                    },
//                    ZuZhangFei_Count = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.ZuZhangFei.Count,
//                    ZuZhangFei_Price = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.ZuZhangFei.Price,


//                };
//                return output;

//            }

//            public CreateBaoJiaDanOutput CreateBaoJiaDan(CreateBaoJiaDanInput input)
//            {
//                XiangMuYuSuanZongBiaoDto XiangMuYuSuanZong = new XiangMuYuSuanZongBiaoDto(100, 12, 5, ENanDuXiShu.稍难级);


//                XiangMuYuSuanZongBiaoDto XiangMuYuSuanZong2 = new XiangMuYuSuanZongBiaoDto(input);

//                ShiYanFangAnDto fangan = new ShiYanFangAnDto();

//                fangan.YanJiuYuSuanQingDan = new YanJiuYuSuanQingDanDto
//                {
//                    CRO_FuWuFei = "195.04",
//                    RiChangFei = "14.89",
//                    HuiYiFei = "15.37",
//                    CRO_FuWuHeJi = "225.3",
//                    YiYuanYanJiuFei = "275.49",
//                    SMOFei = "145.75",
//                    HanShuiHeJi = "646.5"
//                };

//                fangan.LinChuangShiYanFuWuFei = new LinChuangShiYanFuWuFeiDto
//                {
//                    ShaiXuanYanJiuDanWei = "3",
//                    LinChuangShiYanFangAnSheJi = "15",
//                    LinChuangShiYanFangAnSheJi_1 = "15",
//                    CROFuWuFei = "119.5",
//                    CROFuWuFei_1 = "7.5",
//                    CROFuWuFei_2 = "67.5",
//                    CROFuWuFei_3 = "7.5",
//                    CROFuWuFei_4 = "36",
//                    CROFuWuFei_5 = "1",
//                    ShuJuGuanLiHeTongJiFenXi = "30",
//                    ShuJuGuanLiHeTongJiFenXi_1 = "18",
//                    ShuJuGuanLiHeTongJiFenXi_2 = "12",
//                    ZongJie = "16.5",
//                    ZongJie_1 = "1.5",
//                    ZongJie_2 = "15",
//                    HeJi = "184万",
//                    HanShuiHeJi = "195.04万",
//                };

//                fangan.RiChangFei = new RiChangFeiDto
//                {
//                    BanGongTongXun = "2",
//                    YinShuaZhuangDing = "3",
//                    ChaiLv = "9.05",
//                    HeJi = "14.05",
//                    HanShuiHeJi = "14.89"
//                };

//                fangan.HuiYiFei = new HuiYiFeiDto
//                {
//                    YanTaoHui = "8",
//                    QiDongHui = "1.5",
//                    ShenJiHui = "5",
//                    HeJi = "14.5",
//                    HanShuiHeJi = "14.5",

//                };

//                fangan.LinChuangShiYanYanJiuFei = new LinChuangShiYanYanJiuFeiDto
//                {
//                    LunLiShenCha_Amount = "2.4",
//                    LunLiShenCha_Count = "3家",
//                    LunLiShenCha_Price = "0.8",
//                    ZuZhangDanWei_Amount = "10",
//                    ZuZhangDanWei_Count = "10",
//                    ZuZhangDanWei_Price = "1家",
//                    YanJiuZheFei_Amount = "142.5",
//                    YanJiuZheFei_Count = "150 例",
//                    YanJiuZheFei_Price = "0.95",
//                    ShouShiZheBuTie_Amount1 = "14",
//                    ShouShiZheBuTie_Count1 = "100 例",
//                    ShouShiZheBuTie_Price1 = "0.14",
//                    ShouShiZheBuTie_Amount2 = "19",
//                    ShouShiZheBuTie_Count2 = "0.38",
//                    ShouShiZheBuTie_Price2 = "50 例",
//                    ShouShiZheJianCha_Amount = "18",
//                    ShouShiZheJianCha_Count = "150 例",
//                    ShouShiZheJianCha_Price = "0.12",
//                    JiGouGuanLi_Amount = "54",
//                    JiGouGuanLi_Count = "150 例",
//                    JiGouGuanLi_Price = "0.36",
//                    BaoXian_Amount = "/",
//                    BaoXian_Count = "150 例",
//                    BaoXian_Price = "/",
//                    HeJi = "259.9",
//                    HanShuiHeJi = "275.49",
//                };

//                fangan.SMOFei = new SMOFeiDto
//                {
//                    CRC_FuWu_Count1 = "100例",
//                    CRC_FuWu_Price1 = "8500元",
//                    CRC_CFuWu_Amount1 = "85",
//                    CRC_FuWu_Count2 = "50例",
//                    CRC_FuWu_Price2 = "9500元",
//                    CRC_FuWu_Amount2 = "47.5",
//                    SMO_GongYingShangGuanLi = "13.25",
//                    HanShuiHeJi = "145.75",
//                    AVG = "0.9717万/例",
//                    ShouShiZhe_Count = 150,
//                    Center_Count = 3,
//                    CRC_Count = 1,
//                    CRC_PM_Count = 1,

//                };
//                CreateBaoJiaDanOutput output = new CreateBaoJiaDanOutput();
//                output.ShiYanFangAn = fangan;
//                output.XiangMuYuSuanZongBiao = XiangMuYuSuanZong;

//                string xlsurl = CreateExcel(XiangMuYuSuanZong2);
//                output.XlsFile = new OutputFile
//                {
//                    Name = XiangMuYuSuanZong2.CompanyName,
//                    Url = "http://192.168.20.32:8081/baojia/FileDownload/" + xlsurl
//                };

//                string pdfurl = CreatePdf(fangan);
//                output.PdfFile = new OutputFile
//                {
//                    Name = XiangMuYuSuanZong2.CompanyName,
//                    Url = "http://192.168.20.32:8081/baojia/FileDownload/" + pdfurl
//                };

//                return output;
//            }

//            public string CreateExcel(XiangMuYuSuanZongBiaoDto data)
//            {
//                Workbook wbToStream = new Workbook();
//                #region 表1，项目预算总表（含说明）


//                Worksheet sheet = wbToStream.Worksheets[0];
//                sheet.Name = "表1，项目预算总表（含说明）";

//                CellRange range = sheet.Range["A1:L55"];
//                range.BorderAround(LineStyleType.Thin);
//                range.BorderInside(LineStyleType.Thin);
//                range.Style.VerticalAlignment = VerticalAlignType.Center;//垂直居中
//                range.Style.HorizontalAlignment = HorizontalAlignType.Center; // 水平居中
//                range.Style.WrapText = true;
//                range.RowHeight = 16;
//                range.Style.Font.Size = 10;
//                range.Style.Font.FontName = "宋体";

//                sheet.Columns[1].ColumnWidth = 19;
//                sheet.Columns[3].ColumnWidth = 13;
//                sheet.Columns[7].ColumnWidth = 16;
//                sheet.Columns[9].ColumnWidth = 11;
//                sheet.Columns[10].ColumnWidth = 16;

//                #region row1
//                sheet.Range["A1:L1"].Text = "项  目  预  算  表";
//                sheet.Range["A1:L1"].Merge();
//                sheet.Range["A1:L1"].Style.Font.Size = 14;
//                sheet.Range["A1:L1"].Style.Font.IsBold = true;
//                sheet.Range["A1:L1"].RowHeight = 21;
//                #endregion

//                #region row2 
//                sheet.Range["A2:B2"].Text = "委托方（全称）：";
//                sheet.Range["A2:B2"].Merge();
//                sheet.Range["C2:G2"].Text = data.CompanyName;
//                sheet.Range["C2:G2"].Style.HorizontalAlignment = HorizontalAlignType.Left;
//                sheet.Range["C2:G2"].Merge();
//                sheet.Range["H2"].Text = "总病例数（例）：";
//                sheet.Range["I2"].NumberValue = data.ZongBingLiShu;
//                sheet.Range["I2"].Style.HorizontalAlignment = HorizontalAlignType.Left;


//                sheet.Range["J2:K2"].Text = "计划筛选中心数（个）：";
//                sheet.Range["J2:K2"].Merge();
//                sheet.Range["L2"].NumberValue = data.JiHuaShuaiXuanZhongXinShu;
//                sheet.Range["L2"].Style.HorizontalAlignment = HorizontalAlignType.Left;

//                sheet.Range["A2:L2"].Style.Font.IsBold = true;
//                sheet.Range["L2"].Style.KnownColor = ExcelColors.Yellow;
//                #endregion
//                #region row3

//                sheet.Range["A3:B3"].Text = "项目名称（全称）：";
//                sheet.Range["A3:B3"].Merge();
//                sheet.Range["C3:G3"].Text = data.ProjectName;
//                sheet.Range["C3:G3"].Style.HorizontalAlignment = HorizontalAlignType.Left;
//                sheet.Range["C3:G3"].Merge();

//                sheet.Range["H3"].Text = "总时限（月）：";
//                sheet.Range["I3"].NumberValue = data.ZongShiXian;
//                sheet.Range["I3"].Style.HorizontalAlignment = HorizontalAlignType.Left;

//                sheet.Range["J3:K3"].Text = "计划确定中心数（个）：";
//                sheet.Range["J3:K3"].Merge();
//                sheet.Range["L3"].NumberValue = data.QueDingZhongXinShu;
//                sheet.Range["L3"].Style.HorizontalAlignment = HorizontalAlignType.Left;

//                sheet.Range["A3:L3"].Style.Font.IsBold = true;
//                #endregion

//                #region row4 一、会议费用总额(RMB)：
//                sheet.Range["A4:B4"].Text = "一、会议费用总额(RMB)：";
//                sheet.Range["A4:B4"].Merge();
//                sheet.Range["C4:G4"].NumberValue = data.HuiYiFeiZongE.TotalAmount;
//                sheet.Range["C4:G4"].Style.HorizontalAlignment = HorizontalAlignType.Left;

//                sheet.Range["C4:L4"].Merge();
//                sheet.Range["A4:L4"].Style.Font.IsBold = true;
//                sheet.Range["A4:L4"].Style.KnownColor = ExcelColors.Yellow;


//                sheet.Range["A5"].Text = "费用项目";
//                sheet.Range["B5"].Text = "费用明细";
//                sheet.Range["C5"].Text = "金额";
//                sheet.Range["A6:A11"].Text = "协调会费用";
//                sheet.Range["A6:A11"].IsWrapText = true;
//                sheet.Range["A6:A11"].Merge();

//                sheet.Range["B6"].Text = "会务费";
//                sheet.Range["C6"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.HuiWuFei;

//                sheet.Range["B7"].Text = "交通费";
//                sheet.Range["C7"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.JiaoTongFei;

//                sheet.Range["B8"].Text = "住宿费";
//                sheet.Range["C8"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.ZhuSuFei;

//                sheet.Range["B9"].Text = "会议室费";
//                sheet.Range["C9"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.HuiYiShiFei;

//                sheet.Range["B10"].Text = "其他费用";
//                sheet.Range["C10"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.QiTaFei;

//                sheet.Range["B11"].Text = "合计";
//                sheet.Range["C11"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.HeJi;
//                sheet.Range["C11"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["B11:C11"].Style.Font.IsBold = true;
//                sheet.Range["C6:C11"].Style.HorizontalAlignment = HorizontalAlignType.Left;


//                sheet.Range["D5"].Text = "费用项目";
//                sheet.Range["E5"].Text = "费用明细";
//                sheet.Range["F5"].Text = "金额";
//                sheet.Range["D6"].Text = "总结会费用";
//                sheet.Range["D6:D11"].Merge();

//                sheet.Range["E6"].Text = "会务费";
//                sheet.Range["F6"].NumberValue = data.HuiYiFeiZongE.ZongJieHui.HuiWuFei;

//                sheet.Range["E7"].Text = "交通费";
//                sheet.Range["F7"].NumberValue = data.HuiYiFeiZongE.ZongJieHui.JiaoTongFei;

//                sheet.Range["E8"].Text = "住宿费";
//                sheet.Range["F8"].NumberValue = data.HuiYiFeiZongE.ZongJieHui.ZhuSuFei;

//                sheet.Range["E9"].Text = "会议室费";
//                sheet.Range["F9"].NumberValue = data.HuiYiFeiZongE.ZongJieHui.HuiYiShiFei;

//                sheet.Range["E10"].Text = "其他费用";
//                sheet.Range["F10"].NumberValue = data.HuiYiFeiZongE.ZongJieHui.QiTaFei;

//                sheet.Range["E11"].Text = "合计";
//                sheet.Range["F11"].NumberValue = data.HuiYiFeiZongE.ZongJieHui.HeJi;
//                sheet.Range["F11"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["E11:F11"].Style.Font.IsBold = true;
//                sheet.Range["F6:F11"].Style.HorizontalAlignment = HorizontalAlignType.Left;


//                sheet.Range["G5"].Text = "费用项目";
//                sheet.Range["H5"].Text = "费用明细";
//                sheet.Range["I5"].Text = "金额";
//                sheet.Range["G6"].Text = "中期会（或论证会）";
//                sheet.Range["G6:G11"].Merge();
//                sheet.Range["H6"].Text = "会务费";
//                sheet.Range["I6"].NumberValue = data.HuiYiFeiZongE.ZhongQiHui.HuiWuFei;

//                sheet.Range["H7"].Text = "交通费";
//                sheet.Range["I7"].NumberValue = data.HuiYiFeiZongE.ZhongQiHui.JiaoTongFei;

//                sheet.Range["H8"].Text = "住宿费";
//                sheet.Range["I8"].NumberValue = data.HuiYiFeiZongE.ZhongQiHui.ZhuSuFei;

//                sheet.Range["H9"].Text = "会议室费";
//                sheet.Range["I9"].NumberValue = data.HuiYiFeiZongE.ZhongQiHui.HuiYiShiFei;

//                sheet.Range["H10"].Text = "其他费用";
//                sheet.Range["I10"].NumberValue = data.HuiYiFeiZongE.ZhongQiHui.QiTaFei;

//                sheet.Range["H11"].Text = "合计";
//                sheet.Range["J11"].NumberValue = data.HuiYiFeiZongE.ZhongQiHui.HeJi;
//                sheet.Range["J11"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["H11:J11"].Style.Font.IsBold = true;
//                sheet.Range["I6:I11"].Style.HorizontalAlignment = HorizontalAlignType.Left;

//                sheet.Range["J5"].Text = "费用项目";
//                sheet.Range["K5"].Text = "费用明细";
//                sheet.Range["L5"].Text = "金额";
//                sheet.Range["J6"].Text = "盲态审核会";
//                sheet.Range["J6:J11"].Merge();
//                sheet.Range["K6"].Text = "会务费";
//                sheet.Range["L6"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.HuiWuFei;

//                sheet.Range["K7"].Text = "交通费";
//                sheet.Range["L7"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.JiaoTongFei;

//                sheet.Range["K8"].Text = "住宿费";
//                sheet.Range["L8"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.ZhuSuFei;

//                sheet.Range["K9"].Text = "会议室费";
//                sheet.Range["L9"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.HuiYiShiFei;

//                sheet.Range["K10"].Text = "其他费用";
//                sheet.Range["L10"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.QiTaFei;

//                sheet.Range["K11"].Text = "合计";
//                sheet.Range["L11"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.HeJi;
//                sheet.Range["L11"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["K11:L11"].Style.Font.IsBold = true;
//                sheet.Range["L6:L11"].Style.HorizontalAlignment = HorizontalAlignType.Left;

//                sheet.Range["A5:L5"].Style.KnownColor = ExcelColors.Gray25Percent;
//                sheet.Range["A6:B11"].Style.KnownColor = ExcelColors.Gray25Percent;
//                sheet.Range["D6:E11"].Style.KnownColor = ExcelColors.Gray25Percent;
//                sheet.Range["G6:H11"].Style.KnownColor = ExcelColors.Gray25Percent;
//                sheet.Range["J6:K11"].Style.KnownColor = ExcelColors.Gray25Percent;

//                #endregion

//                #region row4 二、医院合同费用总额(RMB)： 
//                sheet.Range["A12:B12"].Text = "二、医院合同费用总额(RMB)：";
//                sheet.Range["A12:B12"].Merge();

//                sheet.Range["C12:D12"].NumberValue = data.YiYuanHeTongFeiZongE.TotalAmount;
//                sheet.Range["C12:D12"].Style.HorizontalAlignment = HorizontalAlignType.Left;
//                sheet.Range["C12:D12"].Merge();

//                sheet.Range["E12:F12"].Text = "医院单例价格：";
//                sheet.Range["E12:F12"].Merge();

//                sheet.Range["G12:K12"].NumberValue = data.YiYuanHeTongFeiZongE.AvgPrice;
//                sheet.Range["G12:K12"].Merge();
//                sheet.Range["A12:L12"].Style.Font.IsBold = true;
//                sheet.Range["A12:L12"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["A12:L12"].Style.HorizontalAlignment = HorizontalAlignType.Left;


//                sheet.Range["A13"].Text = "费用项目";
//                sheet.Range["B13"].Text = "费用明细";
//                sheet.Range["C13"].Text = "单价";
//                sheet.Range["D13"].Text = "频次\r\n（中心数或例数）";
//                sheet.Range["E13:K13"].Text = "填写说明";
//                sheet.Range["E13:K13"].Merge();
//                sheet.Range["L13"].Text = "合计";
//                sheet.Range["L13"].Style.Font.IsBold = true;
//                sheet.Range["A13:L13"].RowHeight = 25;


//                sheet.Range["A14:A19"].Text = "合同费用";
//                sheet.Range["A14:A19"].Merge();


//                //------------------
//                sheet.Range["B14"].Text = "组长费";
//                sheet.Range["C14"].NumberValue = data.YiYuanHeTongFeiZongE.ZuZhangFei.Price;
//                sheet.Range["D14"].NumberValue = data.YiYuanHeTongFeiZongE.ZuZhangFei.Count;
//                sheet.Range["E14:K14"].Text = "依据不同项目类型（或拟选机构要求）而定，结合商务计划书。";
//                sheet.Range["E14:K14"].Merge();
//                sheet.Range["L14"].NumberValue = data.YiYuanHeTongFeiZongE.ZuZhangFei.HeJi;


//                //------------------
//                sheet.Range["B15"].Text = "机构管理费";
//                sheet.Range["C15"].NumberValue = data.YiYuanHeTongFeiZongE.JiGouGuanLiFei.Price;
//                sheet.Range["D15"].NumberValue = data.YiYuanHeTongFeiZongE.JiGouGuanLiFei.Count;
//                sheet.Range["E15:K15"].Text = "依据不同项目类型（或拟选机构要求）而定，结合商务计划书与确定中心数。含机构管理、药物管理、CRC管理、质控管理、文档管理等费用。";
//                sheet.Range["E15:K15"].Merge();
//                sheet.Range["L15"].NumberValue = data.YiYuanHeTongFeiZongE.JiGouGuanLiFei.HeJi;
//                sheet.Range["A15:L15"].RowHeight = 27;

//                //------------------
//                sheet.Range["B16"].Text = "伦理费";
//                sheet.Range["C16"].NumberValue = data.YiYuanHeTongFeiZongE.LunLiFei.Price;
//                sheet.Range["D16"].NumberValue = data.YiYuanHeTongFeiZongE.LunLiFei.Count;
//                sheet.Range["E16:K16"].Text = "按10000元/次标准，结合中心数*2倍=总次数（考虑重审及结题审查）。";
//                sheet.Range["E16:K16"].Merge();
//                sheet.Range["L16"].NumberValue = data.YiYuanHeTongFeiZongE.LunLiFei.HeJi;

//                //------------------
//                sheet.Range["B17"].Text = "合格病例费用";
//                sheet.Range["C17"].NumberValue = data.YiYuanHeTongFeiZongE.HeGeBingLiFei.Price;
//                sheet.Range["D17"].NumberValue = data.YiYuanHeTongFeiZongE.HeGeBingLiFei.Count;
//                sheet.Range["E17:K17"].Text = "观察费和检查费(指合同签署支付医院，包括研究者劳务费、检查费、床位费、餐费等)。";
//                sheet.Range["E17:K17"].Merge();
//                sheet.Range["L17"].NumberValue = data.YiYuanHeTongFeiZongE.HeGeBingLiFei.HeJi;

//                //------------------
//                sheet.Range["B18"].Text = "筛选病例费";
//                sheet.Range["C18"].NumberValue = data.YiYuanHeTongFeiZongE.ShaiXuanBingLiFei.Price;
//                sheet.Range["D18"].NumberValue = data.YiYuanHeTongFeiZongE.ShaiXuanBingLiFei.Count;
//                sheet.Range["E18:K18"].Text = "依据不同项目的筛败工作量大小和筛选失败率来确定。预计筛败率为10%。";
//                sheet.Range["E18:K18"].Merge();
//                sheet.Range["L18"].NumberValue = data.YiYuanHeTongFeiZongE.ShaiXuanBingLiFei.HeJi;

//                //------------------
//                sheet.Range["B19"].Text = "其他费用";
//                sheet.Range["C19"].NumberValue = data.YiYuanHeTongFeiZongE.QiTaFei.Price;
//                sheet.Range["D19"].NumberValue = data.YiYuanHeTongFeiZongE.QiTaFei.Count;
//                sheet.Range["E19:K19"].Text = "指受试者补偿、AE随访费等费用。";
//                sheet.Range["E19:K19"].Merge();
//                sheet.Range["L19"].NumberValue = data.YiYuanHeTongFeiZongE.QiTaFei.HeJi;


//                sheet.Range["A20:B20"].Text = "合   计：";
//                sheet.Range["A20:B20"].Merge();
//                sheet.Range["A20:B20"].Style.Font.IsBold = true;
//                sheet.Range["L20"].Style.Font.IsBold = true;
//                sheet.Range["L20"].NumberValue = data.YiYuanHeTongFeiZongE.TotalAmount;
//                sheet.Range["E20:K20"].Merge();


//                sheet.Range["A13:L13"].Style.KnownColor = ExcelColors.Gray25Percent;
//                sheet.Range["A14:B20"].Style.KnownColor = ExcelColors.Gray25Percent;

//                sheet.Range["C15:D20"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["E20:L20"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["L14:L20"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["C16"].Style.KnownColor = ExcelColors.White;


//                sheet.Range["C14:L20"].Style.HorizontalAlignment = HorizontalAlignType.Left;
//                #endregion



//                #region row5 三、其它杂费总额(RMB)：

//                sheet.Range["A21:B21"].Text = "三、其它杂费总额(RMB)：";
//                sheet.Range["A21:B21"].Merge();

//                sheet.Range["C21:L21"].NumberValue = data.QiTaZaFeiZongE.TotalAmount;
//                sheet.Range["C21:L21"].Style.HorizontalAlignment = HorizontalAlignType.Left;
//                sheet.Range["C21:L21"].Merge();
//                sheet.Range["A21:L21"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["A21:L21"].Style.Font.IsBold = true;


//                sheet.Range["A22"].Text = "费用项目";
//                sheet.Range["B22"].Text = "费用明细";
//                sheet.Range["C22"].Text = "单价";
//                sheet.Range["D22"].Text = "频次\r\n（中心数或例数）";
//                sheet.Range["E22:K22"].Text = "填写说明";
//                sheet.Range["E22:K22"].Merge();
//                sheet.Range["L22"].Text = "合计";
//                sheet.Range["L22"].Style.Font.IsBold = true;
//                sheet.Range["A22:L22"].RowHeight = 25;

//                sheet.Range["A23:A38"].Text = "其他费用";
//                sheet.Range["A23:A38"].Merge();

//                //------------------
//                sheet.Range["B23"].Text = "启动会费";
//                sheet.Range["C23"].NumberValue = data.QiTaZaFeiZongE.QiDongHuiFei.Price;
//                sheet.Range["D23"].NumberValue = data.QiTaZaFeiZongE.QiDongHuiFei.Count;
//                sheet.Range["E23:K23"].Text = "依据中心数确定，通常每中心2000元。";
//                sheet.Range["E23:K23"].Merge();
//                sheet.Range["L23"].NumberValue = data.QiTaZaFeiZongE.QiDongHuiFei.HeJi;

//                //------------------
//                sheet.Range["B24"].Text = "运输费";
//                sheet.Range["C24"].NumberValue = data.QiTaZaFeiZongE.YunShuFei.Price;
//                sheet.Range["D24"].NumberValue = data.QiTaZaFeiZongE.YunShuFei.Count;
//                sheet.Range["E24:K24"].Text = "依据中心数确定，通常每中心2000元。需根据具体项目要求来预算。";
//                sheet.Range["E24:K24"].Merge();
//                sheet.Range["L24"].NumberValue = data.QiTaZaFeiZongE.YunShuFei.HeJi;

//                //------------------
//                sheet.Range["B25"].Text = "监查差旅费";
//                sheet.Range["C25"].NumberValue = data.QiTaZaFeiZongE.JianChaChaiLvFei.Price;
//                sheet.Range["D25"].NumberValue = data.QiTaZaFeiZongE.JianChaChaiLvFei.Count;
//                sheet.Range["E25:K25"].Text = "见下表计算方法。";
//                sheet.Range["E25:K25"].Merge();
//                sheet.Range["L25"].NumberValue = data.QiTaZaFeiZongE.JianChaChaiLvFei.HeJi;

//                //------------------
//                sheet.Range["B26"].Text = "试剂、耗材费";
//                sheet.Range["C26"].NumberValue = data.QiTaZaFeiZongE.ShiJiHaoCaiFei.Price;
//                sheet.Range["D26"].NumberValue = data.QiTaZaFeiZongE.ShiJiHaoCaiFei.Count;
//                sheet.Range["E26:K26"].Text = "依据合同计划书，结合项目和选择医院的情况确定。如签在医院合中，则预算在上述第二部分中。";
//                sheet.Range["E26:K26"].Merge();
//                sheet.Range["L26"].NumberValue = data.QiTaZaFeiZongE.ShiJiHaoCaiFei.HeJi;
//                sheet.Range["A26:L26"].RowHeight = 27;

//                //------------------
//                sheet.Range["B27"].Text = "中心实验室";
//                sheet.Range["C27"].NumberValue = data.QiTaZaFeiZongE.ZhongXinShiYanShiFei.Price;
//                sheet.Range["D27"].NumberValue = data.QiTaZaFeiZongE.ZhongXinShiYanShiFei.Count;
//                sheet.Range["E27:K27"].Text = "中心阅片按照800-1200/例。";
//                sheet.Range["E27:K27"].Merge();
//                sheet.Range["L27"].NumberValue = data.QiTaZaFeiZongE.ZhongXinShiYanShiFei.HeJi;
//                sheet.Range["B27:L27"].Style.Font.IsBold = true;

//                //------------------
//                sheet.Range["B28"].Text = "数据、统计费";
//                sheet.Range["C28"].NumberValue = data.QiTaZaFeiZongE.ShuJuTongJiFei.Price;
//                sheet.Range["D28"].NumberValue = data.QiTaZaFeiZongE.ShuJuTongJiFei.Count;
//                sheet.Range["E28:K28"].Text = "依据合同计划书确定。";
//                sheet.Range["E28:K28"].Merge();
//                sheet.Range["L28"].NumberValue = data.QiTaZaFeiZongE.ShuJuTongJiFei.HeJi;
//                sheet.Range["B28:L28"].Style.Font.IsBold = true;

//                //------------------
//                sheet.Range["B29"].Text = "系统使用费";
//                sheet.Range["C29"].NumberValue = data.QiTaZaFeiZongE.XiTongShiYongFei.Price;
//                sheet.Range["D29"].NumberValue = data.QiTaZaFeiZongE.XiTongShiYongFei.Count;
//                sheet.Range["E29:K29"].Text = "按需选择，EDC、IWRS、ePRO等相关系统租用费。";
//                sheet.Range["E29:K29"].Merge();
//                sheet.Range["L29"].NumberValue = data.QiTaZaFeiZongE.XiTongShiYongFei.HeJi;

//                //------------------
//                sheet.Range["B30"].Text = "印刷费";
//                sheet.Range["C30"].NumberValue = data.QiTaZaFeiZongE.YinShuaFei.Price;
//                sheet.Range["D30"].NumberValue = data.QiTaZaFeiZongE.YinShuaFei.Count;
//                sheet.Range["E30:K30"].Text = "依据中心数确定，通常每中心3000元。";
//                sheet.Range["E30:K30"].Merge();
//                sheet.Range["L30"].NumberValue = data.QiTaZaFeiZongE.YinShuaFei.HeJi;

//                //------------------
//                sheet.Range["B31"].Text = "其他采购";
//                sheet.Range["C31"].NumberValue = data.QiTaZaFeiZongE.QiTaCaiGouFei.Price;
//                sheet.Range["D31"].NumberValue = data.QiTaZaFeiZongE.QiTaCaiGouFei.Count;
//                sheet.Range["E31:K31"].Text = "依据合同计划书确定（根据试验需要采购的冰箱、温度计、离心机等设备）。";
//                sheet.Range["E31:K31"].Merge();
//                sheet.Range["L31"].NumberValue = data.QiTaZaFeiZongE.QiTaCaiGouFei.HeJi;

//                //------------------
//                sheet.Range["B32"].Text = "受试者招募费";
//                sheet.Range["C32"].NumberValue = data.QiTaZaFeiZongE.ShouShiZheZhaoMuFei.Price;
//                sheet.Range["D32"].NumberValue = data.QiTaZaFeiZongE.ShouShiZheZhaoMuFei.Count;
//                sheet.Range["E32:K32"].Text = "依据合同计划书和项目实际情况进行预算，包括委外招募和研究过程协调促进等费用。";
//                sheet.Range["E32:K32"].Merge();
//                sheet.Range["L32"].NumberValue = data.QiTaZaFeiZongE.ShouShiZheZhaoMuFei.HeJi;

//                //------------------
//                sheet.Range["B33:B34"].Text = "稽查费";
//                sheet.Range["B33:B34"].Merge();

//                sheet.Range["C33"].NumberValue = data.QiTaZaFeiZongE.ShengNeiJiChaFei.Price;
//                sheet.Range["D33"].NumberValue = data.QiTaZaFeiZongE.ShengNeiJiChaFei.Count;
//                sheet.Range["E33:K33"].Text = "根据合同计划书约定范围进行计算，省内稽查按15000元/次。";
//                sheet.Range["E33:K33"].Merge();
//                sheet.Range["L33"].NumberValue = data.QiTaZaFeiZongE.ShengNeiJiChaFei.HeJi;

//                sheet.Range["C34"].NumberValue = data.QiTaZaFeiZongE.ShengWaiJiChaFei.Price;
//                sheet.Range["D34"].NumberValue = data.QiTaZaFeiZongE.ShengWaiJiChaFei.Count;
//                sheet.Range["E34:K34"].Text = "根据合同计划书约定范围进行计算，出差省外稽查按20000元/次。";
//                sheet.Range["E34:K34"].Merge();
//                sheet.Range["L33:L34"].NumberValue = data.QiTaZaFeiZongE.ShengNeiJiChaFei.HeJi + data.QiTaZaFeiZongE.ShengWaiJiChaFei.HeJi;
//                sheet.Range["L33:L34"].Merge();
//                //------------------
//                sheet.Range["B35"].Text = "SMO费用";
//                sheet.Range["C35"].NumberValue = data.QiTaZaFeiZongE.SMOFei.Price;
//                sheet.Range["D35"].NumberValue = data.QiTaZaFeiZongE.SMOFei.Count;
//                sheet.Range["E35:K35"].Text = "依据合同计划书确定。";
//                sheet.Range["E35:K35"].Merge();
//                sheet.Range["L35"].NumberValue = data.QiTaZaFeiZongE.SMOFei.HeJi;
//                sheet.Range["B35:L35"].Style.Font.IsBold = true;

//                //------------------
//                sheet.Range["B36"].Text = "委外监查服务";
//                sheet.Range["C36"].NumberValue = data.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.Price;
//                sheet.Range["D36"].NumberValue = data.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.Count;
//                sheet.Range["E36:K36"].Text = "按附表2进行测算。将预计委托外部公司的病例数进行拆分计算。";
//                sheet.Range["E36:K36"].Merge();
//                sheet.Range["L36"].NumberValue = data.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.HeJi;
//                sheet.Range["B26:L26"].Style.Font.IsBold = true;

//                //------------------
//                sheet.Range["B37"].Text = "遗传办填报";
//                sheet.Range["C37"].NumberValue = data.QiTaZaFeiZongE.YiChuanBanTianBaoFei.Price;
//                sheet.Range["D37"].NumberValue = data.QiTaZaFeiZongE.YiChuanBanTianBaoFei.Count;
//                sheet.Range["E37:K37"].Text = "按需选择。5家中心以内3W，中心增加可视情况增加";
//                sheet.Range["E37:K37"].Merge();
//                sheet.Range["L37"].NumberValue = data.QiTaZaFeiZongE.YiChuanBanTianBaoFei.HeJi;

//                //------------------
//                sheet.Range["B38"].Text = "其他费用";
//                sheet.Range["C38"].NumberValue = data.QiTaZaFeiZongE.QiTaFei.Price;
//                sheet.Range["D38"].NumberValue = data.QiTaZaFeiZongE.QiTaFei.Count;
//                sheet.Range["E38:K38"].Text = "指无法确定的费用，按中心预留，小包通常每中心2000元。大包根据项目难度确定。";
//                sheet.Range["E38:K38"].Merge();
//                sheet.Range["L38"].NumberValue = data.QiTaZaFeiZongE.QiTaFei.HeJi;


//                sheet.Range["A39:B39"].Text = "合   计：";
//                sheet.Range["A39:B39"].Merge();
//                sheet.Range["L39"].NumberValue = data.QiTaZaFeiZongE.TotalAmount;

//                sheet.Range["E39:K39"].Merge();
//                sheet.Range["A39:B39"].Style.Font.IsBold = true;
//                sheet.Range["L39"].Style.Font.IsBold = true;
//                sheet.Range["L39"].NumberValue = data.QiTaZaFeiZongE.TotalAmount;


//                sheet.Range["A22:L22"].Style.KnownColor = ExcelColors.Gray25Percent;
//                sheet.Range["A23:B39"].Style.KnownColor = ExcelColors.Gray25Percent;

//                sheet.Range["C25"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["D23:D25"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["D27:D30"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["D35"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["L23:L39"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["C39:L39"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["C23:L39"].Style.HorizontalAlignment = HorizontalAlignType.Left;
//                #endregion


//                #region row6 四、人工费用总额(RMB)：

//                sheet.Range["A40:B40"].Text = "四、人工费用总额(RMB)：";
//                sheet.Range["A40:B40"].Merge();

//                sheet.Range["C40:L40"].NumberValue = data.RenGongFeiYongZongE.TotalAmount;
//                sheet.Range["C40:L40"].Style.HorizontalAlignment = HorizontalAlignType.Left;
//                sheet.Range["C40:L40"].Merge();
//                sheet.Range["A40:L40"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["A40:L40"].Style.Font.IsBold = true;


//                sheet.Range["A41"].Text = "费用项目";
//                sheet.Range["B41"].Text = "费用明细";
//                sheet.Range["C41"].Text = "单价";
//                sheet.Range["D41"].Text = "频次\r\n（中心数或例数）";
//                sheet.Range["E41:K41"].Text = "填写说明";
//                sheet.Range["E41:K41"].Merge();
//                sheet.Range["L41"].Text = "合计";
//                sheet.Range["L41"].Style.Font.IsBold = true;
//                sheet.Range["A41:L41"].RowHeight = 25;

//                sheet.Range["A42:A52"].Text = "人工费用";
//                sheet.Range["A42:A52"].Merge();

//                //------------------
//                sheet.Range["B42"].Text = "管理人员";
//                sheet.Range["C42"].NumberValue = data.RenGongFeiYongZongE.GuanLiRenYuanFei.Price;
//                sheet.Range["D42"].NumberValue = data.RenGongFeiYongZongE.GuanLiRenYuanFei.Count;
//                sheet.Range["E42:K42"].Text = "简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。";
//                sheet.Range["E42:K42"].Merge();
//                sheet.Range["L42"].NumberValue = data.RenGongFeiYongZongE.GuanLiRenYuanFei.HeJi;

//                //------------------
//                sheet.Range["B43"].Text = "项目经理（PM）";
//                sheet.Range["C43"].NumberValue = data.RenGongFeiYongZongE.PMFei.Price;
//                sheet.Range["D43"].NumberValue = data.RenGongFeiYongZongE.PMFei.Count;
//                sheet.Range["E43:K43"].Text = "依据项目总时限和项目经理平均承担项目数量，基准为8000元-15000元/月*项目总时长（月）。";
//                sheet.Range["E43:K43"].Merge();
//                sheet.Range["L43"].NumberValue = data.RenGongFeiYongZongE.PMFei.HeJi;

//                //------------------
//                sheet.Range["B44"].Text = "项目组长（PL）";
//                sheet.Range["C44"].NumberValue = data.RenGongFeiYongZongE.PLFei.Price;
//                sheet.Range["D44"].NumberValue = data.RenGongFeiYongZongE.PLFei.Count;
//                sheet.Range["E44:K44"].Text = "依据项目总时限和项目备配的情况，基准为6000元-10000元/月*项目总时长（月）。";
//                sheet.Range["E44:K44"].Merge();
//                sheet.Range["L44"].NumberValue = data.RenGongFeiYongZongE.PLFei.HeJi;

//                //------------------
//                sheet.Range["B45"].Text = "项目助理（CTA）";
//                sheet.Range["C45"].NumberValue = data.RenGongFeiYongZongE.CTAFei.Price;
//                sheet.Range["D45"].NumberValue = data.RenGongFeiYongZongE.CTAFei.Count;
//                sheet.Range["E45:K45"].Text = "依据项目总时限和项目备配的情况，基准为3000元-5000元/月*项目总时长（月）。";
//                sheet.Range["E45:K45"].Merge();
//                sheet.Range["L45"].NumberValue = data.RenGongFeiYongZongE.CTAFei.HeJi;

//                //------------------
//                sheet.Range["B46"].Text = "监查服务（CRA）大临床";
//                sheet.Range["C46"].NumberValue = data.RenGongFeiYongZongE.CRAFei.Price;
//                sheet.Range["D46"].NumberValue = data.RenGongFeiYongZongE.CRAFei.Count;
//                sheet.Range["E46:K46"].Text = "按附表2进行测算。";
//                sheet.Range["E46:K46"].Merge();
//                sheet.Range["L46"].NumberValue = data.RenGongFeiYongZongE.CRAFei.HeJi;

//                //------------------
//                sheet.Range["B47"].Text = "协同监查";
//                sheet.Range["C47"].NumberValue = data.RenGongFeiYongZongE.XieTongJianChaFei.Price;
//                sheet.Range["D47"].NumberValue = data.RenGongFeiYongZongE.XieTongJianChaFei.Count;
//                sheet.Range["E47:K47"].Text = "包括PM、LM、QC三种角色的协同访视，通常每中心按2次计算。";
//                sheet.Range["E47:K47"].Merge();
//                sheet.Range["L47"].NumberValue = data.RenGongFeiYongZongE.XieTongJianChaFei.HeJi;

//                //------------------
//                sheet.Range["B48"].Text = "医学支持（撰写）";
//                sheet.Range["C48"].NumberValue = data.RenGongFeiYongZongE.YiXueZhiChi_ZhuanXieFei.Price;
//                sheet.Range["D48"].NumberValue = data.RenGongFeiYongZongE.YiXueZhiChi_ZhuanXieFei.Count;
//                sheet.Range["E48:K48"].Text = "简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。";
//                sheet.Range["E48:K48"].Merge();
//                sheet.Range["L48"].NumberValue = data.RenGongFeiYongZongE.YiXueZhiChi_ZhuanXieFei.HeJi;

//                //------------------
//                sheet.Range["B49"].Text = "医学支持（监查）";
//                sheet.Range["C49"].NumberValue = data.RenGongFeiYongZongE.YiXueZhiChi_JianChaFei.Price;
//                sheet.Range["D49"].NumberValue = data.RenGongFeiYongZongE.YiXueZhiChi_JianChaFei.Count;
//                sheet.Range["E49:K49"].Text = "每次按10000元计算。";
//                sheet.Range["E49:K49"].Merge();
//                sheet.Range["L49"].NumberValue = data.RenGongFeiYongZongE.YiXueZhiChi_JianChaFei.HeJi;

//                //------------------
//                sheet.Range["B50"].Text = "质控支持";
//                sheet.Range["C50"].NumberValue = data.RenGongFeiYongZongE.ZhiKongZhiChiFei.Price;
//                sheet.Range["D50"].NumberValue = data.RenGongFeiYongZongE.ZhiKongZhiChiFei.Count;
//                sheet.Range["E50:K50"].Text = "简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。";
//                sheet.Range["E50:K50"].Merge();
//                sheet.Range["L50"].NumberValue = data.RenGongFeiYongZongE.ZhiKongZhiChiFei.HeJi;

//                //------------------
//                sheet.Range["B51"].Text = "PV支持";
//                sheet.Range["C51"].NumberValue = data.RenGongFeiYongZongE.PVZhiChiFei.Price;
//                sheet.Range["D51"].NumberValue = data.RenGongFeiYongZongE.PVZhiChiFei.Count;
//                sheet.Range["E51:K51"].Text = "需要根据项目合同和计划书确定。";
//                sheet.Range["E51:K51"].Merge();
//                sheet.Range["L51"].NumberValue = data.RenGongFeiYongZongE.PVZhiChiFei.HeJi;

//                //------------------
//                sheet.Range["B52"].Text = "项目奖励";
//                sheet.Range["C52"].NumberValue = data.RenGongFeiYongZongE.XiangMuJiangLiFei.Price;
//                sheet.Range["D52"].NumberValue = data.RenGongFeiYongZongE.XiangMuJiangLiFei.Count;
//                sheet.Range["E52:K52"].Text = "合同系数：＜200W，0.4；200-399W，0.5；400-599W，0.6；600-899W，0.7；900-1199W，0.8；1200-1500W，0.9。";
//                sheet.Range["E52:K52"].Merge();
//                sheet.Range["L52"].NumberValue = data.RenGongFeiYongZongE.XiangMuJiangLiFei.HeJi;

//                sheet.Range["A53:B53"].Text = "合   计：";
//                sheet.Range["A53:B53"].Merge();
//                sheet.Range["L53"].NumberValue = data.RenGongFeiYongZongE.TotalAmount;

//                sheet.Range["E53:K53"].Merge();
//                sheet.Range["A53:B53"].Style.Font.IsBold = true;
//                sheet.Range["L53"].Style.Font.IsBold = true;
//                sheet.Range["L53"].NumberValue = data.RenGongFeiYongZongE.TotalAmount;

//                sheet.Range["A41:L41"].Style.KnownColor = ExcelColors.Gray25Percent;
//                sheet.Range["A42:B53"].Style.KnownColor = ExcelColors.Gray25Percent;

//                sheet.Range["C46"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["D42:D50"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["D52"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["L42:L53"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["C53:L53"].Style.KnownColor = ExcelColors.Yellow;
//                sheet.Range["C42:L53"].Style.HorizontalAlignment = HorizontalAlignType.Left;

//                #endregion

//                #region row7 预算费用总金额(RMB)：

//                sheet.Range["A54:B54"].Text = "预算费用总金额(RMB)：";
//                sheet.Range["A54:B54"].Merge();
//                sheet.Range["A54:B54"].Style.Font.IsBold = true;

//                sheet.Range["C54"].NumberValue = data.TotalAmount;

//                sheet.Range["E54:K54"].Merge();
//                sheet.Range["A54:C54"].Style.KnownColor = ExcelColors.Yellow;

//                sheet.Range["A55:L55"].Text = "注：标黄色的单元格已设定公式，不能修改。";
//                sheet.Range["A55:L55"].Merge();
//                sheet.Range["A55:L55"].RowHeight = 40;
//                sheet.Range["A55:L55"].Style.HorizontalAlignment = HorizontalAlignType.Left;
//                sheet.Range["A55:L55"].Style.Font.Color = Color.Red;
//                #endregion


//                #endregion


//                #region 表2，监查服务成本预算（大临床）

//                Worksheet sheet2 = wbToStream.Worksheets[1];
//                sheet2.Name = "表2，监查服务成本预算（大临床）";

//                CellRange range2 = sheet2.Range["A1:G24"];
//                range2.BorderAround(LineStyleType.Thin);
//                range2.BorderInside(LineStyleType.Thin);
//                range2.Style.VerticalAlignment = VerticalAlignType.Center;//垂直居中
//                                                                          //range2.Style.HorizontalAlignment = HorizontalAlignType.Left; //  
//                range2.Style.WrapText = true;
//                range2.IsWrapText = true;
//                range2.RowHeight = 19.5;
//                range2.Style.Font.Size = 10;
//                range2.Style.Font.FontName = "宋体";

//                sheet2.Range["A1:A23"].Style.Font.IsBold = true;
//                sheet2.Range["A12:G12"].Style.Font.IsBold = true;
//                sheet2.Range["F13:F23"].Style.Font.IsBold = true;

//                sheet2.Range["E13:E21"].Style.KnownColor = ExcelColors.Orange;
//                sheet2.Range["F13:F23"].Style.KnownColor = ExcelColors.Gray25Percent;
//                sheet2.Range["A22:F23"].Style.KnownColor = ExcelColors.Yellow;

//                sheet2.Columns[0].ColumnWidth = 15;
//                sheet2.Columns[1].ColumnWidth = 19;
//                sheet2.Columns[2].ColumnWidth = 18;
//                sheet2.Columns[3].ColumnWidth = 11;
//                sheet2.Columns[6].ColumnWidth = 19;

//                sheet2.Range["B2:G9"].Style.HorizontalAlignment = HorizontalAlignType.Left;

//                #region row1

//                sheet2.Range["A1:G1"].Text = "监查服务成本预算";
//                sheet2.Range["A1:G1"].Merge();
//                sheet2.Range["A1:G1"].Style.HorizontalAlignment = HorizontalAlignType.Center;
//                //-------------------
//                sheet2.Range["A2"].Text = "项目名称";
//                sheet2.Range["B2:G2"].Text = data.ProjectName ?? "";
//                sheet2.Range["B2:G2"].Merge();

//                //-------------------
//                sheet2.Range["A3"].Text = "申办方";
//                sheet2.Range["B3:G3"].Text = data.CompanyName ?? "";
//                sheet2.Range["B3:G3"].Merge();

//                //-------------------
//                sheet2.Range["A4"].Text = "总样本量";
//                sheet2.Range["B4:G4"].NumberValue = data.ZongBingLiShu;
//                sheet2.Range["B4:G4"].Merge();

//                //-------------------
//                sheet2.Range["A5"].Text = "适应症";
//                sheet2.Range["B5:G5"].Text = data.ShiYingZheng ?? "";
//                sheet2.Range["B5:G5"].Merge();

//                //-------------------
//                sheet2.Range["A6"].Text = "随访次数";
//                sheet2.Range["B6:G6"].NumberValue = data.SuiFang;
//                sheet2.Range["B6:G6"].Merge();

//                //-------------------
//                sheet2.Range["A7"].Text = "计划筛选中心数";
//                sheet2.Range["B7:G7"].NumberValue = data.JiHuaShuaiXuanZhongXinShu;
//                sheet2.Range["B7:G7"].Merge();

//                //-------------------
//                sheet2.Range["A8"].Text = "计划确定中心数";
//                sheet2.Range["B8:G8"].NumberValue = data.QueDingZhongXinShu;
//                sheet2.Range["B8:G8"].Merge();

//                //-------------------
//                sheet2.Range["A9"].Text = "合同约定研究总时限（月）";
//                sheet2.Range["B9:G9"].NumberValue = data.ZongShiXian;
//                sheet2.Range["B9:G9"].Merge();
//                sheet2.Range["A9:G9"].RowHeight = 27;

//                //-------------------


//                sheet2.Range["A10"].Text = "难度系数";//文字需要根据难度选中变化
//                                                  // sheet2.Range["B10:G10"].RichText = "简单级1.0；普通级1.14；稍难级1.3；困难级1.54；特别级2.0";
//                sheet2.Range["B10:G10"].Merge();
//                IRichTextString richText = sheet2.Range["B10:G10"].RichText;
//                richText.Text = "简单级1.0；普通级1.14；稍难级1.3；困难级1.54；特别级2.0";
//                IFont font = wbToStream.CreateFont();
//                font.Color = Color.Red;
//                if (data.NamDu == ENanDuXiShu.简单级)
//                {
//                    richText.SetFont(0, 6, font);
//                }
//                else if (data.NamDu == ENanDuXiShu.普通级)
//                {
//                    richText.SetFont(7, 14, font);
//                }
//                else if (data.NamDu == ENanDuXiShu.稍难级)
//                {
//                    richText.SetFont(15, 21, font);
//                }
//                else if (data.NamDu == ENanDuXiShu.困难级)
//                {
//                    richText.SetFont(22, 29, font);
//                }
//                else if (data.NamDu == ENanDuXiShu.特别级)
//                {
//                    richText.SetFont(30, 35, font);
//                }

//                //-------------------
//                sheet2.Range["A11"].Text = "特别系数";//文字需要根据特别选中变化
//                sheet2.Range["B11:G11"].Text = "无特殊1.0；上市后评价随机0.5-0.9；上市后评价开放0.3-0.6；BE研究0.5";
//                sheet2.Range["B11:G11"].Merge();

//                //-------------------
//                sheet2.Range["A12"].Text = "序号";
//                sheet2.Range["B12"].Text = "主要工作条目";
//                sheet2.Range["C12"].Text = "分类计算标准";
//                sheet2.Range["D12"].Text = "单价（元/中心或元/例）";
//                sheet2.Range["E12"].Text = "数量（中心或例）";
//                sheet2.Range["F12"].Text = "预算小计（元）";
//                sheet2.Range["G12"].Text = "填写备注说明";
//                sheet2.Range["A12:G12"].RowHeight = 25;
//                //-------------------
//                sheet2.Range["A13:A14"].Text = "1";
//                sheet2.Range["A13:A14"].Merge();
//                sheet2.Range["B13:B14"].Text = "准备阶段服务费（包括调研、立项、伦理、协助协议、启动前准备完成等工作）";
//                sheet2.Range["B13:B14"].Merge();
//                sheet2.Range["C13"].Text = "简单级、普通级项目";
//                sheet2.Range["D13"].NumberValue = 5000;
//                sheet2.Range["E13"].NumberValue = (data.NamDu == ENanDuXiShu.简单级 || data.NamDu == ENanDuXiShu.普通级) ? data.QueDingZhongXinShu : 0;
//                sheet2.Range["F13"].NumberValue = (data.NamDu == ENanDuXiShu.简单级 || data.NamDu == ENanDuXiShu.普通级) ? data.QueDingZhongXinShu * 5000 : 0;
//                sheet2.Range["A14:G14"].RowHeight = 34;


//                sheet2.Range["C14"].Text = "稍难级、困难级、特别级项目";
//                sheet2.Range["D14"].NumberValue = 8000;
//                sheet2.Range["E14"].NumberValue = (data.NamDu == ENanDuXiShu.稍难级 || data.NamDu == ENanDuXiShu.困难级 || data.NamDu == ENanDuXiShu.特别级) ? data.QueDingZhongXinShu : 0;
//                sheet2.Range["F14"].NumberValue = 0;

//                sheet2.Range["G13:G14"].Text = "按预计筛选中心数计算";
//                sheet2.Range["G13:G14"].Merge();


//                //-------------------


//                sheet2.Range["A15:A19"].Text = "2";
//                sheet2.Range["A15:A19"].Merge();
//                sheet2.Range["B15:B19"].Text = "每例病例监查服务费";
//                sheet2.Range["B15:B19"].Merge();
//                sheet2.Range["C15"].Text = "简单级项目（1000）";
//                sheet2.Range["D15"].NumberValue = 1000;
//                sheet2.Range["E15"].NumberValue = data.NamDu == ENanDuXiShu.简单级 ? data.ZongBingLiShu : 0;
//                sheet2.Range["F15"].NumberValue = data.NamDu == ENanDuXiShu.简单级 ? data.ZongBingLiShu * 1000 : 0; ;

//                sheet2.Range["C16"].Text = "普通级项目（1500）";
//                sheet2.Range["D16"].NumberValue = 1500;
//                sheet2.Range["E16"].NumberValue = data.NamDu == ENanDuXiShu.普通级 ? data.ZongBingLiShu : 0;
//                sheet2.Range["F16"].NumberValue = data.NamDu == ENanDuXiShu.普通级 ? data.ZongBingLiShu * 1500 : 0;

//                sheet2.Range["C17"].Text = "稍难级项目（3000）";
//                sheet2.Range["D17"].NumberValue = 3000;
//                sheet2.Range["E17"].NumberValue = data.NamDu == ENanDuXiShu.稍难级 ? data.ZongBingLiShu : 0;
//                sheet2.Range["F17"].NumberValue = data.NamDu == ENanDuXiShu.稍难级 ? data.ZongBingLiShu * 3000 : 0;

//                sheet2.Range["C18"].Text = "困难级项目（4000）";
//                sheet2.Range["D18"].NumberValue = 4000;
//                sheet2.Range["E18"].NumberValue = data.NamDu == ENanDuXiShu.困难级 ? data.ZongBingLiShu : 0;
//                sheet2.Range["F18"].NumberValue = data.NamDu == ENanDuXiShu.困难级 ? data.ZongBingLiShu * 4000 : 0;

//                sheet2.Range["C19"].Text = "特别级项目（7000）";
//                sheet2.Range["D19"].NumberValue = 7000;
//                sheet2.Range["E19"].NumberValue = data.NamDu == ENanDuXiShu.特别级 ? data.ZongBingLiShu : 0;
//                sheet2.Range["F19"].NumberValue = data.NamDu == ENanDuXiShu.特别级 ? data.ZongBingLiShu * 7000 : 0;


//                sheet2.Range["G15:G19"].Text = "可根据实际项目情况上下浮动调整20%";
//                sheet2.Range["G15:G19"].Merge();

//                //-------------------

//                sheet2.Range["A20:A21"].Text = "3";
//                sheet2.Range["A20:A21"].Merge();
//                sheet2.Range["B20:B21"].Text = "项目关闭中心（包括资料回收、答疑、归档、小结、总结盖章）";
//                sheet2.Range["B20:B21"].Merge();
//                sheet2.Range["C20"].Text = "（简单级、普通级24例以内；稍难级、困难级、特别级12例及以内）";
//                sheet2.Range["D20"].NumberValue = 5000;
//                sheet2.Range["E20"].NumberValue = (((data.NamDu == ENanDuXiShu.简单级 || data.NamDu == ENanDuXiShu.普通级) && data.ZongBingLiShu <= 24) || ((data.NamDu == ENanDuXiShu.稍难级 || data.NamDu == ENanDuXiShu.困难级 || data.NamDu == ENanDuXiShu.特别级) && data.ZongBingLiShu <= 12)) ? data.QueDingZhongXinShu : 0;
//                sheet2.Range["F20"].NumberValue = (((data.NamDu == ENanDuXiShu.简单级 || data.NamDu == ENanDuXiShu.普通级) && data.ZongBingLiShu <= 24) || ((data.NamDu == ENanDuXiShu.稍难级 || data.NamDu == ENanDuXiShu.困难级 || data.NamDu == ENanDuXiShu.特别级) && data.ZongBingLiShu <= 12)) ? data.QueDingZhongXinShu * 5000 : 0;
//                sheet2.Range["A20:G20"].RowHeight = 57;

//                sheet2.Range["C21"].Text = "（简单级、普通级24例以上；稍难级、困难级、特别级12例及以上）";
//                sheet2.Range["D21"].NumberValue = 8000;
//                sheet2.Range["E21"].NumberValue = (((data.NamDu == ENanDuXiShu.简单级 || data.NamDu == ENanDuXiShu.普通级) && data.ZongBingLiShu > 24) || ((data.NamDu == ENanDuXiShu.稍难级 || data.NamDu == ENanDuXiShu.困难级 || data.NamDu == ENanDuXiShu.特别级) && data.ZongBingLiShu > 12)) ? data.QueDingZhongXinShu : 0;
//                sheet2.Range["F21"].NumberValue = (((data.NamDu == ENanDuXiShu.简单级 || data.NamDu == ENanDuXiShu.普通级) && data.ZongBingLiShu > 24) || ((data.NamDu == ENanDuXiShu.稍难级 || data.NamDu == ENanDuXiShu.困难级 || data.NamDu == ENanDuXiShu.特别级) && data.ZongBingLiShu > 12)) ? data.QueDingZhongXinShu * 8000 : 0; ;
//                sheet2.Range["A21:G21"].RowHeight = 57;


//                sheet2.Range["G20:G21"].Text = "按实际计划启动中心数计算";
//                sheet2.Range["G20:G21"].Merge();

//                //-------------------
//                sheet2.Range["A22:E22"].Text = "合计：";
//                sheet2.Range["A22:E22"].Style.HorizontalAlignment = HorizontalAlignType.Right;
//                sheet2.Range["F22"].NumberValue = data.RenGongFeiYongZongE.CRAFei.HeJi;
//                sheet2.Range["A22:E22"].Merge();
//                //-------------------
//                sheet2.Range["A23:E23"].Text = "平均每例：";
//                sheet2.Range["A23:E23"].Style.HorizontalAlignment = HorizontalAlignType.Right;
//                sheet2.Range["F23"].NumberValue = data.RenGongFeiYongZongE.CRAFei.HeJi / data.ZongBingLiShu;
//                sheet2.Range["A23:E23"].Merge();
//                //-------------------
//                sheet2.Range["A24:G24"].Text = "填表说明：1、蓝色单元格中的数据需要根据项目难度系数和特别系数计算更改；2、橙色单元格中的数据需要根据项目的病例数和中心数进行预计；3、无颜色单元格固定不变，请勿修改；4、黄色单元格为最后计算结果，已设定公式，请勿修改。";
//                sheet2.Range["A24:G24"].Merge();
//                sheet2.Range["A24:G24"].Style.Font.Color = Color.Red;
//                sheet2.Range["A24:G24"].RowHeight = 50;
//                #endregion

//                #endregion

//                #region 汇总表（不需填写，已设置公式自动汇总）

//                Worksheet sheet3 = wbToStream.Worksheets[2];
//                sheet3.Name = "汇总表（不需填写，已设置公式自动汇总）";

//                CellRange range3 = sheet3.Range["A1:C41"];
//                range3.Style.VerticalAlignment = VerticalAlignType.Center;//垂直居中 
//                range3.Style.WrapText = true;
//                range3.IsWrapText = true;
//                range3.Style.Font.Size = 10;
//                range3.Style.Font.FontName = "宋体";

//                sheet3.Range["A1:C5"].BorderAround(LineStyleType.None);
//                sheet3.Range["A1:C5"].BorderInside(LineStyleType.None);

//                sheet3.Columns[0].ColumnWidth = 28.5;
//                sheet3.Columns[1].ColumnWidth = 28.5;
//                sheet3.Columns[2].ColumnWidth = 28.5;


//                sheet3.Range["A6:C39"].BorderAround(LineStyleType.Thin);
//                sheet3.Range["A6:C39"].BorderInside(LineStyleType.Thin);

//                #region row1

//                sheet3.Range["A1:C1"].Text = "临床项目成本估算表";
//                sheet3.Range["A1:C1"].Merge();
//                sheet3.Range["A1:C1"].Style.HorizontalAlignment = HorizontalAlignType.Center;
//                sheet3.Range["A1:C1"].Style.Font.Size = 12;
//                sheet3.Range["A1:C1"].RowHeight = 40;

//                sheet3.Range["A2:C2"].Text = "委托方（全称）：" + data.CompanyName;
//                sheet3.Range["A2:C2"].Merge();

//                sheet3.Range["A3:C3"].Text = "项目名称（全称）：" + data.ProjectName;
//                sheet3.Range["A3:C3"].Merge();

//                sheet3.Range["A4:C4"].Text = "年度：";
//                sheet3.Range["A4:C4"].Merge();

//                sheet3.Range["A5:C5"].Text = "金额单位：元";
//                sheet3.Range["A5:C5"].Merge();

//                sheet3.Range["A1:C5"].Style.Font.IsBold = true;
//                sheet3.Range["A1:C5"].RowHeight = 20;


//                sheet3.Range["A6:A7"].Text = "项目";
//                sheet3.Range["A6:A7"].Merge();

//                sheet3.Range["B6:C6"].Text = "预算发生金额";
//                sheet3.Range["B6:C6"].Merge();
//                sheet3.Range["B7:C7"].Text = "整体";
//                sheet3.Range["B7:C7"].Merge();

//                sheet3.Range["A8"].Text = "临床项目";
//                sheet3.Range["B8:C8"].NumberValue = data.TotalAmount;
//                sheet3.Range["B8:C8"].Merge();
//                //---------------------------
//                sheet3.Range["A9"].Text = "  医院成本";
//                sheet3.Range["B9:C9"].NumberValue = data.YiYuanHeTongFeiZongE.TotalAmount;
//                sheet3.Range["B9:C9"].Merge();

//                sheet3.Range["A10"].Text = "    组长费";
//                sheet3.Range["B10:C10"].NumberValue = data.YiYuanHeTongFeiZongE.ZuZhangFei.HeJi;
//                sheet3.Range["B10:C10"].Merge();

//                sheet3.Range["A11"].Text = "    机构管理费";
//                sheet3.Range["B11:C11"].NumberValue = data.YiYuanHeTongFeiZongE.JiGouGuanLiFei.HeJi;
//                sheet3.Range["B11:C11"].Merge();

//                sheet3.Range["A12"].Text = "    伦理费";
//                sheet3.Range["B12:C12"].NumberValue = data.YiYuanHeTongFeiZongE.LunLiFei.HeJi;
//                sheet3.Range["B12:C12"].Merge();

//                sheet3.Range["A13"].Text = "    合格病例费用";
//                sheet3.Range["B13:C13"].NumberValue = data.YiYuanHeTongFeiZongE.HeGeBingLiFei.HeJi;
//                sheet3.Range["B13:C13"].Merge();

//                sheet3.Range["A14"].Text = "    筛选病例费用";
//                sheet3.Range["B14:C14"].NumberValue = data.YiYuanHeTongFeiZongE.ShaiXuanBingLiFei.HeJi;
//                sheet3.Range["B14:C14"].Merge();

//                sheet3.Range["A15"].Text = "    其他费用";
//                sheet3.Range["B15:C15"].NumberValue = data.YiYuanHeTongFeiZongE.QiTaFei.HeJi;
//                sheet3.Range["B15:C15"].Merge();


//                //---------------------------
//                sheet3.Range["A16"].Text = "  人工成本";
//                sheet3.Range["B16:C16"].NumberValue = data.RenGongFeiYongZongE.TotalAmount;
//                sheet3.Range["B16:C16"].Merge();

//                //---------------------------
//                sheet3.Range["A17"].Text = "  会议成本";
//                sheet3.Range["B17:C17"].NumberValue = data.HuiYiFeiZongE.TotalAmount;
//                sheet3.Range["B17:C17"].Merge();

//                sheet3.Range["A18"].Text = "    会务费";
//                sheet3.Range["B18:C18"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.HuiWuFei + data.HuiYiFeiZongE.ZongJieHui.HuiWuFei + data.HuiYiFeiZongE.ZhongQiHui.HuiWuFei + data.HuiYiFeiZongE.MangTaiShenHeHui.HuiWuFei;
//                sheet3.Range["B18:C18"].Merge();

//                sheet3.Range["A19"].Text = "    交通费";
//                sheet3.Range["B19:C19"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.JiaoTongFei + data.HuiYiFeiZongE.ZongJieHui.JiaoTongFei + data.HuiYiFeiZongE.ZhongQiHui.JiaoTongFei + data.HuiYiFeiZongE.MangTaiShenHeHui.JiaoTongFei;
//                sheet3.Range["B19:C19"].Merge();

//                sheet3.Range["A20"].Text = "    住宿费";
//                sheet3.Range["B20:C20"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.ZhuSuFei + data.HuiYiFeiZongE.ZongJieHui.ZhuSuFei + data.HuiYiFeiZongE.ZhongQiHui.ZhuSuFei + data.HuiYiFeiZongE.MangTaiShenHeHui.ZhuSuFei;
//                sheet3.Range["B20:C20"].Merge();

//                sheet3.Range["A21"].Text = "    会议室费";
//                sheet3.Range["B21:C21"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.HuiYiShiFei + data.HuiYiFeiZongE.ZongJieHui.HuiYiShiFei + data.HuiYiFeiZongE.ZhongQiHui.HuiYiShiFei + data.HuiYiFeiZongE.MangTaiShenHeHui.HuiYiShiFei;
//                sheet3.Range["B21:C21"].Merge();

//                sheet3.Range["A22"].Text = "    其他费用";
//                sheet3.Range["B22:C22"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.QiTaFei + data.HuiYiFeiZongE.ZongJieHui.QiTaFei + data.HuiYiFeiZongE.ZhongQiHui.QiTaFei + data.HuiYiFeiZongE.MangTaiShenHeHui.QiTaFei;
//                sheet3.Range["B22:C22"].Merge();


//                //---------------------------
//                sheet3.Range["A23"].Text = "  其他成本";
//                sheet3.Range["B23:C23"].NumberValue = data.QiTaZaFeiZongE.TotalAmount;
//                sheet3.Range["B23:C23"].Merge();

//                sheet3.Range["A24"].Text = "    启动会费";
//                sheet3.Range["B24:C24"].NumberValue = data.QiTaZaFeiZongE.QiDongHuiFei.HeJi;
//                sheet3.Range["B24:C24"].Merge();

//                sheet3.Range["A25"].Text = "    运输费";
//                sheet3.Range["B25:C25"].NumberValue = data.QiTaZaFeiZongE.YunShuFei.HeJi;
//                sheet3.Range["B25:C25"].Merge();

//                sheet3.Range["A26"].Text = "    监查差旅费";
//                sheet3.Range["B26:C26"].NumberValue = data.QiTaZaFeiZongE.JianChaChaiLvFei.HeJi;
//                sheet3.Range["B26:C26"].Merge();

//                sheet3.Range["A27"].Text = "    试剂、耗材费";
//                sheet3.Range["B27:C27"].NumberValue = data.QiTaZaFeiZongE.ShiJiHaoCaiFei.HeJi;
//                sheet3.Range["B27:C27"].Merge();

//                sheet3.Range["A28"].Text = "    中心实验室";
//                sheet3.Range["B28:C28"].NumberValue = data.QiTaZaFeiZongE.ZhongXinShiYanShiFei.HeJi;
//                sheet3.Range["B28:C28"].Merge();

//                sheet3.Range["A29"].Text = "    数据、统计费";
//                sheet3.Range["B29:C29"].NumberValue = data.QiTaZaFeiZongE.ShuJuTongJiFei.HeJi;
//                sheet3.Range["B29:C29"].Merge();

//                sheet3.Range["A30"].Text = "    系统使用费";
//                sheet3.Range["B30:C30"].NumberValue = data.QiTaZaFeiZongE.XiTongShiYongFei.HeJi;
//                sheet3.Range["B30:C30"].Merge();

//                sheet3.Range["A31"].Text = "    印刷费";
//                sheet3.Range["B31:C31"].NumberValue = data.QiTaZaFeiZongE.YinShuaFei.HeJi;
//                sheet3.Range["B31:C31"].Merge();

//                sheet3.Range["A32"].Text = "    其他采购";
//                sheet3.Range["B32:C32"].NumberValue = data.QiTaZaFeiZongE.QiTaCaiGouFei.HeJi;
//                sheet3.Range["B32:C32"].Merge();

//                sheet3.Range["A33"].Text = "    受试者招募费";
//                sheet3.Range["B33:C33"].NumberValue = data.QiTaZaFeiZongE.ShouShiZheZhaoMuFei.HeJi;
//                sheet3.Range["B33:C33"].Merge();

//                sheet3.Range["A34"].Text = "    稽查服务费";
//                sheet3.Range["B34"].NumberValue = data.QiTaZaFeiZongE.ShengNeiJiChaFei.HeJi + data.QiTaZaFeiZongE.ShengWaiJiChaFei.HeJi;
//                sheet3.Range["B34:C34"].Merge();

//                sheet3.Range["A35"].Text = "    SMO费用";
//                sheet3.Range["B35:C35"].NumberValue = data.QiTaZaFeiZongE.SMOFei.HeJi;
//                sheet3.Range["B35:C35"].Merge();

//                sheet3.Range["A36"].Text = "    委外监查服务";
//                sheet3.Range["B36:C36"].NumberValue = data.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.HeJi;
//                sheet3.Range["B36:C36"].Merge();

//                sheet3.Range["A37"].Text = "    遗传办填报";
//                sheet3.Range["B37:C37"].NumberValue = data.QiTaZaFeiZongE.YiChuanBanTianBaoFei.HeJi;
//                sheet3.Range["B37:C37"].Merge();

//                sheet3.Range["A38"].Text = "    其他费用";
//                sheet3.Range["B38:C38"].NumberValue = data.QiTaZaFeiZongE.QiTaFei.HeJi;
//                sheet3.Range["B38:C38"].Merge();

//                //-------------------------------------------------

//                sheet3.Range["A39"].Text = "合计";
//                sheet3.Range["B39:C39"].NumberValue = data.TotalAmount;
//                sheet3.Range["B39:C39"].Merge();

//                //-------------------------------------------------
//                sheet3.Range["A40"].Text = "项目经理：";
//                sheet3.Range["B40"].Text = "部长审核：";
//                sheet3.Range["C40"].Text = "总监审核：";

//                sheet3.Range["A41"].Text = "提交日期：";
//                sheet3.Range["B41"].Text = "审核日期：";
//                sheet3.Range["C41"].Text = "审核日期：";

//                sheet3.Range["A40:C41"].Style.Font.IsBold = true;
//                sheet3.Range["A40:C41"].RowHeight = 20;

//                sheet3.Range["A8:C8"].Style.KnownColor = ExcelColors.Orange;
//                sheet3.Range["A9:C9"].Style.KnownColor = ExcelColors.LightOrange;
//                sheet3.Range["A16:C16"].Style.KnownColor = ExcelColors.LightOrange;
//                sheet3.Range["A17:C17"].Style.KnownColor = ExcelColors.LightOrange;
//                sheet3.Range["A23:C23"].Style.KnownColor = ExcelColors.LightOrange;
//                sheet3.Range["A39:C39"].Style.KnownColor = ExcelColors.LightOrange;

//                #endregion


//                #endregion


//                string path = $"{data.CompanyName}.xls";


//                FileStream file_stream = new FileStream(path, FileMode.Create);
//                wbToStream.SaveToStream(file_stream);
//                file_stream.Close();
//                return path;
//                // System.Diagnostics.Process.Start($"{data.ProjectName}项目成本预估表.xls");

//                //B. Load Excel file from stream
//                //Workbook wbFromStream = new Workbook();
//                //FileStream fileStream = File.OpenRead("sample.xls");
//                //fileStream.Seek(0, SeekOrigin.Begin);
//                //wbFromStream.LoadFromStream(fileStream);
//                //wbFromStream.SaveToFile("From_stream.xls", ExcelVersion.Version97to2003);
//                //fileStream.Dispose();
//                //System.Diagnostics.Process.Start("From_stream.xls");

//            }


//            public string CreatePdf(ShiYanFangAnDto data)
//            {
//                BaseFont bfSun = BaseFont.CreateFont(@"c:\windows\fonts\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
//                //宋体加粗（用于标题部分）
//                iTextSharp.text.Font font_Song_Bold_Title = new iTextSharp.text.Font(bfSun, 12, iTextSharp.text.Font.BOLD);
//                font_Song_Bold_Title.SetStyle("Italic");

//                //宋体加粗（用于表头或着重显示的部分）
//                iTextSharp.text.Font font_Song_Bold = new iTextSharp.text.Font(bfSun, 12, iTextSharp.text.Font.BOLD);
//                font_Song_Bold.SetStyle("Italic");
//                //普通宋体（用于文档内容）
//                BaseFont bfSunZ = BaseFont.CreateFont(@"c:\windows\fonts\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
//                iTextSharp.text.Font font_Song = new iTextSharp.text.Font(bfSunZ, 12);
//                font_Song.SetStyle("Italic");

//                string path = "AB.pdf";//$"{data.CompanyName}.pdf";
//                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
//                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
//                document.Open();

//                float titleLeft = 50, spacingBefore = 5, spacingAfter = 20, padding = 5;

//                #region 四、 医疗器械临床研究费用预算清单 

//                var title = new iTextSharp.text.Paragraph("四、 医疗器械临床研究费用预算清单", font_Song_Bold_Title);
//                title.IndentationLeft = titleLeft;
//                document.Add(title);

//                PdfPCell nCell;
//                PdfPTable table = new PdfPTable(3);
//                table.SetWidthPercentage(new float[] { 150, 150, 200 }, PageSize.A4);
//                table.SpacingBefore = spacingBefore;
//                table.SpacingAfter = spacingAfter;
//                nCell = new PdfPCell(new Phrase("产品", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("项目", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);
//                nCell = new PdfPCell(new Phrase("费用(单位：人民币/万元)", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("注射用透明质酸钠-重组 Ⅲ型人源化胶原蛋白复合 溶液", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 6;
//                nCell.SetLeading(2f, 1.5f);
//                nCell.FixedHeight = 70;
//                table.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("CRO 服务费用(包含数统)", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);
//                nCell = new PdfPCell(new Phrase("195.04", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("日常费用(包含差旅费用)", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);
//                nCell = new PdfPCell(new Phrase("14.89", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("会议费用", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);
//                nCell = new PdfPCell(new Phrase("15.37", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("CRO 服务费合计(税 6%)", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);
//                nCell = new PdfPCell(new Phrase("225.3", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                table.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("医院研究费用", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);
//                nCell = new PdfPCell(new Phrase("275.49", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("SMO 费用", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);
//                nCell = new PdfPCell(new Phrase("145.75", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("合计(税 6%)", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Colspan = 2;
//                table.AddCell(nCell);
//                nCell = new PdfPCell(new Phrase("646.5", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table.AddCell(nCell);


//                document.Add(table);
//                #endregion

//                #region (一)临床试验服务费用

//                title = new iTextSharp.text.Paragraph("(一)临床试验服务费用", font_Song_Bold_Title);
//                title.IndentationLeft = titleLeft;
//                document.Add(title);

//                PdfPTable table2 = new PdfPTable(3);
//                table2.SetWidthPercentage(new float[] { 250, 100, 150 }, PageSize.A4);
//                table2.SpacingBefore = spacingBefore;
//                table2.SpacingAfter = spacingAfter;

//                nCell = new PdfPCell(new Phrase("项       目", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("费用(万元)", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("备       注", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("1.筛选研究单位", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Colspan = 2;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("3", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("研究单位调研、筛选、实地拜访", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("3", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("1.全国办事处进行调研， 以最 终确定研究单位(3 家中心) ；", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);
//                //-------------------------------------

//                nCell = new PdfPCell(new Phrase("2.临床试验方案设计", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Colspan = 2;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("15", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("临床研究方案修订、主持方案协调会及方 案定稿、 CRF、知情同意书设计、受试者文 件夹设计", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("15", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("包括方案， 普通 CRF，无碳写 CRF", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);
//                //-------------------------------------

//                nCell = new PdfPCell(new Phrase("3. CRO 服务费用", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Colspan = 2;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("119.5", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("试验前准备 1", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("7.5", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 15;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("25000 元 *3 家中心", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 15;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("机构立项资料准备、递交、立项", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("报伦理委员会的资料准备、递交、过会", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("研究者手册审核", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("建立研究者档案", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("试验前准备 2", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("临床试验协议商议、签订", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("试验前准备 3", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("研究者资料收集与整理", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("现场 GCP 培训", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("严重不良事件报告培训", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("受试者筛选的现场指导", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("试验文件/器械分发", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("试验器械、资料管理和讲解", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("质量控制讲解", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                //-------------------------------------


//                nCell = new PdfPCell(new Phrase("监查访视", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("67.5", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 12;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("中心数： 3 家中心；\r\n1 名 CRA/中心；", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 12;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("知情同意书核查", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("病例报告表审核", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("病例报告与原始病历核对", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("试验安全性/依从性审核", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("试验管理文件审核", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("试验物资管理审核", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("不良事件和严重不良事件的记录", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("违背方案情况记录", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("入组进度", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("与研究者商讨解决问题", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("完成监查报告", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                //-------------------------------------


//                nCell = new PdfPCell(new Phrase("试验结束访视", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("7.5", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 5;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("25000 元 *3 家中心", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 5;
//                table2.AddCell(nCell);


//                nCell = new PdfPCell(new Phrase("数据疑问和问题解决", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);


//                nCell = new PdfPCell(new Phrase("回收试验物资", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("回收所有试验记录及文件", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("严重不良事件追踪", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);


//                //-------------------------------------

//                nCell = new PdfPCell(new Phrase("项目管理", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("36", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 8;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 8;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("项目管理计划", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("项目管理会议(内部、外部)", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("临床试验文档(TMF)管理", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("团队及沟通管理", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("申办方协同访视", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("研究中心 GCP 质控", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("研究中心付款及跟踪记录", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);
//                //-------------------------------------


//                nCell = new PdfPCell(new Phrase("试验资料的内部专家审核", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("1", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 2;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase(" ", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 2;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("综合审核会费用", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                //-------------------------------------

//                nCell = new PdfPCell(new Phrase("4. 数据管理和统计分析", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Colspan = 2;
//                table2.AddCell(nCell);


//                nCell = new PdfPCell(new Phrase("30", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                //-------------------------------------

//                nCell = new PdfPCell(new Phrase("数据管理", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("18", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 9;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("1.包含 EDC 系统费用;          2.按照《医疗器械临床试验数据 递交要求注册审查指导原则》 \t(2021)要求需要提供原始数据 库、分析数据库、统计分析程序 代码、说明文件等；3.包含随机化系统", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 9;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("CRF 设计与核查", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("数据稽查", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("数据录入", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("数据逻辑检查", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("数据医学检查(专业检查)", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("数据质疑/质疑解决/质疑管理", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("数据库清理/更新", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("数据库锁定/提交/备份", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                //---------------------------------------

//                nCell = new PdfPCell(new Phrase("统计分析", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("12", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 6;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase(" ", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Rowspan = 6;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("统计分析计划书", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("随机过程编程", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("统计分析编程", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("模拟数据统计测试", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("撰写统计分析报告", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                //---------------------------------------

//                nCell = new PdfPCell(new Phrase("5. 总结", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Colspan = 2;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("16.5", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("资料回收、归档", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("1.5", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("临床研究总结报告及盖章", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("15", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_CENTER;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase(" ", font_Song));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                //---------------------------------------

//                nCell = new PdfPCell(new Phrase("6.合计", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("184 万", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Colspan = 2;
//                table2.AddCell(nCell);


//                //----------------------------------------

//                nCell = new PdfPCell(new Phrase("7.含税总计(税率 6%)", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                table2.AddCell(nCell);

//                nCell = new PdfPCell(new Phrase("195.04 万", font_Song_Bold));
//                nCell.HorizontalAlignment = Element.ALIGN_LEFT;
//                nCell.VerticalAlignment = Element.ALIGN_CENTER;
//                nCell.Padding = padding;
//                nCell.Colspan = 2;
//                table2.AddCell(nCell);

//                document.Add(table2);
//                #endregion




//                // table.HorizontalAlignment = Element.ALIGN_CENTER;





//                document.Close();


//                return path;
//            }
//        }
//    }

