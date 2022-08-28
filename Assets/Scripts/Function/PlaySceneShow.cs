using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneShow : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        PlayScene playScene = new PlayScene();
        GameRoot.GetInstance().uiManagerRoot.Push(new StartPanel());
        GameRoot.GetInstance().sceneControlRoot.sceneDict.Add(playScene.SceneName, playScene);
        Debug.Log("Push Start Panel");
    }
}
