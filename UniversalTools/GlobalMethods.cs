using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// Author : better
/// DataTime: 2017-12-28
/// Descrip: 通用的静态方法
/// </summary>
namespace UniversalTools
{
    public  class GlobalMethods
    {
        /// <summary>
        /// 获取地理位置的url
        /// </summary>
        //public static string Url = "http://ip.taobao.com/service/getIpInfo.php?ip=";
        public static string Url = "https://restapi.amap.com/v3/ip?key=2056f36cd6460deee2ad766483c19ddc&ip=";  //高德获取地理位置的url
        /// <summary>
        /// 获取android小键盘的高度
        /// </summary>
        public static int AndroidKeyboardHeight
        {
            get
            {
                using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity").
                        Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");
                    using (AndroidJavaObject rect = new AndroidJavaObject("android.graphics.Rect"))
                    {
                        jo.Call("getWindowVisibleDisplayFrame", rect);
                        return Screen.height - rect.Call<int>("height");
                    }
                }
            }
        }

        /// <summary>
        ///获取ios设备小键盘的高度
        /// </summary>
        public static float IOSKeyboardHeight
        {
            get
            {
                return 0;
                //return TouchScreenKeyboard.area.height;
            }
        }
        
        /// <summary>
        /// 获取外网ip
        /// </summary>
        /// <returns></returns>
        public static string ExternalIP()
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    webClient.Credentials = CredentialCache.DefaultCredentials;
                    byte[] pageDate = webClient.DownloadData("http://pv.sohu.com/cityjson?ie=utf-8");
                    String ip = Encoding.UTF8.GetString(pageDate);
                    webClient.Dispose();

                    Match rebool = System.Text.RegularExpressions.Regex.Match(ip, @"\d{2,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                    return rebool.Value;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public static void StartExe(string exeUrl, string downUrl)
        {
            #region obsolut
            //System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            //string fileName = localURL;
            //System.Diagnostics.ProcessStartInfo myProcessStartInfo = new System.Diagnostics.ProcessStartInfo(exeUrl);
            //myProcessStartInfo.Arguments = string.
            //myProcess.Start(myProcessStartInfo);
            #endregion

            System.Diagnostics.Process.Start(exeUrl, downUrl);

            //获取当前执行程序的文件名
            //string localExePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        }

        /// <summary>
        /// 获取Text显示文字所需的宽度
        /// </summary>
        /// <param name="value">显示的文字</param>
        /// <param name="text">显示在那个文本框中</param>
        /// <returns></returns>
        public static int CaculateLengthOfText(string value, Text text)
        {
            int length = 0;
            if (string.IsNullOrEmpty(value) || text == null) length = 0;

            Font myFont = text.font;
            myFont.RequestCharactersInTexture(value, text.fontSize, text.fontStyle);
            CharacterInfo info = new CharacterInfo();
            Char[] arr = value.ToCharArray();
            foreach (var item in arr)
            {
                myFont.GetCharacterInfo(item, out info, text.fontSize);
                length += info.advance;
            }
            return length;
        }


        /// <summary>
        /// 查找父物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T FindInParents<T>(GameObject go) where T : Component
        {
            if (go == null) return null;
            var comp = go.GetComponent<T>();

            if (comp != null)
                return comp;

            var t = go.transform.parent;
            while (t != null && comp == null)
            {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
            return comp;
        }

        /// <summary>
        /// 实例化物体
        /// </summary>
        /// <param name="prefab">预制体</param>
        /// <param name="parent">父物体</param>
        public static void AddGameObject(GameObject prefab, Transform parent)
        {
            GameObject temp = GameObject.Instantiate(prefab, parent);
            temp.transform.localScale = Vector3.one;
        }

        /// <summary>
        /// 获取rect拖拽的位置
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="rect">拖拽的物体</param>
        /// <returns></returns>
        public Vector3 SetDragPostion(PointerEventData eventData, RectTransform rect)
        {
            Vector3 globalMousePos;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, eventData.position, eventData.pressEventCamera, out globalMousePos))
                return globalMousePos;
            return Vector3.zero;
        }

        /// <summary>
        /// 洗牌算法 新数组每个元素的位置必与原数不同
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T[] shuffle<T>(T[] array)
        {
            int r = 0;
            for (int i = array.Length - 1; i >= 0; i--)
            {
                r = UnityEngine.Random.Range(0, i);
                T temp = array[r];
                array[r] = array[i];
                array[i] = temp;
            }
            return array;
        }

        /// <summary>
        /// 洗牌算法  两两交换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T[] shuff<T>(T[] array)
        {
            int r = 0;
            for (int i = array.Length - 1; i >= 0; i--)
            {
                r = UnityEngine.Random.Range(0, array.Length);
                T temp = array[r];
                array[r] = array[i];
                array[i] = temp;
            }
            return array;
        }

        /// <summary>
        /// 数字转为中文的一 二 三 
        /// </summary>
        /// <param name="inputNumber">11</param>
        /// <param name="value">十一</param>
        public static void ChineseByNumber(string inputNumber, out string value)
        {
            string[] intArr = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", };
            string[] strArr = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", };
            string[] Chinese = { "", "十", "百", "千", "万", "十", "百", "千", "亿" };

            char[] tmpArr = inputNumber.ToString().ToCharArray();
            string tmpVal = "";
            for (int i = 0; i < tmpArr.Length; i++)
            {
                tmpVal += strArr[tmpArr[i] - 48];//ASCII编码 0为48
                tmpVal += Chinese[tmpArr.Length - 1 - i];//根据对应的位数插入对应的单位
            }
            value = tmpVal;
        }
    }
}
