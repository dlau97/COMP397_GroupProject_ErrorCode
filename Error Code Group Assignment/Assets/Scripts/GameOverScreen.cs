using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    private void Start() {
        ShowMouseCursor();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    private void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
