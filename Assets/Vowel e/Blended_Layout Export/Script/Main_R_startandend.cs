using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Main_R_startandend : MonoBehaviour
{
    public string STR_gamename;
    public static Main_R_startandend OBJ_main_selectedcorrectobj;
    public GameObject selectedobj;
    public GameObject[] blur;
    //public Text objname;
    //public int I_attempt;

    public Animator rocket;

    public GameObject[] Questions;
    public int Q_count;
    public GameObject BG;
    public GameObject completed;

    //public GameObject G_block;
    public AudioClip[] wrongs;
    public AudioSource wrong;

    
    public AudioClip[] answers;


    // Start is called before the first frame update
    void Start()
    {
        STR_gamename = this.gameObject.name;
        // OBJ_main_selectedcorrectobj = this;
        //firecount = -1;
        for (int i = 0; i < Questions.Length; i++)
        {
            Questions[i].SetActive(false);
            blur[i].SetActive(false);
        }
        Questions[0].SetActive(true);
        completed.SetActive(false);
        rocket.Play("rocket");


        Q_count = 0;
        BG.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Clicking()
    {
        selectedobj = EventSystem.current.currentSelectedGameObject;

        if (selectedobj.tag == "answer")
        {
            //firecount++;
            //fires[firecount].SetActive(true);

           selectedobj.gameObject.GetComponent<mouse>().enabled = false;   //removing hover effect

            Questions[Q_count].SetActive(false);

            selectedobj.GetComponent<Button>().enabled = false;


            //wrong.clip = wrongs[0];
            //wrong.Play();

            //destroy animation
            Onblur();
           // Invoke("Onblur", 0.5f);

        }
        else
        {
            int random=Random.Range(1, wrongs.Length);
            wrong.clip = wrongs[random];
            wrong.Play();

            //I_attempt++;
            //THI_attmptForgame();
            //if (this.gameObject.name == "S_Obj_Start")
            //{
            //    Main_Blended.OBJ_main_blended.STRL_beginnings.Add(selectedobj.name);
            //}
            //if (this.gameObject.name == "A_Obj_Start")
            //{
            //    Main_Blended.OBJ_main_blended.STRL_beginninga.Add(selectedobj.name);
            //}
            //if (this.gameObject.name == "T_Obj_Start")
            //{
            //    Main_Blended.OBJ_main_blended.STRL_beginningt.Add(selectedobj.name);
            //}
            //if (this.gameObject.name == "P_Obj_Start")
            //{
            //    Main_Blended.OBJ_main_blended.STRL_beginningp.Add(selectedobj.name);
            //}
            //if (this.gameObject.name == "T_Obj_End ")
            //{
            //    Main_Blended.OBJ_main_blended.STRL_endtingt.Add(selectedobj.name);
            //}
            //if (this.gameObject.name == "P_Obj_End")
            //{
            //    Main_Blended.OBJ_main_blended.STRL_endtingp.Add(selectedobj.name);
            //}

        }
        

    }
    //public void THI_attmptForgame()
    //{
    //    if(Main_Blended.OBJ_main_blended.levelno==4)
    //    {
    //        Main_Blended.OBJ_main_blended.SobjectsStartWrongAttempt = I_attempt;
    //    }
    //    if (Main_Blended.OBJ_main_blended.levelno == 12)
    //    {
    //        Main_Blended.OBJ_main_blended.AobjectsStartWrongAttempt = I_attempt;
    //    }
    //    if (Main_Blended.OBJ_main_blended.levelno == 16)
    //    {
    //        Main_Blended.OBJ_main_blended.TobjectsStartWrongAttempt = I_attempt;
    //    }
    //    if (Main_Blended.OBJ_main_blended.levelno == 21)
    //    {
    //        Main_Blended.OBJ_main_blended.TobjectsEndWrongAttempt = I_attempt;
    //    }
    //    if (Main_Blended.OBJ_main_blended.levelno == 26)
    //    {
    //        Main_Blended.OBJ_main_blended.PobjectsEndStartAttempt = I_attempt;
    //    }
    //    if (Main_Blended.OBJ_main_blended.levelno == 27)
    //    {
    //        Main_Blended.OBJ_main_blended.PobjectsEndWrongAttempt = I_attempt;
    //    }
    //}
    public void Onblur()
    {
        
        for (int i = 0; i < Questions.Length; i++)
        {
            blur[i].SetActive(false);
        }
        blur[Q_count].SetActive(true);
        wrong.clip = answers[Q_count];
        wrong.Play();


        Invoke("Offblur", 2f);
        BG.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Debug.Log("blur!!");
    }

    public void Offblur()
    {
        blur[Q_count].SetActive(false);
        
        
        Invoke("ChangeQuestion", 1f);
        
        if (Q_count == 3)
        {
            rocket.Play("rocket_completed");
        }

    }

    public void ChangeQuestion()
    {

        BG.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        if (Q_count < 3)
        {
            Q_count++;
            for (int i = 0; i < Questions.Length; i++)
            {

                Questions[i].SetActive(false);
            }
            Questions[Q_count].SetActive(true);
        }

    }
}
