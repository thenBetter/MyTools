using UnityEngine;
using System.Collections;

namespace UniversalTools
{
    public class OpenAndSaveDialog
    {
        public static OpenAndSaveDialog Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OpenAndSaveDialog();
                }
                return _instance;
            }
        }
        private static OpenAndSaveDialog _instance;

        private System.Action<Texture2D, string, bool> CallBack;                    //打开文件的回调事件

        private void OpenDialog(System.Action<Texture2D, string, bool> callback)
        {
            CallBack = callback;
            string imagePath = string.Empty;
            Texture2D texture = null;
            OpenFileDlg pth = new OpenFileDlg();
            pth.structSize = System.Runtime.InteropServices.Marshal.SizeOf(pth);
            pth.filter = "*.png\0*.png\0*.jpg\0*.jpg\0\0";
            pth.file = new string(new char[256]);
            pth.maxFile = pth.file.Length;
            pth.fileTitle = new string(new char[64]);
            pth.maxFileTitle = pth.fileTitle.Length;
            string path = Application.streamingAssetsPath;
            path = path.Replace('/', '\\');
            pth.initialDir = path;
            pth.title = "打开图片";

            pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
            if (OpenFileDialog.GetOpenFileName(pth))
            {
                string filepath = pth.file;//选择的文件路径;  
                System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.Open);
#if UNITY_EDITOR
            Debug.Log(filepath);
#endif
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                texture = new Texture2D(2, 2);
                //var load = texture.LoadImage(buffer);
                texture.Apply();
                byte[] data = texture.EncodeToPNG();
                imagePath = filepath;
                fs.Close();
                if (CallBack != null)
                {
                    CallBack(texture, imagePath, true);
                }
                GameObject.DestroyImmediate(texture);
            }
        }



        //下载图片
        public void SaveDialog(Texture2D loadTexture)
        {
            if (loadTexture == null) return;

            SaveFileDlg pth = new SaveFileDlg();
            pth.structSize = System.Runtime.InteropServices.Marshal.SizeOf(pth);
            pth.filter = "*.png\0*.png\0*.jpg\0*.jpg\0\0";
            pth.file = new string(new char[256]);
            pth.maxFile = pth.file.Length;
            pth.fileTitle = new string(new char[64]);
            pth.maxFileTitle = pth.fileTitle.Length;
            string path = Application.streamingAssetsPath;
            path = path.Replace('/', '\\');
            pth.initialDir = path;
            pth.title = "保存图片";
            string title = System.DateTime.Now.ToString("yyMMddHHmmss");
            pth.file = title;
            pth.defExt = "png";
            pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
            if (SaveFileDialog.GetSaveFileName(pth))
            {
                string filepath = pth.file;  //选择的文件路径; 
                Texture2D newTexture = new Texture2D(loadTexture.width, loadTexture.height, TextureFormat.ARGB32, false);
                newTexture.SetPixels(0, 0, loadTexture.width, loadTexture.height, loadTexture.GetPixels());
                newTexture.Apply();
                byte[] data = newTexture.EncodeToPNG();
                if (data != null)
                {
                    System.IO.File.WriteAllBytes(filepath, data);
                }
                UnityEngine.Object.Destroy(newTexture);
#if UNITY_EDITOR
            Debug.Log(filepath);
#endif
            }
        }

    }
}

