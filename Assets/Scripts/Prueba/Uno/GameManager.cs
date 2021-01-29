using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uno
{
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
 
        [SerializeField] Transform positionBoardCards;

        #region EncapsulatedFields
        public Transform PositionBoardCards { get => positionBoardCards; set => positionBoardCards = value; }
        #endregion
        #endregion

        private void Awake()
        {
            instance = this;

            InitialDistributeCardRandom();
        }
        void InitialDistributeCardRandom()
        {
            Card myBaseCard = cardsToDistributeInitial[Random.Range(0, cardsToDistributeInitial.Count)];
            cardsToDistributeInitial.Remove(myBaseCard);
            myBaseCard.Prect.SetParent(PositionBoardCards);
            myBaseCard.Prect.position = PositionBoardCards.transform.position;
            myBaseCard.Prect.sizeDelta = new Vector2(452f, 965f);



            for (int i = 0; i < 7; i++)
            {
                Card myCard = cardsToDistributeInitial[Random.Range(0, cardsToDistributeInitial.Count)];
                cardsToDistributeInitial.Remove(myCard);
                cardsToPlayerInitial.Add(myCard);
                myCard.Prect.sizeDelta = new Vector2(452f, 709f);

                myCard.Prect.SetParent(parentPlayer);
                myCard.gameObject.SetActive(true);
            }

            for (int i = 0; i < 7; i++)
            {
                Card myCard = cardsToDistributeInitial[Random.Range(0, cardsToDistributeInitial.Count)];
                cardsToDistributeInitial.Remove(myCard);
                cardsToMachineInitial.Add(myCard);
                myCard.Prect.sizeDelta = new Vector2(452f, 709f);

                myCard.Prect.SetParent(parentMachine);
                myCard.gameObject.SetActive(true);

            }


        }

        void ActualCard()
        {

        }

    }
}
