using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Messaging;
using HS.SupportComponents;

namespace HS.SupportComponents.DistributedCache
{
    public delegate void ExecuteReaderDelegate(Message message);

    public class MessageEngine
    {
        private bool StopProcess = false;

        private string machine = string.Empty;
        private string name = string.Empty;
        private int totle = 10;
        private int mqerrorsleep = 30;

        public event ExecuteReaderDelegate ReaderEventHandler;
        public MessageEngine(string machine, string name)
        {
            this.machine = machine;
            this.name = name;
        }

        public MessageEngine(string machine, string name, int totle)
            : this(machine, name)
        {
            this.totle = totle;
        }

        public MessageEngine(string machine, string name, int totle, int mqerrorsleep)
            : this(machine, name)
        {
            this.totle = totle;
            this.mqerrorsleep = mqerrorsleep;
        }

        /// <summary>
        /// 启动引擎
        /// </summary>
        public void Run()
        {
            StopProcess = false;
            for (int i = 0; i < totle; i++)
            {
                Thread threadReader = new Thread(Execute);
                threadReader.Start();
            }
        }

        /// <summary>
        /// 停止引擎
        /// </summary>
        public void Stop()
        {
            StopProcess = true;
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        private void Execute()
        {
            DistibutedCache distributedCache = new DistibutedCache(this.machine, this.name);
            while (!StopProcess)
            {
                try
                {
                    Message message = distributedCache.Read();
                    if (message != null && ReaderEventHandler != null)
                    {
                        ReaderEventHandler(message);
                    }
                }
                catch (MessageQueueException ex)
                {
                    LogWriter.WriteError(ex, string.Format("MessageEngine定时接收数据错误[MessageQueueException],服务器:{0},消息队列:{1}", this.machine, this.name));
                    distributedCache.Close();
                    Thread.Sleep(mqerrorsleep * 1000);
                }
                catch (Exception ex)
                {
                    LogWriter.WriteError(ex, string.Format("MessageEngine定时接收数据错误,服务器:{0},消息队列:{1}", this.machine, this.name));
                    distributedCache.Close();
                    Thread.Sleep(mqerrorsleep * 1000);
                }
            }
        }
    }
}
