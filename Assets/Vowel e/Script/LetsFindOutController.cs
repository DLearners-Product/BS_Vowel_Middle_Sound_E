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
    public TextMeshProUGUI counterDisplay;
    public GameObject activityCompleted;
    Queue<Sprite> _sprites;
    int displayCounter=0;
    AudioSource audioSource;

    void Awake()
    {
        _sprites = new Queue<Sprite>(sprites);
        audioSource = GetComponent<AudioSource>();
        UpdateDisplayCounter();
    }

    public Sprite GetSprite()
    {
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
        Debug.Log($"Frame Buttonclicked.... {selectedObj.name}");
    }

    public bool EvaluateAnswer(string ansStr)
    {
        return correctAns.Contains(ansStr);
    }

    public void DisplayAnswer(Sprite ansSprite)
    {
        displayImages[displayCounter].sprite = ansSprite;
        displayImages[displayCounter].gameObject.SetActive(true);
        Utilities.Instance.ANIM_CorrectScaleEffect(displayImages[displayCounter++].transform);
        UpdateDisplayCounter();

        if(displayCounter == correctAns.Length) MarkActivityCompleted();
    }

    void MarkActivityCompleted()
    {
        activityCompleted.SetActive(true);
    }
}
