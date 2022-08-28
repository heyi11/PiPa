using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TouchItem : MonoBehaviour
{
    static int [] id_index = new int[4]{-1,-1,-1,-1};
    static bool [] isVabrito = new bool[2]{false,false};
    static float cdis;
    public AK.Wwise.Event stringPick1;
    public AK.Wwise.RTPC  play_index;
    public AK.Wwise.RTPC  strength;
    public AK.Wwise.RTPC  change_distance;
    public AK.Wwise.Event Vibrato;
    public AK.Wwise.Event Vacant;
    public float startTime;
    public float endTime;
    GameObject getVib;

    public Button isVib;

    [Tooltip("Drag阈值")]
    [Range(0.1f, 200f)]
    public float dragLimit = 1f;


    [Tooltip("触发器id")]
    [Range(1, 30)]
    public int triggerId=1;



    [Tooltip("弦id")]
    [Range(1, 4)]
    public int stringId=1;
    

    /// <summary>  
    /// 触发器类别枚举
    /// </summary>
    public enum TriggerType
    {
        LEFTHAND,
        RIGHTHAND,
    }
  
    
    /// <summary>  
    /// 触发器类别：左手/右手
    /// </summary>
    public TriggerType triggerType;
    // public Text text_left;
    // public Text text_right;

    /// <summary>  
    /// 滑动方向枚举
    /// </summary>
    public enum Direction
    {
        NONE,
        UP,
        DOWN,
        LEFT,
        RIGHT,
    }
    /// <summary>  
    /// 有关触摸的相关信息类
    /// </summary>
    public class TouchInfo
    {
        //id👇
        public int _id = -1;
        //信息，给Text用的👇
        public string info;
        //开始时间和结束时间👇
        public float firstTime;
        public float lastTime;
        //时间戳

        //触摸起始位置和结束位置👇
        public Vector3 TouchBegin = Vector3.zero;
        public Vector3 TouchEnd = Vector3.zero;
        //滑动方向👇
        public Direction direction = Direction.NONE;
        // private static List<TouchInfo> touchlist = new List<TouchInfo>();
        //构造函数👇
        public TouchInfo(int id)
        {
            _id = id;
        }
    }
    /// <summary>  
    /// 振动执行实例
    /// </summary>
    public SimpleVibration sv;

    MyTouch myTouch;
    public List<TouchInfo> touchlist = new List<TouchInfo>();

    private void Awake()
    {
        myTouch = GameObject.FindObjectOfType<MyTouch>();
    }

    private void Start()
    {
        //text_right = GameObject.FindWithTag("Respawn").GetComponent<Text>();
        myTouch.myTouchs.Add(this.gameObject);
         //if (triggerType == TriggerType.RIGHTHAND)
         //{
         //    StartCoroutine(showText(text_right));
         //}
        // else
        // {
        //     StartCoroutine(showText(text_left));
        // }
        touchlist = new List<TouchInfo>();
        getVib = GameObject.FindGameObjectWithTag("VibratoButton");

        
        

    }
    /// <summary>  
    /// 被单击
    /// </summary>
    public void OnTapEnd(Touch touch, int id)
    {
        TouchInfo touchInfo = GetTouch(id);
        if (touchInfo != null)
        {
            touchInfo.lastTime=Time.time;
            DoSV();
            DoTapEnd(touchInfo);
            if(triggerType==TriggerType.LEFTHAND){
                sv.TriggerLeave();
            }
            TouchDelete(touchInfo);
        }
        // Debug.Log("OnTapEnd");
        // Debug.Log($"TouchInfoList Count:{touchlist.Count}");
    }
    /// <summary>  
    /// 触摸开始
    /// </summary>
    public void OnTouchBegin(Touch touch, int id)
    {
        TouchInfo touchInfo = GetTouch(id);
        if (touchInfo == null)
        {
            // Debug.Log("OnTouchBegin");
            touchInfo = new TouchInfo(id);
            touchInfo.TouchBegin = touch.position;
            touchInfo.firstTime=Time.time;
            touchInfo.info = "OnTouchBegin";
            if(triggerType==TriggerType.LEFTHAND){
                sv.TriggerLimZSet(transModelPosZ(transform.localPosition.z));
                
                id_index[stringId-1] = triggerId;
                //Debug.Log("triggerId"+triggerId);
                
            }
            //介4个嘛？？
            TouchAdd(touchInfo);
            if(triggerType == TriggerType.RIGHTHAND){
                startTime = Time.time;
                //Debug.Log("我开始啦"+startTime);
            }
            
        }
        // Debug.Log($"TouchInfoList Count:{touchlist.Count}");
    }
    /// <summary>  
    /// 触摸静止，点按途中
    /// </summary>

    public void OnTouchHold(Touch touch, int id)
    {
        TouchInfo touchInfo = GetTouch(id);
        if (touchInfo != null)
        {
            // Debug.Log("OnTouchHold");
            //touchInfo.TouchEnd = touch.position;
            touchInfo.TouchEnd = touch.position;
            Vector2 dis = touchInfo.TouchEnd - touchInfo.TouchBegin;
            touchInfo.info = "OnTouchHold";
            if (triggerType == TriggerType.LEFTHAND)
            {
                id_index[stringId-1] = triggerId;
                //Debug.Log("triggerId HOld"+triggerId);
                //sv.TriggerLimZSet(transModelPosZ(transform.localPosition.z));
            }
        }
        // Debug.Log($"TouchInfoList Count:{touchlist.Count}");
    }
    float transModelPosZ(float localZ)
    {
        return (localZ - 17.5f) * (-0.109f - 0.419f - (-1.03f + 0.134f)) / (-14.04f - 17.5f) + (-1.03f + 0.134f);
    }
    /// <summary>  
    /// 拖拽时静止
    /// </summary>
    public void OnTouchDragHold(Touch touch, int id)
    {
        TouchInfo touchInfo = GetTouch(id);
        if (touchInfo != null)
        {
            // Debug.Log("OnTouchDragHold");
            touchInfo.TouchEnd = touch.position;
            Vector2 dis = touchInfo.TouchEnd - touchInfo.TouchBegin;
            touchInfo.info = "OnTouchDragHold";
            if (triggerType == TriggerType.LEFTHAND)
            {
                //sv.TriggerLimZSet(transModelPosZ(transform.localPosition.z));
            }
        }
        // Debug.Log($"TouchInfoList Count:{touchlist.Count}");
    }

    /// <summary>  
    /// 正在拖拽
    /// </summary>
    public void OnTouchDrag(Touch touch, int id)
    {
        TouchInfo touchInfo = GetTouch(id);
        if (touchInfo != null)
        {
            // Debug.Log("OnTouchDrag");
            touchInfo.TouchEnd = touch.position;
            Vector2 dis = touchInfo.TouchEnd - touchInfo.TouchBegin;
            //拖拽小于一定距离，表示不动
            if(Mathf.Abs(dis.y)<dragLimit)
            {
                OnTouchHold(touch,id);
                return;
            }
            touchInfo.info = "OnTouchDragDistance:" + dis.y;
            DoDragging(touchInfo);
            if(triggerType==TriggerType.LEFTHAND){
                
                //sv.TriggerLimZSet(transModelPosZ(transform.localPosition.z));
                sv.OnStringDragging(dis.y, transModelPosZ(transform.localPosition.z));
                //Debug.Log("transform.position.y"+((transform.localPosition.z-17.5f)*(-0.109f-0.419f-(-1.03f+0.134f))/(-14.04f-17.5f)+(-1.03f+0.134f)));
                sv.setRoot();
                id_index[stringId-1] = triggerId;
            }
        }
        // Debug.Log($"TouchInfoList Count:{touchlist.Count}");
    }
    /// <summary>  
    /// 拖拽结束
    /// </summary>
    public void OnTouchDragEnd(Touch touch, int id)
    {
        TouchInfo touchInfo = GetTouch(id);

        if (touchInfo != null)
        {
            // Debug.Log("OnTouchDragEnd");
            touchInfo.TouchEnd = touch.position;
            DoSV();
            touchInfo.info = "OnTouchDragEnd";
            TouchDelete(touchInfo);
            if(triggerType==TriggerType.LEFTHAND){
                sv.TriggerLeave();
                StartCoroutine(sv.OnStringDragEnd());
                sv.leaveRoot();
                change_distance.SetGlobalValue(0);
                id_index[stringId-1] =-1;
                Debug.Log("END");
                isVabrito[0] = false;
            }
            else{
                endTime = Time.time;
                //Debug.Log("OnTouchDragEnd我结束啦"+endTime);
                Debug.Log("Time"+(endTime-startTime));
                float deltatime = endTime-startTime;
                strength.SetGlobalValue(deltatime);
                if(id_index[stringId-1]!=-1 ){
                    //isVabrito[1] =  getVib.GetComponent<VibratoOnClick>().isDown;
                    isVabrito[1] = VibratoOnClick.isDown;
                    if(isVabrito[0]==true && isVabrito[1]==true)
                    {
                        Vibrato.Post(gameObject);
                        Debug.Log("揉弦");

                    }
                    else
                    {
                        stringPick1.Post(gameObject);
                    }
                
                    
                    //Debug.Log("TouchDrag我进来了"+(id_index[stringId-1]-5));
                    play_index.SetGlobalValue(id_index[stringId-1]-5);
                
                
                    //AkSoundEngine.PostEvent("stringPick1",string1);
                    //Debug.Log(id_index[stringId-1]-5);

                }
            
                else{
                    Vacant.Post(gameObject);
                }
            }
        }
        // Debug.Log($"TouchInfoList Count:{touchlist.Count}");
    }

    /// <summary>  
    /// 正在滑动
    /// </summary>
    public void OnTouchMoving(Touch touch, int id)
    {
        TouchInfo touchInfo = GetTouch(id);
        if (touchInfo != null)
        {   //第n次
            touchInfo.TouchEnd = touch.position;
            CalDirection(touchInfo);
            touchInfo.lastTime=Time.time;
            DoMoving(touchInfo);
            float t1=(touchInfo.lastTime-touchInfo.firstTime);
            touchInfo.info = "OnTouchMoving\nDirection:" + touchInfo.direction
            +"\ntime:"+t1+"\nspeed:"+((touchInfo.TouchEnd.y-touchInfo.TouchBegin.y)/t1);
        }
        else
        {   //第一次
            // Debug.Log("OnTouchMoving");
            touchInfo = new TouchInfo(id);
            touchInfo.TouchBegin = touch.position;
            touchInfo.firstTime=Time.time;
            touchInfo.info = "OnTouchMoving";
            TouchAdd(touchInfo);
        }
        
        // Debug.Log($"TouchInfoList Count:{touchlist.Count}");
    }
    /// <summary>  
    /// 滑动结束
    /// </summary>
    public void OnTouchMovingEnd(Touch touch, int id)
    {
        TouchInfo touchInfo = GetTouch(id);
        if (touchInfo != null)
        {
            // Debug.Log("OnTouchMovingEnd");
            DoSV();
            TouchDelete(touchInfo);
        }
        // Debug.Log($"TouchInfoList Count:{touchlist.Count}");
    }

    static Direction detectDire(Vector3 mousePosUp, Vector3 mousePosDown)
    {
        Vector3 vec = mousePosUp - mousePosDown;
        if (vec.magnitude > 2f)
        {
            float angleY = Mathf.Acos(Vector3.Dot(Vector3.up, vec.normalized)) * Mathf.Rad2Deg;
            float angleX = Mathf.Acos(Vector3.Dot(Vector3.right, vec.normalized)) * Mathf.Rad2Deg;
            // Debug.Log("angleY:" + angleY);
            // Debug.Log("detected");
            if (angleY <= 45)
            {   //↑
                return Direction.UP;

            }
            else if (angleY >= 135)
            {   //↓
                return Direction.DOWN;

            }
            else if (angleX < 45)
            {   //→
                // isDown2Up = false;
                // isUp2Down = false;
                return Direction.RIGHT;
            }
            else if (angleX >= 135)
            {   //←
                // isDown2Up = false;
                // isUp2Down = false;
                return Direction.LEFT;
            }
            else
            {
                return Direction.NONE;
            }

        }
        else
        {
            return Direction.NONE;
        }


    }


    void DoSV()
    {
        if (triggerType == TriggerType.LEFTHAND)
        {
            // sv.TriggerLimZSet(triggerId);
        }
        else
        {
            sv.Trigger();
        }
    }
    public void DoTapEnd(TouchInfo touchInfo){
        /*
        if(Input.touchCount == 1){
            if(triggerType==TriggerType.RIGHTHAND){
                play_index.SetGlobalValue(0);
                AkSoundEngine.PostEvent("stringPick1",string1);
            }
        }
        if(Input.touchCount == 2){
            play_index.SetGlobalValue(5);
            Debug.Log(triggerId);
        }
        */

        if(triggerType==TriggerType.LEFTHAND)
        {//左手

            
            //Debug.Log("left");
            
        
            id_index[stringId-1] = -1;
        }
        else
        {//右手
           //string1 = GameObject.Find("CubeR");
            //
            endTime = Time.time;
            //Debug.Log("OnTapEnd我结束啦"+endTime);
            Debug.Log("Time"+(endTime-startTime));
            float deltatime = endTime-startTime;
            strength.SetGlobalValue(deltatime);
            if(id_index[stringId-1]!=-1 ){
                    //isVabrito[1] =  getVib.GetComponent<VibratoOnClick>().isDown;
                    isVabrito[1] = VibratoOnClick.isDown;
                    if(isVabrito[0]==true && isVabrito[1]==true)
                    {
                        Vibrato.Post(gameObject);
                        Debug.Log("揉弦");

                    }
                    else
                    {
                        stringPick1.Post(gameObject);
                    }
                //stringPick1.Post(gameObject);
                //Debug.Log("我进来了"+(id_index[stringId-1]-5));
                play_index.SetGlobalValue(id_index[stringId-1]-5);
                
                
                //AkSoundEngine.PostEvent("stringPick1",string1);
                //Debug.Log(id_index[stringId-1]-5);


            }
            
            else{
                Vacant.Post(gameObject);
            }
            
            
        }
    }
    public void DoMoving(TouchInfo touchInfo){
        if(triggerType==TriggerType.LEFTHAND)
        {//左手
            
        }
        else
         {//右手
              
        

            if(id_index[stringId-1]!=-1 ){
                    //isVabrito[1] =  getVib.GetComponent<VibratoOnClick>().isDown;
                    isVabrito[1] = VibratoOnClick.isDown;
                    if(isVabrito[0]==true && isVabrito[1]==true)
                    {
                        Vibrato.Post(gameObject);
                        Debug.Log("揉弦");

                    }
                    else
                    {
                        stringPick1.Post(gameObject);
                    }
                //stringPick1.Post(gameObject);
                //Debug.Log("我进来了"+(id_index[stringId-1]-5));
                play_index.SetGlobalValue(id_index[stringId-1]-5);
                
                
                //AkSoundEngine.PostEvent("stringPick1",string1);
                //Debug.Log(id_index[stringId-1]-5);


            }
            
            else{
                Vacant.Post(gameObject);
            }
        }
    }
    public void DoDragging(TouchInfo touchInfo){
        if(triggerType==TriggerType.LEFTHAND)
        {//左手
            play_index.SetGlobalValue(id_index[stringId-1]-5);
            cdis = Math.Abs(touchInfo.TouchEnd.y - touchInfo.TouchBegin.y);
            
            change_distance.SetGlobalValue(cdis/150);
            //Debug.Log("dis"+cdis/150);
            //判断是否onclick，如果是，播放揉弦的SetGlobalValue音频
            isVabrito[0] = true;
            
            
        }
        else
        {//右手


        }
    }



    public void TouchAdd(TouchInfo touchInfo)
    {
        touchlist.Add(touchInfo);
    }
    public void TouchDelete(TouchInfo touchInfo)
    {

        touchlist.Remove(touchInfo);

    }
    public void CalDirection(TouchInfo touchInfo)
    {
        if (!touchInfo.TouchBegin.Equals(Vector3.zero) && !touchInfo.TouchEnd.Equals(Vector3.zero))
        {
            touchInfo.direction = detectDire(touchInfo.TouchEnd, touchInfo.TouchBegin);
        }
    }
    public TouchInfo GetTouch(int id)
    {
        return touchlist.Find(TouchInfo => TouchInfo._id == id);
    }




    IEnumerator showText(Text Text)
    {
        string text;
        if (Text == null)
        {

            StopCoroutine(showText(Text));
        }
        while (true)
        {
            text = $"Type:{triggerType}\nTriggerId:{triggerId}\nTouch Num:{touchlist.Count}\n";
            foreach (TouchInfo touchinfo in touchlist)
            {
                // Debug.Log($"id:{touchinfo._id}\tinfo:{touchinfo.info}\n");
                text += $"id:{touchinfo._id}\tinfo:{touchinfo.info}\n";
            }
            Text.text = text;

            yield return new WaitUntil(() => touchlist.Count > 0);
        }
    }
}
