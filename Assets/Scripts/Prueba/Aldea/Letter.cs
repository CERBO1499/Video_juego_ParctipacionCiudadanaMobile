using UnityEngine;

public class Letter : MonoBehaviour
{
    #region Information
    [Header("Information")]
    public char letter;
    public  Vector2 position;
    #endregion

    private void Awake()
    {
        position = new Vector2(GetComponent<RectTransform>().GetSiblingIndex() - (11 * (GetComponent<RectTransform>().GetSiblingIndex() / 11)), GetComponent<RectTransform>().GetSiblingIndex() / 11);
    }
}