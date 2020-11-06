using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;


public class PostToURL : MonoBehaviour
{

    float Counter;
    private static PostToURL _instance;

    #region Components
    [Header("Components")]
    [SerializeField] DeprecatedBanner deprecatedBanner;
    #endregion

    void Awake()
    {
        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
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
        if (Application.platform == RuntimePlatform.Android)
        {
            StartCoroutine(Upload());
        }

        StartCoroutine(AskForTheVersion());
    }

    IEnumerator AskForTheVersion()
    {
        UnityWebRequest request = new UnityWebRequest("https://www.polygon.us/escuelaspp/version.php", "GET");

        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.isNetworkError)
            Debug.Log(request.error);
        else
        {
            Debug.Log("Ask for version:" + request.responseCode);

            if (request.downloadHandler.text != Application.version)
            {
                deprecatedBanner.Active();

                try
                {
                    Application.OpenURL("https://www.polygon.us/escuelaspp/Device.php");
                }
                catch(Exception error)
                {
                    Debug.Log(error.Message);
                }
            }
        }
    }

    IEnumerator Upload()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        UnityWebRequest www = UnityWebRequest.Post("https://www.polygon.us/escuelaspp/celulares.php", "{\"User\": \"celulares\", \"Password\": \"000\", \"TipoDispositivo\": 2}");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
      
        }
    }


    private void Update()
    {
        Counter += Time.deltaTime;
        if (Counter >= 300)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                StartCoroutine(UploadTimeOnApplicationAndroid());
                Counter = 0f;
            }
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                StartCoroutine(UploadTimeOnApplicationWEBGL());
                Counter = 0f;
            }
           
           
        }
    }
    IEnumerator UploadTimeOnApplicationAndroid()
    {

      
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        UnityWebRequest www = UnityWebRequest.Post("https://polygon.us/escuelaspp/celulares.php", "{\"User\": \"celulares\", \"Password\": \"000\", \"TipoDispositivo\": 2}");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }

    }
    IEnumerator UploadTimeOnApplicationWEBGL()
    {


        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        UnityWebRequest www = UnityWebRequest.Post("https://polygon.us/escuelaspp/webgl.php", "{\"User\": \"\", \"Password\": \"\", \"TipoDispositivo\": 1}");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }

    }

}
