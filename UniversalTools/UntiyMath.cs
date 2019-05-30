using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace UniversalTools
{
    public class UntiyMath
    {
        /// <summary>
        /// 一个点在一条线段上移动一定距离后的点 斜率不为0
        /// </summary>
        /// <param name="moveDis">移动的距离</param>
        /// <param name="curX">起始点</param>
        /// <param name="curY"></param>
        /// <param name="targetX">终止点</param>
        /// <param name="targetY"></param>
        /// <returns></returns>
        public static Vector2 MoveEndPoint(float moveDis, float curX, float curY, float targetX, float targetY)
        {
            double dis = Math.Sqrt(Math.Pow(targetX - curX, 2) + Math.Pow(targetY - curY, 2));
            double offsetY = moveDis * (curY - targetY) / dis;
            double nextY = curY - offsetY;
            double nextX = binaryEquationGetX(curX, curY, targetX, targetY, nextY);
            return new Vector2((float)nextX, (float)nextY);
        }


        private static double binaryEquationGetX(float x1, float y1, float x2, float y2, double y)
        {
            Vector2 kbArr = KBValue(x1, y1, x2, y2);
            float k = kbArr.x;
            float b = kbArr.y;
            return (y - b) / k;
        }

        private static Vector2 KBValue(float x1, float y1, float x2, float y2)
        {
            float k = (y1 - y2) / (x1 - x2);
            float b = (x1 * y2 - x2 * y1) / (x1 - x2);
            return new Vector2(k, b);
        }


        /// <summary>
        /// 点到线段的垂直距离
        /// </summary>
        /// <param name="point">点</param>
        /// <param name="startPoint">线段的起始点</param>
        /// <param name="endPoint">线段的终止点</param>
        /// <returns></returns>
        public static double GetPointDistanceLine(Vector2 point, Vector2 startPoint, Vector2 endPoint)
        {
            double distance = 0;
            //判断是否平行
            
            return distance;
        }
    }
}
