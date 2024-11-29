﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using JetBrains.Annotations;

public class treeapple : MonoBehaviour
{
    public  GameObject G_basket;
    public GameObject[] GA_apples;
    public  bool B_lerp;
    public  int I_collection;
    public GameObject G_levelcomp;
    public  bool B_levelcomp;
    public static treeapple OBJ_treeapple;
    public TextMeshProUGUI counterText;
    int TOTAL_ANS = 6;
#region QA
    private int qIndex;
    public GameObject questionGO;
    public GameObject[] optionsGO;
    public bool isActivityCompleted = false;
    public Dictionary<string, Component> additionalFields;
    Component question;
    Component[] _options;
    Component[] answers;
#endregion

    public void Start()
    {
#region DataSetter
        // Main_Blended.OBJ_main_blended.levelno = 8;
        QAManager.instance.UpdateActivityQuestion();
        qIndex = 0;
        GetData(qIndex);
        GetAdditionalData();
        // AssignData();
#endregion

        OBJ_treeapple = this;
        I_collection = 0;
  
        B_levelcomp = false;
        G_levelcomp.SetActive(false);
        UpdateCounterText();
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

        BlendedOperations.instance.NotifyActivityCompleted();

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
        var currentClickedGO = EventSystem.current.currentSelectedGameObject;
        if(currentClickedGO.name=="a")
        {
            ScoreManager.instance.RightAnswer(qIndex++, questionID: question.id, answerID: GetOptionID(currentClickedGO.transform.GetChild(0).GetComponent<Text>().text));

            currentClickedGO.GetComponent<Rigidbody2D>().gravityScale = 0.8f;

            B_lerp = true;
            I_collection++;
            UpdateCounterText();
            if (I_collection == TOTAL_ANS)
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

            currentClickedGO.GetComponent<Collider2D>().enabled = true;
            Invoke("THI_enableColl", 100f * Time.deltaTime);
        }
        else
        {
            ScoreManager.instance.WrongAnswer(qIndex, questionID: question.id, answerID: GetOptionID(currentClickedGO.transform.GetChild(0).GetComponent<Text>().text));
            Debug.Log("WRONG");
        }
        currentClickedGO.GetComponent<AudioSource>().Play();
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

    void UpdateCounterText()
    {
        counterText.text = $"{I_collection} / {TOTAL_ANS}";
    }

#region QA
    int GetOptionID(string selectedOption)
    {
        for (int i = 0; i < _options.Length; i++)
        {
            if (_options[i].text == selectedOption)
            {
                return _options[i].id;
            }
        }
        return -1;
    }

    void GetData(int questionIndex)
    {
        question = QAManager.instance.GetQuestionAt(0, questionIndex);
        _options = QAManager.instance.GetOption(0, questionIndex);
        answers = QAManager.instance.GetAnswer(0, questionIndex);
    }
 
    void GetAdditionalData()
    {
        additionalFields = QAManager.instance.GetAdditionalField(0);
    }
 
    // void AssignData()
    // {
    //     // Custom code
    //     for (int i = 0; i < optionsGO.Length; i++)
    //     {
    //         optionsGO[i].GetComponent<Image>().sprite = _options[i]._sprite;
    //         optionsGO[i].tag = "Untagged";
    //         Debug.Log(optionsGO[i].name, optionsGO[i]);
    //         // if (CheckOptionIsAns(options[i]))
    //         // {
    //         //     optionsGO[i].tag = "answer";
    //         // }
    //     }
    //     // answerCount.text = "/"+answers.Length;
    // }
#endregion
}
