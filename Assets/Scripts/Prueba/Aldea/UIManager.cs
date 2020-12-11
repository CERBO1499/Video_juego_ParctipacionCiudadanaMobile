using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Information
    [Header("Information")]
    #region Parts
    [Header("Parts")]
    [SerializeField] RectTransform circus;
    #endregion
    [Space]
    [SerializeField] GameObject introduction;
    System.Action Pass;
    #endregion

    public void ActiveFirstactivity(GameObject firstActivity)
    {
        introduction.SetActive(false);

        firstActivity.SetActive(true);

        Pass = () => 
        {
            firstActivity.SetActive(false);
        };
    }

    public void PlayFirstactivity(GameObject firstActivity)
    {
        Pass();

        firstActivity.SetActive(true);

        Pass = () =>
        {
            firstActivity.SetActive(false);
        };
    }
}