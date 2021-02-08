using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Activity : MonoBehaviour
{
    #region Information
    [SerializeField] protected int activityID;
    [SerializeField] protected Sprite feedbackSprite;

    private bool done;
    #endregion

    #region Properties
    public bool Pdone { get => done; set => done = value; }
    public int PactivityID { get => activityID; }
    protected TextMeshProUGUI PfeedbackTxt { get; set; }
    protected Image PfeedbackImg { get; set; }
    #endregion

    /// <summary>
    /// Activates this GameObject.
    /// </summary>
    public void IsActive(bool state) {
        gameObject.SetActive(state);
    }

    /// <summary>
    /// Shows feedback panel's variations.
    /// </summary>
    /// <param name="feedback">Variation to show.</param>
    /// <param name="panel">Panel where to show information.</param>
    public virtual void GiveFeedback(int feedback, GameObject panel) {
        ClearPanel(panel);

        switch (feedback)
        {
            case 0:
                PfeedbackImg.sprite = feedbackSprite;
                PfeedbackImg.color = Vector4.one;
                break;

            case 1:
                PfeedbackTxt.text = "Debes responder las preguntas por completo.";
                break;

            default:
                panel.SetActive(false);
                break;
        }
    }

    public void ClearPanel(GameObject panel) {
        panel.SetActive(true);

        PfeedbackTxt = panel.GetComponentInChildren<TextMeshProUGUI>();
        PfeedbackImg = PfeedbackTxt.GetComponentInChildren<Image>();

        PfeedbackTxt.text = "";
        PfeedbackImg.sprite = null;
        PfeedbackImg.color = Vector4.zero;
    }

    public abstract bool VerifyWinCondition();
}
