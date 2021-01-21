using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Information
    [SerializeField] List <PieceDomino> piecesToDistribute; 
    [SerializeField] List <PieceDomino> piecesToPlayer;
    [SerializeField] List <PieceDomino> piecesToMachine;
    #endregion
    

    private void Awake()
    {
        DistributePiecesAleatori();
    }
    void DistributePiecesAleatori()
    {        
        for (int i = 0; i < 14; i++)
        {
            PieceDomino myPiece = piecesToDistribute[Random.Range(0, piecesToDistribute.Count)];
            piecesToDistribute.Remove(myPiece);
            piecesToPlayer.Add(myPiece);
        }
        for (int i = 0; i < 14; i++)
        {
            PieceDomino myPiece = piecesToDistribute[Random.Range(0, piecesToDistribute.Count)];
            piecesToDistribute.Remove(myPiece);
            piecesToMachine.Add(myPiece);
        }
    }
}
