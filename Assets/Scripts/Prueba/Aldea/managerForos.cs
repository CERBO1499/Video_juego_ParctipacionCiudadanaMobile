using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerForos : MonoBehaviour
{
    #region Information

    [Header("Informacion para Activar Foros")]
    [SerializeField]
    RectTransform btnForos;
    [SerializeField]
    RectTransform foroGeneral;

    [Header("PanelForos")]
    [SerializeField]
    RectTransform inicioForo;
    [SerializeField]
    RectTransform foroFamilia;
    [SerializeField]
    RectTransform foroAprender;
    [SerializeField]
    RectTransform foroDeporte;
    [SerializeField]
    RectTransform foroGamer;

    [SerializeField]
    RectTransform finalImage;
    #endregion
    public void PressedBtnForos()
    {
        btnForos.gameObject.SetActive(false);
        foroGeneral.gameObject.SetActive(true);
    }

    public void primerForo()
    {
        inicioForo.gameObject.SetActive(false);
        foroFamilia.gameObject.SetActive(true);
    }
    public void SegundoForo()
    {
        foroFamilia.gameObject.SetActive(false);
        foroAprender.gameObject.SetActive(true);
    }
    public void TercerForo()
    {
        foroAprender.gameObject.SetActive(false);
        foroDeporte.gameObject.SetActive(true);
    }
    public void CuartoForo()
    {
        foroDeporte.gameObject.SetActive(false);
        foroGamer.gameObject.SetActive(true);
    }

    public void FinalForos()
    {
        foroGamer.gameObject.SetActive(false);
        finalImage.gameObject.SetActive(true);
    }

}
