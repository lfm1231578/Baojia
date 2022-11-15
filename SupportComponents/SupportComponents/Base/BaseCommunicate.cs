using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Collections.Specialized;
using System.Reflection;
using System.ComponentModel;

namespace HS.SupportComponents.Base
{
    class Communicator<T> : ClientBase<T> where T : class
    {
        public T GetChannel()
        {
            return this.Channel;
        }
    }

    public class BaseCommunicate
    {
        public T GetCommunicateService<T>() where T:class
        {
            Communicator<T> communicator = new Communicator<T>();
            return communicator.GetChannel();
        }

        public T GetCommunicateService<T>(string endpointConfigurationName)
        {
            ChannelFactory<T> channelFactory = new ChannelFactory<T>(endpointConfigurationName);
            return channelFactory.CreateChannel();
        }

        public NameValueCollection ConvertToCommSearchCondition1<T>(T condition)
        {
            NameValueCollection ret = new NameValueCollection();
            Type entityType = typeof(T);
            PropertyInfo[] entityPropertypes = entityType.GetProperties();//获取entity的所有公开 属性
            foreach (PropertyInfo entityPropertype in entityPropertypes)
            {
                object[] propertyAttributes = entityPropertype.GetCustomAttributes(false);//可以取出特性
                foreach (object attribute in propertyAttributes)
                {
                    DescriptionAttribute descriptionAttr = attribute as DescriptionAttribute;
                    if (descriptionAttr != null)
                    {
                        object propertypeValue = entityPropertype.GetValue(condition, null);
                        if(propertypeValue == null)
                        {
                            break;
                        }

                        if (propertypeValue is int)
                        {
                            if ((int)propertypeValue != -9999)
                            {
                                ret[descriptionAttr.Description] = propertypeValue.ToString();
                            }
                        }

                        if (propertypeValue is string)
                        {
                            if (!string.IsNullOrEmpty(propertypeValue.ToString()))
                            {
                                ret[descriptionAttr.Description] = propertypeValue.ToString();
                            }
                        }

                        if (propertypeValue is DateTime)
                        {
                            if ((DateTime)propertypeValue != DateTime.MinValue)
                            {
                                ret[descriptionAttr.Description] = ((DateTime)propertypeValue).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                    }
                }
            }
            return ret;
        }

        public Dictionary<string, string> ConvertToCommSearchCondition<T>(T condition)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            Type entityType = typeof(T);
            PropertyInfo[] entityPropertypes = entityType.GetProperties();//获取entity的所有公开 属性
            foreach (PropertyInfo entityPropertype in entityPropertypes)
            {
                object[] propertyAttributes = entityPropertype.GetCustomAttributes(false);//可以取出特性
                foreach (object attribute in propertyAttributes)
                {
                    DescriptionAttribute descriptionAttr = attribute as DescriptionAttribute;
                    if (descriptionAttr != null)
                    {
                        object propertypeValue = entityPropertype.GetValue(condition, null);
                        if (propertypeValue == null)
                        {
                            break;
                        }

                        if (propertypeValue is int)
                        {
                            if ((int)propertypeValue != -9999)
                            {
                                ret[descriptionAttr.Description] = propertypeValue.ToString();
                            }
                        }

                        if (propertypeValue is string)
                        {
                            if (!string.IsNullOrEmpty(propertypeValue.ToString()))
                            {
                                ret[descriptionAttr.Description] = propertypeValue.ToString();
                            }
                        }

                        if (propertypeValue is DateTime)
                        {
                            if ((DateTime)propertypeValue != DateTime.MinValue)
                            {
                                ret[descriptionAttr.Description] = ((DateTime)propertypeValue).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                    }
                }
            }
            return ret;
        }
    }
}
