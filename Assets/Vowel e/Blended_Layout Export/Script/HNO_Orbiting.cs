using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HNO_Orbiting : MonoBehaviour
{
    public GameObject center;
    public float speed = 1;
    //public GameObject star;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Orbiting();
    }
    public void Orbiting()
    {
        //Debug.Log("rotating");
        transform.RotateAround(center.transform.position, Vector3.back, speed * Time.deltaTime);
    }
}
