using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlendWithEController : MonoBehaviour
{
    public GameObject[] imagesObjs;
    public GameObject[] textObjs;
    public GameObject[] _particleSystems;
    public AudioClip[] _unBlendedaudioClips;
    public AudioClip[] _blendedAudioClips;
    public DotNavigation dotNavObj;
    public AudioClip clickAudioClip;
    public AudioSource audioSource;
    public AnimationClip AC_slideLeftToFrame;
    public AnimationClip AC_slideRightToFrame;
    public AnimationClip AC_slideFrameToLeft;

    int counter = 0;
    bool questionTransition = false;

    void EnableNextActivityObject() {
        if((counter + 1) >= 6) return;

        dotNavObj.OnClickNextButton();

        imagesObjs[counter].GetComponent<Animator>().Play("slide_frame_to_left_move");

        counter++;

        imagesObjs[counter].SetActive(true);
        imagesObjs[counter].GetComponent<Animator>().Play("slide_right_to_frame_move");
    }

    void EnablePreviousActivityObject() {
        if((counter - 1) < 0) return;

        dotNavObj.OnClickBackButton();

        imagesObjs[counter].GetComponent<Animator>().Play("slide_frame_to_right");

        counter--;

        ResetActivity();
        imagesObjs[counter].SetActive(true);
        imagesObjs[counter].GetComponent<Animator>().Play("slide_left_to_frame");
    }

    public void ActivityClicked() {
        _particleSystems[counter].SetActive(true);
        _particleSystems[counter].GetComponent<ParticleSystem>().Play();
        questionTransition = true;
        // audioSource.PlayOneShot(clickAudioClip);
        audioSource.PlayOneShot(_blendedAudioClips[counter]);

        if(textObjs[counter].GetComponent<TextMeshProUGUI>().text[0] == textObjs[counter].name[0]) return;

        textObjs[counter].GetComponent<TextMeshProUGUI>().text = textObjs[counter].name[0] + textObjs[counter].GetComponent<TextMeshProUGUI>().text;

        StartCoroutine(WaitForSomeTime(2f));
    }

    public void OnNameBoardClicked()
    {
        if(textObjs[counter].GetComponent<TextMeshProUGUI>().text[0] == '<') 
        {
            audioSource.PlayOneShot(_unBlendedaudioClips[counter]);
            return;
        }

        audioSource.PlayOneShot(_blendedAudioClips[counter]);
    }

    IEnumerator WaitForSomeTime(float waitFor) {
        yield return new WaitForSeconds(waitFor);
        questionTransition = false;
        EnableNextActivityObject();
        _particleSystems[counter].SetActive(false);
    }

    void ResetActivity() {
        if(textObjs[counter].GetComponent<TextMeshProUGUI>().text[0] == '<') return;

        textObjs[counter].GetComponent<TextMeshProUGUI>().text = textObjs[counter].GetComponent<TextMeshProUGUI>().text.Substring(1);
    }

    public void NextQues() {
        if(questionTransition) return;

        EnableNextActivityObject();
    }

    public void PrevioudQues() {
        if(questionTransition) return;

        EnablePreviousActivityObject();
    }
}
