using UnityEngine;
using UnityEngine.UI;

public class LabyrinthManager : MonoBehaviour
{
    #region Components
    [Header("Components")]
    #region Background
    [Header("Background")]
    [SerializeField] Image mapImg;
    [SerializeField] Sprite webGlMap;
    #endregion
    [Space]
    #region Player
    [SerializeField] RectTransform player;
    [SerializeField] Material material;
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

        material.SetVector("_PlayerPosition", new Vector4(player.localPosition.x, player.localPosition.y, 0f, 0f));
    }

    void Update()
    {
        material.SetVector("_PlayerPosition", new Vector4(player.localPosition.x, player.localPosition.y, 0f, 0f));
    }
}