using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Dtos
{
    public class SearchBaoJiaDanInput : PageInputDto
    {

        /// <summary>
        /// 关键字查询
        /// </summary>
        public string Keywords { get; set; }
    }
}
