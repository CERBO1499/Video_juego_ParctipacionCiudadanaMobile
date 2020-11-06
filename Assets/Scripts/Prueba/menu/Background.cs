using System;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    #region Information
    [Header("Information")]
    [SerializeField] Sprite dayBackground;
    [SerializeField] Sprite nightBackground;
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] Image background;
    #endregion

    private void Awake()
    {
        if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour <= 18)
            background.sprite = dayBackground;
        else
            background.sprite = nightBackground;
    }
}