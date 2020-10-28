using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{





    private void Awake()
    {
       //ctrlSemilla = GameObject.Find("ControlSemilla").GetComponent<ControlSemilla>();
    }

    public void ToDrawGame() {

        SceneManager.LoadScene("SceneDraw",LoadSceneMode.Single);

    }
    public void ToMainGame() {
        SceneManager.LoadScene("main", LoadSceneMode.Single);

    }
    public void ToParchados()
    {
        SceneManager.LoadScene("parchados", LoadSceneMode.Single);
    }
    public void ToGeoGuess()
    {
        SceneManager.LoadScene("GeoGuesOriginal", LoadSceneMode.Single);
    }
    public void ToMelodias()
    {
        SceneManager.LoadScene("nuestrasMelodias", LoadSceneMode.Single);
    }

    public void ToMainMenuWithSemilla()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
        ControlSemilla.SumarSemilla(10);

    }





}

