using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sexElection : MonoBehaviour
{
    [SerializeField] GameObject hombre,mujer;
    public static short sexo = 0;  //0 = hombre, 1=mujer
    public static bool inicio=true;


    private void Awake()
    {
        if (inicio == false)
        {
            if (sexo == 0)
            {
                Destroy(mujer);
                hombre.SetActive(true);
            }
            else
            {
                Destroy(hombre);
                mujer.SetActive(true);
            }
        }
    }
    private void Start()
    {
        inicio = false;
    }
    //metodos en menú escoger personaje:
    public void CambioSexo()
    {
        if (sexo == 0)
        {
            mujer.SetActive(true);
            sexo = 1;
            hombre.SetActive(false);
        }
        else
        {
            mujer.SetActive(false);
            sexo = 0;
            hombre.SetActive(true);
        }
    }

    //En el cambio de escena 

    public void CambioEscenaSexo()
    {
        if (sexo == 0)
        {
            Destroy(mujer);         
            hombre.SetActive(true);
        }
        else
        {
            Destroy(hombre); 
            mujer.SetActive(true);
        }
    }

}
