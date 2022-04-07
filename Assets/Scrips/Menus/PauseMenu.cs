using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject pauseMenuUI;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f; //Normal time
        GameIsPause = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //To freeze the game
        GameIsPause = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loadng menu...");
        Time.timeScale = 1f; //Because i paused the game, the time is 0. I need to get it back to normal
        Destroy(GameObject.FindGameObjectWithTag("PlayerGroup"));
        Destroy(GameObject.FindGameObjectWithTag("GameManager"));
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}
