using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Cameraman : MonoBehaviour
{
    #region Singleton
    public static Cameraman instance;
    #endregion
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
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        if (sceneManager != null)
            sceneManager.onToMainGame = () =>
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player == null)
                    Debug.LogWarning("No se encontro el jugdor, revisa si este tiene el tag de player");
                else
                {
                    if (JsonContainer.instance.Pid.IdUsuaio == "" && JsonContainer.instance.Pcharacter.IdUsuaio == "")
                        SceneManager.LoadScene("main", LoadSceneMode.Single);
                    else
                        SendPhoto(() =>
                        {
                            SceneManager.LoadScene("main", LoadSceneMode.Single);
                        });
                }
            };

        if(photo != null)
            photo.PrawImage.SetActive(showPhoto);
    }

    public Texture TakePhoto()
    {
        return photo.PrenderTexture;
    }

    public void SendPhoto(Action output = null)
    {
        StartCoroutine(SendPhotoCoroutine(output));
    }

    IEnumerator SendPhotoCoroutine(Action output = null)
    {
        UnityWebRequest request = new UnityWebRequest("https://polygon.us/apiEscuelaspp/public/Personaje", "POST");

        JsonContainer.instance.Pcharacter = new JsonCharacter();

        JsonContainer.instance.Pcharacter.IdPersonaje = "0";

        if(JsonContainer.instance.Pid.IdUsuaio != "")
            JsonContainer.instance.Pcharacter.IdUsuaio = JsonContainer.instance.Pid.IdUsuaio;

        JsonContainer.instance.Pcharacter.Genero = (sexElection.sexo == 0) ? "1" : "0";

        JsonContainer.instance.Pcharacter.Cabello = (sexElection.sexo == 1) ? selectionFemeleC.NumeroPeloM.ToString() : selectionCharacter.NumeroPelo.ToString();

        JsonContainer.instance.Pcharacter.Cara = (sexElection.sexo == 1) ? selectionFemeleC.NumeroCaraM.ToString() : selectionCharacter.NumeroCara.ToString();

        JsonContainer.instance.Pcharacter.Accesorios = (sexElection.sexo == 1) ? selectionFemeleC.NumeroAccesorioM.ToString() : selectionCharacter.NumeroAccesorio.ToString();

        JsonContainer.instance.Pcharacter.Camisa = (sexElection.sexo == 1) ? selectionFemeleC.NumeroCamisaM.ToString() : selectionCharacter.NumeroCamisa.ToString();

        JsonContainer.instance.Pcharacter.Pantalon = (sexElection.sexo == 1) ? selectionFemeleC.NumeroPantalonM.ToString() : selectionCharacter.NumeroPantalon.ToString();

        JsonContainer.instance.Pcharacter.Zapatos = (sexElection.sexo == 1) ? selectionFemeleC.NumeroZapatoM.ToString() : selectionCharacter.NumeroZapato.ToString();

        Texture2D texture = new Texture2D(256, 256, TextureFormat.RGB24, false);

        RenderTexture.active = photo.PrenderTexture;

        texture.ReadPixels(new Rect(0, 0, photo.PrenderTexture.width, photo.PrenderTexture.height), 0, 0);

        texture.Apply();

        JsonContainer.instance.Pcharacter.FotoPerfil = Convert.ToBase64String(texture.EncodeToPNG());

        if (JsonContainer.instance.Pid.IdUsuaio != "")
            JsonContainer.instance.Pcharacter.Semillas = "0";

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

            output?.Invoke();
        }
    }
}