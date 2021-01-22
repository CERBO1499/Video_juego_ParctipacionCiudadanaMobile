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
        int lastIndex;
        GameObject putPosition;
        bool drag;
        bool release;
        [SerializeField] TypePiece typeOfPiece;
        #endregion

        #region Components
        RectTransform rect;
        BoxCollider2D coll;
        public RectTransform Prect { get => rect; set => rect = value; }
        Image img;
        #endregion


        void Awake()
        {
            coll = GetComponent<BoxCollider2D>();

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
            drag = false;

            GameManager.instance.ScrollToUnactive.enabled = true;

            coll.isTrigger = true;

            if (release)
            {
                switch (typeOfPiece)
                {
                    case TypePiece.Double:
                        gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                        break;
                    case TypePiece.Single:
                        break;
                    default:
                        break;
                }
                gameObject.GetComponent<PieceDomino>().enabled = false;

                img.color = Color.white;
            }
            else
                rect.SetParent(GameManager.instance.ParentToReturn);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.tag == "Piece Domino")
            {
                print("Entro");
                release = true;
            }
            else 
            {
                release = false;
                coll.enabled = false;
                print("Release true");

            }


        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (GameManager.instance.drag)
            {
                GameManager.instance.ScrollToUnactive.enabled = false;

                lastIndex = rect.GetSiblingIndex();

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

                yield return null;

            }
        }

    }
}