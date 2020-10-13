using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class UIImagesGraffiteando : MonoBehaviour
{/*
    [SerializeField] GameObject panelLevelSelector;
    [SerializeField] GameObject containerImages;

    [SerializeField] GameObject imageFramePrefab;
    [SerializeField] GameObject containerPrefab;
    [SerializeField] RawImage imageToPut;
    ScreenShot scShot;

    public void OnEnable()
    {
        ClearPrefabs();
        InstantiateImagePrefab();
    }


    private void ClearPrefabs()
    {
        Destroy(containerImages);
        GameObject newContainer = Instantiate(containerPrefab, panelLevelSelector.transform);
        containerImages = newContainer;
    }

    public void InstantiateImagePrefab()
    {
        
        GameObject imageFrame = Instantiate(imageFramePrefab, containerImages.transform);


        Texture2D myTexture = LoadPNG(Application.persistentDataPath + "/pic" + scShot.CountScrenShot + ".png");
        //imageToPut.texture = myTexture;
        Sprite sprite = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

        imageFrame.GetComponent<Image>().sprite = sprite;

        imageFrame.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = imageFrame.ToString();


    }
    public static Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }*/

}
