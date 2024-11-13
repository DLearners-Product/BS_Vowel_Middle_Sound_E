using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eaudit : MonoBehaviour
{
    public int I_count;
    public AudioSource[] AS_words;
    public static eaudit OBJ_eaudit;
    public AudioSource AS_correct, AS_wrong;
    public Transform speakerBurst;
    AudioSource audioSource;
    public Animator speakerBtnAnim;
    public Transform speakerBTN;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        OBJ_eaudit = this;
        I_count = 0;
        // Invoke(nameof(THI_Sound), 1f);
    }


    public void BUT_next()
    {
        I_count++;
        // THI_Sound();
    }

    public float THI_Sound()
    {
        AS_words[I_count].Play();
        return AS_words[I_count].clip.length;
    }

    public void BUT_yes()
    {
        if(I_count==0 || I_count == 2 || I_count == 3 || I_count == 4 || I_count == 7 || I_count == 8 || I_count == 9 || I_count == 10)
        {
            AS_correct.Play();
        }
        else
        {
            AS_wrong.Play();
        }
        
    }
    public void BUT_no()
    {
        if (I_count == 1 || I_count == 5 || I_count == 6 )
        { 
            AS_correct.Play();
        }
        else
        {
            AS_wrong.Play();
        }
    }

    public void PlayQuestionAudio()
    {
        speakerBTN.gameObject.SetActive(true);
        speakerBtnAnim.Play("speaker");
        Utilities.Instance.ANIM_Explode(speakerBTN);
        // THI_Sound();
        StartCoroutine(StopAnimation(THI_Sound()));
    }

    IEnumerator StopAnimation(float waitSec)
    {
        yield return new WaitForSeconds(1.5f);
        speakerBtnAnim.Play("New State");
        // Utilities.Instance.ANIM_SpeakerReset(speakerBTN);
        speakerBTN.localScale = Vector3.zero;
        speakerBTN.GetComponent<Image>().color = Color.white;
    }

}
