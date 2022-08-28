using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    private static GameRoot instance;
    private UIManager uiManager;
    private SceneControl sceneControl;

    public UIManager uiManagerRoot;
    public SceneControl sceneControlRoot { get => sceneControl; }

    public static GameRoot GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("GameRootÊµÀý²»´æÔÚ");
            return instance;
        }
        else
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        uiManager = new UIManager();
        uiManagerRoot = uiManager;
        sceneControl = new SceneControl();
    }

    private void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        //uiManagerRoot.Push(new StartPanel());

        //PlayScene playScene = new PlayScene();
        //sceneControlRoot.sceneDict.Add(playScene.SceneName, playScene);
    }
}
