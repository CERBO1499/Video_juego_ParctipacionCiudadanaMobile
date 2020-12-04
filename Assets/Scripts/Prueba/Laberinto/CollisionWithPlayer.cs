using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithPlayer : MonoBehaviour
{
    #region Information
    [Header("OPbjects to active")]
    [SerializeField]
    RectTransform Pregunta;

    int Counter = 0;
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Pregunta.gameObject.SetActive(true);
            gameObject.SetActive(false);
            Counter++;
           
        }
    }
       
    
}