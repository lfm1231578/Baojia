using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Dtos
{

    public class InitInput
    {
        /// <summary>
        /// 如果带上Id 则是修改，否则为新增
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 例数
        /// </summary>
        [Required]
        public int LiShu { get; set; }

        /// <summary>
        /// 总时限（月）
        /// </summary>
        [Required]
        public int ShiXian { get; set; }


        /// <summary>
        /// 确定中心数
        /// </summary>
        [Required]
        public int ZhongXinShu { get; set; }

        /// <summary>
        /// 难度
        /// </summary>
        [Required]
        public ENanDuXiShu NanDu { get; set; }
    }

}