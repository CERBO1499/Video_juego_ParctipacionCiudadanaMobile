using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImagenAArrastrar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Information

    [Header("Information")]
    #region Serializadas
    [SerializeField] Vector2 finalLocalPosition;
    #endregion

    #region Privadas
    Vector2 initialLocalPosition;
    int initialSiblingIndex;
    RectTransform parent;
    #endregion

    #region Encapsuladas y publicas
    public Vector2 FinalLocalPosition { get { return finalLocalPosition; } }
    #endregion

    #region Drag
    public static bool drag = true;
    Coroutine dragCoroutine;
    RectTransform keeper;
    int box;
    System.Action onPointerFail;
    #endregion

    #endregion
    #region Components
    RectTransform rect;
    #endregion

    private void Awake()
    {
        rect = GetComponent<RectTransform>();

        initialLocalPosition = rect.localPosition;

        initialSiblingIndex = rect.GetSiblingIndex();

        parent = rect.parent.gameObject.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (drag)
        {
            rect.SetParent(rect.root);

            dragCoroutine = StartCoroutine(DragCoroutine());
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
            StopCoroutine(dragCoroutine);

            dragCoroutine = null;

            if (keeper != null)
            {
                Keeper keeper = this.keeper.gameObject.GetComponent<Keeper>();

                if(keeper.keeped == null)
                {
                    keeper.keeped = gameObject;

                    onPointerFail = keeper.Clear;

                    rect.SetParent(this.keeper.GetChild(0));

                    rect.localPosition = Vector3.zero;
                }
                else
                    OnPointerFail();
            }
            else
                OnPointerFail();
        }
    }

    public void OnPointerFail()
    {
        rect.SetParent(parent);

        rect.SetSiblingIndex(initialSiblingIndex);

        rect.localPosition = initialLocalPosition;

        onPointerFail?.Invoke();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "keeper")
        {
            box++;

            keeper = collision.gameObject.GetComponent<RectTransform>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "keeper")
        {
            box--;
            if (box == 0)
                keeper = null;
        }
    }
}
