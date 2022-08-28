using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanel : BasePanel
{
    private static string name = "HelpPanel";
    private static string path = "Panel/HelpPanel";

    public static readonly UIType uIType = new UIType(path, name);

    public HelpPanel() : base(uIType)
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
        UIFunction.GetInstance().GetOrAddComponentInChild<Button>(ActiveObject, "ReturnButton").onClick.AddListener(GoMain);
    }

    private void GoMain()
    {
        GameRoot.GetInstance().uiManagerRoot.Pop();
    }

}
