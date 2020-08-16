/* ****************************************************************
 * @File Name   :   DoubleClick
 * @Author      :   Better
 * @Date        :   2020/8/16 16:28:06
 * @Description :   to do
 * @Edit        :   2020/8/16 16:28:06
 * ***************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UniversalTools
{
    public class DoubleClick : MonoBehaviour
    {
        public System.Action callBack;

        private int count = 0;
        private float downTime;
        //private float spaceTime;
        private const float spaceConst = 0.2f;
        private const float spaceConst_D = 0.2f;

        private Timer t;

        /// <summary>
        /// 鼠标按下
        /// </summary>
        void OnMouseDown()
        {
            downTime = Time.time;

            //if (count == 0)
            //{
            //    spaceTime = Time.time;
            //}
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        void OnMouseUp()
        {
            if (downTime + spaceConst >= Time.time)
            {
                if (count == 0)
                {
                    //开启计时器
                    Invoke("TimeFun", spaceConst_D);
                }
                count++;
            }
            else
            {
                count = 0;
            }
        }

        public void TimeFun()
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                count = 0;
                return;
            }

            //if里为双击
            if (count >= 2)
                callBack();
            count = 0;
        }
    }

}
}
