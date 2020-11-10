using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerPregunta : MonoBehaviour
{
    #region Information
    [Header("Ingormation")]
    [SerializeField] float timeToAppear=0.2f;
    [SerializeField] AnimationCurve curveToSpawn;

    #endregion
    #region Components
    RectTransform rect;
    #endregion
    #region PrivateVariables
    [SerializeField]bool ActiveBanner;
    #endregion
    #region Propiedades encapsuladas
    public bool ActiveBanner1 { get => ActiveBanner; set => ActiveBanner = value; }
    #endregion

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void ActiveBannerWithPlayer()
    {
        if (rect != null)
        {
            
        }
    }
    /*IEnumerator DownBanner()
    {
        Vector2 intialPosition = rect.anchoredPosition;
        Vector2 finalPosition = rect.anchoredPosition + new Vector2(0f, rect.sizeDelta.y);

        rect.anchoredPosition = intialPosition + ((finalPosition - intialPosition) * curveToSpawn.Evaluate(Time.deltaTime));


    }*/

}
