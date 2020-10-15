using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlSemilla : MonoBehaviour
{


    [SerializeField] GameObject textSemilla;
    [SerializeField] GameObject miniJuegos;
    List <bool> conteoMiniJuegos = new List <bool>();
    List<GameObject> miniJuegosList = new List<GameObject>();
 
    static int semillas = 0;
    
    void Awake()
    {
        foreach(Transform child in miniJuegos.transform)
        {
            conteoMiniJuegos.Add(true);
            miniJuegosList.Add(child.gameObject);
            

        }
    }

    private void Start() 
    {        
        textSemilla.GetComponent<TMP_Text>().text="="+ semillas;
        ActualizarUI();
    }

    public static void SumarSemilla(int _cantSumarSemilla)
    {
        semillas += _cantSumarSemilla;
       

    }

    public void SumarSemillaMinigame(GameObject miniGame, int cantASumar)
    {
        for(int i=0;i<conteoMiniJuegos.Count;i++)
        {
            if(miniGame.name==miniJuegosList[i].name && conteoMiniJuegos[i]==true)
            {
                conteoMiniJuegos[i]=false;                
                semillas+=cantASumar;
                ActualizarUI();
               
            }
        }
    }


    public void ActualizarUI()
    {
        if(textSemilla != null)
        {          
            textSemilla.GetComponent<TMP_Text>().text="="+semillas;
        }

    }


}