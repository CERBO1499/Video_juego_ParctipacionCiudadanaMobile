using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TelarañaManager : MonoBehaviour
{
    #region Static
    public static TelarañaManager instance;
    #endregion

    #region Information
    int circlesIndex = 0;
    public int images = 0;
    public bool drag;
    bool pass;
    [Header("Information")]
    [SerializeField] GameObject circle;
    [SerializeField] GameObject[] circles;
    [SerializeField] AnimationCurve curve;
    #region Questions
    [SerializeField] Sprite[] questions;
    #endregion
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] GameObject options;
    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(ShowCircle());
    }
    IEnumerator ShowCircle()
    {
        StartCoroutine(ShowCircle(circle));

        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < circles.Length; i++)
        {
            StartCoroutine(ShowCircle(circles[i]));

            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator ShowCircle(GameObject circle)
    {
        circle.SetActive(true);

        Vector3 initialLocalScale = Vector3.zero;

        Vector3 finalLocalScale = Vector3.one;

        float t = Time.time;

        while (Time.time <= t + 0.5f)
        {
            circle.transform.localScale = initialLocalScale + ((finalLocalScale - initialLocalScale) * curve.Evaluate((Time.time - t) / 0.5f));

            yield return null;
        }

        circle.transform.localScale = finalLocalScale;

        if (circles[circles.Length - 1] == circle)
        {
            for (int i = 0; i < circles.Length; i++)
            {
                foreach (Transform element in circles[i].transform)
                    element.gameObject.SetActive(true);
            }

            options.SetActive(true);

            yield return new WaitForSeconds(2f);

            StartCoroutine(GetCircle(circles[circlesIndex]));
        }
    }

    IEnumerator GetCircle(GameObject circle)
    {
        bool founded = false;

        for (int i = 0; i < circles.Length; i++)
        {
            if (founded)
                circles[i].SetActive(false);
            else
                circles[i].SetActive(true);

            if (circles[i] == circle)
                founded = true;
        }

        Vector2[] initialLocalPosition = new Vector2[circle.transform.childCount];

        Vector2[] initialLocalScale = new Vector2[circle.transform.childCount];

        for (int i = 0; i < circle.transform.childCount; i++)
        {
            initialLocalPosition[i] = circle.transform.GetChild(i).transform.localPosition;

            initialLocalScale[i] = circle.transform.GetChild(i).transform.localScale;
        }

        Vector2[] finalLocalPosition = { new Vector2(-410f, -290f), new Vector2(-120f, 170f), new Vector2(455f, 170f), new Vector2(160f, -290f) };

        Vector2[] finalLocalScale = { Vector2.one * 4f, Vector2.one * 4f , Vector2.one * 4f , Vector2.one * 4f };

        float t = Time.time;

        while (Time.time <= t + 0.5f)
        {
            for (int i = 0; i < circle.transform.childCount; i++)
            {
                circle.transform.GetChild(i).localPosition = initialLocalPosition[i] + ((finalLocalPosition[i] - initialLocalPosition[i]) * curve.Evaluate((Time.time - t) / 0.5f));

                circle.transform.GetChild(i).localScale = initialLocalScale[i] + ((finalLocalScale[i] - initialLocalScale[i]) * curve.Evaluate((Time.time - t) / 0.5f));
            }

            yield return null;
        }

        for (int i = 0; i < circle.transform.childCount; i++)
        {
            circle.transform.GetChild(i).localPosition = finalLocalPosition[i];

            circle.transform.GetChild(i).localScale = finalLocalScale[i];

            circle.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);

            circle.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = questions[i];

            drag = true;
        }

        while (!pass)
            yield return null;

        drag = false;

        pass = false;

        t = Time.time;

        while (Time.time <= t + 0.5f)
        {
            for (int i = 0; i < circle.transform.childCount; i++)
            {
                circle.transform.GetChild(i).localPosition = finalLocalPosition[i] + ((initialLocalPosition[i] - finalLocalPosition[i]) * curve.Evaluate((Time.time - t) / 0.5f));

                circle.transform.GetChild(i).localScale = finalLocalScale[i] + ((initialLocalScale[i] - finalLocalScale[i]) * curve.Evaluate((Time.time - t) / 0.5f));
            }

            yield return null;
        }

        for (int i = 0; i < circle.transform.childCount; i++)
        {
            circle.transform.GetChild(i).localPosition = initialLocalPosition[i];

            circle.transform.GetChild(i).localScale = initialLocalScale[i];
        }

        circlesIndex++;

        if (circlesIndex < circles.Length)
        {
            StartCoroutine(GetCircle(circles[circlesIndex]));
        }
    }

    public void SetCircle()
    {
        pass = true;
    }
}