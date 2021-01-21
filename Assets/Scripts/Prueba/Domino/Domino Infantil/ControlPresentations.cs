using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPresentations : MonoBehaviour
{
    #region Information
    [SerializeField] RectTransform [] descriptions;
    [SerializeField] RectTransform PanelPresentation;
    int countClick=0;
    #endregion

    public void NextDescription()
    {
        switch (countClick)
        {
            case 0:
                descriptions[0].gameObject.SetActive(false);
                descriptions[1].gameObject.SetActive(true);
                countClick++;
                break;
            case 1:
                descriptions[1].gameObject.SetActive(false);
                descriptions[2].gameObject.SetActive(true);
                countClick++;
                break;
            case 2:
                descriptions[2].gameObject.SetActive(false);
                descriptions[3].gameObject.SetActive(true);
                countClick++;
                break;
            case 3:
                descriptions[3].gameObject.SetActive(false);
                descriptions[4].gameObject.SetActive(true);
                countClick++;
                break;
            case 4:
                descriptions[4].gameObject.SetActive(false);
                descriptions[5].gameObject.SetActive(true);
                countClick++;
                break;
            case 5:
                PanelPresentation.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
