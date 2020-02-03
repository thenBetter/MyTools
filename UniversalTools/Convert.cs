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

        public static bool ApproximateTwoValue(float a, float b)
        {
            return Math.Abs(a - b) > 0.0f;
        }

        public static Color StringToColor(string value, char split)
        {
            if (string.IsNullOrEmpty(value)) return Color.white;
            string[] strArray = value.Split(split);
            float[] f = new float[4] { 0f, 0f, 0f, 255f };
            for (int i = 0; i < strArray.Length && i < f.Length; i++)
                f[i] = float.Parse(strArray[i]);

            return new Color(f[0] / 255f, f[1] / 255f, f[2] / 255f, f[3] / 255f);
        }

        public static float StringToFloat(string value)
        {
            return System.Convert.ToSingle(value);
        }
    }
}
