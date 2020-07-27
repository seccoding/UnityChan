using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Skills;

public class CharacterBase : Stat
{
    public bool _isAttack;
    public Skill _skill;
    public Vector3 _positionToTarget;

    TargetPointerPopup _targetPopup;

    public CharacterSetTarget Target { get; } = new CharacterSetTarget();
    public CharaceterMoveAndRotation Behavior { get; set; }
    public SkillController _skillController;
    public Define.PlayerState _state = Define.PlayerState.Idle;

    private bool IsPlayer()
    {
        return gameObject.CompareTag("Player");
    }

    private void Start()
    {
        if (!IsPlayer())
            SetEnemyType(EnemyType.Slime);
    }

    public void ChooseTarget(GameObject obj)
    {
        if (_targetPopup != null && IsPlayer())
        {
            TargetPointerPopup.Destroy(_targetPopup);
            _targetPopup = null;
        }

        if (Target.Target == obj)
        {
            Managers.Camera.FreeLook.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
            Behavior.LookTarget(obj.transform);
            Target.ReleaseTarget();
        }
        else
        {
            Behavior.LookTarget(obj.transform);
            Managers.Camera.FreeLook.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;

            if (IsPlayer())
                _targetPopup = TargetPointerPopup.Create(Target.Target.transform);
        }
    }

    /*
     * 타겟에게 공격
     */
    public void Attack(Skill skill, GameObject skillButton)
    {
        if (_skillController == null)
            _skillController = GameObject.Find("Skill").GetComponent<SkillController>();

        Image skillImage = skillButton.GetComponent<Image>();

        if (!_skillController.ReadySkill(skillImage))
        {
            GameMessagePopup.Create(transform, "아직 스킬이 준비되지 않았습니다.");
            return;
        }
        else
            _skillController.StartCoolTime(skillImage, skill.Cooltime);

        if (!Target.HaveTarget())
            SetClosedTarget(skill.MinDistance);

        if (!Target.HaveTarget())
        {
            GameMessagePopup.Create(transform, "대상이 존재하지 않습니다.");
            return;
        }

        if (IsClosedDistanceToTarget(skill.MinDistance))
        {
            CharacterBase targetBase = Target.Target.GetComponent<CharacterBase>();

            switch(skill.Type)
            {
                case SkillType.Short:
                    _skillController.InteractionSkill(skill, targetBase);
                    //_skillController.ContinuosDamage(skill, targetBase);
                    DieProcess();
                    break;
                case SkillType.Magic:
                case SkillType.Long:
                    skill.CreatePrefab();
                    skill.Prefab.GetComponent<Collider>().isTrigger = skill.IsPenetrating;

                    ThrowSkill throwSkill = skill.Prefab.AddComponent<ThrowSkill>();
                    throwSkill._player = transform;
                    throwSkill._skill = skill;
                    throwSkill._skillController = _skillController;
                    throwSkill._target = Target.Target;
                    throwSkill.Fire();
                    break;
            }
        }
        else
        {
            this._isAttack = true;
            this._skill = skill;
            this._positionToTarget = Target.Target.transform.position.normalized;
            this._positionToTarget.y = 0f;
        }
    }
    

    /*
     * 플레이어의 정면으로 스킬의 최소 사용거리에 있는 적을 타겟으로 설정
     */
    private void SetClosedTarget(float skillMinDistance)
    {
        Debug.DrawRay(transform.position + Vector3.up, transform.forward * skillMinDistance, Color.red, 1f);
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hitInfo, skillMinDistance, 1 << 8))
        {
            Target.Target = hitInfo.transform.gameObject;
            if (IsPlayer())
                _targetPopup = TargetPointerPopup.Create(Target.Target.transform);
        }
    }

    public float GetTargetDistance()
    {
        Vector3 distance = transform.position - Target.Target.transform.position;
        return Mathf.Abs(distance.magnitude);
    }

    /*
     * 타겟이 스킬의 최소 사용거리에 있는지 확인
     */
    private bool IsClosedDistanceToTarget(float skillMinDistance)
    {
        float targetDistance = GetTargetDistance();
        return targetDistance <= skillMinDistance;
    }

    /*
     * 플레이어/타겟의 죽음확인 및 처리
     */
    public void DieProcess()
    {
        CharacterBase targetBase = Target.Target.GetComponent<CharacterBase>();
        if (targetBase._hp <= 0)
        {
            targetBase.Die();
            Target.Destroy();
            if (IsPlayer())
                TargetPointerPopup.Destroy(_targetPopup);
            Managers.UI.CreateEnemiesIcon();
        }
        if (_hp <= 0)
            Die();
    }

    /*
     * 플레이어/타겟이 죽었을 때 처리
     */
    protected void Die()
    {
        if (IsPlayer())
            Managers.Instance.GameOver();
        else
            Managers.Camera.FreeLook.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
    }
}
