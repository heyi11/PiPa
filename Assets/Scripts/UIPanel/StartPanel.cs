using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private static string name = "StartPanel";
    private static string path = "Panel/StartPanel";

    public static readonly UIType uIType = new UIType(path, name);

    public StartPanel() : base(uIType)
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
        UIFunction.GetInstance().GetOrAddComponentInChild<Button>(ActiveObject, "OptionBtn").onClick.AddListener(GoOption);
        UIFunction.GetInstance().GetOrAddComponentInChild<Button>(ActiveObject, "ProBtn").onClick.AddListener(GoPro);

        GameRoot.GetInstance().uiManagerRoot.Push(new HelpPanel());
    }

    private void GoOption()
    {
        GameRoot.GetInstance().uiManagerRoot.Push(new OptionPanel());
    }

    private void GoPro()
    {
        ProScene proScene = new ProScene();
        GameRoot.GetInstance().sceneControlRoot.LoadScene(proScene.SceneName, proScene);
    }
}
