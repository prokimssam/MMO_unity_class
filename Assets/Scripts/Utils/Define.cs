using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Unknow,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }
    
    public enum CameraMode
    {
        QuaterView
    }
    
    public enum MouseEvent
    {
        Press, 
        Click
    }
    
    public enum UIEvent
    {
        Click,
        Drag
    }
}
