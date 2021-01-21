using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Diverdomino
{
    public class GameManager : MonoBehaviour
    {
        #region Static
        public static GameManager instance;
        #endregion

        #region Information
        public bool drag = true;
        [SerializeField] List<PieceDomino> piecesToDistribute;
        [SerializeField] List<PieceDomino> piecesToPlayer;
        [SerializeField] List<PieceDomino> piecesToMachine;
        [SerializeField] RectTransform[] positions;
        #endregion


        private void Awake()
        {
            instance = this;

            DistributePiecesAleatori();
        }
        void DistributePiecesAleatori()
        {
            for (int i = 0; i < 14; i++)
            {
                PieceDomino myPiece = piecesToDistribute[Random.Range(0, piecesToDistribute.Count)];
                piecesToDistribute.Remove(myPiece);
                piecesToPlayer.Add(myPiece);
                myPiece.transform.position = positions[i].transform.position;
            }
            for (int i = 0; i < 14; i++)
            {
                PieceDomino myPiece = piecesToDistribute[Random.Range(0, piecesToDistribute.Count)];
                piecesToDistribute.Remove(myPiece);
                piecesToMachine.Add(myPiece);
            }
        }
    }
}