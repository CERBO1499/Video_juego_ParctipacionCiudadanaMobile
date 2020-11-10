using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    #region Information
    [Header("Information")]
    [SerializeField] GameObject logo;
    [SerializeField] GameObject userTxt;
    [SerializeField] GameObject passwordTxt;
    [SerializeField] GameObject userIf;
    public string getUser
    {
        get { return userIf.GetComponent<TMPro.TMP_InputField>().text; }
    }
    [SerializeField] GameObject passwordIf;
    public string getPassword
    {
        get { return passwordIf.GetComponent<TMPro.TMP_InputField>().text; }
    }
    [SerializeField] GameObject play;
    [SerializeField] AnimationCurve curve;
    #endregion

    #region Components
    [Header("Components")]
    [SerializeField] Image titleImg;
    [SerializeField] DeprecatedBanner deprecatedBanner;
    [SerializeField] GetCharacter getCharacter;
    #endregion

    void Awake()
    {
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        Color initialColor = titleImg.color;

        Color finalColor = Color.white;

        float t = Time.time;

        while (Time.time <= t + 2f)
        {
            titleImg.color = initialColor + ((finalColor - initialColor) * curve.Evaluate((Time.time - t) / 2f));

            yield return null;
        }

        logo.SetActive(true);

        titleImg.color = finalColor;

        userTxt.SetActive(true);

        passwordTxt.SetActive(true);

        userIf.SetActive(true);

        passwordIf.SetActive(true);

        play.SetActive(true);

        if (deprecatedBanner.PactiveCoroutine)
            play.GetComponent<Button>().interactable = false;

        if (PlayerPrefs.HasKey("User Name"))
        {
            if (PlayerPrefs.GetString("User Name") != "")
            {
                play.GetComponent<Button>().interactable = false;

                userIf.GetComponent<TMPro.TMP_InputField>().text = PlayerPrefs.GetString("User Name");

                passwordIf.GetComponent<TMPro.TMP_InputField>().text = PlayerPrefs.GetString("Password");

                getCharacter.Get();
            }
        }
    }
}