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
        const float SECS_TO_BEFORE_ANIMATION = 0.5f;

        public bool drag = true;
        public bool firstPiece = false;
        [SerializeField] List<PieceDomino> piecesToDistribute;
        [SerializeField] List<PieceDomino> piecesToPlayer;
        [SerializeField] RectTransform pieces;
        [SerializeField] List<PieceDomino> piecesToMachine;
        [SerializeField] Transform parentToPieces;
        [SerializeField] ScrollRect scrollToUnactive;
        [SerializeField] Transform parentToReturn;
        [SerializeField] RectTransform yourTurnImg;
        [SerializeField] RectTransform enemyTurnImg;
        [SerializeField] AnimationCurve curveTurn;


        public RectTransform Ppieces { get { return pieces; } }
        public Transform ParentToPieces { get => parentToPieces; set => parentToPieces = value; }
        public ScrollRect ScrollToUnactive { get => scrollToUnactive; set => scrollToUnactive = value; }
        public Transform ParentToReturn { get => parentToReturn; set => parentToReturn = value; }
        public List<PieceDomino> PiecesToMachine { get => piecesToMachine; set => piecesToMachine = value; }
        public List<PieceDomino> PiecesToPlayer { get => piecesToPlayer; set => piecesToPlayer = value; }
        #endregion


        #region Events
        public System.Action OnTurnPlayer;
        public System.Action OnTurnMachine;
        public System.Action OnPassBtn;
        #endregion
        private void Awake()
        {
            instance = this;

            OcultSinglePieces();

            for (int i = 0; i < piecesToDistribute.Count; i++)
            {
                piecesToDistribute[i].OnFirstPiece += ResetSinglePieces;
            }

            PieceDomino.OnPieceInPlace -= ChangeTurn;
            PieceDomino.OnPieceInPlace += ChangeTurn;
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
            yield return new WaitForSeconds(SECS_TO_BEFORE_ANIMATION);

            yourTurnImg.gameObject.SetActive(true);

            Vector2 iniSize = Vector2.zero;

            Vector2 finiSize = new Vector2(4f, 4f);

            float t = Time.time;

            while (Time.time <= t + 3f)
            {
                yourTurnImg.localScale = iniSize + ((finiSize - iniSize) * curveTurn.Evaluate((Time.time) - t) / 3f);
                yield return null;
            }

            yourTurnImg.localScale = iniSize;
        }
        IEnumerator ShowEnemyTurnCoroutine()
        {
            yield return new WaitForSeconds(SECS_TO_BEFORE_ANIMATION);

            enemyTurnImg.gameObject.SetActive(true);

            Vector2 iniSize = Vector2.zero;

            Vector2 finiSize = new Vector2(6f, 6f);

            float t = Time.time;

            while (Time.time <= t + 3f)
            {
                enemyTurnImg.localScale = iniSize + ((finiSize - iniSize) * curveTurn.Evaluate((Time.time) - t) / 3f);
                yield return null;
            }

            enemyTurnImg.localScale = iniSize;
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
                myPiece.SetAsMachinePiece();
                PiecesToMachine.Add(myPiece);
            }
        }

        void OcultSinglePieces()
        {
            Image pieceImage;

            for (int i = 0; i < piecesToDistribute.Count; i++)
            {
                switch (piecesToDistribute[i].TypeOfPiece)
                {
                    case TypePiece.Double:
                        break;
                    case TypePiece.Single:
                        pieceImage = piecesToDistribute[i].GetComponent<Image>();
                        pieceImage.raycastTarget = false;
                        pieceImage.color = new Color(1f, 1f, 1f, 0.5f);
                        break;
                    default:
                        break;
                }
            }

        }

        void ResetSinglePieces()
        {
            for (int i = 0; i < piecesToPlayer.Count; i++)
            {
                piecesToPlayer[i].GetComponent<Image>();
                piecesToPlayer[i].GetComponent<Image>().raycastTarget = true;
                piecesToPlayer[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
            for (int i = 0; i < PiecesToMachine.Count; i++)
            {
                PiecesToMachine[i].GetComponent<Image>();
                PiecesToMachine[i].GetComponent<Image>().raycastTarget = true;
                PiecesToMachine[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
        }

        void ChangeTurn(GameObject obj, Side side, bool isUserTurn)
        {
            if (obj != null) PiecesToPlayer.Remove(obj.GetComponent<PieceDomino>());

            if (isUserTurn == false)
            {
                StartCoroutine(ShowEnemyTurnCoroutine());
            }
            else
            {
                StartCoroutine(ShowTurnCoroutine());
            }

            foreach (PieceDomino domino in PiecesToPlayer)
            {
                domino.SetBlock(!isUserTurn);
            }
        }

        public void PassTurnButton() {
            ChangeTurn(null, Side.Izq, false);

            OnPassBtn?.Invoke();
        }
    }

}