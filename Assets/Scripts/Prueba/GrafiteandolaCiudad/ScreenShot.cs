using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShot : MonoBehaviour
{

    //public static ScreenShot _instance;
    //private void Awake()
    //{
    //    if (_instance == null)
    //     {
    //       _instance = this;
    //       DontDestroyOnLoad(this.gameObject);
    //     //Rest of your Awake code
    //    }
    //    else 
    //    {
    //       Destroy(this);
    //    }
    //}

   // private int countscreen=0;
    //private int countScrenShot = 0;

//    public int CountScrenShot { get => countScrenShot; set => countScrenShot = value; }

    /* public void  TakeScreenShot(string picname)
     {
         StartCoroutine(ScreenShotDRAW(picname));
     }*/
    /*  IEnumerator ScreenShotDRAW(string picname)
      {
          yield return new WaitForEndOfFrame();

          Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

          texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height),0,0);
          texture.Apply();

          yield return 0;
          byte[] bytes = texture.EncodeToPNG();
          File.WriteAllBytes(Application.persistentDataPath + "/" + picname + ".png", bytes);
          countscreen++;

          Destroy(texture);

      }


      public void buttonForScreenShot()
      {
          countScrenShot++;
          string picname = "/pic" + countScrenShot;
          StartCoroutine(ScreenShotDRAW(picname));
      }*/
}
