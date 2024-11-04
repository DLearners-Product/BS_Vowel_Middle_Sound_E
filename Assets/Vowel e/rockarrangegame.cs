using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class rockarrangegame : MonoBehaviour
{

    public GameObject[] GA_holders;
    public int I_ansCount;
    public GameObject G_smoke;
    public int I_maxCount;
    public Image IM_fill;
    public Text TEX_count;
    public GameObject G_levelComplete;
   // public Image[] IMA_levelcompAns;
  

    void Start()
    {
        I_maxCount = 7;
        IM_fill.fillAmount = 0;
        TEX_count.text = "0";
        G_levelComplete.SetActive(false);
        
    }


    void Update()
    {

    }


    public void BUT_clickAns()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "ans")
        {
            var smoke = Instantiate(G_smoke);
            smoke.transform.SetParent(this.gameObject.transform, false);
            smoke.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
            GA_holders[I_ansCount].transform.GetChild(0).GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
        //    IMA_levelcompAns[I_ansCount].sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
           
            I_ansCount++;
            Destroy(EventSystem.current.currentSelectedGameObject.GetComponent<Image>());
            Destroy(EventSystem.current.currentSelectedGameObject.GetComponent<Button>());
            float score = ((float)I_ansCount / (float)I_maxCount);
            IM_fill.fillAmount = score;
            TEX_count.text = "" + I_ansCount;
            if (I_ansCount == I_maxCount)
            {
                Invoke("THI_levelcomp", 1f);
            }
        }
    }
    public void THI_levelcomp()
    {
        G_levelComplete.SetActive(true);
    }

    //public void BUT_clickFinalAns(AudioClip AC_clip)

    //{
    //    EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>().clip = AC_clip;
    //    EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>().Play();
    //}
}
    
