using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Word : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Information
    [Header("Information")]
    [SerializeField] string word;
    public string Pword
    {
        get { return word; }
    }
    [SerializeField] Vector2 finalLocalPosition;
    public Vector2 PfinalLocalPosition
    {
        get { return finalLocalPosition; }
    }
    Vector2 initialLocalPosition;
    int initialSiblingIndex;
    #region Drag
    public static bool drag = true;
    int box;
    RectTransform keeper;
    Coroutine dragCorotuine;
    #endregion
    #endregion

    #region Components
    RectTransform rect;
    public RectTransform prect
    {
        get { return rect; }
    }
    #endregion

    void Awake()
    {
        rect = GetComponent<RectTransform>();

        initialLocalPosition = rect.localPosition;

        initialSiblingIndex = rect.GetSiblingIndex();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (drag)
        {
            UIManager.instance.UpdateKeepers(gameObject);

            dragCorotuine = StartCoroutine(DragCoroutine());

            rect.SetSiblingIndex(rect.parent.childCount - 1);
        }
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
        if (drag)
        {
            StopCoroutine(dragCorotuine);

            dragCorotuine = null;

            if (keeper != null)
            {
                Keeper keeper = this.keeper.gameObject.GetComponent<Keeper>();

                if (keeper.keeped == null)
                {
                    keeper.keeped = gameObject;

                    rect.localPosition = this.keeper.localPosition;

                    UIManager.instance.Ppass();
                }
                else
                    OnPointerFail();
            }
            else
                OnPointerFail();

            rect.SetSiblingIndex(initialSiblingIndex);
        }
    }

    public void OnPointerFail()
    {
        rect.localPosition = initialLocalPosition;
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