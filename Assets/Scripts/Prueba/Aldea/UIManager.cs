using System;
using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Static
    public static UIManager instance;
    #endregion

    #region Information
    [Header("Information", order = 0)]
    [SerializeField] GameObject introduction;
    Action pass;
    public Action Ppass
    {
        get { return pass; }
    }
    [Space(order = 1)]
    #region Circus
    [Header("Circus", order = 2)]
    [SerializeField] RectTransform circus;
    [SerializeField] Keeper[] keepers;
    int activeKeepers = 3;
    [SerializeField] Word[] words;
    [SerializeField] GameObject continueBtn;
    [SerializeField] AnimationCurve finalCurve;
    [SerializeField] GameObject AnotherGame;
    #endregion
    [Space(order = 3)]
    #region Grafitiando
    [Header("Grafitiando", order = 4)]
    [SerializeField] RectTransform field;
    [SerializeField] GameObject secondActivity;
    [SerializeField] GameObject grafitiando;
    [SerializeField] GameObject grafitiandoFeedback;
    #endregion
    #region Sopa de letras
    [Header("Sopa de letras", order = 5)]
    [SerializeField] RectTransform library;
    [SerializeField] Material lineMaterial;
    [SerializeField] GameObject thirdActivity;
    [SerializeField] GameObject sopaDeLetras;
    [SerializeField] Word[] soupWords;
    [SerializeField] AnimationCurve soupCurve;
    [SerializeField] GameObject soupContinueBtn;
    [SerializeField] GameObject soupFeedback;
    public Word[] PsoupWords
    {
        get { return soupWords; }
    }
    public Material PlineMaterial
    {
        get { return lineMaterial; }
    }
    #endregion
    [Space(order = 6)]
    [SerializeField] AnimationCurve activityCurve;
    #endregion

    private void Awake()
    {
        instance = this;
    }

    public void ActiveFirstactivity(GameObject firstActivity)
    {
        introduction.SetActive(false);

        firstActivity.SetActive(true);

        pass = () => 
        {
            firstActivity.SetActive(false);
        };
    }

    public void PlayFirstactivity(GameObject firstActivity)
    {
        pass();

        firstActivity.SetActive(true);

        pass = () =>
        {
            string message = "";

            for (int i = 0; i < activeKeepers; i++)
            {
                if (keepers[i].keeped != null)
                    message += keepers[i].keeped.GetComponent<Word>().Pword + ((i < activeKeepers - 1) ? " " : "");
                else
                    return;
            }

            if (message == "todos somos importantes")
            {
                EndFirstActivity();

                pass = () =>
                {
                    firstActivity.SetActive(false);

                    StartCoroutine(showItem(circus, () =>
                    {
                        AnotherGame.SetActive(true);
                    }));
                };
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

    public void ActiveSecondActivity()
    {
        secondActivity.SetActive(false);

        grafitiando.SetActive(true);

        pass = () =>
        {
            grafitiando.SetActive(false);

            StartCoroutine(showItem(field, () =>
            {
                grafitiandoFeedback.SetActive(true);
            }));
        };
    }

    public void ActiveThirdActivity()
    {
        grafitiandoFeedback.SetActive(false);

        thirdActivity.SetActive(true);
    }

    public void UpdateSoup()
    {
        string soupSentence = "";

        for (int i = 0; i < soupWords.Length; i++)
        {
            if(soupWords[i].gameObject.GetComponent<RectTransform>().GetChild(0).gameObject.activeSelf)
                soupSentence += soupWords[i].Pword + ((i < soupWords.Length - 1) ? " " : "");
        }

        if (soupSentence == "LA LLENA DE LECTURA AVENTURAS")
        {
            aldea.LineCreator.create = false;

            for (int i = 0; i < soupWords.Length; i++)
                soupWords[i].gameObject.GetComponent<RectTransform>().GetChild(0).gameObject.SetActive(false);

            StartCoroutine(ReorderSoupWords());
        }
    }

    IEnumerator ReorderSoupWords()
    {
        soupWords[0].gameObject.GetComponent<RectTransform>().parent.gameObject.GetComponent<UnityEngine.UI.VerticalLayoutGroup>().enabled = false;

        Vector2[] initialAnchoredPosition = new Vector2[soupWords.Length];

        for (int i = 0; i < soupWords.Length; i++)
            initialAnchoredPosition[i] = soupWords[i].prect.anchoredPosition;

        float t = Time.time;

        while (Time.time <= t + 1f)
        {
            for (int i = 0; i < soupWords.Length; i++)
                soupWords[i].prect.anchoredPosition = initialAnchoredPosition[i] + ((soupWords[i].PfinalLocalPosition - initialAnchoredPosition[i]) * soupCurve.Evaluate(Time.time - t));

            yield return null;
        }

        for (int i = 0; i < soupWords.Length; i++)
            soupWords[i].prect.anchoredPosition = soupWords[i].PfinalLocalPosition;

        pass = () =>
        {
            sopaDeLetras.SetActive(false);

            StartCoroutine(showItem(library, () => 
            {
                soupFeedback.SetActive(true);
            }));
        };

        soupContinueBtn.SetActive(true);
    }

    public void ReactiveFirstActivity(GameObject firstActivity)
    {
        for (int i = 0; i < activeKeepers; i++)
            keepers[i].gameObject.SetActive(false);

        Keeper[] newKeepers = new Keeper[keepers.Length - activeKeepers];

        for (int i = 0; i < newKeepers.Length; i++)
            newKeepers[i] = keepers[i + activeKeepers];

        keepers = newKeepers;

        for (int i = 0; i < activeKeepers; i++)
            words[i].gameObject.SetActive(false);

        Word[] newWords = new Word[words.Length - activeKeepers];

        for (int i = 0; i < newWords.Length; i++)
            newWords[i] = words[i + activeKeepers];

        words = newWords;

        activeKeepers = 5;

        for (int i = 0; i < activeKeepers; i++)
            keepers[i].gameObject.SetActive(true);

        for (int i = 0; i < activeKeepers; i++)
            words[i].gameObject.SetActive(true);

        continueBtn.SetActive(false);

        AnotherGame.SetActive(false);

        firstActivity.SetActive(true);

        Word.drag = true;

        pass = () =>
        {
            string message = "";

            for (int i = 0; i < activeKeepers; i++)
            {
                if (keepers[i].keeped != null)
                    message += keepers[i].keeped.GetComponent<Word>().Pword + ((i < activeKeepers - 1) ? " " : "");
                else
                    return;
            }

            if (message == "puedes crear tu propio universo")
            {
                EndFirstActivity();

                pass = () =>
                {
                    firstActivity.SetActive(false);

                    ExitToFirstActivity();
                };
            }
        };
    }

    public void EndFirstActivity()
    {
        Word.drag = false;

        StartCoroutine(EndFirstActivityCorotuine());
    }

    IEnumerator EndFirstActivityCorotuine()
    {
        float t = Time.time;

        while (Time.time <= t + 0.5f)
        {
            for (int i = 0; i < words.Length; i++)
                words[i].gameObject.transform.localScale = Vector3.one * finalCurve.Evaluate((Time.time - t) / 0.5f);

            yield return null;
        }

        continueBtn.SetActive(true);
    }

    public void Continue()
    {
        pass();
    }

    public void ExitToFirstActivity()
    {
        if (AnotherGame.activeSelf)
            AnotherGame.SetActive(false);

        secondActivity.SetActive(true);
    }
    public void PlayThirdactivity()
    {
        thirdActivity.SetActive(false);

        sopaDeLetras.SetActive(true);
    }

    public void ExitToSecondActivity()
    {
        pass();
    }

    IEnumerator showItem(RectTransform rect, Action output)
    {
        float t = Time.time;

        while (Time.time <= t + 0.75f)
        {
            rect.localScale = Vector3.one * activityCurve.Evaluate((Time.time - t) / 0.75f);

            yield return null;
        }

        rect.localScale = Vector3.one;

        yield return new WaitForSeconds(0.25f);

        output();
    }
}