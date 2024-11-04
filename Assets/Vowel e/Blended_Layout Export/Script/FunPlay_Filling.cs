using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FunPlay_Filling : MonoBehaviour
{
    
    public GameObject selectedobject, G_dash,block;
    public GameObject handle, rolling, coin;
    

    public Animator coin_anim;
    public AnimationClip AC_right, AC_wrong;

    public GameObject[] questions, answers,options;
    public int count;

   
    public AudioSource wrong;
    public AudioClip[] clips,answer_clips;
    

    //public GameObject optionPanel; //T_Phonemic-1
    //public GameObject[] otherOptions;  //T_Phonemic-1

    private void Start()
    {
        count = 0;
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(false);
            answers[i].SetActive(false);
            options[i].SetActive(false);
        }
        questions[count].SetActive(true);
        options[count].SetActive(true);

        handle.SetActive(true);
        coin.SetActive(false);
        rolling.SetActive(false);
        G_dash.SetActive(true);
        block.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
    }



    public void clicking()
    {
        
        selectedobject = EventSystem.current.currentSelectedGameObject;


        if (selectedobject.tag == "answer")
        {
            Debug.Log("ans");
            
           
                coin_anim.Play("crtans_coin");
                G_dash.SetActive(false);
                handle.SetActive(false);
                rolling.SetActive(true);
                Invoke("coindefaultanim", AC_right.length);
                Invoke("coinandanswer",clips[0].length);
                wrong.clip = clips[0];
                wrong.Play();
                block.SetActive(true);
            //selectedobject.gameObject.GetComponent<Button>().enabled = false;
                //this.GetComponent<DragandDrop>().enabled = false;

            
        }
        else
        {
            block.SetActive(true);
            coin_anim.Play("wrgans_coin");
            int random = Random.Range(1, clips.Length);
            wrong.clip = clips[random];
            wrong.Play();
            Invoke("coindefaultanim", AC_wrong.length);
        }

    }

    public void coinandanswer()
    {
        Debug.Log("ansshow");
        coin.SetActive(true);
        answers[count].SetActive(true);
        wrong.clip = answer_clips[count];
        wrong.Play();

        Invoke("changequestion", 2f);
    }

    public void changequestion()
    {
        handle.SetActive(true);
        coin.SetActive(false);
        rolling.SetActive(false);
        G_dash.SetActive(true);
        
        if (count < 4)
        {
            count++;
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i].SetActive(false);
                answers[i].SetActive(false);
                options[i].SetActive(false);
            }
            block.SetActive(false);
            questions[count].SetActive(true);
            options[count].SetActive(true);
        }
        else
        {
            G_dash.SetActive(false);
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i].SetActive(false);
                answers[i].SetActive(false);
                options[i].SetActive(false);
            }
        }
    }
    public void coindefaultanim()
    {
        coin_anim.Play("New State");
        block.SetActive(false);
    }
}


