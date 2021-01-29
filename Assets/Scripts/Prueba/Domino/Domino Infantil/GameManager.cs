using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Diverdomino
{
    enum PassTurnType {none, first, second}

    public class GameManager : MonoBehaviour
    {
        #region Static
        public static GameManager instance;
        #endregion

        #region Information
        const float SECS_TO_BEFORE_ANIMATION = 0.5f;
        const float ANIMATION_DURATION = 1f;

        public bool drag = true;
        PassTurnType turnPassed = PassTurnType.none;
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
        [SerializeField] Button passBtn;
        [SerializeField] Image winnerImg;

        Coroutine enemyTurn, userTurn;

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

            winnerImg.gameObject.SetActive(false);

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

            Vector2 finiSize = new Vector2(2f, 2f);

            float t = Time.time;

            while (Time.time <= t + ANIMATION_DURATION)
            {
                yourTurnImg.localScale = iniSize + ((finiSize - iniSize) * curveTurn.Evaluate((Time.time) - t) / ANIMATION_DURATION);
                yield return null;
            }

            yourTurnImg.localScale = iniSize;

            passBtn.interactable = true;
        }
        IEnumerator ShowEnemyTurnCoroutine()
        {
            yield return new WaitForSeconds(SECS_TO_BEFORE_ANIMATION);

            enemyTurnImg.gameObject.SetActive(true);

            Vector2 iniSize = Vector2.zero;

            Vector2 finiSize = new Vector2(2f, 2f);

            float t = Time.time;

            while (Time.time <= t + ANIMATION_DURATION)
            {
                enemyTurnImg.localScale = iniSize + ((finiSize - iniSize) * curveTurn.Evaluate((Time.time) - t) / ANIMATION_DURATION);
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
            if (PiecesToPlayer.Count <= 0 || PiecesToMachine.Count <= 0) GameOver = true;

            if (turnPassed == PassTurnType.first) { 
                turnPassed = PassTurnType.none;
                SetPassBtnAlert(false);
            }

            if (obj != null) PiecesToPlayer.Remove(obj.GetComponent<PieceDomino>());

            if (isUserTurn == false)
            {
                passBtn.interactable = false;
                enemyTurn = StartCoroutine(ShowEnemyTurnCoroutine());
            }
            else
            {
                userTurn = StartCoroutine(ShowTurnCoroutine());
            }

            foreach (PieceDomino domino in PiecesToPlayer)
            {
                domino.SetBlock(!isUserTurn);
            }

            VerifyWinner();
        }

        public void PassTurnButton()
        {
            //  If user pass turn:
            if (GameOver == false) {
                ChangeTurn(null, Side.Izq, false);
                OnPassBtn?.Invoke();
            }
            else {
                VerifyWinner();
            }
            SetPassBtnAlert(true);
        }
        public void PassTurnMachine()
        {
            //  If machine pass turn:
            Debug.Log($"Turn Pass To Player");
            if (GameOver == false)
            {
                ChangeTurn(null, Side.Izq, true);
            }
            else
            {
                VerifyWinner();
            }
            SetPassBtnAlert(true);
        }

        void VerifyWinner()
        {
            if (GameOver == true)
            {
                if (PiecesToPlayer.Count <= 0) {
                    GameOver = true;
                    UserIsWinner(true);
                }
                else if (PiecesToMachine.Count <= 0) {
                    GameOver = true;
                    UserIsWinner(false);
                }
                else if (PiecesToPlayer.Count >= PiecesToMachine.Count)
                    UserIsWinner(true);
                else
                    UserIsWinner(false);
            }
        }

        void UserIsWinner(bool userWon)
        {
            GameOver = true;
            winnerImg.gameObject.SetActive(true);
            winnerImg.GetComponentInChildren<TextMeshProUGUI>().text = userWon ? "¡Ganaste!" : "Sigue intentando";
        }

        void SetPassBtnAlert(bool state) {
            if(state == true) {
                if (turnPassed == PassTurnType.second) {
                    GameOver = true;
                    VerifyWinner();
                    Debug.Log("1111111111111 Verifico el ganador.");
                } 
                else {
                    if (turnPassed == PassTurnType.none) turnPassed = PassTurnType.first;
                    else if (turnPassed == PassTurnType.first) turnPassed = PassTurnType.second;

                    var colors = new ColorBlock();
                    colors.normalColor = Color.white;
                    colors.highlightedColor = Color.white;
                    colors.pressedColor = new Color(0.8f, 0.8f, 0.8f);
                    colors.disabledColor = new Color(0.8f, 0.8f, 0.8f, 0.5f);
                    colors.colorMultiplier = 1f;
                    passBtn.colors = colors;

                    passBtn.GetComponent<Image>().color = Color.red;
                    GetComponentInChildren<TextMeshProUGUI>().text = "Terminar juego";

                    Debug.Log("22222222222222 Alerta roja.");
                }
            }
            else {
                passBtn.GetComponent<Image>().color = Color.white;
                GetComponentInChildren<TextMeshProUGUI>().text = "Pasar turno";

                Debug.Log("33333333333333 Vuelvo el botón normal.");
            }

            Debug.Log("PASS BUTTON STATE: " + turnPassed);
        }

        private bool GameOver { get; set; }
    }

}