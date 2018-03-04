using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour {

    public void btnOpenGraph(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
