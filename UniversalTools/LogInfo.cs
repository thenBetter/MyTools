using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 打印日志信息
/// </summary>
namespace UniversalTools
{
    public class LogInfo
    {
        public static LinkedList<string> list = new LinkedList<string>();

        /// <summary>
        /// 打印消息
        /// </summary>
        /// <param name="tip">日志类型</param>
        /// <param name="obj">日志数据</param>
        public static void DebugInfo(string tip, object obj)
        {
            string tips = tip + "-" + obj.ToString();
            UnityEngine.Debug.Log(tips);
            list.AddLast(tips);
        }
    }
}
