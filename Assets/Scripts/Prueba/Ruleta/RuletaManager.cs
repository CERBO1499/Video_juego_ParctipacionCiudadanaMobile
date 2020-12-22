using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuletaManager : MonoBehaviour
{
    #region information
    [Header("informaation")]
    [SerializeField] RectTransform player;
    int point = 0;
    [SerializeField] RectTransform way;
    [SerializeField] float worldSpeed;
    Coroutine moveWorldCoroutine;
    [SerializeField] List<RectTransform> points;

    [SerializeField]
    RectTransform Historieta;
    [SerializeField]
    RectTransform fbEmocion1, fbEmocion2, fbEmocion3;
    [SerializeField]
    RectTransform Ruleta;

    [SerializeField]
    Image[] backGrounds;
    [SerializeField]
    RectTransform[] Keepers;
    [SerializeField]
    Color _color,_colorActive;
    [SerializeField]
    RectTransform FeedBackFinal;

    [SerializeField]
    RectTransform btnCloseNormal, btnCloseHistprieta;
    
    int counterEmociones=0;
    #region Restart
    Vector3 playerInitialLocalPosition;
    Vector3 worldInitialLocalPosition;
    [SerializeField] ParticleSystem particles;
    #endregion
    #endregion
    private void Awake()
    {
        playerInitialLocalPosition = player.localPosition;

        worldInitialLocalPosition = way.localPosition;

        

        for (int i = 0; i < backGrounds.Length; i++)
        {
            backGrounds[i].GetComponent<Image>();
            
            backGrounds[i].color=_color;
        }

        for (int i = 0; i < Keepers.Length; i++)
        {
            Keepers[i].gameObject.SetActive(false);
        }
    }

    public void Move(int boxes)
    {
        if (boxes > 0)
        {
            if (point + (boxes * 3) > points.Count - 1)
                boxes = ((points.Count - 1) - point) / 3;
        }

        if (boxes < 0)
        {
            if (point + (boxes * 3) < 0)
                boxes = -(point / 3);
        };

        if (boxes != 0)
        {
            moveWorldCoroutine = StartCoroutine(MoveWorldCorotuine((boxes > 0) ? 1 : -1));

            StartCoroutine(MovePlayerCorotuine(boxes));
        }
        else
            Ruleta.gameObject.SetActive(true);

    }

    IEnumerator MoveWorldCorotuine(int sense)
    {
        while (true)
        {
            way.localPosition += new Vector3(0f, -sense * worldSpeed * Time.deltaTime, 0f);

            yield return null;
        }
    }

    IEnumerator MovePlayerCorotuine(int boxes) 
    {
        for (int i = 0; i < Mathf.Abs(boxes); i++)
        {
            float t = Time.time;

            Vector3 initialPosition = player.localPosition;

            Vector3 finalPosition = points[point + ((boxes > 0) ? 1 : -1)].localPosition;

            while (Time.time <= t + 0.25f)
            {
                player.localPosition = initialPosition + ((finalPosition - initialPosition) * ((Time.time - t) / 0.25f));

                yield return null;
            }

            player.localPosition = finalPosition;

            t = Time.time;

            initialPosition = player.localPosition;

            finalPosition = points[point + ((boxes > 0) ? 2 : -2)].localPosition;

            while (Time.time <= t + 0.5f)
            {
                player.localPosition = initialPosition + ((finalPosition - initialPosition) * ((Time.time - t) / 0.5f));

                yield return null;
            }

            player.localPosition = finalPosition;

            t = Time.time;

            initialPosition = player.localPosition;

            finalPosition = points[point + ((boxes > 0) ? 3 : -3)].localPosition;

            while (Time.time <= t + 0.2f)
            {
                player.localPosition = initialPosition + ((finalPosition - initialPosition) * ((Time.time - t) / 0.2f));

                yield return null;
            }

            player.localPosition = finalPosition;

            point += ((boxes > 0) ? 3 : -3);
        }
        StopCoroutine(moveWorldCoroutine);

        Ruleta.gameObject.SetActive(true);


        if (point == points.Count - 1)
            FeedBackFinal.gameObject.SetActive(true);
    }

    public void Restart()
    {
        player.gameObject.SetActive(false);

        particles.transform.position = new Vector2(player.position.x, player.position.y) ;

        particles.Play();

        StartCoroutine(RestartCorotuine());
    }

    IEnumerator RestartCorotuine()
    {
        Vector3 finalocalPosition = way.localPosition;

        float t = Time.time;

        while (Time.time <= t + 1.5f)
        {
            way.localPosition = finalocalPosition + ((worldInitialLocalPosition - finalocalPosition) * ((Time.time - t) / 1.5f));

            yield return null;
        }

        way.localPosition = worldInitialLocalPosition;

        player.localPosition = playerInitialLocalPosition;

        particles.transform.position = new Vector2(player.position.x, player.position.y);

        particles.Play();

        player.gameObject.SetActive(true);

        Ruleta.gameObject.SetActive(true);
    }

    public void OpenEmocionPanel()
    {
       /* if (counterEmociones == 0)
            fbEmocion1.gameObject.SetActive(true);
       else if (counterEmociones == 1)
            fbEmocion2.gameObject.SetActive(true);
       else if (counterEmociones == 3)
            fbEmocion3.gameObject.SetActive(true);*/


        switch (counterEmociones)
        {
            case 0:
                fbEmocion1.gameObject.SetActive(true);
                backGrounds[0].color = _colorActive;
                Keepers[0].gameObject.SetActive(true);
                break;
            case 1:
                backGrounds[1].color = _colorActive;
                Keepers[1].gameObject.SetActive(true);
                fbEmocion2.gameObject.SetActive(true);
                break; 
            case 2:
                backGrounds[2].color = _colorActive;
                Keepers[2].gameObject.SetActive(true);
                break;
            case 3:
                backGrounds[3].color = _colorActive;
                Keepers[3].gameObject.SetActive(true);
                fbEmocion3.gameObject.SetActive(true);
                break;
            case 4:
                backGrounds[4].color = _colorActive;
                Keepers[4].gameObject.SetActive(true);
                break;
            case 5:
                backGrounds[5].color = _colorActive;
                Keepers[5].gameObject.SetActive(true);
                break;
            case 6:
                backGrounds[6].color = _colorActive;
                Keepers[6].gameObject.SetActive(true);
                break;
            case 7:
                backGrounds[7].color = _colorActive;
                Keepers[7].gameObject.SetActive(true);
                break;
            case 8:
                backGrounds[8].color = _colorActive;
                Keepers[8].gameObject.SetActive(true);
                btnCloseNormal.gameObject.SetActive(false);
                btnCloseHistprieta.gameObject.SetActive(true);
                break;

            default:
                break;
        }

        Historieta.gameObject.SetActive(true);
        counterEmociones++;      
    }

    public void ClosePanelHistory()
    {
        Historieta.gameObject.SetActive(false);
        Ruleta.gameObject.SetActive(true);
    }

    public void ClosePanelWithFeedBack()
    {
        Historieta.gameObject.SetActive(false);
        FeedBackFinal.gameObject.SetActive(true);        
    }
}