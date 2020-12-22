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

    #region Components
    [SerializeField]
    RuletaManager rltManager;
    RectTransform rectImageSelectd;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Images")
        {
           
           idpieza = collision.GetComponent<IdentificadorPieza>();
           idpieza.Selected = true;
            rectImageSelectd = collision.GetComponent<RectTransform>();
            for (int i = 0; i < Images.Length; i++)
            {
                if (Images[i].GetComponent<IdentificadorPieza>().Selected==false)
                {
                    Images[i].gameObject.SetActive(false);
                }
                
            }

           

            //collision.transform.position = new Vector3(collision.transform.position.x, 1.1f);
            StartCoroutine(snapImageCoroutine(rectImageSelectd, rectImageSelectd.position, new Vector2(rectImageSelectd.position.x, 1.1f)));
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
    IEnumerator snapImageCoroutine(RectTransform imgSeleced,Vector2 iniPos,Vector2 finiPos)
    {         
        float t = Time.time;

        while (Time.time<=t+0.5f)
        {
            imgSeleced.position= iniPos + ((finiPos - iniPos) * curve.Evaluate((Time.time - t) / 0.5f));           

            yield return null;
        }
        
        imgSeleced.position = finiPos;
    }
}
