﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class Utilities : MonoGenericSingleton<Utilities>
{

    public void ANIM_ShowNormal(Transform obj) => obj.DOScale(Vector3.one, 0.5f);

    public void ScaleObject(Transform obj)
    {
        obj.DOScale(Vector3.one * 1.5f, 0f);
    }

    public void ANIM_ShowBounceNormal(Transform obj)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(obj.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f));
        sequence.Append(obj.DOScale(Vector3.one, 0.5f));
        sequence.Play();
    }


    public void ANIM_HideNormal(Transform obj) => obj.DOScale(Vector3.zero, 0.5f);


    public void ANIM_HideBounce(Transform obj)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(obj.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.25f));
        sequence.Join(obj.DOScale(new Vector3(0, 0, 0), 0.5f).SetDelay(0.25f));
        sequence.Play();
    }


    public void ANIM_Move(Transform obj, Vector3 endPos)
    {
        obj.DOMove(endPos, 0.5f);
    }

    public void ANIM_MoveWithScaleUp(Transform obj, Vector3 endPos)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(obj.DOMove(endPos, 0.5f));
        sequence.Join(obj.DOScale(Vector3.one * 1.5f, 0.5f));
        sequence.Play();
    }

    public void ANIM_MoveWithScaleDown(Transform obj, Vector3 endPos)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(obj.DOMove(endPos, 0.5f));
        sequence.Join(obj.DOScale(Vector3.one, 0.5f));
        sequence.Play();
    }

    public void ANIM_CorrectScaleEffect(Transform obj) => StartCoroutine(IENUM_Hearbeat(obj));
    IEnumerator IENUM_Hearbeat(Transform obj)
    {
        for (int i = 0; i < 3; i++)
        {
            obj.DOScale(new Vector3(1.25f, 1.25f, 1), 0.5f);
            yield return new WaitForSeconds(0.25f);
            obj.DOScale(new Vector3(1, 1, 1), 0.5f);
            yield return new WaitForSeconds(0.25f);
        }
    }


    public void ANIM_WrongShakeEffect(Transform obj) => StartCoroutine(IENUM_HeadShake(obj));
    IEnumerator IENUM_HeadShake(Transform obj)
    {
        obj.GetComponent<Button>().interactable = false;
        for (int i = 0; i < 4; i++)
        {
            obj.DOMove(obj.position + new Vector3(0.25f, 0f, 0f), 0.1f);
            yield return new WaitForSeconds(0.1f);
            obj.DOMove(obj.position - new Vector3(0.25f, 0f, 0f), 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        obj.GetComponent<Button>().interactable = true;
    }


    public void ANIM_RotateHide(Transform obj) => obj.DORotate(new Vector3(0, 90, 0), 0.35f);
    public void ANIM_RotateShow(Transform obj) => obj.DORotate(new Vector3(0, 0, 0), 0.35f);

    public void ANIM_ShrinkObject(Transform obj)
    {
        obj.DOScale(Vector3.zero, 0.5f);
    }

    public void ANIM_FlyIn(Transform obj) => obj.DOMoveY(-1.6f, 2f).SetEase(Ease.OutCirc);
    // public void ANIM_FlyIn(Transform obj) => obj.DOMove(new Vector3(obj.transform.position.x, -1.6f, 0), 2f).SetEase(Ease.OutCirc);


}