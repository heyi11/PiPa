using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class VibratoOnClick : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    
   static public bool isDown = false;  
    // Start is called before the first frame update
     public void OnPointerDown(PointerEventData eventData)
    {
        print("按下！！！！");
        isDown = true;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        print("抬起！！！！");
        isDown = false;
    }
}
