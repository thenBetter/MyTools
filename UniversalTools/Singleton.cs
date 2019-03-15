using UnityEngine;
using System.Collections;

/// <summary>
/// 泛型单例
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : class, new()
{
    /// <summary>
    /// 加上一个锁
    /// </summary>
    private static readonly object _synLock = new object();

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (_synLock)
                {
                    if (instance == null)
                    {
                        instance = new T();
                    }
                }
            }
            return instance;
        }
    }
    protected Singleton()
    {
        if (instance == null)
            throw new System.Exception(string.Format("单例已经被实例化过了:{0}", typeof(T)));
        Init();
    }

    public virtual void Init() { }

    public void OnApplicationQuit()
    {
        ReleaseValue();
        OnAppQuit();
        instance = null;
    }

    protected virtual void OnAppQuit() { }

    public void ReleaseValue()
    {
        OnReleaseValue();
    }

    protected virtual void OnReleaseValue() { }
}
