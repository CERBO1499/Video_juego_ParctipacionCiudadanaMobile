using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PostToURL : MonoBehaviour
{

    float Counter = 300;
    public static PostToURL _instance;

    #region Components
    [Header("Components")]
    [SerializeField] DeprecatedBanner deprecatedBanner;
    #endregion

    void Awake()
    {
        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(gameObject);
            Guid.NewGuid();
            Guid myGuid = Guid.NewGuid();
            print("Esta es mi guid" + myGuid);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        StartCoroutine(AskForTheVersion());
    }

    IEnumerator AskForTheVersion()
    {
        UnityWebRequest request = new UnityWebRequest("https://www.polygon.us/escuelaspp/version.php", "GET");

        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Ask for version: " + request.responseCode);

            if (request.downloadHandler.text != Application.version)
            {
                deprecatedBanner.Active();

                try
                {
                    Application.OpenURL("https://www.polygon.us/escuelaspp/Device.php");
                }
                catch (Exception error)
                {
                    Debug.Log(error.Message);
                }
            }
        }
    }
}
