using UnityEngine;

public class LChunk : MonoBehaviour
{
    #region Information
    [Header("Information")]
    [SerializeField] GameObject nextChuck;
    [SerializeField] Vector2 localPosition;

    public static new bool enabled;
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] RectTransform player;
    #endregion


    void OnTriggerEnter2D(Collider2D collision)
    {
        enabled = false;

        foreach (RectTransform rect in nextChuck.GetComponent<RectTransform>().parent.gameObject.GetComponent<RectTransform>())
        {
            if (rect.gameObject.activeSelf)
            {
                rect.gameObject.SetActive(false);

                break;
            }
        }

        nextChuck.SetActive(true);

        player.localPosition = localPosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enabled = true;
    }
}