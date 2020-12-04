using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourPieceEmpty : MonoBehaviour
{
    #region Information
    public static BehaviourPieceEmpty firstPiece;
    public static BehaviourPieceEmpty secondPiece;
    [Header("Piece information")]
    [SerializeField]
    MemoryBoxType type;
    public MemoryBoxType Ptype
    {
        get { return type; }
    }
    [SerializeField]
    RectTransform PieceBase;
    [SerializeField]
    Sprite PieceDiscovered,sprPiecebase;
    public string PieceDiscoveredName
    {
        get { return PieceDiscovered.name; }
    }
    [SerializeField]
    AnimationCurve curveRotation;
    [SerializeField]
    AnimationCurve curvedesapearImage;

    Image imgPiecebase;


    [Header("Informacion final")]
    [SerializeField]
    RectTransform finalImage;
    #endregion

    #region Components
    [SerializeField]
    ManagerMemoriGame managerMemoryGame;
    #endregion

    private void Start()
    {
        imgPiecebase = PieceBase.GetComponent<Image>();
    }
    public void PieceSelected()
    {
        StartCoroutine(RotatePiece(() =>
        {
            if (firstPiece == null)
                firstPiece = this;
            else if (secondPiece == null)
            {
                secondPiece = this;

                managerMemoryGame.Validate(firstPiece, secondPiece);

                firstPiece = null;

                secondPiece = null;
            }
        }));

        StartCoroutine(ApearImageDiscovered());
       
        

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

        if (managerMemoryGame.CounterCorrect1 > 8)
        {
            finalImage.gameObject.SetActive(true);
        }
    }
    public IEnumerator ApearImageBase()
    {
        yield return new WaitForSeconds(0.425f);
        imgPiecebase.sprite = sprPiecebase;
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

        output?.Invoke();
    }
    IEnumerator ApearImageDiscovered()
    {
        yield return new WaitForSeconds(0.425f);
        imgPiecebase.sprite = PieceDiscovered;
    }
    public IEnumerator ImageCompleteCreace()
    {
        Vector3 initialRotation = Vector3.one;
        Vector3 finalRotation = new Vector3(1.3f, 1.3f, 1.3f);

        float t = Time.time;

        while (Time.time <= 0.5f + t)
        {
            PieceBase.localScale = initialRotation + ((finalRotation - initialRotation) * curvedesapearImage.Evaluate((Time.time - t) / 0.5f));
            yield return null;
        }

        PieceBase.localScale = finalRotation;
        StartCoroutine(CloseImageComplete());
    }
    IEnumerator CloseImageComplete()
    {
        Vector3 initialRotation = new Vector3(1.3f, 1.3f, 1.3f);
        Vector3 finalRotation =Vector3.zero;

        float t = Time.time;

        while (Time.time <= 0.5f + t)
        {
            PieceBase.localScale = initialRotation + ((finalRotation - initialRotation) * curvedesapearImage.Evaluate((Time.time - t) / 0.5f));
            yield return null;
        }

        PieceBase.localScale = finalRotation;
    }



}