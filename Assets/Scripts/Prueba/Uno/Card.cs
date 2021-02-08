using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


public enum ColorCard
{
    Amarillo, Azul, Rojo, Verde, Negro
}
public enum NumberCard
{
    Alejo, Waira, Jaika, Emiliano, Valentina, PlusTwo, Reverse, Stop, Questions, PlusFour, ChangeColor
}

namespace Uno
{
    public class Card : MonoBehaviour, IPointerDownHandler
    {
        #region Information
        [SerializeField] ColorCard colorCard;
        [SerializeField] NumberCard numberCard;
        #endregion

        #region Components
        RectTransform rect;
        Sprite mySprite;
        #endregion

        #region EncapsulatedFields
        public RectTransform Prect { get => rect; set => rect = value; }
        public ColorCard ColorCard { get => colorCard; set => colorCard = value; }
        public NumberCard NumberCard { get => numberCard; set => numberCard = value; }
        public Sprite MySprite { get => mySprite; set => mySprite = value; }
        #endregion

        private void Awake()
        {
            MySprite = gameObject.GetComponent<Image>().sprite;
            rect = GetComponent<RectTransform>();

            IsActive = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (GameManager.instance.PplayerTurn && IsActive == true)
            {
                if (GameManager.instance.ActualColor == colorCard.ToString() || GameManager.instance.ActualNumber == numberCard.ToString() || colorCard == ColorCard.Negro)
                {
                    IsActive = false;

                    GameManager.instance.PlayerTurn = true;

                    rect.SetParent(GameManager.instance.PositionBoardCards);
                    rect.position = GameManager.instance.PositionBoardCards.position;
                    rect.sizeDelta = new Vector2(452f, 965f);
                    GameManager.instance.MyCurrentCard = gameObject.GetComponent<Card>();
                    GameManager.instance.ActualCard();
                    GameManager.instance.UserPlayed(this);


                    switch (numberCard)
                    {
                        case NumberCard.PlusTwo:
                            GameManager.instance.TakeTwoCards();
                            break;
                        case NumberCard.Reverse:
                            GameManager.instance.ChangeTurn(false);
                            break;
                        case NumberCard.Stop:
                            GameManager.instance.ChangeTurn(false);
                            break;
                        case NumberCard.Questions:
                            ActivitiesManager.Pinstance.ShowActivity(GameManager.questionIndex);
                            GameManager.questionIndex++;
                            if (GameManager.questionIndex < ActivitiesManager.Pinstance.Pactivities.Length) {
                                ActivitiesManager.Pinstance.ShowActivity(GameManager.questionIndex);
                                GameManager.questionIndex++;
                            }
                            else {
                                GameManager.instance.ChangeTurn(false);
                            }

                            //GameManager.instance.ChangeTurn(false);
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
                }
            }
        }

        public bool IsActive { get; set; }
    }
}