using UnityEngine;

public class WebLine : MonoBehaviour
{
    [HideInInspector]
    public RectTransform initialPosition;
    [HideInInspector]
    public RectTransform finalPosition;

    #region Components
    LineRenderer LineRenderer;
    #endregion

    public void Awake()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }

    public void UpdatePosition()
    {
        LineRenderer.SetPosition(0, initialPosition.position);

        LineRenderer.SetPosition(1, finalPosition.position);
    }
}