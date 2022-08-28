using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : SceneBase
{
    public readonly string SceneName = "PlayScene";
    public override void EnterScene()
    {
        PlayScene playScene = new PlayScene();
        GameRoot.GetInstance().uiManagerRoot.Push(new StartPanel());
        Debug.Log("Push Start Panel");
    }

    public override void ExitScene()
    {
        
    }
}
