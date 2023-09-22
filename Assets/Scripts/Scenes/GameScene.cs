using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Inven>();

        for (int i = 0; i < 3; i++)
        {
            Managers.Resource.Instantiate("UnityChan");
            Managers.Resource.Instantiate("Tank");
        }
    }

    public override void Clear()
    {
        
    }
}
