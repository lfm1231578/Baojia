using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents
{
    public class ListProcessHelper
    {
        /// <summary>
        /// 根据指定的页码和大小进行List子集截取
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sourceList">数据源</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns>返回结果如果大于分页大小（理论上等于分页大小+1），则表示有下一页，否则表示没有下一页</returns>
        public static IList<T> GetSubList<T>(IList<T> sourceList, int pageIndex, int pageSize)
        {
            int minSize = (pageIndex - 1) * pageSize + 1;    //源数据有对应子集的最小条数

            if (sourceList == null || minSize > sourceList.Count) //如果源数据为空，或者源数据集总数小于当前分页所需最小条数
            {
                return null;   //返回空
            }
            int startIndex = (pageIndex - 1) * pageSize;                  //分页起始指标
            int subSize = sourceList.Count - (pageIndex - 1) * pageSize;  //从分页开始到结束的剩余数
            subSize = subSize > pageSize ? pageSize : subSize;            //如果剩余数大于分页大小，则取分页大小，否则取剩余数


            return ((List<T>)sourceList).GetRange(startIndex, subSize);               //返回分页
        }


    }
}
