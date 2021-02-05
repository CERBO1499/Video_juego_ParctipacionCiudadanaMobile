using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PieceCloth : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Information
    bool isInposition;
    bool drag;
    GameObject posibility;
    Coroutine dragCoroutine;
    KeeperBody actualKeeper;
    #endregion

    #region Components
    RectTransform rect;
    Vector3 initialPosition;
    #endregion

    #region EncapsulatedFields
    public RectTransform Prect { get => rect; set => rect = value; }
    public Vector3 PinitialPosition { get => initialPosition; set => initialPosition = value; }
    #endregion

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        initialPosition = transform.localPosition;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        dragCoroutine = StartCoroutine(DragCoroutine(eventData));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        drag = false;

        if (isInposition)
        {
            
                actualKeeper.isKeeped = false;
                isInposition = false;
                actualKeeper.GetComponent<Image>().raycastTarget = true;
                rect.localPosition = initialPosition;

        }
        else
        {
            if (posibility == null)
            {
                rect.localPosition = initialPosition;

            }
            else
            {
                actualKeeper = posibility.GetComponent<KeeperBody>();
                if (!actualKeeper.IsKeeped)
                {
                    rect.position = posibility.GetComponent<RectTransform>().position;
                    actualKeeper.IsKeeped = true;
                    isInposition = true;
                    actualKeeper.GetComponent<Image>().raycastTarget = false;
                }
                else
                    rect.localPosition = initialPosition;
            }

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

            posibility = null;

            for (int i = 0; i < resoults.Count; i++)
            {
                if (resoults[i].gameObject.tag == "keeper")
                {
                    posibility = resoults[i].gameObject;

                    break;
                }

            }
            yield return null;
        }

    }

}
