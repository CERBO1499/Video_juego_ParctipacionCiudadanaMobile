using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class comunaDetect : MonoBehaviour
{

    [SerializeField]TextMeshProUGUI text;
    [SerializeField] int numeroComuna;
    [SerializeField] string nombreComuna;
     GameObject miniPlayer;
    private string comuna;


    private void Awake()
    {
        miniPlayer = GameObject.Find("MiniPlayer"); //Objeto de UI que se para en el minimapa
    }
    void Start()
    {
        text = GameObject.FindGameObjectWithTag("comuna").GetComponent<TextMeshProUGUI>();//Tag corresponde al UI
        comuna = "Comuna";
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (numeroComuna <= 16) { 
            text.text = comuna +" "+ numeroComuna.ToString()+" "+ nombreComuna;
            }else
            {
                text.text = "Corregimiento "+nombreComuna;
            }
            miniPlayer.GetComponent<miniMap>().ChangeComuna(numeroComuna);
        }
    }
}
