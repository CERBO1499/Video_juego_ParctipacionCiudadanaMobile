using Newtonsoft.Json;
using System.Collections;
using System.Xml.Serialization.Configuration;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetCharacter : MonoBehaviour
{
    #region Information
    [Header("Components")]
    [SerializeField]
    Title title;
    [SerializeField]
    boton currentBtn;
    private static JsonId jsonId;
    public static JsonCharacter jsonCharacter;
    [Space]
    public bool ignore;
    #endregion

    public void Get()
    {
        if(ignore)
            currentBtn.ChooseScene();
        else
            StartCoroutine(AskForTheCharacter());
    }

    IEnumerator AskForTheCharacter()
    {
        UnityWebRequest request = new UnityWebRequest("http://192.168.2.87/apiEscuelaspp/public/Usuarios/" + title.getUser +"/" + title.getPassword, "GET");

        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError)
            Debug.Log(request.error);
        else
        {
            Debug.Log(request.responseCode);

            if (request.downloadHandler.text == "null")
                Debug.Log("Character not found");
            else if (request.downloadHandler.text.Split(':').Length == 2)
            {
                jsonId = JsonConvert.DeserializeObject<JsonId>(request.downloadHandler.text);

                currentBtn.ChooseScene();
            }
            else
            {
                jsonCharacter = JsonConvert.DeserializeObject<JsonCharacter>(request.downloadHandler.text);

                if (jsonCharacter.Genero == "0")
                    sexElection.sexo = 1;
                else
                    sexElection.sexo = 0;

                sexElection.inicio = false;

                selectionFemeleC.NumeroPeloM = int.Parse(jsonCharacter.Cabello);
                selectionFemeleC.NumeroCaraM = int.Parse(jsonCharacter.Cara);
                selectionFemeleC.NumeroAccesorioM = int.Parse(jsonCharacter.Accesorios);
                //selectionFemeleC.NumeroCamisaM = int.Parse(jsonCharacter.Camisa);
                selectionFemeleC.NumeroPantalonM = int.Parse(jsonCharacter.Pantalon);
                selectionFemeleC.NumeroZapatoM = int.Parse(jsonCharacter.Zapatos);

                SceneManager.LoadScene("main");
            }

            Debug.Log("Get Character:" + request.downloadHandler.text);
        }
    }
}