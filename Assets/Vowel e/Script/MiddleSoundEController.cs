using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiddleSoundEController : MonoBehaviour
{
    public Randomizer firstCharacterRandomizer;
    public Randomizer secCharacterRandomizer;
    public Randomizer questionRandomizer;
    public GameObject celebrateObj;
    public TextMeshProUGUI textMeshProUGUI;
    public TextMeshProUGUI counterDisplay;
    public GameObject activityCompleted;
    public AudioClip AC_right, AC_wrong;
    public AudioClip[] AC_questions;
    AudioSource audioSource;
    int totalQuestion = 6;
    int counter = 0;

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

    void Start()
    {
#region DataSetter
      //  Main_Blended.OBJ_main_blended.levelno = 5;
        QAManager.instance.UpdateActivityQuestion();
        qIndex = 0;
        GetData(qIndex);
        GetAdditionalData();
        // AssignData();
#endregion

        audioSource = GetComponent<AudioSource>();
        UpdateCounter();
    }

    private void OnEnable() {
        ImageDropSlot.onDropInSlot += OnVowelDroped;
    }

    private void OnDisable() {
        ImageDropSlot.onDropInSlot -= OnVowelDroped;
    }

    void OnVowelDroped(GameObject droppedObj)
    {
        if(droppedObj.name == "E"){
            celebrateObj.SetActive(true);
            celebrateObj.GetComponent<ParticleSystem>().Play();
            textMeshProUGUI.text = "e";
            ScoreManager.instance.RightAnswer(qIndex, questionID: question.id, answerID: GetOptionID("e"));
            audioSource.PlayOneShot(AC_questions[counter - 1]);
            StartCoroutine(DisableParticleSystem());
            GetData(++qIndex);
        }else{
            ScoreManager.instance.WrongAnswer(qIndex, questionID: question.id, answerID: GetOptionID(droppedObj.name.ToLower()));
            audioSource.PlayOneShot(AC_wrong);
        }
    }

    IEnumerator DisableParticleSystem() 
    {
        yield return new WaitForSeconds(2f);
        celebrateObj.SetActive(false);

        if(counter < totalQuestion){
            textMeshProUGUI.text = "__";
            firstCharacterRandomizer.SwitchObject();
            secCharacterRandomizer.SwitchObject();
            questionRandomizer.SwitchObject();
        }else{
            activityCompleted.SetActive(true);
            BlendedOperations.instance.NotifyActivityCompleted();
        }

        UpdateCounter();
    }

    void UpdateCounter() {
        counterDisplay.text = $"{++counter} / {totalQuestion}";
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
    //         optionsGO[i].GetComponent<Image>().sprite = options[i]._sprite;
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
