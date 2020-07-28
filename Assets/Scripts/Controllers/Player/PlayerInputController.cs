using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public float _joystickDistance;
    public Vector3 _joystickAngle;

    PlayerStatusController _status;
    PlayerTargetingController _target;

    void Start()
    {
        _status = GetComponent<PlayerStatusController>();
        _target = GetComponent<PlayerTargetingController>();

        Managers.Input.JoystickAction -= OnJoystickMove;
        Managers.Input.JoystickAction += OnJoystickMove;
    }

    void OnJoystickMove(Vector2 direction)
    {
        if (direction.x == 0.0f && direction.y == 0.0f)
        {
            _joystickAngle = Vector3.zero;
            _joystickDistance = 0f;
            if (!_status.IsActionStatus(Define.ActionState.Targeting))
                _status.State = Define.State.Idle;
            else
                _status.State = Define.State.Moving;
            return;
        }

        if (!_target.HaveTarget())
            _joystickAngle = Managers.Camera.Main.transform.TransformDirection(new Vector3(direction.x, 0f, direction.y));
        else
            _joystickAngle = transform.TransformDirection(new Vector3(direction.x, 0f, direction.y));
        _joystickAngle.y = 0f;
        _joystickDistance = Mathf.Sqrt(direction.x * direction.x) + Mathf.Sqrt(direction.y * direction.y);

        _status.State = Define.State.Moving;
    }
}
