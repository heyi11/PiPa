using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Text;
using System;
using UnityEngine.UI;

public class ArduinoConnect : MonoBehaviour
{
    //���������Ϣ
    string portName_1 = "COM4";
    int baudRate = 9600;
    //Parity parity = Parity.None;
    int dataBits = 8;
    //StopBits stopBits = StopBits.One;

    //SerialPort serialPort_1 = null;
  
    public AudioSource TestSound;

    void Start()
    {
        OpenPort();

    }

    // Update is called once per frame
    void Update()
    {
        ReadData();
      //  if (isClosePort != 1) ClosePort();
    }

    public void OpenPort()
    {
        //serialPort_1 = new SerialPort(portName_1, baudRate, parity, dataBits, stopBits);

        try
        {
            //serialPort_1.Open();
           
            Debug.Log("Open port success.");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }

    public void ClosePort()
    {
        try
        {
            //serialPort_1.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }


    //��ȡ����
    public void ReadData()
    {
        //if (serialPort_1.IsOpen)
        //{
        //    string a = serialPort_1.ReadExisting();
        //    print(a);
        //    /*  if (int.Parse(a) > 200)
        //      {
        //          TestSound.gameObject.SetActive(true);
        //         // Volume(a);

        //      }
        //      else
        //      {
        //          TestSound.gameObject.SetActive(false);
        //      }*/
        //    TestSound.volume = float.Parse(a)/50;
        //    Thread.Sleep(200);
        //}
        //else
        //{
        //    Debug.Log("Port is not opened");
        //}
    }


//��������������
    public void Volume(string touch)
    {
         int i= int.Parse(touch);
         if(i>1000)
                {
                    i = 1000;
                }
         TestSound.volume = i/1000;
     }




    


}