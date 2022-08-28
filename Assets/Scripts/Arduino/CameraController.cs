using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float pos;
    private List<float> list = new List<float>();
    private int count = 0;
    private float sum;
    private double result;
    private double temp = 0;

    public static int avarageNum = 10;


    void Start()
    {
        for(int i=0; i<20; i++)
        {
            list.Add(0f);
        }
        Debug.Log(list);
        
    }

    // Update is called once per frame
    void Update()
    {
        // 这个pos是arduino接收到的输入数据，单位为厘米
        //pos = SerialConnect.receive; 
        Debug.Log(pos);
        // 进行进一步的平均数处理
        if (count >= avarageNum)
        {
            count = 0;
        }
        
        list[count] = pos;
        count++;
        sum = 0;
        for (int i=0; i < avarageNum; i++)
        {
            sum += list[i];
        }
        result = (((sum / avarageNum)) +45)*0.9;
        //if(Math.Abs(result-temp) > 0.2)
        //{
        string str1 = result.ToString("f3");
        transform.position = new Vector3(transform.position.x, transform.position.y, float.Parse(str1));
        //temp = result;
        //}
        
    }
}
