using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class picture : MonoBehaviour
{
   /* [SerializeField] GameObject[] screenPlace;   //lugares donde van los pantallazos
    //parte tomar pantallazo
    [SerializeField] GameObject camara_;
    Camera myCamera;
    List<GameObject> children = new List<GameObject>();
    short actualChildren;
    //private bool takescreenShot;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
    }
    private void Start()
    {
        myCamera = camara_.GetComponent<Camera>();
    }


    public void Pantallazo()
    {
        //PArte tomar pantallazo

        RenderTexture renderText = myCamera.targetTexture;
        Debug.Log("-1");
        Texture2D renderResult = new Texture2D(renderText.width, renderText.height, TextureFormat.RGB24, false);
        Debug.Log("0");
        Rect rect = new Rect(0, 0, renderText.width, renderText.height);
        Debug.Log("1");
        renderResult.ReadPixels(rect, 0, 0);
        byte[] byteArray = renderResult.EncodeToPNG();

        screenPlace[actualChildren].GetComponent<Image>().sprite = Sprite.Create(renderResult, new Rect(0f, 0f, renderResult.width, renderResult.height), new Vector2(0.5f, 0.5f), 100f);
        //Fin parte tomar pantallazo
        actualChildren++;
    }
   */


}
