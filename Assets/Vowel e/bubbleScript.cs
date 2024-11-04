using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class bubbleScript : MonoBehaviour
{
    public GameObject[] bubbles;
    public Sprite bubbleBurstSprite;
    public GameObject shade;
    public GameObject jellyFish;

    public void Click_burst()
    {
        EventSystem.current.currentSelectedGameObject.gameObject.GetComponent<Image>().sprite = bubbleBurstSprite;

        StartCoroutine(DisplayShade());
    }

    IEnumerator DisplayShade()
    {
        yield return new WaitForSeconds(0.5f);

        for (int i =0; i < bubbles.Length; i++)
        {
            bubbles[i].SetActive(false);
        }
        jellyFish.SetActive(false);

        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
        shade.SetActive(true);


        shade.gameObject.transform.Find("selectedImage").GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.gameObject.transform.GetChild(0).GetComponent<Image>().sprite;

        char firstLetter = EventSystem.current.currentSelectedGameObject.gameObject.transform.GetChild(0).name.ToString()[0];
        char middleLetter = EventSystem.current.currentSelectedGameObject.gameObject.transform.GetChild(0).name.ToString()[1];
        char lastLetter = EventSystem.current.currentSelectedGameObject.gameObject.transform.GetChild(0).name.ToString()[2];
        shade.gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(firstLetter + "<#F7E73D>" + middleLetter + "</color>" + lastLetter);

        yield return new WaitForSeconds(1.5f);

        shade.SetActive(false);

        for (int i = 0; i < bubbles.Length; i++)
        {
            bubbles[i].SetActive(true);
        }

        jellyFish.SetActive(true);
        EventSystem.current.currentSelectedGameObject.gameObject.SetActive(false);
    }
}