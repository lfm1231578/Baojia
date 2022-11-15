using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration;

namespace HS.SupportComponents.Base
{
    /// <summary>
    /// 注入容器
    /// </summary>
    public class XmlRegistration
    {
        #region 单件设计模式

        private class Nested
        {
            internal static readonly XmlRegistration Instance = new XmlRegistration();
            static Nested() { }
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        private XmlRegistration()
        {

        }

        /// <summary>
        /// 获取当前实例。
        /// </summary>
        public static XmlRegistration Current
        {
            get { return Nested.Instance; }
        }

        #endregion

        private IUnityContainer IUnityContainer;
        /// <summary>
        /// 注入初始化
        /// </summary>
        public void RegistrationXml()
        {
            IUnityContainer = new UnityContainer();
            //通过配置文件实现注入配置
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Containers.Default.Configure(IUnityContainer);

        }
        /// <summary>
        /// 获取注入的容器
        /// </summary>
        /// <returns></returns>
        public IUnityContainer GetResovleContainer()
        {
            try
            {
                if (IUnityContainer == null)
                {
                    using (IUnityContainer = new UnityContainer())
                    {
                        UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                        section.Containers.Default.Configure(IUnityContainer);
                    }
                    return this.IUnityContainer;
                }
                else
                {
                    return this.IUnityContainer;
                }
            }
            catch (Exception)
            {
                return null;

            }
        }

    }
}
