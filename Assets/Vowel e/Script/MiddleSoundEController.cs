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
    int totalQuestion = 6;
    int counter = 0;

    void Start()
    {
        UpdateCounter();
    }

    private void OnEnable() {
        ImageDropSlot.onDropInSlot += OnVowelDroped;
    }

    void OnVowelDroped(GameObject droppedObj)
    {
        if(droppedObj.name == "E" && counter < totalQuestion){
            celebrateObj.SetActive(true);
            celebrateObj.GetComponent<ParticleSystem>().Play();
            textMeshProUGUI.text = "E";
            StartCoroutine(DisableParticleSystem());
        }
    }

    IEnumerator DisableParticleSystem() 
    {
        yield return new WaitForSeconds(2f);
        celebrateObj.SetActive(false);
        textMeshProUGUI.text = "__";
        firstCharacterRandomizer.SwitchObject();
        secCharacterRandomizer.SwitchObject();
        questionRandomizer.SwitchObject();
        UpdateCounter();
    }

    void UpdateCounter() {
        counterDisplay.text = $"0{++counter} / 0{totalQuestion}";
    }
}
