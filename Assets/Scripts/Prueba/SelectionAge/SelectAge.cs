using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAge : MonoBehaviour
{
    #region Information
    [Header("Botones")]
    [SerializeField]RectTransform btnNiño;
    [SerializeField]RectTransform btnJoven;
    [SerializeField]RectTransform btnJugar;
    [SerializeField]AnimationCurve curve;

    bool firstResponse=false;
    #endregion


    public void UserResponseNiño()
    {
        Button tmpButton;
        tmpButton = btnNiño.GetComponent<Button>();
        tmpButton.interactable = false;

        Button tmpButtonActive;
        tmpButtonActive = btnJoven.GetComponent<Button>();
        tmpButtonActive.interactable = true;

        StartCoroutine(AnimationButton());
    }
    public void UserResponseJoven()
    {
        Button tmpButton;
        tmpButton = btnNiño.GetComponent<Button>();
        tmpButton.interactable = true;

        Button tmpButtonActive;
        tmpButtonActive = btnJoven.GetComponent<Button>();
        tmpButtonActive.interactable = false;

        StartCoroutine(AnimationButton());
    }
    IEnumerator AnimationButton()
    {
        if (!firstResponse)
        {
            btnJugar.gameObject.SetActive(true);

            Vector3 initialSize = Vector3.zero;
            Vector3 finalSize = Vector3.one;
            float t = Time.time;

            while (Time.time <= t + 0.5f)
            {
                btnJugar.localScale = initialSize + ((finalSize - initialSize) * curve.Evaluate((Time.time - t) / 0.5f));
                yield return null;
            }
            btnJugar.localScale = finalSize;
            firstResponse = true;

        }


    }
}
