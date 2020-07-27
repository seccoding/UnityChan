using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills
{
    static Dictionary<float, Skill> _skills;

    private static void Init()
    {
        _skills = new Dictionary<float, Skill>();
        _skills.Add(0f, new Default()) ;
        _skills.Add(1f, new Hammering());
        _skills.Add(2f, new PoisonArrow());
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
