using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    public UnityAction OnJoystickPointerUp;
    public UnityAction OnJoystickPointerDown;
    public override void OnPointerUp(PointerEventData eventData)
    {
        OnJoystickPointerUp?.Invoke();
        base.OnPointerUp(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        OnJoystickPointerDown?.Invoke();
        base.OnPointerDown(eventData);
    }
}