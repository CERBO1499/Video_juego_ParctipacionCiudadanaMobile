using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textActivator : MonoBehaviour
{
    List<GameObject> children = new List<GameObject>(); //lista de los hijos para luego elegir uno de ellos aleatoriamente u ordenados... activarlo
    short actualChildren;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
    }

    private void Start()
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].SetActive(false);       //desactive todos los hijos
        }
    }
}
