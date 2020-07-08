using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Define
{
    public enum MouseEvent 
    {
        Press,
        Click,
    }

    public enum CameraMode
    {
        QuaterView,
    }

    public enum Layer
    {
        Monster = 8,
        Wall = 9,
    }

    public enum MouseButton
    {
        Left = 0,
        Right = 1,
        Wheel = 2,
    }
}


