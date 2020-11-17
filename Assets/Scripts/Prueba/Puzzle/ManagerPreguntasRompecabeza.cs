using System.Collections;
using UnityEngine;

public class ManagerPreguntasRompecabeza : MonoBehaviour
{
    #region Information
    [Header("Numero respuesta correta a incorrecta", order = 0)]
    [SerializeField]
    int CorrectAns;
    [SerializeField]
    int mediumAns;
    [SerializeField]
    int LowAns;
    [Space(order = 1)]

    [Header("FeedBacks", order = 2)]
    [SerializeField]
    RectTransform FeedBackGood;
    [SerializeField]
    RectTransform MediumFeedBack;
    [SerializeField]
    RectTransform FeedBackBad;
    [Space(order = 3)]

    [Header("Other", order = 4)]
    [SerializeField] PuzzlePieceId idObject;
    [SerializeField] GameObject CloseQuestion;
    [Space(order = 5)]

    [Header("Botones de respuesta", order = 6)]
    [SerializeField]
    GameObject [] Botones;


    Vector3 scaleFeedBackGood;
    Vector3 scaleFeedBackMedium;
    Vector3 scaleFeedBackBad;
    [SerializeField]
    AnimationCurve curve;

    #endregion
    #region Components
    [SerializeField]
    Puzzle puzzle;
    [SerializeField]
    ControlSemilla ctrlSemilla;
    #endregion

    private void Start()
    {

        scaleFeedBackGood = FeedBackGood.localScale;
        scaleFeedBackMedium = MediumFeedBack.localScale;
        scaleFeedBackBad = FeedBackBad.localScale;
            
        FeedBackGood.gameObject.SetActive(false);
        FeedBackBad.gameObject.SetActive(false);
        MediumFeedBack.gameObject.SetActive(false);
    }
    void CheckCorrectAns()
    {
            puzzle.Add(idObject.Id);
            PlayerPrefs.SetString("Puzzle Piece " + idObject.Id, "true");
            idObject.gameObject.SetActive(false);
        
    }

    public void UserResponse(int response)
    {
        if (response == CorrectAns)
        {

            waitForClose();
            // FeedBackGood.gameObject.SetActive(true);
            StartCoroutine(ActiveFeedBack(FeedBackGood));
            ctrlSemilla.SumarSemillaEnEscena(3);
        }
        else if(response==mediumAns)
        {

            waitForClose();
            // MediumFeedBack.gameObject.SetActive(true);
            StartCoroutine(ActiveFeedBack(MediumFeedBack));
            ctrlSemilla.SumarSemillaEnEscena(2);
           
        }
        else if (response==LowAns)
        {

            waitForClose();
            //FeedBackBad.gameObject.SetActive(true);
            StartCoroutine(ActiveFeedBack(FeedBackBad));
            ctrlSemilla.SumarSemillaEnEscena(1);
        }

        CheckCorrectAns();
    }


    void waitForClose()
    {      

        for (int i = 0; i < Botones.Length; i++)
        {
            Botones[i].SetActive(false);
        }

    }
    IEnumerator ActiveFeedBack(RectTransform txtFedback)
    {
        txtFedback.gameObject.SetActive(true);

        Vector3 initialLocalScale = Vector3.zero;
        Vector3 finalLocalScale = txtFedback.localScale;

        float t = Time.time;

        while (Time.time <= t + 0.5f)
        {
            txtFedback.localScale = initialLocalScale + ((finalLocalScale - initialLocalScale) * curve.Evaluate((Time.time - t)) / 0.5f);
            yield return null;
        }
        txtFedback.localScale = finalLocalScale;
    }

   

}
