using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 分发消息类(TODO)
/// </summary>
public abstract class MessageCenter
{
    //具体的消息
    public struct Message
    {
        private string messageName;
        private int messageId;
        private int proprity;
        private Action<object> callback;
    }

    private Dictionary<string, Message> _dictionary;

    public abstract void AddLisetner();
}
