using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Text;

public class ControlSemilla : MonoBehaviour
{
    [SerializeField] GameObject textSemilla;
    [SerializeField] GameObject miniJuegos;
    List <bool> conteoMiniJuegos = new List <bool>();
    List<GameObject> miniJuegosList = new List<GameObject>();

    public static ControlSemilla instance;

    void Awake()
    {
        foreach(Transform child in miniJuegos.transform)
        {
            conteoMiniJuegos.Add(true);
            miniJuegosList.Add(child.gameObject);
        }

        instance = this;
    }

    private void Start() 
    {        
        textSemilla.GetComponent<TMP_Text>().text = JsonContainer.instance.Pcharacter.Semillas;

        ActualizarUI();
    }

    public static void SumarSemilla(int _cantSumarSemilla)
    {
        JsonContainer.instance.StartCoroutine(SetSemillasCoroutine(_cantSumarSemilla.ToString(), instance.ActualizarUI));
    }
    public void SumarSemillaEnEscena(int _cantSumarSemilla)
    {
        JsonContainer.instance.StartCoroutine(SetSemillasCoroutine(_cantSumarSemilla.ToString(), ActualizarUI));
    }
    public void SumarSemillaMinigame(GameObject miniGame, int cantASumar)
    {
        for(int i=0;i<conteoMiniJuegos.Count;i++)
        {
            if(miniGame.name==miniJuegosList[i].name && conteoMiniJuegos[i]==true)
            {
                conteoMiniJuegos[i]=false;

                StartCoroutine(SetSemillasCoroutine(cantASumar.ToString(), ActualizarUI));      
            }
        }
    }

    public void ActualizarUI()
    {
        if(textSemilla != null)
            textSemilla.GetComponent<TMP_Text>().text = JsonContainer.instance.Pcharacter.Semillas;
    }

    public static IEnumerator GetSemillasCoroutine(System.Action output = null)
    {
        if (JsonContainer.instance.Pid.IdUsuaio != "" || JsonContainer.instance.Pcharacter.IdUsuaio != "")
        {
            UnityWebRequest request = new UnityWebRequest("https://www.polygon.us/apiEscuelaspp/public/Semillas/" + JsonContainer.instance.Pcharacter.IdPersonaje.ToString(), "GET");

            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.isNetworkError)
                Debug.Log(request.error);
            else
            {
                Debug.Log("Get Semillas: " + request.responseCode);

                output();
            }
        }
    }

    public static IEnumerator SetSemillasCoroutine(string semillas, System.Action output = null)
    {
        if (JsonContainer.instance.Pid.IdUsuaio != "" || JsonContainer.instance.Pcharacter.IdUsuaio != "")
        {
            UnityWebRequest request = new UnityWebRequest("https://www.polygon.us/apiEscuelaspp/public/Semillas", "POST");

            byte[] body = Encoding.UTF8.GetBytes("{\"IdPersonaje\":\"" + JsonContainer.instance.Pcharacter.IdPersonaje.ToString() + "\",\"Semillas\":\"" + semillas + "\"}");

            request.uploadHandler = new UploadHandlerRaw(body);

            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError)
                Debug.Log(request.error);
            else
            {
                Debug.Log("Set Semillas: " + request.responseCode);

                JsonContainer.instance.StartCoroutine(GetSemillasCoroutine(output));
            }
        }
    }
}