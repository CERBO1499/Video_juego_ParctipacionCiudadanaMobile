using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Cameraman : MonoBehaviour
{
    #region Singleton
    public static Cameraman instance;
    #endregion
    #pragma warning disable CS0414
    #region Components
    [Header("Components")]
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
        UnityWebRequest request = new UnityWebRequest("", "POST");

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
}