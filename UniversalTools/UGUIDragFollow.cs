using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UGUIDragFollow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform target;

    private void Awake()
    {
        target = transform as RectTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(target, eventData.position, eventData.pressEventCamera, out globalPos))
        {
            target.position = globalPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
    }
}
