using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// UGUI的点击事件，但是和scrollview的滑动冲突
/// </summary>

public class UGUIEventTriggerListern : EventTrigger
{
    public delegate void VoidDelegate(GameObject go);
    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;
    public VoidDelegate onSelect;
    public VoidDelegate onUpdateSelect;
    public VoidDelegate onSubmiet;

    public static UGUIEventTriggerListern Get(GameObject go)
    {
        UGUIEventTriggerListern listener = go.GetComponent<UGUIEventTriggerListern>();
        if (listener == null) listener = go.AddComponent<UGUIEventTriggerListern>();
        return listener;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect(gameObject);
    }

    public override void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) onUpdateSelect(gameObject);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        if (onSubmiet != null) onSubmiet(gameObject);
        {

        }
    }
}
