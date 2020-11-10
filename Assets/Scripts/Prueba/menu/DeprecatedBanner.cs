using System.Collections;
using UnityEngine;

public class DeprecatedBanner : MonoBehaviour
{
    #region Information
    [Header("Information")]
    [SerializeField] int timeToSpawn = 30;
    [SerializeField] AnimationCurve curveToSpwan;
    WaitForSeconds waitForSeconds;
    Coroutine activeCoroutine;
    #endregion

    #region Components
    RectTransform rect;
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
            if (activeCoroutine == null)
                activeCoroutine = StartCoroutine(ActiveCoroutine());
            else
            {
                StopCoroutine(activeCoroutine);

                rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -13f);
            }
        }
    }

    IEnumerator ActiveCoroutine()
    {
        Vector2 initialPosition = rect.anchoredPosition;

        Vector2 finalPosition = rect.anchoredPosition + new Vector2(0f, rect.sizeDelta.y);

        while (true)
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
    }
}