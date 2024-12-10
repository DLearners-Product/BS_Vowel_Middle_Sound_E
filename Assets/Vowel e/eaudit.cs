﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI counterText;
    public GameObject activityCompleted;

#region QA
    private int qIndex;
    public GameObject questionGO;
    public GameObject[] optionsGO;
    public bool isActivityCompleted = false;
    public Dictionary<string, Component> additionalFields;
    Component question;
    Component[] options;
    Component[] answers;
#endregion

    public void Start()
    {
#region DataSetter
        // Main_Blended.OBJ_main_blended.levelno = 9;
        QAManager.instance.UpdateActivityQuestion();
        qIndex = 0;
        GetData(qIndex);
        GetAdditionalData();
        // AssignData();
#endregion
        audioSource = GetComponent<AudioSource>();
        OBJ_eaudit = this;
        I_count = 0;
        // Invoke(nameof(THI_Sound), 1f);
        UpdateCounterText();
        PlayQuestionAudio();
    }


    public void BUT_next()
    {
        I_count++;

        if(I_count == AS_words.Length) {
            activityCompleted.SetActive(true); 
            BlendedOperations.instance.NotifyActivityCompleted();
            return;
        }

        GetData(I_count);
        UpdateCounterText();
        PlayQuestionAudio();
    }

    public float THI_Sound()
    {
        AS_words[I_count].Play();
        return AS_words[I_count].clip.length;
    }

    public void BUT_yes()
    {
        MoveSelectedObjectUp();
        if(I_count==0 || I_count == 2 || I_count == 3 || I_count == 4 || I_count == 7 || I_count == 8 || I_count == 9 || I_count == 10 || I_count == 11)
        {
            ScoreManager.instance.RightAnswer(qIndex++, questionID: question.id, answerID: GetOptionID("Yes"));
            AS_correct.Play();
            StartCoroutine(CallNextQuestion());
        }
        else
        {
            ScoreManager.instance.WrongAnswer(qIndex, questionID: question.id, answerID: GetOptionID("Yes"));
            AS_wrong.Play();
        }
        
    }

    void MoveSelectedObjectUp()
    {
        var selectedBoard = EventSystem.current.currentSelectedGameObject;
        Utilities.Instance.ANIM_Move(selectedBoard.transform, new Vector3(
                                selectedBoard.transform.position.x,
                                selectedBoard.transform.position.y + 0.4f,
                                selectedBoard.transform.position.z));
        StartCoroutine(ResetOptions(selectedBoard));
    }

    IEnumerator ResetOptions(GameObject resetObj)
    {
        yield return new WaitForSeconds(1f);
        Utilities.Instance.ANIM_Move(resetObj.transform, new Vector3(
                        resetObj.transform.position.x,
                        resetObj.transform.position.y - 0.4f,
                        resetObj.transform.position.z));
    }

    public void BUT_no()
    {
        MoveSelectedObjectUp();
        if (I_count == 1 || I_count == 5 || I_count == 6 )
        { 
            ScoreManager.instance.RightAnswer(qIndex++, questionID: question.id, answerID: GetOptionID("No"));
            AS_correct.Play();
            StartCoroutine(CallNextQuestion());
        }
        else
        {
            ScoreManager.instance.WrongAnswer(qIndex, questionID: question.id, answerID: GetOptionID("No"));
            AS_wrong.Play();
        }
    }

    IEnumerator CallNextQuestion()
    {
        yield return new WaitForSeconds(AS_correct.clip.length + 0.1f);
        BUT_next();
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

    void UpdateCounterText()
    {
        counterText.text = $"{I_count + 1} / {AS_words.Length}";
    }

#region QA
    int GetOptionID(string selectedOption)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].text == selectedOption)
            {
                return options[i].id;
            }
        }
        return -1;
    }

    void GetData(int questionIndex)
    {
        question = QAManager.instance.GetQuestionAt(0, questionIndex);
        options = QAManager.instance.GetOption(0, questionIndex);
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
