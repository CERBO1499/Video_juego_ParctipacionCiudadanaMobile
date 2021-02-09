using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] GameObject peloBlock;
    [SerializeField] GameObject accesorio;
    [SerializeField] GameObject accesorioBlock;
    [SerializeField] GameObject cara;
    [SerializeField] GameObject caraBlock;
    [SerializeField] GameObject camisa;
    [SerializeField] GameObject camisaBlock;
    [SerializeField] GameObject pantalon;
    [SerializeField] GameObject pantalonBlock;
    [SerializeField] GameObject zapato;
    [SerializeField] GameObject zapatoBlock;
    [Space]
    [SerializeField] Personalization.Restrictions restrictions;


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
        int numeroPelo = Enumerable.Range(0, pelucas.Count).First(pelo => pelucas[pelo].activeSelf);

        if (numeroPelo + 1 < pelucas.Count)
        {
            pelucas[numeroPelo].SetActive(false);

            numeroPelo++;

            pelucas[numeroPelo].SetActive(true);

            NumeroPeloM = (numeroPelo < 3) ? numeroPelo : ((restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.pelo, numeroPelo]) ? numeroPelo : NumeroPeloM);

            if (NumeroPeloM != numeroPelo)
                peloBlock.SetActive(true);
            else
                peloBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            pelucas[numeroPelo].SetActive(false);

            numeroPelo = 0;

            pelucas[numeroPelo].SetActive(true);

            NumeroPeloM = numeroPelo;

            if (peloBlock.activeSelf)
                peloBlock.SetActive(false);
        }
    }
    public void PrevHair()
    {
        int numeroPelo = Enumerable.Range(0, pelucas.Count).First(pelo => pelucas[pelo].activeSelf);

        if (numeroPelo - 1 > -1)
        {
            pelucas[numeroPelo].SetActive(false);

            numeroPelo--;

            pelucas[numeroPelo].SetActive(true);

            NumeroPeloM = (numeroPelo < 3) ? numeroPelo : ((restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.pelo, numeroPelo]) ? numeroPelo : NumeroPeloM);

            if (NumeroPeloM != numeroPelo)
                peloBlock.SetActive(true);
            else
                peloBlock.SetActive(false);

        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            pelucas[numeroPelo].SetActive(false);

            pelucas[pelucas.Count - 1].SetActive(true);

            if (restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.pelo, numeroPelo])
                NumeroPeloM = pelucas.Count - 1;
            else
                peloBlock.SetActive(true);
        }
    }

    public void NextAccesorio()
    {
        int numeroAccesorio = Enumerable.Range(0, accesorios.Count).First(accersorio => accesorios[accersorio].activeSelf);

        if (numeroAccesorio + 1 < accesorios.Count)
        {
            accesorios[numeroAccesorio].SetActive(false);

            numeroAccesorio++;

            accesorios[numeroAccesorio].SetActive(true);

            NumeroAccesorioM = (numeroAccesorio < 3) ? numeroAccesorio : ((restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.accesorios, numeroAccesorio]) ? numeroAccesorio : NumeroAccesorioM);

            if (NumeroAccesorioM != numeroAccesorio)
                accesorioBlock.SetActive(true);
            else
                accesorioBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            accesorios[numeroAccesorio].SetActive(false);

            numeroAccesorio = 0;

            accesorios[numeroAccesorio].SetActive(true);

            NumeroAccesorioM = numeroAccesorio;

            if (accesorioBlock.activeSelf)
                accesorioBlock.SetActive(false);
        }
    }
    public void PrevAccesorio()
    {
        int numeroAccesorio = Enumerable.Range(0, accesorios.Count).First(accersorio => accesorios[accersorio].activeSelf);

        if (numeroAccesorio - 1 > -1)
        {
            accesorios[numeroAccesorio].SetActive(false);

            numeroAccesorio--;

            accesorios[numeroAccesorio].SetActive(true);

            NumeroAccesorioM = (numeroAccesorio < 3) ? numeroAccesorio : ((restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.accesorios, numeroAccesorio]) ? numeroAccesorio : NumeroAccesorioM);

            if (NumeroAccesorioM != numeroAccesorio)
                accesorioBlock.SetActive(true);
            else
                accesorioBlock.SetActive(false);

        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            accesorios[numeroAccesorio].SetActive(false);

            accesorios[accesorios.Count - 1].SetActive(true);

            if (restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.accesorios, numeroAccesorio])
                NumeroAccesorioM = accesorios.Count - 1;
            else
                accesorioBlock.SetActive(true);
        }
    }

    public void NextFace()
    {
        int numeroCara = Enumerable.Range(0, caras.Count).First(cara => caras[cara].activeSelf);

        if (numeroCara + 1 < caras.Count)
        {
            caras[numeroCara].SetActive(false);

            numeroCara++;

            caras[numeroCara].SetActive(true);

            NumeroCaraM = (numeroCara < 3) ? numeroCara : ((restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.cara, numeroCara]) ? numeroCara : NumeroCaraM);

            if (NumeroCaraM != numeroCara)
                caraBlock.SetActive(true);
            else
                caraBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            caras[numeroCara].SetActive(false);

            numeroCara = 0;

            caras[numeroCara].SetActive(true);

            NumeroCaraM = numeroCara;

            if (caraBlock.activeSelf)
                caraBlock.SetActive(false);
        }
    }
    public void PrevFace()
    {
        int numeroCara = Enumerable.Range(0, caras.Count).First(cara => caras[cara].activeSelf);

        if (numeroCara - 1 > -1)
        {
            caras[numeroCara].SetActive(false);

            numeroCara--;

            caras[numeroCara].SetActive(true);

            NumeroCaraM = (numeroCara < 3) ? numeroCara : ((restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.cara, numeroCara]) ? numeroCara : NumeroCaraM);

            if (NumeroCaraM != numeroCara)
                caraBlock.SetActive(true);
            else
                caraBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            caras[numeroCara].SetActive(false);

            caras[caras.Count - 1].SetActive(true);

            if (restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.cara, numeroCara])
                NumeroCaraM = caras.Count - 1;
            else
                caraBlock.SetActive(true);
        }
    }

    public void NextCamisa()
    {
        int numeroCamisa = Enumerable.Range(0, camisas.Count).First(camisa => camisas[camisa].activeSelf);

        if (numeroCamisa + 1 < camisas.Count)
        {
            camisas[numeroCamisa].SetActive(false);

            numeroCamisa++;

            camisas[numeroCamisa].SetActive(true);

            NumeroCamisaM = (numeroCamisa < 3) ? numeroCamisa : ((restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.camisa, numeroCamisa]) ? numeroCamisa : NumeroCamisaM);

            if (NumeroCamisaM != numeroCamisa)
                camisaBlock.SetActive(true);
            else
                camisaBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            camisas[numeroCamisa].SetActive(false);

            numeroCamisa = 0;

            camisas[numeroCamisa].SetActive(true);

            NumeroCamisaM = numeroCamisa;

            if (camisaBlock.activeSelf)
                camisaBlock.SetActive(false);
        }
    }

    public void PrevCamisa()
    {
        int numeroCamisa = Enumerable.Range(0, camisas.Count).First(camisa => camisas[camisa].activeSelf);

        if (numeroCamisa - 1 > -1)
        {
            camisas[numeroCamisa].SetActive(false);

            numeroCamisa--;

            camisas[numeroCamisa].SetActive(true);

            NumeroCamisaM = (numeroCamisa < 3) ? numeroCamisa : ((restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.camisa, numeroCamisa]) ? numeroCamisa : NumeroCamisaM);

            if (NumeroCamisaM != numeroCamisa)
                camisaBlock.SetActive(true);
            else
                camisaBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            camisas[numeroCamisa].SetActive(false);

            camisas[camisas.Count - 1].SetActive(true);

            if (restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.camisa, numeroCamisa])
                NumeroCamisaM = camisas.Count - 1;
            else
                camisaBlock.SetActive(true);
        }
    }
    public void NextPantalon()
    {
        int numeroPantalon = Enumerable.Range(0, pantalones.Count).First(pantalon => pantalones[pantalon].activeSelf);

        if (numeroPantalon + 1 < pantalones.Count)
        {
            pantalones[numeroPantalon].SetActive(false);

            numeroPantalon++;

            pantalones[numeroPantalon].SetActive(true);

            NumeroPantalonM = (numeroPantalon < 3) ? numeroPantalon : ((restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.pantalon, numeroPantalon]) ? numeroPantalon : NumeroPantalonM);

            if (NumeroPantalonM != numeroPantalon)
                pantalonBlock.SetActive(true);
            else
                pantalonBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            pantalones[numeroPantalon].SetActive(false);

            numeroPantalon = 0;

            pantalones[numeroPantalon].SetActive(true);

            NumeroPantalonM = numeroPantalon;

            if (pantalonBlock.activeSelf)
                pantalonBlock.SetActive(false);
        }
    }
    public void PrevPantalon()
    {
        int numeroPantalon = Enumerable.Range(0, pantalones.Count).First(pantalon => pantalones[pantalon].activeSelf);

        if (numeroPantalon - 1 > -1)
        {
            pantalones[numeroPantalon].SetActive(false);

            numeroPantalon--;

            pantalones[numeroPantalon].SetActive(true);

            NumeroPantalonM = (numeroPantalon < 3) ? numeroPantalon : ((restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.pantalon, numeroPantalon]) ? numeroPantalon : NumeroPantalonM);

            if (NumeroPantalonM != numeroPantalon)
                pantalonBlock.SetActive(true);
            else
                pantalonBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            pantalones[numeroPantalon].SetActive(false);

            pantalones[pantalones.Count - 1].SetActive(true);

            if (restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.pantalon, numeroPantalon])
                NumeroPantalonM = pantalones.Count - 1;
            else
                pantalonBlock.SetActive(true);
        }
    }
    public void NextZapato()
    {
        int numeroZapato = Enumerable.Range(0, zapatos.Count).First(zapato => zapatos[zapato].activeSelf);

        if (numeroZapato + 1 < zapatos.Count)
        {
            zapatos[numeroZapato].SetActive(false);

            numeroZapato++;

            zapatos[numeroZapato].SetActive(true);

            NumeroZapatoM = (numeroZapato < 3) ? numeroZapato : ((restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.zapatos, numeroZapato]) ? numeroZapato : NumeroZapatoM);

            if (NumeroZapatoM != numeroZapato)
                zapatoBlock.SetActive(true);
            else
                zapatoBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            zapatos[numeroZapato].SetActive(false);

            numeroZapato = 0;

            zapatos[numeroZapato].SetActive(true);

            NumeroZapatoM = numeroZapato;

            if (zapatoBlock.activeSelf)
                zapatoBlock.SetActive(false);
        }
    }
    public void PrevZapato()
    {
        int numeroZapato = Enumerable.Range(0, zapatos.Count).First(zapato => zapatos[zapato].activeSelf);

        if (numeroZapato - 1 > -1)
        {
            zapatos[numeroZapato].SetActive(false);

            numeroZapato--;

            zapatos[numeroZapato].SetActive(true);

            NumeroZapatoM = (numeroZapato < 3) ? numeroZapato : ((restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.zapatos, numeroZapato]) ? numeroZapato : NumeroZapatoM);

            if (NumeroZapatoM != numeroZapato)
                zapatoBlock.SetActive(true);
            else
                zapatoBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            zapatos[numeroZapato].SetActive(false);

            zapatos[zapatos.Count - 1].SetActive(true);

            if (restrictions[Personalization.Restrictions.Sex.female, Personalization.Restrictions.Type.zapatos, numeroZapato])
                NumeroZapatoM = zapatos.Count - 1;
            else
                zapatoBlock.SetActive(true);
        }
    }
}