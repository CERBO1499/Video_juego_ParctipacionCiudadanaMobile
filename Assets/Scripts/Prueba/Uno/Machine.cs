using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Uno
{
    public class Machine : MonoBehaviour
    {
        #region Components
        Card machineCard = null;
        #endregion

        #region Events
        Queue<System.Action> Events;

        #endregion
        
        public Card PickCard()
        {
            string colorToSelect = GameManager.instance.ActualColor;
            string numberToSelect = GameManager.instance.ActualNumber;

            for (int i = 0; i < GameManager.instance.CardsToMachineInitial.Count; i++)
            {
                machineCard = GameManager.instance.CardsToMachineInitial[i];

                if (machineCard.ColorCard.ToString() == colorToSelect || machineCard.NumberCard.ToString() == numberToSelect || machineCard.ColorCard == ColorCard.Negro)
                    break;

                machineCard = null;
            }

            GameManager.instance.CardsToMachineInitial.Remove(machineCard);

            return (machineCard) ? machineCard : null;
        }

        public void PositionCard()
        {
            machineCard.Prect.SetParent(GameManager.instance.PositionBoardCards);

            machineCard.Prect.position = GameManager.instance.PositionBoardCards.position;

            machineCard.GetComponent<Image>().sprite = machineCard.MySprite;

            machineCard.Prect.sizeDelta = new Vector2(452f, 965f);

            GameManager.instance.MyCurrentCard = machineCard;

            GameManager.instance.ActualCard();
        }
    }
}

