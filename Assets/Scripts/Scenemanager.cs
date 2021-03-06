﻿using System;
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

    public void ToPersonalitation()
    {
        SceneManager.LoadScene("choseScene",LoadSceneMode.Single);
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

    public void ToAldea()
    {
        SceneManager.LoadScene("Aldea Jovenes", LoadSceneMode.Single);
    }
    public void ToAldeaNiños()
    {
        SceneManager.LoadScene("Aldea Niños", LoadSceneMode.Single);
    }

    public void ToMiCasaMiTerritorio()
    {
        SceneManager.LoadScene("Mi casa, mi territorio", LoadSceneMode.Single);
    }

    public void ToRuleta()
    {
        SceneManager.LoadScene("Ruleta", LoadSceneMode.Single);
    }

    public void ToDominoInfantil()
    {
        SceneManager.LoadScene("Domino Infantil", LoadSceneMode.Single);
    }

    public void ToUnoGame()
    {
        SceneManager.LoadScene("Uno 1", LoadSceneMode.Single);
    }

    public void ToRompecabezas()
    {
        SceneManager.LoadScene("Rompecabezas", LoadSceneMode.Single);
    }
    public void ToMainMenuWithSemilla()
    {
        ControlSemilla.SumarSemilla(10, () =>
        {
            SceneManager.LoadScene("main", LoadSceneMode.Single);
        });
    }

    public void ToMainMenuWithSemilla(int semillas)
    {
        ControlSemilla.SumarSemilla(semillas, () =>
        {
            SceneManager.LoadScene("main", LoadSceneMode.Single);
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data">
    /// X: isPlayerWinner? 1 : 0
    /// Y: Prize recived for loosing
    /// Z: Prize recived for winning
    /// </param>
    public void ToMainMenuWithSemilla(Transform data)
    {
        if (data.localPosition.x == 0 ||
            data.localPosition.x == 1) { 
            ControlSemilla.SumarSemilla((int)(data.localPosition.x == 0 ? data.localPosition.y : data.localPosition.z), () =>
            {
                SceneManager.LoadScene("main", LoadSceneMode.Single);
            });
        }
    }
}