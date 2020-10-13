using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class capturaPantalla : MonoBehaviour
{
    // Start is called before the first frame update
    int count;
    List <Image> screen = new List<Image>();
    private void Start()
    {
        count = 0;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {

            //ScreenCapture.CaptureScreenshot("C:\\Users\\user\\Pictures\\Screenshot" + count + ".png");
            //screen.Add();
            //ScreenCapture.CaptureScreenshot("SomeLevel");
            OnFoto();
            Debug.Log("imprime entra");
        }
    
    }


    public void OnFoto()
    {
        Debug.Log(count);
            System.Diagnostics.Process.Start("mspaint.exe", "Screenshot" + count + ".png");
        count++;
    }
}
