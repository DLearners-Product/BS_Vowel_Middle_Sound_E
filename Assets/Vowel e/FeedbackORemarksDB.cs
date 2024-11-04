using System.Collections.Generic;
using UnityEngine.UI;
using System;

[Serializable]
public class FeedbackORemarksDB
{
    public string other_remarks;

    public FeedbackORemarksDB(string other_remarks)
    {
        this.other_remarks = other_remarks;
    }
}