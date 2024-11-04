using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class reviewahand : MonoBehaviour
{
    public GameObject[] GA_objects;
    public int I_obj;


    public void Start()
    {
      for(int i = 0; i <GA_objects.Length;i++)
        {
            GA_objects[i].SetActive(false);
        }
    }

    public void THI_appearObject()
    {
        if (GA_objects[I_obj] != null && (I_obj < GA_objects.Length))
        {
               
                GA_objects[I_obj].SetActive(true);
                I_obj++;
        }
       
    }
    
}
