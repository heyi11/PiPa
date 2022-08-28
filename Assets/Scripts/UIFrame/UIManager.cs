using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public Stack<BasePanel> uiStack; // �洢panel��ջ
    public Dictionary<string, GameObject> uiDict; // �洢panel������������Ķ�Ӧ��ϵ
    public GameObject localObj;

    private static UIManager instance;

    public GameObject GetSingleObject(UIType uiType, BasePanel basePanel)
    {
        if (uiDict.ContainsKey(uiType.Name))
        {
            return uiDict[uiType.Name];
        }
        if (localObj == null)
        {
            localObj = UIFunction.GetInstance().FindCanvas();
        }
        // �ӱ��ؼ��ز�ʵ����
        GameObject gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(uiType.Path), localObj.transform);
        uiDict.Add(basePanel.uiType.Name, gameObject); 
        return gameObject;
    }

    public static UIManager GetInstance()
    {
        if(instance == null)
        {
            Debug.LogError("��ȡ��ʵ��������");
            return instance;
        }
        else
        {
            return instance;
        }
    }

    public UIManager()
    {
        instance = this;
        uiStack = new Stack<BasePanel>();
        uiDict = new Dictionary<string, GameObject>();
    }

    public void Push(BasePanel basePanel)
    {
        Debug.Log($"{basePanel.uiType.Name}��ջ");
        if(uiStack.Count > 0)
        {
            // �Ƚ�������panel��������ջ
            uiStack.Peek().onDisable();
        }
        //����
        GameObject uiObject = GetSingleObject(basePanel.uiType, basePanel);
        basePanel.ActiveObject = uiObject;

        if(uiStack.Count == 0)
        {
            uiStack.Push(basePanel);
        }
        else
        {
            if(uiStack.Peek().uiType.Name != basePanel.uiType.Name)
            {
                uiStack.Push(basePanel);
            }
        }

        basePanel.onStart();
    }

    public void PopAll()
    {
        if(uiStack.Count > 0)
        {
            uiStack.Peek().onDisable();
            uiStack.Peek().onDestroy();
            GameObject.Destroy(uiDict[uiStack.Peek().uiType.Name]);
            uiDict.Remove(uiStack.Peek().uiType.Name);
            uiStack.Pop();
            PopAll();
        }
    }

    public void Pop()
    {
        if(uiStack.Count > 0)
        {
            uiStack.Peek().onDisable();
            uiStack.Peek().onDestroy();
            GameObject.Destroy(uiDict[uiStack.Peek().uiType.Name]);
            uiDict.Remove(uiStack.Peek().uiType.Name);
            uiStack.Pop();

            if(uiStack.Count > 0)
            {
                uiStack.Peek().onEnable();
            }
        }
    }
}
