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
    [SerializeField] GameObject CloseQuestion;

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
            StartCoroutine(waitForClose());
            
        }
        else 
        {
            FeedBackBad.SetActive(true);
            responseGood = false;
            StartCoroutine(waitForClose());
        }

        CheckCorrectAns();
    }


    IEnumerator waitForClose()
    {
        yield return new WaitForSeconds(0.4f);
        CloseQuestion.SetActive(false);
        FeedBackBad.SetActive(false);
        FeedBackGood.SetActive(false);
    }
}
