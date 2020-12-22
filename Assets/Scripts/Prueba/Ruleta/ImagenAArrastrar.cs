using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public enum TipoArrastrable
{
    imagen,
    globodetexto
}

public class ImagenAArrastrar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Information

    [Header("Information")]
    #region Serializadas
    [SerializeField] Vector2 finalLocalPosition;
    [SerializeField] TipoArrastrable tipoArrastrable;
    [SerializeField] AnimationCurve curve;
    [SerializeField] RectTransform btnCloseGlobo;
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
    RectTransform keeper,tmpKeeper;
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

                    switch (tipoArrastrable)
                    {
                        case TipoArrastrable.imagen:
                            break;
                        case TipoArrastrable.globodetexto:
                            StartCoroutine(CreaceGloboTexto());
                            break;
                        default:
                            break;
                    }
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


    IEnumerator CreaceGloboTexto()
    {
        tmpKeeper = keeper;

        rect.SetParent(rect.root);
        
        Vector2 initialSize = Vector2.one;
        Vector2 finalSize = new Vector2(3f, 3f);

        Vector2 initialPos = rect.transform.localPosition;
        Vector2 finiPos =Vector2.zero;

        float t = Time.time;

        while (Time.time <= t + 1f)
        {
            rect.localPosition = initialPos + ((finiPos - initialPos) * curve.Evaluate((Time.time - t) / 1f));
            rect.localScale = initialSize + ((finalSize - initialSize) * curve.Evaluate((Time.time - t) / 1f));

            yield return null;
        }

        rect.localPosition = finiPos;
        rect.localScale = finalSize;

        gameObject.transform.GetChild(0).GetComponent<TMP_InputField>().enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        btnCloseGlobo.gameObject.SetActive(true);
    }
    public void DecreaceImg()
    {
        StartCoroutine(DecreaceGlobo());
    }
    public IEnumerator DecreaceGlobo()
    {
        print("Decreace keeper: " + "" + keeper);
        rect.SetParent(tmpKeeper.GetChild(0));


        Vector2 initialSize = new Vector2(3f, 3f);
        Vector2 finalSize = Vector2.one;

        Vector2 initialPos = rect.localPosition;
        Vector2 finiPos = Vector3.zero;

        float t = Time.time;

        while (Time.time <= t + 1f)
        {
            rect.localPosition = initialPos + ((finiPos - initialPos) * curve.Evaluate((Time.time - t) / 1f));
            rect.localScale = initialSize + ((finalSize - initialSize) * curve.Evaluate((Time.time - t) / 1f));

            yield return null;
        }

        rect.localPosition = finiPos;
        rect.localScale = finalSize;

        gameObject.transform.GetChild(0).GetComponent<TMP_InputField>().enabled = false;
        btnCloseGlobo.gameObject.SetActive(false);

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
