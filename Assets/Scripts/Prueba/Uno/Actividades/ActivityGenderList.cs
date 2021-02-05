using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActivityGenderList : Activity
{
    #region Components
    GenderListBtnLine[] btnLines;
    #endregion

    private void Awake()
    {
        btnLines = GetComponentsInChildren<GenderListBtnLine>();
    }

    public override bool VerifyWinCondition()
    {
        int questionsAnswered = 0;

        for (int i = 0; i < btnLines.Length; i++)
        {
            if (btnLines[i].PisAnswered == true)
            {
                questionsAnswered++;
            }
        }

        return questionsAnswered == btnLines.Length;
    }
}
