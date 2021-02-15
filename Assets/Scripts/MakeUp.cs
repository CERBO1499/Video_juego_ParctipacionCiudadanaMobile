using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeUp : MonoBehaviour
{
    public GameObject seleccionarEdad;
    public RectTransform botones;
    public RectTransform sexPanel;
    public GameObject nexstScenegame;

    // Start is called before the first frame update
    void Start()
    {
        if(JsonContainer.instance.Pcharacter.IdPersonaje != "")
        {
            seleccionarEdad.SetActive(false);

            botones.anchoredPosition = new Vector2(40f, botones.anchoredPosition.y);

            sexPanel.anchoredPosition = new Vector2(-492.02f, sexPanel.anchoredPosition.y);

            sexPanel.localScale = Vector3.one;

            nexstScenegame.GetComponent<RectTransform>().anchoredPosition = new Vector2(-438f, nexstScenegame.GetComponent<RectTransform>().anchoredPosition.y);

            nexstScenegame.SetActive(true);
        }
    }
}
