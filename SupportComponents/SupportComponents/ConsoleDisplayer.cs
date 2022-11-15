using System;
using System.Runtime.InteropServices;

namespace HS.SupportComponents
{
    /// <summary>
    /// 控制台显示器
    /// </summary>
    internal class ConsoleDisplayer
    {
        #region Delegates

        public delegate bool HandlerRoutine(int dwCtrlType);

        #endregion

        /// <summary>
        /// 跟踪类型
        /// </summary>
        private static bool HASRUNCONSOLE;

        /// <summary>
        /// 锁标志
        /// </summary>
        private static readonly object LOCKFLAG = new object();

        /// <summary>
        /// 运行控制台
        /// </summary>
        public static void RunConsole()
        {
            if (!HASRUNCONSOLE)
            {
                lock (LOCKFLAG)
                {
                    if (!HASRUNCONSOLE)
                    {
                        HASRUNCONSOLE = true;
                        AllocConsole();
                    }
                }
            }
        }

        /// <summary>
        /// 启动窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        private static extern bool AllocConsole();

        /// <summary>
        /// 释放窗口，即关闭
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "FreeConsole")]
        private static extern bool FreeConsole();

        /// <summary>
        /// 找出运行的窗口
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 取出窗口运行的菜单
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="bRevert"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        /// <summary>
        /// 灰掉按钮
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="uPosition"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        private static extern IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        /// <summary>
        /// 改变标题的名称
        /// </summary>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        public static extern bool SetConsoleTitle(string strMessage);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine HandlerRoutine, bool add);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool GenerateConsoleCtrlEvent(int code, int value);
    }
}