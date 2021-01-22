using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

enum TypePiece
{
    Double,
    Single
}

namespace Diverdomino
{
    public class PieceDomino : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
    {
        #region Information
        [SerializeField] TypePiece typeOfPiece;
        int lastIndex;
        bool drag;
        GameObject posibilty;
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

        public void OnPointerDown(PointerEventData eventData)
        {
            if (GameManager.instance.drag)
            {
                lastIndex = rect.GetSiblingIndex();

                GameManager.instance.ScrollToUnactive.enabled = false;

                rect.SetParent(GameManager.instance.ParentToPieces);

                StartCoroutine(DragCoroutine(eventData));
            }
        }
        IEnumerator DragCoroutine(PointerEventData pointerEventData)
        {
            drag = true;

            while (drag)
            {
                Vector3 screenPoint = Input.mousePosition;

                screenPoint.z = 90.0f;

                rect.position = Camera.main.ScreenToWorldPoint(screenPoint);

                pointerEventData.position = Input.mousePosition;

                List<RaycastResult> resoults = new List<RaycastResult>();

                EventSystem.current.RaycastAll(pointerEventData, resoults);

                bool found = false;

                for (int i = 0; i < resoults.Count; i++)
                {
                    if (resoults[i].gameObject.tag == "Piece Domino")
                    {
                        found = true;

                        posibilty = resoults[i].gameObject;

                        break;
                    }
                }

                if (!found)
                    posibilty = null;

                yield return null;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            drag = false;

            GameManager.instance.ScrollToUnactive.enabled = true;

            if (posibilty == null)
            {
                rect.SetParent(GameManager.instance.Ppieces);

                rect.SetSiblingIndex(lastIndex);

                rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, 0f);
            }
            else
            {
                rect.position = posibilty.GetComponent<RectTransform>().position;

                posibilty = null;
            }
        }
    }
}