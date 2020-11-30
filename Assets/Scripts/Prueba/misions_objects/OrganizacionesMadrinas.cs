using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizacionesMadrinas : MonoBehaviour
{
    #region Information
    [Header("UI punto organizaciones madrinas")]
    [SerializeField]
    RectTransform uIOrganuizacionesMadrinas;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            uIOrganuizacionesMadrinas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            uIOrganuizacionesMadrinas.gameObject.SetActive(false);
        }   
    }

}
