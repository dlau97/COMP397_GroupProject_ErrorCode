using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject healthBarUI;
    public GameObject InventoryUI;
    public GameObject OptionsUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc");
            
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();

            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        healthBarUI.SetActive(true);
        ShowMouseCursor();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Paused()
    {
        pauseMenuUI.SetActive(true);
        healthBarUI.SetActive(false);
        ShowMouseCursor();
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }   
    
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
