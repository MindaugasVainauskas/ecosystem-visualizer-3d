﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour {

    public void btnChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void btnExitApp()
    {
        Application.Quit();
    }
}
