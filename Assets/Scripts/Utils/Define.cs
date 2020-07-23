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

    public enum Layer
    {
        Monster = 8,
        Wall = 9,
        Ground = 10,
    }

    public enum PlayerState
    {
        Idle,
        Moving,
        Die,
        Channeling,
        Jumping,
        Falling,
        Sturn,
        Poison,
        Bleeding,
        Knockback,
    }
}


