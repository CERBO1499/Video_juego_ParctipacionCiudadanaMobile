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
    public void ToparchadosInfantil()
    {
        SceneManager.LoadScene("ParchadosInfantiles", LoadSceneMode.Single);
    }
    public void ToGeoGuess()
    {
        SceneManager.LoadScene("GeoGuesOriginal", LoadSceneMode.Single);
    }
    public void ToMelodias()
    {
        SceneManager.LoadScene("nuestrasMelodias", LoadSceneMode.Single);
    }

    public void ToTelaraña() 
    {
        SceneManager.LoadScene("Telaraña", LoadSceneMode.Single);
    }

    public void ToLaberinto()
    {
        SceneManager.LoadScene("Laberinto", LoadSceneMode.Single);
    }
    public void ToLaberintoNiños()
    {
        SceneManager.LoadScene("LaberintoNiños", LoadSceneMode.Single);
    }

    public void ToRuleta()
    {
        SceneManager.LoadScene("Ruleta", LoadSceneMode.Single);
    }
    public void ToRuletaNiños()
    {
        SceneManager.LoadScene("RuletaNiños", LoadSceneMode.Single);
    }

    public void ToMainMenuWithSemilla()
    {
        ControlSemilla.SumarSemilla(10, () =>
        {
            SceneManager.LoadScene("main", LoadSceneMode.Single);
        });
    }
}