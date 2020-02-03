/* ****************************************************************
 * @File Name   :   BlackBoard
 * @Author      :   Better
 * @Date        :   2020/1/31 16:34:46
 * @Description :   throw black borad set  key , value
 * @Edit        :   2020/1/31 16:34:46
 * ***************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UniversalTools
{
    public class BlackBoard : IEnumerable<KeyValuePair<string, BlackBoard.BbItem>>
    {
        public class BbItem
        {
            public object Value { get { return _value; } }
            private object _value;

            public void SetValue(object v)
            {
                this._value = v;
            }

            public T GetValue<T>()
            {
                return (T)_value;
            }
        }

        private Dictionary<string, BbItem> _items;
        public BlackBoard()
        {
            _items = new Dictionary<string, BbItem>();
        }

        public void SetValue(string key, object v)
        {
            BbItem item;
            if (_items.ContainsKey(key))
                item = _items[key];
            else
            {
                item = new BbItem();
                _items.Add(key, item);
            }
            item.SetValue(v);
        }

        public T GetValue<T>(string key, T defauleValue)
        {
            if (_items.ContainsKey(key)) return _items[key].GetValue<T>();

            else return defauleValue;
        }

        public IEnumerator<KeyValuePair<string, BbItem>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_items).GetEnumerator();
        }
    }
}
