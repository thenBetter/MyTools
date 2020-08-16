using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Object = System.Object;
/// <summary>
/// 分发消息类(TODO)
/// </summary>
/// 
/// <summary>
/// 消息结构
/// </summary>
public class Message
{
    public string msgName;

    public Object obj;

    public Message(string msgName, Object obj)
    {
        this.msgName = msgName;
        this.obj = obj;
    }
}

public class MessageNotify
{
    //优先级
    public enum Priority
    {
        Low,
        Mid,
        High
    }
    //具体的消息
    public struct MSGListener
    {
        public bool isOnce;
        public bool isDownWard;
        public Priority priority;
        public Action<Object> callback;

        public MSGListener(Action<Object> callback, bool isOnce, bool isDownWard, Priority priority)
        {
            this.callback = callback;
            this.isOnce = isOnce;
            this.isDownWard = isDownWard;
            this.priority = priority;
        }
    }
    private Dictionary<string, List<MSGListener>> msgListenerArray;
    public static MessageNotify GetInstance()
    {
        if (instance == null)
            instance = new MessageNotify();
        return instance;
    }
    private static MessageNotify instance;

    private MessageNotify()
    {
        msgListenerArray = new Dictionary<string, List<MSGListener>>();
    }


    public void RegisterListener(string name, Action<Object> callback)
    {
        RegisterListener(name, callback, false, true, Priority.Low);
    }

    public void RegisterListener(string name, Action<Object> callback, bool isOnce)
    {
        RegisterListener(name, callback, isOnce, true, Priority.Low);
    }

    public void RegisterListener(string name, Action<Object> callback, bool isOnce, bool isDownWard)
    {
        RegisterListener(name, callback, isOnce, isDownWard, Priority.Low);
    }

    /// <summary>
    /// 注册消息
    /// </summary>
    /// <param name="name"></param>
    /// <param name="callback"></param>
    /// <param name="isOnce"></param>
    /// <param name="isDownWard"></param>
    /// <param name="priority"></param>
    public void RegisterListener(string name, Action<Object> callback, bool isOnce, bool isDownWard, Priority priority)
    {
#if UNITY_EIDTOR
         UnityEngine.Debug.Log("register message <" + name + ">");
#endif
        MSGListener temp = new MSGListener(callback, isOnce, isDownWard, priority);
        List<MSGListener> tempLink;
        if (msgListenerArray.TryGetValue(name, out tempLink))
        {
            for (int i = 0; i < tempLink.Count; i++)
            {
                if (tempLink[i].callback == callback)
                {
                    UnityEngine.Debug.Log("this message has register");
                    return;
                }
            }
            tempLink.Add(temp);
        }
        else
        {
            tempLink = new List<MSGListener>();
            tempLink.Add(temp);
            msgListenerArray.Add(name, tempLink);
        }
    }

    /// <summary>
    /// 广播消息
    /// </summary>
    /// <param name="msgName"></param>
    /// <param name="obj"></param>
    public void Notify(string msgName, Object obj)
    {
        UnityEngine.Debug.Log("brocast message:" + msgName);
        List<MSGListener> tempLink;
        if (msgListenerArray.TryGetValue(msgName, out tempLink))
        {
            for (int i = tempLink.Count - 1; i >= 0; i--)
            {
                bool isDownWard = tempLink[i].isDownWard;
                tempLink[i].callback(obj);
                if (!isDownWard)
                    break;
            }
        }
        else
        {
            UnityEngine.Debug.Log("this mesage Name:" + msgName  + " not  register");
        }
    }

    /// <summary>
    /// 广播实体
    /// </summary>
    /// <param name="message"></param>
    public void Notity(Message message)
    {
        UnityEngine.Debug.Log("brocast message:" + message.msgName);
        List<MSGListener> tempLink;
        if (msgListenerArray.TryGetValue(message.msgName, out tempLink))
        {
            for (int i = tempLink.Count - 1; i >= 0; i--)
            {
                bool isDownWard = tempLink[i].isDownWard;
                tempLink[i].callback(message.obj);
                if (!isDownWard)
                    break;
            }
        }
        else
        {
            UnityEngine.Debug.Log("this mesage Name:" + message.msgName + " not  register");
        }
    }

    /// <summary>
    /// 取消消息
    /// </summary>
    /// <param name="msgName"></param>
    /// <param name="callback"></param>
    public void CancelListener(string msgName, Action<Object> callback)
    {
        List<MSGListener> temp;

        if (msgListenerArray.TryGetValue(msgName, out temp))
        {
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].callback == callback)
                {
                    temp.RemoveAt(i);
                    break;
                }
            }
        }
    }

    public void CancelAllMsgListener()
    {
        if (msgListenerArray.Count > 0)
        {
            msgListenerArray.Clear();
        }
    }

    public T GetMessageAttribute<T>(Object o) where T : class
    {
        try
        {
            T t = o as T;
            return t;
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log(ex.ToString());
            return default(T);
        }
    }

    public void PrintAllMsg()
    {
        foreach (List<MSGListener> item in msgListenerArray.Values)
        {
            foreach (var clone in item)
            {
                UnityEngine.Debug.Log(clone.callback.Method.Name);
            }
        }
    }
}