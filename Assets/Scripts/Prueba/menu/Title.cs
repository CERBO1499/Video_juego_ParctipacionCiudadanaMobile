using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    #region Information
    [Header("Information")]
    [SerializeField] GameObject play;
    [SerializeField] GameObject logo;
    [SerializeField] AnimationCurve curve;
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] Image titleImg;  
    #endregion

    void Awake()
    {
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        Color initialColor = titleImg.color;

        Color finalColor = Color.white;

        float t = Time.time;

        while (Time.time <= t + 2f)
        {
            titleImg.color = initialColor + ((finalColor - initialColor) * curve.Evaluate((Time.time - t) / 2f));

            yield return null;
        }

        titleImg.color = finalColor;

        play.SetActive(true);

        logo.SetActive(true);
    }
}