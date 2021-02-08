using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Uno
{
    enum NSelectionType { Button, Toggle }
    public class MultipleSelectionBtns : MonoBehaviour
    {
        #region Components
        private Button[] toggles;
        #endregion

        #region Information
        private bool isAnswered;
        #endregion

        #region Properties
        private bool PisAnswered { get => isAnswered; }
        #endregion

        private void Awake()
        {
            toggles = GetComponentsInChildren<Button>();

            isAnswered = false;

            TurnOffOptions();
        }

        public void SelectOption(Button tggl)
        {
            isAnswered = true;

            TurnOffOptions();
        }

        protected void TurnOffOptions()
        {
            for (int i = 0; i < toggles.Length; i++)
            {

            }
        }
    }
}