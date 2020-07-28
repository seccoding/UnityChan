using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammering : Skill
{
    public override void CreatePrefab()
    {
        Prefab = Managers.Resource.Instantiate("Skill/Hammering");
    }

    public override IEnumerator Effect(Transform player, GameObject target, Define.SkillCaster caster)
    {
        if (caster == Define.SkillCaster.Player)
            target.GetComponent<EnemyStatusController>().AddEffect(ContinuosDamage.Type, ContinuosDamage.Duration);
        else
            target.GetComponent<PlayerStatusController>().AddEffect(ContinuosDamage.Type, ContinuosDamage.Duration);

        float timer = 0;
        while (true)
        {
            if (target == null || target.transform == null)
                break;

            target.transform.position += (player.forward * Time.deltaTime * 20f);
            yield return new WaitForSeconds(Time.deltaTime);

            timer += 0.1f;
            if (timer >= ContinuosDamage.Duration)
                break;
        }
    }

    public Hammering()
    {
        Type = Define.SkillType.Short;
        RequireLevel = 1; ;
        Name = "내려찍기";
        Speed = 20;
        Cooltime = 1f;
        IsMultipleTarget = false;
        RequireTarget = true;
        MaxTargetCount = 1f;
        UseMana = 3f;
        UseHP = 0f;
        Damage = 10f;
        MinDistance = 2f;
        ContinuosDamage = new ContinuosDamage() { Type = Define.ContinuosDamageType.Knockback, Duration = 0.3f };
        Icon = Managers.Resource.LoadSprite("Icon/Skills/Hand");
    }

}