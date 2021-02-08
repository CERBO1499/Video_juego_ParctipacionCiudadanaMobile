using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityLines : Activity
{
    #region Components
    [SerializeField] private GameObject[] options;
    #endregion

    #region Information
    private bool[] optionsSelected;
    #endregion

    private void Awake() {
        optionsSelected = new bool[options.Length];

        for (int i = 0; i < optionsSelected.Length; i++) {
            optionsSelected[i] = false;
        }
    }

    public void SelectOption(GameObject obj) { 
        for(int i = 0; i < optionsSelected.Length; i++) {
            if (obj == options[i]) {
                optionsSelected[i] = true;
            }
        }
    }

    public override bool VerifyWinCondition() {
        bool result = false;
        int selectedCount = 0;

        for (int i = 0; i < optionsSelected.Length; i++) {
            if(optionsSelected[i] == true) selectedCount++;
        }

        result = selectedCount == optionsSelected.Length;

        if(result == true) { 
            foreach (Transform obj in gameObject.transform) {
                if (obj.CompareTag("Lines")) obj.gameObject.SetActive(false);
            }
        }

        Debug.Log("Selected count: " + selectedCount);

        return result;
    }
}
