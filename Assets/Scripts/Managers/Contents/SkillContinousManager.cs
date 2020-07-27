using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillContinousManager
{
    public Queue<IEnumerator> SkillContinousAction;

    private void Init()
    {
        if (SkillContinousAction == null)
            SkillContinousAction = new Queue<IEnumerator>();
    }

    public void Push(IEnumerator enumerator)
    {
        Init();
        SkillContinousAction.Enqueue(enumerator);
    }

    public IEnumerator GetEnumerator()
    {
        Init();

        if (SkillContinousAction.Count == 0)
            return null;

        return SkillContinousAction.Dequeue();
    }
}
