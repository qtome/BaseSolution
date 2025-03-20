using System;
using System.Text;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Base.Util.Common.Utils.IO
{
    /// <summary>
    /// 文件操作 辅助类
    /// </summary>
    public class FileHelper
    {
        #region 转换

        /// <summary> 
        /// 将 Base64String 转成 byte[] 
        /// </summary> 
        public static byte[] String64ToBytes(string input)
        {
            return Convert.FromBase64String(input);
        }

        /// <summary> 
        /// 将 byte[] 转成 Base64String 
        /// </summary> 
        public static string BytesToString64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary> 
        /// 将 Stream 转成 byte[] 
        /// </summary> 
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary> 
        /// 将 byte[] 转成 Stream 
        /// </summary> 
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary> 
        /// 将 Base64String 转成 byte[] 
        /// </summary> 
        public static Stream String64ToStream(string input)
        {
            byte[] bytes = String64ToBytes(input);
            return BytesToStream(bytes);
        }

        /// <summary> 
        /// 将 byte[] 转成 Base64String 
        /// </summary> 
        public static string StreamToString64(Stream stream)
        {
            byte[] bytes = StreamToBytes(stream);
            return BytesToString64(bytes);
        }

        /// <summary> 
        /// 将 Stream 写入文件 
        /// </summary> 
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[] 
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);

            // 把 byte[] 写入文件 
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary> 
        /// 从文件读取 Stream 
        /// </summary> 
        public static Stream FileToStream(string fileName)
        {
            // 打开文件 
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[] 
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream 
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        #endregion

        #region 文本文件

        /// <summary>
        /// 向文本文件中写入内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="text">写入的内容</param>
        /// <param name="encoding">编码</param>
        public static void WriteText(string filePath, string text, Encoding encoding)
        {
            //向文件写入内容
            File.WriteAllText(filePath, text, encoding);
        }

        /// <summary>
        /// 向文本文件的尾部追加内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="content">写入的内容</param>
        public static void AppendText(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }

        #endregion

        #region 文件操作类

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="filePath">完整路径（包括文件名）</param>
        /// <returns></returns>
        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 读取文件后缀
        /// </summary>
        /// <param name="filePath">完整路径（包括文件名）</param>
        /// <returns>byte[]</returns>
        public static string GetFileExtension(string filePath)
        {
            int start = filePath.LastIndexOf(".");
            int length = filePath.Length;
            string postfix = filePath.Substring(start, length - start);
            return postfix;
        }


        /// <summary>
        /// 读取文件后缀
        /// </summary>
        /// <param name="filePath">完整路径（包括文件名）</param>
        /// <returns>byte[]</returns>
        public static string GetFileExt(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        /// <summary>
        /// 创建或读取文件数据
        /// </summary>
        /// <param name="filePath">完整路径（包括文件名）</param>
        /// <returns></returns>
        public static void FileCreate(string filePath)
        {
            FileInfo CreateFile = new FileInfo(filePath); //创建文件 
            if (!CreateFile.Exists)
            {
                FileStream FS = CreateFile.Create();
                FS.Close();
            }
        }

        /// <summary>
        /// 创建或读取文件数据
        /// </summary>
        /// <param name="filePath">完整路径（包括文件名）</param>
        /// <returns>byte[]</returns>
        public static byte[] FileRead(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// 读取文件数据流
        /// </summary>
        /// <param name="filePath">完整路径（包括文件名）</param>
        /// <returns>byte[]</returns>
        public static FileStream FileStreamRead(string filePath, FileMode mode = FileMode.Open)
        {
            return File.Open(filePath, mode);
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="OrignFile">原始文件</param>
        /// <param name="NewFile">新文件路径</param>
        /// <param name="overwrite">是否复写</param>
        public static void FileCopy(string OrignFile, string NewFile, bool overwrite = true)
        {
            if (FileExists(OrignFile))
            {
                File.Copy(OrignFile, NewFile, true);
            }
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="OrignFile">原始路径</param>
        /// <param name="NewFile">新路径</param>
        public static void FileMove(string OrignFile, string NewFile)
        {
            if (FileExists(NewFile))
            {
                FileDelete(NewFile);
            }
            File.Move(OrignFile, NewFile);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">路径</param>
        public static void FileDelete(string path)
        {
            if (FileExists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// 获取指定文件详细属性
        /// </summary>
        /// <param name="filePath">文件详细路径</param>
        /// <returns></returns>
        public static string GetFileAttibe(string filePath)
        {
            string str = "";
            FileInfo objFI = new FileInfo(filePath);
            str += "详细路径:" + objFI.FullName + "<br>文件名称:" + objFI.Name + "<br>文件长度:" + objFI.Length.ToString() + "字节<br>创建时间" + objFI.CreationTime.ToString() + "<br>最后访问时间:" + objFI.LastAccessTime.ToString() + "<br>修改时间:" + objFI.LastWriteTime.ToString() + "<br>所在目录:" + objFI.DirectoryName + "<br>扩展名:" + objFI.Extension;
            return str;
        }

        /// <summary>
        /// 检查文件夹是否存在
        /// </summary>
        /// <param name="floder">文件夹路径</param>
        /// <returns></returns>
        public static bool FolderExists(string floder)
        {
            return Directory.Exists(floder);
        }

        /// <summary>
        /// 创建文件夹
        /// 如果文件夹不存在则创建
        /// </summary>
        /// <param name="floder">文件夹路径</param>
        /// <returns></returns>
        public static void FolderCreate(string floder)
        {
            if (!FolderExists(floder))
                Directory.CreateDirectory(floder);
        }

        /// <summary>
        /// 删除文件夹目录及文件(递归)
        /// </summary>
        /// <param name="floder"></param>  
        /// <returns></returns>
        public static void FolderDelete(string floder)
        {
            if (FolderExists(floder)) //如果存在这个文件夹删除之 
            {
                foreach (string d in Directory.GetFileSystemEntries(floder))
                {
                    if (FileExists(d))
                        File.Delete(d); //直接删除其中的文件                        
                    else
                        FolderDelete(d); //递归删除子文件夹 
                }
                Directory.Delete(floder, true); //删除已空文件夹                 
            }
        }

        /// <summary>
        /// 指定文件夹下面的所有内容copy到目标文件夹下面(递归)
        /// </summary>
        /// <param name="srcPath">原始路径</param>
        /// <param name="aimPath">目标文件夹</param>
        public static void FolderCopy(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                FolderCreate(aimPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                //如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                //string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                //遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    //先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (FolderExists(file))
                        FolderCopy(file, aimPath + Path.GetFileName(file));
                    //否则直接Copy文件
                    else
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 指定文件夹下面的所有内容 移动 到目标文件夹下面(递归)
        /// </summary>
        /// <param name="srcPath">原始路径</param>
        /// <param name="aimPath">目标文件夹</param>
        public static void FolderMove(string srcPath, string aimPath)
        {
            try
            {
                Directory.Move(srcPath, aimPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 获取文件夹大小
        /// </summary>
        /// <param name="folder">文件夹路径</param>
        /// <returns></returns>
        public static long GetFolderLength(string folder)
        {
            if (!Directory.Exists(folder))
                return 0;
            long len = 0;
            DirectoryInfo di = new DirectoryInfo(folder);
            foreach (FileInfo fi in di.GetFiles())
            {
                len += fi.Length;
            }
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                for (int i = 0; i < dis.Length; i++)
                {
                    len += GetFolderLength(dis[i].FullName);
                }
            }
            return len;
        }

        /// <summary>
        /// 递归文件夹-找到所有文件夹
        /// </summary>
        /// <returns></returns>
        public static void GetDirectoryInfo(string path, List<DirectoryInfo> directoryInfos)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            DirectoryInfo[] dics = root.GetDirectories();
            if (dics.Count() > 0)
            {
                foreach (var item in dics)
                {
                    GetDirectoryInfo(item.FullName, directoryInfos);
                }
            }
            else
            {
                directoryInfos.Add(root);
            }
        }

        #endregion

        #region 获取指定文件夹下所有子目录及文件(树形)

        /// <summary>
        /// 获取指定文件夹下所有子目录及文件
        /// </summary>
        /// <param name="Path">详细路径</param>
        public static string GetFoldAll(string Path)
        {
            string str = "";
            DirectoryInfo thisOne = new DirectoryInfo(Path);
            str = ListTreeShow(thisOne, 0, str);
            return str;
        }

        /// <summary>
        /// 获取指定文件夹下所有子目录及文件函数
        /// </summary>
        /// <param name="theDir">指定目录</param>
        /// <param name="nLevel">默认起始值,调用时,一般为0</param>
        /// <param name="Rn">用于迭加的传入值,一般为空</param>
        /// <returns></returns>
        public static string ListTreeShow(DirectoryInfo theDir, int nLevel, string Rn)//递归目录 文件
        {
            DirectoryInfo[] subDirectories = theDir.GetDirectories();//获得目录
            foreach (DirectoryInfo dirinfo in subDirectories)
            {

                if (nLevel == 0)
                {
                    Rn += "├";
                }
                else
                {
                    string _s = "";
                    for (int i = 1; i <= nLevel; i++)
                    {
                        _s += "│&nbsp;";
                    }
                    Rn += _s + "├";
                }
                Rn += "<b>" + dirinfo.Name.ToString() + "</b><br />";
                FileInfo[] fileInfo = dirinfo.GetFiles();   //目录下的文件
                foreach (FileInfo fInfo in fileInfo)
                {
                    if (nLevel == 0)
                    {
                        Rn += "│&nbsp;├";
                    }
                    else
                    {
                        string _f = "";
                        for (int i = 1; i <= nLevel; i++)
                        {
                            _f += "│&nbsp;";
                        }
                        Rn += _f + "│&nbsp;├";
                    }
                    Rn += fInfo.Name.ToString() + " <br />";
                }
                Rn = ListTreeShow(dirinfo, nLevel + 1, Rn);


            }
            return Rn;
        }

        /// <summary>
        /// 获取指定文件夹下所有子目录及文件(下拉框形)
        /// </summary>
        /// <param name="Path">详细路径</param>
        ///<param name="DropName">下拉列表名称</param>
        ///<param name="tplPath">默认选择模板名称</param>
        public static string GetFoldAll(string Path, string DropName, string tplPath)
        {
            string strDrop = "<select name=\"" + DropName + "\" id=\"" + DropName + "\"><option value=\"\">--请选择详细模板--</option>";
            string str = "";
            DirectoryInfo thisOne = new DirectoryInfo(Path);
            str = ListTreeShow(thisOne, 0, str, tplPath);
            return strDrop + str + "</select>";

        }

        /// <summary>
        /// 获取指定文件夹下所有子目录及文件函数
        /// </summary>
        /// <param name="theDir">指定目录</param>
        /// <param name="nLevel">默认起始值,调用时,一般为0</param>
        /// <param name="Rn">用于迭加的传入值,一般为空</param>
        /// <param name="tplPath">默认选择模板名称</param>
        /// <returns></returns>
        public static string ListTreeShow(DirectoryInfo theDir, int nLevel, string Rn, string tplPath)//递归目录 文件
        {
            DirectoryInfo[] subDirectories = theDir.GetDirectories();//获得目录

            foreach (DirectoryInfo dirinfo in subDirectories)
            {

                Rn += "<option value=\"" + dirinfo.Name.ToString() + "\"";
                if (tplPath.ToLower() == dirinfo.Name.ToString().ToLower())
                {
                    Rn += " selected ";
                }
                Rn += ">";

                if (nLevel == 0)
                {
                    Rn += "┣";
                }
                else
                {
                    string _s = "";
                    for (int i = 1; i <= nLevel; i++)
                    {
                        _s += "│&nbsp;";
                    }
                    Rn += _s + "┣";
                }
                Rn += "" + dirinfo.Name.ToString() + "</option>";


                FileInfo[] fileInfo = dirinfo.GetFiles();   //目录下的文件
                foreach (FileInfo fInfo in fileInfo)
                {
                    Rn += "<option value=\"" + dirinfo.Name.ToString() + "/" + fInfo.Name.ToString() + "\"";
                    if (tplPath.ToLower() == fInfo.Name.ToString().ToLower())
                    {
                        Rn += " selected ";
                    }
                    Rn += ">";

                    if (nLevel == 0)
                    {
                        Rn += "│&nbsp;├";
                    }
                    else
                    {
                        string _f = "";
                        for (int i = 1; i <= nLevel; i++)
                        {
                            _f += "│&nbsp;";
                        }
                        Rn += _f + "│&nbsp;├";
                    }
                    Rn += fInfo.Name.ToString() + "</option>";
                }
                Rn = ListTreeShow(dirinfo, nLevel + 1, Rn, tplPath);


            }
            return Rn;
        }
        #endregion
    }
}


