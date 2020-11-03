using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PostToURL : MonoBehaviour
{
    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            StartCoroutine(Upload());
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
}
