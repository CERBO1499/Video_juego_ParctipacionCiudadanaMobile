using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System.IO;
using TMPro;


[RequireComponent(typeof(AudioSource))]
public class Gamemanager : MonoBehaviour
{
    // Singleton for activation of manager
    // public static Gamemanager _instance;
    // private void Awake() {
    //        if (_instance == null)
    //     {
    //         _instance = this;
    //         DontDestroyOnLoad(this.gameObject);
    
    //         //Rest of your Awake code
    
    //     } else {
    //         Destroy(this);
    //     }
    // } 

    public List<Question> _questions;
    private List<Question> unanswerQuestions;
    private Question currenteQuestion;
    private string routAcces;
    private string loadQuestion;
    private int countContestadas;
    private int countAcertadas;


    [SerializeField] private TMP_Text _factText;
    [SerializeField] private TMP_Text [] Respuesta;
    [SerializeField] private TMP_Text contestadas;
    [SerializeField] private TMP_Text acertadas;
    [SerializeField] private GameObject pantallaResultados;
     public int pregunResponder;
    // [SerializeField] private Image UIImage;

    // [SerializeField] private TMP_Text LetretoGanador;
    [SerializeField] private TMP_Text puntuacionFinal;
    //[SerializeField] private GameObject nextQuestion;
    [SerializeField] private GameObject PanelRespuestas;
    [SerializeField] GameObject Good;
    [SerializeField] GameObject Bad;
    [SerializeField] GameObject clock,Timer;
    [SerializeField] float timeToNextQuestion;
    [SerializeField] GameObject CorrectAnsfeedBack;

    [SerializeField] TMP_Text tmpCorrectAns;

    TimerQuestions tmQ;

    // private void SaveJson()
    // {
    //     routAcces= Application.dataPath+"/Json"+"Questions.Json";
    //     ListQuestions newQuestions = new ListQuestions(_questions);
    //     loadQuestion = JsonUtility.ToJson(newQuestions);
    //     Debug.Log(loadQuestion);
    //     File.WriteAllText(routAcces,loadQuestion);
    // }
    // private void LoadJson(){
    //      routAcces = Application.dataPath + "/Json" + "Questions.Json";
    //     loadQuestion = File.ReadAllText(routAcces);
    //     ListQuestions cargar = JsonUtility.FromJson<ListQuestions>(loadQuestion);
    //     _questions = cargar.jsnQUestions;
    // }


    private void Start() {
        tmQ= Timer.GetComponent<TimerQuestions>();  //para el conteo del tiempo
        pantallaResultados.SetActive(false);
        PanelRespuestas.SetActive(true);
        // SaveJson();
        // LoadJson();
        if(unanswerQuestions==null||unanswerQuestions.Count==0)
        {
            unanswerQuestions=_questions.ToList<Question>();
        }
        SetRandomQuestions();
        //nextQuestion.SetActive(false);
    }

    public void SetRandomQuestions(){
        int rdmQuestionIndex = Random.Range(0,unanswerQuestions.Count);
        currenteQuestion=unanswerQuestions[rdmQuestionIndex];

        _factText.text=currenteQuestion.fact;
        Respuesta[0].text = currenteQuestion.respuestaA;
        Respuesta[1].text = currenteQuestion.RespuestaB;
        Respuesta[2].text = currenteQuestion.RespuestaC;
        Respuesta[3].text = currenteQuestion.RespuestaD;

        unanswerQuestions.RemoveAt(rdmQuestionIndex);
    }


    void ActualizarUIAcertadas ()
    {
        countContestadas++;
        countAcertadas++;
        contestadas.text=countContestadas.ToString()+"/"+pregunResponder;
        acertadas.text=countAcertadas.ToString()+"/"+pregunResponder;
        Good.SetActive(true);
         StartCoroutine(nextQuestionRoutine());

    }

    void ActualizarUINOAcertadas(){
        countContestadas++;
        contestadas.text=countContestadas.ToString() + "/" + pregunResponder;
        Bad.SetActive(true);
        CorrectAnsfeedBack.SetActive(true);
        switch (currenteQuestion.correctAns)
        {   
            case 0:
            tmpCorrectAns.text=Respuesta[0].text;
            break;
            case 1:
            tmpCorrectAns.text=Respuesta[1].text;
            break;
            case 2:
            tmpCorrectAns.text=Respuesta[2].text;
            break;
            case 3:
            tmpCorrectAns.text=Respuesta[3].text;
            break;

            
        }
        StartCoroutine(nextQuestionRoutine());


    }

    public void UserSelectResponseA()
    {
        if(countContestadas <= pregunResponder)
        {
            if(currenteQuestion.correctAns==0)
            {
                ActualizarUIAcertadas();
            }
            else            
            {
                ActualizarUINOAcertadas();
            }
        }

        if(countContestadas>=pregunResponder)
        {
            StartCoroutine(DetGanador());
        }
       // nextQuestion.SetActive(true);
        PanelRespuestas.SetActive(false);

    }
    public void UserSelectResponseB()
    {
        if(countContestadas <= pregunResponder)
        {
            if(currenteQuestion.correctAns==1)
            {
                ActualizarUIAcertadas();
            }
            else            
            {
                ActualizarUINOAcertadas();
            }
        }

        if(countContestadas>=pregunResponder)
        {
            StartCoroutine(DetGanador());
        }
       // nextQuestion.SetActive(true);
        PanelRespuestas.SetActive(false);

    }

    public void UserSelectResponseC()
    {
        if(countContestadas <= pregunResponder)
        {
            if(currenteQuestion.correctAns==2)
            {
                ActualizarUIAcertadas();
            }
            else            
            {
                ActualizarUINOAcertadas();
            }
        }

        if(countContestadas>=pregunResponder)
        {
            StartCoroutine(DetGanador());
            
        }
       // nextQuestion.SetActive(true);
        PanelRespuestas.SetActive(false);

    }
    public void UserSelectResponseD()
    {
        if(countContestadas <= pregunResponder)
        {
            if(currenteQuestion.correctAns==3)
            {
                ActualizarUIAcertadas();
            }
            else            
            {
                ActualizarUINOAcertadas();
            }
        }

        if(countContestadas>=pregunResponder)
        {
            StartCoroutine(DetGanador());
        }
      //  nextQuestion.SetActive(true);
        PanelRespuestas.SetActive(false);

    }

    IEnumerator DetGanador(){
        yield return new WaitForSeconds(timeToNextQuestion);
        DeterminarGanador();
    }
    void DeterminarGanador()        //al terminar
    {
        pantallaResultados.SetActive(true);
        if(countContestadas==pregunResponder){
            puntuacionFinal.text=countAcertadas + "/" + pregunResponder;
            CorrectAnsfeedBack.SetActive(false);
            Good.SetActive(false);
            Bad.SetActive(false);
            clock.SetActive(false);
            tmQ.Finish();
            Timer.SetActive(false);
            logrosManager.LogrosSuma(2, 1);//vaya a suma PIN

        }
    }


    IEnumerator nextQuestionRoutine(){
        yield return  new WaitForSeconds(timeToNextQuestion);
        NextQuestion();
      
    }

    public void NextQuestion(){
        Good.SetActive(false);
        Bad.SetActive(false);
        SetRandomQuestions();
        PanelRespuestas.SetActive(true);
        CorrectAnsfeedBack.SetActive(false);

        //nextQuestion.SetActive(false);
    }


 


    








}
