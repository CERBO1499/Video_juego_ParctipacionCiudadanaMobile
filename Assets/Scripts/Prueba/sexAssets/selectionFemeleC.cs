using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

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
    [SerializeField] GameObject semillasPanel;
    [SerializeField] TMPro.TextMeshProUGUI semillasTxt;
    [SerializeField] TMPro.TextMeshProUGUI totalSemillasTxt;
    [SerializeField] UnityEngine.UI.Button buyBtn;
    [SerializeField] UnityEngine.UI.Button closeBtn;
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
        if (JsonContainer.instance.Pcharacter.IdPersonaje == "")
        {
            peloBlock.GetComponent<RectTransform>().GetChild(0).gameObject.SetActive(false);
            accesorioBlock.GetComponent<RectTransform>().GetChild(0).gameObject.SetActive(false);
            caraBlock.GetComponent<RectTransform>().GetChild(0).gameObject.SetActive(false);
            camisaBlock.GetComponent<RectTransform>().GetChild(0).gameObject.SetActive(false);
            pantalonBlock.GetComponent<RectTransform>().GetChild(0).gameObject.SetActive(false);
            zapatoBlock.GetComponent<RectTransform>().GetChild(0).gameObject.SetActive(false);
        }

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

    public IEnumerator GetInventoryCoroutine(System.Action output = null)
    {
        UnityWebRequest request = new UnityWebRequest("https://www.polygon.us/apiEscuelaspp/public/Tienda/" + JsonContainer.instance.Pcharacter.IdUsuaio, "Get");

        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError)
            Debug.Log(request.error);
        else
        {
            Debug.Log("Get inventory: " + request.responseCode);

            if (PlayerPrefs.HasKey("internal_buy"))
                restrictions.items = JsonConvert.DeserializeObject<Personalization.Items>(request.downloadHandler.text);
        }

        output?.Invoke();
    }

    public void NextHair()
    {
        int numeroPelo = Enumerable.Range(0, pelucas.Count).First(pelo => pelucas[pelo].activeSelf);

        if (numeroPelo + 1 < pelucas.Count)
        {
            pelucas[numeroPelo].SetActive(false);

            numeroPelo++;

            pelucas[numeroPelo].SetActive(true);

            NumeroPeloM = (numeroPelo < 3) ? numeroPelo : ((restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.pelo, numeroPelo]) ? numeroPelo : NumeroPeloM);

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

            NumeroPeloM = (numeroPelo < 3) ? numeroPelo : ((restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.pelo, numeroPelo]) ? numeroPelo : NumeroPeloM);

            if (NumeroPeloM != numeroPelo)
                peloBlock.SetActive(true);
            else
                peloBlock.SetActive(false);

        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            pelucas[numeroPelo].SetActive(false);

            pelucas[pelucas.Count - 1].SetActive(true);

            if (restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.pelo, numeroPelo])
                NumeroPeloM = pelucas.Count - 1;
            else
                peloBlock.SetActive(true);
        }
    }

    public void BuyHair()
    {
        semillasTxt.text = JsonContainer.instance.Pcharacter.Semillas;

        totalSemillasTxt.text = (int.Parse(JsonContainer.instance.Pcharacter.Semillas) - selectionCharacter.price).ToString();

        buyBtn.interactable = true;

        if (int.Parse(totalSemillasTxt.text) <= 0)
        {
            buyBtn.interactable = false;

            totalSemillasTxt.color = Color.red;
        }
        else
            selectionCharacter.buy = () =>
            {
                buyBtn.interactable = false;

                closeBtn.interactable = false;

                StartCoroutine(BoyBuyCoroutine(peloBlock, "pelo", Enumerable.Range(0, pelucas.Count).First(pelo => pelucas[pelo].activeSelf)));
            };

        semillasPanel.SetActive(true);
    }

    public void NextAccesorio()
    {
        int numeroAccesorio = Enumerable.Range(0, accesorios.Count).First(accersorio => accesorios[accersorio].activeSelf);

        if (numeroAccesorio + 1 < accesorios.Count)
        {
            accesorios[numeroAccesorio].SetActive(false);

            numeroAccesorio++;

            accesorios[numeroAccesorio].SetActive(true);

            NumeroAccesorioM = (numeroAccesorio < 3) ? numeroAccesorio : ((restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.accesorios, numeroAccesorio]) ? numeroAccesorio : NumeroAccesorioM);

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

            NumeroAccesorioM = (numeroAccesorio < 3) ? numeroAccesorio : ((restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.accesorios, numeroAccesorio]) ? numeroAccesorio : NumeroAccesorioM);

            if (NumeroAccesorioM != numeroAccesorio)
                accesorioBlock.SetActive(true);
            else
                accesorioBlock.SetActive(false);

        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            accesorios[numeroAccesorio].SetActive(false);

            accesorios[accesorios.Count - 1].SetActive(true);

            if (restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.accesorios, numeroAccesorio])
                NumeroAccesorioM = accesorios.Count - 1;
            else
                accesorioBlock.SetActive(true);
        }
    }

    public void BuyAccesorio()
    {
        semillasTxt.text = JsonContainer.instance.Pcharacter.Semillas;

        totalSemillasTxt.text = (int.Parse(JsonContainer.instance.Pcharacter.Semillas) - selectionCharacter.price).ToString();

        buyBtn.interactable = true;

        if (int.Parse(totalSemillasTxt.text) <= 0)
        {
            buyBtn.interactable = false;

            totalSemillasTxt.color = Color.red;
        }
        else
            selectionCharacter.buy = () =>
            {
                buyBtn.interactable = false;

                closeBtn.interactable = false;

                StartCoroutine(BoyBuyCoroutine(accesorioBlock, "accesorios", Enumerable.Range(0, accesorios.Count).First(accesorio => accesorios[accesorio].activeSelf)));
            };

        semillasPanel.SetActive(true);
    }

    public void NextFace()
    {
        int numeroCara = Enumerable.Range(0, caras.Count).First(cara => caras[cara].activeSelf);

        if (numeroCara + 1 < caras.Count)
        {
            caras[numeroCara].SetActive(false);

            numeroCara++;

            caras[numeroCara].SetActive(true);

            NumeroCaraM = (numeroCara < 3) ? numeroCara : ((restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.cara, numeroCara]) ? numeroCara : NumeroCaraM);

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

            NumeroCaraM = (numeroCara < 3) ? numeroCara : ((restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.cara, numeroCara]) ? numeroCara : NumeroCaraM);

            if (NumeroCaraM != numeroCara)
                caraBlock.SetActive(true);
            else
                caraBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            caras[numeroCara].SetActive(false);

            caras[caras.Count - 1].SetActive(true);

            if (restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.cara, numeroCara])
                NumeroCaraM = caras.Count - 1;
            else
                caraBlock.SetActive(true);
        }
    }

    public void BuyCara()
    {
        semillasTxt.text = JsonContainer.instance.Pcharacter.Semillas;

        totalSemillasTxt.text = (int.Parse(JsonContainer.instance.Pcharacter.Semillas) - selectionCharacter.price).ToString();

        buyBtn.interactable = true;

        if (int.Parse(totalSemillasTxt.text) <= 0)
        {
            buyBtn.interactable = false;

            totalSemillasTxt.color = Color.red;
        }
        else
            selectionCharacter.buy = () =>
            {
                buyBtn.interactable = false;

                closeBtn.interactable = false;

                StartCoroutine(BoyBuyCoroutine(caraBlock, "cara", Enumerable.Range(0, caras.Count).First(cara => caras[cara].activeSelf)));
            };

        semillasPanel.SetActive(true);
    }

    public void NextCamisa()
    {
        int numeroCamisa = Enumerable.Range(0, camisas.Count).First(camisa => camisas[camisa].activeSelf);

        if (numeroCamisa + 1 < camisas.Count)
        {
            camisas[numeroCamisa].SetActive(false);

            numeroCamisa++;

            camisas[numeroCamisa].SetActive(true);

            NumeroCamisaM = (numeroCamisa < 3) ? numeroCamisa : ((restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.camisa, numeroCamisa]) ? numeroCamisa : NumeroCamisaM);

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

            NumeroCamisaM = (numeroCamisa < 3) ? numeroCamisa : ((restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.camisa, numeroCamisa]) ? numeroCamisa : NumeroCamisaM);

            if (NumeroCamisaM != numeroCamisa)
                camisaBlock.SetActive(true);
            else
                camisaBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            camisas[numeroCamisa].SetActive(false);

            camisas[camisas.Count - 1].SetActive(true);

            if (restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.camisa, numeroCamisa])
                NumeroCamisaM = camisas.Count - 1;
            else
                camisaBlock.SetActive(true);
        }
    }

    public void BuyCamisa()
    {
        semillasTxt.text = JsonContainer.instance.Pcharacter.Semillas;

        totalSemillasTxt.text = (int.Parse(JsonContainer.instance.Pcharacter.Semillas) - selectionCharacter.price).ToString();

        buyBtn.interactable = true;

        if (int.Parse(totalSemillasTxt.text) <= 0)
        {
            buyBtn.interactable = false;

            totalSemillasTxt.color = Color.red;
        }
        else
            selectionCharacter.buy = () =>
            {
                buyBtn.interactable = false;

                closeBtn.interactable = false;

                StartCoroutine(BoyBuyCoroutine(camisaBlock, "camisa", Enumerable.Range(0, camisas.Count).First(camisa => camisas[camisa].activeSelf)));
            };

        semillasPanel.SetActive(true);
    }

    public void NextPantalon()
    {
        int numeroPantalon = Enumerable.Range(0, pantalones.Count).First(pantalon => pantalones[pantalon].activeSelf);

        if (numeroPantalon + 1 < pantalones.Count)
        {
            pantalones[numeroPantalon].SetActive(false);

            numeroPantalon++;

            pantalones[numeroPantalon].SetActive(true);

            NumeroPantalonM = (numeroPantalon < 3) ? numeroPantalon : ((restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.pantalon, numeroPantalon]) ? numeroPantalon : NumeroPantalonM);

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

            NumeroPantalonM = (numeroPantalon < 3) ? numeroPantalon : ((restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.pantalon, numeroPantalon]) ? numeroPantalon : NumeroPantalonM);

            if (NumeroPantalonM != numeroPantalon)
                pantalonBlock.SetActive(true);
            else
                pantalonBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            pantalones[numeroPantalon].SetActive(false);

            pantalones[pantalones.Count - 1].SetActive(true);

            if (restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.pantalon, numeroPantalon])
                NumeroPantalonM = pantalones.Count - 1;
            else
                pantalonBlock.SetActive(true);
        }
    }

    public void BuyPantalon()
    {
        semillasTxt.text = JsonContainer.instance.Pcharacter.Semillas;

        totalSemillasTxt.text = (int.Parse(JsonContainer.instance.Pcharacter.Semillas) - selectionCharacter.price).ToString();

        buyBtn.interactable = true;

        if (int.Parse(totalSemillasTxt.text) <= 0)
        {
            buyBtn.interactable = false;

            totalSemillasTxt.color = Color.red;
        }
        else
            selectionCharacter.buy = () =>
            {
                buyBtn.interactable = false;

                closeBtn.interactable = false;

                StartCoroutine(BoyBuyCoroutine(pantalonBlock, "pantalon", Enumerable.Range(0, pantalones.Count).First(pantalon => pantalones[pantalon].activeSelf)));
            };

        semillasPanel.SetActive(true);
    }

    public void NextZapato()
    {
        int numeroZapato = Enumerable.Range(0, zapatos.Count).First(zapato => zapatos[zapato].activeSelf);

        if (numeroZapato + 1 < zapatos.Count)
        {
            zapatos[numeroZapato].SetActive(false);

            numeroZapato++;

            zapatos[numeroZapato].SetActive(true);

            NumeroZapatoM = (numeroZapato < 3) ? numeroZapato : ((restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.zapatos, numeroZapato]) ? numeroZapato : NumeroZapatoM);

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

            NumeroZapatoM = (numeroZapato < 3) ? numeroZapato : ((restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.zapatos, numeroZapato]) ? numeroZapato : NumeroZapatoM);

            if (NumeroZapatoM != numeroZapato)
                zapatoBlock.SetActive(true);
            else
                zapatoBlock.SetActive(false);
        }
        else  //si ya no tiene mas posiciones en la lista hacia adelante entonces vuelva a la pos 0.
        {
            zapatos[numeroZapato].SetActive(false);

            zapatos[zapatos.Count - 1].SetActive(true);

            if (restrictions[Personalization.Restrictions.Sex.F, Personalization.Restrictions.Type.zapatos, numeroZapato])
                NumeroZapatoM = zapatos.Count - 1;
            else
                zapatoBlock.SetActive(true);
        }
    }

    public void BuyZapatos()
    {
        semillasTxt.text = JsonContainer.instance.Pcharacter.Semillas;

        totalSemillasTxt.text = (int.Parse(JsonContainer.instance.Pcharacter.Semillas) - selectionCharacter.price).ToString();

        buyBtn.interactable = true;

        if (int.Parse(totalSemillasTxt.text) <= 0)
        {
            buyBtn.interactable = false;

            totalSemillasTxt.color = Color.red;
        }
        else
            selectionCharacter.buy = () =>
            {
                buyBtn.interactable = false;

                closeBtn.interactable = false;

                StartCoroutine(BoyBuyCoroutine(zapatoBlock, "zapatos", Enumerable.Range(0, pantalones.Count).First(zapato => zapatos[zapato].activeSelf)));
            };

        semillasPanel.SetActive(true);
    }

    IEnumerator BoyBuyCoroutine(GameObject locker, string part, int index)
    {
        UnityWebRequest request = new UnityWebRequest("https://www.polygon.us/apiEscuelaspp/public/Tienda/" + JsonContainer.instance.Pcharacter.IdUsuaio, (!PlayerPrefs.HasKey("internal_buy")) ? "POST" : "PUT");

        byte[] body = Encoding.UTF8.GetBytes(
            "{" +
            "\"Sexo\":\"M\",\"Parte\":\"" + part + "\",\"index\":\"" + index.ToString() +
            "\"}");

        request.uploadHandler = new UploadHandlerRaw(body);

        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError)
            Debug.Log(request.error);
        else
        {
            Debug.Log("Set in inventory: " + request.responseCode);

            if (request.responseCode == 200)
            {
                PlayerPrefs.SetString("internal_buy", "setter");

                JsonContainer.instance.Pcharacter.Semillas = (int.Parse(JsonContainer.instance.Pcharacter.Semillas) - selectionCharacter.price).ToString();

                request = new UnityWebRequest("https://www.polygon.us/apiEscuelaspp/public/Personaje", "PUT");

                body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(JsonContainer.instance.Pcharacter));

                request.uploadHandler = new UploadHandlerRaw(body);

                request.downloadHandler = new DownloadHandlerBuffer();

                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.isNetworkError)
                    Debug.Log(request.error);
                else
                {
                    Debug.Log("Set Character: " + request.responseCode);

                    StartCoroutine(GetInventoryCoroutine(() =>
                    {
                        locker.SetActive(false);

                        buyBtn.interactable = true;

                        closeBtn.interactable = true;

                        semillasPanel.SetActive(false);
                    }));

                    yield break;
                }
            }

            buyBtn.interactable = true;

            closeBtn.interactable = true;

            semillasPanel.SetActive(false);
        }
    }

    public void Close()
    {
        selectionCharacter.buy = null;

        semillasPanel.SetActive(false);
    }
}