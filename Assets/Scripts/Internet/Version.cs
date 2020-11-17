using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Version : MonoBehaviour
{
    #region Components
    [Header("Components")]
    [SerializeField] Button playBtn;
    [SerializeField] DeprecatedBanner deprecatedBanner;
    #endregion

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
                playBtn.interactable = false;

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
