using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActivityIntimity : Activity
{
    #region Information
    private bool[] selectionsDone;
    #endregion

    #region Properties
    string PactivityName { get; set; }
    bool Pdone { get; set; }
    #endregion

    private void Awake() {
        selectionsDone = new bool[2];
    }

    public void FirstSelectionMade() {
        selectionsDone[0] = true;
    }

    public void SecondSelectionMade() {
        selectionsDone[1] = true;
    }

    public override bool VerifyWinCondition() {
        return selectionsDone[0] == true && selectionsDone[1] == true;
    }
}
