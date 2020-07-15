using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Define.PlayerState _state = Define.PlayerState.Idle;

    Joystick _joystick;
    float _joystickDistance;
    Vector3 _joystickAngle;

    [SerializeField] float _speed = 6f;

    void Start()
    {
        _joystick = FindObjectOfType<Joystick>();
        transform.rotation = Camera.main.transform.localRotation;
        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.localRotation.eulerAngles.y, 0f);
        // 혹시라도 다른 부분에서 KeyAction에 동일한 이벤트를 넣었을 경우, 두번 실행되는 것을 방지하기 위해서 하나를 끊는다.
        Managers.Input.JoystickAction -= OnJoystickMove;
        Managers.Input.JoystickAction += OnJoystickMove;
    }

    void Update()
    {
        switch (_state)
        {
            case Define.PlayerState.Die:
                UpdateDie();
                break;
            case Define.PlayerState.Moving:
                UpdateMoving();
                break;
            case Define.PlayerState.Idle:
                UpdateIdle();
                break;
        }
    }

    // RUN00_F에서 호출하는 이벤트
    void OnRunEvent()
    {
        //Debug.Log($"뚜벅뚜벅");
    }

    void UpdateDie()
    {
    }

    void UpdateMoving()
    {
        if (_state == Define.PlayerState.Moving)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_joystickAngle), 0.2f);
            transform.Translate(Vector3.forward * (_speed * _joystickDistance) * Time.deltaTime);
        }

        // 애니메이션 처리
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _joystickDistance);
    }

    void UpdateIdle()
    {
        // 애니메이션 처리
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }

    void OnJoystickMove(Vector2 direction)
    {
        if (direction.x == 0.0f && direction.y == 0.0f)
        {
            _state = Define.PlayerState.Idle;
            return;
        }

        _joystickAngle = Camera.main.transform.TransformDirection(new Vector3(direction.x, 0f, direction.y));
        _joystickAngle.y = 0f;
        _joystickDistance = Mathf.Sqrt(direction.x * direction.x) + Mathf.Sqrt(direction.y * direction.y);
        
        _state = Define.PlayerState.Moving;
    }

}
