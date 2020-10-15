using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eleccionAleatoria : MonoBehaviour
{
    List<GameObject> children = new List<GameObject>(); //lista de los hijos para luego elegir uno de ellos aleatoriamente u ordenados... activarlo
    short actualChildren;
    [SerializeField] GameObject imagenFinal,botonExit;    //la lleva "Preguntas" de la escena Draw y el boton de salir
   // [SerializeField] GameObject[] screenPlace;   //lugares donde van los pantallazos
   

    

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("start");
       // imagenFinal.SetActive(false);
        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetActive(false);
        }
        if (gameObject.name == "Panel_Fondos")      
        {
            children[Random.Range(0, children.Count)].SetActive(true); //elija uno aleatorio y activelo --- esto es de sceneDraw.
        }
        else
        {
            children[0].SetActive(true);        //el primero de los hijos... activelo
            actualChildren = 0;
        }
       
    }

    public void Next()  //active el siguiente hijo de la lista
    {
        Debug.Log("afuera");
        if (actualChildren < children.Count-1)    //si el numero en el que se esta actualmente aun se puede sumar...
        {
            
            children[actualChildren].SetActive(false);
            children[actualChildren + 1].SetActive(true);
            actualChildren++;



        }


        else if(gameObject.name=="Preguntas")   // Draw scene --- esta ahi cuadrado por si se neceista ver
        {
           
            imagenFinal.SetActive(true);
            gameObject.SetActive(false);
            botonExit.SetActive(false);
            //aca se suman el contador pines de LogrosManager del main
            // logrosManager.MisionesPin++;
            //  logrosManager.SumaPin();
            logrosManager.LogrosSuma(2,1);      //1= cubo mision 2=pin mision, Valor
            //hay que hacerlo con metodo



            Debug.Log("Termino exitosamente la actividad.");
        }
        else
        {
            gameObject.SetActive(false);

        }
    }


}
