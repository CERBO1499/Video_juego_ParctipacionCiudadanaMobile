using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RespuestasAleatorias : MonoBehaviour
{
  [SerializeField] GameObject[] responsesPos;
  [SerializeField] GameObject[] botons;  
   int Rand;
   int Lenght = 4;
   List<int> listNumbers = new List<int>(); 
private void Start() {
   SetrandomNumber();
}
    public void SetrandomNumber()
    {
      listNumbers = new List<int>(new int[Lenght]);
        for (int j = 0; j < Lenght; j++)
        {
            Rand = Random.Range(0,5);
            while (listNumbers.Contains(Rand))
            {
                Rand = Random.Range(0,5);
            }
            listNumbers[j] = Rand;
            print(listNumbers[j]);
        }
        botons[0].transform.position=responsesPos[listNumbers[0]-1].transform.position;
        botons[1].transform.position=responsesPos[listNumbers[1]-1].transform.position;
        botons[2].transform.position=responsesPos[listNumbers[2]-1].transform.position;
        botons[3].transform.position=responsesPos[listNumbers[3]-1].transform.position;  
    }
}











