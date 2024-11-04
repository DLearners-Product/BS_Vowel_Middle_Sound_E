using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class frog : MonoBehaviour
{

    public GameObject G_shadow;

    

    public void THI_onshadow()
    {
        G_shadow.GetComponent<Image>().enabled = true;
//        Debug.Log("shadown on!!");
    }
}
