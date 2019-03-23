using UnityEngine;
using System.Collections;
/// <summary>
/// 显示帧率
/// </summary>
public class FPSManger : MonoBehaviour
{
    private const float updateTime = 1f;  //固定时间
    private float frames = 0f;
    private float time = 0;
    private string fps = "";


    private void Update()
    {
        time += Time.deltaTime;
        if (time >= updateTime)
        {
            fps = string.Format("FPS:{0:F2}", frames/time);
            time = 0;
            frames = 0;
        }
        frames++;
    }
}
