using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 公共的转换方法
/// </summary>
namespace UniversalTools
{
    public  class Convert
    {
        /// <summary>
        /// 字符串转为vector2
        /// </summary>
        /// <param name="value">需要转换的字符串</param>
        /// <param name="split">分隔符</param>
        /// <returns></returns>
        public static Vector2 StringToVector2(string value, char split)
        {
            if (string.IsNullOrEmpty(value)) return Vector2.zero;
            string[] strArray = value.Split(split);
            float x = float.Parse(strArray[0]);
            float y = float.Parse(strArray[1]);
            return new Vector2(x, y);
        }

        public static bool ApproximateTwoFloatValue(float a, float b)
        {
            return Mathf.Approximately(a, b);
        }

        public static Color StringToColor(string value, char split)
        {
            if (string.IsNullOrEmpty(value)) return Color.white;
            string[] strArray = value.Split(split);
            if (strArray.Length < 3) return Color.white;
            float r = 1;
            float g = 1;
            float b = 1;
            float a = 1;
            r = float.Parse(strArray[0]) / 255f;
            g = float.Parse(strArray[1]) / 255f;
            b = float.Parse(strArray[2]) / 255f;
            if (strArray.Length == 4)
                a = float.Parse(strArray[3]) / 255f;

            return new Color(r, g, b, a);
        }
    }
}
