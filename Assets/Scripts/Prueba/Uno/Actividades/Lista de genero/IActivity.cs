using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivity
{
    string PactivityName { get; set; }

    void Activate();

    void GiveFeedback(int feedback, GameObject panel);

    bool VerifyWinCondition();
}