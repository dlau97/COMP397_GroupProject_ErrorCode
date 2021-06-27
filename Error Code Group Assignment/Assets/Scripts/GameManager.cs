using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int enemiesLeft = 50;
    bool gameHasEnded = false;

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
        if(enemiesLeft <=0){
            SceneManager.LoadScene("Win Scene");
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game Scene");
    }

}
