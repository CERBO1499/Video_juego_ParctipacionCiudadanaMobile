using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class JsonContainer : MonoBehaviour
{
    #region Static
    public static JsonContainer instance;
    #endregion

    #region Information
    bool quit;
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] JsonCharacter character;
    public JsonCharacter Pcharacter
    {
        get { return character; } set { character = value; }
    }
    [SerializeField] JsonId id;
    public JsonId Pid
    {
        get { return id; }
        set { id = value; }
    }
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        Application.wantsToQuit += () =>
        {
            if (!quit)
            {
                quit = true;

                StartCoroutine(CloseAppCoroutine());

                return false;
            }

            return true;
        };
    }

    IEnumerator CloseAppCoroutine()
    {
        if (id.IdUsuaio != "" || character.IdUsuaio != "")
        {
            UnityWebRequest request = new UnityWebRequest("https://www.polygon.us/escuelaspp/public/StillAlive", "POST");

            byte[] body = Encoding.UTF8.GetBytes("{\"IdUsuario\":\"" + ((id.IdUsuaio != "") ? id.IdUsuaio.ToString() : character.IdUsuaio.ToString()) + "\",\"Tiempo\":\"" + Time.time.ToString() + "\"}");

            request.uploadHandler = new UploadHandlerRaw(body);

            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError)
                Debug.Log(request.error);
            else
                Debug.Log("Application Quit: " + request.responseCode);

            Application.Quit();
        }

        Application.Quit();
    }
}