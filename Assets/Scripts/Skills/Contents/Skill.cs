using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public enum ContinuosDamageType
{
    None,
    Sturn,
    Poison,
    Knockback,
    Bleeding,
}

public enum SkillType
{
    Short,
    Long,
    Magic,
}

public class ContinuosDamage
{
    public ContinuosDamageType Type;
    public float DamagePerSecond;
    public float Duration;
}

public abstract class Skill
{
    public SkillType Type;
    public byte RequireLevel;
    public string Name;
    public float Cooltime;

    public float Speed = 4f;

    public bool IsTracking = false;
    public bool IsPenetrating;

    public bool IsMultipleTarget;
    public bool RequireTarget;
    public float MaxTargetCount;

    public float UseMana;
    public float UseHP;
    public float Damage;
    public float MinDistance;
    public ContinuosDamage ContinuosDamage;
    public Sprite Icon;

    public GameObject Prefab;

    public abstract void CreatePrefab();

    public abstract IEnumerator Effect(Transform player, CharacterBase target, Define.PlayerState tempState);
}
