using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectionCharacter : MonoBehaviour
{
    //una lista de objetos que tiene ... una lista por cada grupo de ellos
    //todos apagados en el star menos 1

    // estaticos de elementos para que sea coherente el actual aca con el de la escena main
    static int numeroPelo = 0;
    static int numeroAccesorio = 0;
    static int numeroCara = 0;
    static int numeroCamisa = 0;
    static int numeroPantalon = 0;
    static int numeroZapato = 0;
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

    public static int NumeroPelo { get => numeroPelo; set => numeroPelo = value; }
    public static int NumeroAccesorio { get => numeroAccesorio; set => numeroAccesorio = value; }
    public static int NumeroCara { get => numeroCara; set => numeroCara = value; }
    public static int NumeroCamisa { get => numeroCamisa; set => numeroCamisa = value; }
    public static int NumeroPantalon { get => numeroPantalon; set => numeroPantalon = value; }
    public static int NumeroZapato { get => numeroZapato; set => numeroZapato = value; }

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
        //ya guardamos aca arriba todos los hijos de los papas en las listas de ellos mismos, entonces tenemos el count

    }

    private void Start()
    {
        for (int i = 0; i < pelucas.Count; i++)  //para dejar solo prendido el gameObject que tenga el numero del static     --- pelucas
        {
            if (i != NumeroPelo)
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
            if (i != NumeroAccesorio)
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
            if (i != NumeroCara)
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
            if (i != NumeroCamisa)
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
            if (i != NumeroPantalon)
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
            if (i != NumeroZapato)
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
        if (NumeroPelo + 1 != pelucas.Count)
        {

            pelucas[NumeroPelo + 1].SetActive(true);
            pelucas[NumeroPelo].SetActive(false);
            NumeroPelo++;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            pelucas[0].SetActive(true);
            pelucas[NumeroPelo].SetActive(false);
            NumeroPelo = 0;
        }
    }
    public void PrevHair()
    {
        if (NumeroPelo - 1 != -1)
        {

            pelucas[NumeroPelo - 1].SetActive(true);
            pelucas[NumeroPelo].SetActive(false);
            NumeroPelo--;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            pelucas[pelucas.Count - 1].SetActive(true);
            pelucas[NumeroPelo].SetActive(false);
            NumeroPelo = pelucas.Count - 1;
        }
    }

    public void NextAccesorio()
    {
        if (NumeroAccesorio + 1 != accesorios.Count)
        {

            accesorios[NumeroAccesorio + 1].SetActive(true);
            accesorios[NumeroAccesorio].SetActive(false);
            NumeroAccesorio++;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            accesorios[0].SetActive(true);
            accesorios[NumeroAccesorio].SetActive(false);
            NumeroAccesorio = 0;
        }
    }
    public void PrevAccesorio()
    {
        if (NumeroAccesorio - 1 != -1)
        {

            accesorios[NumeroAccesorio - 1].SetActive(true);
            accesorios[NumeroAccesorio].SetActive(false);
            NumeroAccesorio--;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            accesorios[accesorios.Count - 1].SetActive(true);
            accesorios[NumeroAccesorio].SetActive(false);
            NumeroAccesorio = accesorios.Count - 1;
        }
    }

    public void NextFace()
    {
        if (NumeroCara + 1 != caras.Count)
        {

            caras[NumeroCara + 1].SetActive(true);
            caras[NumeroCara].SetActive(false);
            NumeroCara++;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            caras[0].SetActive(true);
            caras[NumeroCara].SetActive(false);
            NumeroCara = 0;
        }
    }
    public void PrevFace()
    {
        if (NumeroCara - 1 != -1)
        {
            caras[NumeroCara - 1].SetActive(true);
            caras[NumeroCara].SetActive(false);
            NumeroCara--;
        }
        else
        {
            caras[caras.Count - 1].SetActive(true);
            caras[NumeroCara].SetActive(false);
            NumeroCara = caras.Count - 1;
        }
    }

    public void NextCamisa()
    {
        if (NumeroCamisa + 1 != camisas.Count)
        {

            camisas[NumeroCamisa + 1].SetActive(true);
            camisas[NumeroCamisa].SetActive(false);
            NumeroCamisa++;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            camisas[0].SetActive(true);
            camisas[NumeroCamisa].SetActive(false);
            NumeroCamisa = 0;
        }
    }

    public void PrevCamisa()
    {
        if (NumeroCamisa - 1 != -1)
        {
            camisas[NumeroCamisa - 1].SetActive(true);
            camisas[NumeroCamisa].SetActive(false);
            NumeroCamisa--;
        }
        else
        {
            camisas[camisas.Count - 1].SetActive(true);
            camisas[NumeroCamisa].SetActive(false);
            NumeroCamisa = camisas.Count - 1;
        }
    }
    public void NextPantalon()
    {
        if (NumeroPantalon + 1 != pantalones.Count)
        {

            pantalones[NumeroPantalon + 1].SetActive(true);
            pantalones[NumeroPantalon].SetActive(false);
            NumeroPantalon++;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            pantalones[0].SetActive(true);
            pantalones[NumeroPantalon].SetActive(false);
            NumeroPantalon = 0;
        }
    }
    public void PrevPantalon()
    {
        if (NumeroPantalon - 1 != -1)
        {
            pantalones[NumeroPantalon - 1].SetActive(true);
            pantalones[NumeroPantalon].SetActive(false);
            NumeroPantalon--;
        }
        else
        {
            pantalones[pantalones.Count - 1].SetActive(true);
            pantalones[NumeroPantalon].SetActive(false);
            NumeroPantalon = pantalones.Count - 1;
        }
    }
    public void NextZapato()
    {
        if (NumeroZapato + 1 != zapatos.Count)
        {

            zapatos[NumeroZapato + 1].SetActive(true);
            zapatos[NumeroZapato].SetActive(false);
            NumeroZapato++;
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            zapatos[0].SetActive(true);
            zapatos[NumeroZapato].SetActive(false);
            NumeroZapato = 0;
        }
    }
    public void PrevZapato()
    {
        if (NumeroZapato - 1 != -1)
        {
            zapatos[NumeroZapato - 1].SetActive(true);
            zapatos[NumeroZapato].SetActive(false);
            NumeroZapato--;
        }
        else
        {
            zapatos[zapatos.Count - 1].SetActive(true);
            zapatos[NumeroZapato].SetActive(false);
            NumeroZapato = zapatos.Count - 1;
        }
    }




}
