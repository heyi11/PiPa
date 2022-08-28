using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProScene : SceneBase
{
    public readonly string SceneName = "ProScene";
    public override void EnterScene()
    {
        GameRoot.GetInstance().uiManagerRoot.Push(new ProPanel());
        Debug.Log("Push Pro Panel");
    }

    public override void ExitScene()
    {
        //GameRoot.GetInstance().uiManagerRoot.PopAll();
        //Debug.Log("ÎªÊ²Ã´²»pop????");
    }
}
