using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 length = GetComponent<MeshFilter>().mesh.bounds.size;
        float xlength = length.x * transform.lossyScale.x;
        float ylength = length.y * transform.lossyScale.y;
        float zlength = length.z * transform.lossyScale.z;
        Debug.Log(xlength);
        Debug.Log(ylength);
        Debug.Log(zlength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
