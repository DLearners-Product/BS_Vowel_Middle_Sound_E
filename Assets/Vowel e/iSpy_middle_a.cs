using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class iSpy_middle_a : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject shade;

    public int object_count;
    public Text scoreText;

    private void Start()
    {
        object_count = 0;
    }
    public void ImgHighlight()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        StartCoroutine(DisplayShade());
    }

    IEnumerator DisplayShade()
    {
        object_count++;
        scoreText.text = ""+ object_count;

        for (int i=0; i< objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().enabled = false;
        shade.SetActive(true);

        shade.gameObject.transform.Find("selectedImage").GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
        char firstLetter = EventSystem.current.currentSelectedGameObject.name.ToString()[0];
        char middleLetter = EventSystem.current.currentSelectedGameObject.name.ToString()[1];
        char lastLetter = EventSystem.current.currentSelectedGameObject.name.ToString()[2];
        shade.gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(firstLetter + "<#F7E73D>" + middleLetter + "</color>" + lastLetter);

        yield return new WaitForSeconds(1.5f);

        shade.SetActive(false);

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
        }
    }
}