using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivityManliness : Activity
{
    TMP_InputField inputField;

    private void Awake() {
        inputField = GetComponentInChildren<TMP_InputField>();
    }

    public override bool VerifyWinCondition() {
        return inputField.text.Length >= 25;
    }
}
