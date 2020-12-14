using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Static
    public static UIManager instance;
    #endregion

    #region Information
    [Header("Information")]
    [SerializeField] GameObject introduction;
    System.Action Pass;
    public System.Action PPass
    {
        get { return Pass; }
    }
    [Space]
    #region Circus
    [Header("Circus")]
    [SerializeField] RectTransform circus;
    [SerializeField] Keeper[] keepers;
    int activeKeepers = 5;
    [SerializeField] GameObject AnotherGame;
    #endregion
    #endregion

    private void Awake()
    {
        instance = this;
    }

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
            string message = "";

            for (int i = 0; i < activeKeepers; i++)
            {
                if (keepers[i].keeped != null)
                    message += keepers[i].keeped.GetComponent<Word>().Pword + ((i < activeKeepers - 1) ? " " : "");
                else
                    return;
            }

            if (message == "Puedes crear tu propio universo")
            {
                firstActivity.SetActive(false);

                AnotherGame.SetActive(true);
            }
        };
    }

    public void UpdateKeepers(GameObject word)
    {
        for (int i = 0; i < keepers.Length; i++)
        {
            if (word == keepers[i].keeped)
            {
                keepers[i].keeped = null;

                break;
            }
        }
    }
}