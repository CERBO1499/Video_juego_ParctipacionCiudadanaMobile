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
            } else if (gameObject.name == "Button_Link_nuestrasM")
            {
                gameObject.transform.position += new Vector3(0f, 231f, 0f);
            }
        }
       
    }


}
