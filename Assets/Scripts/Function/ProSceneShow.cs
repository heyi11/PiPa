using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProSceneShow : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
    }
}
