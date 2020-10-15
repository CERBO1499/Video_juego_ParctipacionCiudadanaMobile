using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPrefs_ : MonoBehaviour
{
    /*
     guardaremos:
    -   numero misiones PIN
    -   Numero misiones Cubo
    -   numero de lugares especiales ---> usarlo para la persistencia entre escenas
    -   Preferencias de Avatar
    -   numero de semillas
     
     */
    private void Start()
    {
        // SemillaStore();
        Debug.Log("semillas player prefs" + PlayerPrefs.GetInt("semillas"));
        Debug.Log("semillas estatico: " + ControlSemilla.Semillas);
    }

    void SemillaStore()
    {
        PlayerPrefs.SetInt("semillas", ControlSemilla.Semillas);
        Debug.Log("semillas playerPrefs" + PlayerPrefs.GetInt("semillas")) ;
        Debug.Log("semillas estatico: "+ ControlSemilla.Semillas);
    }



}
