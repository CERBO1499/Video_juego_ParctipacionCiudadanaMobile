using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Uno
{
    public class ActivityHarassment : Activity
    {
        #region Constants
        const int MIN_CHARACTERES = 1;
        #endregion

        #region Information
        private bool selectionMade;
        TMP_InputField inputField;
        #endregion

        private void Awake()
        {
            selectionMade = false;
            inputField = GetComponentInChildren<TMP_InputField>();
        }

        public void SelectionMade()
        {
            selectionMade = true;
        }

        public override bool VerifyWinCondition()
        {
            return selectionMade == true && inputField.text.Length >= MIN_CHARACTERES;
        }
    }
}