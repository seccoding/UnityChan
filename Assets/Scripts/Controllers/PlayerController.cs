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

    [SerializeField] float _speed = 6f;

    void Start()
    {
        SetCharacterType(CharacterType.Knight);

        Managers.Input.JoystickAction -= OnJoystickMove;
        Managers.Input.JoystickAction += OnJoystickMove;
        Managers.UI.ChooseTargetAction -= ChooseTarget;
        Managers.UI.ChooseTargetAction += ChooseTarget;

        Managers.UI.SkillAction -= UseSkill;
        Managers.UI.SkillAction += UseSkill;

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
            if (_target == null)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_joystickAngle), 0.2f);
                transform.Translate(Vector3.forward * (_speed * _joystickDistance) * Time.deltaTime);
            }
            else
            {
                LookTarget(_target.transform);
                transform.Translate(_joystickAngle * (_speed * _joystickDistance) * Time.deltaTime);
            }
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

        if (_target == null)
            _joystickAngle = Managers.Camera.Main.transform.TransformDirection(new Vector3(direction.x, 0f, direction.y));
        else
            _joystickAngle = Managers.Camera.FreeLook.transform.TransformDirection(new Vector3(direction.x, 0f, direction.y));
        _joystickAngle.y = 0f;
        _joystickDistance = Mathf.Sqrt(direction.x * direction.x) + Mathf.Sqrt(direction.y * direction.y);
        
        _state = Define.PlayerState.Moving;
    }

    protected void ChooseTarget(GameObject obj)
    {
        DebugManager.Debug($"monster: {obj}");

        if (_target == obj)
        {
            Managers.Camera.FreeLook.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
            LookTarget(obj.transform);
            ReleaseTarget();
        }
        else
        {
            LookTarget(obj.transform);
            Managers.Camera.FreeLook.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
        }
    }

    void UseSkill(float skillNumber)
    {
        Debug.Log($"Use Skill {skillNumber}");
    }

}
