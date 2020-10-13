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
public class GamemanagerGEOGUESS : MonoBehaviour
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

    public List<QuestionGEOGUESS> _questionsGEOGUESS;
    private static List<QuestionGEOGUESS> unanswerQuestions;
    private QuestionGEOGUESS currenteQuestion;
    private string routAcces;
    private string loadQuestion;
    private int countContestadas;
    private int countAcertadas;


    [SerializeField] private TMP_Text _factText;
    [SerializeField] private RawImage [] Respuesta;
    [SerializeField] private TMP_Text contestadas;
    [SerializeField] private TMP_Text acertadas;
    [SerializeField] private GameObject pantallaResultados;
    [SerializeField] private int pregunResponder;
    // [SerializeField] private Image UIImage;

    // [SerializeField] private TMP_Text LetretoGanador;
    [SerializeField] private TMP_Text puntuacionFinal;
    [SerializeField] private GameObject nextQuestion;


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
        pantallaResultados.SetActive(false);
        // SaveJson();
        // LoadJson();
        if(unanswerQuestions==null||unanswerQuestions.Count==0)
        {
            unanswerQuestions=_questionsGEOGUESS.ToList<QuestionGEOGUESS>();
        }
        SetRandomQuestions();
        nextQuestion.SetActive(false);
    }

    public void SetRandomQuestions(){
        int rdmQuestionIndex = Random.Range(0,unanswerQuestions.Count);
        currenteQuestion=unanswerQuestions[rdmQuestionIndex];

        _factText.text=currenteQuestion.fact;
        Respuesta[0].texture = currenteQuestion.respuestaA;
        Respuesta[1].texture = currenteQuestion.RespuestaB;
        Respuesta[2].texture = currenteQuestion.RespuestaC;
        Respuesta[3].texture = currenteQuestion.RespuestaD;

        unanswerQuestions.RemoveAt(rdmQuestionIndex);
    }


    void ActualizarUIAcertadas ()
    {
        countContestadas++;
        countAcertadas++;
        contestadas.text=countContestadas.ToString()+"/"+pregunResponder;
        acertadas.text=countAcertadas.ToString()+"/"+pregunResponder;
    }

    void ActualizarUINOAcertadas(){
        countContestadas++;
        contestadas.text=countContestadas.ToString() + "/" + pregunResponder;
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
            DeterminarGanador();
        }
        nextQuestion.SetActive(true);

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
            DeterminarGanador();
        }
        nextQuestion.SetActive(true);

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
            DeterminarGanador();
        }
        nextQuestion.SetActive(true);

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
            DeterminarGanador();
        }
        nextQuestion.SetActive(true);

    }
    void DeterminarGanador()
    {
        pantallaResultados.SetActive(true);
        if(countContestadas==pregunResponder){
            puntuacionFinal.text=countAcertadas + "/" + pregunResponder;
        }
    }

    public void NextQuestion(){
        SetRandomQuestions();
    }


 


    








}
