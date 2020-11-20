using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Album : MonoBehaviour
{
    #region Information
    int page = 0;
    [Header("Pieces album", order = 0)]
    [SerializeField]
    List<RectTransform> piecesAlbum;
    [SerializeField]
    List<AlbumPieceId> objToActiveAlbum;
    [SerializeField]
    AnimationCurve curve;
    List <int> get;
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] RectTransform pages;
    [SerializeField] Button back, pass;
    #endregion

    public void Pass()
    {
        if (page + 1 < pages.childCount)
        {
            page++;

            pages.GetChild(page - 1).gameObject.SetActive(false);

            pages.GetChild(page).gameObject.SetActive(true);

            if (page == pages.childCount - 1)
                pass.interactable = false;
            else
            {
                if (!back.interactable)
                    back.interactable = true;
            }
        }
    }

    public void Back()
    {
        if (page - 1 >= 0)
        {
            page--;

            pages.GetChild(page + 1).gameObject.SetActive(false);

            pages.GetChild(page).gameObject.SetActive(true);

            if (page == 0)
                back.interactable = false;
            else
            {
                if (!pass.interactable)
                    pass.interactable = true;
            }
        }
    }

    //Logica del juego
    public void Add(int id)
    {
        get.Add(id);
    }
    void Awake()
    {
        get = new List<int>();   
    }
    private void OnEnable()
    {
        for (int i = 0; i < piecesAlbum.Count; i++)
        {
            if (get.Contains(i + 1)) ActivePieceAlbum(i);
            else
            {
                if (PlayerPrefs.HasKey("Album Piece" + (i + 1).ToString()))
                {
                    Debug.Log("Album pieces" + (i + 1).ToString() + "" + PlayerPrefs.GetString("Album pieces" + (i + 1).ToString()));
                    piecesAlbum[i].gameObject.SetActive(PlayerPrefs.GetString("Album Piece" + (i + 1).ToString()) == "true");
                    piecesAlbum[i].localScale = Vector3.one;
                }
                else Debug.LogWarning("Album piece" + (i + 1).ToString() + ": No existe");
            }
            if (PlayerPrefs.GetString("Album Piece" + (i + 1).ToString()) == "false")
            {
               
            } 
        }

        get.Clear();
        for (int i = 0; i < objToActiveAlbum.Count; i++)
        {
            if (PlayerPrefs.GetString("Album Piece" + (objToActiveAlbum[i].IdPiece).ToString()) == "true")
            objToActiveAlbum[i].gameObject.SetActive(false);
            else objToActiveAlbum[i].gameObject.SetActive(true);            
        }
    }

    public void ActivePieceAlbum(int _pieceAlbum)
    {
        if (_pieceAlbum < piecesAlbum.Count)
        {
            StartCoroutine(ActivePieceAlbum(piecesAlbum[_pieceAlbum]));
        }
        else Debug.LogWarning("La pieza numero" + _pieceAlbum.ToString() + "no existe");

    }

    IEnumerator ActivePieceAlbum(RectTransform _pieceAlbum)
    {
        _pieceAlbum.gameObject.SetActive(true);
        Vector3 initialScale = Vector3.zero;
        Vector3 finalScale = Vector3.one;

        float t = Time.time;

        while (Time.time <= t + 0.5f)
        {
            _pieceAlbum.localScale = initialScale + ((finalScale - initialScale) * curve.Evaluate((Time.time - t) / 0.5f));
            yield return null;
        }
        _pieceAlbum.localScale = finalScale;
    }
}