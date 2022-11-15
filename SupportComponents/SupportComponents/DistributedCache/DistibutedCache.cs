using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Messaging;
using System.Configuration;
using System.Xml;

namespace HS.SupportComponents.DistributedCache
{
    public class DistibutedCache
    {
        private string msname;
        private MessageQueue Queen;
        private string fullname;
        //protected BinaryMessageFormatter formatter;
        private TimeSpan interval = new TimeSpan(0, 0, 10);

        public DistibutedCache(string machine,string msname)
        {
            this.msname = msname;
            Machine = machine;
        }


        /// <summary>
        /// 消息队列机器
        /// </summary>
        public string Machine
        {
            set
            {
                string temp = value;
                StringBuilder name = new StringBuilder(128);

                if (temp == ".")
                {
                    // 如果队列机是指向本机
                    name.Append(".\\Private$\\");
                }
                else
                {
                    // 如果队列机指向其它机器
                    name.Append("FormatName:DIRECT=");
                    if (temp.IndexOf('.') > 0)
                        name.Append("TCP:");
                    else
                        name.Append("OS:");
                    name.Append(temp);
                    name.Append("\\Private$\\");
                }

                this.fullname = name.ToString() + this.msname;
            }
        }



        /// <summary>
        /// 打开队列器
        /// </summary>
        public void Open()
        {
            if (this.Queen == null)
            {
                this.Queen = new MessageQueue(this.fullname);
                this.Queen.Formatter = new ActiveXMessageFormatter();
            }

        }

        /// <summary>
        /// 关闭队列
        /// </summary>
        public void Close()
        {
            if (null != this.Queen)
            {
                this.Queen.Close();
                this.Queen = null;
            }
        }


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        public void Send(Message message)
        {
            if (null == this.Queen)
                this.Open();
            Queen.Send(message);
        }


        /// <summary>
        /// 读取消息
        /// </summary>
        /// <returns></returns>
        public Message Read()
        {
            Message message = null;
            if (null == this.Queen)
                this.Open();
            //if (formatter == null)
            //    formatter = new BinaryMessageFormatter();

            message = ReadMessage();
            return message;
        }



        /// <summary>
        /// 先读优先级高的队列
        /// </summary>
        /// <returns></returns>
        private Message ReadMessage()
        {
            Message message = null;
            try
            {
                message = this.Queen.Receive(interval);  
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode != MessageQueueErrorCode.IOTimeout)
                    throw ex;
            }
            return message;
        }


    }
}
