using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

[AddComponentMenu("UI/MyButton")]
public class MyButton : Button
{
    UnityEvent mOnDown = new UnityEvent();
    UnityEvent mOnUp = new UnityEvent();

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        mOnDown.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        mOnUp.Invoke();
    }

    public UnityEvent onDown
    {
        get { return mOnDown; }
    }

    public UnityEvent onUp
    {
        get { return mOnUp; }
    }
}