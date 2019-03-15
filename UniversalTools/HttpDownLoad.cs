using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UniversalTools
{
    public class DownLoadEnum
    {
        public string url;
        public string path;

        public DownLoadEnum(string url, string path)
        {
            this.url = url;
            this.path = path;
        }
    }
    /// <summary>
    /// 使用http下载类
    /// </summary>
    public class HttpDownLoad
    {
        public void HttpDownload(object down)
        {
            DownLoadEnum loader =  down as  DownLoadEnum;
            if (!Directory.Exists(loader.path))
                Directory.CreateDirectory(loader.path);

            try
            {
                FileStream fs = new FileStream(loader.path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                HttpWebRequest request = WebRequest.Create(loader.url) as HttpWebRequest;
                HttpWebResponse reponse = request.GetResponse() as HttpWebResponse;         //必须要使用request的post请求
                Stream sr = reponse.GetResponseStream();
                byte[] bt = new byte[1024];
                int size = sr.Read(bt, 0, (int)bt.Length);
                while (size > 0)
                {
                    fs.Write(bt, 0, size);
                    size = sr.Read(bt, 0, (int)bt.Length);
                }
                fs.Close();
                sr.Close();
                string suffixName = loader.url;
                int su = suffixName.LastIndexOf('/');
                suffixName = loader.path + suffixName.Substring(su);
                //System.IO.File.Move();  //从临时地址拷贝到指定地址
                Debug.Log("下载完成");
            }
            catch (Exception e)
            {
                Debug.Log("下载失败：错误提示" + e.Message);
            }
        }
    }
}
