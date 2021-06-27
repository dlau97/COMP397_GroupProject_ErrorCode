using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineController : MonoBehaviour
{
    public GameObject explosionFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.SendMessage("TakeDamage", 25f);
            Instantiate(explosionFX, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            GameObject.Find("Sound Controller").SendMessage("PlayMineExplodeSFX");
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "PlayerBullet"){
            Destroy(Instantiate(explosionFX, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity), 5f);
            GameObject.Find("Sound Controller").SendMessage("PlayMineExplodeSFX");
            Destroy(this.gameObject);
        }
    }
}
