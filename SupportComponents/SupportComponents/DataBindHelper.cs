using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using HS.SupportComponents;

namespace HS.SupportComponents
{
    public class DataBindHelper
    {
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T CreateEntity<T>(IDataReader dr) where T : new()
        {
            T info = new T();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (!dr[i].Equals(DBNull.Value))
                {
                    if (typeof(T).GetProperty(dr.GetName(i)) != null)
                    {
                        if (typeof(T).GetProperty(dr.GetName(i)).GetValue(info, null) is bool)
                        {
                            typeof(T).GetProperty(dr.GetName(i)).SetValue(info, Convert.ToBoolean(dr[i]), null);
                        }
                        else if (typeof(T).GetProperty(dr.GetName(i)).GetValue(info, null) is float)
                        {
                            typeof(T).GetProperty(dr.GetName(i)).SetValue(info, float.Parse(dr[i].ToString()), null);
                        }
                        else if (typeof(T).GetProperty(dr.GetName(i)).GetValue(info, null) is char)
                        {
                            typeof(T).GetProperty(dr.GetName(i)).SetValue(info, Convert.ToChar(dr[i]), null);
                        }
                        else if (typeof(T).GetProperty(dr.GetName(i)).GetValue(info, null) is long)
                        {
                            typeof(T).GetProperty(dr.GetName(i)).SetValue(info, Convert.ToInt64(dr[i].ToString()), null);
                        }
                        else
                        {
                            typeof(T).GetProperty(dr.GetName(i)).SetValue(info, dr[i], null);
                        }
                    }
                }
            }
            return info;
        }

        /// <summary>
        /// 通过IDataReader，获取对应实体列表
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="iDataReader"></param>
        /// <returns></returns>
        public static List<TEntity> GetList<TEntity>(IDataReader iDataReader) where TEntity : new()
        {
            List<TEntity> list = new List<TEntity>();
            while (iDataReader.Read())
            {
                TEntity local = (default(TEntity) == null) ? Activator.CreateInstance<TEntity>() : default(TEntity);
                foreach (PropertyInfo info in local.GetType().GetProperties())
                {
                    if (FieldExists(iDataReader, info.Name))
                    {
                        if (!(iDataReader[info.Name] is DBNull))
                        {
                            info.SetValue(local, iDataReader[info.Name], null);
                        }
                    }
                }
                list.Add(local);
            }
            return list;
        }

        /// <summary>
        /// 通过IDataReader，获取对应实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="iDataReader"></param>
        /// <returns></returns>
        public static TEntity Load<TEntity>(IDataReader iDataReader) where TEntity : new()
        {
            TEntity local = default(TEntity);
            while (iDataReader.Read())
            {
                local = (default(TEntity) == null) ? Activator.CreateInstance<TEntity>() : default(TEntity);
                foreach (PropertyInfo info in local.GetType().GetProperties())
                {
                    if (FieldExists(iDataReader, info.Name))
                    {
                        if (!(iDataReader[info.Name] is DBNull))
                        {
                            info.SetValue(local, iDataReader[info.Name], null);
                        }
                    }
                }
            }
            return local;
        }

        /// <summary>
        /// 检查字段名是否存在
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public static bool FieldExists(IDataReader reader, string columnName)
        {
            bool result = true;
            try
            {
                object o = reader[columnName];
            }
            catch (System.IndexOutOfRangeException) { result = false; }
            return result;
        }
    }
}
