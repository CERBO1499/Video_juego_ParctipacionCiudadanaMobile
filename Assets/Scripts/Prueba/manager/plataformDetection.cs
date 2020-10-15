using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android && gameObject.name== "instrucciones print")    //aplicado en scene draw : a -> instruction print
        {
            Destroy(gameObject);
        }
    }


}
