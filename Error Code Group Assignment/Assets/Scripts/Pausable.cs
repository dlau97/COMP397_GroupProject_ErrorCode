using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pausable : MonoBehaviour
{
    public List<MonoBehaviour> scripts;
    public bool isGamePaused;
    
    // Start is called before the first frame update
    void Start()
    {
        isGamePaused = false;
    }


    public void TogglePaused()
    {
        isGamePaused = !isGamePaused;

        foreach (var script in scripts)
        {
            if(script != null){
                script.enabled = !isGamePaused;
            }
            
        }

    }
}
