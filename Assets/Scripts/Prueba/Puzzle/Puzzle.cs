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
    }

    void OnEnable()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (get.Contains(i + 1))
                ActivePiece(i);
            else
            {
                if (PlayerPrefs.HasKey("Puzzle Piece " + (i + 1).ToString()))
                {
                    Debug.Log("Puzzle Piece " + (i + 1).ToString() + " :Existe");

                    pieces[i].gameObject.SetActive(PlayerPrefs.GetString("Puzzle Piece " + (i + 1).ToString()) == "true");
                }
                else
                    Debug.LogWarning("Puzzle Piece " + (i + 1).ToString() + " :No existe");
            }
        }

        get.Clear();
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
        Vector3 initialLocalScale = Vector3.zero;

        Vector3 finalLocalScale = piece.localScale;

        float t = Time.time;

        while (t <= Time.time + 0.5f)
        {
            piece.localScale = initialLocalScale + ((finalLocalScale - initialLocalScale) * curve.Evaluate((Time.time - t) * 0.5f));

            yield return null;
        }

        piece.localScale = finalLocalScale;
    }
}