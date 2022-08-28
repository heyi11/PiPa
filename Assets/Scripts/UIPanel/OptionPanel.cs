using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : BasePanel
{
    private static string name = "OptionPanel";
    private static string path = "Panel/OptionPanel";

    public static readonly UIType uIType = new UIType(path, name);

    public OptionPanel() : base(uIType)
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
        UIFunction.GetInstance().GetOrAddComponentInChild<Button>(ActiveObject, "OptionBackBtn").onClick.AddListener(OptionBack);
    }

    private void OptionBack()
    {
        GameRoot.GetInstance().uiManagerRoot.Pop();
    }
}
