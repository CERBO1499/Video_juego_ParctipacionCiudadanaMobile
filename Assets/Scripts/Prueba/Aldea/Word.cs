using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Word : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Information
    Vector2 initialLocalPosition;
    int initialSiblingIndex;
    #region Drag
    int box;
    RectTransform keeper;
    Coroutine dragCorotuine;
    #endregion
    #endregion

    #region Components
    RectTransform rect;
    #endregion

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        initialLocalPosition = rect.localPosition;

        initialSiblingIndex = rect.GetSiblingIndex();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragCorotuine = StartCoroutine(DragCoroutine());

        rect.SetSiblingIndex(rect.parent.childCount - 1);
    }

    IEnumerator DragCoroutine()
    {
        while (true)
        {
            Vector3 screenPoint = Input.mousePosition;

            screenPoint.z = 100f;

            rect.position = Camera.main.ScreenToWorldPoint(screenPoint);

            yield return null;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(dragCorotuine);

        dragCorotuine = null;

        if (keeper != null)
            rect.localPosition = keeper.localPosition;
        else
            rect.localPosition = initialLocalPosition;

        rect.SetSiblingIndex(initialSiblingIndex);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "keeper")
        {
            box++;

            keeper = collision.gameObject.GetComponent<RectTransform>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "keeper")
        {
            box--;

            if(box == 0)
                keeper = null;
        }
    }
}