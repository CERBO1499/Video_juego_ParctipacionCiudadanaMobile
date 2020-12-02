using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrowKey : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    #region Components
    bool exit;
    [Header("Information")]
    [SerializeField] Vector2 axis;
    #endregion

    #region Components
    Button btn;
    [Header("Components")]
    [SerializeField] LabyrinthMove labyrinthMove;
    #endregion

    void Awake()
    {
        btn = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (exit)
        {
            exit = false;

            btn.interactable = true;
        }

        labyrinthMove.Move(axis);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!exit)
        {
            exit = true;

            labyrinthMove.Move(Vector2.zero);

            btn.interactable = false;
        }
    }
}