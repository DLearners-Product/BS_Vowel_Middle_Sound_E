using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.Experimental.Rendering;

public class Thumbnail8Controller : MonoBehaviour
{

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answerText;
    public TextMeshProUGUI counterText;
    public AnimationClip ANIMCLIP_optionPop;
    public AnimationClip ANIMCLIP_qtransition;
    public Animator _opt1Anim, _opt2Anim;
    public string[] _questions;
    public string[] _answers;
    public GameObject[] options;
    public AudioClip AC_oceanSFX;
    public AudioClip AC_winSFX;
    public AudioClip AC_wrongSFX;
    public GameObject transitionObj;
    public GameObject activityCompleted;
    AudioSource audioSource;
    int questionCounter = 0;

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

    void Start()
    {
#region DataSetter
        // Main_Blended.OBJ_main_blended.levelno = 7;
        QAManager.instance.UpdateActivityQuestion();
        qIndex = 0;
        GetData(qIndex);
        GetAdditionalData();
        // AssignData();
#endregion
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(DisableAnimator());
        UpdateCounterText();
        DisplayNextQuestion();
    }

    private void OnEnable() {
        ImageDropSlot.onDropInSlot += OnOptionDrop;
    }

    private void OnDisable() {
        ImageDropSlot.onDropInSlot -= OnOptionDrop;
    }

    IEnumerator DisableAnimator()
    {
        yield return new WaitForSeconds(ANIMCLIP_optionPop.length);
        _opt1Anim.enabled = false;
        _opt2Anim.enabled = false;
    }

    void OnOptionDrop(GameObject droppedObj)
    {
        string selectedAns = droppedObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        if(selectedAns == _answers[questionCounter - 1])
        {
            ScoreManager.instance.RightAnswer(qIndex++, questionID: question.id, answerID: GetOptionID(selectedAns));
            audioSource.PlayOneShot(AC_winSFX);
            Destroy(droppedObj);
            answerText.text = _answers[questionCounter - 1];
            UpdateCounterText();
            DisplayNextQuestion();
        }else{
            ScoreManager.instance.WrongAnswer(qIndex, questionID: question.id, answerID: GetOptionID(selectedAns));
            audioSource.PlayOneShot(AC_wrongSFX);
        }
    }

    void DisplayNextQuestion()
    {
        if(questionCounter >= _questions.Length){
            BlendedOperations.instance.NotifyActivityCompleted();
            StartCoroutine(ActivityCompleted());
            return;
        }

        if(questionCounter > 0){
            StartCoroutine(QuestionTransition());
        }
        StartCoroutine(ChangeQuestion());
        GetData(qIndex);
    }

    IEnumerator QuestionTransition()
    {
        audioSource.PlayOneShot(AC_oceanSFX);
        transitionObj.SetActive(true);
        yield return new WaitForSeconds(ANIMCLIP_qtransition.length);
        transitionObj.SetActive(false);
    }

    IEnumerator ChangeQuestion()
    {
        yield return new WaitForSeconds(
            (questionCounter == 0) ? 0f : ANIMCLIP_qtransition.length/2
        );
        questionText.text = _questions[questionCounter++];
        answerText.text = "";
    }

    IEnumerator ActivityCompleted()
    {
        yield return new WaitForSeconds(1.5f);
        activityCompleted.SetActive(true);
    }

    void UpdateCounterText()
    {
        counterText.text = $"{questionCounter} / {_questions.Length}";
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
