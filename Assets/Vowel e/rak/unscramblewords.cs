using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class unscramblewords : MonoBehaviour
{
    public static unscramblewords OBJ_uw;
    public GameObject[] GA_questions;
    public int I_question, I_ansCount;
    public bool B_quesChange;
    public GameObject G_object;
    public GameObject G_levelComp;

    void Start()
    {
        OBJ_uw = this;
        B_quesChange = false;
        I_question = 0;
        for (int i = 0; i < GA_questions.Length; i++)
        {
            GA_questions[i].SetActive(false);
        }
        GA_questions[I_question].SetActive(true);
        G_object = GameObject.FindGameObjectWithTag("unscramble");
        I_ansCount = 0;
        G_levelComp.SetActive(false);


    }
    public void Update()
    {
        if(B_quesChange && I_question<GA_questions.Length)
        {
            B_quesChange = false;
         //   G_object.GetComponent<Image>().color = Color.white;
            Invoke("THI_QuesChange", 2f);
        }
    }
    public void THI_QuesChange()
    {
      
        I_question++;
        if (I_question >= GA_questions.Length)
        {
            G_levelComp.SetActive(true);

        }
        for (int i = 0; i < GA_questions.Length; i++)
        {
            GA_questions[i].SetActive(false);
        }
        GA_questions[I_question].SetActive(true);
        G_object = GameObject.FindGameObjectWithTag("unscramble");
        I_ansCount = 0;
        
    }
}

