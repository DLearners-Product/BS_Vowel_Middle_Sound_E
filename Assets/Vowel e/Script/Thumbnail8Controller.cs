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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(DisableAnimator());
        ImageDropSlot.onDropInSlot += OnOptionDrop;
        UpdateCounterText();
        DisplayNextQuestion();
    }

    IEnumerator DisableAnimator()
    {
        yield return new WaitForSeconds(ANIMCLIP_optionPop.length);
        _opt1Anim.enabled = false;
        _opt2Anim.enabled = false;
    }

    void OnOptionDrop(GameObject droppedObj)
    {
        if(droppedObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == _answers[questionCounter - 1])
        {
            audioSource.PlayOneShot(AC_winSFX);
            Destroy(droppedObj);
            answerText.text = _answers[questionCounter - 1];
            UpdateCounterText();
            DisplayNextQuestion();
        }else{
            audioSource.PlayOneShot(AC_wrongSFX);
        }
    }

    void DisplayNextQuestion()
    {
        if(questionCounter >= _questions.Length){
            StartCoroutine(ActivityCompleted());
            return;
        }

        if(questionCounter > 0){
            StartCoroutine(QuestionTransition());
        }
        StartCoroutine(ChangeQuestion());
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
}
