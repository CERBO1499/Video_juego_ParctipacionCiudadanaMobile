using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBotton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject objetoAActivar;     
    


    public void Presionado()
    {
        gameObject.SetActive(false);//oculta el objeto con este script
    }
    public void activar()
    {
        objetoAActivar.SetActive(true);//activa el objeto serializadoa
        //actu
    }

  

}
