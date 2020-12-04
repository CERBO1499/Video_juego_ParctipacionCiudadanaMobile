using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SendSession : MonoBehaviour
{
    public static IEnumerator SendSessionCoroutine(string userID)
    {
        #if False
        UnityWebRequest request = new UnityWebRequest("", "POST");

        byte[] body = Encoding.UTF8.GetBytes("{\"IdUsuario\":\"" + userID + "\",\"device\":\"" + ((Application.platform != RuntimePlatform.Android) ? "1" : "0") + "\"}");

        request.uploadHandler = new UploadHandlerRaw(body);

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError)
            Debug.Log(request.error);
        else
            Debug.Log("Send Session: " + request.responseCode);
        #else
        yield return null;
        #endif
    }
}