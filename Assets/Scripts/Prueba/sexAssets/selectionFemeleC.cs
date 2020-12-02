using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionFemeleC : MonoBehaviour
{
    //una lista de objetos que tiene ... una lista por cada grupo de ellos
    //todos apagados en el star menos 1

    // estaticos de elementos para que sea coherente el actual aca con el de la escena main
    static int numeroPeloM = 0;
    static int numeroAccesorioM = 0;
    static int numeroCaraM = 0;
    static int numeroCamisaM = 0;
    static int numeroPantalonM = 0;
    static int numeroZapatoM = 0;
    //

    [SerializeField] GameObject pelo; //el papa de los objetos disponibles de esta categoria
    [SerializeField] GameObject accesorio;
    [SerializeField] GameObject cara;
    [SerializeField] GameObject camisa;
    [SerializeField] GameObject pantalon;
    [SerializeField] GameObject zapato;


    List<GameObject> pelucas = new List<GameObject>();
    List<GameObject> accesorios = new List<GameObject>();
    List<GameObject> caras = new List<GameObject>();
    List<GameObject> camisas = new List<GameObject>();
    List<GameObject> pantalones = new List<GameObject>();
    List<GameObject> zapatos = new List<GameObject>();

    public static int NumeroPeloM { get => numeroPeloM; set => numeroPeloM = value; }
    public static int NumeroAccesorioM { get => numeroAccesorioM; set => numeroAccesorioM = value; }
    public static int NumeroCaraM { get => numeroCaraM; set => numeroCaraM = value; }
    public static int NumeroCamisaM { get => numeroCamisaM; set => numeroCamisaM = value; }
    public static int NumeroPantalonM { get => numeroPantalonM; set => numeroPantalonM = value; }
    public static int NumeroZapatoM { get => numeroZapatoM; set => numeroZapatoM = value; }

    //Nota: el orden que tienen los objetos en esta escena (editor) lo debe tener el personaje en la escena main

    private void Awake()
    {
        foreach (Transform child in pelo.transform)
        {
            pelucas.Add(child.gameObject);
        }
        foreach (Transform child in accesorio.transform)
        {
            accesorios.Add(child.gameObject);
        }
        foreach (Transform child in cara.transform)
        {
            caras.Add(child.gameObject);
        }
        foreach (Transform child in camisa.transform)
        {
            camisas.Add(child.gameObject);
        }
        foreach (Transform child in pantalon.transform)
        {
            pantalones.Add(child.gameObject);
        }
        foreach (Transform child in zapato.transform)
        {
            zapatos.Add(child.gameObject);
        }

    }

    private void Start()
    {
        for (int i = 0; i < pelucas.Count; i++)  //para dejar solo prendido el gameObject que tenga el numero del static     --- pelucas
        {
            if (i != NumeroPeloM)
            {
                pelucas[i].SetActive(false);
            }
            else
            {
                pelucas[i].SetActive(true);
            }
        }
        for (int i = 0; i < accesorios.Count; i++)  //para dejar solo prendido el gameObject que tenga el numero del static     --- accesorios
        {
            if (i != NumeroAccesorioM)
            {
                accesorios[i].SetActive(false);
            }
            else
            {
                accesorios[i].SetActive(true);
            }
        }
        for (int i = 0; i < caras.Count; i++)  //para dejar solo prendido el gameObject que tenga el numero del static        --- caras
        {
            if (i != NumeroCaraM)
            {
                caras[i].SetActive(false);
            }
            else
            {
                caras[i].SetActive(true);
            }
        }
        for (int i = 0; i < camisas.Count; i++)  //para dejar solo prendido el gameObject que tenga el numero del static     --- camisas
        {
            if (i != NumeroCamisaM)
            {
                camisas[i].SetActive(false);
            }
            else
            {
                camisas[i].SetActive(true);
            }
        }
        for (int i = 0; i < pantalones.Count; i++)  //para dejar solo prendido el gameObject que tenga el numero del static     --- pantalones
        {
            if (i != NumeroPantalonM)
            {
                pantalones[i].SetActive(false);
            }
            else
            {
                pantalones[i].SetActive(true);
            }
        }
        for (int i = 0; i < zapatos.Count; i++)  //para dejar solo prendido el gameObject que tenga el numero del static     --- zapatos
        {
            if (i != NumeroZapatoM)
            {
                zapatos[i].SetActive(false);
            }
            else
            {
                zapatos[i].SetActive(true);
            }
        }

    }


    public void NextHair()
    {
        if (NumeroPeloM + 1 != pelucas.Count)
        {
            pelucas[NumeroPeloM + 1].SetActive(true);
            pelucas[NumeroPeloM].SetActive(false);
            NumeroPeloM++;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            pelucas[0].SetActive(true);
            pelucas[NumeroPeloM].SetActive(false);
            NumeroPeloM = 0;
        }
    }
    public void PrevHair()
    {
        if (NumeroPeloM - 1 != -1)
        {

            pelucas[NumeroPeloM - 1].SetActive(true);
            pelucas[NumeroPeloM].SetActive(false);
            NumeroPeloM--;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            pelucas[pelucas.Count - 1].SetActive(true);
            pelucas[NumeroPeloM].SetActive(false);
            NumeroPeloM = pelucas.Count - 1;
        }
    }

    public void NextAccesorio()
    {
        if (NumeroAccesorioM + 1 != accesorios.Count)
        {

            accesorios[NumeroAccesorioM + 1].SetActive(true);
            accesorios[NumeroAccesorioM].SetActive(false);
            NumeroAccesorioM++;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            accesorios[0].SetActive(true);
            accesorios[NumeroAccesorioM].SetActive(false);
            NumeroAccesorioM = 0;
        }
    }
    public void PrevAccesorio()
    {
        if (NumeroAccesorioM - 1 != -1)
        {

            accesorios[NumeroAccesorioM - 1].SetActive(true);
            accesorios[NumeroAccesorioM].SetActive(false);
            NumeroAccesorioM--;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            accesorios[accesorios.Count - 1].SetActive(true);
            accesorios[NumeroAccesorioM].SetActive(false);
            NumeroAccesorioM = accesorios.Count - 1;
        }
    }

    public void NextFace()
    {
        if (NumeroCaraM + 1 != caras.Count)
        {

            caras[NumeroCaraM + 1].SetActive(true);
            caras[NumeroCaraM].SetActive(false);
            NumeroCaraM++;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            caras[0].SetActive(true);
            caras[NumeroCaraM].SetActive(false);
            NumeroCaraM = 0;
        }
    }
    public void PrevFace()
    {
        if (NumeroCaraM - 1 != -1)
        {
            caras[NumeroCaraM - 1].SetActive(true);
            caras[NumeroCaraM].SetActive(false);
            NumeroCaraM--;
        }
        else
        {
            caras[caras.Count - 1].SetActive(true);
            caras[NumeroCaraM].SetActive(false);
            NumeroCaraM = caras.Count - 1;
        }
    }

    public void NextCamisa()
    {
        if (NumeroCamisaM + 1 != camisas.Count)
        {

            camisas[NumeroCamisaM + 1].SetActive(true);
            camisas[NumeroCamisaM].SetActive(false);
            NumeroCamisaM++;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            camisas[0].SetActive(true);
            camisas[NumeroCamisaM].SetActive(false);
            NumeroCamisaM = 0;
        }
    }

    public void PrevCamisa()
    {
        if (NumeroCamisaM - 1 != -1)
        {
            camisas[NumeroCamisaM - 1].SetActive(true);
            camisas[NumeroCamisaM].SetActive(false);
            NumeroCamisaM--;
        }
        else
        {
            camisas[camisas.Count - 1].SetActive(true);
            camisas[NumeroCamisaM].SetActive(false);
            NumeroCamisaM = camisas.Count - 1;
        }
    }
    public void NextPantalon()
    {
        if (NumeroPantalonM + 1 != pantalones.Count)
        {

            pantalones[NumeroPantalonM + 1].SetActive(true);
            pantalones[NumeroPantalonM].SetActive(false);
            NumeroPantalonM++;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            pantalones[0].SetActive(true);
            pantalones[NumeroPantalonM].SetActive(false);
            NumeroPantalonM = 0;
        }
    }
    public void PrevPantalon()
    {
        if (NumeroPantalonM - 1 != -1)
        {
            pantalones[NumeroPantalonM - 1].SetActive(true);
            pantalones[NumeroPantalonM].SetActive(false);
            NumeroPantalonM--;
        }
        else
        {
            pantalones[pantalones.Count - 1].SetActive(true);
            pantalones[NumeroPantalonM].SetActive(false);
            NumeroPantalonM = pantalones.Count - 1;
        }
    }
    public void NextZapato()
    {
        if (NumeroZapatoM + 1 != zapatos.Count)
        {

            zapatos[NumeroZapatoM + 1].SetActive(true);
            zapatos[NumeroZapatoM].SetActive(false);
            NumeroZapatoM++;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            zapatos[0].SetActive(true);
            zapatos[NumeroZapatoM].SetActive(false);
            NumeroZapatoM = 0;
        }
    }
    public void PrevZapato()
    {
        if (NumeroZapatoM - 1 != -1)
        {
            zapatos[NumeroZapatoM - 1].SetActive(true);
            zapatos[NumeroZapatoM].SetActive(false);
            NumeroZapatoM--;
        }
        else
        {
            zapatos[zapatos.Count - 1].SetActive(true);
            zapatos[NumeroZapatoM].SetActive(false);
            NumeroZapatoM = zapatos.Count - 1;
        }
    }

}
