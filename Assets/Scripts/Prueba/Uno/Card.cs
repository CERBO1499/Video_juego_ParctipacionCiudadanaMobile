using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public enum ColorCard
{
    Amarillo,Azul,Rojo,Verde
}
public enum NumberCard
{
    Alejo,Waira ,Jaika ,Emiliano , Valentina ,PlusTwo ,Reverse ,Stop ,Questions ,PlusFour,ChangeColor  
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

        public RectTransform Prect { get => rect; set => rect = value; }

        public void OnPointerDown(PointerEventData eventData)
        {
            rect.SetParent(GameManager.instance.PositionBoardCards);
            rect.position = GameManager.instance.PositionBoardCards.position;
            rect.sizeDelta = new Vector2(452f, 965f);
        }
        #endregion

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }
    }


}
