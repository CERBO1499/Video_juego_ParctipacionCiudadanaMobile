using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFinish : MonoBehaviour
{
   
    [SerializeField]
    RectTransform FinalButon;
    [SerializeField]
    AnimationCurve curve;



    bool FinishGame = false;
   
    public void CheckIsFinish()
    {
        FinishGame = true;
        CheckCounter();
       
    }
    void CheckCounter()
    {
        if(FinishGame)
            StartCoroutine(CreaceBtn());
    }

    IEnumerator CreaceBtn()
    {
        FinalButon.gameObject.SetActive(true);

        Vector2 initialSize = Vector3.zero;
        Vector2 finalSize = Vector3.one;

        float t = Time.time;

        while (Time.time <= t + 1f)
        {
            FinalButon.localScale = initialSize + ((finalSize - initialSize) * curve.Evaluate((Time.time - t) / 1f));
            yield return null;
        }
        FinalButon.localScale = finalSize;

    }
}
