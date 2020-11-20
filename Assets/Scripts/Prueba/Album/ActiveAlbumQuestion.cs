using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAlbumQuestion : MonoBehaviour
{
    #region Information
    [SerializeField]
    GameObject
    QuestionToActivate;
    #endregion


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            QuestionToActivate.SetActive(true);
        }
    }

    public void CloseObject(GameObject objectToUnactive)
    {
        objectToUnactive.SetActive(false);
    }    

    public void OpenPanel(GameObject objectToOpen)
    {
        objectToOpen.SetActive(true);
    }
}
