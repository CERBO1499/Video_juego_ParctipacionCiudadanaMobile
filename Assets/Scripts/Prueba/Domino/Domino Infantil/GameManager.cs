using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] Transform parentToPieces;
        [SerializeField] ScrollRect scrollToUnactive;
        [SerializeField] Transform parentToReturn;
        [SerializeField] RectTransform yourTurnImg;
        [SerializeField] AnimationCurve curveTurn;

        public RectTransform Ppieces { get { return pieces; } }
        public Transform ParentToPieces { get => parentToPieces; set => parentToPieces = value; }
        public ScrollRect ScrollToUnactive { get => scrollToUnactive; set => scrollToUnactive = value; }
        public Transform ParentToReturn { get => parentToReturn; set => parentToReturn = value; }
        #endregion

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            DistributePiecesAleatori();
        }

        private void OnEnable()
        {
            StartCoroutine(ShowTurnCoroutine());
        }
        IEnumerator ShowTurnCoroutine()
        {
            yield return new WaitForSeconds(2f);

            yourTurnImg.gameObject.SetActive(true);

            Vector2 iniSize = Vector2.zero;

            Vector2 finiSize = new Vector2(2f,2f);

            float t = Time.time;

            while (Time.time <= t + 3f)
            {
                yourTurnImg.localScale = iniSize + ((finiSize - iniSize) * curveTurn.Evaluate((Time.time) - t) / 3f);
                yield return null;
            }

            yourTurnImg.localScale = iniSize;
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