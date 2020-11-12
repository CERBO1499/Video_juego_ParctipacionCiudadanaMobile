using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveQuestion : MonoBehaviour
{
    #region Information
    [SerializeField] 
    GameObject 
    QuestionToActivate,Good,Medium,Bad;
   
    #endregion
    private void Start()
    {
        QuestionToActivate.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            QuestionToActivate.SetActive(true);
        }
    }

    public void closeQuestion()
    {
        QuestionToActivate.SetActive(false);
        Good.SetActive(false);
        Bad.SetActive(false);
        Medium.SetActive(false);
    }
}
