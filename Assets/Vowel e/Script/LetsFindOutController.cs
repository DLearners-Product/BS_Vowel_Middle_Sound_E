using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LetsFindOutController : MonoBehaviour
{
    public Sprite[] sprites;
    public string[] correctAns;
    public Image[] displayImages;
    public AudioClip[] _audioClips;
    public AudioClip wrongClip;
    public TextMeshProUGUI counterDisplay;
    public GameObject activityCompleted;
    Queue<Sprite> _sprites;
    int displayCounter=0;
    AudioSource audioSource;
    List<string> _answeredQuestion;
  
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

    void Awake()
    {
        _sprites = new Queue<Sprite>(sprites);
        _answeredQuestion = new List<string>();
        audioSource = GetComponent<AudioSource>();
        UpdateDisplayCounter();
    }

    private void Start() {
#region DataSetter
        Main_Blended.OBJ_main_blended.levelno = 6;
        QAManager.instance.UpdateActivityQuestion();
        qIndex = 0;
        GetData(qIndex);
        GetAdditionalData();
        // AssignData();
#endregion
    }

    public Sprite GetSprite()
    {
        if(_answeredQuestion.Contains(_sprites.Peek().name)){
            _sprites.Dequeue();
            return null;
        }
        _sprites.Enqueue(_sprites.Peek());
        return _sprites.Dequeue();
    }

    void UpdateDisplayCounter()
    {
        counterDisplay.text = $"{displayCounter} / {correctAns.Length}";
    }

    public bool CheckCorrectAns(string checkStr)
    {
        return correctAns.Contains(checkStr);
    }

    public void OnFrameClicked()
    {
        var selectedObj = EventSystem.current.currentSelectedGameObject;
    }

    public bool EvaluateAnswer(string ansStr)
    {
        return correctAns.Contains(ansStr) && !_answeredQuestion.Contains(ansStr);
    }

    public void DisplayAnswer(Sprite ansSprite)
    {
        displayImages[displayCounter].sprite = ansSprite;
        displayImages[displayCounter].gameObject.SetActive(true);
        Utilities.Instance.ANIM_CorrectScaleEffect(displayImages[displayCounter++].transform.parent);
        UpdateDisplayCounter();
        float clipLen = PlayAnswerAudio(ansSprite.name);
        _answeredQuestion.Add(ansSprite.name);

        if(displayCounter == correctAns.Length) StartCoroutine(WaitFor(clipLen + 1));
    }

    IEnumerator WaitFor(float waitSecs)
    {
        yield return new WaitForSeconds(waitSecs);
        EnableActivityCompleted();
    }

    public void WronglyAnswered()
    {
        audioSource.PlayOneShot(wrongClip);
    }

    float PlayAnswerAudio(string ansSTR)
    {
        for (int i = 0; i < _audioClips.Length; i++)
        {
            if(_audioClips[i].name == ansSTR)
            {
                audioSource.PlayOneShot(_audioClips[i]);
                return _audioClips[i].length;
            }
        }

        return 0f;
    }

    public void PlayHighlightedFrameAudio(string audioStr)
    {
        PlayAnswerAudio(audioStr);
    }

    void EnableActivityCompleted()
    {
        activityCompleted.SetActive(true);
    }

#region QA
 
    int GetOptionID(string selectedOption)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].text == selectedOption)
            {
                Debug.Log(selectedOption);
                return options[i].id;
            }
        }
        return -1;
    }

    void GetData(int questionIndex)
    {
        question = QAManager.instance.GetQuestionAt(0, questionIndex);
        // if(question != null){
        options = QAManager.instance.GetOption(0, questionIndex);
        answers = QAManager.instance.GetAnswer(0, questionIndex);
        // }
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
    //         if (CheckOptionIsAns(options[i]))
    //         {
    //             optionsGO[i].tag = "answer";
    //         }
    //     }
    //     // answerCount.text = "/"+answers.Length;
    // }

#endregion
}
