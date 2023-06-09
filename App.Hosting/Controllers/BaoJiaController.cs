using App.Application; 
using App.Application.Blog.Dtos;
using App.Application.Dtos;
using App.Framwork.Extensions;
using App.Framwork.Result;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.Formula.Functions;
using NPOI.XWPF.UserModel;
using Spire.Xls;
using Spire.Xls.Core;
using SqlSugar.Extensions;
using System.Drawing;
using Document = iTextSharp.text.Document;
using PageSize = iTextSharp.text.PageSize;

namespace App.Hosting.Controllers
{
    public class BaoJiaController : Controller
    {
        public IActionResult In()
        {
            return View();
        }

        private readonly IBaoJiaService _baoJiaService = new BaiJiaAppService2();

        //public BaoJiaAppService(IBaoJiaService baoJiaService)
        //{
        //    _baoJiaService = baoJiaService;
        //}

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public UnifyResult<CreateBaoJiaDanInput> Init([FromBody] InitInput input)
        {
            return _baoJiaService.Init(input);
        }

        /// <summary>
        /// 生成报价单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public UnifyResult<CreateBaoJiaDanOutput> CreateBaoJiaDan([FromBody] CreateBaoJiaDanInput input)
        {
            return _baoJiaService.CreateBaoJiaDan(input);
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> FileDownload(string name)
        {

            using (var stream = System.IO.File.OpenRead(name))
            {
                //  var (stream, _) = await "https://furion.baiqian.ltd/img/rm1.png".GetAsStreamAsync();


                // 将 stream 转 byte[]
                byte[] bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes);
                stream.Seek(0, SeekOrigin.Begin);

                return new FileContentResult(bytes, "application/octet-stream")
                {
                    FileDownloadName = "" // 配置文件下载显示名
                };
            }


        }

        /// <summary>
        /// 查询报价单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public UnifyResult<SqlSugarPagedList<BaoJiaDanListOutput>> SearchBaoJiaDan([FromBody] SearchBaoJiaDanInput input)
        {

            return _baoJiaService.SearchBaoJiaDan(input);
        }
        /// <summary>
        /// 更新报价单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<UnifyResult<CreateBaoJiaDanOutput>> UpdateBaoJiaDan([FromBody] UpdateBaoJiaDanInput input)
        {
            return await _baoJiaService.UpdateBaoJiaDan(input);
        }

        /// <summary>
        /// 获取报价单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public UnifyResult<CreateBaoJiaDanInput> GetDetail(string id)
        {
            return _baoJiaService.GetDetail(id);

        }

        /// <summary>
        /// 查询委托方公司名称是否存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public UnifyResult CheckCompany([FromBody]  CheckCompanyInput input)
        {
            return _baoJiaService.CheckCompany(input);
        }
    }


    public class BaiJiaAppService2 : IBaoJiaService
    {

        public UnifyResult<CreateBaoJiaDanInput> Init(InitInput input)
        {
            var output = new CreateBaoJiaDanInput();
            XiangMuYuSuanZongBiaoDto XiangMuYuSuanZong = new XiangMuYuSuanZongBiaoDto(input.LiShu, input.ShiXian, input.ZhongXinShu, input.NanDu);

            if (input.Id.IsNullOrEmpty())
            {

                output = new CreateBaoJiaDanInput()
                {
                    XieTiaoHui = new HuiYiFeiInput
                    {
                        HuiWuFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.HuiWuFei,
                        HuiYiShiFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.HuiYiShiFei,
                        JiaoTongFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.JiaoTongFei,
                        QiTaFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.QiTaFei,
                        ZhuSuFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.ZhuSuFei,
                    },
                    ZongJieHui = new HuiYiFeiInput
                    {
                        HuiWuFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.HuiWuFei,
                        HuiYiShiFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.HuiYiShiFei,
                        JiaoTongFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.JiaoTongFei,
                        QiTaFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.QiTaFei,
                        ZhuSuFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.ZhuSuFei,
                    },
                    ZhongQiHui = new HuiYiFeiInput
                    {
                        HuiWuFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.HuiWuFei,
                        HuiYiShiFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.HuiYiShiFei,
                        JiaoTongFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.JiaoTongFei,
                        QiTaFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.QiTaFei,
                        ZhuSuFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.ZhuSuFei,

                    },
                    MangTaiShenHeHui = new HuiYiFeiInput
                    {
                        HuiWuFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.HuiWuFei,
                        HuiYiShiFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.HuiYiShiFei,
                        JiaoTongFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.JiaoTongFei,
                        QiTaFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.QiTaFei,
                        ZhuSuFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.ZhuSuFei,
                    },

                    ZuZhangFei_Count = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.ZuZhangFei.Count,
                    ZuZhangFei_Price = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.ZuZhangFei.Price,
                    JiGouGuanLiFei_Price = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.JiGouGuanLiFei.Price,
                    LunLiFei_Price = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.LunLiFei.Price,
                    HeGeBingLiFei_Count = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.HeGeBingLiFei.Count,
                    HeGeBingLiFei_Price = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.HeGeBingLiFei.Price,
                    ShaiXuanBingLiFei_Count = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.ShaiXuanBingLiFei.Price,
                    ShaiXuanBingLiFei_Price = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.ShaiXuanBingLiFei.Count,


                    QiDongHuiFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.QiDongHuiFei.Price,
                    YunShuFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.YunShuFei.Price,
                    JianChaChaiLvFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.JianChaChaiLvFei.Price,
                    ShiJiHaoCaiFei_Count = XiangMuYuSuanZong.QiTaZaFeiZongE.ShiJiHaoCaiFei.Count,
                    ShiJiHaoCaiFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.ShiJiHaoCaiFei.Price,
                    ZhongXinShiYanShiFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.ZhongXinShiYanShiFei.Price,
                    ShuJuTongJiFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.ShuJuTongJiFei.Price,
                    XiTongShiYongFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.XiTongShiYongFei.Price,
                    YinShuaFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.YinShuaFei.Price,
                    QiTaCaiGouFei_Count = XiangMuYuSuanZong.QiTaZaFeiZongE.QiTaCaiGouFei.Count,
                    QiTaCaiGouFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.QiTaCaiGouFei.Price,
                    ShouShiZheZhaoMuFei_Count = XiangMuYuSuanZong.QiTaZaFeiZongE.ShouShiZheZhaoMuFei.Count,
                    ShouShiZheZhaoMuFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.ShouShiZheZhaoMuFei.Price,
                    ShengNeiJiChaFei_Count = XiangMuYuSuanZong.QiTaZaFeiZongE.ShengNeiJiChaFei.Count,
                    ShengNeiJiChaFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.ShengNeiJiChaFei.Price,
                    ShengWaiJiChaFei_Count = XiangMuYuSuanZong.QiTaZaFeiZongE.ShengWaiJiChaFei.Count,
                    ShengWaiJiChaFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.ShengWaiJiChaFei.Price,
                    SMOFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.SMOFei.Price,
                    WeiWaiJianChaFuWuFei_Count = XiangMuYuSuanZong.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.Count,
                    WeiWaiJianChaFuWuFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.Price,
                    YiChuanBanTianBaoFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.YiChuanBanTianBaoFei.Price,
                    QiTaFei_Price = XiangMuYuSuanZong.QiTaZaFeiZongE.QiTaFei.Price,

                    GuanLiRenYuanFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.GuanLiRenYuanFei.Price,
                    PMFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.PMFei.Price,
                    PLFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.PLFei.Price,
                    CTAFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.CTAFei.Price,
                    XieTongJianChaFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.XieTongJianChaFei.Price,
                    YiXueZhiChiZhuanXieFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.YiXueZhiChi_ZhuanXieFei.Price,
                    YiXueZhiChiJianChaFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.YiXueZhiChi_JianChaFei.Price,
                    ZhiKongZhiChiFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.ZhiKongZhiChiFei.Price,
                    PVZhiChiFei_Count = XiangMuYuSuanZong.RenGongFeiYongZongE.PVZhiChiFei.Count,
                    PVZhiChiFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.PVZhiChiFei.Price,
                    XiangMuJiangLiFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.XiangMuJiangLiFei.Price,

                    NanDu = input.NanDu,
                    ProjectName = XiangMuYuSuanZong.ProjectName,
                    ShiXian = input.ShiXian,
                    ShiYingZheng = XiangMuYuSuanZong.ShiYingZheng,
                    SuiFang = XiangMuYuSuanZong.SuiFang,
                    Type = 0,
                    ZhongXinShu = XiangMuYuSuanZong.QueDingZhongXinShu,
                    LiShu = XiangMuYuSuanZong.ZongBingLiShu,
                    CompanyName = XiangMuYuSuanZong.CompanyName,
                    FeiXieYi = 1000,
                    Admin_BuChuChai_Price = 500,
                    Admin_ChuChai_Price = 3000,
                    CRA_BuChuChai_Price = 800,
                    CRA_ChuChai_Price = 3000,
                    PM_BuChuChai_Price = 500,
                    PM_ChuChai_Price = 3000,
                    RuZuHouJianChaFei = 100,
                    ShaiXuanQiJianChaFei = 50,
                    V1 = 100,
                    V2 = 100,
                    V3 = 100,
                    V4 = 100,
                    V5 = 100,
                    XieYi_RuZu = 1000,
                    XieYi_ShaiXuanQi = 500,
                    AE = 100,
                    DanLiShouShiZheBuZhu = XiangMuYuSuanZong.DanLiShouShiZheBuZhu.DanLiShouShiZheBuZhu_Price,
                };
            }
            else
            {
                var outputnew = GetDetail(input.Id).Data;
                output = outputnew;

                if (input.ZhongXinShu != outputnew.ZhongXinShu)
                {
                    //如果中心数有修改
                    output.XieTiaoHui = new HuiYiFeiInput
                    {
                        HuiWuFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.HuiWuFei,
                        HuiYiShiFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.HuiYiShiFei,
                        JiaoTongFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.JiaoTongFei,
                        QiTaFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.QiTaFei,
                        ZhuSuFei = XiangMuYuSuanZong.HuiYiFeiZongE.XieTiaoHui.ZhuSuFei,
                    };
                    output.ZongJieHui = new HuiYiFeiInput
                    {
                        HuiWuFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.HuiWuFei,
                        HuiYiShiFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.HuiYiShiFei,
                        JiaoTongFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.JiaoTongFei,
                        QiTaFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.QiTaFei,
                        ZhuSuFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZongJieHui.ZhuSuFei,
                    };
                    output.ZhongQiHui = new HuiYiFeiInput
                    {
                        HuiWuFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.HuiWuFei,
                        HuiYiShiFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.HuiYiShiFei,
                        JiaoTongFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.JiaoTongFei,
                        QiTaFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.QiTaFei,
                        ZhuSuFei = XiangMuYuSuanZong.HuiYiFeiZongE.ZhongQiHui.ZhuSuFei,

                    };
                    output.MangTaiShenHeHui = new HuiYiFeiInput
                    {
                        HuiWuFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.HuiWuFei,
                        HuiYiShiFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.HuiYiShiFei,
                        JiaoTongFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.JiaoTongFei,
                        QiTaFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.QiTaFei,
                        ZhuSuFei = XiangMuYuSuanZong.HuiYiFeiZongE.MangTaiShenHeHui.ZhuSuFei,
                    };
                }
                if (input.NanDu != output.NanDu)
                {
                    output.GuanLiRenYuanFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.GuanLiRenYuanFei.Price;
                    output.YiXueZhiChiZhuanXieFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.YiXueZhiChi_ZhuanXieFei.Price;
                    output.ZhiKongZhiChiFei_Price = XiangMuYuSuanZong.RenGongFeiYongZongE.ZhiKongZhiChiFei.Price;
                }
                output.LiShu = input.LiShu;
                output.ZhongXinShu = input.ZhongXinShu;
                output.ShiXian = input.ShiXian;
                output.NanDu = input.NanDu;
            }


            UnifyResult<CreateBaoJiaDanInput> result = new UnifyResult<CreateBaoJiaDanInput>();

            result.Data = output;
            result.StatusCode = App.Framwork.Result.ResultCode.Success;

            return result;

        }

        public UnifyResult<CreateBaoJiaDanOutput> CreateBaoJiaDan(CreateBaoJiaDanInput input)
        {
            UnifyResult<CreateBaoJiaDanOutput> result = new UnifyResult<CreateBaoJiaDanOutput>();

            XiangMuYuSuanZongBiaoDto XiangMuYuSuanZong2 = new XiangMuYuSuanZongBiaoDto(input);

            var hassql = "select * from AirtelBaoJai where companyname='" + input.CompanyName + "'";
            var hasEntity = GetGoalsEntity(hassql);
            if (hasEntity.Count > 0)
            {
                result.Message = "请您重新输入，系统已经存在该委托方名字了哦！";
                result.Type = "warning";
                result.StatusCode = App.Framwork.Result.ResultCode.Error;
                return result;
            }
            var res = AddInfo(input);
            if (res.Result.ToString() == "失败")
            {
                result.Message = "生成报价单失败，请重新操作。";
                result.Type = "warning";
                result.StatusCode = App.Framwork.Result.ResultCode.Error;
                return result;
            }

            CreateBaoJiaDanOutput output = new CreateBaoJiaDanOutput();
            output.ShiYanFangAn = InitShiYanFangAn(XiangMuYuSuanZong2);
            string xlsurl = CreateExcel(XiangMuYuSuanZong2);
            output.XlsFile = new OutputFile
            {
                Name = XiangMuYuSuanZong2.CompanyName,
                Url = "http://192.168.10.12:8066/web/pdf/EXCEL/" + xlsurl
            };

            string pdfurl = CreatePdf(output.ShiYanFangAn);
            output.PdfFile = new OutputFile
            {
                Name = XiangMuYuSuanZong2.CompanyName,
                Url = "http://192.168.10.12:8066/web/pdf/PDF/" + pdfurl
            };
            string docurl = CreateDoc(output.ShiYanFangAn);
            output.DocFile = new OutputFile
            {
                Name = XiangMuYuSuanZong2.CompanyName,
                Url = "http://192.168.10.12:8066/web/pdf/PDF/" + docurl
            };
            result.Data = output;
            result.StatusCode = App.Framwork.Result.ResultCode.Success;
            return result;
        }

        public UnifyResult<SqlSugarPagedList<BaoJiaDanListOutput>> SearchBaoJiaDan(SearchBaoJiaDanInput input)
        {
            UnifyResult<SqlSugarPagedList<BaoJiaDanListOutput>> result = new UnifyResult<SqlSugarPagedList<BaoJiaDanListOutput>>();

            string str = "SELECT * FROM AirtelBaoJai where companyname like '%" + input.Keywords + "%' or projectname like '%" + input.Keywords + "%' ORDER BY CONVERT(datetime,CreateTime,120)  DESC ";
            var list = GetGoalsEntity(str);
            var res = list.Select(c => new BaoJiaDanListOutput
            {
                CompanyName = c.CompanyName,
                Id = c.Id,
                LiShu = c.LiShu,
                NanDu = c.NanDu,
                ProjectName = c.ProjectName,
                ShiXian = c.ShiXian,
                ZhongXinShu = c.ZhongXinShu

            }).ToPagedList(input.Page, input.Limit);

            result.Data = res;
            result.StatusCode = App.Framwork.Result.ResultCode.Success;

            return result;
        }

        public async Task<UnifyResult<CreateBaoJiaDanOutput>> UpdateBaoJiaDan(UpdateBaoJiaDanInput input)
        {
            UnifyResult<CreateBaoJiaDanOutput> result = new UnifyResult<CreateBaoJiaDanOutput>();  
            result.Message = await UPDATEInfo(input); 
            if (result.Message.ToString() == "成修成功！")
            {
                XiangMuYuSuanZongBiaoDto XiangMuYuSuanZong2 = new XiangMuYuSuanZongBiaoDto(input);
                CreateBaoJiaDanOutput output = new CreateBaoJiaDanOutput();
                output.ShiYanFangAn = InitShiYanFangAn(XiangMuYuSuanZong2);
                string xlsurl = CreateExcel(XiangMuYuSuanZong2);
                output.XlsFile = new OutputFile
                {
                    Name = XiangMuYuSuanZong2.CompanyName,
                    Url = "http://192.168.10.12:8066/web/pdf/EXCEL/" + xlsurl
                };

                string pdfurl = CreatePdf(output.ShiYanFangAn);
                output.PdfFile = new OutputFile
                {
                    Name = XiangMuYuSuanZong2.CompanyName,
                    Url = "http://192.168.10.12:8066/web/pdf/PDF/" + pdfurl
                };
                string docurl = CreateDoc(output.ShiYanFangAn);
                output.DocFile = new OutputFile
                {
                    Name = XiangMuYuSuanZong2.CompanyName,
                    Url = "http://192.168.10.12:8066/web/pdf/PDF/" + docurl
                };
                result.Data = output;
                result.StatusCode = App.Framwork.Result.ResultCode.Success;
                return result; 
            }
            else {
                result.Message = "修改报价单失败，请重新操作。";
                result.Type = "warning";
                result.StatusCode = App.Framwork.Result.ResultCode.Error;
            }
            return result;
        }


        public UnifyResult<CreateBaoJiaDanInput> GetDetail(string id)
        {
            UnifyResult<CreateBaoJiaDanInput> result = new UnifyResult<CreateBaoJiaDanInput>();
            result.StatusCode = App.Framwork.Result.ResultCode.Success;
            string sql = "select * from AirtelBaoJai where id=" + id + "";
            var entity = GetGoalsEntity(sql).FirstOrDefault();
            if (entity != null)
            {
                CreateBaoJiaDanInput input = new CreateBaoJiaDanInput();

                input = new CreateBaoJiaDanInput()
                { Id = entity.Id,
                    CompanyName = entity.CompanyName,
                    CTAFei_Price = entity.CTAFei_Price.ObjToMoney(),
                    GuanLiRenYuanFei_Price = entity.GuanLiRenYuanFei_Price.ObjToMoney(),
                    LiShu = entity.LiShu.ObjToInt(),
                    LunLiFei_Price = entity.LunLiFei_Price.ObjToMoney(),
                    MangTaiShenHeHui = new HuiYiFeiInput
                    {
                        HuiWuFei = entity.MangTaiShenHeHui_HuiWuFei.ObjToMoney(),
                        HuiYiShiFei = entity.MangTaiShenHeHui_HuiYiShiFei.ObjToMoney(),
                        JiaoTongFei = entity.MangTaiShenHeHui_JiaoTongFei.ObjToMoney(),
                        QiTaFei = entity.MangTaiShenHeHui_QiTaFei.ObjToMoney(),
                        ZhuSuFei = entity.MangTaiShenHeHui_ZhuSuFei.ObjToMoney(),
                    },
                    NanDu = (ENanDuXiShu)entity.NanDu.ObjToInt(),
                    PLFei_Price = entity.PLFei_Price.ObjToMoney(),
                    PMFei_Price = entity.PMFei_Price.ObjToMoney(),
                    ProjectName = entity.ProjectName,
                    PVZhiChiFei_Count = entity.PVZhiChiFei_Count.ObjToInt(),
                    PVZhiChiFei_Price = entity.PVZhiChiFei_Price.ObjToMoney(),
                    QiDongHuiFei_Price = entity.QiDongHuiFei_Price.ObjToMoney(),
                    QiTaCaiGouFei_Count = entity.QiTaCaiGouFei_Count.ObjToInt(),
                    QiTaCaiGouFei_Price = entity.QiTaCaiGouFei_Price.ObjToMoney(),
                    QiTaFei_Price = entity.QiTaFei_Price.ObjToMoney(),
                    ShengNeiJiChaFei_Count = entity.ShengNeiJiChaFei_Count.ObjToInt(),
                    ShengNeiJiChaFei_Price = entity.ShengNeiJiChaFei_Price.ObjToMoney(),
                    ShengWaiJiChaFei_Count = entity.ShengWaiJiChaFei_Count.ObjToInt(),
                    ShengWaiJiChaFei_Price = entity.ShengWaiJiChaFei_Price.ObjToMoney(),
                    ShiJiHaoCaiFei_Count = entity.ShiJiHaoCaiFei_Count.ObjToInt(),
                    ShiJiHaoCaiFei_Price = entity.ShiJiHaoCaiFei_Price.ObjToMoney(),
                    ShiXian = entity.ShiXian.ObjToInt(),
                    ShiYingZheng = entity.ShiYingZheng,
                    ShouShiZheZhaoMuFei_Count = entity.ShouShiZheZhaoMuFei_Count.ObjToInt(),
                    ShouShiZheZhaoMuFei_Price = entity.ShouShiZheZhaoMuFei_Price.ObjToMoney(),
                    ShuJuTongJiFei_Price = entity.ShuJuTongJiFei_Price.ObjToMoney(),
                    SMOFei_Price = entity.SMOFei_Price.ObjToMoney(),
                    SuiFang = entity.SuiFang.ObjToInt(),
                    Type = 0,
                    WeiWaiJianChaFuWuFei_Count = entity.WeiWaiJianChaFuWuFei_Count.ObjToInt(),
                    WeiWaiJianChaFuWuFei_Price = entity.WeiWaiJianChaFuWuFei_Price.ObjToMoney(),
                    XiangMuJiangLiFei_Price = entity.XiangMuJiangLiFei_Price.ObjToMoney(),
                    XieTiaoHui = new HuiYiFeiInput
                    {
                        HuiWuFei = entity.XieTiaoHui_HuiWuFei.ObjToMoney(),
                        HuiYiShiFei = entity.XieTiaoHui_HuiYiShiFei.ObjToMoney(),
                        JiaoTongFei = entity.XieTiaoHui_JiaoTongFei.ObjToMoney(),
                        QiTaFei = entity.XieTiaoHui_QiTaFei.ObjToMoney(),
                        ZhuSuFei = entity.XieTiaoHui_ZhuSuFei.ObjToMoney(),
                    },
                    XieTongJianChaFei_Price = entity.XieTongJianChaFei_Price.ObjToMoney(),
                    XiTongShiYongFei_Price = entity.XiTongShiYongFei_Price.ObjToMoney(),
                    YiChuanBanTianBaoFei_Price = entity.YiChuanBanTianBaoFei_Price.ObjToMoney(),
                    YinShuaFei_Price = entity.YinShuaFei_Price.ObjToMoney(),
                    YiXueZhiChiJianChaFei_Price = entity.YiXueZhiChiJianChaFei_Price.ObjToMoney(),
                    YiXueZhiChiZhuanXieFei_Price = entity.YiXueZhiChiZhuanXieFei_Price.ObjToMoney(),
                    YunShuFei_Price = entity.YunShuFei_Price.ObjToMoney(),
                    ZhiKongZhiChiFei_Price = entity.ZhiKongZhiChiFei_Price.ObjToMoney(),
                    ZhongQiHui = new HuiYiFeiInput
                    {
                        HuiWuFei = entity.ZhongQiHui_HuiWuFei.ObjToMoney(),
                        HuiYiShiFei = entity.ZhongQiHui_HuiYiShiFei.ObjToMoney(),
                        JiaoTongFei = entity.ZhongQiHui_JiaoTongFei.ObjToMoney(),
                        QiTaFei = entity.ZhongQiHui_QiTaFei.ObjToMoney(),
                        ZhuSuFei = entity.ZhongQiHui_ZhuSuFei.ObjToMoney(),

                    },
                    ZhongXinShiYanShiFei_Price = entity.ZhongXinShiYanShiFei_Price.ObjToMoney(),
                    ZhongXinShu = entity.ZhongXinShu.ObjToInt(),
                    ZongJieHui = new HuiYiFeiInput
                    {
                        HuiWuFei = entity.ZongJieHui_HuiWuFei.ObjToMoney(),
                        HuiYiShiFei = entity.ZongJieHui_HuiYiShiFei.ObjToMoney(),
                        JiaoTongFei = entity.ZongJieHui_JiaoTongFei.ObjToMoney(),
                        QiTaFei = entity.ZongJieHui_QiTaFei.ObjToMoney(),
                        ZhuSuFei = entity.ZongJieHui_ZhuSuFei.ObjToMoney(),
                    },
                    ZuZhangFei_Count = entity.ZuZhangFei_Count.ObjToInt(),
                    ZuZhangFei_Price = entity.ZuZhangFei_Price.ObjToMoney(),

                    FeiXieYi = entity.FeiXieYi.ObjToMoney(),
                    Admin_BuChuChai_Price = entity.Admin_BuChuChai_Price.ObjToMoney(),
                    Admin_ChuChai_Price = entity.Admin_ChuChai_Price.ObjToMoney(),
                    CRA_BuChuChai_Price = entity.CRA_BuChuChai_Price.ObjToMoney(),
                    CRA_ChuChai_Price = entity.CRA_ChuChai_Price.ObjToMoney(),
                    PM_BuChuChai_Price = entity.PM_BuChuChai_Price.ObjToMoney(),
                    PM_ChuChai_Price = entity.PM_ChuChai_Price.ObjToMoney(),
                    RuZuHouJianChaFei = entity.RuZuHouJianChaFei.ObjToMoney(),
                    ShaiXuanQiJianChaFei = entity.ShaiXuanQiJianChaFei.ObjToMoney(),
                    V1 = entity.V1.ObjToMoney(),
                    V2 = entity.V2.ObjToMoney(),
                    V3 = entity.V3.ObjToMoney(),
                    V4 = entity.V4.ObjToMoney(),
                    V5 = entity.V5.ObjToMoney(),
                    XieYi_RuZu = entity.XieYi_RuZu.ObjToMoney(),
                    XieYi_ShaiXuanQi = entity.XieYi_ShaiXuanQi.ObjToMoney(),
                    DanLiShouShiZheBuZhu=entity.DanLiShouShiZheBuZhu.ObjToMoney(),

                };
                XiangMuYuSuanZongBiaoDto XiangMuYuSuanZong = new XiangMuYuSuanZongBiaoDto(input);
                input.HeGeBingLiFei_Count = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.HeGeBingLiFei.Count;
                input.HeGeBingLiFei_Price = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.HeGeBingLiFei.Price;
                input.ShaiXuanBingLiFei_Count = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.ShaiXuanBingLiFei.Price;
                input.ShaiXuanBingLiFei_Price = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.ShaiXuanBingLiFei.Count;
                input.JianChaChaiLvFei_Price = XiangMuYuSuanZong.YiYuanHeTongFeiZongE.ShaiXuanBingLiFei.Price;

                input.Admin_BuChuChai_Count = XiangMuYuSuanZong.ChaiLvFeiChengBenGuiZeBiao.Admin_BuChuChai_Count;
                input.Admin_ChuChai_Count = XiangMuYuSuanZong.ChaiLvFeiChengBenGuiZeBiao.Admin_ChuChai_Count;
                input.CRA_BuChuChai_Count = XiangMuYuSuanZong.ChaiLvFeiChengBenGuiZeBiao.CRA_BuChuChai_Count;
                input.CRA_ChuChai_Count = XiangMuYuSuanZong.ChaiLvFeiChengBenGuiZeBiao.CRA_ChuChai_Count;
                input.PM_BuChuChai_Count = XiangMuYuSuanZong.ChaiLvFeiChengBenGuiZeBiao.PM_BuChuChai_Count;
                input.PM_ChuChai_Count = XiangMuYuSuanZong.ChaiLvFeiChengBenGuiZeBiao.PM_ChuChai_Count;

                result.Data = input;
            }
            else {
                result.Message = "该记录不存在";
                result.Type = "warning";
                result.StatusCode = App.Framwork.Result.ResultCode.Error;
            }

            return result;
        }

        public UnifyResult CheckCompany(CheckCompanyInput input)
        {
            UnifyResult result = new UnifyResult();
            string sqlHas = "select * from AirtelBaoJai where companyname='" + input.CompanyName + "'";
            var res = GetGoalsEntity(sqlHas);
            if (res.Count > 0)
            {
                result.StatusCode = App.Framwork.Result.ResultCode.Error;
                result.Message = "请您重新输入，系统已经存在该委托方名称了哦！";
            }
            else
            {
                result.StatusCode = App.Framwork.Result.ResultCode.Success;
            }

            return result;

        }

        public ShiYanFangAnDto InitShiYanFangAn(XiangMuYuSuanZongBiaoDto data)
        {

            ShiYanFangAnDto fangan = new ShiYanFangAnDto();
            fangan.YanJiuYuSuanQingDan = new YanJiuYuSuanQingDanDto();
            fangan.ZongBingLiShu = data.ZongBingLiShu;
            fangan.ZongShiXian = data.ZongShiXian;
            fangan.QueDingZhongXinShu = data.QueDingZhongXinShu;
            fangan.CompanyName = data.CompanyName;
            fangan.ProjectName = data.ProjectName;

            //CRO 服务费用(包含数统)//税前
            double crofuwufei = (
                 data.QiTaZaFeiZongE.YunShuFei.HeJi
               + data.QiTaZaFeiZongE.ShiJiHaoCaiFei.HeJi
               + data.QiTaZaFeiZongE.XiTongShiYongFei.HeJi
               + data.QiTaZaFeiZongE.ShuJuTongJiFei.HeJi
               + data.QiTaZaFeiZongE.QiTaCaiGouFei.HeJi
               + data.QiTaZaFeiZongE.ZhongXinShiYanShiFei.HeJi
               + data.QiTaZaFeiZongE.ShouShiZheZhaoMuFei.HeJi
               + data.QiTaZaFeiZongE.YiChuanBanTianBaoFei.HeJi
               + data.QiTaZaFeiZongE.QiTaFei.HeJi
               + data.RenGongFeiYongZongE.YiXueZhiChi_ZhuanXieFei.HeJi
               + data.RenGongFeiYongZongE.YiXueZhiChi_JianChaFei.HeJi
               + data.RenGongFeiYongZongE.ZhiKongZhiChiFei.HeJi
               + data.RenGongFeiYongZongE.PVZhiChiFei.HeJi
               + data.RenGongFeiYongZongE.XiangMuJiangLiFei.HeJi
               );
            double shuihou_crofuwufei = crofuwufei + crofuwufei * 0.06;
            fangan.YanJiuYuSuanQingDan.CRO_FuWuFei = (shuihou_crofuwufei / 10000).ToString();

            //日常费用(包含差旅费用) 税前
            double richangfei = (
                data.QiTaZaFeiZongE.JianChaChaiLvFei.HeJi
                + data.QiTaZaFeiZongE.YinShuaFei.HeJi
                + data.QiTaZaFeiZongE.ShengNeiJiChaFei.HeJi
                + data.QiTaZaFeiZongE.ShengWaiJiChaFei.HeJi
                + data.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.HeJi
                + data.RenGongFeiYongZongE.GuanLiRenYuanFei.HeJi
                + data.RenGongFeiYongZongE.PMFei.HeJi
                + data.RenGongFeiYongZongE.PLFei.HeJi
                + data.RenGongFeiYongZongE.CTAFei.HeJi
                + data.RenGongFeiYongZongE.CRAFei.HeJi
                + data.RenGongFeiYongZongE.XieTongJianChaFei.HeJi
                );

            //税后
            double shuihou_richangfei = richangfei + richangfei * 0.06;
            fangan.YanJiuYuSuanQingDan.RiChangFei = (shuihou_richangfei / 10000).ToString();

            //会议费用 税前
            double huiyifei = (
                data.HuiYiFeiZongE.TotalAmount
                + data.QiTaZaFeiZongE.QiDongHuiFei.HeJi
                );
            double shuihou_huiyifei = huiyifei + huiyifei * 0.06;
            fangan.YanJiuYuSuanQingDan.HuiYiFei = (shuihou_huiyifei / 10000).ToString();


            //CRO 服务费合计(税 6%)
            double shuishou_crofuwufeiheji = shuihou_crofuwufei + shuihou_richangfei + shuihou_huiyifei;
            fangan.YanJiuYuSuanQingDan.CRO_FuWuHeJi = (shuishou_crofuwufeiheji / 10000).ToString();

            //医院研究费用 税前
            double yiyuanyuanjiufei = data.YiYuanHeTongFeiZongE.TotalAmount;
            //税后
            double shuihou_yiyuanyuanjiufei = yiyuanyuanjiufei + yiyuanyuanjiufei * 0.06;
            fangan.YanJiuYuSuanQingDan.YiYuanYanJiuFei = (shuihou_yiyuanyuanjiufei / 10000).ToString();   //(shuihou_yiyuanyuanjiufei / 10000).ToString();

            //SMO 费用 税前
            double smofei = data.QiTaZaFeiZongE.SMOFei.HeJi;

            //税后
            double shuihou_smofei = smofei + smofei * 0.06;
            fangan.YanJiuYuSuanQingDan.SMOFei = (shuihou_smofei / 10000).ToString(); //(shuihou_smofei / 10000).ToString();

            //合计
            double hanshuiheji = shuishou_crofuwufeiheji + shuihou_yiyuanyuanjiufei + shuihou_smofei;
            fangan.YanJiuYuSuanQingDan.HanShuiHeJi = (hanshuiheji / 10000).ToString();

            //(一)临床试验服务费用
            fangan.LinChuangShiYanFuWuFei = new LinChuangShiYanFuWuFeiDto();
            double ShaiXuanYanJiuDanWei_1 = crofuwufei * 0.02;
            double LinChuangShiYanFangAnSheJi_1 = crofuwufei * 0.08;

            double CROFuWuFei_1 = crofuwufei * 0.04;
            double CROFuWuFei_2 = crofuwufei * 0.36;
            double CROFuWuFei_3 = crofuwufei * 0.05;
            double CROFuWuFei_4 = crofuwufei * 0.2;
            double CROFuWuFei_5 = crofuwufei * 0.005;
            double CROFuWuFei = CROFuWuFei_1 + CROFuWuFei_2 + CROFuWuFei_3 + CROFuWuFei_4 + CROFuWuFei_5;
            double CROFuWuFei_1_Price = CROFuWuFei_1 / data.QueDingZhongXinShu;
            double CROFuWuFei_3_Price = CROFuWuFei_3 / data.QueDingZhongXinShu;

            double ShuJuGuanLiHeTongJiFenXi_1 = crofuwufei * 0.1;
            double ShuJuGuanLiHeTongJiFenXi_2 = crofuwufei * 0.065;
            double ShuJuGuanLiHeTongJiFenXi = ShuJuGuanLiHeTongJiFenXi_1 + ShuJuGuanLiHeTongJiFenXi_2;

            double ZongJie_1 = crofuwufei * 0.02;
            double ZongJie_2 = crofuwufei * 0.06;
            double ZongJie = ZongJie_1 + ZongJie_2;

            fangan.LinChuangShiYanFuWuFei.ShaiXuanYanJiuDanWei = (ShaiXuanYanJiuDanWei_1 / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.ShaiXuanYanJiuDanWei_1 = (ShaiXuanYanJiuDanWei_1 / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.LinChuangShiYanFangAnSheJi = (LinChuangShiYanFangAnSheJi_1 / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.LinChuangShiYanFangAnSheJi_1 = (LinChuangShiYanFangAnSheJi_1 / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.CROFuWuFei = (CROFuWuFei / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.CROFuWuFei_1 = (CROFuWuFei_1 / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.CROFuWuFei_1_Price = (CROFuWuFei_1_Price).ToString();
            fangan.LinChuangShiYanFuWuFei.CROFuWuFei_2 = (CROFuWuFei_2 / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.CROFuWuFei_3 = (CROFuWuFei_3 / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.CROFuWuFei_3_Price = (CROFuWuFei_3_Price).ToString();
            fangan.LinChuangShiYanFuWuFei.CROFuWuFei_4 = (CROFuWuFei_4 / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.CROFuWuFei_5 = (CROFuWuFei_5 / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.ShuJuGuanLiHeTongJiFenXi = (ShuJuGuanLiHeTongJiFenXi / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.ShuJuGuanLiHeTongJiFenXi_1 = (ShuJuGuanLiHeTongJiFenXi_1 / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.ShuJuGuanLiHeTongJiFenXi_2 = (ShuJuGuanLiHeTongJiFenXi_2 / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.ZongJie = (ZongJie / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.ZongJie_1 = (ZongJie_1 / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.ZongJie_2 = (ZongJie_2 / 10000).ToString();
            fangan.LinChuangShiYanFuWuFei.HeJi = $"{(crofuwufei / 10000).ToString()} 万";
            fangan.LinChuangShiYanFuWuFei.HanShuiHeJi = $"{(shuihou_crofuwufei / 10000).ToString()} 万";

            //(二)日常费用
            fangan.RiChangFei = new RiChangFeiDto();

            //办公费及通讯费 税前
            double bangongtongxun = (
                +data.QiTaZaFeiZongE.ShengNeiJiChaFei.HeJi
                + data.QiTaZaFeiZongE.ShengWaiJiChaFei.HeJi
                + data.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.HeJi
                + data.RenGongFeiYongZongE.GuanLiRenYuanFei.HeJi
                + data.RenGongFeiYongZongE.PMFei.HeJi
                + data.RenGongFeiYongZongE.PLFei.HeJi
                + data.RenGongFeiYongZongE.CTAFei.HeJi
                + data.RenGongFeiYongZongE.CRAFei.HeJi
                + data.RenGongFeiYongZongE.XieTongJianChaFei.HeJi
                );
            fangan.RiChangFei.BanGongTongXun = (bangongtongxun / 10000).ToString();

            //临床试验文件印刷装订费用 税前 
            fangan.RiChangFei.YinShuaZhuangDing = (data.QiTaZaFeiZongE.YinShuaFei.HeJi / 10000).ToString();

            //差旅费 税前
            fangan.RiChangFei.ChaiLv = (data.QiTaZaFeiZongE.JianChaChaiLvFei.HeJi / 10000).ToString();

            //税前
            double richangfeiheji = bangongtongxun + data.QiTaZaFeiZongE.YinShuaFei.HeJi + data.QiTaZaFeiZongE.JianChaChaiLvFei.HeJi;
            fangan.RiChangFei.HeJi = (richangfeiheji / 10000).ToString();

            //税后
            double shiuhuo_richangfeiheji = richangfeiheji + richangfeiheji * 0.06;
            fangan.RiChangFei.HanShuiHeJi = (shiuhuo_richangfeiheji / 10000).ToString();


            //附表1

            fangan.ChaiLvFuFeiBiao = new ChaiLvFuFeiBiaoDto();

            double AdminChaiLvFei = (data.ChaiLvFeiChengBenGuiZeBiao.Admin_BuChuChai_Amount + data.ChaiLvFeiChengBenGuiZeBiao.Admin_ChuChai_Amount);
            double PMChaiLvFei = (data.ChaiLvFeiChengBenGuiZeBiao.PM_BuChuChai_Amount + data.ChaiLvFeiChengBenGuiZeBiao.PM_ChuChai_Amount);
            double CRAChaiLvFei = (data.ChaiLvFeiChengBenGuiZeBiao.CRA_BuChuChai_Amount + data.ChaiLvFeiChengBenGuiZeBiao.CRA_ChuChai_Amount);

            double AdminChaiLvFei_Price = AdminChaiLvFei / data.ZongShiXian;
            double PMChaiLvFei_Price = PMChaiLvFei / data.ZongShiXian;
            double CRAChaiLvFei_Price = CRAChaiLvFei / data.ZongShiXian;

            fangan.ChaiLvFuFeiBiao.AdminChaiLvFei = (AdminChaiLvFei / 10000).ToString();
            fangan.ChaiLvFuFeiBiao.AdminChaiLvFei_Price = AdminChaiLvFei_Price.ToString();
            fangan.ChaiLvFuFeiBiao.PMChaiLvFei = (PMChaiLvFei / 10000).ToString();
            fangan.ChaiLvFuFeiBiao.PMChaiLvFei_Price = PMChaiLvFei_Price.ToString();
            fangan.ChaiLvFuFeiBiao.CRAChaiLvFei = (CRAChaiLvFei / 10000).ToString();
            fangan.ChaiLvFuFeiBiao.CRAChaiLvFei_Price = CRAChaiLvFei_Price.ToString();

            //(三)会议费用
            fangan.HuiYiFei = new HuiYiFeiDto();

            double yantaohui = (
                data.HuiYiFeiZongE.XieTiaoHui.HeJi
                + data.HuiYiFeiZongE.ZongJieHui.HeJi
                + data.HuiYiFeiZongE.ZhongQiHui.HeJi
                );

            fangan.HuiYiFei.YanTaoHui = (yantaohui / 10000).ToString();
            fangan.HuiYiFei.QiDongHui = (data.QiTaZaFeiZongE.QiDongHuiFei.HeJi / 10000).ToString();
            fangan.HuiYiFei.QiDongHui_Price = (data.QiTaZaFeiZongE.QiDongHuiFei.Price).ToString();
            fangan.HuiYiFei.ShenJiHui = (data.HuiYiFeiZongE.MangTaiShenHeHui.HeJi / 10000).ToString();
            fangan.HuiYiFei.HeJi = (huiyifei / 10000).ToString();
            fangan.HuiYiFei.HanShuiHeJi = (shuihou_huiyifei / 10000).ToString();

            //(四) 临床试验研究费用
            fangan.LinChuangShiYanYanJiuFei = new LinChuangShiYanYanJiuFeiDto();

            fangan.LinChuangShiYanYanJiuFei.LunLiShenCha_Amount = (data.YiYuanHeTongFeiZongE.LunLiFei.HeJi / 10000).ToString();
            fangan.LinChuangShiYanYanJiuFei.LunLiShenCha_Count = $"{data.YiYuanHeTongFeiZongE.LunLiFei.Count} 家";
            fangan.LinChuangShiYanYanJiuFei.LunLiShenCha_Price = (data.YiYuanHeTongFeiZongE.LunLiFei.Price / 10000).ToString();

            fangan.LinChuangShiYanYanJiuFei.ZuZhangDanWei_Amount = (data.YiYuanHeTongFeiZongE.ZuZhangFei.HeJi / 10000).ToString();
            fangan.LinChuangShiYanYanJiuFei.ZuZhangDanWei_Count = $"{data.YiYuanHeTongFeiZongE.ZuZhangFei.Count} 家";
            fangan.LinChuangShiYanYanJiuFei.ZuZhangDanWei_Price = (data.YiYuanHeTongFeiZongE.ZuZhangFei.Price / 10000).ToString();

            //研究者费 税前
            double yanjiuzhefei = data.YiYuanHeTongFeiZongE.ShaiXuanBingLiFei.HeJi; 
            double yanjiuzhefei_price = yanjiuzhefei / data.ZongBingLiShu;
            fangan.LinChuangShiYanYanJiuFei.YanJiuZheFei_Amount = (yanjiuzhefei / 10000).ToString();
            fangan.LinChuangShiYanYanJiuFei.YanJiuZheFei_Count = $"{data.ZongBingLiShu} 例";
            fangan.LinChuangShiYanYanJiuFei.YanJiuZheFei_Price = (yanjiuzhefei_price / 10000).ToString();

            //受试者--实验组人数
            int shoushizhe_count1 = (int)Math.Ceiling(data.ZongBingLiShu * (2d / 3d));//向上取整数
            //受试者--对照组人数
            int shoushizhe_count2 = data.ZongBingLiShu - shoushizhe_count1;

            //税前
            double shoushizhe_amount1 = shoushizhe_count1 * data.YiYuanHeTongFeiZongE.QiTaFei.Price;
            //税前
            double shoushizhe_amount2 = shoushizhe_count2 * data.YiYuanHeTongFeiZongE.QiTaFei.Price;

            fangan.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Amount1 = (shoushizhe_amount1 / 10000).ToString();
            fangan.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Count1 = $"{shoushizhe_count1} 例";
            fangan.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Price1 = (data.YiYuanHeTongFeiZongE.QiTaFei.Price / 10000).ToString();
            fangan.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Amount2 = (shoushizhe_amount2 / 10000).ToString();
            fangan.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Count2 = $"{shoushizhe_count2} 例";
            fangan.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Price2 = (data.YiYuanHeTongFeiZongE.QiTaFei.Price / 10000).ToString();

            //受试者检查 税前
            double shoushizhejiancha = data.YiYuanHeTongFeiZongE.HeGeBingLiFei.HeJi;

            fangan.LinChuangShiYanYanJiuFei.ShouShiZheJianCha_Amount = (shoushizhejiancha / 10000).ToString();
            fangan.LinChuangShiYanYanJiuFei.ShouShiZheJianCha_Count = $"{data.ZongBingLiShu} 例";
            fangan.LinChuangShiYanYanJiuFei.ShouShiZheJianCha_Price = (data.YiYuanHeTongFeiZongE.HeGeBingLiFei.Price / 10000).ToString();

            //研究机构管理 税前
            double yanjiujigouguanli = data.YiYuanHeTongFeiZongE.JiGouGuanLiFei.HeJi;
            double yanjiujigouguanli_price = yanjiujigouguanli / data.ZongBingLiShu;
            fangan.LinChuangShiYanYanJiuFei.JiGouGuanLi_Amount = (yanjiujigouguanli / 10000).ToString();
            fangan.LinChuangShiYanYanJiuFei.JiGouGuanLi_Count = $"{data.ZongBingLiShu} 例";
            fangan.LinChuangShiYanYanJiuFei.JiGouGuanLi_Price = (yanjiujigouguanli_price / 10000).ToString();

            fangan.LinChuangShiYanYanJiuFei.BaoXian_Amount = "/";
            fangan.LinChuangShiYanYanJiuFei.BaoXian_Count = $"{data.ZongBingLiShu} 例";
            fangan.LinChuangShiYanYanJiuFei.BaoXian_Price = "/";


            fangan.LinChuangShiYanYanJiuFei.HeJi = (yiyuanyuanjiufei / 10000).ToString();
            fangan.LinChuangShiYanYanJiuFei.HanShuiHeJi = fangan.YanJiuYuSuanQingDan.YiYuanYanJiuFei;


            //smo供应商管理费 税前，按照1/10比例取，剩下的是CRC服务费用
            double smogongyingshang = smofei * 0.1;

            //CRC 服务费用 税前
            double crcfuwu = smofei - smogongyingshang;

            //CRC 服务费用 税后
            double shuihou_crcfuwu = crcfuwu + crcfuwu * 0.06;

            //实验组的金额按照 65%的比例

            double crc_amount1 = crcfuwu * 0.65;

            //实验组的单价
            double crc_price1 = crc_amount1 / shoushizhe_count1;


            //对照组的金额
            double crc_amount2 = crcfuwu - crc_amount1;

            //实验组的单价
            double crc_price2 = crc_amount2 / shoushizhe_count2;

            //折合每例 税后
            double avg = (shuihou_crcfuwu / data.ZongBingLiShu);


            fangan.SMOFei = new SMOFeiDto
            {
                CRC_FuWu_Count1 = $"{shoushizhe_count1} 例",
                CRC_FuWu_Price1 = $"{crc_price1.ToString("##.##")} 元/例",
                CRC_FuWu_Amount1 = (crc_amount1 / 10000).ToString(),
                CRC_FuWu_Count2 = $"{shoushizhe_count2} 例",
                CRC_FuWu_Price2 = $"{crc_price2.ToString("##.##")} 元/例",
                CRC_FuWu_Amount2 = (crc_amount2 / 10000).ToString(),
                SMO_GongYingShangGuanLi = (smogongyingshang / 10000).ToString(),
                HanShuiHeJi = ((smofei + smofei * 0.06) / 10000).ToString(),
                AVG = $"{(avg / 10000).ToString()} 万/例",
                ShouShiZhe_Count = data.ZongBingLiShu,
                Center_Count = data.QueDingZhongXinShu,
                CRC_Count = 1,
                CRC_PM_Count = 1,

            };
            return fangan;
        }


        public string CreateExcel(XiangMuYuSuanZongBiaoDto data)
        {
            Workbook wbToStream = new Workbook();
            #region 表1，项目预算总表（含说明）


            Worksheet sheet = wbToStream.Worksheets[0];
            sheet.Name = "表1，项目预算总表（含说明）";

            CellRange range = sheet.Range["A1:L55"];
            range.BorderAround(LineStyleType.Thin);
            range.BorderInside(LineStyleType.Thin);
            range.Style.VerticalAlignment = VerticalAlignType.Center;//垂直居中
            range.Style.HorizontalAlignment = HorizontalAlignType.Center; // 水平居中
            range.Style.WrapText = true;
            range.RowHeight = 16;
            range.Style.Font.Size = 10;
            range.Style.Font.FontName = "宋体";



            #region row1
            sheet.Range["A1:L1"].Text = "项  目  预  算  表";
            sheet.Range["A1:L1"].Merge();
            sheet.Range["A1:L1"].Style.Font.Size = 14;
            sheet.Range["A1:L1"].Style.Font.IsBold = true;
            sheet.Range["A1:L1"].RowHeight = 21;
            #endregion

            #region row2 
            sheet.Range["A2:B2"].Text = "委托方（全称）：";
            sheet.Range["A2:B2"].Merge();
            sheet.Range["C2:G2"].Text = data.CompanyName;
            sheet.Range["C2:G2"].Style.HorizontalAlignment = HorizontalAlignType.Left;
            sheet.Range["C2:G2"].Merge();
            sheet.Range["H2"].Text = "总病例数（例）：";
            sheet.Range["I2"].NumberValue = data.ZongBingLiShu;
            sheet.Range["I2"].Style.HorizontalAlignment = HorizontalAlignType.Left;


            sheet.Range["J2:K2"].Text = "计划筛选中心数（个）：";
            sheet.Range["J2:K2"].Merge();
            sheet.Range["L2"].NumberValue = data.JiHuaShuaiXuanZhongXinShu;
            sheet.Range["L2"].Style.HorizontalAlignment = HorizontalAlignType.Left;

            sheet.Range["A2:L2"].Style.Font.IsBold = true;
            //sheet.Range["L2"].Style.KnownColor = ExcelColors.Yellow;
            sheet.Range["F11"].Style.Color = Color.FromArgb(255, 255, 0);
            #endregion
            #region row3

            sheet.Range["A3:B3"].Text = "项目名称（全称）：";
            sheet.Range["A3:B3"].Merge();
            sheet.Range["C3:G3"].Text = data.ProjectName;
            sheet.Range["C3:G3"].Style.HorizontalAlignment = HorizontalAlignType.Left;
            sheet.Range["C3:G3"].Merge();

            sheet.Range["H3"].Text = "总时限（月）：";
            sheet.Range["I3"].NumberValue = data.ZongShiXian;
            sheet.Range["I3"].Style.HorizontalAlignment = HorizontalAlignType.Left;

            sheet.Range["J3:K3"].Text = "计划确定中心数（个）：";
            sheet.Range["J3:K3"].Merge();
            sheet.Range["L3"].NumberValue = data.QueDingZhongXinShu;
            sheet.Range["L3"].Style.HorizontalAlignment = HorizontalAlignType.Left;

            sheet.Range["A3:L3"].Style.Font.IsBold = true;
            #endregion

            #region row4 一、会议费用总额(RMB)：
            sheet.Range["A4:B4"].Text = "一、会议费用总额(RMB)：";
            sheet.Range["A4:B4"].Merge();
            sheet.Range["C4:G4"].NumberValue = data.HuiYiFeiZongE.TotalAmount;
            sheet.Range["C4:G4"].Style.HorizontalAlignment = HorizontalAlignType.Left;

            sheet.Range["C4:L4"].Merge();
            sheet.Range["A4:L4"].Style.Font.IsBold = true;
            //sheet.Range["A4:L4"].Style.KnownColor = ExcelColors.Yellow;
            sheet.Range["A4:L4"].Style.Color = Color.FromArgb(255, 255, 0);

            sheet.Range["A5"].Text = "费用项目";
            sheet.Range["B5"].Text = "费用明细";
            sheet.Range["C5"].Text = "金额";
            sheet.Range["A6:A11"].Text = "协调会费用";
            sheet.Range["A6:A11"].IsWrapText = true;
            sheet.Range["A6:A11"].Merge();

            sheet.Range["B6"].Text = "会务费";
            sheet.Range["C6"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.HuiWuFei;

            sheet.Range["B7"].Text = "交通费";
            sheet.Range["C7"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.JiaoTongFei;

            sheet.Range["B8"].Text = "住宿费";
            sheet.Range["C8"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.ZhuSuFei;

            sheet.Range["B9"].Text = "会议室费";
            sheet.Range["C9"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.HuiYiShiFei;

            sheet.Range["B10"].Text = "其他费用";
            sheet.Range["C10"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.QiTaFei;

            sheet.Range["B11"].Text = "合计";
            sheet.Range["C11"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.HeJi;
            //sheet.Range["C11"].Style.KnownColor = ExcelColors.Yellow;
            sheet.Range["C11"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["B11:C11"].Style.Font.IsBold = true;
            sheet.Range["C6:C11"].Style.HorizontalAlignment = HorizontalAlignType.Left;


            sheet.Range["D5"].Text = "费用项目";
            sheet.Range["E5"].Text = "费用明细";
            sheet.Range["F5"].Text = "金额";
            sheet.Range["D6"].Text = "总结会费用";
            sheet.Range["D6:D11"].Merge();

            sheet.Range["E6"].Text = "会务费";
            sheet.Range["F6"].NumberValue = data.HuiYiFeiZongE.ZongJieHui.HuiWuFei;

            sheet.Range["E7"].Text = "交通费";
            sheet.Range["F7"].NumberValue = data.HuiYiFeiZongE.ZongJieHui.JiaoTongFei;

            sheet.Range["E8"].Text = "住宿费";
            sheet.Range["F8"].NumberValue = data.HuiYiFeiZongE.ZongJieHui.ZhuSuFei;

            sheet.Range["E9"].Text = "会议室费";
            sheet.Range["F9"].NumberValue = data.HuiYiFeiZongE.ZongJieHui.HuiYiShiFei;

            sheet.Range["E10"].Text = "其他费用";
            sheet.Range["F10"].NumberValue = data.HuiYiFeiZongE.ZongJieHui.QiTaFei;

            sheet.Range["E11"].Text = "合计";
            sheet.Range["F11"].NumberValue = data.HuiYiFeiZongE.ZongJieHui.HeJi;
            // sheet.Range["F11"].Style.KnownColor = ExcelColors.Yellow;
            sheet.Range["F11"].Style.Color = Color.FromArgb(255, 255, 0);


            sheet.Range["E11:F11"].Style.Font.IsBold = true;
            sheet.Range["F6:F11"].Style.HorizontalAlignment = HorizontalAlignType.Left;


            sheet.Range["G5"].Text = "费用项目";
            sheet.Range["H5"].Text = "费用明细";
            sheet.Range["I5"].Text = "金额";
            sheet.Range["G6"].Text = "中期会（或论证会）";
            sheet.Range["G6:G11"].Merge();
            sheet.Range["H6"].Text = "会务费";
            sheet.Range["I6"].NumberValue = data.HuiYiFeiZongE.ZhongQiHui.HuiWuFei;

            sheet.Range["H7"].Text = "交通费";
            sheet.Range["I7"].NumberValue = data.HuiYiFeiZongE.ZhongQiHui.JiaoTongFei;

            sheet.Range["H8"].Text = "住宿费";
            sheet.Range["I8"].NumberValue = data.HuiYiFeiZongE.ZhongQiHui.ZhuSuFei;

            sheet.Range["H9"].Text = "会议室费";
            sheet.Range["I9"].NumberValue = data.HuiYiFeiZongE.ZhongQiHui.HuiYiShiFei;

            sheet.Range["H10"].Text = "其他费用";
            sheet.Range["I10"].NumberValue = data.HuiYiFeiZongE.ZhongQiHui.QiTaFei;

            sheet.Range["H11"].Text = "合计";
            sheet.Range["J11"].NumberValue = data.HuiYiFeiZongE.ZhongQiHui.HeJi;
            //sheet.Range["J11"].Style.KnownColor = ExcelColors.Yellow;
            sheet.Range["J11"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["H11:J11"].Style.Font.IsBold = true;
            sheet.Range["I6:I11"].Style.HorizontalAlignment = HorizontalAlignType.Left;

            sheet.Range["J5"].Text = "费用项目";
            sheet.Range["K5"].Text = "费用明细";
            sheet.Range["L5"].Text = "金额";
            sheet.Range["J6"].Text = "盲态审核会";
            sheet.Range["J6:J11"].Merge();
            sheet.Range["K6"].Text = "会务费";
            sheet.Range["L6"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.HuiWuFei;

            sheet.Range["K7"].Text = "交通费";
            sheet.Range["L7"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.JiaoTongFei;

            sheet.Range["K8"].Text = "住宿费";
            sheet.Range["L8"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.ZhuSuFei;

            sheet.Range["K9"].Text = "会议室费";
            sheet.Range["L9"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.HuiYiShiFei;

            sheet.Range["K10"].Text = "其他费用";
            sheet.Range["L10"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.QiTaFei;

            sheet.Range["K11"].Text = "合计";
            sheet.Range["L11"].NumberValue = data.HuiYiFeiZongE.MangTaiShenHeHui.HeJi;
            // sheet.Range["L11"].Style.KnownColor = ExcelColors.Yellow;
            sheet.Range["L11"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["K11:L11"].Style.Font.IsBold = true;
            sheet.Range["L6:L11"].Style.HorizontalAlignment = HorizontalAlignType.Left;

            //sheet.Range["A5:L5"].Style.KnownColor = ExcelColors.Gray25Percent;
            //sheet.Range["A6:B11"].Style.KnownColor = ExcelColors.Gray25Percent;
            //sheet.Range["D6:E11"].Style.KnownColor = ExcelColors.Gray25Percent;
            //sheet.Range["G6:H11"].Style.KnownColor = ExcelColors.Gray25Percent;
            //sheet.Range["J6:K11"].Style.KnownColor = ExcelColors.Gray25Percent;

            sheet.Range["A5:L5"].Style.Color = Color.FromArgb(192, 192, 192);
            sheet.Range["A6:B11"].Style.Color = Color.FromArgb(192, 192, 192);
            sheet.Range["D6:E11"].Style.Color = Color.FromArgb(192, 192, 192);
            sheet.Range["G6:H11"].Style.Color = Color.FromArgb(192, 192, 192);
            sheet.Range["J6:K11"].Style.Color = Color.FromArgb(192, 192, 192);


            #endregion

            #region row4 二、医院合同费用总额(RMB)： 
            sheet.Range["A12:B12"].Text = "二、医院合同费用总额(RMB)：";
            sheet.Range["A12:B12"].Merge();

            sheet.Range["C12:D12"].NumberValue = data.YiYuanHeTongFeiZongE.TotalAmount;
            sheet.Range["C12:D12"].Style.HorizontalAlignment = HorizontalAlignType.Left;
            sheet.Range["C12:D12"].Merge();

            sheet.Range["E12:F12"].Text = "医院单例价格：";
            sheet.Range["E12:F12"].Merge();

            sheet.Range["G12:K12"].NumberValue = data.YiYuanHeTongFeiZongE.AvgPrice;
            sheet.Range["G12:K12"].Merge();
            sheet.Range["A12:L12"].Style.Font.IsBold = true;
            //sheet.Range["A12:L12"].Style.KnownColor = ExcelColors.Yellow;
            sheet.Range["A12:L12"].Style.Color = Color.FromArgb(255, 255, 0);

            sheet.Range["A12:L12"].Style.HorizontalAlignment = HorizontalAlignType.Left;


            sheet.Range["A13"].Text = "费用项目";
            sheet.Range["B13"].Text = "费用明细";
            sheet.Range["C13"].Text = "单价";
            sheet.Range["D13"].Text = "频次\r\n（中心数或例数）";
            sheet.Range["E13:K13"].Text = "填写说明";
            sheet.Range["E13:K13"].Merge();
            sheet.Range["L13"].Text = "合计";
            sheet.Range["L13"].Style.Font.IsBold = true;
            sheet.Range["A13:L13"].RowHeight = 25;


            sheet.Range["A14:A19"].Text = "合同费用";
            sheet.Range["A14:A19"].Merge();


            //------------------
            sheet.Range["B14"].Text = "组长费";
            sheet.Range["C14"].NumberValue = data.YiYuanHeTongFeiZongE.ZuZhangFei.Price;
            sheet.Range["D14"].NumberValue = data.YiYuanHeTongFeiZongE.ZuZhangFei.Count;
            sheet.Range["E14:K14"].Text = "依据不同项目类型（或拟选机构要求）而定，结合商务计划书。";
            sheet.Range["E14:K14"].Merge();
            sheet.Range["L14"].NumberValue = data.YiYuanHeTongFeiZongE.ZuZhangFei.HeJi;


            //------------------
            sheet.Range["B15"].Text = "机构管理费";
            sheet.Range["C15"].NumberValue = data.YiYuanHeTongFeiZongE.JiGouGuanLiFei.Price;
            sheet.Range["D15"].NumberValue = data.YiYuanHeTongFeiZongE.JiGouGuanLiFei.Count;
            sheet.Range["E15:K15"].Text = "依据不同项目类型（或拟选机构要求）而定，结合商务计划书与确定中心数。含机构管理、药物管理、CRC管理、质控管理、文档管理等费用。";
            sheet.Range["E15:K15"].Merge();
            sheet.Range["L15"].NumberValue = data.YiYuanHeTongFeiZongE.JiGouGuanLiFei.HeJi;
            sheet.Range["A15:L15"].RowHeight = 27;

            //------------------
            sheet.Range["B16"].Text = "伦理费";
            sheet.Range["C16"].NumberValue = data.YiYuanHeTongFeiZongE.LunLiFei.Price;
            sheet.Range["D16"].NumberValue = data.YiYuanHeTongFeiZongE.LunLiFei.Count;
            sheet.Range["E16:K16"].Text = "按10000元/次标准，结合中心数*2倍=总次数（考虑重审及结题审查）。";
            sheet.Range["E16:K16"].Merge();
            sheet.Range["L16"].NumberValue = data.YiYuanHeTongFeiZongE.LunLiFei.HeJi;

            //------------------
            sheet.Range["B17"].Text = "合格病例费用";
            sheet.Range["C17"].NumberValue = data.YiYuanHeTongFeiZongE.HeGeBingLiFei.Price;
            sheet.Range["D17"].NumberValue = data.YiYuanHeTongFeiZongE.HeGeBingLiFei.Count;
            sheet.Range["E17:K17"].Text = "观察费和检查费(指合同签署支付医院，包括研究者劳务费、检查费、床位费、餐费等)。";
            sheet.Range["E17:K17"].Merge();
            sheet.Range["L17"].NumberValue = data.YiYuanHeTongFeiZongE.HeGeBingLiFei.HeJi;

            //------------------
            sheet.Range["B18"].Text = "筛选病例费";
            sheet.Range["C18"].NumberValue = data.YiYuanHeTongFeiZongE.ShaiXuanBingLiFei.Price;
            sheet.Range["D18"].NumberValue = data.YiYuanHeTongFeiZongE.ShaiXuanBingLiFei.Count;
            //sheet.Range["E18:K18"].Text = "依据不同项目的筛败工作量大小和筛选失败率来确定。预计筛败率为10%。";
            IRichTextString richText1 = sheet.Range["E18:K18"].RichText;
            richText1.Text = "依据不同项目的筛败工作量大小和筛选失败率来确定。预计筛败率为10%。";
            IFont font1 = wbToStream.CreateFont();
            font1.Color = Color.Red;
            richText1.SetFont(24, 32, font1);

            //------------------
            sheet.Range["B19"].Text = "其他费用";
            sheet.Range["C19"].NumberValue = data.YiYuanHeTongFeiZongE.QiTaFei.Price;
            sheet.Range["D19"].NumberValue = data.YiYuanHeTongFeiZongE.QiTaFei.Count;
            sheet.Range["E19:K19"].Text = "指受试者补偿、AE随访费等费用。";
            sheet.Range["E19:K19"].Merge();
            sheet.Range["L19"].NumberValue = data.YiYuanHeTongFeiZongE.QiTaFei.HeJi;


            sheet.Range["A20:B20"].Text = "合   计：";
            sheet.Range["A20:B20"].Merge();
            sheet.Range["A20:B20"].Style.Font.IsBold = true;
            sheet.Range["L20"].Style.Font.IsBold = true;
            sheet.Range["L20"].NumberValue = data.YiYuanHeTongFeiZongE.TotalAmount;
            sheet.Range["E20:K20"].Merge();

            //sheet.Range["A13:L13"].Style.KnownColor = ExcelColors.Gray25Percent;
            //sheet.Range["A14:B20"].Style.KnownColor = ExcelColors.Gray25Percent;

            sheet.Range["A13:L13"].Style.Color = Color.FromArgb(192, 192, 192);
            sheet.Range["A14:B20"].Style.Color = Color.FromArgb(192, 192, 192);


            //sheet.Range["C15:D20"].Style.KnownColor = ExcelColors.Yellow;
            //sheet.Range["E20:L20"].Style.KnownColor = ExcelColors.Yellow;
            //sheet.Range["L14:L20"].Style.KnownColor = ExcelColors.Yellow;

            sheet.Range["C15:D20"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["E20:L20"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["L14:L20"].Style.Color = Color.FromArgb(255, 255, 0);

            sheet.Range["C16"].Style.KnownColor = ExcelColors.White;


            sheet.Range["C14:L20"].Style.HorizontalAlignment = HorizontalAlignType.Left;
            #endregion



            #region row5 三、其它杂费总额(RMB)：

            sheet.Range["A21:B21"].Text = "三、其它杂费总额(RMB)：";
            sheet.Range["A21:B21"].Merge();

            sheet.Range["C21:L21"].NumberValue = data.QiTaZaFeiZongE.TotalAmount;
            sheet.Range["C21:L21"].Style.HorizontalAlignment = HorizontalAlignType.Left;
            sheet.Range["C21:L21"].Merge();
            //sheet.Range["A21:L21"].Style.KnownColor = ExcelColors.Yellow;
            sheet.Range["A21:L21"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["A21:L21"].Style.Font.IsBold = true;


            sheet.Range["A22"].Text = "费用项目";
            sheet.Range["B22"].Text = "费用明细";
            sheet.Range["C22"].Text = "单价";
            sheet.Range["D22"].Text = "频次\r\n（中心数或例数或次数）";
            sheet.Range["E22:K22"].Text = "填写说明";
            sheet.Range["E22:K22"].Merge();
            sheet.Range["L22"].Text = "合计";
            sheet.Range["L22"].Style.Font.IsBold = true;
            sheet.Range["A22:L22"].RowHeight = 25;

            sheet.Range["A23:A38"].Text = "其他费用";
            sheet.Range["A23:A38"].Merge();

            //------------------
            sheet.Range["B23"].Text = "启动会费";
            sheet.Range["C23"].NumberValue = data.QiTaZaFeiZongE.QiDongHuiFei.Price;
            sheet.Range["D23"].NumberValue = data.QiTaZaFeiZongE.QiDongHuiFei.Count;
            sheet.Range["E23:K23"].Text = "依据中心数确定，通常每中心2000元。";
            sheet.Range["E23:K23"].Merge();
            sheet.Range["L23"].NumberValue = data.QiTaZaFeiZongE.QiDongHuiFei.HeJi;

            //------------------
            sheet.Range["B24"].Text = "运输费";
            sheet.Range["C24"].NumberValue = data.QiTaZaFeiZongE.YunShuFei.Price;
            sheet.Range["D24"].NumberValue = data.QiTaZaFeiZongE.YunShuFei.Count;
            sheet.Range["E24:K24"].Text = "依据中心数确定，通常每中心2000元。需根据具体项目要求来预算。";
            sheet.Range["E24:K24"].Merge();
            sheet.Range["L24"].NumberValue = data.QiTaZaFeiZongE.YunShuFei.HeJi;

            //------------------
            sheet.Range["B25"].Text = "监查差旅费";
            sheet.Range["C25"].NumberValue = data.QiTaZaFeiZongE.JianChaChaiLvFei.Price;
            sheet.Range["D25"].NumberValue = data.QiTaZaFeiZongE.JianChaChaiLvFei.Count;
            sheet.Range["E25:K25"].Text = "见下表计算方法。";
            sheet.Range["E25:K25"].Merge();
            sheet.Range["L25"].NumberValue = data.QiTaZaFeiZongE.JianChaChaiLvFei.HeJi;

            //------------------
            sheet.Range["B26"].Text = "试剂、耗材费";
            sheet.Range["C26"].NumberValue = data.QiTaZaFeiZongE.ShiJiHaoCaiFei.Price;
            sheet.Range["D26"].NumberValue = data.QiTaZaFeiZongE.ShiJiHaoCaiFei.Count;
            sheet.Range["E26:K26"].Text = "依据合同计划书，结合项目和选择医院的情况确定。如签在医院合中，则预算在上述第二部分中。";
            sheet.Range["E26:K26"].Merge();
            sheet.Range["L26"].NumberValue = data.QiTaZaFeiZongE.ShiJiHaoCaiFei.HeJi;
            sheet.Range["A26:L26"].RowHeight = 27;

            //------------------
            sheet.Range["B27"].Text = "中心实验室";
            sheet.Range["C27"].NumberValue = data.QiTaZaFeiZongE.ZhongXinShiYanShiFei.Price;
            sheet.Range["D27"].NumberValue = data.QiTaZaFeiZongE.ZhongXinShiYanShiFei.Count;
            sheet.Range["E27:K27"].Text = "中心阅片按照800-1200/例。";
            sheet.Range["E27:K27"].Merge();
            sheet.Range["L27"].NumberValue = data.QiTaZaFeiZongE.ZhongXinShiYanShiFei.HeJi;
            sheet.Range["B27:L27"].Style.Font.IsBold = true;

            //------------------
            sheet.Range["B28"].Text = "数据、统计费";
            sheet.Range["C28"].NumberValue = data.QiTaZaFeiZongE.ShuJuTongJiFei.Price;
            sheet.Range["D28"].NumberValue = data.QiTaZaFeiZongE.ShuJuTongJiFei.Count;
            sheet.Range["E28:K28"].Text = "依据合同计划书确定。";
            sheet.Range["E28:K28"].Merge();
            sheet.Range["L28"].NumberValue = data.QiTaZaFeiZongE.ShuJuTongJiFei.HeJi;
            sheet.Range["B28:L28"].Style.Font.IsBold = true;

            //------------------
            sheet.Range["B29"].Text = "系统使用费";
            sheet.Range["C29"].NumberValue = data.QiTaZaFeiZongE.XiTongShiYongFei.Price;
            sheet.Range["D29"].NumberValue = data.QiTaZaFeiZongE.XiTongShiYongFei.Count;
            sheet.Range["E29:K29"].Text = "按需选择，EDC、IWRS、ePRO等相关系统租用费。";
            sheet.Range["E29:K29"].Merge();
            sheet.Range["L29"].NumberValue = data.QiTaZaFeiZongE.XiTongShiYongFei.HeJi;

            //------------------
            sheet.Range["B30"].Text = "印刷费";
            sheet.Range["C30"].NumberValue = data.QiTaZaFeiZongE.YinShuaFei.Price;
            sheet.Range["D30"].NumberValue = data.QiTaZaFeiZongE.YinShuaFei.Count;
            sheet.Range["E30:K30"].Text = "依据中心数确定，通常每中心3000元。";
            sheet.Range["E30:K30"].Merge();
            sheet.Range["L30"].NumberValue = data.QiTaZaFeiZongE.YinShuaFei.HeJi;

            //------------------
            sheet.Range["B31"].Text = "其他采购";
            sheet.Range["C31"].NumberValue = data.QiTaZaFeiZongE.QiTaCaiGouFei.Price;
            sheet.Range["D31"].NumberValue = data.QiTaZaFeiZongE.QiTaCaiGouFei.Count;
            sheet.Range["E31:K31"].Text = "依据合同计划书确定（根据试验需要采购的冰箱、温度计、离心机等设备）。";
            sheet.Range["E31:K31"].Merge();
            sheet.Range["L31"].NumberValue = data.QiTaZaFeiZongE.QiTaCaiGouFei.HeJi;

            //------------------
            sheet.Range["B32"].Text = "受试者招募费";
            sheet.Range["C32"].NumberValue = data.QiTaZaFeiZongE.ShouShiZheZhaoMuFei.Price;
            sheet.Range["D32"].NumberValue = data.QiTaZaFeiZongE.ShouShiZheZhaoMuFei.Count;
            sheet.Range["E32:K32"].Text = "依据合同计划书和项目实际情况进行预算，包括委外招募和研究过程协调促进等费用。";
            sheet.Range["E32:K32"].Merge();
            sheet.Range["L32"].NumberValue = data.QiTaZaFeiZongE.ShouShiZheZhaoMuFei.HeJi;

            //------------------
            sheet.Range["B33:B34"].Text = "稽查费";
            sheet.Range["B33:B34"].Merge();

            sheet.Range["C33"].NumberValue = data.QiTaZaFeiZongE.ShengNeiJiChaFei.Price;
            sheet.Range["D33"].NumberValue = data.QiTaZaFeiZongE.ShengNeiJiChaFei.Count;
            sheet.Range["E33:K33"].Text = "根据合同计划书约定范围进行计算，省内稽查按15000元/次。";
            sheet.Range["E33:K33"].Merge();
            sheet.Range["L33"].NumberValue = data.QiTaZaFeiZongE.ShengNeiJiChaFei.HeJi;

            sheet.Range["C34"].NumberValue = data.QiTaZaFeiZongE.ShengWaiJiChaFei.Price;
            sheet.Range["D34"].NumberValue = data.QiTaZaFeiZongE.ShengWaiJiChaFei.Count;
            sheet.Range["E34:K34"].Text = "根据合同计划书约定范围进行计算，出差省外稽查按20000元/次。";
            sheet.Range["E34:K34"].Merge();
            sheet.Range["L33:L34"].NumberValue = data.QiTaZaFeiZongE.ShengNeiJiChaFei.HeJi + data.QiTaZaFeiZongE.ShengWaiJiChaFei.HeJi;
            sheet.Range["L33:L34"].Merge();
            //------------------
            sheet.Range["B35"].Text = "SMO费用";
            sheet.Range["C35"].NumberValue = data.QiTaZaFeiZongE.SMOFei.Price;
            sheet.Range["D35"].NumberValue = data.QiTaZaFeiZongE.SMOFei.Count;
            sheet.Range["E35:K35"].Text = "依据合同计划书确定。";
            sheet.Range["E35:K35"].Merge();
            sheet.Range["L35"].NumberValue = data.QiTaZaFeiZongE.SMOFei.HeJi;
            sheet.Range["B35:L35"].Style.Font.IsBold = true;

            //------------------
            sheet.Range["B36"].Text = "委外监查服务";
            sheet.Range["C36"].NumberValue = data.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.Price;
            sheet.Range["D36"].NumberValue = data.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.Count;
            sheet.Range["E36:K36"].Text = "按附表2进行测算。将预计委托外部公司的病例数进行拆分计算。";
            sheet.Range["E36:K36"].Merge();
            sheet.Range["L36"].NumberValue = data.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.HeJi;
            sheet.Range["B26:L26"].Style.Font.IsBold = true;

            //------------------
            sheet.Range["B37"].Text = "遗传办填报";
            sheet.Range["C37"].NumberValue = data.QiTaZaFeiZongE.YiChuanBanTianBaoFei.Price;
            sheet.Range["D37"].NumberValue = data.QiTaZaFeiZongE.YiChuanBanTianBaoFei.Count;
            sheet.Range["E37:K37"].Text = "按需选择。5家中心以内3W，中心增加可视情况增加";
            sheet.Range["E37:K37"].Merge();
            sheet.Range["L37"].NumberValue = data.QiTaZaFeiZongE.YiChuanBanTianBaoFei.HeJi;

            //------------------
            sheet.Range["B38"].Text = "其他费用";
            sheet.Range["C38"].NumberValue = data.QiTaZaFeiZongE.QiTaFei.Price;
            sheet.Range["D38"].NumberValue = data.QiTaZaFeiZongE.QiTaFei.Count;
            sheet.Range["E38:K38"].Text = "指无法确定的费用，按中心预留，小包通常每中心2000元。大包根据项目难度确定。";
            sheet.Range["E38:K38"].Merge();
            sheet.Range["L38"].NumberValue = data.QiTaZaFeiZongE.QiTaFei.HeJi;
            sheet.Range["B38:L38"].Style.Font.Color = Color.Red;



            sheet.Range["A39:B39"].Text = "合   计：";
            sheet.Range["A39:B39"].Merge();
            sheet.Range["L39"].NumberValue = data.QiTaZaFeiZongE.TotalAmount;

            sheet.Range["E39:K39"].Merge();
            sheet.Range["A39:B39"].Style.Font.IsBold = true;
            sheet.Range["L39"].Style.Font.IsBold = true;
            sheet.Range["L39"].NumberValue = data.QiTaZaFeiZongE.TotalAmount;


            //sheet.Range["A22:L22"].Style.KnownColor = ExcelColors.Gray25Percent;
            //sheet.Range["A23:B39"].Style.KnownColor = ExcelColors.Gray25Percent;

            sheet.Range["A22:L22"].Style.Color = Color.FromArgb(192, 192, 192);
            sheet.Range["A23:B39"].Style.Color = Color.FromArgb(192, 192, 192);

            //sheet.Range["C25"].Style.KnownColor = ExcelColors.Yellow;
            //sheet.Range["D23:D25"].Style.KnownColor = ExcelColors.Yellow;
            //sheet.Range["D27:D30"].Style.KnownColor = ExcelColors.Yellow;
            //sheet.Range["D35"].Style.KnownColor = ExcelColors.Yellow;
            //sheet.Range["L23:L39"].Style.KnownColor = ExcelColors.Yellow;
            //sheet.Range["C39:L39"].Style.KnownColor = ExcelColors.Yellow;

            sheet.Range["C25"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["D23:D25"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["D27:D30"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["D35"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["L23:L39"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["C39:L39"].Style.Color = Color.FromArgb(255, 255, 0);

            sheet.Range["C23:L39"].Style.HorizontalAlignment = HorizontalAlignType.Left;
            #endregion


            #region row6 四、人工费用总额(RMB)：

            sheet.Range["A40:B40"].Text = "四、人工费用总额(RMB)：";
            sheet.Range["A40:B40"].Merge();

            sheet.Range["C40:L40"].NumberValue = data.RenGongFeiYongZongE.TotalAmount;
            sheet.Range["C40:L40"].Style.HorizontalAlignment = HorizontalAlignType.Left;
            sheet.Range["C40:L40"].Merge();
            //sheet.Range["A40:L40"].Style.KnownColor = ExcelColors.Yellow;
            sheet.Range["A40:L40"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["A40:L40"].Style.Font.IsBold = true;


            sheet.Range["A41"].Text = "费用项目";
            sheet.Range["B41"].Text = "费用明细";
            sheet.Range["C41"].Text = "单价";
            sheet.Range["D41"].Text = "频次\r\n（月数/中心/例数）";
            sheet.Range["E41:K41"].Text = "填写说明";
            sheet.Range["E41:K41"].Merge();
            sheet.Range["L41"].Text = "合计";
            sheet.Range["L41"].Style.Font.IsBold = true;
            sheet.Range["A41:L41"].RowHeight = 25;

            sheet.Range["A42:A52"].Text = "人工费用";
            sheet.Range["A42:A52"].Merge();

            //------------------
            sheet.Range["B42"].Text = "管理人员";
            sheet.Range["C42"].NumberValue = data.RenGongFeiYongZongE.GuanLiRenYuanFei.Price;
            sheet.Range["D42"].NumberValue = data.RenGongFeiYongZongE.GuanLiRenYuanFei.Count;
            sheet.Range["E42:K42"].Text = "简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。";
            sheet.Range["E42:K42"].Merge();
            sheet.Range["L42"].NumberValue = data.RenGongFeiYongZongE.GuanLiRenYuanFei.HeJi;

            //------------------
            sheet.Range["B43"].Text = "项目经理（PM）";
            sheet.Range["C43"].NumberValue = data.RenGongFeiYongZongE.PMFei.Price;
            sheet.Range["D43"].NumberValue = data.RenGongFeiYongZongE.PMFei.Count;
            sheet.Range["E43:K43"].Text = "依据项目总时限和项目经理平均承担项目数量，基准为8000元-15000元/月*项目总时长（月）。";
            sheet.Range["E43:K43"].Merge();
            sheet.Range["L43"].NumberValue = data.RenGongFeiYongZongE.PMFei.HeJi;

            //------------------
            sheet.Range["B44"].Text = "项目组长（PL）";
            sheet.Range["C44"].NumberValue = data.RenGongFeiYongZongE.PLFei.Price;
            sheet.Range["D44"].NumberValue = data.RenGongFeiYongZongE.PLFei.Count;
            sheet.Range["E44:K44"].Text = "依据项目总时限和项目备配的情况，基准为6000元-10000元/月*项目总时长（月）。";
            sheet.Range["E44:K44"].Merge();
            sheet.Range["L44"].NumberValue = data.RenGongFeiYongZongE.PLFei.HeJi;

            //------------------
            sheet.Range["B45"].Text = "项目助理（CTA）";
            sheet.Range["C45"].NumberValue = data.RenGongFeiYongZongE.CTAFei.Price;
            sheet.Range["D45"].NumberValue = data.RenGongFeiYongZongE.CTAFei.Count;
            sheet.Range["E45:K45"].Text = "依据项目总时限和项目备配的情况，基准为3000元-5000元/月*项目总时长（月）。";
            sheet.Range["E45:K45"].Merge();
            sheet.Range["L45"].NumberValue = data.RenGongFeiYongZongE.CTAFei.HeJi;

            //------------------
            sheet.Range["B46"].Text = "监查服务（CRA）大临床";
            sheet.Range["C46"].NumberValue = data.RenGongFeiYongZongE.CRAFei.Price;
            sheet.Range["D46"].NumberValue = data.RenGongFeiYongZongE.CRAFei.Count;
            sheet.Range["E46:K46"].Text = "按附表2进行测算。";
            sheet.Range["E46:K46"].Merge();
            sheet.Range["L46"].NumberValue = data.RenGongFeiYongZongE.CRAFei.HeJi;

            //------------------
            sheet.Range["B47"].Text = "协同监查";
            sheet.Range["C47"].NumberValue = data.RenGongFeiYongZongE.XieTongJianChaFei.Price;
            sheet.Range["D47"].NumberValue = data.RenGongFeiYongZongE.XieTongJianChaFei.Count;
            sheet.Range["E47:K47"].Text = "包括PM、LM、QC三种角色的协同访视，通常每中心按2次计算。";
            sheet.Range["E47:K47"].Merge();
            sheet.Range["L47"].NumberValue = data.RenGongFeiYongZongE.XieTongJianChaFei.HeJi;

            //------------------
            sheet.Range["B48"].Text = "医学支持（撰写）";
            sheet.Range["C48"].NumberValue = data.RenGongFeiYongZongE.YiXueZhiChi_ZhuanXieFei.Price;
            sheet.Range["D48"].NumberValue = data.RenGongFeiYongZongE.YiXueZhiChi_ZhuanXieFei.Count;
            sheet.Range["E48:K48"].Text = "简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。";
            sheet.Range["E48:K48"].Merge();
            sheet.Range["L48"].NumberValue = data.RenGongFeiYongZongE.YiXueZhiChi_ZhuanXieFei.HeJi;

            //------------------
            sheet.Range["B49"].Text = "医学支持（监查）";
            sheet.Range["C49"].NumberValue = data.RenGongFeiYongZongE.YiXueZhiChi_JianChaFei.Price;
            sheet.Range["D49"].NumberValue = data.RenGongFeiYongZongE.YiXueZhiChi_JianChaFei.Count;
            sheet.Range["E49:K49"].Text = "每次按10000元计算。";
            sheet.Range["E49:K49"].Merge();
            sheet.Range["L49"].NumberValue = data.RenGongFeiYongZongE.YiXueZhiChi_JianChaFei.HeJi;

            //------------------
            sheet.Range["B50"].Text = "质控支持";
            sheet.Range["C50"].NumberValue = data.RenGongFeiYongZongE.ZhiKongZhiChiFei.Price;
            sheet.Range["D50"].NumberValue = data.RenGongFeiYongZongE.ZhiKongZhiChiFei.Count;
            sheet.Range["E50:K50"].Text = "简单、普通项目3万/项目；稍难、困难项目5万/项目；特别级项目7万/项目。";
            sheet.Range["E50:K50"].Merge();
            sheet.Range["L50"].NumberValue = data.RenGongFeiYongZongE.ZhiKongZhiChiFei.HeJi;

            //------------------
            sheet.Range["B51"].Text = "PV支持";
            sheet.Range["C51"].NumberValue = data.RenGongFeiYongZongE.PVZhiChiFei.Price;
            sheet.Range["D51"].NumberValue = data.RenGongFeiYongZongE.PVZhiChiFei.Count;
            sheet.Range["E51:K51"].Text = "需要根据项目合同和计划书确定。";
            sheet.Range["E51:K51"].Merge();
            sheet.Range["L51"].NumberValue = data.RenGongFeiYongZongE.PVZhiChiFei.HeJi;

            //------------------
            sheet.Range["B52"].Text = "项目奖励";
            sheet.Range["C52"].NumberValue = data.RenGongFeiYongZongE.XiangMuJiangLiFei.Price;
            sheet.Range["D52"].NumberValue = data.RenGongFeiYongZongE.XiangMuJiangLiFei.Count;
            sheet.Range["E52:K52"].Text = "合同系数：＜200W，0.4；200-399W，0.5；400-599W，0.6；600-899W，0.7；900-1199W，0.8；1200-1500W，0.9。";
            sheet.Range["E52:K52"].Merge();
            sheet.Range["L52"].NumberValue = data.RenGongFeiYongZongE.XiangMuJiangLiFei.HeJi;

            sheet.Range["A53:B53"].Text = "合   计：";
            sheet.Range["A53:B53"].Merge();
            sheet.Range["L53"].NumberValue = data.RenGongFeiYongZongE.TotalAmount;

            sheet.Range["E53:K53"].Merge();
            sheet.Range["A53:B53"].Style.Font.IsBold = true;
            sheet.Range["L53"].Style.Font.IsBold = true;
            sheet.Range["L53"].NumberValue = data.RenGongFeiYongZongE.TotalAmount;

            //sheet.Range["A41:L41"].Style.KnownColor = ExcelColors.Gray25Percent;
            //sheet.Range["A42:B53"].Style.KnownColor = ExcelColors.Gray25Percent;

            sheet.Range["A41:L41"].Style.Color = Color.FromArgb(192, 192, 192);
            sheet.Range["A42:B53"].Style.Color = Color.FromArgb(192, 192, 192);


            //sheet.Range["C46"].Style.KnownColor = ExcelColors.Yellow;
            //sheet.Range["D42:D50"].Style.KnownColor = ExcelColors.Yellow;
            //sheet.Range["D52"].Style.KnownColor = ExcelColors.Yellow;
            //sheet.Range["L42:L53"].Style.KnownColor = ExcelColors.Yellow;
            //sheet.Range["C53:L53"].Style.KnownColor = ExcelColors.Yellow;

            sheet.Range["C46"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["D42:D50"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["D52"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["L42:L53"].Style.Color = Color.FromArgb(255, 255, 0);
            sheet.Range["C53:L53"].Style.Color = Color.FromArgb(255, 255, 0);

            sheet.Range["C42:L53"].Style.HorizontalAlignment = HorizontalAlignType.Left;

            #endregion

            #region row7 预算费用总金额(RMB)：

            sheet.Range["A54:B54"].Text = "预算费用总金额(RMB)：";
            sheet.Range["A54:B54"].Merge();
            sheet.Range["A54:B54"].Style.Font.IsBold = true;

            sheet.Range["C54"].NumberValue = data.TotalAmount;
            sheet.Range["C54"].Style.HorizontalAlignment = HorizontalAlignType.Left;
            sheet.Range["E54:K54"].Merge();
            //sheet.Range["A54:C54"].Style.KnownColor = ExcelColors.Yellow;
            sheet.Range["A54:C54"].Style.Color = Color.FromArgb(255, 255, 0);

            sheet.Range["A55:L55"].Text = "注：标黄色的单元格已设定公式，不能修改。";
            sheet.Range["A55:L55"].Merge();
            sheet.Range["A55:L55"].RowHeight = 40;
            sheet.Range["A55:L55"].Style.HorizontalAlignment = HorizontalAlignType.Left;
            sheet.Range["A55:L55"].Style.Font.Color = Color.Red;
            #endregion


            CellRange range1_1 = sheet.Range["N1:X39"];
            range1_1.Style.VerticalAlignment = VerticalAlignType.Center;//垂直居中
            range1_1.RowHeight = 16;
            range1_1.Style.Font.Size = 10;
            range1_1.Style.Font.FontName = "宋体";

            #region 单例观察费 

            sheet.Range["N1"].Text = "单例观察费";
            sheet.Range["O1"].NumberValue = data.DanLiGuanChaFei.TotalAmount;

            sheet.Range["N2:O2"].Text = "协议";
            sheet.Range["N2:O2"].Merge();
            sheet.Range["N2:O2"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["P2:P3"].Text = "非协议";
            sheet.Range["P2:P3"].Merge();
            sheet.Range["N3"].Text = "筛选期";
            sheet.Range["O3"].Text = "入组";
            sheet.Range["N4"].NumberValue = data.DanLiGuanChaFei.ShaiXuanQi;
            sheet.Range["O4"].NumberValue = data.DanLiGuanChaFei.RuZu;
            sheet.Range["P4"].NumberValue = data.DanLiGuanChaFei.FeiXieYi;
            //sheet.Range["N1:P4"].Style.KnownColor = ExcelColors.LightOrange;
            sheet.Range["N1:P4"].Style.Color = Color.FromArgb(253, 233, 217);
            sheet.Range["N1:P4"].BorderAround(LineStyleType.Thin);
            sheet.Range["N1:P4"].BorderInside(LineStyleType.Thin);

            sheet.Range["N1:O1"].Style.Font.IsBold = true;
            sheet.Range["N4:P4"].Style.Font.IsBold = true;

            #endregion

            #region 单例检查费
            sheet.Range["N6"].Text = "单例检查费";
            sheet.Range["O6:U6"].NumberValue = data.DanLiJIanChaFei.TotalAmount;
            sheet.Range["O6:U6"].HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["O6:U6"].Merge();
            //sheet.Range["N6:U6"].Style.KnownColor = ExcelColors.SkyBlue;
            sheet.Range["N6:U6"].Style.Color = Color.FromArgb(218, 238, 243);
            sheet.Range["N6:U6"].BorderAround(LineStyleType.Thin);
            sheet.Range["N6:U6"].BorderInside(LineStyleType.Thin);


            sheet.Range["N7"].Text = "筛选期检查费";
            sheet.Range["O7"].NumberValue = data.DanLiJIanChaFei.ShaiXuanQiJianChaFei;
            sheet.Range["O7:Q7"].Merge();
            sheet.Range["O7:Q7"].HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["T7"].Text = "入组后检查费";
            sheet.Range["U7"].NumberValue = data.DanLiJIanChaFei.RuZuHouJIanChaFei;
            sheet.Range["U7:W7"].Merge();
            sheet.Range["U7:W7"].HorizontalAlignment = HorizontalAlignType.Center;
            sheet.Range["N8"].Text = "检查项目";
            sheet.Range["O8"].Text = "单价(元)";
            sheet.Range["P8"].Text = "次数";
            sheet.Range["Q8"].Text = "小计(元)";

            sheet.Range["T8"].Text = "检查项目";
            sheet.Range["U8"].Text = "次数";
            sheet.Range["V8"].Text = "单价(元)";
            sheet.Range["W8"].Text = "小计(元)";

            sheet.Range["N9"].Text = "血常规";
            sheet.Range["O9"].NumberValue = 50;
            sheet.Range["P9"].NumberValue = 1;
            sheet.Range["Q9"].NumberValue = 50;
            sheet.Range["T9"].Text = "血常规";
            sheet.Range["U9"].NumberValue = 50;
            sheet.Range["V9"].NumberValue = 2;
            sheet.Range["W9"].NumberValue = 100;

            sheet.Range["N10"].Text = "肝功能";
            sheet.Range["O10"].NumberValue = 0;
            sheet.Range["P10"].NumberValue = 0;
            sheet.Range["Q10"].NumberValue = 0;
            sheet.Range["T10"].Text = "肝功能";
            sheet.Range["U10"].NumberValue = 0;
            sheet.Range["V10"].NumberValue = 0;
            sheet.Range["W10"].NumberValue = 0;

            sheet.Range["N11"].Text = "肾功能";
            sheet.Range["O11"].NumberValue = 0;
            sheet.Range["P11"].NumberValue = 0;
            sheet.Range["Q11"].NumberValue = 0;
            sheet.Range["T11"].Text = "肾功能";
            sheet.Range["U11"].NumberValue = 0;
            sheet.Range["V11"].NumberValue = 0;
            sheet.Range["W11"].NumberValue = 0;

            sheet.Range["N12"].Text = "凝血四项";
            sheet.Range["O12"].NumberValue = 0;
            sheet.Range["P12"].NumberValue = 0;
            sheet.Range["Q12"].NumberValue = 0;
            sheet.Range["T12"].Text = "凝血四项";
            sheet.Range["U12"].NumberValue = 0;
            sheet.Range["V12"].NumberValue = 0;
            sheet.Range["W12"].NumberValue = 0;

            sheet.Range["N13"].Text = "输血四项";
            sheet.Range["O13"].NumberValue = 0;
            sheet.Range["P13"].NumberValue = 0;
            sheet.Range["Q13"].NumberValue = 0;
            sheet.Range["T13"].Text = "输血四项";
            sheet.Range["U13"].NumberValue = 0;
            sheet.Range["V13"].NumberValue = 0;
            sheet.Range["W13"].NumberValue = 0;

            sheet.Range["N14"].Text = "血妊娠";
            sheet.Range["O14"].NumberValue = 0;
            sheet.Range["P14"].NumberValue = 0;
            sheet.Range["Q14"].NumberValue = 0;
            sheet.Range["T14"].Text = "血妊娠";
            sheet.Range["U14"].NumberValue = 0;
            sheet.Range["V14"].NumberValue = 0;
            sheet.Range["W14"].NumberValue = 0;

            sheet.Range["N15"].Text = "尿妊娠";
            sheet.Range["O15"].NumberValue = 0;
            sheet.Range["P15"].NumberValue = 0;
            sheet.Range["Q15"].NumberValue = 0;
            sheet.Range["T15"].Text = "尿妊娠";
            sheet.Range["U15"].NumberValue = 0;
            sheet.Range["V15"].NumberValue = 0;
            sheet.Range["W15"].NumberValue = 0;

            sheet.Range["N16"].Text = "心电图";
            sheet.Range["O16"].NumberValue = 0;
            sheet.Range["P16"].NumberValue = 0;
            sheet.Range["Q16"].NumberValue = 0;
            sheet.Range["T16"].Text = "心电图";
            sheet.Range["U16"].NumberValue = 0;
            sheet.Range["V16"].NumberValue = 0;
            sheet.Range["W16"].NumberValue = 0;

            sheet.Range["N17"].Text = "尿常规";
            sheet.Range["O17"].NumberValue = 0;
            sheet.Range["P17"].NumberValue = 0;
            sheet.Range["Q17"].NumberValue = 0;
            sheet.Range["T17"].Text = "尿常规";
            sheet.Range["U17"].NumberValue = 0;
            sheet.Range["V17"].NumberValue = 0;
            sheet.Range["W17"].NumberValue = 0;



            sheet.Range["N6:U7"].Style.Font.IsBold = true;

            //sheet.Range["N7:Q17"].Style.KnownColor = ExcelColors.SkyBlue;
            sheet.Range["N7:Q17"].Style.Color = Color.FromArgb(218, 238, 243);
            sheet.Range["N7:Q17"].BorderAround(LineStyleType.Thin);
            sheet.Range["N7:Q17"].BorderInside(LineStyleType.Thin);

            //sheet.Range["T7:W17"].Style.KnownColor = ExcelColors.SkyBlue;
            sheet.Range["T7:W17"].Style.Color = Color.FromArgb(218, 238, 243);
            sheet.Range["T7:W17"].BorderAround(LineStyleType.Thin);
            sheet.Range["T7:W17"].BorderInside(LineStyleType.Thin);

            #endregion

            #region 单例受试者补助

            sheet.Range["N23"].Text = "单例受试者补助";
            sheet.Range["O23"].NumberValue = data.DanLiShouShiZheBuZhu.DanLiShouShiZheBuZhu_Price;
            sheet.Range["N23:O23"].Style.Font.IsBold = true;
            sheet.Range["N24"].Text = "V1";
            sheet.Range["O24"].NumberValue = data.DanLiShouShiZheBuZhu.V1;
            sheet.Range["N25"].Text = "V2";
            sheet.Range["O25"].NumberValue = data.DanLiShouShiZheBuZhu.V2;
            sheet.Range["N26"].Text = "V3";
            sheet.Range["O26"].NumberValue = data.DanLiShouShiZheBuZhu.V3;
            sheet.Range["N27"].Text = "V4";
            sheet.Range["O27"].NumberValue = data.DanLiShouShiZheBuZhu.V4;
            sheet.Range["N28"].Text = "V5";
            sheet.Range["O28"].NumberValue = data.DanLiShouShiZheBuZhu.V5;

            //sheet.Range["N23:O28"].Style.KnownColor = ExcelColors.Gray25Percent;
            sheet.Range["N23:O28"].Style.Color = Color.FromArgb(228, 223, 236);
            sheet.Range["N23:O28"].BorderAround(LineStyleType.Thin);
            sheet.Range["N23:O28"].BorderInside(LineStyleType.Thin);

            #endregion


            #region 附：差旅费成本预算规则

            sheet.Range["N31:X31"].Text = "附：差旅费成本预算规则";
            sheet.Range["N31:X31"].Merge();
            sheet.Range["N32"].Text = "职位";
            sheet.Range["O32"].Text = "成本小计（元）";
            sheet.Range["P32"].Text = "单价选择";
            sheet.Range["Q32"].Text = "频次/数";
            sheet.Range["R32"].Text = "中心数";
            sheet.Range["S32:X32"].Text = "填写说明";
            sheet.Range["S32:X32"].Merge();
            sheet.Range["N31:X32"].Style.Font.IsBold = true;

            sheet.Range["N33"].Text = "PM（出差）";
            sheet.Range["O33"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.PM_ChuChai_Amount;
            sheet.Range["P33"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.PM_ChuChai_Price;
            sheet.Range["Q33"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.PM_ChuChai_Count;
            sheet.Range["R33"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.PM_ChuChai_ZhongXin;
            sheet.Range["S33:X33"].Text = "PM出差，按预计启动和协同监查次数计算，每中心预计3次，平均每次3000元";
            sheet.Range["S33:X33"].Merge();

            sheet.Range["N34"].Text = "PM（不出差）";
            sheet.Range["O34"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.PM_BuChuChai_Amount;
            sheet.Range["P34"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.PM_BuChuChai_Price;
            sheet.Range["Q34"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.PM_BuChuChai_Count;
            sheet.Range["R34"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.PM_BuChuChai_ZhongXin;
            sheet.Range["S34:X34"].Text = "PM不出差，按预计启动和协同监查次数计算，每中心预计3次，平均每次500元";
            sheet.Range["S34:X34"].Merge();

            sheet.Range["N35"].Text = "CRA（出差）";
            sheet.Range["O35"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.CRA_ChuChai_Amount;
            sheet.Range["P35"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.CRA_ChuChai_Price;
            sheet.Range["Q35"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.CRA_ChuChai_Count;
            sheet.Range["R35"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.CRA_ChuChai_ZhongXin;
            sheet.Range["S35:X35"].Text = "CRA出差，出差中心2个，出差每次3000元，出差次数与服务周期同步。";
            sheet.Range["S35:X35"].Merge();

            sheet.Range["N36"].Text = "CRA（不出差）";
            sheet.Range["O36"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.CRA_BuChuChai_Amount;
            sheet.Range["P36"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.CRA_BuChuChai_Price;
            sheet.Range["Q36"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.CRA_BuChuChai_Count;
            sheet.Range["R36"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.CRA_BuChuChai_ZhongXin;
            sheet.Range["S36:X36"].Text = "CRA出差，出差中心2个，出差每次3000元，出差次数与服务周期同步。";
            sheet.Range["S36:X36"].Merge();

            sheet.Range["N37"].Text = "管理人员（出差）";
            sheet.Range["O37"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.Admin_ChuChai_Amount;
            sheet.Range["P37"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.Admin_ChuChai_Price;
            sheet.Range["Q37"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.Admin_ChuChai_Count;
            sheet.Range["R37"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.Admin_ChuChai_ZhongXin;
            sheet.Range["S37:X37"].Text = "管理人员出差，每家中心预计2次，平均每次3000元";
            sheet.Range["S37:X37"].Merge();

            sheet.Range["N38"].Text = "管理人员（不出差）";
            sheet.Range["O38"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.Admin_BuChuChai_Amount;
            sheet.Range["P38"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.Admin_BuChuChai_Price;
            sheet.Range["Q38"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.Admin_BuChuChai_Count;
            sheet.Range["R38"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.Admin_BuChuChai_ZhongXin;
            sheet.Range["S38:X38"].Text = "管理人员出差，每家中心预计2次，平均每次3000元";
            sheet.Range["S38:X38"].Merge();
            sheet.Range["N39"].Text = "合计";
            sheet.Range["O39"].NumberValue = data.ChaiLvFeiChengBenGuiZeBiao.TotalAmount;
            sheet.Range["S39:X39"].Merge();

            sheet.Range["N39:O39"].Style.Font.IsBold = true;

            //sheet.Range["O33:O39"].Style.KnownColor = ExcelColors.Yellow;
            sheet.Range["O33:O39"].Style.Color = Color.FromArgb(255, 255, 0);

            sheet.Range["N31:X39"].BorderAround(LineStyleType.Thin);
            sheet.Range["N31:X39"].BorderInside(LineStyleType.Thin);

            #endregion
            sheet.Columns[1].ColumnWidth = 19;
            sheet.Columns[2].ColumnWidth = 12;
            sheet.Columns[3].ColumnWidth = 17;
            sheet.Columns[7].ColumnWidth = 16;
            sheet.Columns[9].ColumnWidth = 11;
            sheet.Columns[10].ColumnWidth = 16;
            sheet.Columns[11].ColumnWidth = 12;
            sheet.Columns[13].ColumnWidth = 16;
            sheet.Columns[19].ColumnWidth = 21;
            sheet.Columns[20].ColumnWidth = 12;
            sheet.Columns[23].ColumnWidth = 21;

            sheet.Range["D13"].RowHeight = 30;
            sheet.Range["D22"].RowHeight = 46;
            sheet.Range["D41"].RowHeight = 30;
            sheet.Range["E15"].RowHeight = 27;
            sheet.Range["E26"].RowHeight = 27;

            sheet.Range["C4:C54"].NumberFormat = "0.00";
            sheet.Range["L6:L53"].NumberFormat = "0.00";
            #endregion

            #region 表2，监查服务成本预算（大临床）

            Worksheet sheet2 = wbToStream.Worksheets[1];
            sheet2.Name = "表2，监查服务成本预算（大临床）";

            CellRange range2 = sheet2.Range["A1:G24"];
            range2.BorderAround(LineStyleType.Thin);
            range2.BorderInside(LineStyleType.Thin);
            range2.Style.VerticalAlignment = VerticalAlignType.Center;//垂直居中
            //range2.Style.HorizontalAlignment = HorizontalAlignType.Left; //  
            range2.Style.WrapText = true;
            range2.IsWrapText = true;
            range2.RowHeight = 19.5;
            range2.Style.Font.Size = 10;
            range2.Style.Font.FontName = "宋体";

            sheet2.Range["A1:A23"].Style.Font.IsBold = true;
            sheet2.Range["A12:G12"].Style.Font.IsBold = true;
            sheet2.Range["F13:F23"].Style.Font.IsBold = true;

            //sheet2.Range["E13:E21"].Style.KnownColor = ExcelColors.Orange; 
            //sheet2.Range["A22:F23"].Style.KnownColor = ExcelColors.Yellow;

            sheet2.Range["E13:E21"].Style.Color = Color.FromArgb(247, 150, 70);
            sheet2.Range["A22:F23"].Style.Color = Color.FromArgb(255, 255, 0);


            sheet2.Columns[0].ColumnWidth = 15;
            sheet2.Columns[1].ColumnWidth = 19;
            sheet2.Columns[2].ColumnWidth = 18;
            sheet2.Columns[3].ColumnWidth = 11;
            sheet2.Columns[6].ColumnWidth = 19;

            sheet2.Range["B2:G9"].Style.HorizontalAlignment = HorizontalAlignType.Left;

            #region row1

            sheet2.Range["A1:G1"].Text = "监查服务成本预算";
            sheet2.Range["A1:G1"].Merge();
            sheet2.Range["A1:G1"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            //-------------------
            sheet2.Range["A2"].Text = "项目名称";
            sheet2.Range["B2:G2"].Text = data.ProjectName ?? "";
            sheet2.Range["B2:G2"].Merge();

            //-------------------
            sheet2.Range["A3"].Text = "申办方";
            sheet2.Range["B3:G3"].Text = data.CompanyName ?? "";
            sheet2.Range["B3:G3"].Merge();

            //-------------------
            sheet2.Range["A4"].Text = "总样本量";
            sheet2.Range["B4:G4"].NumberValue = data.ZongBingLiShu;
            sheet2.Range["B4:G4"].Merge();

            //-------------------
            sheet2.Range["A5"].Text = "适应症";
            sheet2.Range["B5:G5"].Text = data.ShiYingZheng ?? "";
            sheet2.Range["B5:G5"].Merge();

            //-------------------
            sheet2.Range["A6"].Text = "随访次数";
            sheet2.Range["B6:G6"].NumberValue = data.SuiFang;
            sheet2.Range["B6:G6"].Merge();

            //-------------------
            sheet2.Range["A7"].Text = "计划筛选中心数";
            sheet2.Range["B7:G7"].NumberValue = data.JiHuaShuaiXuanZhongXinShu;
            sheet2.Range["B7:G7"].Merge();

            //-------------------
            sheet2.Range["A8"].Text = "计划确定中心数";
            sheet2.Range["B8:G8"].NumberValue = data.QueDingZhongXinShu;
            sheet2.Range["B8:G8"].Merge();

            //-------------------
            sheet2.Range["A9"].Text = "合同约定研究总时限（月）";
            sheet2.Range["B9:G9"].NumberValue = data.ZongShiXian;
            sheet2.Range["B9:G9"].Merge();
            sheet2.Range["A9:G9"].RowHeight = 27;

            //-------------------


            sheet2.Range["A10"].Text = "难度系数";//文字需要根据难度选中变化
                                              // sheet2.Range["B10:G10"].RichText = "简单级1.0；普通级1.14；稍难级1.3；困难级1.54；特别级2.0";
            sheet2.Range["B10:G10"].Merge();
            IRichTextString richText = sheet2.Range["B10:G10"].RichText;
            richText.Text = "简单级1.0；普通级1.14；稍难级1.3；困难级1.54；特别级2.0";
            IFont font = wbToStream.CreateFont();
            font.Color = Color.Red;
            if (data.NamDu == ENanDuXiShu.简单级)
            {
                richText.SetFont(0, 6, font);
            }
            else if (data.NamDu == ENanDuXiShu.普通级)
            {
                richText.SetFont(7, 14, font);
            }
            else if (data.NamDu == ENanDuXiShu.稍难级)
            {
                richText.SetFont(15, 21, font);
            }
            else if (data.NamDu == ENanDuXiShu.困难级)
            {
                richText.SetFont(22, 29, font);
            }
            else if (data.NamDu == ENanDuXiShu.特别级)
            {
                richText.SetFont(30, 35, font);
            }

            //-------------------
            sheet2.Range["A11"].Text = "特别系数";//文字需要根据特别选中变化
            sheet2.Range["B11:G11"].Text = "无特殊1.0；上市后评价随机0.5-0.9；上市后评价开放0.3-0.6；BE研究0.5";
            sheet2.Range["B11:G11"].Merge();

            //-------------------
            sheet2.Range["A12"].Text = "序号";
            sheet2.Range["B12"].Text = "主要工作条目";
            sheet2.Range["C12"].Text = "分类计算标准";
            sheet2.Range["D12"].Text = "单价（元/中心或元/例）";
            sheet2.Range["E12"].Text = "数量（中心或例）";
            sheet2.Range["F12"].Text = "预算小计（元）";
            sheet2.Range["G12"].Text = "填写备注说明";
            sheet2.Range["A12:G12"].RowHeight = 25;
            //-------------------
            sheet2.Range["A13:A14"].Text = "1";
            sheet2.Range["A13:A14"].Merge();
            sheet2.Range["B13:B14"].Text = "准备阶段服务费（包括调研、立项、伦理、协助协议、启动前准备完成等工作）";
            sheet2.Range["B13:B14"].Merge();
            sheet2.Range["C13"].Text = "简单级、普通级项目";
            sheet2.Range["D13"].NumberValue = 5000;
            sheet2.Range["E13"].NumberValue = (data.NamDu == ENanDuXiShu.简单级 || data.NamDu == ENanDuXiShu.普通级) ? data.QueDingZhongXinShu : 0;
            sheet2.Range["F13"].NumberValue = (data.NamDu == ENanDuXiShu.简单级 || data.NamDu == ENanDuXiShu.普通级) ? data.QueDingZhongXinShu * 5000 : 0;
            sheet2.Range["A14:G14"].RowHeight = 34;


            sheet2.Range["C14"].Text = "稍难级、困难级、特别级项目";
            sheet2.Range["D14"].NumberValue = 8000;
            sheet2.Range["E14"].NumberValue = (data.NamDu == ENanDuXiShu.稍难级 || data.NamDu == ENanDuXiShu.困难级 || data.NamDu == ENanDuXiShu.特别级) ? data.QueDingZhongXinShu : 0;
            sheet2.Range["F14"].NumberValue = (data.NamDu == ENanDuXiShu.稍难级 || data.NamDu == ENanDuXiShu.困难级 || data.NamDu == ENanDuXiShu.特别级) ? data.QueDingZhongXinShu * 8000 : 0; 

            sheet2.Range["G13:G14"].Text = "按预计筛选中心数计算";
            sheet2.Range["G13:G14"].Merge();


            //-------------------


            sheet2.Range["A15:A19"].Text = "2";
            sheet2.Range["A15:A19"].Merge();
            sheet2.Range["B15:B19"].Text = "每例病例监查服务费";
            sheet2.Range["B15:B19"].Merge();
            sheet2.Range["C15"].Text = "简单级项目（1000）";
            sheet2.Range["D15"].NumberValue = 1000;
            sheet2.Range["E15"].NumberValue = data.NamDu == ENanDuXiShu.简单级 ? data.ZongBingLiShu : 0;
            sheet2.Range["F15"].NumberValue = data.NamDu == ENanDuXiShu.简单级 ? data.ZongBingLiShu * 1000 : 0; ;

            sheet2.Range["C16"].Text = "普通级项目（1500）";
            sheet2.Range["D16"].NumberValue = 1500;
            sheet2.Range["E16"].NumberValue = data.NamDu == ENanDuXiShu.普通级 ? data.ZongBingLiShu : 0;
            sheet2.Range["F16"].NumberValue = data.NamDu == ENanDuXiShu.普通级 ? data.ZongBingLiShu * 1500 : 0;

            sheet2.Range["C17"].Text = "稍难级项目（3000）";
            sheet2.Range["D17"].NumberValue = 3000;
            sheet2.Range["E17"].NumberValue = data.NamDu == ENanDuXiShu.稍难级 ? data.ZongBingLiShu : 0;
            sheet2.Range["F17"].NumberValue = data.NamDu == ENanDuXiShu.稍难级 ? data.ZongBingLiShu * 3000 : 0;

            sheet2.Range["C18"].Text = "困难级项目（4000）";
            sheet2.Range["D18"].NumberValue = 4000;
            sheet2.Range["E18"].NumberValue = data.NamDu == ENanDuXiShu.困难级 ? data.ZongBingLiShu : 0;
            sheet2.Range["F18"].NumberValue = data.NamDu == ENanDuXiShu.困难级 ? data.ZongBingLiShu * 4000 : 0;

            sheet2.Range["C19"].Text = "特别级项目（7000）";
            sheet2.Range["D19"].NumberValue = 7000;
            sheet2.Range["E19"].NumberValue = data.NamDu == ENanDuXiShu.特别级 ? data.ZongBingLiShu : 0;
            sheet2.Range["F19"].NumberValue = data.NamDu == ENanDuXiShu.特别级 ? data.ZongBingLiShu * 7000 : 0;


            sheet2.Range["G15:G19"].Text = "可根据实际项目情况上下浮动调整20%";
            sheet2.Range["G15:G19"].Merge();

            //-------------------

            sheet2.Range["A20:A21"].Text = "3";
            sheet2.Range["A20:A21"].Merge();
            sheet2.Range["B20:B21"].Text = "项目关闭中心（包括资料回收、答疑、归档、小结、总结盖章）";
            sheet2.Range["B20:B21"].Merge();
            sheet2.Range["C20"].Text = "（简单级、普通级24例以内；稍难级、困难级、特别级12例及以内）";
            sheet2.Range["D20"].NumberValue = 5000;
            //sheet2.Range["E20"].NumberValue = (((data.NamDu == ENanDuXiShu.简单级 || data.NamDu == ENanDuXiShu.普通级) && data.ZongBingLiShu <= 24) || ((data.NamDu == ENanDuXiShu.稍难级 || data.NamDu == ENanDuXiShu.困难级 || data.NamDu == ENanDuXiShu.特别级) && data.ZongBingLiShu <= 12)) ? data.QueDingZhongXinShu : 0; 
            //sheet2.Range["F20"].NumberValue = (((data.NamDu == ENanDuXiShu.简单级 || data.NamDu == ENanDuXiShu.普通级) && data.ZongBingLiShu <= 24) || ((data.NamDu == ENanDuXiShu.稍难级 || data.NamDu == ENanDuXiShu.困难级 || data.NamDu == ENanDuXiShu.特别级) && data.ZongBingLiShu <= 12)) ? data.QueDingZhongXinShu * 5000 : 0;

            sheet2.Range["E20"].NumberValue = data.QueDingZhongXinShu;
            sheet2.Range["F20"].NumberValue = data.QueDingZhongXinShu * 5000;
            sheet2.Range["A20:G20"].RowHeight = 57;

            sheet2.Range["C21"].Text = "（简单级、普通级24例以上；稍难级、困难级、特别级12例及以上）";
            sheet2.Range["D21"].NumberValue = 8000;
            //sheet2.Range["E21"].NumberValue = (((data.NamDu == ENanDuXiShu.简单级 || data.NamDu == ENanDuXiShu.普通级) && data.ZongBingLiShu > 24) || ((data.NamDu == ENanDuXiShu.稍难级 || data.NamDu == ENanDuXiShu.困难级 || data.NamDu == ENanDuXiShu.特别级) && data.ZongBingLiShu > 12)) ? data.QueDingZhongXinShu : 0;
            //sheet2.Range["F21"].NumberValue = (((data.NamDu == ENanDuXiShu.简单级 || data.NamDu == ENanDuXiShu.普通级) && data.ZongBingLiShu > 24) || ((data.NamDu == ENanDuXiShu.稍难级 || data.NamDu == ENanDuXiShu.困难级 || data.NamDu == ENanDuXiShu.特别级) && data.ZongBingLiShu > 12)) ? data.QueDingZhongXinShu * 8000 : 0; 
            sheet2.Range["E21"].NumberValue = 0;
            sheet2.Range["F21"].NumberValue = 0;

            sheet2.Range["A21:G21"].RowHeight = 57;


            sheet2.Range["G20:G21"].Text = "按实际计划启动中心数计算";
            sheet2.Range["G20:G21"].Merge();

            //-------------------
            sheet2.Range["A22:E22"].Text = "合计：";
            sheet2.Range["A22:E22"].Style.HorizontalAlignment = HorizontalAlignType.Right;
            sheet2.Range["F22"].NumberValue = data.RenGongFeiYongZongE.CRAFei.HeJi;
            sheet2.Range["A22:E22"].Merge();
            //-------------------
            sheet2.Range["A23:E23"].Text = "平均每例：";
            sheet2.Range["A23:E23"].Style.HorizontalAlignment = HorizontalAlignType.Right;
            sheet2.Range["F23"].NumberValue = data.RenGongFeiYongZongE.CRAFei.HeJi / data.ZongBingLiShu;
            sheet2.Range["A23:E23"].Merge();
            //-------------------
            sheet2.Range["A24:G24"].Text = "填表说明：1、蓝色单元格中的数据需要根据项目难度系数和特别系数计算更改；2、橙色单元格中的数据需要根据项目的病例数和中心数进行预计；3、无颜色单元格固定不变，请勿修改；4、黄色单元格为最后计算结果，已设定公式，请勿修改。";
            sheet2.Range["A24:G24"].Merge();
            sheet2.Range["A24:G24"].Style.Font.Color = Color.Red;
            sheet2.Range["A24:G24"].RowHeight = 50;
            #endregion

            #endregion

            #region 汇总表（不需填写，已设置公式自动汇总）

            Worksheet sheet3 = wbToStream.Worksheets[2];
            sheet3.Name = "汇总表（不需填写，已设置公式自动汇总）";

            CellRange range3 = sheet3.Range["A1:C41"];
            range3.Style.VerticalAlignment = VerticalAlignType.Center;//垂直居中 
            range3.Style.WrapText = true;
            range3.IsWrapText = true;
            range3.Style.Font.Size = 10;
            range3.Style.Font.FontName = "宋体";

            sheet3.Range["A1:C5"].BorderAround(LineStyleType.None);
            sheet3.Range["A1:C5"].BorderInside(LineStyleType.None);

            sheet3.Columns[0].ColumnWidth = 28.5;
            sheet3.Columns[1].ColumnWidth = 28.5;
            sheet3.Columns[2].ColumnWidth = 28.5;


            sheet3.Range["A6:C39"].BorderAround(LineStyleType.Thin);
            sheet3.Range["A6:C39"].BorderInside(LineStyleType.Thin);

            #region row1

            sheet3.Range["A1:C1"].Text = "临床项目成本估算表";
            sheet3.Range["A1:C1"].Merge();
            sheet3.Range["A1:C1"].Style.HorizontalAlignment = HorizontalAlignType.Center;
            sheet3.Range["A1:C1"].Style.Font.Size = 12;
            sheet3.Range["A1:C1"].RowHeight = 40;

            sheet3.Range["A2:C2"].Text = "委托方（全称）：" + data.CompanyName;
            sheet3.Range["A2:C2"].Merge();

            sheet3.Range["A3:C3"].Text = "项目名称（全称）：" + data.ProjectName;
            sheet3.Range["A3:C3"].Merge();

            sheet3.Range["A4:C4"].Text = "年度：";
            sheet3.Range["A4:C4"].Merge();

            sheet3.Range["A5:C5"].Text = "金额单位：元";
            sheet3.Range["A5:C5"].Merge();

            sheet3.Range["A1:C5"].Style.Font.IsBold = true;
            sheet3.Range["A1:C5"].RowHeight = 20;


            sheet3.Range["A6:A7"].Text = "项目";
            sheet3.Range["A6:A7"].Merge();

            sheet3.Range["B6:C6"].Text = "预算发生金额";
            sheet3.Range["B6:C6"].Merge();
            sheet3.Range["B7:C7"].Text = "整体";
            sheet3.Range["B7:C7"].Merge();

            sheet3.Range["A8"].Text = "临床项目";
            sheet3.Range["B8:C8"].NumberValue = data.TotalAmount;
            sheet3.Range["B8:C8"].Merge();
            sheet3.Range["B8:C8"].Style.Font.IsBold = true;
            //---------------------------
            sheet3.Range["A9"].Text = "  医院成本";
            sheet3.Range["B9:C9"].NumberValue = data.YiYuanHeTongFeiZongE.TotalAmount;
            sheet3.Range["B9:C9"].Merge();
            sheet3.Range["A9:C9"].Style.Font.IsBold = true;

            sheet3.Range["A10"].Text = "    组长费";
            sheet3.Range["B10:C10"].NumberValue = data.YiYuanHeTongFeiZongE.ZuZhangFei.HeJi;
            sheet3.Range["B10:C10"].Merge();

            sheet3.Range["A11"].Text = "    机构管理费";
            sheet3.Range["B11:C11"].NumberValue = data.YiYuanHeTongFeiZongE.JiGouGuanLiFei.HeJi;
            sheet3.Range["B11:C11"].Merge();

            sheet3.Range["A12"].Text = "    伦理费";
            sheet3.Range["B12:C12"].NumberValue = data.YiYuanHeTongFeiZongE.LunLiFei.HeJi;
            sheet3.Range["B12:C12"].Merge();

            sheet3.Range["A13"].Text = "    合格病例费用";
            sheet3.Range["B13:C13"].NumberValue = data.YiYuanHeTongFeiZongE.HeGeBingLiFei.HeJi;
            sheet3.Range["B13:C13"].Merge();

            sheet3.Range["A14"].Text = "    筛选病例费用";
            sheet3.Range["B14:C14"].NumberValue = data.YiYuanHeTongFeiZongE.ShaiXuanBingLiFei.HeJi;
            sheet3.Range["B14:C14"].Merge();

            sheet3.Range["A15"].Text = "    其他费用";
            sheet3.Range["B15:C15"].NumberValue = data.YiYuanHeTongFeiZongE.QiTaFei.HeJi;
            sheet3.Range["B15:C15"].Merge();


            //---------------------------
            sheet3.Range["A16"].Text = "  人工成本";
            sheet3.Range["B16:C16"].NumberValue = data.RenGongFeiYongZongE.TotalAmount;
            sheet3.Range["B16:C16"].Merge();
            sheet3.Range["A16:C16"].Style.Font.IsBold = true;

            //---------------------------
            sheet3.Range["A17"].Text = "  会议成本";
            sheet3.Range["B17:C17"].NumberValue = data.HuiYiFeiZongE.TotalAmount;
            sheet3.Range["B17:C17"].Merge();
            sheet3.Range["B17:C17"].Style.Font.IsBold = true;

            sheet3.Range["A18"].Text = "    会务费";
            sheet3.Range["B18:C18"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.HuiWuFei + data.HuiYiFeiZongE.ZongJieHui.HuiWuFei + data.HuiYiFeiZongE.ZhongQiHui.HuiWuFei + data.HuiYiFeiZongE.MangTaiShenHeHui.HuiWuFei;
            sheet3.Range["B18:C18"].Merge();

            sheet3.Range["A19"].Text = "    交通费";
            sheet3.Range["B19:C19"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.JiaoTongFei + data.HuiYiFeiZongE.ZongJieHui.JiaoTongFei + data.HuiYiFeiZongE.ZhongQiHui.JiaoTongFei + data.HuiYiFeiZongE.MangTaiShenHeHui.JiaoTongFei;
            sheet3.Range["B19:C19"].Merge();

            sheet3.Range["A20"].Text = "    住宿费";
            sheet3.Range["B20:C20"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.ZhuSuFei + data.HuiYiFeiZongE.ZongJieHui.ZhuSuFei + data.HuiYiFeiZongE.ZhongQiHui.ZhuSuFei + data.HuiYiFeiZongE.MangTaiShenHeHui.ZhuSuFei;
            sheet3.Range["B20:C20"].Merge();

            sheet3.Range["A21"].Text = "    会议室费";
            sheet3.Range["B21:C21"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.HuiYiShiFei + data.HuiYiFeiZongE.ZongJieHui.HuiYiShiFei + data.HuiYiFeiZongE.ZhongQiHui.HuiYiShiFei + data.HuiYiFeiZongE.MangTaiShenHeHui.HuiYiShiFei;
            sheet3.Range["B21:C21"].Merge();

            sheet3.Range["A22"].Text = "    其他费用";
            sheet3.Range["B22:C22"].NumberValue = data.HuiYiFeiZongE.XieTiaoHui.QiTaFei + data.HuiYiFeiZongE.ZongJieHui.QiTaFei + data.HuiYiFeiZongE.ZhongQiHui.QiTaFei + data.HuiYiFeiZongE.MangTaiShenHeHui.QiTaFei;
            sheet3.Range["B22:C22"].Merge();


            //---------------------------
            sheet3.Range["A23"].Text = "  其他成本";
            sheet3.Range["B23:C23"].NumberValue = data.QiTaZaFeiZongE.TotalAmount;
            sheet3.Range["B23:C23"].Merge();
            sheet3.Range["A23:C23"].Style.Font.IsBold = true;

            sheet3.Range["A24"].Text = "    启动会费";
            sheet3.Range["B24:C24"].NumberValue = data.QiTaZaFeiZongE.QiDongHuiFei.HeJi;
            sheet3.Range["B24:C24"].Merge();

            sheet3.Range["A25"].Text = "    运输费";
            sheet3.Range["B25:C25"].NumberValue = data.QiTaZaFeiZongE.YunShuFei.HeJi;
            sheet3.Range["B25:C25"].Merge();

            sheet3.Range["A26"].Text = "    监查差旅费";
            sheet3.Range["B26:C26"].NumberValue = data.QiTaZaFeiZongE.JianChaChaiLvFei.HeJi;
            sheet3.Range["B26:C26"].Merge();

            sheet3.Range["A27"].Text = "    试剂、耗材费";
            sheet3.Range["B27:C27"].NumberValue = data.QiTaZaFeiZongE.ShiJiHaoCaiFei.HeJi;
            sheet3.Range["B27:C27"].Merge();

            sheet3.Range["A28"].Text = "    中心实验室";
            sheet3.Range["B28:C28"].NumberValue = data.QiTaZaFeiZongE.ZhongXinShiYanShiFei.HeJi;
            sheet3.Range["B28:C28"].Merge();

            sheet3.Range["A29"].Text = "    数据、统计费";
            sheet3.Range["B29:C29"].NumberValue = data.QiTaZaFeiZongE.ShuJuTongJiFei.HeJi;
            sheet3.Range["B29:C29"].Merge();

            sheet3.Range["A30"].Text = "    系统使用费";
            sheet3.Range["B30:C30"].NumberValue = data.QiTaZaFeiZongE.XiTongShiYongFei.HeJi;
            sheet3.Range["B30:C30"].NumberValue = data.QiTaZaFeiZongE.XiTongShiYongFei.HeJi;
            sheet3.Range["B30:C30"].Merge();

            sheet3.Range["A31"].Text = "    印刷费";
            sheet3.Range["B31:C31"].NumberValue = data.QiTaZaFeiZongE.YinShuaFei.HeJi;
            sheet3.Range["B31:C31"].Merge();

            sheet3.Range["A32"].Text = "    其他采购";
            sheet3.Range["B32:C32"].NumberValue = data.QiTaZaFeiZongE.QiTaCaiGouFei.HeJi;
            sheet3.Range["B32:C32"].Merge();

            sheet3.Range["A33"].Text = "    受试者招募费";
            sheet3.Range["B33:C33"].NumberValue = data.QiTaZaFeiZongE.ShouShiZheZhaoMuFei.HeJi;
            sheet3.Range["B33:C33"].Merge();

            sheet3.Range["A34"].Text = "    稽查服务费";
            sheet3.Range["B34"].NumberValue = data.QiTaZaFeiZongE.ShengNeiJiChaFei.HeJi + data.QiTaZaFeiZongE.ShengWaiJiChaFei.HeJi;
            sheet3.Range["B34:C34"].Merge();

            sheet3.Range["A35"].Text = "    SMO费用";
            sheet3.Range["B35:C35"].NumberValue = data.QiTaZaFeiZongE.SMOFei.HeJi;
            sheet3.Range["B35:C35"].Merge();

            sheet3.Range["A36"].Text = "    委外监查服务";
            sheet3.Range["B36:C36"].NumberValue = data.QiTaZaFeiZongE.WeiWaiJianChaFuWuFei.HeJi;
            sheet3.Range["B36:C36"].Merge();

            sheet3.Range["A37"].Text = "    遗传办填报";
            sheet3.Range["B37:C37"].NumberValue = data.QiTaZaFeiZongE.YiChuanBanTianBaoFei.HeJi;
            sheet3.Range["B37:C37"].Merge();

            sheet3.Range["A38"].Text = "    其他费用";
            sheet3.Range["B38:C38"].NumberValue = data.QiTaZaFeiZongE.QiTaFei.HeJi;
            sheet3.Range["B38:C38"].Merge();

            //-------------------------------------------------

            sheet3.Range["A39"].Text = "合计";
            sheet3.Range["B39:C39"].NumberValue = data.TotalAmount;
            sheet3.Range["B39:C39"].Merge();
            sheet3.Range["A39:C39"].Style.Font.IsBold = true;

            //-------------------------------------------------
            sheet3.Range["A40"].Text = "项目经理：";
            sheet3.Range["B40"].Text = "部长审核：";
            sheet3.Range["C40"].Text = "总监审核：";

            sheet3.Range["A41"].Text = "提交日期：";
            sheet3.Range["B41"].Text = "审核日期：";
            sheet3.Range["C41"].Text = "审核日期：";

            sheet3.Range["A40:C41"].Style.Font.IsBold = true;
            sheet3.Range["A40:C41"].RowHeight = 20;

            //sheet3.Range["A8:C8"].Style.KnownColor = ExcelColors.Orange;
            //sheet3.Range["A9:C9"].Style.KnownColor = ExcelColors.LightOrange;
            //sheet3.Range["A16:C16"].Style.KnownColor = ExcelColors.LightOrange;
            //sheet3.Range["A17:C17"].Style.KnownColor = ExcelColors.LightOrange;
            //sheet3.Range["A23:C23"].Style.KnownColor = ExcelColors.LightOrange;
            //sheet3.Range["A39:C39"].Style.KnownColor = ExcelColors.LightOrange;

            sheet3.Range["A8:C8"].Style.Color = Color.FromArgb(252, 213, 180);
            sheet3.Range["A9:C9"].Style.Color = Color.FromArgb(253, 233, 217);
            sheet3.Range["A16:C16"].Style.Color = Color.FromArgb(253, 233, 217);
            sheet3.Range["A17:C17"].Style.Color = Color.FromArgb(253, 233, 217);
            sheet3.Range["A23:C23"].Style.Color = Color.FromArgb(253, 233, 217);
            sheet3.Range["A39:C39"].Style.Color = Color.FromArgb(253, 233, 217);

            sheet3.Range["B8:B39"].NumberFormat = "#,##0.00";
            #endregion


            #endregion


            string path = $"{data.CompanyName}.xls";


            FileStream file_stream = new FileStream(@"D:\\web\\web6\\wwwroot\\web\\pdf\\EXCEL\\" + path, FileMode.Create);
            wbToStream.SaveToStream(file_stream);
            file_stream.Close();
            return path;
            // System.Diagnostics.Process.Start($"{data.ProjectName}项目成本预估表.xls");

            //B. Load Excel file from stream
            //Workbook wbFromStream = new Workbook();
            //FileStream fileStream = File.OpenRead("sample.xls");
            //fileStream.Seek(0, SeekOrigin.Begin);
            //wbFromStream.LoadFromStream(fileStream);
            //wbFromStream.SaveToFile("From_stream.xls", ExcelVersion.Version97to2003);
            //fileStream.Dispose();
            //System.Diagnostics.Process.Start("From_stream.xls");

        }

        public string CreatePdf(ShiYanFangAnDto data)
        {
            BaseFont bfSun = BaseFont.CreateFont(@"c:\windows\fonts\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //宋体加粗（用于标题部分）
            iTextSharp.text.Font font_Song_Bold_Title = new iTextSharp.text.Font(bfSun, 12, iTextSharp.text.Font.BOLD);
            font_Song_Bold_Title.SetStyle("Italic");

            //宋体加粗（用于表头或着重显示的部分）
            iTextSharp.text.Font font_Song_Bold = new iTextSharp.text.Font(bfSun, 12, iTextSharp.text.Font.BOLD);
            font_Song_Bold.SetStyle("Italic");
            //普通宋体（用于文档内容）
            BaseFont bfSunZ = BaseFont.CreateFont(@"c:\windows\fonts\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font_Song = new iTextSharp.text.Font(bfSunZ, 12);
            font_Song.SetStyle("Italic");

            string path = $"{data.CompanyName}.pdf";
            Document document = new Document(PageSize.A4, 10, 10, 10, 10);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@"D:\\web\\web6\\wwwroot\\web\\pdf\\PDF\\" + path, FileMode.Create));
            document.Open();

            float titleLeft = 50, spacingBefore = 5, spacingAfter = 20, padding = 5;

            #region 四、 医疗器械临床研究费用预算清单 

            var title = new iTextSharp.text.Paragraph("医疗器械临床研究费用预算清单", font_Song_Bold_Title);
            title.IndentationLeft = titleLeft;
            document.Add(title);

            PdfPCell nCell;
            PdfPTable table = new PdfPTable(3);
            table.SetWidthPercentage(new float[] { 150, 150, 200 }, PageSize.A4);
            table.SpacingBefore = spacingBefore;
            table.SpacingAfter = spacingAfter;
            nCell = new PdfPCell(new Phrase("产品", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("项目", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);
            nCell = new PdfPCell(new Phrase("费用(单位：人民币/万元)", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.ProjectName, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 6;
            nCell.SetLeading(2f, 1.5f);
            nCell.FixedHeight = 70;
            table.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("CRO 服务费用(包含数统)", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);
            nCell = new PdfPCell(new Phrase(data.YanJiuYuSuanQingDan.CRO_FuWuFei, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("日常费用(包含差旅费用)", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);
            nCell = new PdfPCell(new Phrase(data.YanJiuYuSuanQingDan.RiChangFei, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("会议费用", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);
            nCell = new PdfPCell(new Phrase(data.YanJiuYuSuanQingDan.HuiYiFei, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("CRO 服务费合计(税 6%)", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);
            nCell = new PdfPCell(new Phrase(data.YanJiuYuSuanQingDan.CRO_FuWuHeJi, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            table.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("医院研究费用", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);
            nCell = new PdfPCell(new Phrase(data.YanJiuYuSuanQingDan.YiYuanYanJiuFei, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("SMO 费用", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);
            nCell = new PdfPCell(new Phrase(data.YanJiuYuSuanQingDan.SMOFei, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("合计(税 6%)", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 2;
            table.AddCell(nCell);
            nCell = new PdfPCell(new Phrase(data.YanJiuYuSuanQingDan.HanShuiHeJi, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table.AddCell(nCell);

            document.Add(table);
            #endregion

            #region (一)临床试验服务费用

            title = new iTextSharp.text.Paragraph("(一)临床试验服务费用", font_Song_Bold_Title);
            title.IndentationLeft = titleLeft;
            document.Add(title);

            PdfPTable table2 = new PdfPTable(3);
            table2.SetWidthPercentage(new float[] { 250, 100, 150 }, PageSize.A4);
            table2.SpacingBefore = spacingBefore;
            table2.SpacingAfter = spacingAfter;

            nCell = new PdfPCell(new Phrase("项       目", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("费用(万元)", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("备       注", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("1.筛选研究单位", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 2;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.ShaiXuanYanJiuDanWei, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("研究单位调研、筛选、实地拜访", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.ShaiXuanYanJiuDanWei, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase($"1.全国办事处进行调研， 以最 终确定研究单位({data.QueDingZhongXinShu} 家中心) ；", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);
            //-------------------------------------

            nCell = new PdfPCell(new Phrase("2.临床试验方案设计", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 2;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.LinChuangShiYanFangAnSheJi, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("临床研究方案修订、主持方案协调会及方 案定稿、 CRF、知情同意书设计、受试者文 件夹设计", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.LinChuangShiYanFangAnSheJi, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("包括方案， 普通 CRF，无碳写 CRF", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);
            //-------------------------------------

            nCell = new PdfPCell(new Phrase("3. CRO 服务费用", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 2;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.CROFuWuFei, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("试验前准备 1", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.CROFuWuFei_1, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 15;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase($"{data.LinChuangShiYanFuWuFei.CROFuWuFei_1_Price} 元 *{data.QueDingZhongXinShu} 家中心", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 15;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("机构立项资料准备、递交、立项", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("报伦理委员会的资料准备、递交、过会", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("研究者手册审核", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("建立研究者档案", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("试验前准备 2", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("临床试验协议商议、签订", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("试验前准备 3", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("研究者资料收集与整理", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("现场 GCP 培训", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("严重不良事件报告培训", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("受试者筛选的现场指导", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("试验文件/器械分发", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("试验器械、资料管理和讲解", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("质量控制讲解", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            //-------------------------------------


            nCell = new PdfPCell(new Phrase("监查访视", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.CROFuWuFei_2, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 12;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase($"中心数：{data.QueDingZhongXinShu} 家中心；\r\n1 名 CRA/中心；", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 12;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("知情同意书核查", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("病例报告表审核", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("病例报告与原始病历核对", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("试验安全性/依从性审核", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("试验管理文件审核", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("试验物资管理审核", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("不良事件和严重不良事件的记录", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("违背方案情况记录", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("入组进度", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("与研究者商讨解决问题", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("完成监查报告", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            //-------------------------------------


            nCell = new PdfPCell(new Phrase("试验结束访视", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.CROFuWuFei_3, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 5;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase($"{data.LinChuangShiYanFuWuFei.CROFuWuFei_3_Price} 元 *{data.QueDingZhongXinShu} 家中心", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 5;
            table2.AddCell(nCell);


            nCell = new PdfPCell(new Phrase("数据疑问和问题解决", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);


            nCell = new PdfPCell(new Phrase("回收试验物资", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("回收所有试验记录及文件", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("严重不良事件追踪", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);


            //-------------------------------------

            nCell = new PdfPCell(new Phrase("项目管理", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.CROFuWuFei_4, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 8;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 8;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("项目管理计划", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("项目管理会议(内部、外部)", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("临床试验文档(TMF)管理", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("团队及沟通管理", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("申办方协同访视", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("研究中心 GCP 质控", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("研究中心付款及跟踪记录", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);
            //-------------------------------------


            nCell = new PdfPCell(new Phrase("试验资料的内部专家审核", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.CROFuWuFei_5, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 2;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(" ", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 2;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("综合审核会费用", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            //-------------------------------------

            nCell = new PdfPCell(new Phrase("4. 数据管理和统计分析", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 2;
            table2.AddCell(nCell);


            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.ShuJuGuanLiHeTongJiFenXi, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            //-------------------------------------

            nCell = new PdfPCell(new Phrase("数据管理", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.ShuJuGuanLiHeTongJiFenXi_1, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 9;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("1.包含 EDC 系统费用;\r\n2.按照《医疗器械临床试验数据 递交要求注册审查指导原则》(2021)要求需要提供原始数据 库、分析数据库、统计分析程序 代码、说明文件等；\r\n3.包含随机化系统", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 9;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("CRF 设计与核查", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("数据稽查", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("数据录入", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("数据逻辑检查", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("数据医学检查(专业检查)", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("数据质疑/质疑解决/质疑管理", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("数据库清理/更新", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("数据库锁定/提交/备份", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            //---------------------------------------

            nCell = new PdfPCell(new Phrase("统计分析", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.ShuJuGuanLiHeTongJiFenXi_2, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 6;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(" ", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 6;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("统计分析计划书", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("随机过程编程", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("统计分析编程", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("模拟数据统计测试", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("撰写统计分析报告", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            //---------------------------------------

            nCell = new PdfPCell(new Phrase("5. 总结", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 2;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.ZongJie, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("资料回收、归档", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.ZongJie_1, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("临床研究总结报告及盖章", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.ZongJie_2, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(" ", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            //---------------------------------------

            nCell = new PdfPCell(new Phrase("6.合计", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.HeJi, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 2;
            table2.AddCell(nCell);


            //----------------------------------------

            nCell = new PdfPCell(new Phrase("7.含税总计(税率 6%)", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table2.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanFuWuFei.HanShuiHeJi, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 2;
            table2.AddCell(nCell);

            document.Add(table2);
            #endregion

            #region (二)日常费用

            var title2 = new iTextSharp.text.Paragraph("(二)日常费用", font_Song_Bold_Title);
            title2.IndentationLeft = titleLeft;
            document.Add(title2);


            PdfPTable table3 = new PdfPTable(4);
            table3.SetWidthPercentage(new float[] { 50, 180, 120, 150 }, PageSize.A4);
            table3.SpacingBefore = spacingBefore;
            table3.SpacingAfter = spacingAfter;


            nCell = new PdfPCell(new Phrase("序号", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);


            nCell = new PdfPCell(new Phrase("内容", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("费用(人民币：万)", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);


            nCell = new PdfPCell(new Phrase("备注", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);
            //---------------------------------------------------------
            nCell = new PdfPCell(new Phrase("1", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("办公费及通讯费", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.RiChangFei.BanGongTongXun, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(" ", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            //---------------------------------------------------


            nCell = new PdfPCell(new Phrase("2", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("临床试验文件印刷装订费用", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.RiChangFei.YinShuaZhuangDing, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(" ", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            //---------------------------------------------------

            nCell = new PdfPCell(new Phrase("3", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("差旅费", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.RiChangFei.ChaiLv, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("见附表 1", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            //---------------------------------------------------

            nCell = new PdfPCell(new Phrase("费 用 合计", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 2;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("合计\t税前- 日常支出", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.RiChangFei.HeJi, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(" ", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            //---------------------------------------------------

            nCell = new PdfPCell(new Phrase("税后- 日常支出(税率6%)", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.RiChangFei.HanShuiHeJi, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(" ", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table3.AddCell(nCell);
            //---------------------------------------------------


            document.Add(table3);



            #endregion

            #region 附表 1

            var title3 = new iTextSharp.text.Paragraph("附表 1", font_Song_Bold_Title);
            title3.IndentationLeft = titleLeft;
            document.Add(title3);



            PdfPTable table4 = new PdfPTable(3);
            table4.SetWidthPercentage(new float[] { 200, 100, 200 }, PageSize.A4);
            table4.SpacingBefore = spacingBefore;
            table4.SpacingAfter = spacingAfter;

            nCell = new PdfPCell(new Phrase("差旅费", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 2;
            table4.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(" ", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table4.AddCell(nCell);
            //----------------------------------------------------------



            //每月 1 次，1,875 元/次，1,875 元 *12 个月； 5 个中心， 1 人 

            nCell = new PdfPCell(new Phrase("项目经理差旅费、住宿、餐饮、 话费及补贴", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table4.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.ChaiLvFuFeiBiao.PMChaiLvFei, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table4.AddCell(nCell);

            nCell = new PdfPCell(new Phrase($"按 1 人，每月 1 次，差旅费{data.ChaiLvFuFeiBiao.PMChaiLvFei_Price} 元/次，{data.ChaiLvFuFeiBiao.PMChaiLvFei_Price} 元 *{data.ZongShiXian} 个月； {data.QueDingZhongXinShu} 个中心", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table4.AddCell(nCell);

            //----------------------------------------------------------

            nCell = new PdfPCell(new Phrase("监查员差旅费、住宿、餐饮、话 费及补贴", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table4.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.ChaiLvFuFeiBiao.CRAChaiLvFei, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table4.AddCell(nCell);

            nCell = new PdfPCell(new Phrase($"按 1 人，每月 1 次，差旅费{data.ChaiLvFuFeiBiao.CRAChaiLvFei_Price} 元/次，{data.ChaiLvFuFeiBiao.CRAChaiLvFei_Price} 元 *{data.ZongShiXian} 个月； {data.QueDingZhongXinShu} 个中心", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table4.AddCell(nCell);

            //----------------------------------------------------------

            nCell = new PdfPCell(new Phrase("管理人员差旅、住宿、餐饮、话 费及补贴", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table4.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.ChaiLvFuFeiBiao.AdminChaiLvFei, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table4.AddCell(nCell);

            nCell = new PdfPCell(new Phrase($"按 1 人，每月 1 次，差旅费{data.ChaiLvFuFeiBiao.AdminChaiLvFei_Price} 元/次，{data.ChaiLvFuFeiBiao.AdminChaiLvFei_Price} 元 *{data.ZongShiXian} 个月； {data.QueDingZhongXinShu} 个中心", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table4.AddCell(nCell);

            document.Add(table4);

            #endregion

            #region (三)会议费用
            var title4 = new iTextSharp.text.Paragraph("(三)会议费用", font_Song_Bold_Title);
            title4.IndentationLeft = titleLeft;
            document.Add(title4);



            PdfPTable table5 = new PdfPTable(4);
            table5.SetWidthPercentage(new float[] { 50, 150, 150, 150 }, PageSize.A4);
            table5.SpacingBefore = spacingBefore;
            table5.SpacingAfter = spacingAfter;

            nCell = new PdfPCell(new Phrase("序号", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);


            nCell = new PdfPCell(new Phrase("内容", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("费用(人民币/ 万)", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("备注", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            //------------------------------------------------------------

            nCell = new PdfPCell(new Phrase("1", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("临床试验方案专 家讨论会", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.HuiYiFei.YanTaoHui, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("预计 1 次会议、预计专家人员 8 名(研 究者、科室专家、机构老师)，包含： 参会餐费、参会专家劳务费、差旅费、 住宿费、交通费等", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            //------------------------------------------------------------

            nCell = new PdfPCell(new Phrase("2", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("项目启动会", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.HuiYiFei.QiDongHui, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase($"{data.HuiYiFei.QiDongHui_Price} 元/中心,{data.QueDingZhongXinShu} 家中心", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            //------------------------------------------------------------

            nCell = new PdfPCell(new Phrase("3", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("数据审核会", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.HuiYiFei.ShenJiHui, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("预计 1 次会议、预计专家人员 8 名(研 究者、科室专家、机构老师)，包含： 参会餐费、参会专家劳务费、差旅费、 住宿费、交通费等", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);


            //------------------------------------------------------------

            nCell = new PdfPCell(new Phrase("4", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("合计", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.HuiYiFei.HeJi, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("/", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            //------------------------------------------------------------

            nCell = new PdfPCell(new Phrase("5", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("总计 (税率 6%)", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.HuiYiFei.HanShuiHeJi, font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("/", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table5.AddCell(nCell);

            document.Add(table5);

            #endregion

            #region (四) 临床试验研究费用
            var title5 = new iTextSharp.text.Paragraph("(四) 临床试验研究费用", font_Song_Bold_Title);
            title5.IndentationLeft = titleLeft;
            document.Add(title5);



            PdfPTable table6 = new PdfPTable(5);
            table6.SetWidthPercentage(new float[] { 50, 150, 100, 100, 100 }, PageSize.A4);
            table6.SpacingBefore = spacingBefore;
            table6.SpacingAfter = spacingAfter;

            nCell = new PdfPCell(new Phrase("类别", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("项目内容", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("数量", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("单价(万元)", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("小计(万元)", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);
            //-------------------------------------------------------------

            //竖行排列位置？？？
            nCell = new PdfPCell(new Phrase("医   院   研   究   费   用", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 7;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("伦理审查费", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.LunLiShenCha_Count, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);


            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.LunLiShenCha_Price, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.LunLiShenCha_Amount, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            //---------------------------------

            nCell = new PdfPCell(new Phrase("组长单位牵头费", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.ZuZhangDanWei_Count, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);


            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.ZuZhangDanWei_Price, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.ZuZhangDanWei_Amount, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            //---------------------------------

            nCell = new PdfPCell(new Phrase("研究者费", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.YanJiuZheFei_Count, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);


            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.YanJiuZheFei_Price, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.YanJiuZheFei_Amount, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            //---------------------------------


            nCell = new PdfPCell(new Phrase("受试者补贴", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 2;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Count1, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);


            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Price1, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Amount1, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);


            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Count2, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);


            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Price2, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Amount2, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);


            //---------------------------------

            nCell = new PdfPCell(new Phrase("受试者检查费用\r\n(以方案终稿及实际发生为准)", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.ShouShiZheJianCha_Count, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);


            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.ShouShiZheJianCha_Price, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.ShouShiZheJianCha_Amount, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            //---------------------------------

            nCell = new PdfPCell(new Phrase("研究机构管理费\r\n(含质控/资料保管费等)", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.JiGouGuanLi_Count, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);


            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.JiGouGuanLi_Price, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.JiGouGuanLi_Amount, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            //---------------------------------

            nCell = new PdfPCell(new Phrase("第三方费用", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("保险费用", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.BaoXian_Count, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);


            nCell = new PdfPCell(new Phrase("/", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("/", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table6.AddCell(nCell);

            //---------------------------------

            nCell = new PdfPCell(new Phrase("小计", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 2;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.HeJi, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 3;
            table6.AddCell(nCell);


            //---------------------------------


            nCell = new PdfPCell(new Phrase("合计 (税率 6%)", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 2;
            table6.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.LinChuangShiYanYanJiuFei.HanShuiHeJi, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 3;
            table6.AddCell(nCell);

            document.Add(table6);

            #endregion

            #region (五) SMO 费用
            var title6 = new iTextSharp.text.Paragraph("(五) SMO 费用", font_Song_Bold_Title);
            title6.IndentationLeft = titleLeft;
            document.Add(title6);



            PdfPTable table7 = new PdfPTable(3);
            table7.SetWidthPercentage(new float[] { 50, 150, 300 }, PageSize.A4);
            table7.SpacingBefore = spacingBefore;
            table7.SpacingAfter = spacingAfter;

            nCell = new PdfPCell(new Phrase("SMO 服务报价", font_Song_Bold));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 3;
            table7.AddCell(nCell);
            //------------------------------------------------------

            nCell = new PdfPCell(new Phrase("序号", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table7.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("项目", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table7.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("金额(单位：万元)", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table7.AddCell(nCell);

            //------------------------------------------------------

            nCell = new PdfPCell(new Phrase("1", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 2;
            table7.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("CRC 服务费用", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Rowspan = 2;
            table7.AddCell(nCell);

            nCell = new PdfPCell(new Phrase($"实验组： {data.SMOFei.CRC_FuWu_Amount1} ({data.SMOFei.CRC_FuWu_Count1}*{data.SMOFei.CRC_FuWu_Price1})", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table7.AddCell(nCell);

            nCell = new PdfPCell(new Phrase($"对照组： {data.SMOFei.CRC_FuWu_Amount2} ({data.SMOFei.CRC_FuWu_Count2}*{data.SMOFei.CRC_FuWu_Price2})", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table7.AddCell(nCell);
            //------------------------------------------------------

            nCell = new PdfPCell(new Phrase("2", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table7.AddCell(nCell);

            nCell = new PdfPCell(new Phrase("SMO 供应商管理费用", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table7.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.SMOFei.SMO_GongYingShangGuanLi, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table7.AddCell(nCell);

            //------------------------------------------------------

            nCell = new PdfPCell(new Phrase("合计 (含税)", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 2;
            table7.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.SMOFei.HanShuiHeJi, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table7.AddCell(nCell);

            //------------------------------------------------------

            nCell = new PdfPCell(new Phrase("折合每例(含税)", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 2;
            table7.AddCell(nCell);

            nCell = new PdfPCell(new Phrase(data.SMOFei.AVG, font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            table7.AddCell(nCell);

            //------------------------------------------------------

            nCell = new PdfPCell(new Phrase($"备注：1.共计 {data.ZongBingLiShu} 例受试者， {data.QueDingZhongXinShu} 家中心， 每家中心 1 名 CRC，1 名 CRC 项目经理；\r\n2.其他费用如，通讯费，交通费， 打印传真和快递费。", font_Song));
            nCell.HorizontalAlignment = Element.ALIGN_LEFT;
            nCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nCell.Padding = padding;
            nCell.Colspan = 3;
            table7.AddCell(nCell);


            document.Add(table7);
            #endregion


            document.Close();


            CreateDoc(data);


            return path;
        }
        public string CreateDoc(ShiYanFangAnDto data)
        {
            string path = $"{data.CompanyName}.docx";

            using (FileStream fs = new FileStream(@"D:\\web\\web6\\wwwroot\\web\\pdf\\PDF\\" + path, FileMode.Create))
            {
                using (XWPFDocument m_Doc = new XWPFDocument())
                {
                    m_Doc.Document.body.sectPr = new CT_SectPr();
                    var sectPr = m_Doc.Document.body.sectPr;
                    sectPr.pgMar.left = 1450;
                    sectPr.pgMar.right = 1450;

                    #region 四医疗器械临床研究费用预算清单
                    XWPFParagraph paragraph = m_Doc.CreateParagraph();
                    XWPFRun run = paragraph.CreateRun();
                    //换页（本页未满直接写下一页）
                    //paragraph = doc.CreateParagraph();
                    //paragraph.CreateRun().AddBreak(BreakType.PAGE);

                    addTitle(paragraph, run, "医疗器械临床研究费用预算清单");
                    XWPFTable table = m_Doc.CreateTable(8, 3);
                    var tblLayout1 = table.GetCTTbl().tblPr.AddNewTblLayout();
                    tblLayout1.type = ST_TblLayoutType.@fixed;

                    table.SetColumnWidth(0, 2500);
                    table.SetColumnWidth(1, 3000);
                    table.SetColumnWidth(2, 3500);
                    table.SetCellMargins(20, 20, 20, 20);

                    addCell(paragraph, run, table, 0, 0, "产品", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table, 0, 1, "项目", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table, 0, 2, "费用(单位：人民币/万元)", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table, 1, 0, data.ProjectName, true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table, 1, 1, "CRO 服务费用(包含数统)");
                    addCell(paragraph, run, table, 2, 1, "日常费用(包含差旅费用)");
                    addCell(paragraph, run, table, 3, 1, "会议费用");
                    addCell(paragraph, run, table, 4, 1, "CRO 服务费合计(税 6%)", true);
                    addCell(paragraph, run, table, 5, 1, "医院研究费用", true);
                    addCell(paragraph, run, table, 6, 1, "SMO 费用", true);
                    addCell(paragraph, run, table, 7, 0, "合计(税 6%)", true, ParagraphAlignment.CENTER);

                    addCell(paragraph, run, table, 1, 2, data.YanJiuYuSuanQingDan.CRO_FuWuFei, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table, 2, 2, data.YanJiuYuSuanQingDan.RiChangFei, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table, 3, 2, data.YanJiuYuSuanQingDan.HuiYiFei, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table, 4, 2, data.YanJiuYuSuanQingDan.CRO_FuWuHeJi, true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table, 5, 2, data.YanJiuYuSuanQingDan.YiYuanYanJiuFei, true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table, 6, 2, data.YanJiuYuSuanQingDan.SMOFei, true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table, 7, 2, data.YanJiuYuSuanQingDan.HanShuiHeJi, true, ParagraphAlignment.CENTER);

                    MYMergeCells(table, 0, 0, 1, 6);
                    MYMergeCells(table, 0, 1, 7, 7);

                    #endregion

                    #region (一)临床试验服务费用
                    paragraph = m_Doc.CreateParagraph();
                    run = paragraph.CreateRun();
                    addTitle(paragraph, run, "(一)临床试验服务费用");
                    XWPFTable table2 = m_Doc.CreateTable(69, 3);
                    var tblLayout2 = table2.GetCTTbl().tblPr.AddNewTblLayout();
                    tblLayout2.type = ST_TblLayoutType.@fixed;
                    table2.SetColumnWidth(0, 4000);
                    table2.SetColumnWidth(1, 1500);
                    table2.SetColumnWidth(2, 3500);

                    addCell(paragraph, run, table2, 0, 0, "项       目", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table2, 0, 1, "费用(万元)", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table2, 0, 2, "备       注", true, ParagraphAlignment.CENTER);
                    //------------------------------------
                    addCell(paragraph, run, table2, 1, 0, "1.筛选研究单位", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 1, 2, data.LinChuangShiYanFuWuFei.ShaiXuanYanJiuDanWei, true, ParagraphAlignment.RIGHT);
                    MYMergeCells(table2, 0, 1, 1, 1);

                    addCell(paragraph, run, table2, 2, 0, "研究单位调研、筛选、实地拜访", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 2, 1, data.LinChuangShiYanFuWuFei.ShaiXuanYanJiuDanWei, true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table2, 2, 2, $"1.全国办事处进行调研， 以最 终确定研究单位({data.QueDingZhongXinShu} 家中心) ；", false, ParagraphAlignment.LEFT);
                    //------------------------------------ 
                    addCell(paragraph, run, table2, 3, 0, "2.临床试验方案设计 ", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 3, 2, data.LinChuangShiYanFuWuFei.LinChuangShiYanFangAnSheJi, true, ParagraphAlignment.RIGHT);
                    MYMergeCells(table2, 0, 1, 3, 3);

                    addCell(paragraph, run, table2, 4, 0, "临床研究方案修订、主持方案协调会及方 案定稿、 CRF、知情同意书设计、受试者文 件夹设计  ", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 4, 1, data.LinChuangShiYanFuWuFei.LinChuangShiYanFangAnSheJi, true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table2, 4, 2, "包括方案， 普通 CRF，无碳写 CRF", false, ParagraphAlignment.LEFT);
                    //------------------------------------
                    addCell(paragraph, run, table2, 5, 0, "3. CRO 服务费用 ", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 5, 2, data.LinChuangShiYanFuWuFei.CROFuWuFei, true, ParagraphAlignment.RIGHT);
                    MYMergeCells(table2, 0, 1, 5, 5);

                    addCell(paragraph, run, table2, 6, 0, "试验前准备 1", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 6, 1, data.LinChuangShiYanFuWuFei.CROFuWuFei_1, true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table2, 6, 2, $"{data.LinChuangShiYanFuWuFei.CROFuWuFei_1_Price} 元 *{data.QueDingZhongXinShu} 家中心", false, ParagraphAlignment.LEFT);
                    //------------------------------------

                    addCell(paragraph, run, table2, 7, 0, "机构立项资料准备、递交、立项", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 8, 0, "报伦理委员会的资料准备、递交、过会", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 9, 0, "研究者手册审核", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 10, 0, "建立研究者档案", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 11, 0, "试验前准备 2", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 12, 0, "临床试验协议商议、签订", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 13, 0, "试验前准备 3", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 14, 0, "研究者资料收集与整理", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 15, 0, "现场 GCP 培训", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 16, 0, "严重不良事件报告培训", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 17, 0, "受试者筛选的现场指导", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 18, 0, "试验文件/器械分发", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 19, 0, "试验器械、资料管理和讲解", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 20, 0, "质量控制讲解", false, ParagraphAlignment.LEFT);
                    MYMergeCells(table2, 1, 1, 6, 20);
                    MYMergeCells(table2, 2, 2, 6, 20);
                    //------------------------------------ 
                    addCell(paragraph, run, table2, 21, 0, "监查访视", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 21, 1, data.LinChuangShiYanFuWuFei.CROFuWuFei_2, true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table2, 21, 2, $"中心数：{data.QueDingZhongXinShu} 家中心；\r\n1 名 CRA/中心；", false, ParagraphAlignment.LEFT);
                    //------------------------------------

                    addCell(paragraph, run, table2, 22, 0, "知情同意书核查", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 23, 0, "病例报告表审核", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 24, 0, "病例报告与原始病历核对", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 25, 0, "试验安全性/依从性审核", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 26, 0, "试验管理文件审核", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 27, 0, "试验物资管理审核", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 28, 0, "不良事件和严重不良事件的记录", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 29, 0, "违背方案情况记录", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 30, 0, "入组进度", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 31, 0, "与研究者商讨解决问题", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 32, 0, "完成监查报告", false, ParagraphAlignment.LEFT);
                    MYMergeCells(table2, 1, 1, 21, 32);
                    MYMergeCells(table2, 2, 2, 21, 32);
                    //------------------------------------
                    addCell(paragraph, run, table2, 33, 0, "试验结束访视", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 33, 1, data.LinChuangShiYanFuWuFei.CROFuWuFei_3, true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table2, 33, 2, $"{data.LinChuangShiYanFuWuFei.CROFuWuFei_3_Price} 元 *{data.QueDingZhongXinShu} 家中心", false, ParagraphAlignment.LEFT);
                    //------------------------------------

                    addCell(paragraph, run, table2, 34, 0, "数据疑问和问题解决", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 35, 0, "回收试验物资", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 36, 0, "回收所有试验记录及文件", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 37, 0, "严重不良事件追踪", false, ParagraphAlignment.LEFT);
                    MYMergeCells(table2, 1, 1, 33, 37);
                    MYMergeCells(table2, 2, 2, 33, 37);
                    //------------------------------------
                    addCell(paragraph, run, table2, 38, 0, "项目管理", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 38, 1, data.LinChuangShiYanFuWuFei.CROFuWuFei_4, true, ParagraphAlignment.CENTER);
                    //------------------------------------

                    addCell(paragraph, run, table2, 39, 0, "项目管理计划", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 40, 0, "项目管理会议(内部、外部)", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 41, 0, "临床试验文档(TMF)管理", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 42, 0, "团队及沟通管理", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 43, 0, "申办方协同访视", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 44, 0, "研究中心 GCP 质控", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 45, 0, "研究中心付款及跟踪记录", false, ParagraphAlignment.LEFT);
                    MYMergeCells(table2, 1, 1, 38, 45);
                    MYMergeCells(table2, 2, 2, 38, 45);
                    //------------------------------------ 
                    addCell(paragraph, run, table2, 46, 0, "试验资料的内部专家审核", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 46, 1, data.LinChuangShiYanFuWuFei.CROFuWuFei_5, true, ParagraphAlignment.CENTER);
                    //------------------------------------

                    addCell(paragraph, run, table2, 47, 0, "综合审核会费用", false, ParagraphAlignment.LEFT);
                    MYMergeCells(table2, 1, 1, 46, 47);
                    MYMergeCells(table2, 2, 2, 46, 47);

                    //------------------------------------
                    addCell(paragraph, run, table2, 48, 0, "4. 数据管理和统计分析 ", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 48, 2, data.LinChuangShiYanFuWuFei.ShuJuGuanLiHeTongJiFenXi, true, ParagraphAlignment.RIGHT);
                    MYMergeCells(table2, 0, 1, 48, 48);

                    addCell(paragraph, run, table2, 49, 0, "数据管理", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 49, 1, data.LinChuangShiYanFuWuFei.ShuJuGuanLiHeTongJiFenXi_1, true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table2, 49, 2, "1.包含 EDC 系统费用；\r\n2.按照《医疗器械临床试验数据 递交要求注册审查指导原则》 \t(2021)要求需要提供原始数据 库、分析数据库、统计分析程序 代码、说明文件等；\r\n3.包含随机化系统", false, ParagraphAlignment.LEFT, 0, 0, 10.5);
                    //------------------------------------
                    addCell(paragraph, run, table2, 50, 0, "CRF 设计与核查", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 51, 0, "数据稽查", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 52, 0, "数据录入", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 53, 0, "数据逻辑检查", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 54, 0, "数据医学检查(专业检查)", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 55, 0, "数据质疑/质疑解决/质疑管理", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 56, 0, "数据库清理/更新", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 57, 0, "数据库锁定/提交/备份", false, ParagraphAlignment.LEFT);
                    MYMergeCells(table2, 1, 1, 49, 57);
                    MYMergeCells(table2, 2, 1, 49, 57);
                    //------------------------------------ 

                    addCell(paragraph, run, table2, 58, 0, "统计分析", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 58, 1, data.LinChuangShiYanFuWuFei.ShuJuGuanLiHeTongJiFenXi_2, true, ParagraphAlignment.CENTER);
                    //------------------------------------

                    addCell(paragraph, run, table2, 59, 0, "统计分析计划书", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 60, 0, "随机过程编程", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 61, 0, "统计分析编程", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 62, 0, "模拟数据统计测试", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 63, 0, "撰写统计分析报告", false, ParagraphAlignment.LEFT);
                    MYMergeCells(table2, 1, 1, 58, 63);
                    MYMergeCells(table2, 2, 1, 58, 63);
                    //------------------------------------ 
                    addCell(paragraph, run, table2, 64, 0, "5. 总结 ", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 64, 2, data.LinChuangShiYanFuWuFei.ZongJie, true, ParagraphAlignment.RIGHT);
                    MYMergeCells(table2, 0, 1, 64, 64);

                    addCell(paragraph, run, table2, 65, 0, "资料回收、归档", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 65, 1, data.LinChuangShiYanFuWuFei.ZongJie_1, true, ParagraphAlignment.CENTER);

                    addCell(paragraph, run, table2, 66, 0, "临床研究总结报告及盖章", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 66, 1, data.LinChuangShiYanFuWuFei.ZongJie_2, true, ParagraphAlignment.CENTER);
                    //------------------------------------

                    addCell(paragraph, run, table2, 67, 0, "6.合计 ", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 67, 1, data.LinChuangShiYanFuWuFei.HeJi, true, ParagraphAlignment.LEFT);
                    MYMergeCells(table2, 1, 2, 67, 67);
                    //------------------------------------

                    addCell(paragraph, run, table2, 68, 0, "7.含税总计(税率 6%) ", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table2, 68, 1, data.LinChuangShiYanFuWuFei.HanShuiHeJi, true, ParagraphAlignment.LEFT);
                    MYMergeCells(table2, 1, 2, 68, 68);
                    //-----------------------------------

                    #endregion

                    #region (二)日常费用

                    paragraph = m_Doc.CreateParagraph();
                    run = paragraph.CreateRun();
                    addTitle(paragraph, run, "(二)日常费用");
                    XWPFTable table3 = m_Doc.CreateTable(6, 4);
                    var tblLayout3 = table3.GetCTTbl().tblPr.AddNewTblLayout();
                    tblLayout2.type = ST_TblLayoutType.@fixed;
                    table3.SetColumnWidth(0, 800);
                    table3.SetColumnWidth(1, 3500);
                    table3.SetColumnWidth(2, 1500);
                    table3.SetColumnWidth(3, 3200);

                    addCell(paragraph, run, table3, 0, 0, "序号", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table3, 0, 1, "内容", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table3, 0, 2, "费用    (人民币： 万)", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table3, 0, 3, "备注", true, ParagraphAlignment.CENTER);

                    //-----------------------------------------
                    addCell(paragraph, run, table3, 1, 0, "1", false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table3, 1, 1, "办公费及通讯费", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table3, 1, 2, data.RiChangFei.BanGongTongXun, false, ParagraphAlignment.CENTER);
                    //-----------------------------------------
                    addCell(paragraph, run, table3, 2, 0, "2", false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table3, 2, 1, "临床试验文件印刷装订费用", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table3, 2, 2, data.RiChangFei.YinShuaZhuangDing, false, ParagraphAlignment.CENTER);

                    //-----------------------------------------
                    addCell(paragraph, run, table3, 3, 0, "3", false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table3, 3, 1, "差旅费", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table3, 3, 2, data.RiChangFei.ChaiLv, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table3, 3, 3, "见附表 1", false, ParagraphAlignment.LEFT);

                    //-----------------------------------------
                    addCell(paragraph, run, table3, 4, 0, "费 用 合计", true, ParagraphAlignment.CENTER, 2);
                    addCell(paragraph, run, table3, 4, 1, "税前- 日常支出", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table3, 4, 2, data.RiChangFei.HeJi, false, ParagraphAlignment.LEFT);

                    addCell(paragraph, run, table3, 5, 1, "税后- 日常支出(税率6%)", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table3, 5, 2, data.RiChangFei.HanShuiHeJi, true, ParagraphAlignment.LEFT);
                    #endregion

                    #region 附表 1
                    paragraph = m_Doc.CreateParagraph();
                    run = paragraph.CreateRun();
                    addTitle(paragraph, run, "附表 1");
                    XWPFTable table4 = m_Doc.CreateTable(4, 3);
                    var tblLayout4 = table4.GetCTTbl().tblPr.AddNewTblLayout();
                    tblLayout4.type = ST_TblLayoutType.@fixed;
                    table4.SetColumnWidth(0, 3500);
                    table4.SetColumnWidth(1, 2000);
                    table4.SetColumnWidth(2, 3500);

                    //-----------------------------------------------
                    addCell(paragraph, run, table4, 0, 0, "差旅费", true, ParagraphAlignment.LEFT, 0, 2);

                    //-----------------------------------------------
                    addCell(paragraph, run, table4, 1, 0, "项目经理差旅费、住宿、餐饮、 话费及补贴15", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table4, 1, 1, data.ChaiLvFuFeiBiao.PMChaiLvFei, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table4, 1, 2, $"按 1 人，每月 1 次，差旅费{data.ChaiLvFuFeiBiao.PMChaiLvFei_Price} 元/次，{data.ChaiLvFuFeiBiao.PMChaiLvFei_Price} 元 *{data.ZongShiXian} 个月； {data.QueDingZhongXinShu} 个中心", false, ParagraphAlignment.LEFT);

                    //-----------------------------------------------
                    addCell(paragraph, run, table4, 2, 0, "监查员差旅费、住宿、餐饮、话 费及补贴", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table4, 2, 1, data.ChaiLvFuFeiBiao.CRAChaiLvFei, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table4, 2, 2, $"按 1 人，每月 1 次，差旅费{data.ChaiLvFuFeiBiao.CRAChaiLvFei_Price} 元/次，{data.ChaiLvFuFeiBiao.CRAChaiLvFei_Price} 元 *{data.ZongShiXian} 个月； {data.QueDingZhongXinShu} 个中心", false, ParagraphAlignment.LEFT);

                    //-----------------------------------------------
                    addCell(paragraph, run, table4, 3, 0, "管理人员差旅、住宿、餐饮、话 费及补贴", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table4, 3, 1, data.ChaiLvFuFeiBiao.AdminChaiLvFei, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table4, 3, 2, $"按 1 人，每月 1 次，差旅费{data.ChaiLvFuFeiBiao.AdminChaiLvFei_Price} 元/次，{data.ChaiLvFuFeiBiao.AdminChaiLvFei_Price} 元 *{data.ZongShiXian} 个月； {data.QueDingZhongXinShu} 个中心", false, ParagraphAlignment.LEFT);

                    #endregion

                    #region (三)会议费用
                    paragraph = m_Doc.CreateParagraph();
                    run = paragraph.CreateRun();
                    addTitle(paragraph, run, "(三)会议费用");
                    XWPFTable table5 = m_Doc.CreateTable(6, 4);
                    var tblLayout5 = table5.GetCTTbl().tblPr.AddNewTblLayout();
                    tblLayout5.type = ST_TblLayoutType.@fixed;
                    table5.SetColumnWidth(0, 800);
                    table5.SetColumnWidth(1, 3500);
                    table5.SetColumnWidth(2, 1500);
                    table5.SetColumnWidth(3, 3200);

                    addCell(paragraph, run, table5, 0, 0, "序号", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table5, 0, 1, "内容", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table5, 0, 2, "费用    (人民币： 万)", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table5, 0, 3, "备注", true, ParagraphAlignment.CENTER);
                    //----------------------------------------------
                    addCell(paragraph, run, table5, 1, 0, "1", false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table5, 1, 1, "临床试验方案专 家讨论会", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table5, 1, 2, data.HuiYiFei.YanTaoHui, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table5, 1, 3, "预计 1 次会议、预计专家人员 8 名(研 究者、科室专家、机构老师)，包含： 参会餐费、参会专家劳务费、差旅费、 住宿费、交通费等", false, ParagraphAlignment.LEFT);
                    //----------------------------------------------
                    addCell(paragraph, run, table5, 2, 0, "2", false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table5, 2, 1, "项目启动会", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table5, 2, 2, data.HuiYiFei.QiDongHui, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table5, 2, 3, $"{data.HuiYiFei.QiDongHui_Price} 元/中心,{data.QueDingZhongXinShu} 家中心", false, ParagraphAlignment.LEFT);
                    //----------------------------------------------
                    addCell(paragraph, run, table5, 3, 0, "3", false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table5, 3, 1, "数据审核会", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table5, 3, 2, data.HuiYiFei.ShenJiHui, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table5, 3, 3, "预计 1 次会议、预计专家人员 8 名(研 究者、科室专家、机构老师)，包含： 参会餐费、参会专家劳务费、差旅费、 住宿费、交通费等", false, ParagraphAlignment.LEFT);
                    //----------------------------------------------
                    addCell(paragraph, run, table5, 4, 0, "4", false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table5, 4, 1, "合计", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table5, 4, 2, data.HuiYiFei.HeJi, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table5, 4, 3, "/", false, ParagraphAlignment.LEFT);
                    //----------------------------------------------
                    addCell(paragraph, run, table5, 5, 0, "5", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table5, 5, 1, "总计 (税率 6%)", true, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table5, 5, 2, data.HuiYiFei.HanShuiHeJi, true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table5, 5, 3, "/", true, ParagraphAlignment.LEFT);
                    #endregion

                    #region (四) 临床试验研究费用
                    paragraph = m_Doc.CreateParagraph();
                    run = paragraph.CreateRun();
                    addTitle(paragraph, run, "(四) 临床试验研究费用");
                    XWPFTable table6 = m_Doc.CreateTable(11, 5);
                    var tblLayout6 = table6.GetCTTbl().tblPr.AddNewTblLayout();
                    tblLayout5.type = ST_TblLayoutType.@fixed;
                    table6.SetColumnWidth(0, 1000);
                    table6.SetColumnWidth(1, 3500);
                    table6.SetColumnWidth(2, 1500);
                    table6.SetColumnWidth(3, 1500);
                    table6.SetColumnWidth(4, 1500);

                    addCell(paragraph, run, table6, 0, 0, "类别", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 0, 1, "项目内容", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 0, 2, "数量", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 0, 3, "单价(万元)", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 0, 4, "小计(万元)", true, ParagraphAlignment.CENTER);

                    //------------------------------------------------
                    addCell(paragraph, run, table6, 1, 0, "医     院     研     究     费     用", true, ParagraphAlignment.CENTER, 7);
                    addCell(paragraph, run, table6, 1, 1, "伦理审查费", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table6, 1, 2, data.LinChuangShiYanYanJiuFei.LunLiShenCha_Count, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 1, 3, data.LinChuangShiYanYanJiuFei.LunLiShenCha_Price, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 1, 4, data.LinChuangShiYanYanJiuFei.LunLiShenCha_Amount, false, ParagraphAlignment.CENTER);

                    //------------------------------------------------ 
                    addCell(paragraph, run, table6, 2, 1, "组长单位牵头费", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table6, 2, 2, data.LinChuangShiYanYanJiuFei.ZuZhangDanWei_Count, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 2, 3, data.LinChuangShiYanYanJiuFei.ZuZhangDanWei_Price, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 2, 4, data.LinChuangShiYanYanJiuFei.ZuZhangDanWei_Amount, false, ParagraphAlignment.CENTER);
                    //------------------------------------------------ 
                    addCell(paragraph, run, table6, 3, 1, "研究者费", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table6, 3, 2, data.LinChuangShiYanYanJiuFei.YanJiuZheFei_Count, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 3, 3, data.LinChuangShiYanYanJiuFei.YanJiuZheFei_Price, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 3, 4, data.LinChuangShiYanYanJiuFei.YanJiuZheFei_Amount, false, ParagraphAlignment.CENTER);
                    //------------------------------------------------ 
                    addCell(paragraph, run, table6, 4, 1, "受试者补贴", false, ParagraphAlignment.LEFT, 2);
                    addCell(paragraph, run, table6, 4, 2, data.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Count1, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 4, 3, data.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Price1, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 4, 4, data.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Amount1, false, ParagraphAlignment.CENTER);

                    addCell(paragraph, run, table6, 5, 2, data.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Count2, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 5, 3, data.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Price2, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 5, 4, data.LinChuangShiYanYanJiuFei.ShouShiZheBuTie_Amount2, false, ParagraphAlignment.CENTER);
                    //------------------------------------------------ 
                    addCell(paragraph, run, table6, 6, 1, "受试者检查费用\r\n(以方案终稿及实际发生为准)", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table6, 6, 2, data.LinChuangShiYanYanJiuFei.ShouShiZheJianCha_Count, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 6, 3, data.LinChuangShiYanYanJiuFei.ShouShiZheJianCha_Price, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 6, 4, data.LinChuangShiYanYanJiuFei.ShouShiZheJianCha_Amount, false, ParagraphAlignment.CENTER);
                    //------------------------------------------------ 
                    addCell(paragraph, run, table6, 7, 1, "研究机构管理费\r\n(含质控/资料保管费等)", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table6, 7, 2, data.LinChuangShiYanYanJiuFei.JiGouGuanLi_Count, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 7, 3, data.LinChuangShiYanYanJiuFei.JiGouGuanLi_Price, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 7, 4, data.LinChuangShiYanYanJiuFei.JiGouGuanLi_Amount, false, ParagraphAlignment.CENTER);
                    //------------------------------------------------ 
                    addCell(paragraph, run, table6, 8, 0, "第三方费用", true, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 8, 1, "保险费用", false, ParagraphAlignment.LEFT);
                    addCell(paragraph, run, table6, 8, 2, data.LinChuangShiYanYanJiuFei.BaoXian_Count, false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 8, 3, "/", false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table6, 8, 4, "/", false, ParagraphAlignment.CENTER);
                    //------------------------------------------------ 
                    addCell(paragraph, run, table6, 9, 0, "小计", true, ParagraphAlignment.CENTER, 0, 2);
                    addCell(paragraph, run, table6, 9, 1, data.LinChuangShiYanYanJiuFei.HeJi, false, ParagraphAlignment.CENTER, 0, 3);
                    //------------------------------------------------ 
                    addCell(paragraph, run, table6, 10, 0, "合计 (税率 6%)", true, ParagraphAlignment.CENTER, 0, 2);
                    addCell(paragraph, run, table6, 10, 1, data.LinChuangShiYanYanJiuFei.HanShuiHeJi, true, ParagraphAlignment.CENTER, 0, 3);
                    #endregion

                    #region (五) SMO 费用
                    paragraph = m_Doc.CreateParagraph();
                    run = paragraph.CreateRun();
                    addTitle(paragraph, run, "(五) SMO 费用");
                    XWPFTable table7 = m_Doc.CreateTable(8, 3);
                    var tblLayout7 = table7.GetCTTbl().tblPr.AddNewTblLayout();
                    tblLayout7.type = ST_TblLayoutType.@fixed;
                    table7.SetColumnWidth(0, 800);
                    table7.SetColumnWidth(1, 2700);
                    table7.SetColumnWidth(2, 5500);

                    addCell(paragraph, run, table7, 0, 0, "SMO 服务报价", true, ParagraphAlignment.CENTER, 0, 3);

                    //-----------------------------------
                    addCell(paragraph, run, table7, 1, 0, "序号", false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table7, 1, 1, "项目", false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table7, 1, 2, "金额(单位：万元)", false, ParagraphAlignment.CENTER);

                    //-----------------------------------
                    addCell(paragraph, run, table7, 2, 0, "1", false, ParagraphAlignment.CENTER, 2);
                    addCell(paragraph, run, table7, 2, 1, "CRC 服务费用", false, ParagraphAlignment.CENTER, 2);
                    addCell(paragraph, run, table7, 2, 2, $"实验组： {data.SMOFei.CRC_FuWu_Amount1} ({data.SMOFei.CRC_FuWu_Count1}*{data.SMOFei.CRC_FuWu_Price1})", false, ParagraphAlignment.CENTER);
                    //-----------------------------------  
                    addCell(paragraph, run, table7, 3, 2, $"对照组： {data.SMOFei.CRC_FuWu_Amount2} ({data.SMOFei.CRC_FuWu_Count2}*{data.SMOFei.CRC_FuWu_Price2})", false, ParagraphAlignment.CENTER);
                    //-----------------------------------
                    addCell(paragraph, run, table7, 4, 0, "2", false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table7, 4, 1, "SMO 供应商管理费用", false, ParagraphAlignment.CENTER);
                    addCell(paragraph, run, table7, 4, 2, data.SMOFei.SMO_GongYingShangGuanLi, false, ParagraphAlignment.CENTER);

                    //-----------------------------------
                    addCell(paragraph, run, table7, 5, 0, "合计 (含税)", false, ParagraphAlignment.CENTER, 0, 2);
                    addCell(paragraph, run, table7, 5, 1, data.SMOFei.HanShuiHeJi, false, ParagraphAlignment.CENTER);

                    //-----------------------------------
                    addCell(paragraph, run, table7, 6, 0, "折合每例(含税)", false, ParagraphAlignment.CENTER, 0, 2);
                    addCell(paragraph, run, table7, 6, 1, data.SMOFei.AVG, false, ParagraphAlignment.CENTER);

                    //-----------------------------------
                    addCell(paragraph, run, table7, 7, 0, $"备注：1.共计 {data.ZongBingLiShu} 例受试者， {data.QueDingZhongXinShu} 家中心， 每家中心 1 名 CRC，1 名 CRC 项目经理；\r\n2.其他费用如，通讯费，交通费， 打印传真和快递费。", false, ParagraphAlignment.CENTER, 0, 3, 10.5);
                    #endregion

                    m_Doc.Write(fs);
                }
            }

            return path;
        }

        public void addTitle(XWPFParagraph paragraph, XWPFRun run, string text)
        {
            paragraph.Alignment = ParagraphAlignment.LEFT;
            paragraph.SpacingBeforeLines = 80;
            paragraph.SpacingAfterLines = 40;
            run.FontFamily = "仿宋";
            run.FontSize = 12;
            run.IsBold = true;
            run.SetText(text);
        }



        public void addCell(XWPFParagraph paragraph, XWPFRun run, XWPFTable table, int row, int cell, string text = "", bool IsBold = false, ParagraphAlignment alignment = ParagraphAlignment.LEFT, int rowSpan = 0, int colSpan = 0, double fontSize = 12)
        {
            CT_P para = new CT_P();
            paragraph = new XWPFParagraph(para, table.Body);
            paragraph.Alignment = alignment;
            paragraph.VerticalAlignment = TextAlignment.CENTER;
            paragraph.IndentFromLeft = 100;
            paragraph.IndentFromRight = 100;
            paragraph.SpacingBeforeLines = 15;
            paragraph.SpacingAfterLines = 15;
            run = paragraph.CreateRun();
            run.FontFamily = "仿宋";
            run.FontSize = fontSize;
            run.IsBold = IsBold;
            run.SetText(text);
            var c = table.GetRow(row).GetCell(cell);
            c.SetParagraph(paragraph);
            if (rowSpan > 0)
            {
                MYMergeCells(table, cell, cell, row, row + rowSpan - 1);
            }
            if (colSpan > 0)
            {
                MYMergeCells(table, cell, cell + colSpan - 1, row, row);
            }
            c.SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);//垂直居中

        }



        public static XWPFTableCell MYMergeCells(XWPFTable table, int fromCol, int toCol, int fromRow, int toRow)
        {
            for (int rowIndex = fromRow; rowIndex <= toRow; rowIndex++)
            {
                if (fromCol < toCol)
                {
                    table.GetRow(rowIndex).MergeCells(fromCol, toCol);
                }
                XWPFTableCell rowcell = table.GetRow(rowIndex).GetCell(fromCol);
                CT_Tc cttc = rowcell.GetCTTc();
                if (cttc.tcPr == null)
                {
                    cttc.AddNewTcPr();
                }
                if (rowIndex == fromRow)
                {
                    // The first merged cell is set with RESTART merge value  
                    rowcell.GetCTTc().tcPr.AddNewVMerge().val = ST_Merge.restart;
                }
                else
                {
                    // Cells which join (merge) the first one, are set with CONTINUE  
                    rowcell.GetCTTc().tcPr.AddNewVMerge().val = ST_Merge.@continue;
                }
            }
            table.GetRow(fromRow).GetCell(fromCol).SetVerticalAlignment(XWPFTableCell.XWPFVertAlign.CENTER);
            table.GetRow(fromRow).GetCell(fromCol).Paragraphs[0].Alignment = ParagraphAlignment.CENTER;
            return table.GetRow(fromRow).GetCell(fromCol);
        }



        public async Task<object> ModuleList(string id, string papes, string cid, string isen)
        {
            string str = "SELECT * FROM AirtelBaoJai ";
            var list = GetGoalsEntity(str);
            return new { isen, cid, papes, id, list };
        }
        public async Task<object> AddInfo(CreateBaoJiaDanInput input)
        {
            var rusult = string.Empty;
            var strsql = "INSERT INTO AirtelBaoJai ( CompanyName, ProjectName, Yushuan, Other, Type, LiShu, ShiXian, ZhongXinShu, NanDu, ShiYingZheng, SuiFang, XieTiaoHui_HuiWuFei, XieTiaoHui_JiaoTongFei, XieTiaoHui_ZhuSuFei, XieTiaoHui_HuiYiShiFei, XieTiaoHui_QiTaFei, ZongJieHui_HuiWuFei, ZongJieHui_JiaoTongFei, ZongJieHui_ZhuSuFei, ZongJieHui_HuiYiShiFei, ZongJieHui_QiTaFei, ZhongQiHui_HuiWuFei, ZhongQiHui_JiaoTongFei, ZhongQiHui_ZhuSuFei, ZhongQiHui_HuiYiShiFei, ZhongQiHui_QiTaFei, MangTaiShenHeHui_HuiWuFei, MangTaiShenHeHui_JiaoTongFei, MangTaiShenHeHui_ZhuSuFei, MangTaiShenHeHui_HuiYiShiFei, MangTaiShenHeHui_QiTaFei, ZuZhangFei_Price, ZuZhangFei_Count, LunLiFei_Price, QiDongHuiFei_Price, YunShuFei_Price, ShiJiHaoCaiFei_Price, ShiJiHaoCaiFei_Count, ZhongXinShiYanShiFei_Price, ShuJuTongJiFei_Price, XiTongShiYongFei_Price, YinShuaFei_Price, QiTaCaiGouFei_Price, QiTaCaiGouFei_Count, ShouShiZheZhaoMuFei_Price, ShouShiZheZhaoMuFei_Count, ShengNeiJiChaFei_Price, ShengNeiJiChaFei_Count, ShengWaiJiChaFei_Price, ShengWaiJiChaFei_Count, SMOFei_Price, WeiWaiJianChaFuWuFei_Price, WeiWaiJianChaFuWuFei_Count, YiChuanBanTianBaoFei_Price, QiTaFei_Price, GuanLiRenYuanFei_Price, PMFei_Price, PLFei_Price, CTAFei_Price, XieTongJianChaFei_Price, YiXueZhiChiZhuanXieFei_Price, YiXueZhiChiJianChaFei_Price, ZhiKongZhiChiFei_Price, PVZhiChiFei_Price, PVZhiChiFei_Count, XiangMuJiangLiFei_Price, PM_ChuChai_Price, PM_BuChuChai_Price, CRA_ChuChai_Price, CRA_BuChuChai_Price, Admin_ChuChai_Price, Admin_BuChuChai_Price, XieYi_ShaiXuanQi, XieYi_RuZu, FeiXieYi, ShaiXuanQiJianChaFei, RuZuHouJianChaFei, V1, V2, V3, V4, V5,AE,DanLiJianChaFei,DanLiShouShiZheBuZhu,CreateTime) VALUES ("
                               + "'" + input.CompanyName + @"',
                                '" + input.ProjectName + @"',
                                '" + input.Yushuan + @"',
                                '" + input.Other + @"',
                                '" + input.Type + @"',
                                '" + input.LiShu + @"',
                                '" + input.ShiXian + @"',
                                '" + input.ZhongXinShu + @"',
                                '" + input.NanDu.ObjToInt() + @"',
                                '" + input.ShiYingZheng + @"',
                                '" + input.SuiFang + @"',
                                '" + input.XieTiaoHui.HuiWuFei + @"',
                                '" + input.XieTiaoHui.JiaoTongFei + @"',
                                '" + input.XieTiaoHui.ZhuSuFei + @"',
                                '" + input.XieTiaoHui.HuiYiShiFei + @"',
                                '" + input.XieTiaoHui.QiTaFei + @"',
                                '" + input.ZongJieHui.HuiWuFei + @"',
                                '" + input.ZongJieHui.JiaoTongFei + @"',
                                '" + input.ZongJieHui.ZhuSuFei + @"',
                                '" + input.ZongJieHui.HuiYiShiFei + @"',
                                '" + input.ZongJieHui.QiTaFei + @"',
                                '" + input.ZhongQiHui.HuiWuFei + @"',
                                '" + input.ZhongQiHui.JiaoTongFei + @"',
                                '" + input.ZhongQiHui.ZhuSuFei + @"',
                                '" + input.ZhongQiHui.HuiYiShiFei + @"',
                                '" + input.ZhongQiHui.QiTaFei + @"',
                                '" + input.MangTaiShenHeHui.HuiWuFei + @"',
                                '" + input.MangTaiShenHeHui.JiaoTongFei + @"',
                                '" + input.MangTaiShenHeHui.ZhuSuFei + @"',
                                '" + input.MangTaiShenHeHui.HuiYiShiFei + @"',
                                '" + input.MangTaiShenHeHui.QiTaFei + @"',
                                '" + input.ZuZhangFei_Price + @"',
                                '" + input.ZuZhangFei_Count + @"',
                                '" + input.LunLiFei_Price + @"',
                                '" + input.QiDongHuiFei_Price + @"',
                                '" + input.YunShuFei_Price + @"',
                                '" + input.ShiJiHaoCaiFei_Price + @"',
                                '" + input.ShiJiHaoCaiFei_Count + @"',
                                '" + input.ZhongXinShiYanShiFei_Price + @"',
                                '" + input.ShuJuTongJiFei_Price + @"',
                                '" + input.XiTongShiYongFei_Price + @"',
                                '" + input.YinShuaFei_Price + @"',
                                '" + input.QiTaCaiGouFei_Price + @"',
                                '" + input.QiTaCaiGouFei_Count + @"',
                                '" + input.ShouShiZheZhaoMuFei_Price + @"',
                                '" + input.ShouShiZheZhaoMuFei_Count + @"',
                                '" + input.ShengNeiJiChaFei_Price + @"',
                                '" + input.ShengNeiJiChaFei_Count + @"',
                                '" + input.ShengWaiJiChaFei_Price + @"',
                                '" + input.ShengWaiJiChaFei_Count + @"',
                                '" + input.SMOFei_Price + @"',
                                '" + input.WeiWaiJianChaFuWuFei_Price + @"',
                                '" + input.WeiWaiJianChaFuWuFei_Count + @"',
                                '" + input.YiChuanBanTianBaoFei_Price + @"',
                                '" + input.QiTaFei_Price + @"',
                                '" + input.GuanLiRenYuanFei_Price + @"',
                                '" + input.PMFei_Price + @"',
                                '" + input.PLFei_Price + @"',
                                '" + input.CTAFei_Price + @"',
                                '" + input.XieTongJianChaFei_Price + @"',
                                '" + input.YiXueZhiChiZhuanXieFei_Price + @"',
                                '" + input.YiXueZhiChiJianChaFei_Price + @"',
                                '" + input.ZhiKongZhiChiFei_Price + @"',
                                '" + input.PVZhiChiFei_Price + @"',
                                '" + input.PVZhiChiFei_Count + @"',
                                '" + input.XiangMuJiangLiFei_Price + @"',
                                '" + input.PM_ChuChai_Price + @"',
                                '" + input.PM_BuChuChai_Price + @"',
                                '" + input.CRA_ChuChai_Price + @"',
                                '" + input.CRA_BuChuChai_Price + @"',
                                '" + input.Admin_ChuChai_Price + @"',
                                '" + input.Admin_BuChuChai_Price + @"',
                                '" + input.XieYi_ShaiXuanQi + @"',
                                '" + input.XieYi_RuZu + @"',
                                '" + input.FeiXieYi + @"',
                                '" + input.ShaiXuanQiJianChaFei + @"',
                                '" + input.RuZuHouJianChaFei + @"',
                                '" + input.V1 + @"',
                                '" + input.V2 + @"',
                                '" + input.V3 + @"',
                                '" + input.V4 + @"',
                                '" + input.V5 + @"',
                                '" + input.AE + @"',
                                '" + input.DanLiJianChaFei + @"',
                                '" + input.DanLiShouShiZheBuZhu + @"',
                                '" + DateTime.Now + "')";
            rusult = GetIntegralByMonth(strsql);
            return new { rusult, CompanyName = input.CompanyName, ProjectName = input.ProjectName, Yushuan = input.Yushuan, ShiYingZheng = input.ShiYingZheng, Other = input.Other, Type = input.Other, LiShu = input.LiShu, ShiXian = input.LiShu, ZhongXinShu = input.ZhongXinShu };
        }
        public async Task<string> UPDATEInfo(UpdateBaoJiaDanInput input)
        {
            string sql23 = "UPDATE  AirtelBaoJai SET    CompanyName = '" + input.CompanyName + @"',
                                                        ProjectName = '" + input.ProjectName + @"',
                                                        Yushuan = '" + input.Yushuan + @"',
                                                        Other = '" + input.Other + @"',
                                                        Type = '" + input.Type + @"',
                                                        LiShu = '" + input.LiShu + @"',
                                                        ShiXian = '" + input.ShiXian + @"',
                                                        ZhongXinShu = '" + input.ZhongXinShu + @"',
                                                        NanDu = '" + input.NanDu.ObjToInt() + @"',
                                                        ShiYingZheng = '" + input.ShiYingZheng + @"',
                                                        SuiFang = '" + input.SuiFang + @"',
                                                        XieTiaoHui_HuiWuFei = '" + input.XieTiaoHui.HuiWuFei + @"',
                                                        XieTiaoHui_JiaoTongFei = '" + input.XieTiaoHui.JiaoTongFei + @"',
                                                        XieTiaoHui_ZhuSuFei = '" + input.XieTiaoHui.ZhuSuFei + @"',
                                                        XieTiaoHui_HuiYiShiFei = '" + input.XieTiaoHui.HuiYiShiFei + @"',
                                                        XieTiaoHui_QiTaFei = '" + input.XieTiaoHui.QiTaFei + @"',
                                                        ZongJieHui_HuiWuFei = '" + input.ZongJieHui.HuiWuFei + @"',
                                                        ZongJieHui_JiaoTongFei = '" + input.ZongJieHui.JiaoTongFei + @"',
                                                        ZongJieHui_ZhuSuFei = '" + input.ZongJieHui.ZhuSuFei + @"',
                                                        ZongJieHui_HuiYiShiFei = '" + input.ZongJieHui.HuiYiShiFei + @"',
                                                        ZongJieHui_QiTaFei = '" + input.ZongJieHui.QiTaFei + @"',
                                                        ZhongQiHui_HuiWuFei = '" + input.ZhongQiHui.HuiWuFei + @"',
                                                        ZhongQiHui_JiaoTongFei = '" + input.ZhongQiHui.JiaoTongFei + @"',
                                                        ZhongQiHui_ZhuSuFei = '" + input.ZhongQiHui.ZhuSuFei + @"',
                                                        ZhongQiHui_HuiYiShiFei = '" + input.ZhongQiHui.HuiYiShiFei + @"',
                                                        ZhongQiHui_QiTaFei = '" + input.ZhongQiHui.QiTaFei + @"',
                                                        MangTaiShenHeHui_HuiWuFei = '" + input.MangTaiShenHeHui.HuiWuFei + @"',
                                                        MangTaiShenHeHui_JiaoTongFei = '" + input.MangTaiShenHeHui.JiaoTongFei + @"',
                                                        MangTaiShenHeHui_ZhuSuFei = '" + input.MangTaiShenHeHui.ZhuSuFei + @"',
                                                        MangTaiShenHeHui_HuiYiShiFei = '" + input.MangTaiShenHeHui.HuiYiShiFei + @"',
                                                        MangTaiShenHeHui_QiTaFei = '" + input.MangTaiShenHeHui.QiTaFei + @"',
                                                        ZuZhangFei_Price = '" + input.ZuZhangFei_Price + @"',
                                                        ZuZhangFei_Count = '" + input.ZuZhangFei_Count + @"',
                                                        LunLiFei_Price = '" + input.LunLiFei_Price + @"',
                                                        QiDongHuiFei_Price = '" + input.QiDongHuiFei_Price + @"',
                                                        YunShuFei_Price = '" + input.YunShuFei_Price + @"',
                                                        ShiJiHaoCaiFei_Price = '" + input.ShiJiHaoCaiFei_Price + @"',
                                                        ShiJiHaoCaiFei_Count = '" + input.ShiJiHaoCaiFei_Count + @"',
                                                        ZhongXinShiYanShiFei_Price = '" + input.ZhongXinShiYanShiFei_Price + @"',
                                                        ShuJuTongJiFei_Price = '" + input.ShuJuTongJiFei_Price + @"',
                                                        XiTongShiYongFei_Price = '" + input.XiTongShiYongFei_Price + @"',
                                                        YinShuaFei_Price = '" + input.YinShuaFei_Price + @"',
                                                        QiTaCaiGouFei_Price = '" + input.QiTaCaiGouFei_Price + @"',
                                                        QiTaCaiGouFei_Count = '" + input.QiTaCaiGouFei_Count + @"',
                                                        ShouShiZheZhaoMuFei_Price = '" + input.ShouShiZheZhaoMuFei_Price + @"',
                                                        ShouShiZheZhaoMuFei_Count = '" + input.ShouShiZheZhaoMuFei_Count + @"',
                                                        ShengNeiJiChaFei_Price = '" + input.ShengNeiJiChaFei_Price + @"',
                                                        ShengNeiJiChaFei_Count = '" + input.ShengNeiJiChaFei_Count + @"',
                                                        ShengWaiJiChaFei_Price = '" + input.ShengWaiJiChaFei_Price + @"',
                                                        ShengWaiJiChaFei_Count = '" + input.ShengWaiJiChaFei_Count + @"',
                                                        SMOFei_Price = '" + input.SMOFei_Price + @"',
                                                        WeiWaiJianChaFuWuFei_Price = '" + input.WeiWaiJianChaFuWuFei_Price + @"',
                                                        WeiWaiJianChaFuWuFei_Count = '" + input.WeiWaiJianChaFuWuFei_Count + @"',
                                                        YiChuanBanTianBaoFei_Price = '" + input.YiChuanBanTianBaoFei_Price + @"',
                                                        QiTaFei_Price = '" + input.QiTaFei_Price + @"',
                                                        GuanLiRenYuanFei_Price = '" + input.GuanLiRenYuanFei_Price + @"',
                                                        PMFei_Price = '" + input.PMFei_Price + @"',
                                                        PLFei_Price = '" + input.PLFei_Price + @"',
                                                        CTAFei_Price = '" + input.CTAFei_Price + @"',
                                                        XieTongJianChaFei_Price = '" + input.XieTongJianChaFei_Price + @"',
                                                        YiXueZhiChiZhuanXieFei_Price = '" + input.YiXueZhiChiZhuanXieFei_Price + @"',
                                                        YiXueZhiChiJianChaFei_Price = '" + input.YiXueZhiChiJianChaFei_Price + @"',
                                                        ZhiKongZhiChiFei_Price = '" + input.ZhiKongZhiChiFei_Price + @"',
                                                        PVZhiChiFei_Price = '" + input.PVZhiChiFei_Price + @"',
                                                        PVZhiChiFei_Count = '" + input.PVZhiChiFei_Count + @"',
                                                        XiangMuJiangLiFei_Price = '" + input.XiangMuJiangLiFei_Price + @"',
                                                        PM_ChuChai_Price = '" + input.PM_ChuChai_Price + @"',
                                                        PM_BuChuChai_Price = '" + input.PM_BuChuChai_Price + @"',
                                                        CRA_ChuChai_Price = '" + input.CRA_ChuChai_Price + @"',
                                                        CRA_BuChuChai_Price = '" + input.CRA_BuChuChai_Price + @"',
                                                        Admin_ChuChai_Price = '" + input.Admin_ChuChai_Price + @"',
                                                        Admin_BuChuChai_Price = '" + input.Admin_BuChuChai_Price + @"',
                                                        XieYi_ShaiXuanQi = '" + input.XieYi_ShaiXuanQi + @"',
                                                        XieYi_RuZu = '" + input.XieYi_RuZu + @"',
                                                        FeiXieYi = '" + input.FeiXieYi + @"',
                                                        ShaiXuanQiJianChaFei = '" + input.ShaiXuanQiJianChaFei + @"',
                                                        RuZuHouJianChaFei = '" + input.RuZuHouJianChaFei + @"',
                                                        AE'" + input.AE + @"',
                                                        DanLiJIanChaFei'" + input.DanLiJianChaFei + @"',
                                                        DanLiShouShiZheBuZhu'" + input.DanLiShouShiZheBuZhu + @"',
                                                        V1 = '" + input.V1 + @"',
                                                        V2 = '" + input.V2 + @"',
                                                        V3 = '" + input.V3 + @"',
                                                        V4 = '" + input.V4 + @"',
                                                        V5 = '" + input.V5 + @"' where Id  = '" + input.Id + "'";
            string rusult23 = GetIntegralByMonth(sql23);

            return "成修成功！";
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
                month = "成功";
                dr.Close();//关闭DataReader对象  
            }
            catch
            {

            }
            return month;

        }
        private IList<ArticleBaojiaTable> GetGoalsEntity(string sqlstr, string filter = "")
        {
            var list1 = new List<ArticleBaojiaTable>();
            string conStr = "server=192.168.10.12;user=sa;pwd=123456;database=Blog";//连接字符串  
            SqlConnection conText = new SqlConnection(conStr);//创建Connection对象 
            conText.Open();//打开数据库  
            string sqls = sqlstr;//创建统计语句  
            SqlCommand comText = new SqlCommand(sqls, conText);//创建Command对象  
            SqlDataReader dr;//创建DataReader对象  
            dr = comText.ExecuteReader();//执行查询  
            while (dr.Read())//判断数据表中是否含有数据  
            {
                var i = new ArticleBaojiaTable();
                var date = dr;
                i.Id = date["Id"].ToString();
                i.CompanyName = date["CompanyName"].ToString();
                i.ProjectName = date["ProjectName"].ToString();
                i.Yushuan = date["Yushuan"].ToString();
                i.Other = date["Other"].ToString();
                i.Type = date["Type"].ToString();
                i.LiShu = date["LiShu"].ToString();
                i.ShiXian = date["ShiXian"].ToString();
                i.ZhongXinShu = date["ZhongXinShu"].ToString();
                i.NanDu = date["NanDu"].ToString();
                i.ShiYingZheng = date["ShiYingZheng"].ToString();
                i.SuiFang = date["SuiFang"].ToString();
                i.XieTiaoHui_HuiWuFei = date["XieTiaoHui_HuiWuFei"].ToString();
                i.XieTiaoHui_JiaoTongFei = date["XieTiaoHui_JiaoTongFei"].ToString();
                i.XieTiaoHui_ZhuSuFei = date["XieTiaoHui_ZhuSuFei"].ToString();
                i.XieTiaoHui_HuiYiShiFei = date["XieTiaoHui_HuiYiShiFei"].ToString();
                i.XieTiaoHui_QiTaFei = date["XieTiaoHui_QiTaFei"].ToString();
                i.ZhongQiHui_HuiWuFei = date["ZhongQiHui_HuiWuFei"].ToString();
                i.ZhongQiHui_JiaoTongFei = date["ZhongQiHui_JiaoTongFei"].ToString();
                i.ZhongQiHui_ZhuSuFei = date["ZhongQiHui_ZhuSuFei"].ToString();
                i.ZhongQiHui_HuiYiShiFei = date["ZhongQiHui_HuiYiShiFei"].ToString();
                i.ZhongQiHui_QiTaFei = date["ZhongQiHui_QiTaFei"].ToString();
                i.ZongJieHui_HuiWuFei = date["ZongJieHui_HuiWuFei"].ToString();
                i.ZongJieHui_JiaoTongFei = date["ZongJieHui_JiaoTongFei"].ToString();
                i.ZongJieHui_ZhuSuFei = date["ZongJieHui_ZhuSuFei"].ToString();
                i.ZongJieHui_HuiYiShiFei = date["ZongJieHui_HuiYiShiFei"].ToString();
                i.ZongJieHui_QiTaFei = date["ZongJieHui_QiTaFei"].ToString();
                i.MangTaiShenHeHui_HuiWuFei = date["MangTaiShenHeHui_HuiWuFei"].ToString();
                i.MangTaiShenHeHui_JiaoTongFei = date["MangTaiShenHeHui_JiaoTongFei"].ToString();
                i.MangTaiShenHeHui_ZhuSuFei = date["MangTaiShenHeHui_ZhuSuFei"].ToString();
                i.MangTaiShenHeHui_HuiYiShiFei = date["MangTaiShenHeHui_HuiYiShiFei"].ToString();
                i.MangTaiShenHeHui_QiTaFei = date["MangTaiShenHeHui_QiTaFei"].ToString();
                i.ZuZhangFei_Price = date["ZuZhangFei_Price"].ToString();
                i.ZuZhangFei_Count = date["ZuZhangFei_Count"].ToString();
                i.LunLiFei_Price = date["LunLiFei_Price"].ToString();
                i.QiDongHuiFei_Price = date["QiDongHuiFei_Price"].ToString();
                i.YunShuFei_Price = date["YunShuFei_Price"].ToString();
                i.ShiJiHaoCaiFei_Price = date["ShiJiHaoCaiFei_Price"].ToString();
                i.ShiJiHaoCaiFei_Count = date["ShiJiHaoCaiFei_Count"].ToString();
                i.ZhongXinShiYanShiFei_Price = date["ZhongXinShiYanShiFei_Price"].ToString();
                i.ShuJuTongJiFei_Price = date["ShuJuTongJiFei_Price"].ToString();
                i.XiTongShiYongFei_Price = date["XiTongShiYongFei_Price"].ToString();
                i.YinShuaFei_Price = date["YinShuaFei_Price"].ToString();
                i.QiTaCaiGouFei_Price = date["QiTaCaiGouFei_Price"].ToString();
                i.QiTaCaiGouFei_Count = date["QiTaCaiGouFei_Count"].ToString();
                i.ShouShiZheZhaoMuFei_Price = date["ShouShiZheZhaoMuFei_Price"].ToString();
                i.ShouShiZheZhaoMuFei_Count = date["ShouShiZheZhaoMuFei_Count"].ToString();
                i.ShengNeiJiChaFei_Price = date["ShengNeiJiChaFei_Price"].ToString();
                i.ShengNeiJiChaFei_Count = date["ShengNeiJiChaFei_Count"].ToString();
                i.ShengWaiJiChaFei_Price = date["ShengWaiJiChaFei_Price"].ToString();
                i.ShengWaiJiChaFei_Count = date["ShengWaiJiChaFei_Count"].ToString();
                i.SMOFei_Price = date["SMOFei_Price"].ToString();
                i.WeiWaiJianChaFuWuFei_Price = date["WeiWaiJianChaFuWuFei_Price"].ToString();
                i.WeiWaiJianChaFuWuFei_Count = date["WeiWaiJianChaFuWuFei_Count"].ToString();
                i.YiChuanBanTianBaoFei_Price = date["YiChuanBanTianBaoFei_Price"].ToString();
                i.QiTaFei_Price = date["QiTaFei_Price"].ToString();
                i.GuanLiRenYuanFei_Price = date["GuanLiRenYuanFei_Price"].ToString();
                i.PMFei_Price = date["PMFei_Price"].ToString();
                i.PLFei_Price = date["PLFei_Price"].ToString();
                i.CTAFei_Price = date["CTAFei_Price"].ToString();
                i.XieTongJianChaFei_Price = date["XieTongJianChaFei_Price"].ToString();
                i.YiXueZhiChiZhuanXieFei_Price = date["YiXueZhiChiZhuanXieFei_Price"].ToString();
                i.YiXueZhiChiJianChaFei_Price = date["YiXueZhiChiJianChaFei_Price"].ToString();
                i.ZhiKongZhiChiFei_Price = date["ZhiKongZhiChiFei_Price"].ToString();
                i.PVZhiChiFei_Price = date["PVZhiChiFei_Price"].ToString();
                i.PVZhiChiFei_Count = date["PVZhiChiFei_Count"].ToString();
                i.XiangMuJiangLiFei_Price = date["XiangMuJiangLiFei_Price"].ToString();
                i.PM_ChuChai_Price = date["PM_ChuChai_Price"].ToString();
                i.PM_BuChuChai_Price = date["PM_BuChuChai_Price"].ToString();
                i.CRA_ChuChai_Price = date["CRA_ChuChai_Price"].ToString();
                i.CRA_BuChuChai_Price = date["CRA_BuChuChai_Price"].ToString();
                i.Admin_ChuChai_Price = date["Admin_ChuChai_Price"].ToString();
                i.Admin_BuChuChai_Price = date["Admin_BuChuChai_Price"].ToString();
                i.XieYi_ShaiXuanQi = date["XieYi_ShaiXuanQi"].ToString();
                i.XieYi_RuZu = date["XieYi_RuZu"].ToString();
                i.FeiXieYi = date["FeiXieYi"].ToString();
                i.ShaiXuanQiJianChaFei = date["ShaiXuanQiJianChaFei"].ToString();
                i.RuZuHouJianChaFei = date["RuZuHouJianChaFei"].ToString();
                i.V1 = date["V1"].ToString();
                i.V2 = date["V2"].ToString();
                i.V3 = date["V3"].ToString();
                i.V4 = date["V4"].ToString();
                i.V5 = date["V5"].ToString();
                i.CreateTime = date["CreateTime"].ToString();
                i.AE = date["AE"].ToString();
                i.DanLiJianChaFei = date["DanLiJianChaFei"].ToString();
                i.DanLiShouShiZheBuZhu = date["DanLiShouShiZheBuZhu"].ToString();

                list1.Add(i);
            }
            dr.Close();//关闭DataReader对象  
            return list1;
        }

    }


}
