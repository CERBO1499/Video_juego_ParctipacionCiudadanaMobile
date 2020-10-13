using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lugarEspecial : MonoBehaviour
{

    [SerializeField] GameObject UiPuntoEstrategico;
    [SerializeField] GameObject logrosManager;
    GameObject brujula;
    

    private void Awake()
    {
        brujula = GameObject.FindGameObjectWithTag("brujula");  //detecta brujula en el mapa
    }
    void Start()
    {
        brujula.GetComponent<brujula>().NewObjetivo(transform);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
           // print("conecta imagen ref");
            UiPuntoEstrategico.SetActive(true);
            logrosManager.GetComponent<logrosManager>().Lugar(gameObject);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
           // print("conecta imagen ref");
            UiPuntoEstrategico.SetActive(false);

        }
    }

    public void Objetivo()
    {
        brujula.GetComponent<brujula>().NewObjetivo(transform); //aun no esta listo
    }

}
