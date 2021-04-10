// ========================================================
// Description：
// Author：lch 
// CreateTime：2021/04/10 00:13
// Version：2020.2.5f1
// ========================================================
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EventTriggerListener : EventTrigger
{

    public delegate void UIDelegate(GameObject go);

    public event UIDelegate onClick;

    public static EventTriggerListener GetListener(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null) listener = go.AddComponent<EventTriggerListener>();
        return listener;
    }


    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
    }

}