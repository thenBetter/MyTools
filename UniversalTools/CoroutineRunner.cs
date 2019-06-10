using UnityEngine;
using System.Collections;
/// <summary>
/// 通用的携程管理器
/// </summary>
public class CoroutineRunner : MonoBehaviour
{
    public static CoroutineRunner Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("CoroutineRunner");
                instance = go.AddComponent<CoroutineRunner>();
                instance.hideFlags = HideFlags.HideInInspector;
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
    private static CoroutineRunner instance;

    //模拟使用的迭代器
    public IEnumerator GameRunner
    {
        get { return gameRunner; }
    }
    private IEnumerator gameRunner;

    public void StartGameRunner(IEnumerator routine)
    {
        gameRunner = routine;
        instance.StartCoroutineRunner(routine);
    }

    public Coroutine StartCoroutineRunner(IEnumerator routine)
    {
        return instance.StartCoroutine(routine);
    }

    /// <summary>
    /// 停止当前的携程
    /// </summary>
    public void StopGameRunner()
    {
        instance.StopCoroutine(gameRunner);
    }

    public void StopRunner(Coroutine coroutine)
    {
        if (coroutine != null)
            instance.StopCoroutine(coroutine);
    }

    /// <summary>
    /// 停止所有携程
    /// </summary>
    public void StopAllRunner()
    {
        instance.StopAllCoroutines();
    }

}
