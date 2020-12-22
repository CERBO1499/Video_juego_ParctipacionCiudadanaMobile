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
    [SerializeField]
    BoxCollider2D bxCollider;

    [SerializeField]
    Vector3 []positionImgSelectd;

    #endregion

    #region
    [SerializeField]
    RuletaManager rltManager;
    #endregion

    private void Awake()
    {
         bxCollider.GetComponent<Collider2D>();
        rltManager.GetComponent<RuletaManager>();

        for (int i = 0; i < Images.Length; i++)
        {
            positionImgSelectd[i] = Images[i].transform.localPosition;
        }
    }
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
            //CheckImgSelected(idpieza);
            StartCoroutine(waitToClose(collision.gameObject));
            
        }  
    }

    void CheckImgSelected(IdentificadorPieza idPieza)
    {

        Ruleta.gameObject.SetActive(false);

        switch (idPieza.TipoDeImagen)
        {
            case ETipoDeImagen.AvanzaTres:
                rltManager.Move(3);
                break;
            case ETipoDeImagen.AvanzaDos:
                rltManager.Move(2);
                break;
            case ETipoDeImagen.RetrocedeUna:
                rltManager.Move(-1);
                break;
            case ETipoDeImagen.VuelveInicio:
                rltManager.Restart();
                break;
            case ETipoDeImagen.Emocion:
                rltManager.OpenEmocionPanel();
                break;               
                
        }   


    }    

    IEnumerator waitToClose(GameObject objToSetPosition)
    {
        yield return new WaitForSeconds(1f);
        CheckImgSelected(idpieza);
        ResetRoulette(objToSetPosition);
    
    }

    void ResetRoulette(GameObject selectdPiece)
    {
        bxCollider.enabled = false;        
       
        for (int i = 0; i < Images.Length; i++)
        {
            Images[i].transform.localPosition = positionImgSelectd[i];

            Images[i].GetComponent<IdentificadorPieza>().Selected = false;
            Images[i].gameObject.SetActive(true);           
        }             
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
