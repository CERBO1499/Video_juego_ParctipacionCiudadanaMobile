using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class TimerQuestions : MonoBehaviour
{
    [SerializeField] TMP_Text timer;
    [SerializeField] GameObject panelTimeOver;
    public float timerStart;

    bool timerActive = false;
    private void Start()
    {
        timer.text = timerStart.ToString();
    }

    private void Update()
    {
        if (timerActive)
        {
            timerStart -= Time.deltaTime;
            
            timer.text = Mathf.Round(timerStart).ToString();
            checkTimer();
        }
    }

    public void ActiveTimer()
    {
        timerActive = true;
    }


    void checkTimer()
    {
        if (timerStart <= 0.2)
        {
            panelTimeOver.SetActive(true);
            timer.text = "00";
        }
    }
    public void Finish()            //cuando se gana el tiempo deja de contar
    {
        timerActive = false;
        
    }
}
