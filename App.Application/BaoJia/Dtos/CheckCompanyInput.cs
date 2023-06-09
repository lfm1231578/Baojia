using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Dtos
{
    public class CheckCompanyInput
    {
        /// <summary>
        /// 委托方名称
        /// </summary>
        [Required]
        public string CompanyName { get; set; }

    }
}
