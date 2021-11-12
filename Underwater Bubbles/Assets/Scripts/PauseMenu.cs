using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public bool achievementActive = false;

    public GameObject pauseMenu;

    public GameObject achievementMenu;
    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Used for testing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && !achievementMenu)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    private void OnMouseDown()
    {
        if (isPaused && !achievementMenu)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        achievementActive = false;
        achievementMenu.SetActive(false);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OpenAchievement()
    {
        //Show the achievement menu
        achievementMenu.SetActive(!achievementMenu.activeSelf);
        pauseMenu.SetActive(false);
        achievementActive = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quiting the game... ");
        Application.Quit();
    }
}
