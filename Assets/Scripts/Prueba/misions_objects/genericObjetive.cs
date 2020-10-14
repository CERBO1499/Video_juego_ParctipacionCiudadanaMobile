using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class genericObjetive : MonoBehaviour
{

    Vector3 rotationDir;

    bool esObjetivo;
    //[SerializeField] short linkNumber;
    //[SerializeField] string link;
    [SerializeField] GameObject imagenjuegoUi;
    ControlSemilla controlSemilla;

    
    bool ActividadRealizada = false;
   // controlProgreso ctrlProgress;


    private void Awake()
    {
        if (gameObject.tag == "pin") esObjetivo = true; //si es un pin activeme el booleano
        else if(true) { esObjetivo = false; }

    }
    void Start() {

        if (esObjetivo==false)rotationDir = new Vector3(45,45,45);
        else if (esObjetivo)
        {
            rotationDir = new Vector3(0, 45, 0);
        }
        imagenjuegoUi.SetActive(false);
    }

    void Update()
    {
        transform.Rotate(rotationDir*Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (ActividadRealizada == false)
            {
               // ctrlProgress.Actividadesrealizadas++;
                ActividadRealizada = true;
            }
            imagenjuegoUi.SetActive(true);
            //gameObje0ct.SetActive(false);

            controlSemilla=GameObject.Find("ControlSemilla").GetComponent<ControlSemilla>();
            controlSemilla.SumarSemillaMinigame(gameObject,5);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            imagenjuegoUi.SetActive(false);
        }
    }
}