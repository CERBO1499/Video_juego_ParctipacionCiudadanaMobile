using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseBotton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject objetoAActivar;     
    
    public void Presionado()
    {
        gameObject.SetActive(false);//oculta el objeto con este script
    }
    public void activar()
    {
        objetoAActivar.SetActive(true);//activa el objeto serializadoa
        //actu
    }

    public void LogOut()
    {
        PlayerPrefs.SetString("User Name", "");
        PlayerPrefs.SetString("Password", "");

        SceneManager.LoadScene("menu");
    }
}