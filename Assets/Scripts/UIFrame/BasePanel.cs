using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel
{
    public UIType uiType;
    public GameObject ActiveObject; // panel�ڳ����ж�Ӧ������

    public BasePanel(UIType uitype)
    {
        uiType = uitype;
    }

    public virtual void onStart()
    {
        Debug.Log($"{uiType.Name}������");
        if (ActiveObject.GetComponent<CanvasGroup>() == null)
        {
            ActiveObject.AddComponent<CanvasGroup>();
        }
    }

    public virtual void onEnable()
    {
        Debug.Log($"{uiType.Name}����");
        UIFunction.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObject).interactable = true;

    }

    public virtual void onDisable()
    {
        Debug.Log($"{uiType.Name}����");
        UIFunction.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObject).interactable = false;
    }

    public virtual void onDestroy()
    {
        Debug.Log($"{uiType.Name}�ټ�");
        UIFunction.GetInstance().AddOrGetComponent<CanvasGroup>(ActiveObject).interactable = false;
    }

}
