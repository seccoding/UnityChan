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

    public override IEnumerator Effect(Transform player, CharacterBase target, Define.PlayerState tempState)
    {
        throw new System.NotImplementedException();
    }

    public Default()
    {
        Type = SkillType.Short;
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
        ContinuosDamage = new ContinuosDamage() { Type = ContinuosDamageType.None };
        Icon = Managers.Resource.LoadSprite("Icon/Skills/Hand");
    }

}