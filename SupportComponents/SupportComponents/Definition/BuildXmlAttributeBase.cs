using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents.Definition
{
    /// <summary>
    /// 定义生成XML时属性名称
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.All, AllowMultiple = false)]
    public class BuildXmlAttributeBase : Attribute
    {
        public string XmlPropertyName
        { get; set; }
    }
}
