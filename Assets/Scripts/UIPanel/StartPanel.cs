using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


class MonoStub : MonoBehaviour
{

}
public class StartPanel : BasePanel
{
    private static string name = "StartPanel";
    private static string path = "Panel/StartPanel";

    public Animator transition;

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

        transition = UIFunction.GetInstance().GetOrAddComponentInChild<Animator>(ActiveObject, "Fade");
        transition.SetTrigger("Start");
        Debug.Log("Wait Start");
        //peTimer.AddTimeTask(action, 500, PETimeUnit.Millisecond, 1);
        //peTimer.SetHandle((Action<int> cb, int tid) => {
        //    //覆盖默认的回调处理
        //    ProScene proScene = new ProScene();
        //    Debug.Log("Wait End?");
        //    GameRoot.GetInstance().sceneControlRoot.LoadScene(proScene.SceneName, proScene);
        //});
        new GameObject().AddComponent<MonoStub>().StartCoroutine(Fade());
        Debug.Log("Wait End");

    }

    IEnumerator Fade()
    {
        ProScene proScene = new ProScene();
        Debug.Log("Wait Start");
        yield return new WaitForSeconds(0.7f);
        Debug.Log("Wait End");
        GameRoot.GetInstance().sceneControlRoot.LoadScene(proScene.SceneName, proScene);
    }
}
