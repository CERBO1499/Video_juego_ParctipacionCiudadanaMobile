using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PieceDomino : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    #region Components
    RectTransform rectTrasnform;
    public RectTransform PrectTrasnform { get => rectTrasnform; set => rectTrasnform = value; }
    Image img;
    #endregion


    void Awake()
    {
        rectTrasnform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
         img.color =Color.white;
    }

    public void OnPointerUp(PointerEventData eventData)
    {/*
        if (drag)
        {
            lastIndex = rect.GetSiblingIndex();

            rect.SetParent(rect.parent.parent.parent.parent);

            root.sizeDelta = new Vector3(root.sizeDelta.x, root.sizeDelta.y - (rect.sizeDelta.y + 240));

            StartCoroutine(DragCoroutine(eventData));
        }*/
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
