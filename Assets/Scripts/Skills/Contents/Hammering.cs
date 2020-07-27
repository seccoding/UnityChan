using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammering : Skill
{
    public override void CreatePrefab()
    {
        Prefab = Managers.Resource.Instantiate("Skill/Hammering");
    }

    public override IEnumerator Effect(Transform player, CharacterBase target, Define.PlayerState tempState)
    {
        float timer = 0;
        while (true)
        {
            if (target == null || target.transform == null)
                break;

            if (target._state == Define.PlayerState.Knockback)
            {
                target.transform.position += (player.forward * Time.deltaTime * 20f);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            else
                break;

            timer += 0.1f;
            if (timer >= ContinuosDamage.Duration)
            {
                target._state = tempState;
                break;
            }
        }
    }

    public Hammering()
    {
        Type = SkillType.Short;
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
        ContinuosDamage = new ContinuosDamage() { Type = ContinuosDamageType.Knockback, Duration = 0.3f };
        Icon = Managers.Resource.LoadSprite("Icon/Skills/Hand");
    }

}