using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class boton : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string setTime(string time);

    [SerializeField] Transform objetive; //el transform del Pin para que la brujula lo mire.

    public void PressScript()
    {        
        SceneManager.LoadScene("main");
    }
    public void ChooseScene()
    {
        SceneManager.LoadScene("choseScene");
    }

    public void BrujulaNuevoObjetivo()
    {
        GameObject bruj = GameObject.Find("brujula");
        bruj.GetComponent<brujula>().NewObjetivo(objetive);   //va a darle la nueva ubicacion a la cual mirar (la brujula)
        bruj.GetComponentInChildren<ParticleSystem>().Play();
    }
    public void CloseApp()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            Quit();
        else
            StartCoroutine(CloseAppCoroutine());
    }

    IEnumerator CloseAppCoroutine()
    {
        if (JsonContainer.instance.Pid.IdUsuaio != "" || JsonContainer.instance.Pcharacter.IdUsuaio != "")
        {
            UnityWebRequest request = new UnityWebRequest("https://www.polygon.us/escuelaspp/public/StillAlive", "POST");

            byte[] body = Encoding.UTF8.GetBytes("{\"IdUsuario\":\"" + ((JsonContainer.instance.Pid.IdUsuaio != "") ? JsonContainer.instance.Pid.IdUsuaio.ToString() : JsonContainer.instance.Pcharacter.IdUsuaio.ToString()) + "\",\"Tiempo\":\"" + Time.time.ToString() + "\"}");

            request.uploadHandler = new UploadHandlerRaw(body);

            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError)
                Debug.Log(request.error);
            else
                Debug.Log("Application Quit: " + request.responseCode);
        }

        Application.Quit();
    }

    public void Quit()
    {
        setTime("{\"IdUsuario\":\"" + ((JsonContainer.instance.Pid.IdUsuaio != "") ? JsonContainer.instance.Pid.IdUsuaio.ToString() : JsonContainer.instance.Pcharacter.IdUsuaio.ToString()) + "\",\"Tiempo\":\"" + Time.time.ToString() + "\"}");

        PlayerPrefs.SetString("User Name", "");
        PlayerPrefs.SetString("Password", "");

        SceneManager.LoadScene("menu");
    }
}