using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMap : MonoBehaviour
{
    [SerializeField] GameObject comunaPapa;  //los transform del minimapa de las 16 comunas 


    List<GameObject> comunas = new List<GameObject>();

    private void Awake()
    {
        foreach (Transform child in comunaPapa.transform)
        {
            comunas.Add(child.gameObject);
        }
    }
    private void Start()
    {
        Debug.Log(comunas.Count);
    }

    public void ChangeComuna(int numeroComuna) //lo recibe 
    {
        transform.position = comunas[numeroComuna-1].transform.position;    //lleva aMiniPlayer al transform de la comuna en el minimapa
      //  Debug.Log("se va a la comuna numero: "+numeroComuna);
    }

}
