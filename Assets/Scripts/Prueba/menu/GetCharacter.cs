using Newtonsoft.Json;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetCharacter : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string getCharacter(string userName, string password);

    #region Static
    public static GetCharacter instance;
    #endregion

    public bool ignore;
    [Space]
    #region Components
    [SerializeField]
    Title title;
    [SerializeField]
    Button playBtn;
    [SerializeField]
    DeprecatedBanner banner;
    #endregion

    public void Get()
    {
        if (!ignore)
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
                StartCoroutine(AskForTheCharacter());
            else
            {
                GameObject receiver = new GameObject("Receiver");

                receiver.AddComponent<Response>().output = (string data) =>
                {
                    Debug.Log("Ask for character: " + data);

                    if (data == ">_<")
                    {
                        playBtn.interactable = true;

                        banner.Active("Te falta usuario y contraseña.");
                    }
                    else if (data == "null")
                    {
                        playBtn.interactable = true;

                        banner.Active("Usuario no encontrado.");
                    }
                    else
                        SetTCharacter(data);
                };

                getCharacter(title.getUser, title.getPassword);
            }

        }
        else
            SceneManager.LoadScene("choseScene");
    }

    public void Choose()
    {
        SceneManager.LoadScene("choseScene");
    }

    IEnumerator AskForTheCharacter()
    {
        UnityWebRequest request = new UnityWebRequest("https://polygon.us/apiEscuelaspp/public/Usuarios/" + title.getUser +"/" + title.getPassword, "GET");

        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Access-Control-Allow-Origin", "*");

        request.SetRequestHeader("Access-Control-Allow-Methods", "GET");

        request.SetRequestHeader("Access-Control-Allow-Headers", "accept, content-type");

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            ignore = true;

            playBtn.interactable = true;

            banner.Active(request.error);
        }
        else
        {
            Debug.Log("Ask for character: " + request.responseCode);

            if (request.responseCode == 404)
            {
                playBtn.interactable = true;

                banner.Active("Te falta usuario y contraseña.");
            }
            else if (request.downloadHandler.text == "null")
            {
                playBtn.interactable = true;

                banner.Active("Usuario no encontrado.");
            }
            else if (request.responseCode != 500)
                SetTCharacter(request.downloadHandler.text);
            else
            {
                playBtn.interactable = true;

                banner.Active("El servidor esta caido.");
            }
        }
    }

    public void SetTCharacter(string data)
    {
        if (data.Split(':').Length == 2)
        {
            JsonContainer.instance.Pid = JsonConvert.DeserializeObject<JsonId>(data);

            PlayerPrefs.SetString("User Name", title.getUser);
            PlayerPrefs.SetString("Password", title.getPassword);

            SceneManager.LoadScene("choseScene");
        }
        else
        {
            JsonContainer.instance.Pcharacter = JsonConvert.DeserializeObject<JsonCharacter>(data);

            PlayerPrefs.SetString("User Name", title.getUser);
            PlayerPrefs.SetString("Password", title.getPassword);

            if (JsonContainer.instance.Pcharacter.Genero == "0")
            {
                sexElection.sexo = 1;

                selectionFemeleC.NumeroPeloM = int.Parse(JsonContainer.instance.Pcharacter.Cabello);
                selectionFemeleC.NumeroCaraM = int.Parse(JsonContainer.instance.Pcharacter.Cara);
                selectionFemeleC.NumeroAccesorioM = int.Parse(JsonContainer.instance.Pcharacter.Accesorios);
                selectionFemeleC.NumeroCamisaM = int.Parse(JsonContainer.instance.Pcharacter.Camisa);
                selectionFemeleC.NumeroPantalonM = int.Parse(JsonContainer.instance.Pcharacter.Pantalon);
                selectionFemeleC.NumeroZapatoM = int.Parse(JsonContainer.instance.Pcharacter.Zapatos);
            }
            else
            {
                sexElection.sexo = 0;

                selectionCharacter.NumeroPelo = int.Parse(JsonContainer.instance.Pcharacter.Cabello);
                selectionCharacter.NumeroCara = int.Parse(JsonContainer.instance.Pcharacter.Cara);
                selectionCharacter.NumeroAccesorio = int.Parse(JsonContainer.instance.Pcharacter.Accesorios);
                selectionCharacter.NumeroCamisa = int.Parse(JsonContainer.instance.Pcharacter.Camisa);
                selectionCharacter.NumeroPantalon = int.Parse(JsonContainer.instance.Pcharacter.Pantalon);
                selectionCharacter.NumeroZapato = int.Parse(JsonContainer.instance.Pcharacter.Zapatos);
            }

            sexElection.inicio = false;

            PlayerPrefs.SetInt("semillas", int.Parse(JsonContainer.instance.Pcharacter.Semillas));

            SceneManager.LoadScene("main");
        }
    }
}