using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoroutineRunner : MonoBehaviour {

    private static MonoBehaviour runner;

    private static IEnumerator gameRunner;

	// Use this for initialization
    void Awake()
    {
        runner = this;
	}

    public static Coroutine StartCoroutineRunner(IEnumerator routine) {
        return runner.StartCoroutine(routine);
    }

    public static void StartGameRunner(IEnumerator routine)
    {
        gameRunner = routine;
        runner.StartCoroutine(gameRunner);

    }
    public static void StopGameRunner()
    {
        runner.StopCoroutine(gameRunner);
    }

    public static void StopRunner(Coroutine coroutine)
    {
        if(coroutine!=null)
            runner.StopCoroutine(coroutine);
    }

}
