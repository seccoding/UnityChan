using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{
    public Define.ActionState Action { get; set; } = Define.ActionState.None;
    public Define.State State { get; set; } = Define.State.Idle;

    public struct SkillEffectDuration
    {
        public Define.ContinuosDamageType effect;
        public float duration;
    }

    public List<SkillEffectDuration> _effect = new List<SkillEffectDuration>();

    public bool IsActionStatus(Define.ActionState action)
    {
        return Action == action;
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

    void Start()
    {

    }

    void Update()
    {
        for (int i = 0; i < _effect.Count; i++)
        {
            Debug.Log(_effect[i].effect + " / " + _effect[i].duration);
        }
    }
}
