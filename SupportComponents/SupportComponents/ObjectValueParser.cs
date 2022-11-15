using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HS.SupportComponents
{
    /// <summary>
    /// 对象值解析器
    /// </summary>
    public class ObjectValueParser
    {
        /// <summary>
        /// 设置对象的值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dr"></param>
        public static void SetObjectValueFromDataRow(object obj, System.Data.DataRow dr)
        {
            foreach (System.Reflection.PropertyInfo pInfo in obj.GetType().GetProperties())
            {
                try
                {
                    string pName = pInfo.Name;
                    object pValue = dr[pName];
                    SetObjectValue(obj, pInfo, pValue);
                    //pInfo.SetValue(obj, pValue, null);
                }
                catch (Exception ex)
                {

                }
            }
        }

        /// <summary>
        /// 设置对象的值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dr"></param>
        /// <param name="columnMap"></param>
        public static void SetObjectValueFromDataRow(object obj, System.Data.DataRow dr, IDictionary<string, string> columnMap)
        {
            foreach (System.Reflection.PropertyInfo pInfo in obj.GetType().GetProperties())
            {
                try
                {
                    string pName = pInfo.Name;
                    if (!columnMap.ContainsKey(pName))
                        continue;
                    object pValue = dr[columnMap[pName]];
                    SetObjectValue(obj, pInfo, pValue);
                    // pInfo.SetValue(obj, pValue, null);
                }
                catch (Exception ex)
                {

                }
            }
        }

        /// <summary>
        /// 设置对象的值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dr"></param>
        /// <param name="columnMap"></param>
        public static void SetObjectValueFromDataRowWithColumnValue(object obj, System.Data.DataRow dr, IDictionary<string, string> columnMap)
        {
            foreach (System.Reflection.PropertyInfo pInfo in obj.GetType().GetProperties())
            {
                try
                {
                    string pName = pInfo.Name;
                    IEnumerable<KeyValuePair<string, string>> columnRelationShipList = columnMap.Where(p => p.Value == pName);
                    foreach (KeyValuePair<string, string> columnRelationShip in columnRelationShipList)
                    {
                        if (!(columnRelationShip.Value == (pName)))
                            continue;
                        object pValue = dr[columnRelationShip.Key];
                        SetObjectValue(obj, pInfo, pValue);
                    }
                    // pInfo.SetValue(obj, pValue, null);
                }
                catch (Exception ex)
                {

                }
            }
        }

        private static void SetObjectValue(object obj, System.Reflection.PropertyInfo pInfo, object pValue)
        {
            pValue = ChangeType(pValue, pInfo.PropertyType);
            pInfo.SetValue(obj, pValue, null);
        }

        /// <summary>    
        /// 类型转换
        /// </summary>    
        /// <param name="value">反射的值</param>    
        /// <param name="conversionType">反射的值的类型</param>    
        /// <returns></returns>    
        public static object ChangeType(object value, Type conversionType)
        {
            //INFO 对DBNull类型特殊处理       
            if (Convert.IsDBNull(value))
            {
                //非可空类型的值类型处理          
                if (!(conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))))
                    return Activator.CreateInstance(conversionType);
                else
                    return null;
            }

            //可空类型类型处理     
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }

            try
            {
                if (conversionType.Name == "Guid")
                    return new Guid(value.ToString());
                else
                    return Convert.ChangeType(value, conversionType);
            }
            catch (Exception ex)
            {

            }


            object returnValue = null;
            returnValue = Enum.Parse(conversionType, value.ToString());

            //switch (conversionType.Name.ToLower())
            //{
            //    case "byte":
            //        returnValue = (byte)value;
            //        break;
            //    case "sbyte":
            //        returnValue = (sbyte)value;
            //        break;
            //    case "int16":
            //        returnValue = Enum.Parse(conversionType, value.ToString());
            //        //returnValue = (short)value;
            //        break;
            //    case "ushort":
            //        returnValue = (ushort)value;
            //        break;
            //    case "int32":
            //        returnValue = (int)value;
            //        break;
            //    case "uint":
            //        returnValue = (uint)value;
            //        break;
            //    case "int64":
            //        returnValue = (long)value;
            //        break;
            //    case "ulong":
            //        returnValue = (ulong)value;
            //        break;
            //    default:
            //        break;

            //    //return Convert.ChangeType(value, conversionType.GetType());
            //}

            return returnValue;
        }
    }
}
