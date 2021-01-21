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
        [SerializeField] RectTransform pieces;
        [SerializeField] List<PieceDomino> piecesToMachine;
        #endregion

        private void Awake()
        {
            instance = this;
        }

        private void Start()
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

                myPiece.Prect.SetParent(pieces);
                pieces.sizeDelta = new Vector2(pieces.childCount * 315f + pieces.GetComponent<UnityEngine.UI.HorizontalLayoutGroup>().spacing, pieces.sizeDelta.y);

                myPiece.gameObject.SetActive(true);
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