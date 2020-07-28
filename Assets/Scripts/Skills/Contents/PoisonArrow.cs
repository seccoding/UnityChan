using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Skills;

public class PoisonArrow : Skill
{
    public override void CreatePrefab()
    {
        Prefab = Managers.Resource.Instantiate("Skill/PoisonArrow");
    }

    public override IEnumerator Effect(Transform player, GameObject target, Define.SkillCaster caster)
    {
        EnemyStatController targetStatController = null;
        PlayerStatController playerStatController = null;

        if (caster == Define.SkillCaster.Player)
        {
            targetStatController = target.GetComponent<EnemyStatController>();
            target.GetComponent<EnemyStatusController>().AddEffect(ContinuosDamage.Type, ContinuosDamage.Duration);
        }
        else
        {
            playerStatController = target.GetComponent<PlayerStatController>();
            target.GetComponent<PlayerStatusController>().AddEffect(ContinuosDamage.Type, ContinuosDamage.Duration);
        }

        float timer = 0;
        while (true)
        {
            if (target == null || target.transform == null)
                break;

            if (caster == Define.SkillCaster.Player)
                targetStatController._hp -= ContinuosDamage.DamagePerSecond;
            else
                playerStatController._hp -= ContinuosDamage.DamagePerSecond;

            DamagePopup.Create(target.transform.position + Vector3.up * (target.GetComponent<Collider>().bounds.size.y), ContinuosDamage.DamagePerSecond, false);

            timer += 1;
            if (timer >= ContinuosDamage.Duration)
                break;

            yield return new WaitForSeconds(1f);
        }
    }

    public PoisonArrow()
    {
        Type = Define.SkillType.Long;
        RequireLevel = 1;
        Name = "독공격";
        Speed = 40f;
        Cooltime = 3f;
        IsTracking = false;
        IsPenetrating = true;
        IsMultipleTarget = true;
        RequireTarget = true;
        MaxTargetCount = 2f;
        UseMana = 3f;
        UseHP = 0f;
        Damage = 0f;
        MinDistance = 15f;
        ContinuosDamage = new ContinuosDamage() { Type = Define.ContinuosDamageType.Poison, Duration = 3f, DamagePerSecond = 10f };
        Icon = Managers.Resource.LoadSprite("Icon/Skills/Hand");
    }
    
}
