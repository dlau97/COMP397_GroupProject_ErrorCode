using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{

    public float BulletDamage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 6f); //Destroy bullet if it doesn't collide with anything after 6 seconds
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Enemy"){
            //Insert Damage code to player

            other.gameObject.SendMessage("TakeDamage", BulletDamage);

            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Ground"){
            //Insert any collision sfx / vfx

            Destroy(this.gameObject);
        }
    }
}
