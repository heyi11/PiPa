using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl
{
    private static SceneControl instance;

    public Dictionary<string, SceneBase> sceneDict;

    public static SceneControl GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("SceneControl实例不存在");
            return instance;
        }
        else
        {
            return instance;
        }
    }

    public SceneControl()
    {
        sceneDict = new Dictionary<string, SceneBase>();
    }

    public void LoadScene(string sceneName, SceneBase sceneBase)
    {
        if (!sceneDict.ContainsKey(sceneName))
        {
            sceneDict.Add(sceneName, sceneBase);
        }
        if (sceneDict.ContainsKey(SceneManager.GetActiveScene().name))
        {
            sceneDict[SceneManager.GetActiveScene().name].ExitScene();
        }
        else
        {
            Debug.LogError($"SceneControl字典不包含{SceneManager.GetActiveScene().name}");
        }

        Debug.Log("Pop了所有的Panel");
        GameRoot.GetInstance().uiManagerRoot.PopAll();

        SceneManager.LoadScene(sceneName);
        sceneBase.EnterScene();
        
    }

}
