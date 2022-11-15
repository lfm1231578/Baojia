using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents
{
    /// <summary>
    /// 方法调用的带翻页的参数列表
    /// </summary>
    [Serializable]
    public class ParamListForPaging<T>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ParamListForPaging()
        {

        }

        private T _paramEntity;
        /// <summary>
        /// 参数实体
        /// </summary>
        public T ParamEntity
        {
            get { return this._paramEntity; }
            set { this._paramEntity = value; }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        private int _pageIndex = 1;
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }
        /// <summary>
        /// 当前页(用于接收jqgrid分页参数)
        /// </summary>
        public int page
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        /// <summary>
        /// 页大小
        /// </summary>
        private int _pageSize = 20;
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        /// <summary>
        /// 页大小(用于接收jqgrid分页参数)
        /// </summary>
        public int rows
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        private int _rowCount = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int RowCount
        {
            get { return _rowCount; }
            set { _rowCount = value; }
        }

        ///// <summary>
        ///// 总页数
        ///// </summary>
        //private int _pageCount = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                double pageSizeFloat = this.RowCount / (this.PageSize * 1.0);
                int pageSizeInt = (int)pageSizeFloat;
                return pageSizeFloat > pageSizeInt * 1.0 ? pageSizeInt + 1 : pageSizeInt;
            }
            //set { _pageCount = value; }
        }

        private string _sortIndex = string.Empty;
        /// <summary>
        /// 排序索引
        /// </summary>
        public string SortIndex
        {
            get { return _sortIndex; }
            set { _sortIndex = value; }
        }
        /// <summary>
        /// 排序索引(用于接收jqgrid分页参数)
        /// </summary>
        public string sidx
        {
            get { return _sortIndex; }
            set { _sortIndex = value; }
        }

        private string _sortDirction = "asc";
        /// <summary>
        /// 排序方向
        /// </summary>
        public string SortDirction
        {
            get { return _sortDirction; }
            set { _sortDirction = value; }
        }
        /// <summary>
        /// 排序方向(用于接收jqgrid分页参数)
        /// </summary>
        public string sord
        {
            get { return _sortDirction; }
            set { _sortDirction = value; }
        }

        private bool _isQuery = false;
        /// <summary>
        /// 是否是查询(用以翻页或排序时统计总记录数)
        /// </summary>
        public bool IsQuery
        {
            get { return _isQuery; }
            set { _isQuery = value; }
        }
    }
}
