using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlSemilla : MonoBehaviour
{


    [SerializeField] GameObject textSemilla;
    static int semillas = 0;


    private void Start() {

        //textSemilla = GetComponent<TextMeshProUGUI>();
        //textSemilla = GameObject.Find("txtSemilla").GetComponent<TextMeshProUGUI>();
        textSemilla.GetComponent<TMP_Text>().text="="+ semillas;
        ActualizarUI();
    }

    public static void SumarSemilla(int _cantSumarSemilla)
    {
        semillas += _cantSumarSemilla;
        print("Semillas sumada" + semillas);

    }


    public void ActualizarUI()
    {
        if(textSemilla != null){
            // textSemilla.GetComponent<TMP_Text>().text="="+semillas;
            textSemilla.GetComponent<TMP_Text>().text="="+semillas;
        }

    }


}