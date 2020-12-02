using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowKey : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Components
    [Header("Information")]
    [SerializeField] Vector2 axis;
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] LabyrinthMove characterMove;
    #endregion

    public void OnPointerDown(PointerEventData eventData)
    {
        characterMove.Move(axis);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        characterMove.Move(Vector2.zero);
    }
}
