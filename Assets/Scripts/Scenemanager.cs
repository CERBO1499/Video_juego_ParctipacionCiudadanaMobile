using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenemanager : MonoBehaviour
{
    #region Events
    public Action onToMainGame;
    #endregion

    private void Awake()
    {
       //ctrlSemilla = GameObject.Find("ControlSemilla").GetComponent<ControlSemilla>();
    }

    public void ToDrawGame() {

        SceneManager.LoadScene("SceneDraw",LoadSceneMode.Single);

    }
    public void ToMainGame() {

        if (onToMainGame != null)
        {
            onToMainGame?.Invoke();

            onToMainGame = null;
        }
        else
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