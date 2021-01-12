using System.Collections;
using UnityEngine.UI;
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
    Button btnActiveRuleta;
    [SerializeField]
    Collider2D colliderDetectorImg;
    [SerializeField]
    AnimationCurve downVelocity;

    [SerializeField]
    Rigidbody2D rbColumna;

    int randomValue;
    float timeInterval;

    bool rowStopped;
    string stoppedSlot;
  
    #endregion
    #endregion

    private void Start()
    {
        rowStopped = true;
        rbColumna = columnaAGirar.GetComponent<Rigidbody2D>();
    }
    public void GirarRuleta()
    {
        btnActiveRuleta.interactable = false;
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

        rbColumna.velocity = new Vector2(0f, -25f);

        float initialTime = Random.Range(3f, 5f);

        float t = Time.time;

        while (Time.time <= t + initialTime)
        {
            if (columnaAGirar.transform.localPosition.y <= -7566.5f)
                columnaAGirar.transform.localPosition = new Vector2(columnaAGirar.transform.localPosition.x, 7464f);

            yield return null;
        }

        t = Time.time;

        while (Time.time <= t + 3f)
        {
            rbColumna.velocity = new Vector2(0f, -25f * downVelocity.Evaluate((Time.time - t) / 3f));

            yield return null;

            if (columnaAGirar.transform.localPosition.y <= -7566.5f)
                columnaAGirar.transform.localPosition = new Vector2(columnaAGirar.transform.localPosition.x, 7464f);
        }

        rbColumna.velocity = Vector3.zero;

        rowStopped = true;

        ouput();
    }
   
    IEnumerator snapLastImg()
    {

        if (rowStopped)
        {
            colliderDetectorImg.enabled = true;
            yield return new WaitForSeconds(1f);
            colliderDetectorImg.enabled = false;
            btnActiveRuleta.interactable = true;
        }      
    
    }

}
