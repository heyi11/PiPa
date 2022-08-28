using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TouchDetector : MonoBehaviour
{
    private Vector2 multiPosition1=new Vector2();
    private Vector2 multiPosition2=new Vector2();
    Vector2 m_screenPos = new Vector2();
    public SimpleVibration sv;
    public Text touchInfo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount <= 0)
        return;

        if(Input.touchCount==1)
        {
            if (Input.touches[0].phase == TouchPhase.Began||Input.touches[0].phase == TouchPhase.Moved)
            {
                m_screenPos = Input.touches[0].position;

                Ray ray = Camera.main.ScreenPointToRay(m_screenPos);

 
                RaycastHit hitInfo = new RaycastHit();

                if(Physics.Raycast(ray,out hitInfo))
                {
                    if(hitInfo.collider.transform.name==transform.name)
                    {
                        sv.Trigger();
                    }
                }
            }
        }

        else if (Input.touchCount >1)
        {

            for(int i=0; i<2;i++)
            {
                Touch touch = Input.touches[i];
                if (touch.phase == TouchPhase.Began||touch.phase ==TouchPhase.Moved)
                {
                    if(i==0)
                    {
                        multiPosition1=touch.position;
                        Ray ray = Camera.main.ScreenPointToRay(multiPosition1);

                        RaycastHit hitInfo = new RaycastHit();

                        if(Physics.Raycast(ray,out hitInfo))
                        {
                            if(hitInfo.collider.transform.name==transform.name)
                            {
                                sv.Trigger();
                            }
                        }
                    }
                    else
                    {
                        multiPosition2=touch.position;
                        Ray ray = Camera.main.ScreenPointToRay(multiPosition2);

                        RaycastHit hitInfo = new RaycastHit();

                        if(Physics.Raycast(ray,out hitInfo))
                        {
                            if(hitInfo.collider.transform.name==transform.name)
                            {
                                sv.Trigger();
                            }
                        }

                    }
                }
            }

        }
        touchInfo.text="触摸点数："+Input.touchCount;
    }
}
