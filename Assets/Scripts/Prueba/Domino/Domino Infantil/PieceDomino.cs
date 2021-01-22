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
        public bool isInPosition = true;
        Image rayCastToUnactive;
        GameObject posibilty;
        Coroutine dragCoroutine;
        #endregion

        #region Components
        RectTransform rect;
        public RectTransform Prect { get => rect; set => rect = value; }

        Image img;

        #endregion

        #region Events
        #endregion

        void Awake()
        {
            rayCastToUnactive = GetComponent<Image>();

            rect = GetComponent<RectTransform>();

            img = GetComponent<Image>();

            gameObject.SetActive(false);

            switch (typeOfPiece)
            {
                case TypePiece.Double:
                    break;
                case TypePiece.Single:
                    img.raycastTarget = false;
                    img.color = new Color(1f, 1f, 1f, 0.5f);
                    break;
                default:
                    break;
            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(isInPosition)
            img.color = new Color(1f, 1f, 1f, 0.5f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            img.color = Color.white;
        }

        public void OnPointerDown(PointerEventData eventData)
        {  
                if (GameManager.instance.drag && isInPosition)
                {
                    Debug.Log("sisas");

                    lastIndex = rect.GetSiblingIndex();

                    GameManager.instance.ScrollToUnactive.enabled = false;

                    rect.SetParent(GameManager.instance.ParentToPieces);

                    dragCoroutine =  StartCoroutine(DragCoroutine(eventData));
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

                        gameObject.transform.localEulerAngles = posibilty.transform.localEulerAngles;

                        switch (typeOfPiece)
                        {
                            case TypePiece.Double:
                                gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                                break;
                            case TypePiece.Single:
                                gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
                                break;
                            default:
                                break;
                        }

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

            if (isInPosition) {
                if (posibilty == null)
                {                    
                    rect.SetParent(GameManager.instance.Ppieces);

                    rect.SetSiblingIndex(lastIndex);

                    rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, 0f);

                    rect.localEulerAngles = Vector3.zero;
                }
                else
                {
                    rect.position = posibilty.GetComponent<RectTransform>().position;

                    posibilty.gameObject.GetComponent<Image>().raycastTarget = false;

                    posibilty = null;

                    PieceInPosition();
                }
            }
           
        }

        void PieceInPosition()
        {
            StopCoroutine(dragCoroutine);

            rayCastToUnactive.raycastTarget = false;

            isInPosition = false;

            foreach (Transform item in transform)
            {
                if (item != null)
                {
                    item.gameObject.SetActive(true);
                }              
            }          

        }   
    }
}