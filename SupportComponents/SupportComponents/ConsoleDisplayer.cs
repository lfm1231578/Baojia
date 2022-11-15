using System;
using System.Runtime.InteropServices;

namespace HS.SupportComponents
{
    /// <summary>
    /// ����̨��ʾ��
    /// </summary>
    internal class ConsoleDisplayer
    {
        #region Delegates

        public delegate bool HandlerRoutine(int dwCtrlType);

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        private static bool HASRUNCONSOLE;

        /// <summary>
        /// ����־
        /// </summary>
        private static readonly object LOCKFLAG = new object();

        /// <summary>
        /// ���п���̨
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
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DllImport("Kernel32.dll")]
        private static extern bool AllocConsole();

        /// <summary>
        /// �ͷŴ��ڣ����ر�
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "FreeConsole")]
        private static extern bool FreeConsole();

        /// <summary>
        /// �ҳ����еĴ���
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// ȡ���������еĲ˵�
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="bRevert"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        /// <summary>
        /// �ҵ���ť
        /// </summary>
        /// <param name="hMenu"></param>
        /// <param name="uPosition"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        private static extern IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        /// <summary>
        /// �ı���������
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