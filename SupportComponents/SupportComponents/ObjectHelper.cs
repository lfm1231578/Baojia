using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Collections;

namespace HS.SupportComponents
{
    public class ObjectHelper
    {
        /// <summary>
        /// 将对象转为二进制
        /// </summary>
        /// <param name="tObject">必须可序列化的对象</param>
        /// <returns></returns>
        public static byte[] ConvertObjectToBytes<T>(T tObject)
        {
            if (tObject == null)
                return new byte[] { };

            byte[] objBytes = null;

            using (System.IO.MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(ms, tObject);
                objBytes = ms.ToArray();
            }

            return objBytes;
        }

        /// <summary>
        /// 将二进制转为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tBytes"></param>
        /// <returns></returns>
        public static T ConvertObjectFromBytes<T>(byte[] tBytes)
        {
            T t = default(T);

            using (System.IO.MemoryStream ms = new MemoryStream(tBytes))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                t = (T)binaryFormatter.Deserialize(ms);
            }

            return t;
        }

        /// <summary> 
        /// BaseObject类是一个用来继承的抽象类。 
        /// 每一个由此类继承而来的类将自动支持克隆方法。 
        /// 该类实现了Icloneable接口，并且每个从该对象继承而来的对象都将同样地 
        /// 支持Icloneable接口。 
        /// 克隆对象，并返回一个已克隆对象的引用    
        /// <returns>引用新的克隆对象</returns>         
        /// </summary>  
        public static object CloneAll(object source)
        {
            #region
            //首先我们建立指定类型的一个实例 
            object newObject = Activator.CreateInstance(source.GetType()); //我们取得新的类型实例的字段数组。 
            FieldInfo[] fields = newObject.GetType().GetFields();
            int i = 0;
            foreach (FieldInfo fi in source.GetType().GetFields())
            {
                //我们判断字段是否支持ICloneable接口。 
                Type ICloneType = fi.FieldType.GetInterface("ICloneable", true);
                if (ICloneType != null)
                {
                    //取得对象的Icloneable接口。 
                    ICloneable IClone = (ICloneable)fi.GetValue(source); //我们使用克隆方法给字段设定新值。 
                    fields[i].SetValue(newObject, IClone.Clone());
                }
                else
                {
                    // 如果该字段部支持Icloneable接口，直接设置即可。 
                    fields[i].SetValue(newObject, fi.GetValue(source));
                }
                //现在我们检查该对象是否支持IEnumerable接口，如果支持， 
                //我们还需要枚举其所有项并检查他们是否支持IList 或 IDictionary 接口。 
                Type IEnumerableType = fi.FieldType.GetInterface("IEnumerable", true);
                if (IEnumerableType != null)
                {
                    //取得该字段的IEnumerable接口 
                    IEnumerable IEnum = (IEnumerable)fi.GetValue(source);
                    Type IListType = fields[i].FieldType.GetInterface("IList", true);
                    Type IDicType = fields[i].FieldType.GetInterface("IDictionary", true);
                    int j = 0;
                    if (IListType != null)
                    {
                        //取得IList接口。 
                        IList list = (IList)fields[i].GetValue(newObject);
                        foreach (object obj in IEnum)
                        {
                            //查看当前项是否支持支持ICloneable 接口。 
                            ICloneType = obj.GetType().GetInterface("ICloneable", true);
                            if (ICloneType != null)
                            {
                                //如果支持ICloneable 接口，     
                                //我们用它李设置列表中的对象的克隆 
                                ICloneable clone = (ICloneable)obj;
                                list[j] = clone.Clone();
                            }
                            //注意：如果列表中的项不支持ICloneable接口，那么     
                            //在克隆列表的项将与原列表对应项相同      
                            //（只要该类型是引用类型） 
                            j++;
                        }
                    }
                    else if (IDicType != null)
                    {
                        //取得IDictionary 接口 
                        IDictionary dic = (IDictionary)fields[i].GetValue(newObject);
                        j = 0;
                        foreach (DictionaryEntry de in IEnum)
                        {
                            //查看当前项是否支持支持ICloneable 接口。 
                            ICloneType = de.Value.GetType().GetInterface("ICloneable", true);
                            if (ICloneType != null)
                            {
                                ICloneable clone = (ICloneable)de.Value;
                                dic[de.Key] = clone.Clone();
                            }
                            j++;
                        }
                    }
                }
                i++;
            }
            #endregion
            return newObject;
        }

        public static ResultEntity Convert<SOURCE, TARGET>(ParamListForPaging<SOURCE> sourceT, ParamListForPaging<TARGET> targetT)
        {
            ResultEntity rst = new ResultEntity();

            return rst;
        }

        /// <summary>
        /// 将sourceT克隆到targetT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceT"></param>
        /// <param name="targetT"></param>
        /// <returns></returns>
        public static ResultEntity Convert<SOURCE, TARGET>(SOURCE sourceT, TARGET targetT)
        {
            return Convert<SOURCE, TARGET>(sourceT, ref targetT);
        }

        /// <summary>
        /// 将sourceT克隆到targetT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceT"></param>
        /// <param name="targetT"></param>
        /// <returns></returns>
        public static ResultEntity Convert<SOURCE, TARGET>(SOURCE sourceT, ref TARGET targetT)
        {
            return Convert<SOURCE, TARGET>(sourceT, ref targetT, null, false);
        }

        /// <summary>
        /// 将sourceT克隆到targetT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceT"></param>
        /// <param name="targetT"></param>
        /// <returns></returns>
        public static ResultEntity Convert<SOURCE, TARGET>(SOURCE sourceT, ref TARGET targetT, bool isIgnoreCase)
        {
            return Convert<SOURCE, TARGET>(sourceT, ref targetT, null, isIgnoreCase);
        }

        /// <summary>
        /// 将sourceT克隆到targetT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceT"></param>
        /// <param name="targetT"></param>
        /// <returns></returns>
        public static ResultEntity Convert<SOURCE, TARGET>(SOURCE sourceT, TARGET targetT, IList<System.Type> targetTypeFilterList)
        {
            return Convert<SOURCE, TARGET>(sourceT, ref targetT, targetTypeFilterList, false);
        }

        /// <summary>
        /// 将sourceT克隆到targetT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceT"></param>
        /// <param name="targetT"></param>
        /// <returns></returns>
        public static ResultEntity Convert<SOURCE, TARGET>(SOURCE sourceT, TARGET targetT, IList<System.Type> targetTypeFilterList, bool isIgnoreCase)
        {
            return Convert<SOURCE, TARGET>(sourceT, ref targetT, targetTypeFilterList, isIgnoreCase);
        }

        /// <summary>
        /// 将sourceT克隆到targetT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceT"></param>
        /// <param name="targetT"></param>
        /// <returns></returns>
        public static ResultEntity ConvertParamListForPaging<CHILDCLASS, PARENTCLASS>(ParamListForPaging<CHILDCLASS> sourceT, ParamListForPaging<PARENTCLASS> targetT)
        {
            return ConvertParamListForPaging<CHILDCLASS, PARENTCLASS>(sourceT, targetT, false);
        }

        /// <summary>
        /// 将sourceT克隆到targetT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceT"></param>
        /// <param name="targetT"></param>
        /// <returns></returns>
        public static ResultEntity ConvertParamListForPaging<CHILDCLASS, PARENTCLASS>(ParamListForPaging<CHILDCLASS> sourceT, ParamListForPaging<PARENTCLASS> targetT, bool isIgnoreCase)
        {
            IList<System.Type> targetTypeFilterList = new List<Type>();
            targetTypeFilterList.Add(typeof(PARENTCLASS));
            ResultEntity rst = Convert<ParamListForPaging<CHILDCLASS>, ParamListForPaging<PARENTCLASS>>(sourceT, ref targetT, targetTypeFilterList, isIgnoreCase);
            try
            {
                targetT.ParamEntity = (PARENTCLASS)((object)sourceT.ParamEntity);// System.Convert.ChangeType(sourceT.ParamEntity, typeof(CHILDCLASS));
            }
            catch (Exception ex)
            {
                rst.IsSuccess = false;
                rst.Decription += ex.Message;
            }
            return rst;
        }

        /// <summary>
        /// 将sourceT克隆到targetT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceT"></param>
        /// <param name="targetT"></param>
        /// <returns></returns>
        public static ResultEntity Convert<SOURCE, TARGET>(SOURCE sourceT, ref TARGET targetT, IList<System.Type> targetTypeFilterList)
        {
            return Convert<SOURCE, TARGET>(sourceT, ref targetT, targetTypeFilterList, false);
        }

        /// <summary>
        /// 将sourceT克隆到targetT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceT"></param>
        /// <param name="targetT"></param>
        /// <returns></returns>
        public static ResultEntity Convert<SOURCE, TARGET>(SOURCE sourceT, ref TARGET targetT, IList<System.Type> targetTypeFilterList, bool isIgnoreCase)
        {
            ResultEntity rst = new ResultEntity();

            if (sourceT == null)
            {//源对象为null
                rst.IsSuccess = false;
                rst.Decription = "传入的源对象为null";
                return rst;
            }
            if (targetT == null)
            {//目标对象为null
                rst.IsSuccess = false;
                rst.Decription = "传入的目标对象为null";
                return rst;
            }

            System.Reflection.PropertyInfo[] propertiesSource = typeof(SOURCE).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            System.Reflection.PropertyInfo[] propertiesTarget = typeof(TARGET).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            foreach (System.Reflection.PropertyInfo itemSource in propertiesSource)
            {
                IEnumerable<System.Reflection.PropertyInfo> pis = isIgnoreCase
                                                                      ? propertiesTarget.Where(
                                                                          pt =>
                                                                          pt.Name.ToLower() == itemSource.Name.ToLower())
                                                                      : propertiesTarget.Where(
                                                                          pt => pt.Name == itemSource.Name);
                if (pis.Count() == 0)
                {//目标对象找不到属性
                    rst.Decription += "目标对象找不到属性：" + itemSource.Name;
                }

                foreach (System.Reflection.PropertyInfo itemTarget in pis)
                {
                    if (targetTypeFilterList != null && targetTypeFilterList.Count >= 1)
                    {
                        if (targetTypeFilterList.IndexOf(itemTarget.PropertyType) >= 0)
                            continue;
                    }

                    object value = itemSource.GetValue(sourceT, null);
                    try
                    {
                        //目标对象属性赋值
                        itemTarget.SetValue(targetT, ObjectValueParser.ChangeType(value, itemTarget.PropertyType), null);
                    }
                    catch (Exception)
                    {
                        rst.Decription += "目标对象属性：" + itemSource.Name + "赋值失败";
                    }
                    break;
                }
            }

            return rst;
        }


        #region 反射获取类成员

        /// <summary>获取实体的属性（包括公共的成员变量）值。
        /// 格式：每个属性值以空格分开
        /// </summary>
        public static string GetEntityListProperties(object[] entityList, params Type[] typesCanBeGetted)// JJY added on 2009.8.24
        {
            if (entityList == null)
                return "";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < entityList.Length; i++)
            {
                string str = GetEntityProperties("    ", entityList[i], typesCanBeGetted);
                sb.AppendLine();
                sb.Append(string.Format("        List[{0}]数据：", i));
                sb.Append(str);
            }
            return sb.ToString();
        }

        /// <summary>获取列表中所有实体的属性（包括公共的成员变量）值。
        /// 格式：每个实体的属性值都在新的一行中，属性值以空格分开
        /// </summary>
        public static string GetEntityProperties(object entity, params Type[] typesCanBeGetted)// JJY added on 2009.9.1
        {
            return GetEntityProperties("", entity, typesCanBeGetted);
        }

        private static string GetEntityProperties(string prevSpace, object entity, params Type[] typesCanBeGetted)// JJY added on 2009.8.24
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                if (entity == null)
                    return "";

                Type type = entity.GetType();
                List<MemberInfo> infos = new List<MemberInfo>();
                FieldInfo[] fiArr = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
                PropertyInfo[] piArr = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                if (fiArr != null)
                    infos.AddRange(fiArr);
                if (piArr != null)
                    infos.AddRange(piArr);

                // 设置返回的数据
                if (infos != null && infos.Count > 0)
                {
                    foreach (MemberInfo info in infos)
                    {
                        try
                        {
                            bool tagType = false;
                            object val = type.InvokeMember(info.Name, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.GetField | BindingFlags.Instance, null, entity, null);

                            // 若是目标要打印的实体类型，则递归打印该实体
                            if (typesCanBeGetted != null)
                            {
                                foreach (Type t in typesCanBeGetted)
                                {
                                    if (info.ToString().StartsWith(t.FullName + " "))
                                    {
                                        tagType = true;
                                        sb.AppendLine();
                                        sb.AppendLine(string.Format("{0}{0}{0}{1}实体数据：    {2}", prevSpace, t.Name, GetEntityProperties(prevSpace, val, typesCanBeGetted)));
                                        break;
                                    }
                                }
                            }

                            // 若不是目标要打印的实体类型，则直接打印属性值
                            if (!tagType)
                                sb.AppendFormat(" {0}=\"{1}\" ", info.Name, ConvertToString(val));
                        }
                        catch { }
                    }
                }
            }
            catch { }

            return sb.ToString();
        }

        private static string BuildEntity<T>(T entity) where T : class
        {
            StringBuilder sb = new StringBuilder("\r\n");

            try
            {
                if (entity != null)
                {
                    PropertyInfo[] infos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance);
                    if (infos != null && infos.Length > 0)
                    {
                        foreach (PropertyInfo info in infos)
                        {
                            try
                            {
                                object val = typeof(T).InvokeMember(info.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance, null, entity, null);
                                sb.AppendFormat(" {0}=\"{1}\" ", info.Name, ConvertToString(val));
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { }

            return sb.ToString();
        }

        public static string BuildEntity<T>(IList<T> list) where T : class
        {
            StringBuilder sb = new StringBuilder("\r\n");
            try
            {
                if (list != null && list.Count > 0)
                {
                    PropertyInfo[] infos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance);
                    if (infos != null && infos.Length > 0)
                    {
                        foreach (T obj in list)
                        {
                            foreach (PropertyInfo info in infos)
                            {
                                try
                                {
                                    object val = typeof(T).InvokeMember(info.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance, null, obj, null);
                                    sb.AppendFormat(" {0}=\"{1}\"", info.Name, ConvertToString(val));
                                }
                                catch { }
                            }
                            sb.Append(" \r\n");
                        }
                    }
                }
            }
            catch { }

            return sb.ToString();
        }

        public static string ConvertToString(object val)
        {
            if (val is DateTime)
            {
                return ((DateTime)val).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (val is Enum)
            {
                return Enum.Format(val.GetType(), val, "d");
            }
            else if (val is bool)
            {
                if ((bool)val)
                    return "1";
                else
                    return "0";
            }
            else if (val is byte[])
            {
                return val == null ? "" : System.Convert.ToBase64String(val as byte[]);
            }
            else
            {
                return System.Convert.ToString(val);
            }
        }
        #endregion

        public static object memoryStream { get; set; }
    }
}
