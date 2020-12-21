using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarRuleta : MonoBehaviour
{
    #region Information
    #region PalancaRuleta
    [Header("Informacion ruleta")]
    [SerializeField]
    RectTransform Palanca,bolaPalanca;
    [SerializeField]
    AnimationCurve curve;
    #endregion
    #region ImagenesRuleta
    [Header("Informacion Columna ruleta")]

    [SerializeField]
    RectTransform columnaAGirar;
    [SerializeField]
    Collider2D colliderDetectorImg;

    int randomValue;
    float timeInterval;

    bool rowStopped;
    string stoppedSlot;
    #endregion
    #endregion

    private void Start()
    {
        rowStopped = true;
    }
    public void GirarRuleta()
    {
        print("Entro a btn");
        stoppedSlot = "";
        StartCoroutine(GirarPalancaRuletaCoroutine());
    }

    IEnumerator GirarPalancaRuletaCoroutine()
    {
        Vector2 iniRotation = Vector2.zero;
        Vector2 finalRotation = new Vector2(-180f, 0f);    


        float t = Time.time;
        while (Time.time <= 1.5f + t)
        {
            Palanca.localEulerAngles = iniRotation + ((finalRotation - iniRotation) * curve.Evaluate((Time.time - t) / 1.5f));
            yield return null;
        }
        Palanca.localEulerAngles = finalRotation;

        StartCoroutine(GirarRuletaImgCoroutine(()=> {
            StartCoroutine(snapLastImg());
        }));

        Vector2 iniRotation2 = new Vector2(-180f, 0f);
        Vector2 finalRotation2 = Vector2.zero;       

        t = Time.time;
        while (Time.time <= 1.5f + t)
        {
            Palanca.localEulerAngles = iniRotation2 + ((finalRotation2 - iniRotation2) * curve.Evaluate((Time.time - t) / 1.5f));
            yield return null;
        }
        Palanca.localEulerAngles = finalRotation2;
    }

    IEnumerator GirarRuletaImgCoroutine(System.Action ouput)
    {
        rowStopped = false;
        timeInterval = 0.025f;

        for (int i = 0; i < 30; i++)
        {
            if (columnaAGirar.transform.localPosition.y <= -5985f)
                columnaAGirar.transform.localPosition = new Vector2(columnaAGirar.transform.localPosition.x, 5707f);

            columnaAGirar.transform.localPosition = new Vector2(columnaAGirar.transform.localPosition.x, columnaAGirar.transform.localPosition.y -144.25f);
            yield return new WaitForSeconds(timeInterval);
        }
        randomValue = Random.Range(60, 100);

        switch (randomValue % 3)
        {
            case 1:
                randomValue += 2;
                break;
            case 2:
                randomValue += 1;
                break;           
        }
        for (int i = 0; i < randomValue; i++)
        {
            if (columnaAGirar.transform.localPosition.y <= -5985f)
                columnaAGirar.transform.localPosition = new Vector2(columnaAGirar.transform.localPosition.x, 5707f);

            columnaAGirar.transform.localPosition = new Vector2(columnaAGirar.transform.localPosition.x, columnaAGirar.transform.localPosition.y - 144.25f);

            //Controlar el snap

            if (i > Mathf.RoundToInt(randomValue * 0.25f))
                timeInterval = 0.05f;
            if (i > Mathf.RoundToInt(randomValue * 0.5f))
                timeInterval = 0.1f;
            if (i > Mathf.RoundToInt(randomValue * 0.75f))
                timeInterval = 0.15f;
            if (i > Mathf.RoundToInt(randomValue * 0.95f))
                timeInterval = 0.2f;

            yield return new WaitForSeconds(timeInterval);
            rowStopped = true;
            ouput();        
        }
    }
    IEnumerator snapLastImg()
    {
        yield return new WaitForSeconds(8f);
        if (rowStopped)
        {
            colliderDetectorImg.enabled = true;
            yield return new WaitForSeconds(1f);
            colliderDetectorImg.enabled = false;
        }      
    
    }

}
