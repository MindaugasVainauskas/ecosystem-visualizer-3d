using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour {
    
    //load the graph scene(for now. Later will load QR code reader first)
    public void btnChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Exit from application
    public void btnExitApp()
    {
        //Clean out the current stored graph
        PlayerPrefs.SetString("JSON_graph_data", null);
        //exit the application.
        Application.Quit();
    }
}
