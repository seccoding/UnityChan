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

    public override IEnumerator Effect(Transform player, CharacterBase target, Define.PlayerState tempState)
    {
        target._state = Define.PlayerState.Poison;

        float timer = 0;
        while (true)
        {
            if (target == null || target.transform == null)
                break;

            target._hp -= ContinuosDamage.DamagePerSecond;
            DamagePopup.Create(target.transform.position + Vector3.up * (target.GetComponent<Collider>().bounds.size.y), ContinuosDamage.DamagePerSecond, false);

            timer += 1;
            if (timer >= ContinuosDamage.Duration)
            {
                target._state = tempState;
                break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public PoisonArrow()
    {
        Type = SkillType.Long;
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
        ContinuosDamage = new ContinuosDamage() { Type = ContinuosDamageType.Poison, Duration = 3f, DamagePerSecond = 10f };
        Icon = Managers.Resource.LoadSprite("Icon/Skills/Hand");
    }
    
}
