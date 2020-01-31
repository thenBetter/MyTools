/* ****************************************************************
 * @File Name   :   UnityEventArgs
 * @Author      :   Better
 * @Date        :   2020/1/31 16:03:59
 * @Description :   在event中使用
 * @Edit        :   2020/1/31 16:03:59
 * ***************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UniversalTools
{
    public class UnityEventArgs<T> : EventArgs
    {
        public T value;

        public UnityEventArgs(T value)
        {
            this.value = value;
        }
        public UnityEventArgs () : this (default(T)) { }

        public override string ToString()
        {
            return string.Format("type = {0}, value = {1}", typeof(T), value);
        }
    }
}
