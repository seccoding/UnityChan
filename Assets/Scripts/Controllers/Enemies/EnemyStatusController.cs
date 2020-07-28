using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusController : MonoBehaviour
{
    public struct SkillEffectDuration
    {
        public Define.ContinuosDamageType effect;
        public float duration;
    }

    public List<SkillEffectDuration> _effect = new List<SkillEffectDuration>();
    
    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < _effect.Count; i++)
        {
            Debug.Log(transform.name + " = " + _effect[i].effect + " / " + _effect[i].duration);
        }
    }

    public SkillEffectDuration GetEffect(Define.ContinuosDamageType effect)
    {
        foreach (SkillEffectDuration efct in _effect)
        {
            if (efct.effect == effect) return efct;
        }
        return default;
    }

    public bool HasEffect(Define.ContinuosDamageType effect)
    {
        return GetEffect(effect).effect == effect;
    }

    public void AddEffect(Define.ContinuosDamageType effect, float duration)
    {
        SkillEffectDuration eff = GetEffect(effect);
        if (eff.effect == effect) _effect.Remove(eff);

        _effect.Add(new SkillEffectDuration() { effect = effect, duration = duration });
    }
}
