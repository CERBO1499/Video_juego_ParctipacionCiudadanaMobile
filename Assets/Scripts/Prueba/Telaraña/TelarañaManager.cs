using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TelarañaManager : MonoBehaviour
{
    #region Static
    public static TelarañaManager instance;
    #endregion

    #region Information
    bool render;
    int circlesIndex = 0;
    [HideInInspector] public int images = 0;
    [HideInInspector] public bool drag;
    bool pass;
    public bool line;
    [Header("Information")]
    [SerializeField] GameObject circle;
    [SerializeField] GameObject[] circles;
    [SerializeField] AnimationCurve curve;
    #region Questions
    [SerializeField] Sprite[] questions;
    #endregion
    #region Jaika
    [SerializeField] GameObject secondActivity;
    [SerializeField] Transform lines;
    public Material lineMaterial;
    public Transform Plines
    {
        get { return lines; }
    }
    [SerializeField] GameObject sendBtn;
    [SerializeField] GameObject end;
    #endregion
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] GameObject options;
    [SerializeField] RectTransform spiderWeb;
    [SerializeField] RectTransform optionsRect;
    [SerializeField] GameObject title;
    [SerializeField] RenderTexture renderTexture;
    #endregion

    void Awake()
    {
        instance = this;

        if (JsonContainer.instance != null)
        {
            byte[] imageBytes = Convert.FromBase64String(JsonContainer.instance.Pcharacter.FotoPerfil);

            Texture2D texture = new Texture2D(512, 512);

            texture.LoadImage(imageBytes);

            circle.GetComponent<RectTransform>().GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }

    void Start()
    {
        StartCoroutine(ShowCircle());
    }

    IEnumerator ShowCircle()
    {
        StartCoroutine(ShowCircle(circle));

        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < circles.Length; i++)
        {
            StartCoroutine(ShowCircle(circles[i]));

            yield return new WaitForSeconds(0.25f);
        }
    }

    IEnumerator ShowCircle(GameObject circle)
    {
        circle.SetActive(true);

        Vector3 initialLocalScale = Vector3.zero;

        Vector3 finalLocalScale = Vector3.one;

        float t = Time.time;

        while (Time.time <= t + 0.5f)
        {
            circle.transform.localScale = initialLocalScale + ((finalLocalScale - initialLocalScale) * curve.Evaluate((Time.time - t) / 0.5f));

            yield return null;
        }

        circle.transform.localScale = finalLocalScale;

        if (circles[circles.Length - 1] == circle)
        {
            for (int i = 0; i < circles.Length; i++)
            {
                foreach (Transform element in circles[i].transform)
                    element.gameObject.SetActive(true);
            }

            options.SetActive(true);

            yield return new WaitForSeconds(1f);

            StartCoroutine(GetCircle(circles[circlesIndex]));
        }
    }

    IEnumerator GetCircle(GameObject circle)
    {
        bool founded = false;

        for (int i = 0; i < circles.Length; i++)
        {
            if (founded)
                circles[i].SetActive(false);
            else
                circles[i].SetActive(true);

            if (circles[i] == circle)
                founded = true;
        }

        Vector2[] initialLocalPosition = new Vector2[circle.transform.childCount];

        Vector2[] initialLocalScale = new Vector2[circle.transform.childCount];

        for (int i = 0; i < circle.transform.childCount; i++)
        {
            initialLocalPosition[i] = circle.transform.GetChild(i).transform.localPosition;

            initialLocalScale[i] = circle.transform.GetChild(i).transform.localScale;
        }

        Vector2[] finalLocalPosition = { new Vector2(-410f, -290f), new Vector2(-120f, 170f), new Vector2(455f, 170f), new Vector2(160f, -290f) };

        Vector2[] finalLocalScale = { Vector2.one * 4f, Vector2.one * 4f, Vector2.one * 4f, Vector2.one * 4f };

        float t = Time.time;

        while (Time.time <= t + 0.5f)
        {
            for (int i = 0; i < circle.transform.childCount; i++)
            {
                circle.transform.GetChild(i).localPosition = initialLocalPosition[i] + ((finalLocalPosition[i] - initialLocalPosition[i]) * curve.Evaluate((Time.time - t) / 0.5f));

                circle.transform.GetChild(i).localScale = initialLocalScale[i] + ((finalLocalScale[i] - initialLocalScale[i]) * curve.Evaluate((Time.time - t) / 0.5f));
            }

            yield return null;
        }

        for (int i = 0; i < circle.transform.childCount; i++)
        {
            circle.transform.GetChild(i).gameObject.GetComponent<Image>().raycastTarget = true;

            circle.transform.GetChild(i).localPosition = finalLocalPosition[i];

            circle.transform.GetChild(i).localScale = finalLocalScale[i];

            circle.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);

            circle.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = questions[circlesIndex];
        }

        drag = true;

        while (!pass)
            yield return null;

        pass = false;

        drag = false;

        for (int i = 0; i < circle.transform.childCount; i++)
            circle.transform.GetChild(i).gameObject.GetComponent<Image>().raycastTarget = false;

        t = Time.time;

        while (Time.time <= t + 0.5f)
        {
            for (int i = 0; i < circle.transform.childCount; i++)
            {
                circle.transform.GetChild(i).localPosition = finalLocalPosition[i] + ((initialLocalPosition[i] - finalLocalPosition[i]) * curve.Evaluate((Time.time - t) / 0.5f));

                circle.transform.GetChild(i).localScale = finalLocalScale[i] + ((initialLocalScale[i] - finalLocalScale[i]) * curve.Evaluate((Time.time - t) / 0.5f));
            }

            yield return null;
        }

        for (int i = 0; i < circle.transform.childCount; i++)
        {
            circle.transform.GetChild(i).localPosition = initialLocalPosition[i];

            circle.transform.GetChild(i).localScale = initialLocalScale[i];
        }

        circlesIndex++;

        if (circlesIndex < circles.Length)
            StartCoroutine(GetCircle(circles[circlesIndex]));
        else
        {
            yield return new WaitForSeconds(1f);

            if (Application.platform != RuntimePlatform.Android)
            {
                RectTransform rect = secondActivity.GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<RectTransform>();

                rect.offsetMax = Vector2.zero;

                rect.offsetMin = Vector2.zero;
            }

            secondActivity.SetActive(true);
        };
    }

    public void SetCircle()
    {
        pass = true;
    }

    public void PassToSecondActivity()
    {
        circle.GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<Image>().raycastTarget = true;

        for (int i = 0; i < circles.Length; i++)
        {
            foreach (RectTransform rect in circles[i].GetComponent<RectTransform>())
            {
                rect.gameObject.GetComponent<Image>().raycastTarget = true;
            }
        }

        secondActivity.SetActive(false);

        sendBtn.SetActive(true);

        line = true;
    }

    public void Send()
    {
        circle.GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<Image>().raycastTarget = false;

        for (int i = 0; i < circles.Length; i++)
        {
            foreach (RectTransform rect in circles[i].GetComponent<RectTransform>())
            {
                rect.gameObject.GetComponent<Image>().raycastTarget = false;
            }
        }

        Vector3 spiderWebInitialPosition = new Vector3(-1280f, 0f, 0f);

        Vector3 optionsInitialPosition = new Vector3(640f, 0f, 0f);

        spiderWeb.anchoredPosition = new Vector3(-1920f, 0f, 0f);

        optionsRect.anchoredPosition = Vector3.zero;

        for (int i = 0; i < lines.childCount; i++)
            lines.GetChild(i).gameObject.GetComponent<WebLine>().UpdatePosition();

        title.SetActive(false);

        Camera.main.targetTexture = renderTexture;

        Camera.main.Render();

        Camera.main.targetTexture = null;

        spiderWeb.anchoredPosition = spiderWebInitialPosition;

        optionsRect.anchoredPosition = optionsInitialPosition;

        for(int i = 0; i < lines.childCount; i++)
            lines.GetChild(i).gameObject.GetComponent<WebLine>().UpdatePosition();

        title.SetActive(true);

        StartCoroutine(SendCoroutine(toTexture2D(renderTexture)));
    }

    IEnumerator SendCoroutine(Texture2D texture)
    {
        UnityWebRequest request = new UnityWebRequest("https://www.polygon.us/escuelaspp/tejiendolazos/crearimagen.php", "POST");

        byte[] body = Encoding.UTF8.GetBytes("{\"IdUsuario\":\"" + JsonContainer.instance.Pcharacter.IdUsuaio + "\",\"Spiderweb\":\"" + Convert.ToBase64String(texture.EncodeToPNG()) + "\"}");

        request.uploadHandler = new UploadHandlerRaw(body);

        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError)
            Debug.Log(request.error);
        else
        {
            Debug.Log("Spider Web: " + request.downloadHandler.text);

            if (Application.platform != RuntimePlatform.Android)
            {
                RectTransform rect = end.GetComponent<RectTransform>().GetChild(0).gameObject.GetComponent<RectTransform>();

                rect.offsetMax = Vector2.zero;

                rect.offsetMin = Vector2.zero;
            }

            end.SetActive(true);
        }
    }

    Texture2D toTexture2D(RenderTexture renderTexture)
    {
        Texture2D texture = new Texture2D(1024, 1024, TextureFormat.RGB24, false);

        RenderTexture.active = renderTexture;

        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);

        texture.Apply();

        return texture;
    }

    public void ToMain()
    {
        ControlSemilla.SumarSemilla(10, () => 
        {
            SceneManager.LoadScene("main", LoadSceneMode.Single);
        });
    }
}