using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brujula : MonoBehaviour
{

    Transform objetivo;
    [SerializeField] Transform player;
    [SerializeField] float damping;
    

    public void NewObjetivo(Transform objetivee)
    {
        objetivo = objetivee;
    }
    void Update()
    {
        if (objetivo != null)
        {     
            LookOnYAxis();     
                 
            //transform.LookAt(objetivo);          

        }
    }

    void LookOnYAxis()
    {
        Vector3 lookPos = objetivo.position-transform.position;
        lookPos.y=0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation=Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime*damping);
    }


}

