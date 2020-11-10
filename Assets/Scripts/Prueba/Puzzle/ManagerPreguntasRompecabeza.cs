using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPreguntasRompecabeza : MonoBehaviour
{
    #region Information
    [SerializeField] int CorrectAns;
    [SerializeField] GameObject FeedBackGood;
    [SerializeField] GameObject FeedBackBad;
    [SerializeField] PuzzlePieceId idObject;

    bool responseGood=false;
    #endregion
    #region Components
    [SerializeField]
    Puzzle puzzle;
    #endregion

    private void Start()
    {
        FeedBackGood.SetActive(false);
        FeedBackBad.SetActive(false);
    }
    void CheckCorrectAns()
    {
        if (responseGood)
        {
            puzzle.Add(idObject.Id);
            PlayerPrefs.SetString("Puzzle Piece " + idObject.Id, "true");
            idObject.gameObject.SetActive(false);
        }
    }

    public void UserResponse(int response)
    {
        if (response == CorrectAns)
        {
            FeedBackGood.SetActive(true);
            responseGood = true;           
        }
        else 
        {
            FeedBackBad.SetActive(true);
            responseGood = false;
        }

        CheckCorrectAns();
    }
}
