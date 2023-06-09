using System;
using System.Collections.Generic;
using App.Core.Data;
using App.Core.Share;
using SqlSugar;

namespace App.Core.Entities.Blog
{
    /// <summary>
    /// 记录文章信息
    /// </summary>
    [Serializable]
    public class Articleiteminfo1 : Entity<string>, ISoftDelete
    {
        public string Title { get; set; }

        public string projectcount { get; set; }
        public string title { get; set; }
        public string mill { get; set; }
        public string other { get; set; }



        /// <summary>
        /// 是否删除
        /// </summary>
        public bool DeleteMark { get; set; }

    }
}
