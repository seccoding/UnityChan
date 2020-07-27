using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Skills;

public class SkillController : MonoBehaviour
{
    private CharacterBase _characterBase;

    public GameObject _player;

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
        _characterBase = _player.GetComponent<CharacterBase>();

        _defaultSkillBtn = GameObject.Find("Default_Skill");
        _secondSkillBtn = GameObject.Find("Second_Skill");
        _thirdSkillBtn = GameObject.Find("Third_Skill");
        _fourthSkillBtn = GameObject.Find("Four_Skill");
        _fifthSkillBtn = GameObject.Find("Fifth_Skill");

        _defaultSkill = GetAt(0);
        SetSkill(_defaultSkillBtn, _defaultSkill);

        _secondtSkill = GetAt(1);
        SetSkill(_secondSkillBtn, _secondtSkill);

        _thirdSkill = GetAt(2);
        SetSkill(_thirdSkillBtn, _thirdSkill);
    }

    /*
     * 스킬창에 스킬 셋팅
     */
    private void SetSkill(GameObject skillBtn, Skill skill)
    {
        skillBtn.GetComponent<Image>().sprite = skill.Icon;
        skillBtn.GetComponentInChildren<RawImage>().texture = skill.Icon.texture;
        skillBtn.GetComponent<Button>().onClick.AddListener(() => _characterBase.Attack(skill, skillBtn));
    }

    /*
     * 스킬 사용
     */
    public void InteractionSkill(Skill skill, CharacterBase targetBase)
    {
        SetCriticalHit(_characterBase._ciritical);
        SetAvoidance(targetBase._avoidance);

        float attackDamage = GetAttackDamage(skill);
        float defence = GetDefence(skill, targetBase);

        attackDamage -= defence;
        _characterBase._hp -= skill.UseHP;
        _characterBase._mana = skill.UseMana;

        if (_isAvoidance)
            attackDamage = 0f;

        if (!_isAvoidance && attackDamage > 0)
            DamagePopup.Create(targetBase.transform.position + Vector3.up * (targetBase.GetComponent<Collider>().bounds.size.y), attackDamage, _isCritical);

        if (!targetBase.Target.HaveTarget())
            targetBase.Target.Target = _characterBase.gameObject;

        if (targetBase.Target.HaveTarget())
            targetBase.Behavior.LookTarget(_player.transform);

        targetBase._hp -= attackDamage;
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
            case SkillType.Short:
                attackDamage += _characterBase._strength * 0.1f;
                break;
            case SkillType.Long:
                attackDamage += _characterBase._dex * 0.1f;
                break;
            case SkillType.Magic:
                attackDamage += _characterBase._know * 0.1f;
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
    private float GetDefence(Skill skill, CharacterBase targetBase)
    {
        float defence = 0f;

        switch (skill.Type)
        {
            case SkillType.Short:
            case SkillType.Long:
                defence += targetBase._dex * 0.01f;
                break;
            case SkillType.Magic:
                defence += targetBase._wis * 0.01f;
                break;
        }

        return defence;
    }

    public bool ReadySkill(Image skillImage)
    {
        return skillImage.fillAmount >= 1.0f;
    }

    public void StartCoolTime(Image skillImage, float cooltime)
    {
        if (cooltime == 0f) return;
        skillImage.fillAmount = 0.0f;
        StartCoroutine(CooltimeCoroutine(skillImage, cooltime));
    }

    private IEnumerator CooltimeCoroutine(Image skillImage, float cooltime)
    {
        float time = 0;
        while (true)
        {
            time += 0.02f;
            skillImage.fillAmount = time / cooltime;
            if (time >= cooltime)
                break;
            yield return new WaitForSeconds(0.01f);
        }
    }

}
