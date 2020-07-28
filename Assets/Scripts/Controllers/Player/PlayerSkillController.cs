using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Skills;

public class PlayerSkillController : MonoBehaviour
{
    private PlayerStatController _stat;
    private PlayerTargetingController _target;

    public GameObject _defaultSkillBtn;
    public GameObject _secondSkillBtn;
    public GameObject _thirdSkillBtn;
    public GameObject _fourthSkillBtn;
    public GameObject _fifthSkillBtn;

    public Skill _defaultSkill;
    public Skill _secondtSkill;
    public Skill _thirdSkill;
    public Skill _fourceSkill;
    public Skill _fifthSkill;

    public bool _isCritical;
    public bool _isAvoidance;

    void Start()
    {
        _stat = GetComponent<PlayerStatController>();
        _target = GetComponent<PlayerTargetingController>();

        _defaultSkillBtn = GameObject.Find("Default_Skill");
        _secondSkillBtn = GameObject.Find("Second_Skill");
        _thirdSkillBtn = GameObject.Find("Third_Skill");
        _fourthSkillBtn = GameObject.Find("Four_Skill");
        _fifthSkillBtn = GameObject.Find("Fifth_Skill");

        Debug.Log(_defaultSkillBtn);

        _defaultSkill = GetAt(0);
        SetSkill(_defaultSkillBtn, _defaultSkill);

        _secondtSkill = GetAt(1);
        SetSkill(_secondSkillBtn, _secondtSkill);

        _thirdSkill = GetAt(2);
        SetSkill(_thirdSkillBtn, _thirdSkill);
    }

    void Update()
    {
        
    }

    private void SetSkill(GameObject skillBtn, Skill skill)
    {
        skillBtn.GetComponent<Image>().sprite = skill.Icon;
        skillBtn.GetComponentInChildren<RawImage>().texture = skill.Icon.texture;
        skillBtn.GetComponent<Button>().onClick.AddListener(() => Attack(skill, skillBtn));
    }

    public void Attack(Skill skill, GameObject skillBtn)
    {
        if (!_target.HaveTarget())
            _target.SetClosedTarget(skill.MinDistance);

        if (!_target.HaveTarget())
        {
            GameMessagePopup.Create(transform, "대상이 존재하지 않습니다.");
            return;
        }

        if (_target.IsClosedDistanceToTarget(skill.MinDistance))
        {
            switch (skill.Type)
            {
                case Define.SkillType.Short:
                case Define.SkillType.Magic:
                case Define.SkillType.Long:
                    InteractionSkill(skill, skillBtn);
                    //DieProcess();
                    break;
            }
        }
        else
        {
            GetComponent<PlayerMovementController>()._moveToTarget = true;
            GetComponent<PlayerMovementController>()._useSkill = skill;
            GetComponent<PlayerMovementController>()._useSkillBtn = skillBtn;
            //this._isAttack = true;
            //this._skill = skill;
            //this._positionToTarget = Target.Target.transform.position.normalized;
            //this._positionToTarget.y = 0f;
        }
    }

    /*
     * 스킬 사용
     */
    public void InteractionSkill(Skill skill, GameObject skillBtn)
    {
        Image skillImage = skillBtn.GetComponent<Image>();

        if (!ReadySkill(skillImage))
        {
            GameMessagePopup.Create(transform, "아직 스킬이 준비되지 않았습니다.");
            return;
        }
        else
        {
            StartCoroutine(Cooltime.CooltimeCoroutine(skillImage, skill.Cooltime));
        }

        SetCriticalHit(_stat._ciritical);
        SetAvoidance(_stat._avoidance);

        float attackDamage = GetAttackDamage(skill);
        float defence = GetDefence(skill);

        attackDamage -= defence;
        UseResource(skill);

        if (_isAvoidance)
            attackDamage = 0f;

        GameObject target = _target.Target;

        if (!_isAvoidance && attackDamage > 0)
            DamagePopup.Create(target.transform.position + Vector3.up * (target.GetComponent<Collider>().bounds.size.y), attackDamage, _isCritical);

        EnemyController enemyController = _target.Target.GetComponent<EnemyController>();
        enemyController.SetAndLookTarget(gameObject);

        EnemyStatController enemyStat = _target.Target.GetComponent<EnemyStatController>();
        enemyStat._hp -= attackDamage;
        if (enemyStat.IsDie())
        {
            _target.DieTarget();
            return;
        }

        if (skill.GetContinuosDamage() != null)
        {
            skill.CreatePrefab();
            skill.Prefab.GetComponent<Collider>().isTrigger = skill.IsPenetrating;

            ThrowSkill throwSkill = skill.Prefab.AddComponent<ThrowSkill>();
            throwSkill._caster = Define.SkillCaster.Player;
            throwSkill._player = transform;
            throwSkill._skill = skill;
            throwSkill._target = _target.Target;
            throwSkill.Fire();
        }
    }

    private void UseResource(Skill skill)
    {
        _stat._hp -= skill.UseHP;
        _stat._mana = skill.UseMana;
    }

    /*
     * 스킬 데미지 계산
     */
    private float GetAttackDamage(Skill skill)
    {
        float attackDamage = skill.Damage;

        if (attackDamage == 0) return 0f;

        switch (skill.Type)
        {
            case Define.SkillType.Short:
                attackDamage += _target.Target.GetComponent<EnemyStatController>()._strength * 0.1f;
                break;
            case Define.SkillType.Long:
                attackDamage += _target.Target.GetComponent<EnemyStatController>()._dex * 0.1f;
                break;
            case Define.SkillType.Magic:
                attackDamage += _target.Target.GetComponent<EnemyStatController>()._know * 0.1f;
                break;
        }

        if (_isCritical)
            attackDamage *= 2;

        return attackDamage;
    }

    /*
     * 크리티컬 확률
     */
    void SetCriticalHit(float critical)
    {
        _isCritical = Random.Range(0, 100) < critical;
    }

    /*
     * 회피 확률 계산
     */
    void SetAvoidance(float avoidance)
    {
        _isAvoidance = Random.Range(0, 100) < avoidance;
    }

    /*
     * 적의 방어율 계산
     */
    private float GetDefence(Skill skill)
    {
        float defence = 0f;

        switch (skill.Type)
        {
            case Define.SkillType.Short:
            case Define.SkillType.Long:
                defence += _target.Target.GetComponent<EnemyStatController>()._dex * 0.01f;
                break;
            case Define.SkillType.Magic:
                defence += _target.Target.GetComponent<EnemyStatController>()._wis * 0.01f;
                break;
        }

        return defence;
    }

    public bool ReadySkill(Image skillImage)
    {
        return skillImage.fillAmount >= 1.0f;
    }

}
