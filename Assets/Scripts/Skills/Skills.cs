using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills
{
    public enum SkillType
    {
        Short,
        Long,
        Magic,
    }

    public enum ContinuosDamageType
    {
        None,
        Sturn,
        Poison,
        Knockback,
        Bleeding,
    }

    static Dictionary<float, Skill> _skills;

    public class Skill
    {
        public SkillType Type;
        public float Number;
        public byte RequireLevel;
        public string Name;
        public float Cooltime;

        public bool IsMultipleTarget;
        public bool RequireTarget;
        public float MaxTargetCount;

        public float UseMana;
        public float UseHP;
        public float Damage;
        public float MinDistance;
        public ContinuosDamage ContinuosDamage;
        public Sprite Icon;
    }

    public class ContinuosDamage
    {
        public ContinuosDamageType Type;
        public float DamagePerSecond;
        public float Duration;
    }

    private static void Init()
    {
        _skills = new Dictionary<float, Skill>();
        _skills.Add(0f, new Skill()
        {
            Type = SkillType.Short,
            RequireLevel = 1,
            Number = 0f,
            Name = "맨손공격",
            Cooltime = 0f,
            IsMultipleTarget = false,
            RequireTarget = true,
            MaxTargetCount = 1f,
            UseMana = 0f,
            UseHP = 0f,
            Damage = 5f,
            MinDistance = 2f,
            ContinuosDamage = new ContinuosDamage() { Type = ContinuosDamageType.None },
            Icon = Managers.Resource.LoadSprite("Icon/Skills/Hand"),
        });

        _skills.Add(1f, new Skill()
        {
            Type = SkillType.Short,
            RequireLevel = 1,
            Number = 1f,
            Name = "내려찍기",
            Cooltime = 1f,
            IsMultipleTarget = false,
            RequireTarget = true,
            MaxTargetCount = 1f,
            UseMana = 3f,
            UseHP = 0f,
            Damage = 10f,
            MinDistance = 2f,
            ContinuosDamage = new ContinuosDamage() { Type = ContinuosDamageType.Knockback, Duration = 0.3f },
            Icon = Managers.Resource.LoadSprite("Icon/Skills/Hand"),
        });

        _skills.Add(2f, new Skill()
        {
            Type = SkillType.Short,
            RequireLevel = 1,
            Number = 2f,
            Name = "독공격",
            Cooltime = 3f,
            IsMultipleTarget = false,
            RequireTarget = true,
            MaxTargetCount = 1f,
            UseMana = 3f,
            UseHP = 0f,
            Damage = 0f,
            MinDistance = 2f,
            ContinuosDamage = new ContinuosDamage() { Type = ContinuosDamageType.Poison, Duration = 3f, DamagePerSecond = 10f },
            Icon = Managers.Resource.LoadSprite("Icon/Skills/Hand"),
        });
    }

    public static Skill GetAt(float skillNumber)
    {
        if (_skills == null)
            Init();

        Skill skill;
        if (_skills.TryGetValue(skillNumber, out skill))
        {
            return skill;
        }

        return null;
    }

}
