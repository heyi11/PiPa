using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel
{
    public UIType uiType;
    public GameObject ActiveObject; // panel在场景中对应的物体

    public BasePanel(UIType uitype)
    {
        uiType = uitype;
    }

    public virtual void onStart()
    {
        Debug.Log($"{uiType.Name}启动拉");
        if (ActiveObject.GetComponent<CanvasGroup>() == null)
        {
            ActiveObject.AddComponent<CanvasGroup>();
        }
    }

    public virtual void onEnable()
    {
        Debug.Log($"{uiType.Name}在线");
        UIFunction.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObject).interactable = true;

    }

    public virtual void onDisable()
    {
        Debug.Log($"{uiType.Name}关了");
        UIFunction.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObject).interactable = false;
    }

    public virtual void onDestroy()
    {
        Debug.Log($"{uiType.Name}再见");
        UIFunction.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObject).interactable = false;
    }

}
