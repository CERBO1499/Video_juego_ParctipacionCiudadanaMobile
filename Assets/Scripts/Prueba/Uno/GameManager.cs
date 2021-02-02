﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Uno
{
    #region Delegat
    public delegate void ConcatenedAction(Action output);
    #endregion

    public class GameManager : MonoBehaviour
    {
        #region Statics
        public static GameManager instance;
        #endregion
        #region Information 
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

        string actualColor = "";
        string actualNumber = "";
        Card myCurrentCard;
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
        #endregion
        #endregion

        #region Events        
        
        #endregion

        private void Awake()
        {
            instance = this;

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
            myBaseCard.GetComponent<Image>().raycastTarget = false;

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
                myCard.gameObject.GetComponent<Image>().raycastTarget = false;
                myCard.Prect.sizeDelta = new Vector2(452f, 709f);


                myCard.Prect.SetParent(parentMachine);
                myCard.gameObject.SetActive(true);
            }
            MyCurrentCard = myBaseCard;
            ActualCard();
        }

        public void ChangeTurn(bool turnChanged)
        {
            if(cardsToPlayerInitial.Count <= 0 || CardsToMachineInitial.Count <= 0) {
                gameOver = true;
                winnerPanel.gameObject.SetActive(true);
            }

            if(gameOver == false) { 
                if (turnChanged == true)
                {
                    playerTurn = !playerTurn;

                    if (playerTurn == false)
                    {
                        StartCoroutine(MachinePlayCoroutine());
                    }            
                }

                if(changeTurn != null) {
                    StopCoroutine(changeTurn);
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
            myCard.gameObject.GetComponent<Image>().raycastTarget = false;
            myCard.gameObject.SetActive(true);

            ChangeTurn(true);
        }
        public void TakeTwoCards()
        {
            if (!PlayerTurn)
            {
                for (int i = 0; i < 2; i++)
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
                for (int i = 0; i < 2; i++)
                {
                    Card myCard = cardsToDistributeInitial[UnityEngine.Random.Range(0, cardsToDistributeInitial.Count)];
                    Sprite myCardImage = myCard.gameObject.GetComponent<Image>().sprite;
                    cardsToDistributeInitial.Remove(myCard);
                    cardsToMachineInitial.Add(myCard);
                    myCard.Prect.sizeDelta = new Vector2(452f, 709f);

                    myCard.gameObject.GetComponent<Image>().sprite = spriteEnemy;
                    myCard.gameObject.GetComponent<Image>().raycastTarget = false;
                    myCard.Prect.SetParent(parentMachine);
                    myCard.gameObject.SetActive(true);
                }
            }

            ChangeTurn(true);
        }
        public void TakeFourCards()
        {
            if (!PlayerTurn)
            {
                for (int i = 0; i < 4; i++)
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
                for (int i = 0; i < 4; i++)
                {
                    Card myCard = cardsToDistributeInitial[UnityEngine.Random.Range(0, cardsToDistributeInitial.Count)];
                    Sprite myCardImage = myCard.gameObject.GetComponent<Image>().sprite;
                    cardsToDistributeInitial.Remove(myCard);
                    cardsToMachineInitial.Add(myCard);
                    myCard.Prect.sizeDelta = new Vector2(452f, 709f);

                    myCard.gameObject.GetComponent<Image>().sprite = spriteEnemy;
                    myCard.gameObject.GetComponent<Image>().raycastTarget = false;
                    myCard.Prect.SetParent(parentMachine);
                    myCard.gameObject.SetActive(true);
                }

            }
        }

        public void ChangeColor()
        {
            if (!playerTurn)
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
            RectTransform feedBack;
            if (playerTurn == true) feedBack = feedBackTurno[0];
            else feedBack = feedBackTurno[1];

            Debug.Log("El feedback es del usuario: " + (feedBackTurno[0] == feedBack));

            feedBack.gameObject.SetActive(true);

            Vector2 iniSize = Vector2.zero;
            Vector2 finiSize = new Vector2(4f, 4f);

            float t = Time.time;

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
            changeColorPanel.gameObject.SetActive(true);

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

            ChangeTurn(true);
        }

        IEnumerator MachinePlayCoroutine() {
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
                        StartCoroutine(MachinePlayCoroutine());
                        ChangeTurn(false);
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
            }
        }

        void UpdateColorFrame() {
            switch (ActualColor)
            {
                case "Amarillo":
                    colorFrame.color = Color.yellow;
                    break;

                case "Azul":
                    colorFrame.color = Color.blue;
                    break;

                case "Rojo":
                    colorFrame.color = Color.red;
                    break;

                case "Verde":
                    colorFrame.color = Color.green;
                    break;

                case "Negro":
                    colorFrame.color = Color.black;
                    break;
            }
        }

        public void UserPlayed(Card _card) {
            cardsToPlayerInitial.Remove(_card);
        }
    }
}
