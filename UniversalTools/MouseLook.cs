
using UnityEngine;
using System.Collections;
/// <summary>
/// mouse look 
/// set camera look by mouse or touch
/// </summary>
public class MouseLook : MonoBehaviour
{
    public bool lockX = false;
    public bool lockY = false;

    public float yMinLimit = -89f;
    public float yMaxLimit = 89f;

    private Vector3 _lastMousePos = Vector3.zero;
    private Vector3 _targetRot = Vector3.zero;
    private Vector3 _velocityV3 = Vector3.zero;
    private Vector3 _smooth = Vector3.zero;

    /// <summary>
    /// 旋转速率
    /// </summary>
    public float smoothTime = 0.3f;

    /// <summary>
    /// 旋转比例
    /// </summary>
    public Vector2 ratio = Vector2.zero;

    [Range(0, 2)]
    public int workMouseButton = 0;

    // Use this for initialization
    void Start()
    {
        _smooth.x = transform.localEulerAngles.y;
        _smooth.y = -transform.localEulerAngles.x;

        _targetRot = _smooth;
    }

    private void OnEnable()
    {
        _smooth.x = transform.localEulerAngles.y;
        _smooth.y = -transform.localEulerAngles.x;

        _targetRot = _smooth;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_IPHONE || UNITY_ANDROID
        TouchControl();
#else
        MouseControl();
#endif
        _smooth = Vector3.SmoothDamp(_smooth, _targetRot, ref _velocityV3, smoothTime);
        transform.localEulerAngles = new Vector3(_smooth.y, _smooth.x, 0);
    }

    private void MouseControl()
    {
        if (Input.GetMouseButton(workMouseButton))
        {
            if (Input.GetMouseButtonDown(workMouseButton))
            {
                _lastMousePos = Input.mousePosition;
            }
            if (!lockX)
            {
                _targetRot.x += (Input.mousePosition.x - _lastMousePos.x) * ratio.x;
            }
            if (!lockY)
            {
                _targetRot.y += (Input.mousePosition.y - _lastMousePos.y) * ratio.y;

                _targetRot.y = Mathf.Clamp(_targetRot.y, yMinLimit, yMaxLimit);
            }

            _lastMousePos = Input.mousePosition;
        }
    }

    private void TouchControl()
    {
        if (Input.touchCount == 1)
        {
            if (!lockX)
            {
                _targetRot.x += Input.GetTouch(0).deltaPosition.x * ratio.x;
            }

            if (!lockY)
            {
                _targetRot.y += Input.GetTouch(0).deltaPosition.y * ratio.y;
                _targetRot.y = Mathf.Clamp(_targetRot.y, yMinLimit, yMaxLimit);
            }
        }
    }
}
