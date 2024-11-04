using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragrak : MonoBehaviour
{
    public GameObject G_selectedObject; // selected object
    public GameObject[] GA_objects; // objects to drag
    public Vector2[] VEC2_StartPos; // set the start pos
    bool B_canChangeObj; // select an object
    public GameObject G_finishPos; // matched pos
    public GameObject G_slots; // to off the obj after match


    public AudioSource[] ASA_wrong;
    public AudioSource[] ASA_correct;
    public GameObject[] GA_FinishPos; // unscramble game
    public GameObject G_sfx;  

    void Start()
    {
        B_canChangeObj = true;
        VEC2_StartPos = new Vector2[GA_objects.Length];
        for (int i = 0; i < VEC2_StartPos.Length; i++)
        {
            VEC2_StartPos[i] = GA_objects[i].transform.position;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null)
            {
                if (B_canChangeObj)
                { 
                    if (hit.collider.gameObject.tag != "1" && hit.collider.gameObject.tag != "2" && hit.collider.gameObject.tag != "3") // unscramble game check to avoid other object drag
                    {
                        G_selectedObject = hit.collider.gameObject;
                        B_canChangeObj = false;
                    }
                }
                if(G_selectedObject!=null)
                G_selectedObject.transform.position = worldPoint;
                B_canChangeObj = false;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            B_canChangeObj = true;
            for (int i = 0; i < GA_objects.Length; i++)
            {
                if (G_selectedObject!=null && G_selectedObject.name == GA_objects[i].name)
                {
                    if (GA_objects[i].GetComponent<droprak>().B_matched == false)
                    {
                        int I_randSound = Random.Range(0, ASA_wrong.Length);
                        ASA_wrong[I_randSound].Play();
                        G_selectedObject.transform.position = VEC2_StartPos[i];
                        G_selectedObject = null;
                    }
                    else
                    {
                        if (!this.gameObject.name.Contains("q"))
                        {
                            if (G_selectedObject != null)
                            {
                                GA_objects[i].transform.position = G_finishPos.transform.position; //single drop game
                                this.gameObject.GetComponent<AudioSource>().Play();
                                G_selectedObject = null;
                                for(int f = 0; f <ASA_wrong.Length;f++)
                                {
                                    ASA_wrong[f].Stop();
                                }
                                if(unscramblewords.OBJ_uw!=null)
                                unscramblewords.OBJ_uw.B_quesChange = true;
                            }
                        }
                        else
                        {
                            if (G_selectedObject != null)
                            {
                                GA_objects[i].transform.position = GA_FinishPos[i].transform.position;    //unscramble game
                                G_selectedObject = null;
                                unscramblewords.OBJ_uw.I_ansCount++;
                                if (unscramblewords.OBJ_uw.I_ansCount != 3)
                                {
                                    int I_randSound = Random.Range(0, ASA_correct.Length);
                                    ASA_correct[I_randSound].Play();
                                }
                                if (unscramblewords.OBJ_uw.I_ansCount==3)
                                {
                                    for (int f = 0; f < ASA_wrong.Length; f++)
                                    {
                                        ASA_wrong[f].Stop();
                                    }
                                    this.gameObject.GetComponent<AudioSource>().Play();
                                    unscramblewords.OBJ_uw.B_quesChange = true;
                                }
                            }
                        }
                        GA_objects[i].GetComponent<Collider2D>().enabled = false;

                        if(G_slots!=null)
                        {
                            if (!this.gameObject.name.Contains("q"))  // single drop game
                            {
                                for (int j = 0; j < GA_objects.Length; j++)
                                {
                                    if (GA_objects[j] != GA_objects[i])
                                    {
                                        GA_objects[j].transform.SetParent(G_slots.transform, false);
                                    }
                                }
                                G_sfx.SetActive(true);
                                G_slots.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }
}