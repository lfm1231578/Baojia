using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HS.SupportComponents
{
    /// <summary>
    /// 序列化及反序列化
    /// </summary>
    public class Serialization
    {
        /// <summary>
        /// 序列化及反序列化
        /// </summary>
        public Serialization()
        {

        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">要序列化对象的对象</param>
        /// <param name="filePath">序列化对象后的文件路径</param>
        public static void Serialize<T>(object obj, string filePath)
        {
            SerializeTempObject<T> s = new SerializeTempObject<T>();
            s.TempList = obj as List<T>;

            Serialize(s, filePath);
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">要序列化对象的对象</param>
        /// <param name="filePath">序列化对象后的文件路径</param>
        public static void Serialize(object obj, string filePath)
        {
            Serialize(obj, SerializeType.Xml, filePath);
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">要序列化对象的对象</param>
        /// <param name="filePath">序列化对象的文件路径</param>
        /// <param name="serializeType">序列化对象类型</param>
        public static void Serialize(object obj, SerializeType serializeType, string filePath)
        {
            System.Type type = obj.GetType();
            switch (serializeType)
            {
                case SerializeType.Binary:
                    {
                        using (System.IO.FileStream fs = new System.IO.FileStream(filePath, FileMode.Create))
                        {
                            BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                            bf.Serialize(fs, obj);

                        }
                        break;
                    }
                case SerializeType.Xml:
                    {
                        using (System.IO.FileStream fs = new System.IO.FileStream(filePath, FileMode.Create))
                        {
                            XmlSerializer xs = new XmlSerializer(type);
                            XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
                            xmlns.Add(string.Empty, string.Empty);
                            xs.Serialize(fs, obj, xmlns);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            //return filePath;
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="obj">要反序列化对象的对象</param>
        /// <param name="filePath">反序列化对象的文件路径</param>
        public static void Deserialize<T>(ref object obj, string filePath)
        {
            Object s = new SerializeTempObject<T>();

            Deserialize(ref s, filePath);

            SerializeTempObject<T> rst = s as SerializeTempObject<T>
                ;
            obj = rst.TempList;
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="obj">要反序列化对象的对象</param>
        /// <param name="filePath">反序列化对象的文件路径</param>
        public static void Deserialize(ref object obj, string filePath)
        {
            Deserialize(ref obj, SerializeType.Xml, filePath);
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="obj">要反序列化对象的对象</param>
        /// <param name="filePath">反序列化对象的文件路径</param>
        /// <param name="serializeType">反序列化对象类型</param>
        public static void Deserialize(ref object obj, SerializeType serializeType, string filePath)
        {
            System.Type type = obj.GetType();
            switch (serializeType)
            {
                case SerializeType.Binary:
                    {
                        using (System.IO.FileStream fs = new System.IO.FileStream(filePath, FileMode.Open))
                        {
                            BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                            obj = bf.Deserialize(fs);
                        }
                        break;
                    }
                case SerializeType.Xml:
                    {
                        using (System.IO.FileStream fs = new System.IO.FileStream(filePath, FileMode.Open))
                        {
                            XmlSerializer xs = new XmlSerializer(obj.GetType());
                            obj = xs.Deserialize(fs);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// 用于序列化的临时类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        [Serializable()]
        [XmlRootAttribute("Root")]
        public class SerializeTempObject<T>
        {
            List<T> _tempList;
            /// <summary>
            /// 用于序列化的临时列表
            /// </summary>
            [XmlElementAttribute("InterfaceInfo")]
            public List<T> TempList
            {
                get { return _tempList; }
                set { _tempList = value; }
            }
        }
    }

    /// <summary>
    /// 序列化类型
    /// </summary>
    public enum SerializeType
    {
        /// <summary>
        /// XML序列化
        /// </summary>
        Xml,
        /// <summary>
        /// 二进制序列化
        /// </summary>
        Binary
    }
}
