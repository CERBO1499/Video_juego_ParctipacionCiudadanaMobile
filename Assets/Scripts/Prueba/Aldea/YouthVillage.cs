using System;
using System.Collections;
using UnityEngine;

public class YouthVillage : MonoBehaviour
{
    #region Information
    [Header("Information")]
    [SerializeField] GameObject introduction;
    #region Activitys
    [Header("Activitys")]
    [SerializeField] RectTransform[] activitys;
    int activity;
    [SerializeField] AnimationCurve activityCurve;
    #endregion
    #region Events
    Action pass;
    #endregion
    #region Joker
    [Header("Joker")]
    [SerializeField] GameObject separator;
    [SerializeField] Joker[] jokersParts;
    [SerializeField] GameObject close;
    [SerializeField] AnimationCurve jokerCurve;
    int parts = 6;
    #endregion
    #endregion

    private void Awake()
    {
        pass = () =>
        {
            introduction.SetActive(false);
        };
    }

    public void Pass(GameObject gameOject)
    {
        pass?.Invoke();

        gameOject.SetActive(true);

        pass = () =>
        {
            gameOject.SetActive(false);
        };
    }

    public void PassActivating(GameObject gameobject)
    {
        pass?.Invoke();

        pass = () =>
        {
            gameobject.SetActive(false);
        };

        StartCoroutine(ShowActivity(activitys[activity], 0.75f, () =>
        {
            activity++;

            gameobject.SetActive(true);
        }));
    }

    IEnumerator ShowActivity(RectTransform activity, float timetoShow, Action output)
    {
        float t = Time.time;

        while (Time.time <= t + timetoShow)
        {
            activity.localScale = Vector3.one * activityCurve.Evaluate((Time.time - t) / timetoShow);

            yield return null;
        }

        activity.localScale = Vector3.one;

        yield return new WaitForSeconds(timetoShow / 3f);

        output();
    }

    public void ShowItem(GameObject gameObject)
    {
        gameObject.SetActive(true);

        StartCoroutine(ShowItemCorotuine(gameObject));
    }

    IEnumerator ShowItemCorotuine(GameObject gameObject)
    {
        while (true)
        {
            if (Input.anyKeyDown)
            {
                gameObject.SetActive(false);

                break;
            }

            yield return null;
        }
    }

    public void SetActive(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void ShowItem(int jokerPart)
    {
        separator.SetActive(true);

        jokersParts[jokerPart].question.SetActive(true);
    }

    public void QuitPart(int jokerPart)
    {
        parts--;

        jokersParts[jokerPart].question.SetActive(false);

        StartCoroutine(QuitPartCoroutine(jokersParts[jokerPart].squere.GetComponent<RectTransform>()));
    }

    IEnumerator QuitPartCoroutine(RectTransform jockerPart)
    {
        float t = Time.time;

        while (Time.time <= t + 1f)
        {
            jockerPart.localScale = Vector3.one * jokerCurve.Evaluate(Time.time - t);

            yield return null;
        }

        jockerPart.localScale = Vector3.zero;

        separator.SetActive(false);

        if (parts == 0)
            close.SetActive(true);
    }
}

[Serializable]
public class Joker
{
    #region Information
    public GameObject question;
    public GameObject squere;
    #endregion
}