using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour
{
    #pragma warning disable CS0414
    #region Information
    [Header("Information", order = 0)]
    [SerializeField] RenderTexture texture;
    public RenderTexture PrenderTexture
    {
        get { return texture; }
    }
    #endregion
    [Space(order = 1)]
    #region Components
    [Header("Components", order = 2)]
    [SerializeField] RawImage rawImage;
    public GameObject PrawImage
    {
        get { return rawImage.gameObject; }
    }
    #endregion

    void Start()
    {
        if (rawImage != null)
        {
            if(texture != null)
                rawImage.texture = texture;
        }
    }
}