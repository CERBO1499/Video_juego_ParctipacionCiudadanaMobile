using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeUp : MonoBehaviour
{
    public GameObject seleccionarEdad;
    public RectTransform sexPanel;
    public GameObject nexstScenegame;

    // Start is called before the first frame update
    void Start()
    {
        if(JsonContainer.instance.Pcharacter.IdPersonaje != "")
        {
            seleccionarEdad.SetActive(false);

            sexPanel.anchoredPosition = new Vector2(-185f,0f);

            nexstScenegame.SetActive(true);
        }
    }
}
