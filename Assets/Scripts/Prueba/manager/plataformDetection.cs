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
       // gameObject.transform.position += new Vector3(180f, 0f, 0f);
    }


}
