﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    [SerializeField]
    private string gameplaySceneName;
    [SerializeField]
    private string mapMakerSceneName;
    [SerializeField]
    private string mainMenuSceneName;

    public void ToGameplay()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void ToMapMaker()
    {
        SceneManager.LoadScene(mapMakerSceneName);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
