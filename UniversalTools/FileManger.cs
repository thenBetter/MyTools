using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
/// <summary>
/// 通用的io操作
/// better  2019-04-19
/// </summary>
namespace UniversalTools
{
    public class FileManger
    {
        /// <summary>
        /// 是否保存文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsExitFileInfo(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFile(string path)
        {
            if (!IsExitFileInfo(path))
                File.Create(path);
        }

        /// <summary>
        /// 获取文件夹下的所有文件总数
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int GetFileChildCount(string path)
        {
            int count = 0;
            if (Directory.Exists(path))
                count = Directory.GetFiles(path).Length;
            return count;
        }

        /// <summary>
        /// 根据路径读取文本内容 
        /// </summary>
        /// <param name="path">文件</param>
        /// <param name="content">文件内容</param>
        public static void GetContentByPath(string path, out string content)
        {
            content = string.Empty;
            if (IsExitFileInfo(path))
                content = File.ReadAllText(path);
        }

        /// <summary>
        /// 根据路径读取二进制文件流
        /// </summary>
        /// <param name="path">文件</param>
        /// <param name="bytes">二进制流数据</param>
        public static void GetByteByPath(string path, out byte[] bytes)
        {
            bytes = new byte[0];
            if (IsExitFileInfo(path))
                bytes = File.ReadAllBytes(path);
        }

        /// <summary>
        /// 获取文件夹下所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static FileInfo[] fileInfo(string path, string extension)
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            FileInfo[] info = folder.GetFiles(string.Format("*.{0}", extension), SearchOption.TopDirectoryOnly);
            return info;
        }

        public static string ReadFileContent(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
