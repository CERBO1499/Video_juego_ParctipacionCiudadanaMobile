using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActivityWearing : Activity
{
    private bool selectionMade;
    private Toggle[] toggles;
    private Image[] images;
    private TMP_InputField inputField;

    private void Awake()
    {
        toggles = GetComponentsInChildren<Toggle>();

        inputField = GetComponentInChildren<TMP_InputField>();

        images = new Image[toggles.Length];

        for (int i = 0; i < images.Length; i++)
        {
            images[i] = toggles[i].GetComponentInChildren<Image>();
        }

        selectionMade = false;
    }

    public void SelectionMade(Image tgglBackground)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].color = Vector4.zero;
        }

        selectionMade = true;
        tgglBackground.color = new Color(0f, 1f, 0f, 0.5f);
    }

    public override bool VerifyWinCondition()
    {
        return selectionMade == true && inputField.text.Length >= 15;
    }
}
