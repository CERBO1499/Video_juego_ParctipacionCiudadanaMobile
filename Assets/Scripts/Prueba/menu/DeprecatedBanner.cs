using System.Collections;
using UnityEngine;
using TMPro;

public class DeprecatedBanner : MonoBehaviour
{
    #region Information
    [Header("Information")]
    [SerializeField] int timeToSpawn = 30;
    [SerializeField] AnimationCurve curveToSpwan;
    WaitForSeconds waitForSeconds;
    Coroutine activeCoroutine;
    public bool PactiveCoroutine
    {
        get { return activeCoroutine != null; }
    }
    #endregion

    #region Components
    RectTransform rect;
    [Header("Components")]
    [SerializeField] TMP_Text text;
    #endregion

    public virtual void Awake()
    {
        rect = GetComponent<RectTransform>();

        waitForSeconds = new WaitForSeconds(timeToSpawn);

        DontDestroyOnLoad(rect.parent.gameObject);
    }

    public void Active()
    {
        if (rect != null)
        {
            activeCoroutine = StartCoroutine(ActiveCoroutine(true));
        }
    }

    public void Active(string message)
    {
        text.text = message;

        if (rect != null)
        {
            if (activeCoroutine == null)
                activeCoroutine = StartCoroutine(ActiveCoroutine(false));
            else
            {
                StopCoroutine(activeCoroutine);

                activeCoroutine = StartCoroutine(ActiveCoroutine(false));
            }
        }
    }


    IEnumerator ActiveCoroutine(bool loop)
    {
        Vector2 initialPosition = new Vector3(0f, -12.5f, 0f);

        Vector2 finalPosition = new Vector3(0f, 12.5f, 0f);

        do
        {
            float t = Time.time;

            while (Time.time <= t + 1f)
            {
                rect.anchoredPosition = initialPosition + ((finalPosition - initialPosition) * curveToSpwan.Evaluate(Time.time - t));

                yield return null;
            }

            rect.anchoredPosition = finalPosition;

            rect.GetChild(0).gameObject.SetActive(true);

            yield return waitForSeconds;

            rect.GetChild(0).gameObject.SetActive(false);

            t = Time.time;

            while (Time.time <= t + 1f)
            {
                rect.anchoredPosition = finalPosition + ((initialPosition - finalPosition) * curveToSpwan.Evaluate(Time.time - t));

                yield return null;
            }

            rect.anchoredPosition = initialPosition;

            yield return waitForSeconds;
        }
        while (loop);
    }
}