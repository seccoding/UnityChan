using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : CharacterBase
{
    float _joystickDistance;
    Vector3 _joystickAngle;

    void Start()
    {
        Behavior = new CharaceterMoveAndRotation(this);
        SetCharacterType(CharacterType.Knight);

        Managers.Input.JoystickAction -= OnJoystickMove;
        Managers.Input.JoystickAction += OnJoystickMove;
        Managers.UI.ChooseTargetAction -= ChooseTarget;
        Managers.UI.ChooseTargetAction += ChooseTarget;

        Managers.UI.CreateEnemiesIcon();

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
            if (_isAttack)
                Behavior.MoveToTargetAndAttack();
            else
                Behavior.Move(_joystickAngle, _joystickDistance);

            // 애니메이션 처리
            Animator anim = GetComponent<Animator>();
            anim.SetFloat("speed", _joystickDistance);
        }
    }

    void UpdateIdle()
    {
        if (_isAttack)
        {
            _state = Define.PlayerState.Moving;
            return;
        }

        // 애니메이션 처리
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }

    void OnJoystickMove(Vector2 direction)
    {
        if (direction.x == 0.0f && direction.y == 0.0f)
        {
            if (!_isAttack)
                _state = Define.PlayerState.Idle;
            else
                _state = Define.PlayerState.Moving;
            
            return;
        }

        if (!Target.HaveTarget())
            _joystickAngle = Managers.Camera.Main.transform.TransformDirection(new Vector3(direction.x, 0f, direction.y));
        else
            _joystickAngle = Managers.Camera.FreeLook.transform.TransformDirection(new Vector3(direction.x, 0f, direction.y));
        _joystickAngle.y = 0f;
        _joystickDistance = Mathf.Sqrt(direction.x * direction.x) + Mathf.Sqrt(direction.y * direction.y);
        
        _state = Define.PlayerState.Moving;
    }

}
