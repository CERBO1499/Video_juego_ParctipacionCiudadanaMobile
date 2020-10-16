using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class boton : MonoBehaviour
{

    [SerializeField] Transform objetive; //el transform del Pin para que la brujula lo mire.

   
    
    public void PressScript()
    {        
        
        SceneManager.LoadScene("main");

    }
    public void ChooseScene()
    {
        SceneManager.LoadScene("choseScene");
    }

    public void BrujulaNuevoObjetivo()
    {
        GameObject bruj = GameObject.Find("brujula");
        bruj.GetComponent<brujula>().NewObjetivo(objetive);   //va a darle la nueva ubicacion a la cual mirar (la brujula)
        bruj.GetComponentInChildren<ParticleSystem>().Play();
    }
    public void CloseApp()
    {
        Application.Quit();
    }


    


}
