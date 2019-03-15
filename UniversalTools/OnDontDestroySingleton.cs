using UnityEngine;
using System.Collections;
/// <summary>
/// 不销毁的泛型单例类
/// </summary>
/// <typeparam name="T"></typeparam>
public class OnDontDestroySingleton<T> : MonoBehaviour  where T : OnDontDestroySingleton<T> 
{
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.Find("OnDontDestroy");
                if (go == null)
                {
                    go = new GameObject("OnDontDestroy");
                    DontDestroyOnLoad(go);
                }
                instance = go.GetComponent<T>();
                if (instance == null)
                    instance = go.AddComponent<T>();
            }
            return instance;
        }
    }
    private static T instance = null;

    /// <summary>
    /// 退出事件
    /// </summary>
    private void OnApplicationQuit() { }

    /// <summary>
    /// 释放事件
    /// </summary>
    public virtual void ReleaseValue()
    {

    }
}
