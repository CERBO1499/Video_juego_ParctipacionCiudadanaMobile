using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivityGenderList : MonoBehaviour, IActivity
{
    #region Components
    GenderListBtnLine[] btnLines;
    #endregion

    #region Information
    [SerializeField] private string activityName;
    public string PactivityName { get; set; }
    #endregion

    private void Awake()
    {
        btnLines = GetComponentsInChildren<GenderListBtnLine>();

        PactivityName = activityName;

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Activates this GameObject.
    /// </summary>
    public void Activate() { 
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Shows feedback panel's variations.
    /// </summary>
    /// <param name="feedback">Variation to show.</param>
    /// <param name="panel">Panel where to show information.</param>
    public void GiveFeedback(int feedback, GameObject panel)
    {
        switch (feedback)
        {
            case 0:
                panel.GetComponentInChildren<TextMeshProUGUI>().text = "Debes marcar todas las respuestas.";
                break;

            case 1:
                panel.GetComponentInChildren<TextMeshProUGUI>().text = "La carga doméstica aún sigue estando mayoritariamente en manos de las mujeres, ayúdanos a cambiar esta situación.";
                break;
        }
    }

    public bool VerifyWinCondition()
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
