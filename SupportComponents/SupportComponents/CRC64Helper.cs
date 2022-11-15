using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace HS.SupportComponents
{
    /// CRC 效验
    /// 快速检测算法
    /// </summary>
    public class CRC64Helper
    {
        static protected UInt64[] crc32Table;
        static protected CRC64Helper _crc32 = new CRC64Helper();
        private const UInt64 INITIALCRC = 0xFFFFFFFFFFFFFFFFUL;

        private CRC64Helper()
        {
            const UInt64 ulPolynomial = 0x95AC9329AC4BC9B5UL;
            UInt64 dwCrc;
            crc32Table = new UInt64[256];
            int i, j;
            for (i = 0; i < 256; i++)
            {
                dwCrc = (UInt64)i;
                for (j = 8; j > 0; j--)
                {
                    if ((dwCrc & 1) == 1)
                        dwCrc = (dwCrc >> 1) ^ ulPolynomial;
                    else
                        dwCrc >>= 1;
                }
                crc32Table[i] = dwCrc;
            }
        }

        /// <summary>
        /// 字节数组效验
        /// </summary>
        /// <param name="buffer">ref 字节数组</param>
        /// <returns></returns>
        static public UInt64 ByteCRC(ref byte[] buffer)
        {
            UInt64 ulCRC = INITIALCRC;
            uint len;
            len = (uint)buffer.Length;
            for (uint buffptr = 0; buffptr < len; buffptr++)
            {
                uint tabPtr = (uint)ulCRC & 0xFF;
                tabPtr = tabPtr ^ buffer[buffptr];
                ulCRC = ulCRC >> 8;
                ulCRC = ulCRC ^ crc32Table[tabPtr];
            }
            return ulCRC ^ INITIALCRC;
        }

        /// <summary>
        /// 字符串效验
        /// </summary>
        /// <param name="sInputString">字符串</param>
        /// <returns></returns>
        static public UInt64 StringCRC(string sInputString)
        {
            if (sInputString != null)
                sInputString = sInputString.ToUpper().Trim();
            byte[] buffer = Encoding.Default.GetBytes(sInputString);
            return ByteCRC(ref buffer);
        }

        /// <summary>
        /// 文件效验，不适合大容量文件使用。
        /// </summary>
        /// <param name="sInputFilename">输入文件</param>
        /// <returns></returns>
        static public UInt64 FileCRC(string sInputFilename)
        {
            FileStream inFile = new System.IO.FileStream(sInputFilename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] bInput = new byte[inFile.Length];
            inFile.Read(bInput, 0, bInput.Length);
            inFile.Close();

            return ByteCRC(ref bInput);
        }

        /// <summary>
        /// 获取虚拟身份ID
        /// </summary>
        /// <param name="ProtocolType">协议类别</param>
        /// <param name="ServiceType">服务类别</param>
        /// <param name="Account">账号</param>
        /// <returns></returns>
        public static long GetCyberID(int ProtocolType, int ServiceType, string Account)
        {
            return (long)CRC64Helper.StringCRC(ProtocolType.ToString() + ServiceType.ToString() + Account);
        }
    }
}