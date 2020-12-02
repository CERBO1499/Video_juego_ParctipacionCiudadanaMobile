using UnityEngine;
using UnityEngine.UI;

public class LabyrinthManager : MonoBehaviour
{
    #region Components
    [Header("Components")]
    #region Background
    [Header("Instructions")]
    [SerializeField] Image mapImg;
    [SerializeField] Sprite webGlMap;
    #endregion
    [Space]
    #region Buttons
    [Header("Buttons")]
    [SerializeField] Button upArrow;
    [SerializeField] Button leftArrow;
    [SerializeField] Button rightArrow;
    [SerializeField] Button downArrow;
    #endregion
    #endregion

    void Awake()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            mapImg.sprite = webGlMap;

            upArrow.gameObject.SetActive(false);
            leftArrow.gameObject.SetActive(false);
            rightArrow.gameObject.SetActive(false);
            downArrow.gameObject.SetActive(false);
        }
    }
}