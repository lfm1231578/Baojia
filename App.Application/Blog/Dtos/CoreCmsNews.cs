using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Xml.Linq;

namespace App.Application.Blog.Dtos
{
    /// <summary>
    /// 热门文章
    /// </summary>
    public class CoreCmsNews : EntityDto<string>
    {
        [Display(Name = "序列")]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        [Required(ErrorMessage = "请输入{0}")]
        public System.Int32 id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        [StringLength(maximumLength: 200, ErrorMessage = "{0}不能超过{1}字")]
        public System.String title { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [Display(Name = "简介")]
        [StringLength(maximumLength: 500, ErrorMessage = "{0}不能超过{1}字")]
        public System.String brief { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        [Display(Name = "封面图")]
        [StringLength(maximumLength: 200, ErrorMessage = "{0}不能超过{1}字")]
        public System.String coverImage { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        [Display(Name = "文章内容")]
        public System.String contentBody { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        [Display(Name = "分类id")]
        public System.Int32? typeId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        public System.Int32? sort { get; set; }
        /// <summary>
        /// 是否发布
        /// </summary>
        [Display(Name = "是否发布")]
        public System.Boolean? isPub { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Display(Name = "是否删除")]
        public System.Boolean? isDel { get; set; }
        /// <summary>
        /// 访问量
        /// </summary>
        [Display(Name = "访问量")]
        public System.Int32? pv { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public System.String? createTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        public System.String? updateTime { get; set; }
        /// <summary>
        /// 模块
        /// </summary>
        [Display(Name = "模块")]
        [StringLength(maximumLength: 500, ErrorMessage = "{0}不能超过{1}字")]
        public System.String modular { get; set; }
    }
}