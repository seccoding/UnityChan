using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    public static IEnumerator Posion(Skill skill, CharacterBase target, Define.PlayerState tempState)
    {
        float timer = 0;
        while (true)
        {
            if (target == null || target.transform == null)
                break;

            target._hp -= skill.ContinuosDamage.DamagePerSecond;
            DamagePopup.Create(target.transform.position + Vector3.up * (target.GetComponent<Collider>().bounds.size.y), skill.ContinuosDamage.DamagePerSecond, false);

            timer += 1;
            if (timer >= skill.ContinuosDamage.Duration)
            {
                target._state = tempState;
                break;
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
