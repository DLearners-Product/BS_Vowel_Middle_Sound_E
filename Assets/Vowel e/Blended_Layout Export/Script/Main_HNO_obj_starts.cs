using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Main_HNO_obj_starts : MonoBehaviour
{
    public string STR_gamename;
    public static Main_HNO_obj_starts OBJ_main_selectedcorrectobj;
    public GameObject selectedobj,Lettertohide,fireworks;
    //public GameObject blur;
    //public Text objname;
    //public int I_attempt;

    public GameObject[] fires;
    public int firecount;
    public Material greyscale;

    public Animator bat;
    public AnimationClip AC_happy, AC_sad,AC_firework;

    //public GameObject G_block;
    public AudioClip[] wrongs;
    public AudioSource wrong;


    // Start is called before the first frame update
    void Start()
    {
        STR_gamename = this.gameObject.name;
        // OBJ_main_selectedcorrectobj = this;
        firecount = -1;
        for (int i = 0; i < fires.Length; i++)
        {
            fires[i].SetActive(false);
        }
        Lettertohide.SetActive(true);
        fireworks.SetActive(false);



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
            firecount++;
            fires[firecount].SetActive(true);
            bat.Play("Bat_happy");
            Invoke("coindefaultanim", AC_happy.length);

            selectedobj.gameObject.GetComponent<mouse>().enabled = false;   //removing hover effect

            selectedobj.gameObject.GetComponent<Image>().material = greyscale;

            selectedobj.GetComponent<Button>().enabled = false;
            wrong.clip = wrongs[0];
            wrong.Play();

           if(firecount==3)
            {
                Lettertohide.SetActive(false);
                Function_firework();
            }

        }
        else
        {
            int random=Random.Range(1, wrongs.Length);
            wrong.clip = wrongs[random];
            wrong.Play();
            bat.Play("Bat_sad");
            Invoke("coindefaultanim", AC_sad.length);

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
    //public void change()
    //{
    //    blur.SetActive(true);
    //    objname.text = selectedobj.name;
    //    blur.transform.GetChild(0).GetComponent<Image>().sprite = selectedobj.GetComponent<Image>().sprite;
    //    blur.transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
    //    Invoke("offblur", 2f);
    //    Debug.Log("blur!!");
    //}
    //public void offblur()
    //{
    //    blur.SetActive(false);
    //    G_block.SetActive(true);
    //}
    public void Function_firework()
    {
        fireworks.SetActive(true);
        Invoke("coindefaultanim", AC_firework.length);
        
    }
    public void coindefaultanim()
    {
        if (firecount < 3)
        {
            bat.Play("Bat_Idle");
        }
        else
        {
            bat.Play("Bat_completed");
        }
    }
    
}
