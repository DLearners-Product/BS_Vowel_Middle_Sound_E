using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speakre : MonoBehaviour
{

    public GameObject G_sound;

    private void OnMouseEnter()
    {
        if (this.gameObject.name == "speaker")
        {
            eaudit.OBJ_eaudit.THI_Sound();
            G_sound.SetActive(true);
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
    private void OnMouseExit()
    {
        if (this.gameObject.name == "speaker")
        {
            G_sound.SetActive(false);
        }
    }
}
