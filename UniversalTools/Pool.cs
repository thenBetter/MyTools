using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Pool<T>  where T : new ()
{
    public readonly Stack<T> m_stack = new Stack<T>();    //存放物体的栈
    public readonly UnityAction<T> m_ActionGet;           
    public readonly UnityAction<T> m_ActionRelease;
    public int countAll { get; private set; }
    public int countActive { get { return countAll - countInactive; } }
    public int countInactive { get { return m_stack.Count; } }

    public Pool(UnityAction<T> getAction, UnityAction<T> releaseAction)
    {
        m_ActionGet = getAction;
        m_ActionRelease = releaseAction;
    }

    public T Get()
    {
        T element;
        if (m_stack.Count == 0)
        {
            element = new T();
        }
        else element = m_stack.Pop();
        if (m_ActionGet != null)
            m_ActionGet(element);
        return element;
    }
    
    public void Release(T element)
    {
        if (m_stack.Count > 0 && ReferenceEquals(m_stack.Peek(), element))
            Debug.LogWarning("Trying to Destory object that is already released to pool");
        if (m_ActionRelease != null)
            m_ActionRelease(element);

        m_stack.Push(element);
    }
}
