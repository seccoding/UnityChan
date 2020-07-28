using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Animator _anim;

    private PlayerInputController _input;
    private PlayerStatusController _status;
    private PlayerStatController _stat;
    private PlayerTargetingController _target;
    private PlayerSkillController _skill;
    private CharacterController _controller;

    public bool _moveToTarget;
    public Skill _useSkill;
    public GameObject _useSkillBtn;

    void Start()
    {
        _anim = GetComponent<Animator>();

        _input = GetComponent<PlayerInputController>();
        _status = GetComponent<PlayerStatusController>();
        _stat = GetComponent<PlayerStatController>();
        _target = GetComponent<PlayerTargetingController>();
        _skill = GetComponent<PlayerSkillController>();
        _controller = GetComponent<CharacterController>();
    }

    // RUN00_F에서 호출하는 이벤트
    void OnRunEvent()
    {
        //Debug.Log($"뚜벅뚜벅");
    }

    private void FixedUpdate()
    {
        if (_input._joystickAngle == Vector3.zero)
            return; 

        if (_status.HasEffect(Define.ContinuosDamageType.Sturn)
            || _status.HasEffect(Define.ContinuosDamageType.Knockback))
            return;

        if (_status.State == Define.State.Moving)
        {
            Rotate();
            Move();
        }
        
    }

    void Update()
    {
        UpdateAnimation();
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (_moveToTarget)
        {
            _anim.SetFloat("speed", 1.5f);

            Rotate();
            _controller.Move(_target.GetDistanceVectorToTarget() * Time.deltaTime * _stat._speed);

            if (_useSkill.MinDistance >= Vector3.SqrMagnitude(transform.position - _target.Target.transform.position))
            {
                _moveToTarget = false;
                _skill.InteractionSkill(_useSkill, _useSkillBtn);
            }
        }
    }

    public void Rotate()
    {
        if (_target.HaveTarget())
        {
            Vector3 angle = _target.GetDistanceVectorToTarget();
            transform.rotation = Quaternion.LookRotation(angle);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_input._joystickAngle), 0.2f);
        }
    }   

    public void Move()
    {
        float targetSpeed = _stat._speed * _input._joystickDistance * Time.deltaTime;
        _controller.Move(_input._joystickAngle * targetSpeed);
    }

    public void UpdateAnimation()
    {
        _anim.SetFloat("speed", _input._joystickDistance);
    }
}
