using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Information
    [SerializeField] RectTransform [] piecesToDistribute;
    [SerializeField] RectTransform [] piecesPlayer;
    [SerializeField] RectTransform [] piecesMachine;
    #endregion

    #region Components
    [SerializeField] PieceDomino [] idPieceDomino;
    #endregion

    private void Awake()
    {
        for (int i = 0; i < piecesToDistribute.Length; i++)
        {
            idPieceDomino[i] = piecesToDistribute[i].GetComponent<PieceDomino>();
        }
        DistributePiecesAleatori();
    }
    void DistributePiecesAleatori()
    {
        List<int> tmpNumbers = new List<int>();
        int number;

        for (int i = 0; i < piecesToDistribute.Length; i++)
        {
            do
            {
                number = Random.Range(0, 27);
            } while (tmpNumbers.Contains(number));
            tmpNumbers.Add(number);

            if(idPieceDomino[i].IdNumber == number)
            {

            }
                           
        }

        
       
    }
}
