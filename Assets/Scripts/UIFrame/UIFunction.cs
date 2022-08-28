using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunction
{
    // 一些UI界面的功能

    // 实例化
    private static UIFunction instance;

    public static UIFunction GetInstance()
    {
        if(instance == null)
        {
            instance = new UIFunction();
        }
        return instance;
    }

    // 获得场景当中的canvas
    public GameObject FindCanvas()
    {
        GameObject gameObject = GameObject.FindObjectOfType<PlaceHolder>().gameObject;
        Debug.Log($"找到画布{gameObject}");
        Debug.Log($"找到Type{GameObject.FindObjectOfType<PlaceHolder>()}");
        if (gameObject == null)
        {
            Debug.LogError("没有找到对应的画布");
        }
        return gameObject;
    }

    // 获得场景当中子元素的名称
    public GameObject FindChild(GameObject father, string childName)
    {
        Transform[] transforms = father.GetComponentsInChildren<Transform>();
        foreach (var transName in transforms)
        {
            if(transName.gameObject.name == childName)
            {
                return transName.gameObject;
            }
        }
        Debug.LogError($"在{father.name}中没有找到{childName}");
        return null;
    }

    // 获取目标组件
    public T AddOrGetComponent<T>(GameObject gameObject) where T : Component
    {
        if (gameObject.GetComponent<T>() != null)
        {
            return gameObject.GetComponent<T>();
        }

        Debug.LogWarning($"无法在{gameObject}物体上获得目标组件！");
        return null;
    }

    // 获取子物体的目标组件
    public T GetOrAddComponentInChild<T>(GameObject father, string componentName) where T : Component
    {
        Transform[] transforms = father.GetComponentsInChildren<Transform>();

        foreach (Transform transName in transforms)
        {
            if (transName.gameObject.name == componentName)
            {
                return transName.gameObject.GetComponent<T>();
            }
        }

        Debug.LogWarning($"没有在{father.name}中找到{componentName}物体！");
        return null;
    }

}
