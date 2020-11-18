using UnityEngine;
using UnityEngine.UI;

public class Album : MonoBehaviour
{
    #region Information
    int page = 0;
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] RectTransform pages;
    [SerializeField] Button back, pass;
    #endregion

    public void Pass()
    {
        if (page + 1 < pages.childCount)
        {
            page++;

            pages.GetChild(page - 1).gameObject.SetActive(false);

            pages.GetChild(page).gameObject.SetActive(true);

            if (page == pages.childCount - 1)
                pass.interactable = false;
            else
            {
                if (!back.interactable)
                    back.interactable = true;
            }
        }
    }

    public void Back()
    {
        if (page - 1 >= 0)
        {
            page--;

            pages.GetChild(page + 1).gameObject.SetActive(false);

            pages.GetChild(page).gameObject.SetActive(true);

            if (page == 0)
                back.interactable = false;
            else
            {
                if (!pass.interactable)
                    pass.interactable = true;
            }
        }
    }
}