using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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
            temp.transform.localPosition = Vector3.zero;
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

        /// <summary>
        /// 返回指定的颜色在纹理上的位置
        /// </summary>
        /// <param name="color">指定的颜色</param>
        /// <param name="texture">所在的纹理</param>
        /// <returns></returns>
        public static void GetVectorByColor(Color color, Texture2D texture, out  Vector3 colorPos)
        {
            colorPos = Vector3.zero;
            Color32[] colors = texture.GetPixels32();
            for (int i = 0; i < texture.width; i++)
            {
                for (int j = 0; j < texture.height; j++)
                {
                    Color32 c = colors[(texture.width * j) + i];
                    if (c.a <= 0) continue;
                    string temp = string.Format("{0},{1},{2}", c.r, c.g, c.b);
                    string colorString = string.Format("{0:F0},{1:F0},{2:F0}", color.r * 255, color.g * 255, color.b * 255);
                    if (temp.Equals(colorString))
                    {
                        colorPos = new Vector3(i, j, 0);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 字符串转颜色值
        /// </summary>
        /// <param name="value">字符串按 _ 分割 </param>
        /// <param name="color">返回的颜色值</param>
        public static void ReturnColorByString(string value, out Color color)
        {
            color = Color.black;
            string[] tempValues = value.Split('_');
            float r = 0;
            float g = 0;
            float b = 0;
            float a = 1;
            r = float.Parse(tempValues[0])/255f;
            g = float.Parse(tempValues[1])/255f;
            b = float.Parse(tempValues[2])/255f;
            a = tempValues.Length == 4 ? float.Parse(tempValues[3])/255f : 1;

            color = new Color(r, g, b, a);
        }

        /// <summary>
        ///放回在拖拽中拖拽物体下面的物体个数
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public static List<UnityEngine.EventSystems.RaycastResult> GetRaycastHitCout(UnityEngine.EventSystems.PointerEventData eventData)
        {
            List<UnityEngine.EventSystems.RaycastResult> results = new List<UnityEngine.EventSystems.RaycastResult>();
            UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventData, results);
            return results;
        }

        /// <summary>
        /// 当前鼠标点击下的UI物体
        /// </summary>
        /// <returns></returns>
        public static GameObject CurrentClickUIName()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2
            (
#if UNITY_EDITOR || UNITY_STANDALONE
            Input.mousePosition.x, Input.mousePosition.y
#endif
        );
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            if (results.Count > 0)
                return results[0].gameObject;
            return null;
        }

        /// <summary>
        /// 获取点击到UGUI的物体名字
        /// </summary>
        public static string GetClickOnUGUI
        {
            get
            {
                PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
                Vector2 vector = Vector2.zero;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            vector = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
#elif UNITY_ANDROID || UNITY_IOS
        float x = Input.touchCount > 0 ? Input.GetTouch(0).position.x : 0 ;
        float y = Input.touchCount > 0 ? Input.GetTouch(0).position.y : 0;
        vector =  new Vector2(x, y);
#endif
                eventDataCurrentPosition.position = vector;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
                if (results.Count > 0) return results[0].gameObject.name;
                else
                {
                    if (EventSystem.current.currentSelectedGameObject != null)
                    {
                        return EventSystem.current.currentSelectedGameObject.name;
                    }
                    else return null;
                }
            }
        }

        //立即获取ContentSizeFitter的区域
        public static Vector2 GetPreferredSize(GameObject obj)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(obj.GetComponent<RectTransform>());
            return new Vector2(HandleSelfFittingAlongAxis(0, obj), HandleSelfFittingAlongAxis(1, obj));
        }
        //获取宽和高
        private static float HandleSelfFittingAlongAxis(int axis, GameObject obj)
        {
            UnityEngine.UI.ContentSizeFitter.FitMode fitting = (axis == 0 ? obj.GetComponent<ContentSizeFitter>().horizontalFit : obj.GetComponent<ContentSizeFitter>().verticalFit);
            if (fitting == UnityEngine.UI.ContentSizeFitter.FitMode.MinSize)
            {
                return LayoutUtility.GetMinSize(obj.GetComponent<RectTransform>(), axis);
            }
            else
            {
                return LayoutUtility.GetPreferredSize(obj.GetComponent<RectTransform>(), axis);
            }
        }

        /// <summary>
        /// 记录消息
        /// </summary>
        /// <param name="o">消息</param>
        public static void Debug(object o)
        {
            UnityEngine.Debug.Log("Message:" + o.ToString());
        }

        /// <summary>
        /// 通用的地址                                                      
        /// </summary>
        public static string AssetPath
        {
            get
            {
                string str = "";
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        str += "jar:file://" + Application.dataPath + "!/assets/";  //Application.streamingAssetsPath  + "wenjian"
                        break;
                    case RuntimePlatform.IPhonePlayer:
                        str += Application.dataPath + "/Raw/";
                        break;
                    case RuntimePlatform.WindowsPlayer:
                    case RuntimePlatform.WindowsEditor:
                        str += Application.streamingAssetsPath + "/";
                        break;
                }
                return str;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        public void SendEmail()
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("312253260@qq.com");
            mail.To.Add("1535477644@qq.com");
            mail.Subject = "text mail";  //头部
            mail.Body = "";              //邮件内容
            string path = Application.streamingAssetsPath + "/Demo.pdf";
            mail.Attachments.Add(new Attachment(path));
            SmtpClient smtpServer = new SmtpClient("smtp.qq.com");
            //密码为基于smtp的服务密码
            smtpServer.Credentials = new System.Net.NetworkCredential("312253260@qq.com", "umjjqwljjmwlcbcj") as ICredentialsByHost;
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object o, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtpServer.Send(mail);
        }

        public static void GetBoxColliderCorner(GameObject target, out List<Vector2> vectors)
        {
            vectors = new List<Vector2>();
            BoxCollider boxCollider = target.GetComponent<BoxCollider>();
            Vector3 p1 = target.transform.InverseTransformPoint(new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.min.y));
            vectors.Add(new Vector2(p1.x, p1.y));

            Vector3 p2 = target.transform.InverseTransformPoint(new Vector2(boxCollider.bounds.min.x, boxCollider.bounds.max.y));
            vectors.Add(new Vector2(p2.x, p2.y));

            Vector3 p3 = target.transform.InverseTransformPoint(new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.max.y));
            vectors.Add(new Vector2(p3.x, p3.y));

            Vector3 p4 = target.transform.InverseTransformPoint(new Vector2(boxCollider.bounds.max.x, boxCollider.bounds.min.y));
            vectors.Add(new Vector2(p4.x, p4.y));
        }

    }

}
