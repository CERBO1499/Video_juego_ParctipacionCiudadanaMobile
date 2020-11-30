using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorMisions : MonoBehaviour
{
    #region Information
    [Header("Mision Pin")]
    [SerializeField]
    RectTransform PanelSelectorPin;
    [Header("Mision cubo")]
    [SerializeField]
    RectTransform PanelSelectorCubo;

    [Header("Panel Botones")]
    [SerializeField]
    RectTransform PanelBotones;
    #endregion 


    public void SelectPin()
    {
        PanelBotones.gameObject.SetActive(false);
        PanelSelectorPin.gameObject.SetActive(true);       
    }

    public void SelectCubo()
    {
        PanelBotones.gameObject.SetActive(false);
        PanelSelectorCubo.gameObject.SetActive(true);        
    }

    public void GoBack()
    {
        PanelSelectorPin.gameObject.SetActive(false);
        PanelSelectorCubo.gameObject.SetActive(false);
        PanelBotones.gameObject.SetActive(true);
    }
}
