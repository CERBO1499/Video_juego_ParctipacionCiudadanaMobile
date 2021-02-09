using UnityEngine;

public class sexElection : MonoBehaviour
{
    [SerializeField] GameObject hombre,mujer;
    public static short sexo = 0;  //0 = hombre, 1=mujer


    private void Awake()
    {
        hombre.SetActive(false);
        mujer.SetActive(false);

        if (JsonContainer.instance.Pcharacter.Genero == "0")
            mujer.SetActive(true);
        else
            hombre.SetActive(true);
    }

    //metodos en menú escoger personaje:
    public void CambioSexo()
    {
        if (sexo == 0)
        {
            mujer.SetActive(true);
            sexo = 1;
            hombre.SetActive(false);
        }
        else
        {
            mujer.SetActive(false);
            sexo = 0;
            hombre.SetActive(true);
        }
    }

    //En el cambio de escena 

    public void CambioEscenaSexo()
    {
        if (sexo == 0)
        {
            Destroy(mujer);         
            hombre.SetActive(true);
        }
        else
        {
            Destroy(hombre); 
            mujer.SetActive(true);
        }
    }

}
