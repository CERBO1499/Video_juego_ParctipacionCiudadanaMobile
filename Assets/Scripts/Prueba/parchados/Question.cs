using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public struct Question{
 
public string fact;
public int correctAns;
private const int NUM_ANSWERS=4;

public string respuestaA;
public string RespuestaB;
public string RespuestaC;

public string RespuestaD;
}
public class ListQuestions{
    public List<Question> jsnQUestions;
    public ListQuestions(List<Question> jsnQUestions){
        this.jsnQUestions=jsnQUestions;
    }

}
