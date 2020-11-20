using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumPieceId : MonoBehaviour
{
    #region Information
    [Header("Information")]
    [SerializeField]
    int idPiece;
    [SerializeField]
    bool get;

    public int IdPiece { get => idPiece; set => idPiece = value; }
    #endregion

    private void Start()
    {
        if (PlayerPrefs.HasKey("Album Piece" + idPiece.ToString()))
            get = PlayerPrefs.GetString("Album Piece" + idPiece.ToString()) == "true";
        else
            PlayerPrefs.SetString("Album Piece" + idPiece.ToString(), "false");
    }
}
