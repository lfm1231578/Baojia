using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Dtos
{
    public class BaoJiaDanListOutput
    {
        /// <summary>
        /// 编号
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
        /// 例数
        /// </summary> 
        public string LiShu { get; set; }

        /// <summary>
        /// 总时限（月）
        /// </summary> 
        public string ShiXian { get; set; }


        /// <summary>
        /// 确定中心数
        /// </summary> 
        public string ZhongXinShu { get; set; }

        /// <summary>
        /// 难度
        /// </summary> 
        public string NanDu { get; set; } 

    }
}
