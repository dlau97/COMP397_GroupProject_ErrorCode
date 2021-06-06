using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathColliderController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player" || other.transform.tag == "Mech"){
            Debug.Log("Player Dead");
            SceneManager.LoadScene("Game Over Screen");
        }
        Debug.Log(other.transform.name);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" || other.transform.tag == "Mech"){
            Debug.Log("Player Dead");
            SceneManager.LoadScene("Game Over Screen");
        }
        Debug.Log(other.transform.name);
    }
}
