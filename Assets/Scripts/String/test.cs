using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update

    
    void Start()
    {
        // transform为人物模型的MeshRenderer的transform
        var size = transform.GetComponent<Renderer>().bounds.size;
        Debug.Log ("x: " + size.x + ",y: " + size.y+",z:"+size.z);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
