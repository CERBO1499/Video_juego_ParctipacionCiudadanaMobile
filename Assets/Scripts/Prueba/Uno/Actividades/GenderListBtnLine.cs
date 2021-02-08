using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Uno
{
    public class GenderListBtnLine : MonoBehaviour
    {
        #region Information
        private bool isAnswered;
        #endregion

        #region Components
        private Button[] buttons;
        #endregion

        #region Properties
        public bool PisAnswered { get => isAnswered; private set => isAnswered = value; }
        #endregion

        private void Awake()
        {
            buttons = GetComponentsInChildren<Button>();

            PisAnswered = false;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }

        public void MarkButton(Button btn)
        {
            btn.GetComponentInChildren<TextMeshProUGUI>().text = "X";

            PisAnswered = true;

            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] != btn)
                {
                    buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }
    }
}