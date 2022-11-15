using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HS.SupportComponents
{
    /// <summary>
    /// 处理对象属性的辅助类。
    /// </summary>
    public sealed class PropertyHelper
    {
        private static BindingFlags m_getValueBinding = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance;
        private static BindingFlags m_setValueBinding = BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance;


        /// <summary>
        /// 获取对象指定属性的标注属性。
        /// </summary>
        /// <param name="name">属性名称。</param>
        /// <param name="entity">对象。</param>
        /// <returns></returns>
        public static Attribute GetPropertyAttribute<T>(string name, T entity) where T : class
        {
            if (string.IsNullOrEmpty(name) || entity == null)
                return null;

            PropertyInfo info = entity.GetType().GetProperty(name);
            if (info != null)
            {
                object[] attrs = info.GetCustomAttributes(true);
                if (attrs != null || attrs.Length > 0)
                {
                    foreach (object attr in attrs)
                    {
                        if (attr != null && attr is Attribute)
                            return (Attribute)attr;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 获取对象指定属性的标注属性。
        /// </summary>
        /// <param name="name">属性名称。</param>
        /// <param name="entity">对象。</param>
        /// <param name="attrType">标注属性的类型。</param>
        /// <returns></returns>
        public static Attribute GetPropertyAttribute<T>(string name, T entity, Type attrType) where T : class
        {
            if (string.IsNullOrEmpty(name) || entity == null || attrType == null)
                return null;

            PropertyInfo info = entity.GetType().GetProperty(name);
            if (info != null && info.IsDefined(attrType, true))
            {
                object[] attrs = info.GetCustomAttributes(attrType, true);
                if (attrs != null || attrs.Length > 0)
                {
                    foreach (object attr in attrs)
                    {
                        if (attr != null && attr is Attribute && attr.GetType() == attrType)
                            return (Attribute)attr;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 获取对象的属性信息。
        /// </summary>
        /// <param name="name">属性名称。</param>
        /// <param name="entity">对象。</param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo<T>(string name, T entity) where T : class
        {
            if (name == null) return null;
            name = name.Replace(" ", "");
            if (name.StartsWith(@"this.")) name = name.Substring(@"this.".Length);
            if (name == string.Empty) return null;
            if (entity == null) return null;

            string subName = string.Empty;
            int index = name.IndexOf('.');
            if (index > 0 && index < name.Length - 1)
            {
                subName = name.Substring(index + 1);
                name = name.Substring(0, index);
            }

            try
            {
                if (subName == string.Empty)
                {
                    return entity.GetType().GetProperty(name);
                }
                else
                {
                    object value = GetPropertyValue(name, entity);
                    return GetPropertyInfo(subName, value);
                }
            }
            catch { return null; }
        }

        /// <summary>
        /// 获取指定属性的类型。
        /// </summary>
        /// <param name="name">属性名称。</param>
        /// <param name="entity">对象实例。</param>
        /// <returns>属性的类型。</returns>
        public static Type GetPropertyType<T>(string name, T entity) where T : class
        {
            if (name == null) return null;
            name = name.Replace(" ", "");
            if (name.StartsWith(@"this.")) name = name.Substring(@"this.".Length);
            if (name == string.Empty) return null;
            if (entity == null) return null;

            string subName = string.Empty;
            int index = name.IndexOf('.');
            if (index > 0 && index < name.Length - 1)
            {
                subName = name.Substring(index + 1);
                name = name.Substring(0, index);
            }

            try
            {
                if (subName == string.Empty)
                {
                    return entity.GetType().GetProperty(name).PropertyType;
                }
                else
                {
                    object value = GetPropertyValue(name, entity);
                    return GetPropertyType(subName, value);
                }
            }
            catch { return null; }
        }

        /// <summary>
        /// 取指定属性的值。
        /// </summary>
        /// <param name="name">属性名称。</param>
        /// <param name="entity">对象实例。</param>
        /// <returns>属性值。</returns>
        public static object GetPropertyValue<T>(string name, T entity)
        {
            if (name != null && name.Trim() != string.Empty && entity != null)
            {
                name = name.Replace(" ", "");

                string subName = string.Empty;
                int index = name.IndexOf('.');
                if (index > 0 && index < name.Length - 1)
                {
                    subName = name.Substring(index + 1);
                    name = name.Substring(0, index);
                }

                try
                {
                    if (name == "this")
                    {
                        if (subName == string.Empty)
                            return entity;
                        else
                            return GetPropertyValue(subName, entity);
                    }
                    else
                    {
                        Type type = entity.GetType();
                        if (type.IsEnum)
                        {
                            switch (name.ToLower())
                            {
                                case "value": return entity;
                                case "name": return Enum.GetName(type, entity);
                                case "description":
                                    string fieldName = Enum.GetName(type, entity);
                                    object[] attrs = type.GetField(fieldName).GetCustomAttributes(typeof(DescriptionAttribute), false);
                                    if (attrs.Length > 0)
                                        return ((DescriptionAttribute)attrs[0]).Description;
                                    else
                                        return entity;
                                default:
                                    return entity;
                            }
                        }
                        else if (type.IsClass)
                        {
                            object propertyValue = type.InvokeMember(name, m_getValueBinding, null, entity, null);
                            if (subName == string.Empty)
                                return propertyValue;
                            else
                                return GetPropertyValue(subName, propertyValue);
                        }
                        else
                            return null;
                    }
                }
                catch { return null; }
            }
            return null;
        }

        /// <summary>
        /// 给指定属性赋值。
        /// </summary>
        /// <param name="name">属性名称。</param>
        /// <param name="obj">数据实体。</param>
        /// <param name="value">值。</param>
        public static void SetPropertyValue(string name, object obj, object value)
        {
            if (name == null) return;
            if (obj == null) return;

            name = name.Trim();
            if (name == string.Empty) return;

            string subName = string.Empty;
            int index = name.IndexOf('.');
            if (index > 0 && index < name.Length - 1)
            {
                subName = name.Substring(index + 1);
                name = name.Substring(0, index);
            }

            try
            {
                if (name == "this")
                {
                    if (subName == string.Empty)
                    {
                        obj = value;
                        return;
                    }
                    else
                    {
                        SetPropertyValue(subName, obj, value);
                        return;
                    }
                }
            }
            catch { return; }

            if (!obj.GetType().IsClass) return;
            PropertyInfo info = obj.GetType().GetProperty(name, m_setValueBinding);
            if (info == null) return;

            if (subName != string.Empty)
            {
                try
                {
                    if (info.PropertyType.IsEnum)
                    {
                        switch (subName.ToLower())
                        {
                            case "value":
                                info.SetValue(obj, value, null);
                                break;
                            case "name":
                                info.SetValue(obj, Enum.Parse(info.PropertyType, value.ToString()), null);
                                break;
                            case "description":
                                foreach (string fieldName in Enum.GetNames(info.PropertyType))
                                {
                                    object[] attrs = info.PropertyType.GetField(fieldName).GetCustomAttributes(typeof(DescriptionAttribute), false);
                                    if (attrs.Length > 0 && ((DescriptionAttribute)attrs[0]).Description == value.ToString())
                                    {
                                        info.SetValue(obj, Enum.Parse(info.PropertyType, fieldName), null);
                                        break;
                                    }
                                }
                                break;
                            default:
                                info.SetValue(obj, value, null);
                                break;
                        }
                    }
                    else
                    {
                        object propertyValue = GetPropertyValue(name, obj);
                        if (propertyValue == null)
                        {
                            //如果属性值为空，尝试创建实例。
                            propertyValue = Activator.CreateInstance(GetPropertyType(name, obj));
                            info.SetValue(obj, propertyValue, null);
                        }
                        SetPropertyValue(subName, propertyValue, value);
                    }
                }
                catch { return; }
            }
            else
            {
                //转换类型
                object newValue = ConvertPropertyValue(info, value);

                //赋值
                try
                {
                    info.SetValue(obj, newValue, null);
                }
                catch { return; }
            }
        }

        /// <summary>
        /// 给指定属性赋值。
        /// </summary>
        /// <param name="info">属性信息对象。</param>
        /// <param name="obj">数据实体。</param>
        /// <param name="value">值。</param>
        public static void SetPropertyValue(PropertyInfo info, object obj, object value)
        {
            if (info == null) return;
            if (obj == null) return;

            //转换类型
            object newValue = ConvertPropertyValue(info, value);

            //赋值
            try
            {
                info.SetValue(obj, newValue, null);
            }
            catch { }
        }

        /// <summary>
        /// 将参数转换为属性的类型。
        /// </summary>
        /// <param name="info">属性信息对象</param>
        /// <param name="value">参数的值。</param>
        /// <returns>转换后的参数值。</returns>
        private static object ConvertPropertyValue(PropertyInfo info, object value)
        {
            try
            {
                object newValue = null;

                //转换类型
                if (info.PropertyType.IsValueType)
                {
                    //空字符串不能转换成值类型，只能创建值类型实例。
                    if (value is string && (string)value == string.Empty)
                    {
                        newValue = Activator.CreateInstance(info.PropertyType);
                    }
                    else
                    {
                        TypeConverter converter = TypeDescriptor.GetConverter(info.PropertyType);
                        if (value != null && converter.CanConvertFrom(value.GetType()))
                        {
                            if (value is string && info.PropertyType.Equals(typeof(decimal)))
                            {
                                string spec = "[" + CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol + " ]";
                                // 转换为精确类型，以去除货币的标识字符
                                string formattedValue = Regex.Replace((string)value, spec, "");

                                //转换百分比
                                spec = CultureInfo.CurrentCulture.NumberFormat.PercentSymbol;
                                if (formattedValue.Trim().EndsWith(spec))
                                {
                                    formattedValue = formattedValue.Replace(spec, "");
                                    decimal dValue = decimal.Parse(formattedValue, NumberStyles.Any) / 100;
                                    formattedValue = dValue.ToString();
                                }
                                newValue = decimal.Parse(formattedValue, NumberStyles.Any);
                                //newValue = converter.ConvertFrom(formattedValue);
                            }
                            else
                            {
                                newValue = converter.ConvertFrom(value);
                            }
                        }
                        else
                        {
                            newValue = value;
                        }
                    }
                }
                else
                {
                    if (info.PropertyType.Equals(typeof(string)) && value == null)
                    {
                        //转换 null 字符串为Empty。
                        newValue = string.Empty;
                    }
                    else
                    {
                        newValue = value;
                    }
                }

                return newValue;
            }
            catch { return null; }
        }
    }
}
