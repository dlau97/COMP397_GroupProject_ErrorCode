using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public GameObject LeftShootLocation;

    public GameObject RightShootLocation;
    public GameObject bullet;

    public float bulletSpeed = 30f;

    public Camera fpsCamera;

    public AudioSource source;

    public AudioClip cannonShootSFX;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            LeftShoot();
        }
        if(Input.GetButtonDown("Fire2")){
            RightShoot();
        }
    }

    void LeftShoot(){
        RaycastHit hit;
        Vector3 startPosition = fpsCamera.transform.position;
        startPosition.z += 1f;

        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range)){
            //Debug.Log(hit.transform.tag);
            source.PlayOneShot(cannonShootSFX, 0.5f);
            GameObject PlayerBullet1 = Instantiate(bullet, LeftShootLocation.transform.position, LeftShootLocation.transform.rotation);
            Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
            Vector3 directionVector = hit.point - LeftShootLocation.transform.position;
            bulletRB.velocity = directionVector.normalized * bulletSpeed;

        }
    }

    void RightShoot(){
        RaycastHit hit;
        Vector3 startPosition = fpsCamera.transform.position;
        startPosition.z += 1f;

        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range)){
            //Debug.Log(hit.transform.tag);
            source.PlayOneShot(cannonShootSFX, 0.5f);
            GameObject PlayerBullet2 = Instantiate(bullet, RightShootLocation.transform.position, RightShootLocation.transform.rotation);
            Rigidbody bulletRB = PlayerBullet2.GetComponent<Rigidbody>();
            Vector3 directionVector = hit.point - RightShootLocation.transform.position;
            bulletRB.velocity = directionVector.normalized * bulletSpeed;
        }
    }
}
