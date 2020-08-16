/* ****************************************************************
 * @File Name   :   计时器
 * @Author      :   Better
 * @Date        :   2020/8/16 16:20:20
 * @Description :   to do
 * @Edit        :   2020/8/16 16:20:20
 * ***************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UniversalTools
{
    public class Timer : MonoBehaviour
    {
        private static Timer instance;

        private class TimerStruct
        {
            public float Timer { get; set; }
            public float Interval { get; set; }
            public System.Action CallBack { get; set; }
        }

        private List<TimerStruct> array = new List<TimerStruct>();

        private bool isDelete;

        void Awake()
        {
            StartCoroutine("Function");

            MessageNotify.GetInstance().RegisterListener("RegisterTimer", Register);
            MessageNotify.GetInstance().RegisterListener("CancelTimer", Cancel);
        }

        IEnumerator Function()
        {
            while (true)
            {
                for (int i = 0; i < array.Count; i++)
                {
                    if (array[i].Timer + array[i].Interval <= Time.time)
                    {
                        array[i].CallBack();
                        if (isDelete)
                        {
                            isDelete = false;
                            break;
                        }
                        array[i].Timer = Time.time;
                    }
                }
                yield return null;
            }
        }

        public static Timer GetInstance()
        {
            GameObject runtime = GameObject.Find("Runtime");
            if (runtime == null)
                runtime = new GameObject("Runtime");

            instance = runtime.GetComponent<Timer>();
            if (instance == null)
                instance = runtime.AddComponent<Timer>();

            return instance;
        }

        private void Register(System.Object o)
        {
            RegisterTimerAttribute attribute = MessageNotify.GetInstance().GetMessageAttribute<RegisterTimerAttribute>(o);

            array.Add(new TimerStruct()
            {
                Timer = Time.time,
                Interval = attribute.Interval,
                CallBack = attribute.CallBack
            });
        }

        private void Cancel(System.Object o)
        {
            CancelTimerAttribute attribute = MessageNotify.GetInstance().GetMessageAttribute<CancelTimerAttribute>(o);

            int index = array.FindIndex(clone => clone.CallBack == attribute.CallBack);

            if (index != -1)
            {
                array.RemoveAt(index);
                isDelete = true;
            }
        }

        void OnDestroy()
        {
            MessageNotify.GetInstance().CancelListener("RegisterTimer", Register);
            MessageNotify.GetInstance().CancelListener("CancelTimer", Cancel);

            StopCoroutine("Function");
        }
    }

    public class RegisterTimerAttribute : Message
    {
        public float Interval { get; set; }
        public System.Action CallBack { get; set; }

        public RegisterTimerAttribute()
        {
            msgName = "RegisterTimer";
        }
    }

    public class CancelTimerAttribute : Message
    {
        public System.Action CallBack { get; set; }

        public CancelTimerAttribute()
        {
            msgName = "CancelTimer";
        }
    }

}
}
