using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Revision_STAP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void name_clicked()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        Invoke("obj_name", 0.5f);
    }

    public void obj_name()
    {
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if (this.gameObject.transform.GetChild(2).gameObject.GetComponent<Animator>() != null)
        {
            this.gameObject.transform.GetChild(2).gameObject.GetComponent<Animator>().SetInteger("cond", 1); ;
        }
        Invoke("Animationoff", 1f);
    }
    public  void Animationoff()
    {
        if (this.gameObject.transform.GetChild(2).gameObject.GetComponent<Animator>() != null)
        {
            this.gameObject.transform.GetChild(2).gameObject.GetComponent<Animator>().SetInteger("cond", 0); ;
        }
        Invoke("texton", 0.3f);
    }
    public void texton()
    {
        this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}

