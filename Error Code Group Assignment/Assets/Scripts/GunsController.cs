using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public GameObject m107Left, m107SpawnLeft, m249Left, m249SpawnLeft, bennelliM4Left, bennelliM4SpawnLeft, rPG7Left, rPG7SpawnLeft,
        m107Right, m107SpawnRight, m249Right, m249SpawnRight, bennelliM4Right, bennelliM4SpawnRight, rPG7Right, rPG7SpawnRight;
    public GameObject m107Bullet, m249Bullet, bennelliM4Bullet, rPG7Bullet;

    public int m249Clip = 200, m249CurrentClipLeft = 0, m249CurrentClipRight = 0, m249Ammo = 0;

    public GameObject bullet;
    public float bulletSpeed = 40f;
    public Camera fpsCamera;

    public AudioSource source;
    public AudioClip cannonShootSFX;

    private bool isReloadingLeft, isReloadingRight;
    private RaycastHit hit;
    private Vector3 startPosition;

    // Update is called once per frame
    void Update()
    {
        startPosition = fpsCamera.transform.position;
        startPosition.z += 1f;

        if (Input.GetButtonDown("Fire1"))
        {
            if (m107Left.activeInHierarchy == true && isReloadingLeft == false)
            {
                if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
                {
                    source.PlayOneShot(cannonShootSFX, 0.5f);
                    GameObject PlayerBullet1 = Instantiate(m107Bullet, m107SpawnLeft.transform.position, m107SpawnLeft.transform.rotation);
                    Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
                    Vector3 directionVector = hit.point - m107SpawnLeft.transform.position;
                    bulletRB.velocity = directionVector.normalized * bulletSpeed;
                }
                isReloadingLeft = true;
                Invoke("IsReloadingLeft", 1.0f);
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (m107Right.activeInHierarchy == true && isReloadingRight == false)
            {
                if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
                {
                    source.PlayOneShot(cannonShootSFX, 0.5f);
                    GameObject PlayerBullet1 = Instantiate(m107Bullet, m107SpawnRight.transform.position, m107SpawnRight.transform.rotation);
                    Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
                    Vector3 directionVector = hit.point - m107SpawnRight.transform.position;
                    bulletRB.velocity = directionVector.normalized * bulletSpeed;
                }
                isReloadingRight = true;
                Invoke("IsReloadingRight", 1.0f);
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            if (m249Left.activeInHierarchy == true && isReloadingLeft == false && m249CurrentClipLeft != 0)
            {
                if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
                {
                    M249FireLeft();
                }
            }
            else if (m249Left.activeInHierarchy == true && isReloadingLeft == false && m249CurrentClipLeft == 0 && m249Ammo != 0)
            {
                isReloadingLeft = true;
                Invoke("IsReloadingLeft", 5.0f);
            }
        }

        if (Input.GetButton("Fire2"))
        {
            if (m249Right.activeInHierarchy == true && isReloadingRight == false && m249CurrentClipRight != 0)
            {
                if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
                {
                    M249FireRight();
                }
            }
            else if (m249Right.activeInHierarchy == true && isReloadingRight == false && m249CurrentClipRight == 0 && m249Ammo != 0)
            {
                isReloadingRight = true;
                Invoke("IsReloadingRight", 5.0f);
            }
        }
    }

    void IsReloadingLeft()
    {
        if (m249Left.activeInHierarchy == true)
        {
            if ((m249Ammo - m249Clip) >= 0)
            {
                m249CurrentClipLeft = m249Clip;
                m249Ammo = m249Ammo - m249Clip;
            }
            else if ((m249Ammo - m249Clip) < 0) 
            {
                m249CurrentClipLeft = m249Ammo;
                m249Ammo = 0;
            }
        }     

        isReloadingLeft = false;
    }

    void IsReloadingRight()
    {
        if (m249Right.activeInHierarchy == true)
        {
            if ((m249Ammo - m249Clip) >= 0)
            {
                m249CurrentClipRight = m249Clip;
                m249Ammo = m249Ammo - m249Clip;
            }
            else if ((m249Ammo - m249Clip) < 0)
            {
                m249CurrentClipRight = m249Ammo;
                m249Ammo = 0;
            }
        }

        isReloadingRight = false;
    }

    void M249FireLeft()
    {        
            //source.PlayOneShot(cannonShootSFX, 0.5f);
            GameObject PlayerBullet1 = Instantiate(m249Bullet, m249SpawnLeft.transform.position, m249SpawnLeft.transform.rotation);
            Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
            Vector3 directionVector = hit.point - m249SpawnLeft.transform.position;
            bulletRB.velocity = directionVector.normalized * bulletSpeed;
            m249CurrentClipLeft--;
    }

    void M249FireRight()
    {        
            //source.PlayOneShot(cannonShootSFX, 0.5f);
            GameObject PlayerBullet1 = Instantiate(m249Bullet, m249SpawnRight.transform.position, m249SpawnRight.transform.rotation);
            Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
            Vector3 directionVector = hit.point - m249SpawnRight.transform.position;
            bulletRB.velocity = directionVector.normalized * bulletSpeed;
            m249CurrentClipRight--;
    }
}
