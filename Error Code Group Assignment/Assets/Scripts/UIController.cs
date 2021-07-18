using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject healthBarUI;
    public GameObject InventoryUI;
    public GameObject OptionsUI;
    public GameObject crosshairUI;

    public GameObject enemyCounterText;

    public Pausable pausable;

    public CameraController playerCamera;

    private void Start()
    {
        pausable = FindObjectOfType<Pausable>();

        playerCamera = FindObjectOfType<CameraController>();
    }




    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    GameIsPaused = !GameIsPaused;
        //    Debug.Log("Esc");
            
        //    if (GameIsPaused)
        //    {

        //        Paused();

        //    }
        //    else
        //    {

        //        PausedResume();

        //    }
        //}   
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    GameIsPaused = !GameIsPaused;
            
        //    if (GameIsPaused)
        //    {
        //        InventoryPaused();
        //    }
        //    else
        //    {
        //        InventoryResume();
        //    }
        //}
    }

    public void PausedResume()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        playerCamera.enabled = true;
        //pausable.TogglePaused();
        pauseMenuUI.SetActive(false);
        crosshairUI.SetActive(true);
        enemyCounterText.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameObject.Find("Mech").GetComponentInChildren<GunsController>().enabled = true;
    }

    void Paused()
    {
        //Cursor.lockState = CursorLockMode.None;
        playerCamera.enabled = false;
        //pausable.TogglePaused();
        pauseMenuUI.SetActive(true);
        enemyCounterText.SetActive(false);
        crosshairUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        GameObject.Find("Mech").GetComponentInChildren<GunsController>().enabled = false;

    }

    void InventoryPaused()
    {
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        playerCamera.enabled = false;
        pausable.TogglePaused();
        InventoryUI.SetActive(true);
        enemyCounterText.SetActive(false);
        crosshairUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        GameObject.Find("Mech").GetComponentInChildren<GunsController>().enabled = false;

    }

    public void InventoryResume()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        playerCamera.enabled = true;
        pausable.TogglePaused();
        InventoryUI.SetActive(false);
        enemyCounterText.SetActive(true);
        crosshairUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameObject.Find("Mech").GetComponentInChildren<GunsController>().enabled = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        PausedResume();
        SceneManager.LoadScene("Start Menu");
    }   
    
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void SaveGame()
    {
        Debug.Log("Saving game");
        
        PlayerBehaviour playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        List<EnemyController> enemyControllers = new List<EnemyController>(FindObjectsOfType<EnemyController>());
        GunsController gunController = FindObjectOfType<GunsController>();

        Save save = Save.Build(
            playerBehaviour,
            enemyControllers,
            gunController
        );
        save.Persist();
    }

    public void LoadGame()
    {
        Debug.Log("Loading game");

        PlayerBehaviour playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        List<EnemyController> enemyControllers = new List<EnemyController>(FindObjectsOfType<EnemyController>());
        GunsController gunController = FindObjectOfType<GunsController>();

        Save save = Save.FromSaved();
        save.Load(
            playerBehaviour,
            enemyControllers,
            gunController
        );
    }

    public void OnPausedButtonPressed()
    {
        GameIsPaused = !GameIsPaused;
        Debug.Log("Esc");

        if (GameIsPaused)
        {

            Paused();

        }
        else
        {

            PausedResume();

        }
    }

    public void OnWeaponButtonPressed()
    {
        GameIsPaused = !GameIsPaused;

        if (GameIsPaused)
        {
            InventoryPaused();
        }
        else
        {
            InventoryResume();
        }
    }

    //public void ShowMouseCursor()
    //{
    //    Cursor.visible = true;
    //    Cursor.lockState = CursorLockMode.None;
    //}

    //public void HideMouseCursor()
    //{
    //    Cursor.visible = false;
    //    Cursor.lockState = CursorLockMode.Locked;
    //}
}
