using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace HS.SupportComponents
{
    public static class GlobalContextHelper<T>
    {
       private static LocalDataStoreSlot localSlot = Thread.AllocateDataSlot();
       
        /// <summary>
        /// 设置上下文信息
        /// </summary>
        /// <param name="Client"></param>
        public static void SetContext(T t)
        {
            Thread.SetData(localSlot, t);
        }

        /// <summary>
        /// 获取上下级信息
        /// </summary>
        /// <returns></returns>
        public static T GetContext()
        {
            return (T)Thread.GetData(localSlot);
        }

        /// <summary>
        /// 错误信息的上下文信息
        /// </summary>
        /// <returns></returns>
        public static int GetErrorContext()
        {
            if (Thread.GetData(localSlot) == null)
            {
                return 0;
            }
            return (int)Thread.GetData(localSlot);
        }
    }
}
