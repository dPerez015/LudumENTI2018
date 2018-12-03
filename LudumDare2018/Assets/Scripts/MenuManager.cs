﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    [System.NonSerialized]
    public int currentLevel;

    public float maxTime;

    public void reload()
    {
        SceneManager.LoadScene("Level" + currentLevel.ToString());
    }

   public void loadFirstLevel()
    {
        Timemanager.Instance.setMaxTime(maxTime);
        Timemanager.Instance.setCurrentTime(maxTime);
        SceneManager.LoadScene("Level0");
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void loadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void loadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void exitApp()
    {
        Application.Quit();
    }
}
