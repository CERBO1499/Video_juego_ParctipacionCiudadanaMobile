using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Cameraman : MonoBehaviour
{
    #pragma warning disable CS0414

    [Space(order = 1)]
    #region Components
    [Header("Components", order = 2)]
    [SerializeField] Scenemanager sceneManager;
    [SerializeField] Photo photo;
    #region
    [SerializeField] bool showPhoto;
    [SerializeField] 
    #endregion
    #endregion

    void OnValidate()
    {
        if (photo != null)
            photo.PrawImage.SetActive(showPhoto);
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (sceneManager != null)
            sceneManager.onToMainGame = () =>
            {
                if (JsonContainer.instance.Pid.IdUsuaio == "" && JsonContainer.instance.Pcharacter.IdUsuaio == "")
                    SceneManager.LoadScene("main", LoadSceneMode.Single);
                else
                    SendPhoto(() =>
                    {
                        SceneManager.LoadScene("main", LoadSceneMode.Single);
                    });
            };

        if(photo != null)
            photo.PrawImage.SetActive(showPhoto);
    }

    public void SendPhoto(Action output = null)
    {
        StartCoroutine(SendPhotoCoroutine(output));
    }

    IEnumerator SendPhotoCoroutine(Action output = null)
    {
        UnityWebRequest request = new UnityWebRequest("https://www.polygon.us/apiEscuelaspp/public/Personaje", "POST");

        JsonContainer.instance.Pcharacter = CreateJsonCharacter();

        byte[] body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(JsonContainer.instance.Pcharacter));

        request.uploadHandler = new UploadHandlerRaw(body);

        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError)
            Debug.Log(request.error);
        else
        {
            Debug.Log("Set Character: " + request.responseCode);

            JsonContainer.instance.Pcharacter.IdPersonaje = request.downloadHandler.text;

            output?.Invoke();
        }
    }

    JsonCharacter CreateJsonCharacter()
    {
        JsonCharacter jsonCharacter = new JsonCharacter
        {
            IdPersonaje = "0",
            IdUsuaio = (JsonContainer.instance.Pid.IdUsuaio != "") ? JsonContainer.instance.Pid.IdUsuaio : JsonContainer.instance.Pcharacter.IdUsuaio,
            Genero = (sexElection.sexo == 0) ? "1" : "0",
            Cabello = (sexElection.sexo == 1) ? selectionFemeleC.NumeroPeloM.ToString() : selectionCharacter.NumeroPelo.ToString(),
            Cara = (sexElection.sexo == 1) ? selectionFemeleC.NumeroCaraM.ToString() : selectionCharacter.NumeroCara.ToString(),
            Accesorios = (sexElection.sexo == 1) ? selectionFemeleC.NumeroAccesorioM.ToString() : selectionCharacter.NumeroAccesorio.ToString(),
            Camisa = (sexElection.sexo == 1) ? selectionFemeleC.NumeroCamisaM.ToString() : selectionCharacter.NumeroCamisa.ToString(),
            Pantalon = (sexElection.sexo == 1) ? selectionFemeleC.NumeroPantalonM.ToString() : selectionCharacter.NumeroPantalon.ToString(),
            Zapatos = (sexElection.sexo == 1) ? selectionFemeleC.NumeroZapatoM.ToString() : selectionCharacter.NumeroZapato.ToString(),
            Semillas = (JsonContainer.instance.Pid.IdUsuaio != "") ? "0" : JsonContainer.instance.Pcharacter.Semillas,
            Old = JsonContainer.instance.Pcharacter.Old
        };

        Texture2D texture = new Texture2D(512, 512, TextureFormat.RGB24, false);

        RenderTexture.active = photo.PrenderTexture;

        texture.ReadPixels(new Rect(0, 0, photo.PrenderTexture.width, photo.PrenderTexture.height), 0, 0);

        texture.Apply();

        jsonCharacter.FotoPerfil = Convert.ToBase64String(texture.EncodeToPNG());

        return jsonCharacter;
    }
}