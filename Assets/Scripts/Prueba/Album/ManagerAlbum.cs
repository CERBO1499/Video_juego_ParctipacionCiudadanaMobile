using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerAlbum : MonoBehaviour
{
    #region Information
    [Header("Numero respuesta correta a incorrecta", order = 0)]
    [SerializeField]
    int CorrectAns;
    [SerializeField]
    int diferentAns;
    [SerializeField]
    AlbumPieceId idPieceAlbum;
    

    [Header("FeedBacks", order = 1)]
    [SerializeField]
    RectTransform Good;
    [SerializeField]
    RectTransform Bad;

    [Header("Botones", order = 2)]
    [SerializeField]
    GameObject[] Botones;

    bool responseFirstime=false;


    #endregion
    #region Components
    [SerializeField]
    Album album;
    #endregion

    void CheckCorrecAns()
    {
        album.Add(idPieceAlbum.IdPiece);
        PlayerPrefs.SetString("Album Piece" + idPieceAlbum.IdPiece, "true");
        idPieceAlbum.gameObject.SetActive(false);
    }
    public void UserResponse(int _response)
    {
        if(_response== CorrectAns)
        {

            CloseBotones();
            Good.gameObject.SetActive(true);            
        }
        else if(_response == diferentAns)
        {
            CloseBotones();
            Bad.gameObject.SetActive(true);
            StartCoroutine(ShowButtonsAgain());
        }

        CheckCorrecAns();
    }

    public void MultipleResponse(GameObject ButtonToUnactive)
    {
        ButtonToUnactive.GetComponent<Button>().interactable = false;
        responseFirstime = true;
    }

    public void ActivecloseMultipleResponses(GameObject buttonContinue)
    {
        if (responseFirstime) buttonContinue.SetActive(true);
    }
    public void ResponseFinalMultiple(GameObject objToclose)
    {
        CheckCorrecAns();
        objToclose.SetActive(false);
    }

    void CloseBotones()
    {
        for (int i = 0; i < Botones.Length; i++)
        {
            Botones[i].SetActive(false);          
        }
    }

    void ShowBotones()
    {
        for (int i = 0; i < Botones.Length; i++)
        {
            Botones[i].SetActive(true);
        }
    }

    IEnumerator ShowButtonsAgain()
    {
        yield return new WaitForSeconds(0.3f);
        Bad.gameObject.SetActive(false);
        ShowBotones();
    }


}
