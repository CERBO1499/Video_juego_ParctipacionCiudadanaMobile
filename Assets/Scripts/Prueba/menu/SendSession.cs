using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SendSession : MonoBehaviour
{
    public static IEnumerator SendSessionCoroutine(string userID)
    {
        UnityWebRequest request = new UnityWebRequest("https://www.polygon.us/apiEscuelaspp/public/Entrar", "POST");

        byte[] body = Encoding.UTF8.GetBytes("{\"Idusuario\":\"" + userID + "\",\"device\":\"" + ((Application.platform != RuntimePlatform.Android) ? "1" : "0") + "\"}");

        request.uploadHandler = new UploadHandlerRaw(body);

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
    }
}