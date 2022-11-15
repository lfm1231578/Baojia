using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents.Definition
{
    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = false)]
    public class AlertFormatPropertyAttribute : Attribute
    {
        private string f_name = string.Empty;
        private string v_name = string.Empty;
        private string v_value = string.Empty;
        //private string descriptionProperty = string.Empty;
        /// <summary>
        /// 构造函数。
        /// </summary>
        public AlertFormatPropertyAttribute()
        {
        }

        /// <summary>
        /// 固定字段名称
        /// </summary>
        public string FixedName
        {
            get { return f_name; }
            set { f_name = value; }
        }

        /// <summary>
        /// 可变字段名称
        /// </summary>
        public string VariableName
        {
            get { return v_name; }
            set { v_name = value; }
        }

        /// <summary>
        /// 可变字段值
        /// </summary>
        public string VariableValue
        {
            get { return v_value; }
            set { v_value = value; }
        }

        public string DescriptionProperty
        {
            get;
            set;
        }
    }
}
