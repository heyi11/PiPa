using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProPanel : BasePanel
{
    private static string name = "ProPanel";
    private static string path = "Panel/ProPanel";
    private Camera camera;
    private int buttonPushed = 0;

    public static readonly UIType uIType = new UIType(path, name);

    public ProPanel() : base(uIType)
    {
    }

    public override void onDestroy()
    {
        base.onDestroy();
    }

    public override void onDisable()
    {
        base.onDisable();
    }

    public override void onEnable()
    {
        base.onEnable();
    }

    public override void onStart()
    {
        base.onStart();
        UIFunction.GetInstance().GetOrAddComponentInChild<Button>(ActiveObject, "BackBtn").onClick.AddListener(GoBack);
        UIFunction.GetInstance().GetOrAddComponentInChild<Button>(ActiveObject, "Area01").onClick.AddListener(ChangeAreaOne);
        UIFunction.GetInstance().GetOrAddComponentInChild<Button>(ActiveObject, "Area02").onClick.AddListener(ChangeAreaTwo);
        UIFunction.GetInstance().GetOrAddComponentInChild<Button>(ActiveObject, "Area03").onClick.AddListener(ChangeAreaThree);
        UIFunction.GetInstance().GetOrAddComponentInChild<Button>(ActiveObject, "Area04").onClick.AddListener(ChangeAreaFour);
    }

    private void GoBack()
    {
        PlayScene playScene = new PlayScene();
        GameRoot.GetInstance().sceneControlRoot.LoadScene(playScene.SceneName, playScene);
    }

    private void ChangeAreaOne()
    {
        if(buttonPushed != 1)
        {
            Debug.Log("你按下了按钮01");
            buttonPushed = 1;
            camera = GameObject.FindObjectsOfType<Camera>()[1];
            Debug.Log($"找到了{camera.name}");
            camera.transform.localPosition = new Vector3(-84.36f, 273.1f, 3.5f);
        }
        
    }
    private void ChangeAreaTwo()
    {
        if (buttonPushed != 2)
        {
            Debug.Log("你按下了按钮01");
            buttonPushed = 2;
            camera = GameObject.FindObjectsOfType<Camera>()[1];
            Debug.Log($"找到了{camera.name}");
            camera.transform.localPosition = new Vector3(-84.36f, 220.8f, 3.5f);
        }

    }
    private void ChangeAreaThree()
    {
        if (buttonPushed != 3)
        {
            Debug.Log("你按下了按钮01");
            buttonPushed = 3;
            camera = GameObject.FindObjectsOfType<Camera>()[1];
            Debug.Log($"找到了{camera.name}");
            camera.transform.localPosition = new Vector3(-84.36f, 166f, 3.5f);
        }

    }
    private void ChangeAreaFour()
    {
        if (buttonPushed != 4)
        {
            Debug.Log("你按下了按钮01");
            buttonPushed = 4;
            camera = GameObject.FindObjectsOfType<Camera>()[1];
            Debug.Log($"找到了{camera.name}");
            camera.transform.localPosition = new Vector3(-84.36f, 115f, 3.5f);
        }

    }
}
