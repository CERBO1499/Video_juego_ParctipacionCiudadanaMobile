using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceId : MonoBehaviour
{
    #region Information
    [Header("Information")]
    [SerializeField]
    int id;
    [SerializeField]
    bool get;

    public int Id { get => id; set => id = value; }
    #endregion

    void Start()
    {
        if (PlayerPrefs.HasKey("Puzzle Piece " + id.ToString()))
            get = PlayerPrefs.GetString("Puzzle Piece " + id.ToString()) == "true";
        else
            PlayerPrefs.SetString("Puzzle Piece " + id.ToString(), "false");
    }
}