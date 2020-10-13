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
public class gameManagerGeoguessOriginal : MonoBehaviour
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

    public List<QuestionGEOORIGINAL> _questions;
    private  List<QuestionGEOORIGINAL> unanswerQuestions;
    private QuestionGEOORIGINAL currenteQuestion;
    private string routAcces;
    private string loadQuestion;
    private int countContestadas;
    private int countAcertadas;


    [SerializeField] private RawImage _factText;
    [SerializeField] private TMP_Text comuna;
    [SerializeField] private TMP_Text [] Respuesta;
    [SerializeField] private TMP_Text contestadas;
    [SerializeField] private TMP_Text acertadas;
    [SerializeField] private GameObject pantallaResultados;
    [SerializeField] private int pregunResponder;
    // [SerializeField] private Image UIImage;

    // [SerializeField] private TMP_Text LetretoGanador;
    [SerializeField] private TMP_Text puntuacionFinal;
   // [SerializeField] private GameObject nextQuestion;
    [SerializeField] private GameObject PanelRespuestas;

    [SerializeField] GameObject Good;
    [SerializeField] GameObject Bad;
    [SerializeField] GameObject CorrectAnsfeedBack;
    [SerializeField] GameObject clock, timer;
    [SerializeField] TMP_Text tmpCorrectAns;
    [SerializeField] float timeTonextQuestion;


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
    private void Awake()
    {

    }

    private void Start() {
        pantallaResultados.SetActive(false);
        PanelRespuestas.SetActive(true);
        // SaveJson();
        // LoadJson();
        if(unanswerQuestions==null||unanswerQuestions.Count==0)
        {
            unanswerQuestions=_questions.ToList<QuestionGEOORIGINAL>();
        }
        SetRandomQuestions();
        //nextQuestion.SetActive(false);
    }

    public void SetRandomQuestions(){
        int rdmQuestionIndex = Random.Range(0,unanswerQuestions.Count);
        currenteQuestion=unanswerQuestions[rdmQuestionIndex];

        _factText.texture=currenteQuestion.fact;
        comuna.text = currenteQuestion.comuna;
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
        //nextQuestion.SetActive(true);
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
        //nextQuestion.SetActive(true);
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
        //nextQuestion.SetActive(true);
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
        //nextQuestion.SetActive(true);
        PanelRespuestas.SetActive(false);

    }

    IEnumerator DetGanador(){
        yield return new WaitForSeconds(timeTonextQuestion);
        DeterminarGanador();
    }
    void DeterminarGanador()
    {
        pantallaResultados.SetActive(true);
        if(countContestadas==pregunResponder){
            puntuacionFinal.text=countAcertadas + "/" + pregunResponder;
            CorrectAnsfeedBack.SetActive(false);
            clock.SetActive(false);
            Bad.SetActive(false);
            Good.SetActive(false);
            timer.GetComponent<TimerQuestions>().Finish();
            timer.SetActive(false);

            //aca se envia la puntuacion al logrosManager
            logrosManager.LogrosSuma(2, 1);

        }
    }


    IEnumerator nextQuestionRoutine(){
        yield return  new WaitForSeconds(timeTonextQuestion);
        NextQuestion();
      
    }

    public void NextQuestion(){
        CorrectAnsfeedBack.SetActive(false);
        Good.SetActive(false);
        Bad.SetActive(false);
        SetRandomQuestions();
        PanelRespuestas.SetActive(true);
        //nextQuestion.SetActive(false);
    }


 


    








}
