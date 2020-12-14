using System;
using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Static
    public static UIManager instance;
    #endregion

    #region Information
    [Header("Information")]
    [SerializeField] GameObject introduction;
    Action pass;
    public Action Ppass
    {
        get { return pass; }
    }
    [Space]
    #region Circus
    [Header("Circus")]
    [SerializeField] RectTransform circus;
    [SerializeField] Keeper[] keepers;
    int activeKeepers = 5;
    [SerializeField] Word[] words;
    [SerializeField] GameObject continueBtn;
    [SerializeField] AnimationCurve finalCurve;
    [SerializeField] GameObject AnotherGame;
    #endregion
    [Space]
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

            if (message == "puedes crear tu propio universo")
            {
                EndFirstActivity();

                pass = () =>
                {
                    firstActivity.SetActive(false);

                    StartCoroutine(ShowCircusCoroutine(() =>
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

    IEnumerator ShowCircusCoroutine(Action output)
    {
        float t = Time.time;

        while (Time.time <= t + 0.75f)
        {
            circus.localScale = Vector3.one * activityCurve.Evaluate((Time.time - t) / 0.75f);

            yield return null;
        }

        circus.localScale = Vector3.one;

        output();
    }

    public void ReactiveFirstActivity(GameObject firstActivity)
    {
        for (int i = 0; i < keepers.Length; i++)
        {
            keepers[i].keeped = null;

            keepers[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < words.Length; i++)
        {
            if (i < activeKeepers)
                words[i].OnPointerFail();

            words[i].gameObject.SetActive(true);
        }

        activeKeepers = 8;

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

            if (message == "puedes crear tu propio universo todos somos importantes")
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
    }
}