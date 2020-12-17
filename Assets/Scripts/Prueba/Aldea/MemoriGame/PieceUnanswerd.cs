using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceUnanswerd : MonoBehaviour
{
    #region Information 
    public static PieceUnanswerd firstPieceAnswered;
    public static PieceUnanswerd secondPieceAnswered;

    [Header("Piece information")]  
   
    [SerializeField]
    RectTransform PieceBase;
    [SerializeField]
    Sprite pieceDiscovered, sprPieceBase;

    bool finishActivitie = false;
    public string PieceDiscoveredName { get {return  pieceDiscovered.name; } }

    public bool FinishActivitie { get => finishActivitie; set => finishActivitie = value; }

    [SerializeField]
    AnimationCurve curveRotation;
    [SerializeField]
    AnimationCurve curveDesapearImgs;

    Image imgPieceBase;

    [Header("Informacion Actividad Completada")]
    [SerializeField]
    RectTransform finalImage;
    [SerializeField]
    RectTransform PanelGame;

   
    #endregion
    #region Components
    [SerializeField]
    ManagerMemoriAldea managerMemoryAldea;    
    #endregion

    private void Start()
    {
        imgPieceBase = PieceBase.GetComponent<Image>();
    }

    public void PieceSelected()
    {
        StartCoroutine(RotatePiece(() =>
        {
            if (firstPieceAnswered == null)
                firstPieceAnswered = this;
            else if (secondPieceAnswered == null)
            {
                secondPieceAnswered = this;
                managerMemoryAldea.ValidateMemoriAldea(firstPieceAnswered, secondPieceAnswered);
                firstPieceAnswered = null;
                secondPieceAnswered = null;
            }
        }));

        StartCoroutine(ApearImageDiscovered());
    }


    public IEnumerator ApearImageBase()
    {
        yield return new WaitForSeconds(0.425f);
        imgPieceBase.sprite = sprPieceBase;
    }
    IEnumerator RotatePiece(System.Action output)
    {
        Vector3 initialRotation = Vector3.zero;
        Vector3 finalRotation = new Vector3(0f, -180f, 0f);

        float t = Time.time;

        while (Time.time <= 1.3f + t)
        {
            PieceBase.localEulerAngles = initialRotation + ((finalRotation - initialRotation) * curveRotation.Evaluate((Time.time - t) / 1.3f));
            yield return null;
        }

        PieceBase.localEulerAngles = finalRotation;

        output();
    }
    public IEnumerator ReturnRotate()
    {
        Vector3 initialRotation = new Vector3(0f, -180f, 0f);
        Vector3 finalRotation = Vector3.zero;

        float t = Time.time;

        while (Time.time <= 1.3f + t)
        {
            PieceBase.localEulerAngles = initialRotation + ((finalRotation - initialRotation) * curveRotation.Evaluate((Time.time - t) / 1.3f));
            yield return null;
        }

        PieceBase.localEulerAngles = finalRotation;

       
    }
    IEnumerator ApearImageDiscovered()
    {
        yield return new WaitForSeconds(0.425f);
        imgPieceBase.sprite = pieceDiscovered;
    }

    public IEnumerator ImageCompleteCreace()
    {
        Vector3 initialRotation = Vector3.one;
        Vector3 finalRotation = new Vector3(1.3f, 1.3f, 1.3f);

        float t = Time.time;

        while (Time.time <= 0.5f + t)
        {
            PieceBase.localScale = initialRotation + ((finalRotation - initialRotation) * curveDesapearImgs.Evaluate((Time.time - t) / 0.5f));
            yield return null;
        }

        PieceBase.localScale = finalRotation;
        StartCoroutine(CloseImageComplete());
       
    }

    IEnumerator CloseImageComplete()
    {
        Vector3 initialRotation = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 finalRotation = Vector3.zero;

        float t = Time.time;

        while (Time.time <= 0.5f + t)
        {
            PieceBase.localScale = initialRotation + ((finalRotation - initialRotation) * curveDesapearImgs.Evaluate((Time.time - t) / 0.5f));
            yield return null;
        }

        PieceBase.localScale = finalRotation;
        if (managerMemoryAldea.CounterCorrect > 8)
        {            
            PanelGame.gameObject.SetActive(false);
            finalImage.gameObject.SetActive(true);
        }
    }
}
