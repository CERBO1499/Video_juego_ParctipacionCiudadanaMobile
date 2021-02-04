using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivitiesManager : MonoBehaviour
{
    #region Interfaces
    IActivity[] activities;
    #endregion

    #region Properties
    IActivity CurrentActivity { get; set; }
    #endregion

    public void ShowActivity(string activityName) {
        for(int i = 0; i < activities.Length; i++) { 
            if(activityName == activities[i].PactivityName) {
                activities[i].Activate();
                CurrentActivity = activities[i];
            }
        }
    }

    public void FinishButton() {
        CurrentActivity.VerifyWinCondition();
    }
}
