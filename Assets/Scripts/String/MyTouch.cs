using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyTouch : MonoBehaviour
{
    Ray ray;
    RaycastHit hitInfo;
    Transform hitTrans;
    Vector2 mousePos;
    enum TouchStat
    {
        NONE,
        TAP,
        SLIDE,

    };
    /// <summary>  
    /// 定义的一个手指类  
    /// </summary>  
    class MyFinger
    {
        public int id = -1;
        public Touch touch;
        public Transform touchTrans;
        public TouchStat touchStat;
        public bool isTouching;
        public bool isDragging;
        public LayerMask mask;
        /// <summary>  
        /// 重置类成员
        /// </summary>  
        public void Reset()
        {
            this.id=-1;
            this.touchStat = TouchStat.NONE;
            this.isDragging = false;
            this.isTouching = false;
            this.touchTrans = null;
            this.mask=LayerMask.GetMask("String1", "String2", "String3", "String4");
        }
        public void ResetMask()
        {
            this.mask = LayerMask.GetMask("String1", "String2", "String3", "String4");
        }

        static private List<MyFinger> fingers = new List<MyFinger>();
        /// <summary>  
        /// 手指容器  
        /// </summary>  
        static public List<MyFinger> Fingers
        {
            get
            {
                if (fingers.Count == 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        MyFinger mf = new MyFinger();
                        mf.id = -1;
                        mf.touchTrans = null;
                        mf.touchStat = TouchStat.NONE;
                        mf.isTouching=false;
                        mf.isDragging=false;
                        mf.mask= LayerMask.GetMask("String1", "String2", "String3", "String4");
                        fingers.Add(mf);
                    }
                }
                return fingers;
            }
        }
    }

    // 存储注册进来的Touch 物体
    public List<GameObject> myTouchs;

    void Update()
    {
        Touch[] touches = Input.touches;

        // 遍历所有的已经记录的手指  
        // --掦除已经不存在的手指  
        foreach (MyFinger mf in MyFinger.Fingers)
        {
            if (mf.id == -1)
            {
                continue;
            }
            bool stillExit = false;
            foreach (Touch t in touches)
            {
                if (mf.id == t.fingerId)
                {
                    stillExit = true;
                    break;
                }
            }
            // 掦除  
            if (stillExit == false)
            {
                mf.id = -1;
                mf.touchTrans = null;
            }
        }
        // 遍历当前的touches  
        // --并检查它们在是否已经记录在AllFinger中  
        // --是的话更新对应手指的状态，不是的加进去  
        foreach (Touch t in touches)
        {
            bool stillExit = false;
            // 存在--更新对应的手指  
            foreach (MyFinger mf in MyFinger.Fingers)
            {
                if (t.fingerId == mf.id)
                {
                    stillExit = true;
                    mf.touch = t;
                    break;
                }
            }
            // 不存在--添加新记录  
            if (!stillExit)
            {
                foreach (MyFinger mf in MyFinger.Fingers)
                {
                    if (mf.id == -1)
                    {
                        mf.id = t.fingerId;
                        mf.touch = t;
                        break;
                    }
                }
            }
        }

        // 记录完手指信息后，就是响应相应和状态记录了  
        for (int i = 0; i < MyFinger.Fingers.Count; i++)
        {

            MyFinger mf = MyFinger.Fingers[i];
            if (mf.id != -1)
            {
                mousePos = mf.touch.position;
                // Debug.Log("触摸产生");
                ray = Camera.main.ScreenPointToRay(mousePos);
                bool ishit=Physics.Raycast(ray, out hitInfo,1000f,mf.mask);
                // Debug.Log($"击中:{ishit}");
                // Debug.Log($"mask:{mf.mask.value}");
                if (!ishit||!myTouchs.Contains(hitInfo.collider.transform.gameObject))
                {
                    // Debug.Log("空移动！");
                    if(mf.isTouching){
                        mf.touchTrans.GetComponent<TouchItem>().OnTouchMovingEnd(mf.touch,mf.id);
                        mf.ResetMask();
                        mf.isTouching=false;
                        // Debug.Log("移动结束");
                    }
                    else if(mf.isDragging){
                        mf.touchTrans.GetComponent<TouchItem>().OnTouchDrag(mf.touch,mf.id);
                        //mf.touchStat=TouchStat.SLIDE;
                        //mf.ResetMask();
                        //mf.isDragging=false;
                        // Debug.Log("拖拽结束");
                    }
                    if(mf.touchTrans!=null){
                        if (mf.touch.phase == TouchPhase.Ended){
                            if(mf.touchStat==TouchStat.TAP)
                            {
                                mf.touchTrans.GetComponent<TouchItem>().OnTouchDragEnd(mf.touch,mf.id);
                            }
                            else if (mf.touchStat==TouchStat.SLIDE)
                            {
                                mf.touchTrans.GetComponent<TouchItem>().OnTouchMovingEnd(mf.touch,mf.id);
                            }
                            // Debug.Log("触摸结束1");
                            mf.Reset();
                        }
                    }
                    else
                    {
                        if (mf.touch.phase == TouchPhase.Ended)
                        {
                            mf.Reset();
                            // Debug.Log("空移动结束！");
                        }
                        
                    }
                    continue;
                }
                hitTrans = hitInfo.collider.transform;
                // Debug.Log($"transform:{hitTrans.name}");
                if(!myTouchs.Contains(hitTrans.gameObject)){
                    continue;
                }
                mf.mask = 1<<hitInfo.collider.gameObject.layer;
                // Debug.Log($"mf.mask:{mf.mask.value}");
                if (mf.touch.phase == TouchPhase.Began)
                {
                    mf.touchStat=TouchStat.TAP;
                    mf.touchTrans=hitTrans;
                    // Debug.Log("开始触摸");
                    mf.touchTrans.GetComponent<TouchItem>().OnTouchBegin(mf.touch,mf.id);

                }
                else if (mf.touch.phase == TouchPhase.Stationary)
                {
                    if (mf.touchStat == TouchStat.TAP)
                    {
                        if (!mf.isDragging)
                        {
                            mf.touchTrans.GetComponent<TouchItem>().OnTouchHold(mf.touch, mf.id);
                        }
                        else
                        {
                            mf.touchTrans.GetComponent<TouchItem>().OnTouchDragHold(mf.touch, mf.id);
                        }
                    }
                }
                else if (mf.touch.phase == TouchPhase.Moved)
                {
                    if(mf.touchStat==TouchStat.TAP){
                        mf.isDragging=true;
                        mf.touchTrans.GetComponent<TouchItem>().OnTouchDrag(mf.touch,mf.id);
                    }
                    else{
                        mf.touchStat=TouchStat.SLIDE;
                        mf.touchTrans=hitTrans;
                        mf.isTouching=true;
                        mf.touchTrans.GetComponent<TouchItem>().OnTouchMoving(mf.touch,mf.id);
                    }
                    // Debug.Log("触摸移动");
                }
                else if (mf.touch.phase == TouchPhase.Ended)
                {
                    if(mf.touchStat==TouchStat.TAP){
                        if(mf.isDragging){
                            mf.touchTrans.GetComponent<TouchItem>().OnTouchDragEnd(mf.touch,mf.id);
                        }
                        else
                        {
                            mf.touchTrans.GetComponent<TouchItem>().OnTapEnd(mf.touch, mf.id);
                        }
                    }
                    else if (mf.touchStat==TouchStat.SLIDE){
                        mf.touchTrans.GetComponent<TouchItem>().OnTouchMovingEnd(mf.touch,mf.id);
                    }
                    // Debug.Log("触摸结束2");
                    mf.Reset();
                }

            }
        }
    }


    // EventSystem eventSystem;
    // public GraphicRaycaster RaycastInCanvas;//Canvas上有这个组件
    // Transform CheckGuiRaycastObjects(Vector3 point)
    // {
    //     PointerEventData eventData = new PointerEventData(eventSystem);
    //     eventData.pressPosition = point;
    //     eventData.position = point;
    //     List<RaycastResult> list = new List<RaycastResult>();
    //     RaycastInCanvas.Raycast(eventData, list);
    //     Transform thistrnas = null;
    //     if (list.Count > 0)
    //     {
    //         for (int i = 0; i < list.Count; i++)
    //         {
    //             if (list[i].gameObject.tag == "Controller")
    //             {
    //                     thistrnas = list[i].gameObject.transform;

    //                 break;
    //             }
    //         }

    //     }
    //     return thistrnas;
    // }

    // public Vector3 GetWorldPos(Vector2 screenPos)
    // {
    //     return Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.nearClipPlane + 10));
    // }
}