﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class scrollwithbutton : MonoBehaviour
{
    public Scrollbar Target;
    public Button TheOtherButton;
    public float Step = 0.1f;

    public void Increment()
    {
        if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
        //Target.value = Mathf.Clamp(Target.value + Step, 0, 1);    //Edited by emerson

        // GetComponent<Button>().interactable = Target.value != 1;
        // TheOtherButton.interactable = true;
    }

    public void Decrement()
    {
        if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
        Target.value = Mathf.Clamp(Target.value - Step, 0, 1);
        //  GetComponent<Button>().interactable = Target.value != 0; ;
        // TheOtherButton.interactable = true;
    }
}