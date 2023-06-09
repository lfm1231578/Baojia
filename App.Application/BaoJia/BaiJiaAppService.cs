//using App.Application.Dtos;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace App.Application.BaoJia
//{
//    public class BaiJiaAppService
//    {
        
//        public class BaoJiaAppService : IDynamicApiController
//        {
//            private readonly IBaoJiaService _baoJiaService;

//            public BaoJiaAppService(IBaoJiaService baoJiaService)
//            {
//                _baoJiaService = baoJiaService;
//            }

//            /// <summary>
//            /// 初始化参数
//            /// </summary>
//            /// <param name="input"></param>
//            /// <returns></returns>
//            public CreateBaoJiaDanInput Init(InitInput input)
//            {
//                return _baoJiaService.Init(input);
//            }

//            /// <summary>
//            /// 生成报价单
//            /// </summary>
//            /// <param name="input"></param>
//            /// <returns></returns>
//            public CreateBaoJiaDanOutput CreateBaoJiaDan(CreateBaoJiaDanInput input)
//            {
//                return _baoJiaService.CreateBaoJiaDan(input);
//            }

//            /// <summary>
//            /// 文件下载
//            /// </summary>
//            /// <param name="name"></param>
//            /// <returns></returns>
//            [HttpGet]
//            public async Task<IActionResult> FileDownload(string name)
//            {

//                using (var stream = System.IO.File.OpenRead(name))
//                {
//                    //  var (stream, _) = await "https://furion.baiqian.ltd/img/rm1.png".GetAsStreamAsync();


//                    // 将 stream 转 byte[]
//                    byte[] bytes = new byte[stream.Length];
//                    await stream.ReadAsync(bytes);
//                    stream.Seek(0, SeekOrigin.Begin);

//                    return new FileContentResult(bytes, "application/octet-stream")
//                    {
//                        FileDownloadName = "" // 配置文件下载显示名
//                    };
//                }


//            }
//        }
//    }
//}
