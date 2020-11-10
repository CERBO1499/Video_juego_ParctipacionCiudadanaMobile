using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
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
    [Space]
    #region Components
    [SerializeField]
    Button playBtn;
    [SerializeField]
    DeprecatedBanner banner;
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

            if (request.responseCode == 404)
            {
                Debug.Log("404");

                banner.Active("Te falta usuario y contraseña");

                playBtn.interactable = true;
            }
            else if (request.downloadHandler.text == "null")
            {
                banner.Active("Usuario no encontrado");

                Debug.Log("Character not found");

                playBtn.interactable = true;
            }
            else if (request.downloadHandler.text.Split(':').Length == 2)
            {
                jsonId = JsonConvert.DeserializeObject<JsonId>(request.downloadHandler.text);

                PlayerPrefs.SetString("User Name", title.getUser);
                PlayerPrefs.SetString("Password", title.getPassword);

                currentBtn.ChooseScene();
            }
            else
            {
                jsonCharacter = JsonConvert.DeserializeObject<JsonCharacter>(request.downloadHandler.text);

                PlayerPrefs.SetString("User Name", title.getUser);
                PlayerPrefs.SetString("Password", title.getPassword);

                if (jsonCharacter.Genero == "0")
                {
                    sexElection.sexo = 1;

                    selectionFemeleC.NumeroPeloM = int.Parse(jsonCharacter.Cabello);
                    selectionFemeleC.NumeroCaraM = int.Parse(jsonCharacter.Cara);
                    selectionFemeleC.NumeroAccesorioM = int.Parse(jsonCharacter.Accesorios);
                    //selectionFemeleC.NumeroCamisaM = int.Parse(jsonCharacter.Camisa);
                    selectionFemeleC.NumeroPantalonM = int.Parse(jsonCharacter.Pantalon);
                    selectionFemeleC.NumeroZapatoM = int.Parse(jsonCharacter.Zapatos);
                }
                else
                {
                    sexElection.sexo = 0;

                    selectionCharacter.NumeroPelo = int.Parse(jsonCharacter.Cabello);
                    selectionCharacter.NumeroCara = int.Parse(jsonCharacter.Cara);
                    selectionCharacter.NumeroAccesorio = int.Parse(jsonCharacter.Accesorios);
                    //selectionCharacter.NumeroCamisa = int.Parse(jsonCharacter.Camisa);
                    selectionCharacter.NumeroPantalon = int.Parse(jsonCharacter.Pantalon);
                    selectionCharacter.NumeroZapato = int.Parse(jsonCharacter.Zapatos);
                }

                sexElection.inicio = false;

                PlayerPrefs.SetInt("semillas", int.Parse(jsonCharacter.Semillas));

                SceneManager.LoadScene("main");
            }

            Debug.Log("Get Character:" + request.downloadHandler.text);
        }
    }
}