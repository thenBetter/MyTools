using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f;
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float x = 0;
    public float y = 0;

    public UnityEngine.UI.Button button;
    public UnityEngine.UI.Text text;

    private Vector2 oldPostion1;
    private Vector2 oldPostion2;

    private void Start()
    {
        int width = Screen.currentResolution.width;
        if (width == 1080)
        {
            Screen.SetResolution(1080, 608, false);
        }

        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        button.onClick.AddListener(delegate 
        {
            text.text = "测试结果  ";
        });
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }
        }
        if (Input.touchCount > 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                var tempPostion1 = Input.GetTouch(0).position;
                var tempPostion2 = Input.GetTouch(1).position;
                if (isEnlarge(oldPostion1, oldPostion2, tempPostion1, tempPostion2))
                {
                    if (distance > 3)
                    {
                        distance -= 0.5f;
                    }
                }
                else
                {
                    if (distance < 18.5f)
                    {
                        distance += 0.5f;
                    }
                }
                oldPostion1 = tempPostion1;
                oldPostion2 = tempPostion2;
            }
        }
    }

    private void LateUpdate()
    {
        if (target)
        {
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            var rotation = Quaternion.Euler(y, x, 0);
            Vector3 dis = new Vector3(0, 0, -distance);
            var postion = rotation * dis + target.position;

            transform.rotation = rotation;
            transform.position = postion;
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

    private bool isEnlarge(Vector2 op1, Vector2 op2, Vector2 np1, Vector2 np2)
    {
        var leng1 = Mathf.Sqrt((op1.x - op2.x) * (op1.x - op2.x) + (op1.y - op2.y)* (op1.y - op2.y));
        var leng2 = Mathf.Sqrt((np1.x - np2.x) * (np1.x - np2.x) + (np1.y - np2.y) * (np1.y - np2.y));
        if (leng1 < leng2)
            return true;
        else return false;
    }
}
