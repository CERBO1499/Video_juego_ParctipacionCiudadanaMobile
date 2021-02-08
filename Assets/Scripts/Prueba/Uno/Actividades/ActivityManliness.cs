using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Uno { 
public class ActivityManliness : Activity
    {
        #region Constants
        const int MIN_CHARACTERES = 1;
        #endregion

        #region Information
        TMP_InputField inputField;
        #endregion

        private void Awake() {
            inputField = GetComponentInChildren<TMP_InputField>();
        }

        public override bool VerifyWinCondition() {
            return inputField.text.Length >= MIN_CHARACTERES;
        }
    }
}