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
        const float ANIMATION_DURATION = 3f;
        const float ANIMATION_SIZE = 5f;

        public bool drag = true;
        public bool firstPiece = false;
        bool turnPassed = false;
        bool feedbackBeingUsed = false;
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
        [SerializeField] Button homeBtn;
        [SerializeField] Image feedbackPanel;
        [SerializeField] GameObject scrollPieces;
        [SerializeField] Sprite[] wrongPieceFeedback;
        [SerializeField] Sprite[] goodJobFeedback;

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

            homeBtn.gameObject.SetActive(false);
            passBtn.gameObject.SetActive(false);
            feedbackPanel.gameObject.SetActive(false);

            OcultSinglePieces();

            for (int i = 0; i < piecesToDistribute.Count; i++)
            {
                piecesToDistribute[i].OnFirstPiece += ResetSinglePieces;
            }

            PieceDomino.OnPieceInPlace -= ChangeTurn;
            PieceDomino.OnPieceInPlace += ChangeTurn;
            PieceDomino.OnWrongPiece -= FeedbackWrongPiece;
            PieceDomino.OnWrongPiece += FeedbackWrongPiece;
        }

        private void Start()
        {
            DistributePiecesRandom();
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

            Vector2 finiSize = new Vector2(ANIMATION_SIZE, ANIMATION_SIZE);

            float t = Time.time;

            yourTurnImg.localScale = iniSize;

            passBtn.interactable = true;

            while (Time.time <= t + ANIMATION_DURATION)
            {
                yourTurnImg.localScale = iniSize + ((finiSize - iniSize) * curveTurn.Evaluate((Time.time) - t) / ANIMATION_DURATION);
                yield return null;
            }
        }
        IEnumerator ShowEnemyTurnCoroutine()
        {
            yield return new WaitForSeconds(SECS_TO_BEFORE_ANIMATION);

            enemyTurnImg.gameObject.SetActive(true);

            Vector2 iniSize = Vector2.zero;

            Vector2 finiSize = new Vector2(ANIMATION_SIZE * 1.1f, ANIMATION_SIZE * 1.1f);

            float t = Time.time;

            enemyTurnImg.localScale = iniSize;

            while (Time.time <= t + ANIMATION_DURATION)
            {
                enemyTurnImg.localScale = iniSize + ((finiSize - iniSize) * curveTurn.Evaluate((Time.time) - t) / ANIMATION_DURATION);
                yield return null;
            }
        }
        void DistributePiecesRandom()
        {
            PieceDomino doublePiece = new PieceDomino();
            doublePiece.TypeOfPiece = TypePiece.Single;
            while (doublePiece.TypeOfPiece != TypePiece.Double) {
                doublePiece = piecesToDistribute[Random.Range(0, piecesToDistribute.Count)];
            }
            piecesToPlayer.Add(doublePiece);
            piecesToDistribute.Remove(doublePiece);
            doublePiece.Prect.SetParent(pieces);
            doublePiece.gameObject.SetActive(true);

            for (int i = 0; i < 13; i++)
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
            passBtn.gameObject.SetActive(true);

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

            if ((PiecesToPlayer.Count <= 0 || PiecesToMachine.Count <= 0) && GameOver == false)
            {
                GameOver = true;
                VerifyWinner();
            }

            if(GameOver == false) {
                if (turnPassed == true) {
                    if (isUserTurn == false && feedbackBeingUsed == false) {
                        feedbackBeingUsed = true;

                        feedbackPanel.gameObject.SetActive(true);
                        feedbackPanel.GetComponentInChildren<TextMeshProUGUI>().text = "";
                        feedbackPanel.GetComponentsInChildren<Image>()[1].sprite = goodJobFeedback[Random.Range(1, goodJobFeedback.Length)];
                    }
                    turnPassed = false;
                    SetPassBtnAlert(false);
                }

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
            }
        }

        public void PassTurnButton()
        {
            //  If user pass turn:
            if (GameOver == false)
            {
                if (turnPassed == true) {
            Debug.Log("El jugador pasó turno");
                    GameOver = true;
                    VerifyWinner();
                }
                ChangeTurn(null, Side.Izq, false);
                SetPassBtnAlert(true);
                OnPassBtn?.Invoke();
            }
        }
        public void PassTurnMachine()
        {
            //  If machine pass turn:
            if (GameOver == false)
            {
                if (turnPassed == true)
                {
            Debug.Log("El enemigo pasó turno");
                    GameOver = true;
                    VerifyWinner();
                }
                ChangeTurn(null, Side.Izq, true);
                SetPassBtnAlert(true);
            }
        }

        void VerifyWinner()
        {
            if (GameOver == true)
            {
                if (PiecesToPlayer.Count <= 0) {
                    UserIsWinner(true);
                }
                else if (PiecesToMachine.Count <= 0) {
                    UserIsWinner(false);
                }
                else if (PiecesToPlayer.Count <= PiecesToMachine.Count)
                    UserIsWinner(true);
                else
                    UserIsWinner(false);
            }
        }

        void UserIsWinner(bool userWon)
        {
            scrollPieces.SetActive(false);
            homeBtn.gameObject.SetActive(true);
            passBtn.gameObject.SetActive(false);
            feedbackPanel.gameObject.SetActive(true);
            feedbackPanel.GetComponentsInChildren<Image>()[1].sprite = goodJobFeedback[0];

            var feedbackPanelButtons = feedbackPanel.GetComponentsInChildren<Button>();
            if(feedbackPanelButtons[0] != homeBtn) {
                feedbackPanelButtons[0].gameObject.SetActive(false);
            }
            else {
                feedbackPanelButtons[1].gameObject.SetActive(false);
            }

            feedbackPanel.GetComponentInChildren<TextMeshProUGUI>().text = userWon ? "¡Ganaste!" : "Sigue intentando";
        }

        void SetPassBtnAlert(bool state) {
            if(state == true) {
                turnPassed = true;

                var colors = new ColorBlock();
                colors.normalColor = Color.white;
                colors.highlightedColor = Color.white;
                colors.pressedColor = new Color(0.8f, 0.8f, 0.8f);
                colors.disabledColor = new Color(0.8f, 0.8f, 0.8f, 0.5f);
                colors.colorMultiplier = 1f;
                passBtn.colors = colors;

                passBtn.GetComponent<Image>().color = Color.red;
                passBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Terminar juego";
            }
            else {
                passBtn.GetComponent<Image>().color = Color.white;
                GetComponentInChildren<TextMeshProUGUI>().text = "Pasar turno";
            }
        }

        void FeedbackWrongPiece() {
            if(feedbackBeingUsed == false && GameOver == false) {
                feedbackBeingUsed = true;

                feedbackPanel.gameObject.SetActive(true);
                feedbackPanel.GetComponentInChildren<TextMeshProUGUI>().text = "";
                feedbackPanel.GetComponentsInChildren<Image>()[1].sprite = wrongPieceFeedback[Random.Range(0, wrongPieceFeedback.Length)];
            }
        }

        public void StopUsingFeedback() {
            feedbackBeingUsed = false;
        }

        public bool GameOver { get; private set; }
    }

}
