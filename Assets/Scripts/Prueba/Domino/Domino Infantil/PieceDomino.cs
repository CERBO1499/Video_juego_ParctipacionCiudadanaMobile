using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Diverdomino
{
    public class PieceDomino : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        #region Information
        int lastIndex;
        #endregion

        #region Components
        RectTransform rect;
        public RectTransform Prect { get => rect; set => rect = value; }
        Image img;
        #endregion


        void Awake()
        {
            rect = GetComponent<RectTransform>();

            img = GetComponent<Image>();

            gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            img.color = new Color(1f, 1f, 1f, 0.5f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            img.color = Color.white;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (GameManager.instance.drag)
            {
                lastIndex = rect.GetSiblingIndex();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
    }
}