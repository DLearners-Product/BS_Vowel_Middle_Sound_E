using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HNO_Start_Orbiting_ans : MonoBehaviour
{
    public GameObject star;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        this.transform.position = star.transform.position;    //Object move along the circle position without rotating
    }


	
}
