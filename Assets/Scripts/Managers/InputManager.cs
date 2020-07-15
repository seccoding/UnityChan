using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class InputManager
{
    public Action<Vector2> JoystickAction;

    public Joystick _joystick;

    public void OnUpdate()
    {
        // 조이스틱을 움직인 경우
        if (JoystickAction != null)
            JoystickAction.Invoke(_joystick.Direction);

        // UI를 클릭한것을 무시한다.
        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;
    }
}
