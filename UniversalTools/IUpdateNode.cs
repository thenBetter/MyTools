/***************
NameSpace：UniversalTools
Name： IUpdateNode
Author： Better
Timer：2020/2/1 21:55:28
Introduce:
***************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UniversalTools
{
    public interface IUpdateNode : IDisposable
    {
        bool IsUpdating { get; set; }
        event Action<float> OnUpdate;

        void Update(float delta);

        void Start();
        void Stop();

        void Clear();

    }
}
