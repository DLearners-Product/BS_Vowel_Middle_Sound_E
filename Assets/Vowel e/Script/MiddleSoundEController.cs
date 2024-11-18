using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

    void Start()
    {
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
            textMeshProUGUI.text = "E";
            audioSource.PlayOneShot(AC_questions[counter - 1]);
            StartCoroutine(DisableParticleSystem());
        }else{
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
        }

        UpdateCounter();
    }

    void UpdateCounter() {
        counterDisplay.text = $"{++counter} / {totalQuestion}";
    }
}
