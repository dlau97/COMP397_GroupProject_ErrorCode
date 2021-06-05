using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerBehaviour movement;

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Enemy")
        {
            movement.enabled = false;   //Disable the player movements
            FindObjectOfType<GameManager>().EndGame(); 
        }
    }


}
