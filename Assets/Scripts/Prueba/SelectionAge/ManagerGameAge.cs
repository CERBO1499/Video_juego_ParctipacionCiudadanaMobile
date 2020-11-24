using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGameAge : MonoBehaviour
{
    #region Information
    [Header("Pines position")]
    [SerializeField] List<GameObject> PinesInfantiles;
    [SerializeField] List<GameObject> PinesJuveniles;
    [Header("Canvas Pines")]
    [SerializeField] List<GameObject> CanvasPinesInfantiles;
    [SerializeField] List<GameObject> CanvasPinesJuveniles;
    #endregion

    private void Start()
    {
        if (JsonContainer.instance.Pcharacter.Old == "0")
        {
            for (int i = 0; i < PinesInfantiles.Count; i++)
            {
                PinesInfantiles[i].SetActive(true);
            }
        }

        if (JsonContainer.instance.Pcharacter.Old == "1")
        {
            for (int i = 0; i < PinesJuveniles.Count; i++)
            {
                PinesJuveniles[i].SetActive(true);
            }
        }
    }
}
