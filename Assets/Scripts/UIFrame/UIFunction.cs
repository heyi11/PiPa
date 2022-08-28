using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunction
{
    // һЩUI����Ĺ���

    // ʵ����
    private static UIFunction instance;

    public static UIFunction GetInstance()
    {
        if(instance == null)
        {
            instance = new UIFunction();
        }
        return instance;
    }

    // ��ó������е�canvas
    public GameObject FindCanvas()
    {
        GameObject gameObject = GameObject.FindObjectOfType<PlaceHolder>().gameObject;
        Debug.Log($"�ҵ�����{gameObject}");
        Debug.Log($"�ҵ�Type{GameObject.FindObjectOfType<PlaceHolder>()}");
        if (gameObject == null)
        {
            Debug.LogError("û���ҵ���Ӧ�Ļ���");
        }
        return gameObject;
    }

    // ��ó���������Ԫ�ص�����
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
        Debug.LogError($"��{father.name}��û���ҵ�{childName}");
        return null;
    }

    // ��ȡĿ�����
    public T AddOrGetComponent<T>(GameObject gameObject) where T : Component
    {
        if (gameObject.GetComponent<T>() != null)
        {
            return gameObject.GetComponent<T>();
        }

        Debug.LogWarning($"�޷���{gameObject}�����ϻ��Ŀ�������");
        return null;
    }

    // ��ȡ�������Ŀ�����
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

        Debug.LogWarning($"û����{father.name}���ҵ�{componentName}���壡");
        return null;
    }

}
