using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class treeapple : MonoBehaviour
{
    public  GameObject G_basket;
    public GameObject[] GA_apples;
    public  bool B_lerp;
    public  int I_collection;
    public GameObject G_levelcomp;
    public  bool B_levelcomp;
    public static treeapple OBJ_treeapple;


    public void Start()
    {
        OBJ_treeapple = this;
        I_collection = 0;
  
        B_levelcomp = false;
        G_levelcomp.SetActive(false);
    }
    public void Update()
    {
        if(B_lerp)
        {
            if(EventSystem.current.currentSelectedGameObject!=null)
            G_basket.transform.position = Vector2.Lerp(G_basket.transform.position,new Vector2(EventSystem.current.currentSelectedGameObject.transform.position.x,G_basket.transform.position.y), 5f * Time.deltaTime);
        }
        if(B_levelcomp)
        {
            Invoke("THI_basketFall",2f);
        }

    }
    public void THI_basketFall()
    {
        B_levelcomp = false;
        G_basket.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        G_basket.GetComponent<Rigidbody2D>().gravityScale = 1;
        for (int i = 0; i < GA_apples.Length; i++)
        {
            if (GA_apples[i].GetComponent<Rigidbody2D>() != null)
            {
                GA_apples[i].GetComponent<Collider2D>().enabled = false;
                GA_apples[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                GA_apples[i].GetComponent<Rigidbody2D>().gravityScale = 1;
                Destroy(GA_apples[i].GetComponent<apples>());
            }
        }
        Invoke("THI_levelcomp", 1f);
    }
    public void  THI_levelcomp()
    {
        G_levelcomp.SetActive(true);
    }
    public void BUT_clickApple()
    {
        if(EventSystem.current.currentSelectedGameObject.name=="a")
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Rigidbody2D>().gravityScale = 0.8f;
            B_lerp = true;
            I_collection++;
            if (I_collection == 8)
            {
                B_levelcomp = true;
            }
            for (int i = 0; i < GA_apples.Length; i++)
            {
                if (GA_apples[i].gameObject.GetComponent<apples>() != null)
                {
                    if (GA_apples[i].gameObject.GetComponent<apples>().B_collected == false)
                    {
                        if (GA_apples[i].GetComponent<Collider2D>() != null)
                        {
                            GA_apples[i].GetComponent<Collider2D>().enabled = false;
                            GA_apples[i].GetComponent<Button>().enabled = false;
                        }
                    }
                }
            }

            EventSystem.current.currentSelectedGameObject.GetComponent<Collider2D>().enabled = true;
            Invoke("THI_enableColl", 100f * Time.deltaTime);
        }
        else
        {
            Debug.Log("WRONG");
        }
        EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>().Play();
    }
    public void THI_enableColl()
    {
        B_lerp = false;
        for (int i = 0; i < GA_apples.Length; i++)
        {
            if (GA_apples[i].gameObject.GetComponent<apples>() != null)
            {
                if (GA_apples[i].gameObject.GetComponent<apples>().B_collected == false)
                {
                    if (GA_apples[i].GetComponent<Collider2D>() != null)
                    {
                        GA_apples[i].GetComponent<Collider2D>().enabled = true;
                        GA_apples[i].GetComponent<Button>().enabled = true;
                    }
                }
            }
        }
    }
}
