using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)    
        {
            if(gameObject.name == "instrucciones print")
            { //aplicado en scene draw : a -> instruction print
                Destroy(gameObject);
            }
            else if(gameObject.name == "Lugares especiales-UI")
            {
                gameObject.transform.localScale = new Vector3(0.65f,0.65f,0.65f);
                gameObject.transform.position += new Vector3(180f, 0f, 0f);
            } else if (gameObject.name == "Button_continuar") //boton continuar -- escena: nuestrasMelodias
            {
                gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
                gameObject.transform.position += new Vector3(-50.5f, 60.5f, 0f);
            }

        }
        else
        {
            if (gameObject.name == "Image_Geoguess")
            {
                gameObject.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                gameObject.transform.position += new Vector3(0f, 44f, 0f);
            }else if(gameObject.name== "Button_Exit")
            {
                Destroy(gameObject);
            } 
            else if (gameObject.name == "btnclose_scene")  //boton cerrar actividad de "nuestrasMelodias"
            {
                gameObject.transform.localScale = new Vector3(1f,1f,1f);
                gameObject.transform.position += new Vector3(67.5f,68.5f,0f);
            }else if (gameObject.name == "ButtonFinalizado")    //escena nuestrasMelodias
            {
                gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                gameObject.transform.localPosition += new Vector3(190f, -150f, 0f);
            }
        }
       
    }


}
