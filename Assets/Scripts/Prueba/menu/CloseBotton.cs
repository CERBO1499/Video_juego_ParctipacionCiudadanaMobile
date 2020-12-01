using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CloseBotton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject objetoAActivar;     
    
    public void Presionado()
    {
        gameObject.SetActive(false);//oculta el objeto con este script
    }
    public void activar()
    {
        objetoAActivar.SetActive(true);//activa el objeto serializadoa
        //actu
    }

    public void LogOut()
    {
        StartCoroutine(CloseAppCoroutine());
    }

    IEnumerator CloseAppCoroutine()
    {
        if (JsonContainer.instance.Pid.IdUsuaio != "" || JsonContainer.instance.Pcharacter.IdUsuaio != "")
        {
            UnityWebRequest request = new UnityWebRequest("https://www.polygon.us/apiEscuelaspp/public/StillAlive", "POST");

            byte[] body = Encoding.UTF8.GetBytes("{\"IdUsuario\":\"" + ((JsonContainer.instance.Pid.IdUsuaio != "") ? JsonContainer.instance.Pid.IdUsuaio.ToString() : JsonContainer.instance.Pcharacter.IdUsuaio.ToString()) + "\",\"Tiempo\":\"" + (Time.time - boton.time).ToString() + "\"}");

           boton.time = Time.time;

            request.uploadHandler = new UploadHandlerRaw(body);

            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError)
                Debug.Log(request.error);
            else
                Debug.Log("Application Quit: " + request.responseCode);
        }

        PlayerPrefs.SetString("User Name", "");
        PlayerPrefs.SetString("Password", "");

        SceneManager.LoadScene("menu");
    }

    public void ClosePanel()
    {
        objetoAActivar.SetActive(false);
    }
}