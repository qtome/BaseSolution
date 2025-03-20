﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Weaversys.Util.Common.Utils.Serialize
{
    public partial class SerializeHelper
    {
        public SerializeHelper()
        { }

        #region XML序列化
        /// <summary>
        /// 文件化XML序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="filename">文件路径</param>
        public static void Save(object obj, string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(fs, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 文件化XML反序列化
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="filename">文件路径</param>
        public static object Load(Type type, string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type);
                return serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 文本化XML序列化
        /// </summary>
        /// <param name="item">对象</param>
        public string ToXml<T>(T item)
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                serializer.Serialize(writer, item);
                return sb.ToString();
            }
        }

        /// <summary>
        /// 文本化XML反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public T FromXml<T>(string str)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = new XmlTextReader(new StringReader(str)))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public static void XmlSerialize<T>(string objname, T obj)
        {

            try
            {
                string filename = objname + ".xml";
                if (File.Exists(filename))
                    File.Delete(filename);
                using (FileStream fileStream = new FileStream(filename, FileMode.Create))
                {
                    // 序列化为xml
                    XmlSerializer formatter = new XmlSerializer(typeof(T));
                    formatter.Serialize(fileStream, obj);
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// 从xml序列中反序列化
        /// </summary>
        /// <param name="objname"></param>
        /// <returns></returns>
        public static T XmlDeserailize<T>(string objname)
        {
            // System.Runtime.Serialization.IFormatter formatter = new XmlSerializer(typeof(Car));
            string filename = objname + ".xml";
            T obj;
            if (!File.Exists(filename))
                throw new Exception("对反序列化之前,请先序列化");
            //Xml格式反序列化
            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                obj = (T)formatter.Deserialize(stream);
                stream.Close();
            }
            return obj;
        }

        #endregion

        #region Json序列化
        /// <summary>
        /// JsonSerializer序列化
        /// </summary>
        /// <param name="item">对象</param>
        public string ToJson<T>(T item)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        ///// <summary>
        ///// JsonSerializer反序列化
        ///// </summary>
        ///// <param name="str">字符串序列</param>
        public T FromJson<T>(string str) where T : class
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
            {
                return serializer.ReadObject(ms) as T;
            }
        }
        #endregion

        #region SoapFormatter序列化
        ///// <summary>
        ///// SoapFormatter序列化
        ///// </summary>
        ///// <param name="item">对象</param>
        //public string ToSoap<T>(T item)
        //{
        //    SoapFormatter formatter = new SoapFormatter();
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        formatter.Serialize(ms, item);
        //        ms.Position = 0;
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.Load(ms);
        //        return xmlDoc.InnerXml;
        //    }
        //}

        ///// <summary>
        ///// SoapFormatter反序列化
        ///// </summary>
        ///// <param name="str">字符串序列</param>
        //public T FromSoap<T>(string str)
        //{
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.LoadXml(str);
        //    SoapFormatter formatter = new SoapFormatter();
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        xmlDoc.Save(ms);
        //        ms.Position = 0;
        //        return (T)formatter.Deserialize(ms);
        //    }
        //}

        ///// <summary>
        ///// 序列化为soap 即xml
        ///// </summary>
        ///// <param name="objname"></param>
        ///// <returns></returns>
        //public static void SoapSerialize(string objname,object obj)
        //{
        //    try
        //    {  
        //        string filename=objname+".Soap";
        //        if(System.IO.File.Exists(filename))
        //            System.IO.File.Delete(filename);
        //        using (FileStream fileStream = new FileStream(filename, FileMode.Create))
        //        {
        //            // 序列化为Soap
        //            SoapFormatter formatter = new SoapFormatter();
        //            formatter.Serialize(fileStream, obj);
        //            fileStream.Close();
        //        }

        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


        ///// <summary>
        ///// 反序列对象
        ///// </summary>
        ///// <param name="objname"></param>
        //public static object SoapDeserialize(string objname)
        //{
        //    object obj;
        //    System.Runtime.Serialization.IFormatter formatter = new SoapFormatter();
        //    string filename=objname+".Soap";
        //    if (!System.IO.File.Exists(filename))
        //        throw new Exception("对反序列化之前,请先序列化");
        //    //Soap格式反序列化
        //    using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
        //    {
        //        obj = formatter.Deserialize(stream);
        //        stream.Close();
        //    }
        //    return obj;
        //}

        #endregion

        #region BinaryFormatter序列化
        /// <summary>
        /// BinaryFormatter序列化
        /// </summary>
        /// <param name="item">对象</param>
        public string ToBinary<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                formatter.Serialize(ms, item);
                ms.Position = 0;
                byte[] bytes = ms.ToArray();
                StringBuilder sb = new StringBuilder();
                foreach (byte bt in bytes)
                {
                    sb.Append(string.Format("{0:X2}", bt));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// BinaryFormatter反序列化
        /// </summary>
        /// <param name="str">字符串序列</param>
        public T FromBinary<T>(string str)
        {
            int intLen = str.Length / 2;
            byte[] bytes = new byte[intLen];
            for (int i = 0; i < intLen; i++)
            {
                int ibyte = Convert.ToInt32(str.Substring(i * 2, 2), 16);
                bytes[i] = (byte)ibyte;
            }
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return (T)formatter.Deserialize(ms);
            }
        }

        /// <summary>
        /// 序列化为对象
        /// </summary>
        /// <param name="objname"></param>
        /// <param name="obj"></param>
        public static void BinarySerialize(string objname, object obj)
        {
            try
            {
                string filename = objname + ".Binary";
                if (File.Exists(filename))
                    File.Delete(filename);
                using (FileStream fileStream = new FileStream(filename, FileMode.Create))
                {
                    // 用二进制格式序列化
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fileStream, obj);
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 从二进制文件中反序列化
        /// </summary>
        /// <param name="objname"></param>
        /// <returns></returns>
        public static object BinaryDeserialize(string objname)
        {
            System.Runtime.Serialization.IFormatter formatter = new BinaryFormatter();
            //二进制格式反序列化
            object obj;
            string filename = objname + ".Binary";
            if (!File.Exists(filename))
                throw new Exception("在反序列化之前,请先序列化");
            using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                obj = formatter.Deserialize(stream);
                stream.Close();
            }
            //using (FileStream fs = new FileStream(filename, FileMode.Open))
            //{
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    object obj = formatter.Deserialize(fs);
            //}
            return obj;

        }
        #endregion

    }
}