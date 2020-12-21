using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    int counterEmociones=0;
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Move(-3);
        if (Input.GetKeyDown(KeyCode.S))
            Move(-2);
        if (Input.GetKeyDown(KeyCode.D))
            Move(-1);

        if (Input.GetKeyDown(KeyCode.F))
            Move(1);
        if (Input.GetKeyDown(KeyCode.G))
            Move(2);
        if (Input.GetKeyDown(KeyCode.H))
            Move(3);
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
    }

    public void OpenEmocionPanel()
    {
        if (counterEmociones == 0)
            fbEmocion1.gameObject.SetActive(true);
       else if (counterEmociones == 1)
            fbEmocion2.gameObject.SetActive(true);
       else if (counterEmociones == 3)
            fbEmocion3.gameObject.SetActive(true);
        
        Historieta.gameObject.SetActive(true);
        counterEmociones++;      
    }


    public void ClosePanelHistory()
    {
        Historieta.gameObject.SetActive(false);
        Ruleta.gameObject.SetActive(true);

    }

}