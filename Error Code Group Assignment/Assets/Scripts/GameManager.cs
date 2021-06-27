using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int enemiesLeft = 50;
    bool gameHasEnded = false;

    public TextMeshProUGUI enemyCounterText;


    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over!");
            Restart();
        }
    }


    public void EnemyKill(){
        enemiesLeft--;
        enemyCounterText.text = "Enemies Left: "+enemiesLeft;
        if(enemiesLeft <=0){
            SceneManager.LoadScene("Win Scene");
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game Scene");
    }

}
