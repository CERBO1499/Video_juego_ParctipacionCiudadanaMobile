using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShootHandler : MonoBehaviour
{
  /*  private static ScreenShootHandler instance;
    private Camera myCamera;
    private bool takescreenShot;

    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }
    private void OnPostRender()
    {
        if (takescreenShot)
        {
            takescreenShot = false;
            RenderTexture renderText = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderText.width, renderText.height,TextureFormat.RGB24,false);
            Rect rect = new Rect(0, 0, renderText.width, renderText.height);
            renderResult.ReadPixels(rect, 0, 0);
            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath+"/CamerasScreenShot.png", byteArray);
            Debug.Log("Saved camera screen");

            RenderTexture.ReleaseTemporary(renderText);
            myCamera.targetTexture = null;
        }
         
    }
    private void takescreenShoot(int width, int heigth)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, heigth, 16);
        takescreenShot = true;
    }

    public static void TakeScreenShot_Satic(int width, int heigth)
    {
        instance.takescreenShoot(width, heigth);
    }*/
}
