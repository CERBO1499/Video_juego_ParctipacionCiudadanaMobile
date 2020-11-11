using Newtonsoft.Json;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetCharacter : MonoBehaviour
{
    #region Static
    public static GetCharacter instance;
    #endregion

    #region Information
    [Header("Components")]
    [SerializeField]
    Title title;
    [SerializeField]
    boton currentBtn;
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
        {
            Debug.Log(request.error);

            banner.Active("No tienes internet, si entras no se guardaron los cambios.");

            ignore = true;

            playBtn.interactable = true;

            playBtn.onClick.RemoveAllListeners();

            playBtn.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
            {
                SceneManager.LoadScene("main");
            }));
        }
        else
        {
            Debug.Log("Ask for character: " + request.responseCode);

            if (request.responseCode == 404)
            {
                banner.Active("Te falta usuario y contraseña.");

                playBtn.interactable = true;
            }
            else if (request.downloadHandler.text == "null")
            {
                banner.Active("Usuario no encontrado.");

                Debug.Log("Character not found");

                playBtn.interactable = true;
            }
            else if (request.responseCode != 500)
            {
                if (request.downloadHandler.text.Split(':').Length == 2)
                {
                    JsonContainer.instance.Pid = JsonConvert.DeserializeObject<JsonId>(request.downloadHandler.text);

                    PlayerPrefs.SetString("User Name", title.getUser);
                    PlayerPrefs.SetString("Password", title.getPassword);

                    currentBtn.ChooseScene();
                }
                else
                {
                    JsonContainer.instance.Pcharacter = JsonConvert.DeserializeObject<JsonCharacter>(request.downloadHandler.text);

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
            else
            {
                banner.Active("El servidor esta caido.");

                playBtn.interactable = true;
            }
        }
    }
}