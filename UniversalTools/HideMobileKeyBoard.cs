using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// 屏蔽移动端弹出系统键盘
/// </summary>
public class HideMobileKeyBoard : InputField
{
    protected override void Start()
    {
        keyboardType = (TouchScreenKeyboardType)(-1);
        base.Start();
    }

    protected override void LateUpdate()
    {
        try
        {
            base.LateUpdate();
        }
        catch (System.Exception e)
        {
#if UNITY_EDITOR
          Debug.Log(e.Message);
#endif
        }

    }
}
