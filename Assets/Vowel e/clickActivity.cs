using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class clickActivity : MonoBehaviour
{
    public GameObject destinationPos;
    public GameObject movingObj;
    //  public GameObject G_tmp;
    public GameObject G_pulseletter;


    bool canLerp;

    private void Start()
    {
        canLerp = false;
    }
    private void Update()
    {
        if (canLerp)
        {
            movingObj.transform.position = Vector3.Lerp(movingObj.transform.position, destinationPos.transform.position, 5f * Time.deltaTime);
        }
    }

    public void Click_startLerp()
    {
        canLerp = true;
      //  G_tmp.GetComponent<TextMeshProUGUI>().SetText("<color=red>e</color>d");
        this.gameObject.GetComponent<AudioSource>().Play();
        G_pulseletter.GetComponent<Animator>().SetInteger("cond", 1);
    }
}