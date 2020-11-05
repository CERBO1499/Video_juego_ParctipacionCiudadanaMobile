using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class logrosManager : MonoBehaviour
{
    static int misionesPin = 0; //conteo de cuantas misiones pin van realizadas
    static int misionesCubo = 0;//conteo de misiones Cubo realizadas
    static int lugaresEspeciales = 0;
    
    [SerializeField] GameObject lugaresEspecialesList;  //me cuenta los objetos reales ... esto es para comparar y no repetir la suma
     List<bool> conteoLugares = new List<bool>();  //mirara si el lugar ya fue accedido por medio de esta lista comparando con el .count de lugares especiales escenario
     List<GameObject> lugaresEspecialesObject = new List<GameObject>();  //la lista que me guarda los objetos para comparar si el lugar especial ya se tocó
    [SerializeField] GameObject pinesEscenario,cubosEscenario;
    List<bool> cantidadPin = new List<bool>();
    List<short> cantidadCubo  = new List<short>();
    [SerializeField] GameObject misionesPinUi,misionesCuboUi,lugaresUi;   //gameObject "estadisticas" en el Ui del main
    [SerializeField] GameObject[] chuloPin;
    [SerializeField] GameObject[] candadoPin;
    [SerializeField] GameObject[] candadoCubo;
    [SerializeField] GameObject[] chuloCubo;
    [SerializeField] GameObject[] chuloPos, candadoPos;

    public static int MisionesPin { get => misionesPin; set => misionesPin = value; }
    //      --- PARA PRUEBA singleton
    static bool entroPrimeraVez = true;

    //  [SerializeField] GameObject textoUi;

    


    private static logrosManager _instance;

    public static logrosManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<logrosManager>();
            }
            return _instance;
        }
    }


    private void Awake()
    {
       
       // Debug.Log(entroPrimeraVez);
        //leer cantidad de la lista de cubos y pin y lugaresEspeciales
        foreach (Transform child in pinesEscenario.transform)           //me va a crear un booleano en la lista ... la misma cantidad de pines de mision en el escenario.
        {
            cantidadPin.Add(true);
        }
        foreach (Transform child in cubosEscenario.transform)           //me va a crear un booleano en la lista ... la misma cantidad de pines de mision en el escenario.
        {
            cantidadCubo.Add(0);
        }
       
            
           foreach (Transform child in lugaresEspecialesList.transform)
            {
                conteoLugares.Add(true);
                lugaresEspecialesObject.Add(child.gameObject);
            
            }

        misionesCubo = PlayerPrefs.GetInt("misionesCubo");
        misionesPin = PlayerPrefs.GetInt("misionesPin");

    }
    private void Start()
    {
        
       // Debug.Log("entro en este start");
        misionesPinUi.GetComponent<TMP_Text>().text = "Misiones pin: "+ misionesPin; //me da el porcentaje de cuantas misiones pin que llevo
        misionesCuboUi.GetComponent<TMP_Text>().text = "Misiones cubo: " + misionesCubo;//me da el porcentaje de cuantas misiones cubo que llevo

        PinUi();
        CuboUi();
        LugarUi();
        
        //identifique si en el UI existe tableroLogros ... si existe entonces actualice los porcentajes en UI

        // textoUi.GetComponent<Text>().text = "Pines:" + booleanPin.Count/misionesPin;
    }




    //      Desbloqueo UI
    void PinUi()
    {
        if(misionesPin >= 3)
            {
            chuloPin[0].SetActive(true);    //activa el chulo del Ui de la mision completa 3 juego pin
            candadoPin[0].SetActive(false);
            chuloPin[1].SetActive(true);    //activa el chulo del Ui de la mision completa 3 juego pin
            candadoPin[1].SetActive(false); 

            //activa el chulo y desactiva el candado
        }
        else if (misionesPin >= 1)
        {
            chuloPin[0].SetActive(true);   
            candadoPin[0].SetActive(false); 
        }

    }

    public void CuboUi()
    {
        misionesCuboUi.GetComponent<TMP_Text>().text = "Misiones cubo: " + misionesCubo;
        if (misionesCubo >= 1)
        {
            chuloCubo[0].SetActive(true);    
            candadoCubo[0].SetActive(false);
            

            //activa el chulo y desactiva el candado
        }
       

    }
    void LugarUi()
    {
        lugaresEspeciales = PlayerPrefs.GetInt("lugaresEspeciales");
        lugaresUi.GetComponent<TMP_Text>().text = "Lugares descubiertos: " + lugaresEspeciales + "/" + lugaresEspecialesObject.Count;
       // Debug.Log(lugaresEspeciales);
        if (lugaresEspeciales >= 125)
        {
            chuloPos[2].SetActive(true);
            candadoPos[2].SetActive(false);
            chuloPos[1].SetActive(true);
            candadoPos[1].SetActive(false);
            chuloPos[0].SetActive(true);
            candadoPos[0].SetActive(false);
        }
        else if (lugaresEspeciales >= 25)
        {
            chuloPos[1].SetActive(true);
            candadoPos[1].SetActive(false);
            chuloPos[0].SetActive(true);
            candadoPos[0].SetActive(false);
        }
        else if (lugaresEspeciales >= 5)
        {
            chuloPos[0].SetActive(true);
            candadoPos[0].SetActive(false);
        }
    }

    public void Lugar(GameObject lugar)         //se llama desde LugarEspecial
    {
        for (int i=0;i<conteoLugares.Count;i++)
        {
            if (lugar.name == lugaresEspecialesObject[i].name&& conteoLugares[i] == true)
            {
                conteoLugares[i] = false;
                

                lugaresEspeciales++;
                lugaresUi.GetComponent<TMP_Text>().text = "Lugares descubiertos: "+lugaresEspeciales+"/"+lugaresEspecialesObject.Count;
                PlayerPrefs.SetInt("lugaresEspeciales", lugaresEspeciales);

            }

            if (lugaresEspeciales >= 125)
            {
                lugaresEspeciales = conteoLugares.Count-1;
                chuloPos[2].SetActive(true);
                candadoPos[2].SetActive(false);
            }
            else if (lugaresEspeciales>=25)
            {
                chuloPos[1].SetActive(true);
                candadoPos[1].SetActive(false);
            }
            else if(lugaresEspeciales >= 5)
            {
                chuloPos[0].SetActive(true);
                candadoPos[0].SetActive(false);
            }
        }
    }

    //fin desbloque Ui



    //Prueba metodo estatico                                                                ******************* este es el metodo usado para sumar misiones: ***********************
    public static void LogrosSuma(int tipo,int cantidad)        //tipo= Pin o cubo    1= cubo    2= pin
    {
        if (tipo == 1)          //si es tipo CUBO
        {
            misionesCubo+= cantidad;
            PlayerPrefs.SetInt("misionesCubo",misionesCubo);
          //  Debug.Log("entró por metodo a cubo");
        } else if (tipo == 2)   //si es tipo PIN 
        {
            misionesPin+=cantidad;
            PlayerPrefs.SetInt("misionesPin", misionesPin);
            //CuboUi();
            // Debug.Log("entró por metodo a pin");
        }

    }



    /*
     - se llama desde: GAMEMANAGERPREGUNTADOS --- en preguntados
     - se llama desde: eleccionAlearotia --- en drawScene
     */
    //fin prueba metodo est<atico




}
