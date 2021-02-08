using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Uno
{
    #region Delegate
    public delegate void ConcatenedAction(Action output);
    #endregion

    public class GameManager : MonoBehaviour
    {
    #pragma warning disable CS0649

        #region Statics
        public static GameManager instance;
        public static int questionIndex;
        #endregion
        #region Information 
        [Header("Cards colors")]
        [SerializeField] Color black;
        [SerializeField] Color blue;
        [SerializeField] Color green;
        [SerializeField] Color red;
        [SerializeField] Color yellow;

        [Header("Cards list")]
        [SerializeField] List<Card> cardsToDistributeInitial;
        [SerializeField] List<Card> cardsToPlayerInitial;
        [SerializeField] List<Card> cardsToMachineInitial;

        [Header("Parents")]
        [SerializeField] Transform parentPlayer;
        [SerializeField] Transform parentMachine;
        [SerializeField] Transform parentBoard;

        [Header("Information")]
        [SerializeField] Transform positionBoardCards;
        [SerializeField] Sprite spriteEnemy;
        [SerializeField] RectTransform changeColorPanel;
        [SerializeField] AnimationCurve curveToApearColors;
        [SerializeField] Machine machine;
        [SerializeField] RectTransform[] feedBackTurno;
        [SerializeField] AnimationCurve curveTurno;
        [SerializeField] Image colorFrame;
        [SerializeField] Image winnerPanel;
        [SerializeField] ParticleSystem takeCardIndicator;
        [SerializeField] Image unoFeedback;
        [SerializeField] Button cardsDeckBtn;

        string actualColor = "";
        string actualNumber = "";
        Card myCurrentCard;
        bool uno = false;
        bool playerTurn = true;
        Coroutine changeTurn;

        public bool PplayerTurn
        {
            get { return playerTurn; }
        }
        bool gameOver = false;

        #region EncapsulatedFields
        public Transform PositionBoardCards { get => positionBoardCards; set => positionBoardCards = value; }
        public string ActualColor { get => actualColor; set => actualColor = value; }
        public string ActualNumber { get => actualNumber; set => actualNumber = value; }
        public Card MyCurrentCard { get => myCurrentCard; set => myCurrentCard = value; }
        public bool PlayerTurn { get => playerTurn; set => playerTurn = value; }
        public List<Card> CardsToMachineInitial { get => cardsToMachineInitial; set => cardsToMachineInitial = value; }
        public List<Card> CardsUsed { get => cardsUsed; set => cardsUsed = value; }
        #endregion
        #endregion

        #region Components        
        private List<Card> cardsUsed;
        #endregion

        private void Awake()
        {
            instance = this;

            cardsUsed = new List<Card>();

            questionIndex = 0;

            unoFeedback.gameObject.SetActive(false);
            winnerPanel.gameObject.SetActive(false);
        }
        private void Start()
        {
            InitialDistributeCardRandom();
            changeTurn = StartCoroutine(ChangeTurnCoroutine());
        }
        void InitialDistributeCardRandom()
        {
            Card myBaseCard = cardsToDistributeInitial[UnityEngine.Random.Range(0, cardsToDistributeInitial.Count)];

            while (myBaseCard.ColorCard == ColorCard.Negro)
            {
                myBaseCard = cardsToDistributeInitial[UnityEngine.Random.Range(0, cardsToDistributeInitial.Count)];
            }

            cardsToDistributeInitial.Remove(myBaseCard);
            myBaseCard.Prect.SetParent(PositionBoardCards);
            myBaseCard.Prect.position = PositionBoardCards.transform.position;
            myBaseCard.Prect.sizeDelta = new Vector2(452f, 965f);
            myBaseCard.IsActive = false;

            for (int i = 0; i < 7; i++)
            {
                Card myCard = cardsToDistributeInitial[UnityEngine.Random.Range(0, cardsToDistributeInitial.Count)];
                cardsToDistributeInitial.Remove(myCard);
                cardsToPlayerInitial.Add(myCard);
                myCard.Prect.sizeDelta = new Vector2(452f, 709f);

                myCard.Prect.SetParent(parentPlayer);
                myCard.gameObject.SetActive(true);
            }

            for (int i = 0; i < 7; i++)
            {
                Card myCard = cardsToDistributeInitial[UnityEngine.Random.Range(0, cardsToDistributeInitial.Count)];


                cardsToDistributeInitial.Remove(myCard);
                cardsToMachineInitial.Add(myCard);
                myCard.gameObject.GetComponent<Image>().sprite = spriteEnemy;
                myCard.IsActive = false;
                myCard.Prect.sizeDelta = new Vector2(452f, 709f);


                myCard.Prect.SetParent(parentMachine);
                myCard.gameObject.SetActive(true);
            }
            MyCurrentCard = myBaseCard;
            ActualCard();
        }

        public void ChangeTurn(bool turnChanged)
        {
            if (cardsToPlayerInitial.Count <= 0 || CardsToMachineInitial.Count <= 0)
            {
                gameOver = true;
                winnerPanel.gameObject.SetActive(true);
                winnerPanel.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text =
                        "Menú";
                winnerPanel.GetComponentInChildren<TextMeshProUGUI>().text = 
                    cardsToPlayerInitial.Count <= 0 ? 
                        "Felicitaciones Gamer, has ganado esta partida." : "No te rindas Gamer, vuelve a intentarlo.";
            }

            if (gameOver == false)
            {
                if (turnChanged == true)
                {
                    playerTurn = !playerTurn;

                    if (playerTurn == false)
                    {
                        StartCoroutine(MachinePlayCoroutine());
                    }
                }

                if (changeTurn != null)
                {
                    StopCoroutine(changeTurn);
                }

                if (cardsToDistributeInitial.Count <= 0)
                {
                    cardsToDistributeInitial = RefillDeck();
                }

                changeTurn = StartCoroutine(ChangeTurnCoroutine());
            }
        }
        public void ActualCard()
        {
            ActualColor = MyCurrentCard.ColorCard.ToString();
            ActualNumber = MyCurrentCard.NumberCard.ToString();

            UpdateColorFrame();
        }
        public void TakeNewCardPlayer()
        {
            if (playerTurn)
            {
                Card myCard = cardsToDistributeInitial[UnityEngine.Random.Range(0, cardsToDistributeInitial.Count)];
                cardsToDistributeInitial.Remove(myCard);
                cardsToPlayerInitial.Add(myCard);

                myCard.Prect.sizeDelta = new Vector2(452f, 709f);
                myCard.Prect.SetParent(parentPlayer);
                myCard.gameObject.SetActive(true);

                ChangeTurn(true);
            }
        }
        void TakeNewCardEnemie()
        {
            Card myCard = cardsToDistributeInitial[UnityEngine.Random.Range(0, cardsToDistributeInitial.Count)];
            cardsToDistributeInitial.Remove(myCard);
            cardsToMachineInitial.Add(myCard);

            myCard.Prect.sizeDelta = new Vector2(452f, 709f);
            myCard.Prect.SetParent(parentMachine);
            myCard.gameObject.GetComponent<Image>().sprite = spriteEnemy;
            myCard.IsActive = false;
            myCard.gameObject.SetActive(true);

            ChangeTurn(true);
        }
        public void TakeTwoCards()
        {
            int cardQuantity = Mathf.Clamp(cardsToDistributeInitial.Count, 0, 2);

            if (!PlayerTurn)
            {
                for (int i = 0; i < cardQuantity; i++)
                {
                    Card myCard = cardsToDistributeInitial[UnityEngine.Random.Range(0, cardsToDistributeInitial.Count)];
                    cardsToDistributeInitial.Remove(myCard);
                    cardsToPlayerInitial.Add(myCard);
                    myCard.Prect.sizeDelta = new Vector2(452f, 709f);

                    myCard.Prect.SetParent(parentPlayer);
                    myCard.gameObject.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < cardQuantity; i++)
                {
                    Card myCard = cardsToDistributeInitial[UnityEngine.Random.Range(0, cardsToDistributeInitial.Count)];
                    Sprite myCardImage = myCard.gameObject.GetComponent<Image>().sprite;
                    cardsToDistributeInitial.Remove(myCard);
                    cardsToMachineInitial.Add(myCard);
                    myCard.Prect.sizeDelta = new Vector2(452f, 709f);

                    myCard.gameObject.GetComponent<Image>().sprite = spriteEnemy;
                    myCard.IsActive = false;
                    myCard.Prect.SetParent(parentMachine);
                    myCard.gameObject.SetActive(true);
                }
            }

            ChangeTurn(true);
        }
        public void TakeFourCards()
        {
            int cardQuantity = Mathf.Clamp(cardsToDistributeInitial.Count, 0 , 4);

            if (!PlayerTurn)
            {
                for (int i = 0; i < cardQuantity; i++)
                {
                    Card myCard = cardsToDistributeInitial[UnityEngine.Random.Range(0, cardsToDistributeInitial.Count)];
                    cardsToDistributeInitial.Remove(myCard);
                    cardsToPlayerInitial.Add(myCard);
                    myCard.Prect.sizeDelta = new Vector2(452f, 709f);

                    myCard.Prect.SetParent(parentPlayer);
                    myCard.gameObject.SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < cardQuantity; i++)
                {
                    Card myCard = cardsToDistributeInitial[UnityEngine.Random.Range(0, cardsToDistributeInitial.Count)];
                    Sprite myCardImage = myCard.gameObject.GetComponent<Image>().sprite;
                    cardsToDistributeInitial.Remove(myCard);
                    cardsToMachineInitial.Add(myCard);
                    myCard.Prect.sizeDelta = new Vector2(452f, 709f);

                    myCard.gameObject.GetComponent<Image>().sprite = spriteEnemy;
                    myCard.IsActive = false;
                    myCard.Prect.SetParent(parentMachine);
                    myCard.gameObject.SetActive(true);
                }

            }
        }

        public void ChangeColor()
        {
            if (playerTurn == false)
            {
                ActualColor = ((ColorCard)UnityEngine.Random.Range(0, Enum.GetValues(typeof(ColorCard)).Length - 1)).ToString();

                ChangeTurn(true);
                UpdateColorFrame();
            }
            else
            {
                StartCoroutine(ChangeColorApearCoroutine());
            }
        }

        IEnumerator ChangeTurnCoroutine()
        {
            yield return new WaitForSecondsRealtime(0.5f);

            RectTransform feedBack;

            Vector2 iniSize = Vector2.zero;
            float size = playerTurn == true ? 6f : 4f;
            Vector2 finiSize = new Vector2(size, size);
            float t = Time.time;

            feedBack = feedBackTurno[(playerTurn == true) ? 0 : 1];
            feedBackTurno[(playerTurn == true) ? 1 : 0].gameObject.SetActive(false);


            if (playerTurn == true)
            {
                if (VerifyUserCards() == false) takeCardIndicator.Play();
                else takeCardIndicator.Stop();
            }
            else
            {
                takeCardIndicator.Stop();

                if (cardsToPlayerInitial.Count == 1 && uno == false)
                {
                    winnerPanel.gameObject.SetActive(true);
                    winnerPanel.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = 
                        "Cerrar";
                    winnerPanel.GetComponentInChildren<TextMeshProUGUI>().text = 
                        "Usa el botón UNO antes de que empiece el otro turno.";
                    TakeFourCards();
                }
            }

            feedBack.gameObject.SetActive(true);

            while (Time.time <= t + 3f)
            {
                feedBack.localScale = iniSize + ((finiSize - iniSize) * curveTurno.Evaluate((Time.time) - t) / 3f);
                yield return null;
            }

            feedBack.localScale = finiSize;
            feedBack.gameObject.SetActive(false);
        }
        IEnumerator ChangeColorApearCoroutine()
        {
            ActivateChangingColorPanel(true);

            Vector2 iniSize = Vector2.zero;
            Vector2 finiSize = Vector2.one;
            float t = Time.time;

            while (Time.time <= t + 1f)
            {
                changeColorPanel.localScale = iniSize + ((finiSize - iniSize) * curveToApearColors.Evaluate((Time.time) - t) / 1f);
                yield return null;
            }

            changeColorPanel.localScale = finiSize;
        }
        public void ChangeColorSelection(string colorSelected)
        {
            actualColor = colorSelected;

            UpdateColorFrame();
            StartCoroutine(ChangeColorDesapearCoroutine());
        }

        IEnumerator ChangeColorDesapearCoroutine()
        {
            Vector2 iniSize = Vector2.one;
            Vector2 finiSize = Vector2.zero;

            float t = Time.time;

            while (Time.time <= t + 1f)
            {
                changeColorPanel.localScale = iniSize + ((finiSize - iniSize) * curveToApearColors.Evaluate((Time.time) - t) / 1f);
                yield return null;
            }
            changeColorPanel.localScale = finiSize;

            ActivateChangingColorPanel(false);
            ChangeTurn(true);
        }

        public IEnumerator MachinePlayCoroutine()
        {
            yield return new WaitForSeconds(2f);

            Card card = machine.PickCard();

            if (card == null)
                TakeNewCardEnemie();
            else
            {
                machine.PositionCard();

                switch (card.NumberCard)
                {
                    case NumberCard.PlusTwo:
                        GameManager.instance.TakeTwoCards();
                        break;
                    case NumberCard.Reverse:
                        //  Volver a tirar
                        StartCoroutine(MachinePlayCoroutine());
                        ChangeTurn(false);
                        break;
                    case NumberCard.Stop:
                        //  Volver a tirar
                        StartCoroutine(MachinePlayCoroutine());
                        ChangeTurn(false);
                        break;
                    case NumberCard.Questions:
                        if(questionIndex < ActivitiesManager.Pinstance.Pactivities.Length) { 
                            ActivitiesManager.Pinstance.ShowActivity(questionIndex);
                            questionIndex++;
                        }
                        else { 
                            StartCoroutine(MachinePlayCoroutine());
                            ChangeTurn(false);
                        }
                        break;
                    case NumberCard.PlusFour:
                        GameManager.instance.TakeFourCards();
                        GameManager.instance.ChangeColor();
                        break;
                    case NumberCard.ChangeColor:
                        GameManager.instance.ChangeColor();
                        break;
                    default:
                        GameManager.instance.ChangeTurn(true);
                        break;
                }

                CardsToMachineInitial.Remove(card);
                CardsUsed.Add(card);
                card.IsActive = false;
            }
        }

        void UpdateColorFrame()
        {
            switch (ActualColor)
            {
                case "Amarillo":
                    colorFrame.color = yellow;
                    break;

                case "Azul":
                    colorFrame.color = blue;
                    break;

                case "Rojo":
                    colorFrame.color = red;
                    break;

                case "Verde":
                    colorFrame.color = green;
                    break;

                case "Negro":
                    colorFrame.color = black;
                    break;
            }
        }

        public void UserPlayed(Card _card)
        {
            cardsToPlayerInitial.Remove(_card);
            CardsUsed.Add(_card);
        }

        private bool VerifyUserCards()
        {
            bool areCardsAvailable = false;

            for (int i = 0; i < cardsToPlayerInitial.Count; i++)
            {
                if (cardsToPlayerInitial[i].ColorCard.ToString() == actualColor || cardsToPlayerInitial[i].NumberCard.ToString() == actualNumber ||
                    cardsToPlayerInitial[i].ColorCard.ToString() == ColorCard.Negro.ToString())
                {
                    areCardsAvailable = true;
                    break;
                }
            }

            return areCardsAvailable;
        }

        public void UnoBtn()
        {
            if (uno == false && cardsToPlayerInitial.Count <= 1)
            {
                StartCoroutine(UnoCoroutine());
                uno = true;
                Invoke("ResetUnoBtn", 1f);
            }
        }

        private void ResetUnoBtn()
        {
            uno = false;
        }

        IEnumerator UnoCoroutine()
        {
            Vector2 iniSize = Vector2.zero;
            Vector2 finiSize = new Vector2(4f, 4f);
            float t = Time.time;

            unoFeedback.gameObject.SetActive(true);

            while (Time.time <= t + 1f)
            {
                unoFeedback.transform.localScale = iniSize + ((finiSize - iniSize) * curveTurno.Evaluate((Time.time) - t) / 1f);
                yield return null;
            }

            unoFeedback.transform.localScale = finiSize;
            unoFeedback.gameObject.SetActive(false);
        }

        private void ActivateChangingColorPanel(bool state)
        {
            cardsDeckBtn.interactable = !state;

            for (int i = 0; i < cardsToPlayerInitial.Count; i++)
            {
                cardsToPlayerInitial[i].IsActive = !state;
            }
        }

        private List<Card> RefillDeck()
        {
            List<Card> shuffled = new List<Card>();

            while (CardsUsed.Count > 0)
            {
                int random = UnityEngine.Random.Range(0, CardsUsed.Count);

                var card = CardsUsed[random];
                card.IsActive = true;
                shuffled.Add(card);
                CardsUsed.RemoveAt(random);
            }

            CardsUsed.Clear();

            return shuffled;
        }
    }
}
