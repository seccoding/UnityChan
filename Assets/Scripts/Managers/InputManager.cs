using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager
{
    public Action KeyAction;
    public Action<Define.MouseEvent> MouseAction;

    bool _pressed = false;

    public void OnUpdate()
    {
        // 어떤 키를 눌렀고 키 액션 델리게이트가 있을 때
        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButton((int)MouseButton.RightMouse))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _pressed = false;
            }
        }
    }
}
