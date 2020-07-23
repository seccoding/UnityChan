using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Skills;

public class CharacterBase : Stat
{
    public GameObject _target;
    private CharacterBase _targetBase;

    public SkillController _skillController;

    public Define.PlayerState _state = Define.PlayerState.Idle;

    private bool IsPlayer()
    {
        return gameObject.CompareTag("Player");
    }

    private void Start()
    {
        if (!IsPlayer())
        {
            SetEnemyType(EnemyType.Slime);
        }
    }

    /*
     * 타게팅 해제
     */
    protected void ReleaseTarget()
    {
        _target = null;
    }

    /*
     * 타겟을 바라봄
     */
    protected void LookTarget(Transform target)
    {
        _target = target.gameObject;

        Vector3 targetPosition = _target.transform.position;
        Vector3 vec = targetPosition - transform.position;
        vec.Normalize();

        vec.y = 0;
        transform.rotation = Quaternion.LookRotation(vec);
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

        if (_target == null)
            SetClosedTarget(skill.MinDistance);

        if (_target == null)
        {
            GameMessagePopup.Create(transform, "대상이 존재하지 않습니다.");
            return;
        }

        _targetBase = _target.GetComponent<CharacterBase>();
            
        if (IsClosedDistanceToTarget(skill.MinDistance))
        {
            
            _skillController.InteractionSkill(skill, _targetBase);
            _skillController.ContinuosDamage(skill, _targetBase);

            DieProcess();
        }
        else
        {
            GameMessagePopup.Create(transform, "대상이 너무 멀리 있습니다.");
            return;
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
            _target = hitInfo.transform.gameObject;
    }

    /*
     * 타겟이 스킬의 최소 사용거리에 있는지 확인
     */
    private bool IsClosedDistanceToTarget(float skillMinDistance)
    {
        Vector3 distance = transform.position - _target.transform.position;
        float targetDistance = Mathf.Abs(distance.magnitude);

        return targetDistance <= skillMinDistance;
    }

    /*
     * 플레이어/타겟의 죽음확인 및 처리
     */
    private void DieProcess()
    {
        if (_targetBase._hp <= 0)
        {
            _targetBase.Die();
            GameObject.DestroyImmediate(_target);
            _target = null;
            Managers.UI.CreateEnemiesIcon();
        }
        if (_hp <= 0)
        {
            Die();
        }
    }

    /*
     * 플레이어/타겟이 죽었을 때 처리
     */
    protected void Die()
    {
        if (IsPlayer())
        {
            Debug.Log("플레이어 죽었음.");
            Managers.Instance.GameOver();
        }
        else
        {
            Debug.Log("몬스터 죽었음");
            Managers.Camera.FreeLook.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
        }
    }
}
