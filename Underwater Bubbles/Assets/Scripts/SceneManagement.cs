using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    string scene;
    
    public void ChangeScene(string scene)
    {
        switch (scene)
        {
            case "MainLevel":
                SceneManager.LoadScene("MainLevel");
                break;
            case "Options":
                SceneManager.LoadScene("Options");
                break;
            case "MainMenu":
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
