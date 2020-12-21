using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithImage : MonoBehaviour
{
    #region Information
    [SerializeField]
    RectTransform[] Images;
    [SerializeField]
    RectTransform Ruleta;
    [SerializeField]
    AnimationCurve curve;
    IdentificadorPieza idpieza;
    #endregion
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.tag == "Images")
        {
           idpieza = collision.GetComponent<IdentificadorPieza>();
            idpieza.Selected = true;

            for (int i = 0; i < Images.Length; i++)
            {
                if (Images[i].GetComponent<IdentificadorPieza>().Selected==false)
                {
                    Images[i].gameObject.SetActive(false);
                }
                
            }
            collision.transform.position = new Vector3(collision.transform.position.x, 1.1f);
            CheckImgSelected(idpieza);
        }  
    }

    void CheckImgSelected(IdentificadorPieza idPieza)
    {
        switch (idPieza.TipoDeImagen)
        {
            case ETipoDeImagen.AvanzaTres:
                print("Avanza tres");
                break;
            case ETipoDeImagen.AvanzaDos:
                print("avanza dos");
                break;
            case ETipoDeImagen.RetrocedeUna:
                print("Retrocede una");
                break;
            case ETipoDeImagen.VuelveInicio:
                print("Veulve al inicio");
                break;
            case ETipoDeImagen.Emocion:
                print("Emocion");
                break;               
                
        }
        StartCoroutine(UnactiveRouleteCoroutine(Ruleta));
    }

    IEnumerator UnactiveRouleteCoroutine(RectTransform Ruleta)
    {
        Vector2 iniPos = new Vector2(0f,-5.7f);
        Vector2 finiPos = new Vector2(1322f,-487f);

        Vector2 iniSzie = new Vector2(1f,1f);
        Vector2 finiSize = new Vector2(0.5f,0.5f);

        float t = Time.time;
        while (Time.time<=t+2f)
        {
            Ruleta.localPosition = iniPos + ((finiPos - iniPos) * curve.Evaluate((Time.time - t) / 2f));
            Ruleta.localScale = iniSzie + ((finiSize - iniSzie) * curve.Evaluate((Time.time - t) / 2f));

            yield return null;
        }

        Ruleta.localScale = finiSize;
        Ruleta.localPosition = finiPos;
    }
}
