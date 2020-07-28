using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Define
{
    public enum CameraMode
    {
        QuaterView,
    }

    public enum SkillCaster
    {
        Player,
        Enemy,
    }

    public enum Layer
    {
        Monster = 8,
        Wall = 9,
        Ground = 10,
    }

    public enum ActionState
    {
        None,
        Targeting,
        Attack,
        Defence,
    }

    public enum State
    {
        Idle,
        Moving,
        Die,
        Channeling,
        Jumping,
        Falling,
    }

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
}


