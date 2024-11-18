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

    void Awake()
    {
        _sprites = new Queue<Sprite>(sprites);
        _answeredQuestion = new List<string>();
        audioSource = GetComponent<AudioSource>();
        UpdateDisplayCounter();
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
}
