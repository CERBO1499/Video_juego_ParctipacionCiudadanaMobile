using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Cameraman : MonoBehaviour
{
    #region Singleton
    public static Cameraman instance;
    #endregion
    #pragma warning disable CS0414
    #region Information
    [Header("Information", order = 0)]
    [SerializeField] string url;
    #endregion
    [Space(order = 1)]
    #region Components
    [Header("Components", order = 2)]
    [SerializeField] Scenemanager sceneManager;
    [SerializeField] Photo photo;
    #region
    [SerializeField] bool showPhoto;
    #endregion
    #endregion

    void OnValidate()
    {
        if (photo != null)
            photo.PrawImage.SetActive(showPhoto);
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        if (sceneManager != null)
            sceneManager.onToMainGame = () =>
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                if (player == null)
                    Debug.LogWarning("No se encontro el jugdor, revisa si este tiene el tag de player");
                else
                {
                    Animator animator = player.GetComponent<Animator>();

                    if (animator != null)
                    {
                        animator.Play("Happy Idle", 0, 0.0f);

                        animator.speed = 0;

                        SendPhoto(() =>
                        {
                            SceneManager.LoadScene("main", LoadSceneMode.Single);
                        });
                    }
                }
            };

        if(photo != null)
            photo.PrawImage.SetActive(showPhoto);
    }

    public Texture TakePhoto()
    {
        return photo.Ptexture;
    }

    public void SendPhoto(Action output = null)
    {
        StartCoroutine(SendPhotoCoroutine(output));
    }

    IEnumerator SendPhotoCoroutine(Action output = null)
    {
        if (url != "")
        {
            UnityWebRequest request = new UnityWebRequest(url, "POST");

            byte[] body = Encoding.UTF8.GetBytes("{\"userImage\":\"" + Convert.ToBase64String(((Texture2D)(photo.Ptexture)).EncodeToPNG()) + "\"}");

            request.uploadHandler = new UploadHandlerRaw(body);

            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError)
                Debug.Log(request.error);
            else
            {
                Debug.Log(request.responseCode);

                output?.Invoke();
            }
        }
        else
            output?.Invoke();
    }
}