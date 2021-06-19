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

    public int m249Clip = 200, m249CurrentClipLeft = 0, m249CurrentClipRight = 0, m249Ammo = 0,
        bennelliM4Clip = 6, bennelliM4CurrentClipLeft = 0, bennelliM4CurrentClipRight = 0, bennelliM4Ammo = 0,
        rPG7Clip = 1, rPG7CurrentClipLeft = 0, rPG7CurrentClipRight = 0, rPG7Ammo = 0;

    public GameObject bullet;
    public float bulletSpeed = 40f;
    public Camera fpsCamera;

    public AudioSource source;
    public AudioClip cannonShootSFX;

    private bool isReloadingLeft, isReloadingRight, isBennelliM4DelayLeft, isBennelliM4DelayRight;
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
            else if (bennelliM4Left.activeInHierarchy == true && isReloadingLeft == false && isBennelliM4DelayLeft == false)
            {
                if (bennelliM4CurrentClipLeft != 0)
                {
                    if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
                    {
                        //source.PlayOneShot(cannonShootSFX, 0.5f);
                        GameObject PlayerBullet1 = Instantiate(bennelliM4Bullet, bennelliM4Left.transform.position, bennelliM4SpawnLeft.transform.rotation);
                        Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
                        Vector3 directionVector = hit.point - bennelliM4SpawnLeft.transform.position;
                        bulletRB.velocity = directionVector.normalized * bulletSpeed;
                    }
                    bennelliM4CurrentClipLeft--;
                    isBennelliM4DelayLeft = true;
                    Invoke("BennelliM4DelayLeft", 1f);
                }
                else if (bennelliM4CurrentClipLeft == 0 && bennelliM4Ammo != 0)
                {
                    isReloadingLeft = true;
                    Invoke("IsReloadingLeft", 6.0f);
                }
            }
            else if (rPG7Left.activeInHierarchy == true && isReloadingLeft == false)
            {
                if (rPG7CurrentClipLeft != 0)
                {
                    if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
                    {
                        //source.PlayOneShot(cannonShootSFX, 0.5f);
                        GameObject PlayerBullet1 = Instantiate(rPG7Bullet, rPG7SpawnLeft.transform.position, rPG7SpawnLeft.transform.rotation);
                        Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
                        Vector3 directionVector = hit.point - rPG7SpawnLeft.transform.position;
                        bulletRB.velocity = directionVector.normalized * bulletSpeed;
                    }
                    rPG7CurrentClipLeft--;
                    isReloadingLeft = true;
                    Invoke("IsReloadingLeft", 2.0f);
                }
                else if (rPG7CurrentClipLeft == 0 && rPG7Ammo != 0)
                {
                    isReloadingLeft = true;
                    Invoke("IsReloadingLeft", 2.0f);
                }
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
            else if (bennelliM4Right.activeInHierarchy == true && isReloadingRight == false && isBennelliM4DelayRight == false)
            {
                if (bennelliM4CurrentClipRight != 0)
                {
                    if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
                    {
                        //source.PlayOneShot(cannonShootSFX, 0.5f);
                        GameObject PlayerBullet1 = Instantiate(bennelliM4Bullet, bennelliM4Right.transform.position, bennelliM4SpawnRight.transform.rotation);
                        Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
                        Vector3 directionVector = hit.point - bennelliM4SpawnRight.transform.position;
                        bulletRB.velocity = directionVector.normalized * bulletSpeed;
                    }
                    bennelliM4CurrentClipRight--;
                    isBennelliM4DelayRight = true;
                    Invoke("BennelliM4DelayRight", 1f);
                }
                else if (bennelliM4CurrentClipRight == 0 && bennelliM4Ammo != 0)
                {
                    isReloadingRight = true;
                    Invoke("IsReloadingRight", 6.0f);
                }
            }
            else if (rPG7Right.activeInHierarchy == true && isReloadingRight == false)
            {
                if (rPG7CurrentClipRight != 0)
                {
                    if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
                    {
                        //source.PlayOneShot(cannonShootSFX, 0.5f);
                        GameObject PlayerBullet1 = Instantiate(rPG7Bullet, rPG7SpawnRight.transform.position, rPG7SpawnRight.transform.rotation);
                        Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
                        Vector3 directionVector = hit.point - rPG7SpawnRight.transform.position;
                        bulletRB.velocity = directionVector.normalized * bulletSpeed;
                    }
                    rPG7CurrentClipRight--;
                    isReloadingRight = true;
                    Invoke("IsReloadingRight", 2.0f);
                }
                else if (rPG7CurrentClipRight == 0 && rPG7Ammo != 0)
                {
                    isReloadingRight = true;
                    Invoke("IsReloadingRight", 2.0f);
                }
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

        if (bennelliM4Left.activeInHierarchy == true)
        {
            if ((bennelliM4Ammo - bennelliM4Clip) >= 0)
            {
                bennelliM4CurrentClipLeft = bennelliM4Clip;
                bennelliM4Ammo = bennelliM4Ammo - bennelliM4Clip;
            }
            else if ((bennelliM4Ammo - bennelliM4Clip) < 0)
            {
                bennelliM4CurrentClipLeft = bennelliM4Ammo;
                bennelliM4Ammo = 0;
            }
        }

        if (rPG7Left.activeInHierarchy == true)
        {
            if ((rPG7Ammo - rPG7Clip) >= 0)
            {
                rPG7CurrentClipLeft = rPG7Clip;
                rPG7Ammo = rPG7Ammo - rPG7Clip;
            }
            else if ((rPG7Ammo - rPG7Clip) < 0)
            {
                rPG7CurrentClipLeft = rPG7Ammo;
                rPG7Ammo = 0;
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

        if (bennelliM4Right.activeInHierarchy == true)
        {
            if ((bennelliM4Ammo - bennelliM4Clip) >= 0)
            {
                bennelliM4CurrentClipRight = bennelliM4Clip;
                bennelliM4Ammo = bennelliM4Ammo - bennelliM4Clip;
            }
            else if ((bennelliM4Ammo - bennelliM4Clip) < 0)
            {
                bennelliM4CurrentClipRight = bennelliM4Ammo;
                bennelliM4Ammo = 0;
            }
        }

        if (rPG7Right.activeInHierarchy == true)
        {
            if ((rPG7Ammo - rPG7Clip) >= 0)
            {
                rPG7CurrentClipRight = rPG7Clip;
                rPG7Ammo = rPG7Ammo - rPG7Clip;
            }
            else if ((rPG7Ammo - rPG7Clip) < 0)
            {
                rPG7CurrentClipRight = rPG7Ammo;
                rPG7Ammo = 0;
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

    void BennelliM4DelayLeft()
    {
        isBennelliM4DelayLeft = false;
    }

    void BennelliM4DelayRight()
    {
        isBennelliM4DelayRight = false;
    }
}
