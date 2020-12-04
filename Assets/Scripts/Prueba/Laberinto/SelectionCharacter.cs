using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionCharacter : MonoBehaviour
{
    #region Information
    [Header("Characters")]
    [SerializeField]
    Image Player;
    [SerializeField]
    Sprite sprWaira, sprAlejo;

    [Header("Instrcucciones")]
    [SerializeField]
    RectTransform imgWairaInstuctions;
    [SerializeField]
    RectTransform imgAlejoInstructions;
    [SerializeField]
    RectTransform panelBotones;
    #endregion

    public void SelectWaira()
    {
        Player.sprite = sprWaira;
        OpenWairaInstructions();
    }

    public void SelectAlejo()
    {
        Player.sprite = sprAlejo;
        OpenAlejoInstructions();
    }

    void OpenWairaInstructions()
    {
        imgWairaInstuctions.gameObject.SetActive(true);
        imgAlejoInstructions.gameObject.SetActive(false);
        panelBotones.gameObject.SetActive(false);
        
    }
    void OpenAlejoInstructions()
    {
        imgAlejoInstructions.gameObject.SetActive(true);
        imgWairaInstuctions.gameObject.SetActive(false);
        panelBotones.gameObject.SetActive(false);
    }
}
