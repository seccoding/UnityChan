using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Default : Skill
{
    public override void CreatePrefab()
    {
        Prefab = Managers.Resource.Instantiate("Skill/Default");
    }

    public override IEnumerator Effect(Transform player, GameObject target, Define.SkillCaster caster)
    {
        return default;
    }

    public Default()
    {
        Type = Define.SkillType.Short;
        RequireLevel = 1;
        Name = "맨손공격";
        Speed = 100f;
        Cooltime = 0f;
        IsMultipleTarget = false;
        RequireTarget = true;
        MaxTargetCount = 1f;
        UseMana = 0f;
        UseHP = 0f;
        Damage = 5f;
        MinDistance = 2f;
        ContinuosDamage = new ContinuosDamage() { Type = Define.ContinuosDamageType.None };
        Icon = Managers.Resource.LoadSprite("Icon/Skills/Hand");
    }

}