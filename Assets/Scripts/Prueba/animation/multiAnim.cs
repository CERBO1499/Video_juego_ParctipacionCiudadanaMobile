using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiAnim : MonoBehaviour
{

    List<GameObject> children = new List<GameObject>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
    }
    void Start()
    {
       for (float i = 0; i < children.Count; i++)
        {
            float desfasee = 0;
            if (i != 0) desfasee = i / children.Count;
             else desfasee = 0;

            children[(int)i].GetComponent<Animator>().SetFloat(Animator.StringToHash("desfase"),desfasee);
           // print("Hijo numero: "+i+" desfase="+desfasee);
        }
    }


}
