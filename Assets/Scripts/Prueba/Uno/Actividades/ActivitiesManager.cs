using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Uno;

public class ActivitiesManager : MonoBehaviour
{
    #region Information
    private static ActivitiesManager instance;
    #endregion

    #region Components
    [SerializeField] GameObject feedbackPanel;
    [SerializeField] GameObject confirmButton;

    private Image activitiesPanel;
    private Activity[] activities;
    #endregion

    #region Properties
    private Activity CurrentActivity { get; set; }
    public static ActivitiesManager Pinstance { get => instance; }
    #endregion

    private void Awake()
    {
        instance = this;

        activitiesPanel = GetComponent<Image>();
        activitiesPanel.raycastTarget = false;

        activities = GetComponentsInChildren<Activity>();

        CurrentActivity = null;

        for (int i = 0; i < activities.Length; i++)
        {
            activities[i].IsActive(false);
        }

        feedbackPanel.SetActive(false);
        confirmButton.SetActive(false);
    }

    /// <summary>
    /// Activate an activity based on its name.
    /// </summary>
    /// <param name="activityID">Activitie's name.</param>
    public void ShowActivity(int activityID)
    {
        for (int i = 0; i < activities.Length; i++)
        {
            if (activityID == activities[i].PactivityID)
            {
                confirmButton.SetActive(true);
                activities[i].IsActive(true);
                CurrentActivity = activities[i];
                activitiesPanel.raycastTarget = true;
                break;
            }
        }
    }

    /// <summary>
    /// Verify an give feedback about activity's win condition.
    /// </summary>
    public void FinishButton()
    {
        if (CurrentActivity != null)
        {
            CurrentActivity.Pdone = CurrentActivity.VerifyWinCondition();
            CurrentActivity.GiveFeedback((CurrentActivity.Pdone ? 0 : 1), feedbackPanel);

            //Debug.Log($"La actividad {CurrentActivity.GetType()} se encuentra en estado: {CurrentActivity.Pdone}");
        }
    }

    /// <summary>
    /// Close feedback window whether activity isn't done.
    /// </summary>
    public void CloseFeedback()
    {
        feedbackPanel.SetActive(false);

        if (CurrentActivity.Pdone == true)
        {
            Uno.GameManager.instance.ChangeTurn(false);

            if (GameManager.instance.PlayerTurn == false) {
                StartCoroutine(Uno.GameManager.instance.MachinePlayCoroutine());
            }
         
            activitiesPanel.raycastTarget = false;

            foreach (Transform obj in gameObject.transform)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}
