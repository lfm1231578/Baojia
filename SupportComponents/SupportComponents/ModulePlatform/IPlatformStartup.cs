using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents.ModulePlatform
{
    public interface IPlatformStartup
    {
        /// <summary>
        /// 子模块启动入口
        /// </summary>
        void Start();

        /// <summary>
        /// 子模块启动出口
        /// </summary>
        void Stop();
    }
}
