using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lecturaChoseM : MonoBehaviour
{
    List<GameObject> children = new List<GameObject>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
    }
    private void Start()
    {
        if (gameObject.name == "pelos")
        {
            for (int i = 0; i < children.Count; i++)            // para pelo
            {
                if (i != selectionFemeleC.NumeroPeloM)
                {
                    children[i].SetActive(false);
                }
                else
                {
                    children[i].SetActive(true);
                }
            }
        }
        if (gameObject.name == "accesorios")
        {
            for (int i = 0; i < children.Count; i++)            // para accesorio
            {
                if (i != selectionFemeleC.NumeroAccesorioM)
                {
                    children[i].SetActive(false);
                }
                else
                {
                    children[i].SetActive(true);
                }
            }
        }
        if (gameObject.name == "caras")
        {
            for (int i = 0; i < children.Count; i++)            // para caras
            {
                if (i != selectionFemeleC.NumeroCaraM)
                {
                    children[i].SetActive(false);
                }
                else
                {
                    children[i].SetActive(true);
                }
            }
        }
        if (gameObject.name == "camisas")
        {
            for (int i = 0; i < children.Count; i++)            // para camisas
            {
                if (i != selectionFemeleC.NumeroCamisaM)
                {
                    children[i].SetActive(false);
                }
                else
                {
                    children[i].SetActive(true);
                }
            }
        }
        if (gameObject.name == "pantalones")
        {
            for (int i = 0; i < children.Count; i++)            // para pantalones
            {
                if (i != selectionFemeleC.NumeroPantalonM)
                {
                    children[i].SetActive(false);
                }
                else
                {
                    children[i].SetActive(true);
                }
            }
        }
        if (gameObject.name == "zapatos")
        {
            for (int i = 0; i < children.Count; i++)            // para zapatos
            {
                if (i != selectionFemeleC.NumeroZapatoM)
                {
                    children[i].SetActive(false);
                }
                else
                {
                    children[i].SetActive(true);
                }
            }
        }


    }

}
