using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class boton : MonoBehaviour
{
    #region Information
    float time = 0;
    [SerializeField] Transform objetive; //el transform del Pin para que la brujula lo mire.
    #endregion

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
        StartCoroutine(CloseAppCoroutine());
    }

    IEnumerator CloseAppCoroutine()
    {
        if (JsonContainer.instance.Pid.IdUsuaio != "" || JsonContainer.instance.Pcharacter.IdUsuaio != "")
        {
            UnityWebRequest request = new UnityWebRequest("https://www.polygon.us/apiEscuelaspp/public/StillAlive", "POST");

            byte[] body = Encoding.UTF8.GetBytes("{\"IdUsuario\":\"" + ((JsonContainer.instance.Pid.IdUsuaio != "") ? JsonContainer.instance.Pid.IdUsuaio.ToString() : JsonContainer.instance.Pcharacter.IdUsuaio.ToString()) + "\",\"Tiempo\":\"" + (Time.time - time).ToString() + "\"}");

            time = Time.time;

            request.uploadHandler = new UploadHandlerRaw(body);

            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError)
                Debug.Log(request.error);
            else
                Debug.Log("Application Quit: " + request.responseCode);
        }

        if (Application.platform != RuntimePlatform.WebGLPlayer)
            Application.Quit();
        else
        {
            PlayerPrefs.SetString("User Name", "");
            PlayerPrefs.SetString("Password", "");

            SceneManager.LoadScene("menu");
        }
    }
}