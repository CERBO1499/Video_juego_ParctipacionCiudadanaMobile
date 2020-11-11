using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    #region Information
    [Header("Information")]
    [SerializeField]
    List<RectTransform> pieces;
    List<int> get;
    [SerializeField]
    List<PuzzlePieceId> ObjToActiveCuestion;
    [SerializeField]
    GameObject
    CompleteImage;

    int Counter;
   
    public void Add(int id)
    {
        get.Add(id);
    }
    [SerializeField]
    AnimationCurve curve;
    #endregion




    void Awake()
    {
        get = new List<int>();
        CompleteImage.SetActive(false);
    }

    void OnEnable()
    {
        bool ActiveimageFinal=true;
        for (int i = 0; i < pieces.Count; i++)
        {
            if (get.Contains(i + 1))
                ActivePiece(i);
            else
            {
                if (PlayerPrefs.HasKey("Puzzle Piece " + (i + 1).ToString()))
                {
                    Debug.Log("Puzzle Piece " + (i + 1).ToString() + " " + PlayerPrefs.GetString("Puzzle Piece " + (i + 1).ToString()));

                    pieces[i].gameObject.SetActive(PlayerPrefs.GetString("Puzzle Piece " + (i + 1).ToString()) == "true");

                    pieces[i].localScale = Vector3.one;

                    
                }
                else
                    Debug.LogWarning("Puzzle Piece " + (i + 1).ToString() + " :No existe");
            }
            if (PlayerPrefs.GetString("Puzzle Piece " + (i + 1).ToString()) == "true")
            {
                Counter++;
                print("Counter = " + Counter);
                ActiveimageFinal = false;
            }
        }

        if (ActiveimageFinal) CompleteImage.SetActive(true);

        get.Clear();

        for(int i = 0; i < ObjToActiveCuestion.Count; i++)
        {
            if (PlayerPrefs.GetString("Puzzle Piece " + (ObjToActiveCuestion[i].Id).ToString())=="true")
            {
                ObjToActiveCuestion[i].gameObject.SetActive(false);
                
            }
            else ObjToActiveCuestion[i].gameObject.SetActive(true);
        }

    }

    public void ActivePiece(int piece)
    {
        if (piece < pieces.Count)
        {
            StartCoroutine(ActivePieceCorotuine(pieces[piece]));
        }
        else
            Debug.LogWarning("La pieza numero " + piece.ToString() + " no existe");
    }

    IEnumerator ActivePieceCorotuine(RectTransform piece)
    {
        piece.gameObject.SetActive(true);

        Vector3 initialLocalScale = Vector3.zero;

        Vector3 finalLocalScale = Vector3.one;

        float t = Time.time;

        while (Time.time <= t + 0.5f)
        {
            piece.localScale = initialLocalScale + ((finalLocalScale - initialLocalScale) * curve.Evaluate((Time.time - t) / 0.5f));

            yield return null;
        }

        piece.localScale = finalLocalScale;
    }


}