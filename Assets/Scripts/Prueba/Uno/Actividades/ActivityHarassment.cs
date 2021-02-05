using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivityHarassment : Activity
{
    private bool selectionMade;
    TMP_InputField inputField;

    private void Awake()
    {
        selectionMade = false;
        inputField = GetComponentInChildren<TMP_InputField>();
    }

    public void SelectionMade() {
        selectionMade = true;
    }

    public override bool VerifyWinCondition() {
        return selectionMade && inputField.text.Length > 0;
    }
}