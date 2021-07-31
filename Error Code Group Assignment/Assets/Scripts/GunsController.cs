using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsController : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;

    

    [Header("Gun M107")]
    public GameObject m107Left;
    public GameObject m107SpawnLeft;
    public GameObject m107Right;
    public GameObject m107SpawnRight;
    public GameObject m107Bullet;

    [Header("Gun M249")]
    public GameObject m249Left;
    public GameObject m249SpawnLeft;
    public GameObject m249Right;
    public GameObject m249SpawnRight;
    public GameObject m249Bullet;
    public int m249Clip = 200;
    public int m249CurrentClipLeft = 0;
    public int m249CurrentClipRight = 0;
    public int m249Ammo = 0;

     [Header("Gun Bennelli M4")]
    public GameObject bennelliM4Left;
    public GameObject bennelliM4SpawnLeft;
    public GameObject bennelliM4Right;
    public GameObject bennelliM4SpawnRight;
    public GameObject bennelliM4Bullet;
    public int bennelliM4Clip = 6;
    public int bennelliM4CurrentClipLeft = 0;
    public int bennelliM4CurrentClipRight = 0;
    public int bennelliM4Ammo = 0;

    [Header("Gun RPG7")]
    public GameObject rPG7Left;
    public GameObject rPG7SpawnLeft;
    public GameObject rPG7Right;
    public GameObject rPG7SpawnRight;
    public GameObject rPG7Bullet;
    public int rPG7Clip = 1;
    public int rPG7CurrentClipLeft = 0;
    public int rPG7CurrentClipRight = 0;
    public int rPG7Ammo = 0;

    [Header("Bullet Setting")]
    public GameObject bullet;
    public float bulletSpeed = 40f;
    public Camera fpsCamera;

    [Header("Audio")]
    public AudioSource source;
    //public AudioClip cannonShootSFX;
    public AudioClip m107;
    public AudioClip m249;
    public AudioClip m249Reload;
    public AudioClip bennelliM4;
    public AudioClip bennelliM4Reload;
    public AudioClip rPG7;
    public AudioClip rPG7Reload;

    [Header("Other setting")]
    private bool isReloadingLeft, isReloadingRight, isBennelliM4DelayLeft, isBennelliM4DelayRight;
    private RaycastHit hit;
    private Vector3 startPosition;


    public abstract class Bullet
    {
        public GunsController gunsController;

        public Bullet(GunsController gunsController)
        {
            this.gunsController = gunsController;
        }
        public abstract void FireLeft();
        public abstract void FireRight();
    }

    class M107Bullet : Bullet
    {
        public M107Bullet(GunsController gunsController): base(gunsController) { }

        public override void FireLeft()
        {
            this.gunsController.source.PlayOneShot(this.gunsController.m107);
            GameObject PlayerBullet1 = Instantiate(
                this.gunsController.m107Bullet,
                this.gunsController.m107SpawnLeft.transform.position,
                this.gunsController.m107SpawnLeft.transform.rotation
            );
            Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
            Vector3 directionVector = this.gunsController.hit.point - this.gunsController.m107SpawnLeft.transform.position;
            bulletRB.velocity = directionVector.normalized * this.gunsController.bulletSpeed;
        }

        public override void FireRight()
        {
            this.gunsController.source.PlayOneShot(this.gunsController.m107);
            GameObject PlayerBullet1 = Instantiate(
                this.gunsController.m107Bullet,
                this.gunsController.m107SpawnRight.transform.position,
                this.gunsController.m107SpawnRight.transform.rotation
            );
            Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
            Vector3 directionVector = this.gunsController.hit.point - this.gunsController.m107SpawnRight.transform.position;
            bulletRB.velocity = directionVector.normalized * this.gunsController.bulletSpeed;
        }
    }

    class BennelliM4Bullet : Bullet
    {
        public BennelliM4Bullet(GunsController gunsController) : base(gunsController) { }

        public override void FireLeft()
        {
            this.gunsController.source.PlayOneShot(this.gunsController.bennelliM4);
            GameObject PlayerBullet1 = Instantiate(
                this.gunsController.bennelliM4Bullet,
                this.gunsController.bennelliM4Left.transform.position,
                this.gunsController.bennelliM4SpawnLeft.transform.rotation
            );
            Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
            Vector3 directionVector = this.gunsController.hit.point - this.gunsController.bennelliM4SpawnLeft.transform.position;
            bulletRB.velocity = directionVector.normalized * this.gunsController.bulletSpeed;
        }

        public override void FireRight()
        {
            this.gunsController.source.PlayOneShot(this.gunsController.bennelliM4);
            GameObject PlayerBullet1 = Instantiate(
                this.gunsController.bennelliM4Bullet,
                this.gunsController.bennelliM4Right.transform.position,
                this.gunsController.bennelliM4SpawnRight.transform.rotation
            );
            Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
            Vector3 directionVector = this.gunsController.hit.point - this.gunsController.bennelliM4SpawnRight.transform.position;
            bulletRB.velocity = directionVector.normalized * this.gunsController.bulletSpeed;
        }
    }

    class RPG7Bullet : Bullet
    {
        public RPG7Bullet(GunsController gunsController) : base(gunsController) { }

        public override void FireLeft()
        {
            this.gunsController.source.PlayOneShot(this.gunsController.rPG7);
            GameObject PlayerBullet1 = Instantiate(
                this.gunsController.rPG7Bullet,
                this.gunsController.rPG7SpawnLeft.transform.position, 
                this.gunsController.rPG7SpawnLeft.transform.rotation
            );
            Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
            Vector3 directionVector = this.gunsController.hit.point - this.gunsController.rPG7SpawnLeft.transform.position;
            bulletRB.velocity = directionVector.normalized * this.gunsController.bulletSpeed;
        }

        public override void FireRight()
        {
            this.gunsController.source.PlayOneShot(this.gunsController.rPG7);
            GameObject PlayerBullet1 = Instantiate(
                this.gunsController.rPG7Bullet,
                this.gunsController.rPG7SpawnRight.transform.position,
                this.gunsController.rPG7SpawnRight.transform.rotation
            );
            Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
            Vector3 directionVector = this.gunsController.hit.point - this.gunsController.rPG7SpawnRight.transform.position;
            bulletRB.velocity = directionVector.normalized * this.gunsController.bulletSpeed;
        }
    }

    class BulletFactory
    {
        private GunsController gunsController;

        public BulletFactory(GunsController gunsController)
        {
            this.gunsController = gunsController;
        }

        public Bullet createBullet(string name)
        {
            switch (name)
            {
                case "m107":
                    return new M107Bullet(gunsController);
                case "bennelliM4":
                    return new BennelliM4Bullet(gunsController);
                case "rGP7":
                    return new RPG7Bullet(gunsController);
                default:
                    return null;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        startPosition = fpsCamera.transform.position;
        startPosition.z += 1f;

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    if (m107Left.activeInHierarchy == true && isReloadingLeft == false)
        //    {
        //        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        //        {
        //            //source.PlayOneShot(cannonShootSFX, 0.5f);
        //            source.PlayOneShot(m107);
        //            GameObject PlayerBullet1 = Instantiate(m107Bullet, m107SpawnLeft.transform.position, m107SpawnLeft.transform.rotation);
        //            Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
        //            Vector3 directionVector = hit.point - m107SpawnLeft.transform.position;
        //            bulletRB.velocity = directionVector.normalized * bulletSpeed;
        //        }
        //        isReloadingLeft = true;
        //        Invoke("IsReloadingLeft", 0.5f);
        //    }
        //    else if (bennelliM4Left.activeInHierarchy == true && isReloadingLeft == false && isBennelliM4DelayLeft == false)
        //    {
        //        if (bennelliM4CurrentClipLeft != 0)
        //        {
        //            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        //            {
        //                source.PlayOneShot(bennelliM4);
        //                GameObject PlayerBullet1 = Instantiate(bennelliM4Bullet, bennelliM4Left.transform.position, bennelliM4SpawnLeft.transform.rotation);
        //                Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
        //                Vector3 directionVector = hit.point - bennelliM4SpawnLeft.transform.position;
        //                bulletRB.velocity = directionVector.normalized * bulletSpeed;
        //            }
        //            bennelliM4CurrentClipLeft--;
        //            isBennelliM4DelayLeft = true;
        //            Invoke("BennelliM4DelayLeft", 1f);
        //        }
        //        else if (bennelliM4CurrentClipLeft == 0 && bennelliM4Ammo != 0)
        //        {
        //            isReloadingLeft = true;
        //            Invoke("IsReloadingLeft", 6.0f);
        //        }
        //    }
        //    else if (rPG7Left.activeInHierarchy == true && isReloadingLeft == false)
        //    {
        //        if (rPG7CurrentClipLeft != 0)
        //        {
        //            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        //            {
        //                source.PlayOneShot(rPG7);
        //                GameObject PlayerBullet1 = Instantiate(rPG7Bullet, rPG7SpawnLeft.transform.position, rPG7SpawnLeft.transform.rotation);
        //                Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
        //                Vector3 directionVector = hit.point - rPG7SpawnLeft.transform.position;
        //                bulletRB.velocity = directionVector.normalized * bulletSpeed;
        //            }
        //            rPG7CurrentClipLeft--;
        //            isReloadingLeft = true;
        //            Invoke("IsReloadingLeft", 2.0f);
        //        }
        //        else if (rPG7CurrentClipLeft == 0 && rPG7Ammo != 0)
        //        {
        //            isReloadingLeft = true;
        //            Invoke("IsReloadingLeft", 2.0f);
        //        }
        //    }
        //}

        //if (Input.GetButtonDown("Fire2"))
        //{
        //    if (m107Right.activeInHierarchy == true && isReloadingRight == false)
        //    {
        //        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        //        {
        //            //source.PlayOneShot(cannonShootSFX, 0.5f);
        //            source.PlayOneShot(m107);
        //            GameObject PlayerBullet1 = Instantiate(m107Bullet, m107SpawnRight.transform.position, m107SpawnRight.transform.rotation);
        //            Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
        //            Vector3 directionVector = hit.point - m107SpawnRight.transform.position;
        //            bulletRB.velocity = directionVector.normalized * bulletSpeed;
        //        }
        //        isReloadingRight = true;
        //        Invoke("IsReloadingRight", 0.5f);
        //    }
        //    else if (bennelliM4Right.activeInHierarchy == true && isReloadingRight == false && isBennelliM4DelayRight == false)
        //    {
        //        if (bennelliM4CurrentClipRight != 0)
        //        {
        //            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        //            {
        //                source.PlayOneShot(bennelliM4);
        //                GameObject PlayerBullet1 = Instantiate(bennelliM4Bullet, bennelliM4Right.transform.position, bennelliM4SpawnRight.transform.rotation);
        //                Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
        //                Vector3 directionVector = hit.point - bennelliM4SpawnRight.transform.position;
        //                bulletRB.velocity = directionVector.normalized * bulletSpeed;
        //            }
        //            bennelliM4CurrentClipRight--;
        //            isBennelliM4DelayRight = true;
        //            Invoke("BennelliM4DelayRight", 1f);
        //        }
        //        else if (bennelliM4CurrentClipRight == 0 && bennelliM4Ammo != 0)
        //        {
        //            isReloadingRight = true;
        //            Invoke("IsReloadingRight", 6.0f);
        //        }
        //    }
        //    else if (rPG7Right.activeInHierarchy == true && isReloadingRight == false)
        //    {
        //        if (rPG7CurrentClipRight != 0)
        //        {
        //            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        //            {
        //                source.PlayOneShot(rPG7);
        //                GameObject PlayerBullet1 = Instantiate(rPG7Bullet, rPG7SpawnRight.transform.position, rPG7SpawnRight.transform.rotation);
        //                Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
        //                Vector3 directionVector = hit.point - rPG7SpawnRight.transform.position;
        //                bulletRB.velocity = directionVector.normalized * bulletSpeed;
        //            }
        //            rPG7CurrentClipRight--;
        //            isReloadingRight = true;
        //            Invoke("IsReloadingRight", 2.0f);
        //        }
        //        else if (rPG7CurrentClipRight == 0 && rPG7Ammo != 0)
        //        {
        //            isReloadingRight = true;
        //            Invoke("IsReloadingRight", 2.0f);
        //        }
        //    }
        //}
    }

    void FixedUpdate()
    {
        //if (Input.GetButton("Fire1"))
        //{
        //    if (m249Left.activeInHierarchy == true && isReloadingLeft == false && m249CurrentClipLeft != 0)
        //    {
        //        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        //        {
        //            M249FireLeft();
        //        }
        //    }
        //    else if (m249Left.activeInHierarchy == true && isReloadingLeft == false && m249CurrentClipLeft == 0 && m249Ammo != 0)
        //    {
        //        isReloadingLeft = true;
        //        Invoke("IsReloadingLeft", 5.0f);
        //    }
        //}

        //if (Input.GetButton("Fire2"))
        //{
        //    if (m249Right.activeInHierarchy == true && isReloadingRight == false && m249CurrentClipRight != 0)
        //    {
        //        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        //        {
        //            M249FireRight();
        //        }
        //    }
        //    else if (m249Right.activeInHierarchy == true && isReloadingRight == false && m249CurrentClipRight == 0 && m249Ammo != 0)
        //    {
        //        isReloadingRight = true;
        //        Invoke("IsReloadingRight", 5.0f);
        //    }
        //}
    }

    void IsReloadingLeft()
    {
        if (m249Left.activeInHierarchy == true)
        {
            if ((m249Ammo - m249Clip) >= 0)
            {
                source.PlayOneShot(m249Reload);
                m249CurrentClipLeft = m249Clip;
                m249Ammo = m249Ammo - m249Clip;
            }
            else if ((m249Ammo - m249Clip) < 0)
            {
                source.PlayOneShot(m249Reload);
                m249CurrentClipLeft = m249Ammo;
                m249Ammo = 0;
            }
        }

        if (bennelliM4Left.activeInHierarchy == true)
        {
            if ((bennelliM4Ammo - bennelliM4Clip) >= 0)
            {
                source.PlayOneShot(bennelliM4Reload);
                bennelliM4CurrentClipLeft = bennelliM4Clip;
                bennelliM4Ammo = bennelliM4Ammo - bennelliM4Clip;
            }
            else if ((bennelliM4Ammo - bennelliM4Clip) < 0)
            {
                source.PlayOneShot(bennelliM4Reload);
                bennelliM4CurrentClipLeft = bennelliM4Ammo;
                bennelliM4Ammo = 0;
            }
        }

        if (rPG7Left.activeInHierarchy == true)
        {
            if ((rPG7Ammo - rPG7Clip) >= 0)
            {
                source.PlayOneShot(rPG7Reload);
                rPG7CurrentClipLeft = rPG7Clip;
                rPG7Ammo = rPG7Ammo - rPG7Clip;
            }
            else if ((rPG7Ammo - rPG7Clip) < 0)
            {
                source.PlayOneShot(rPG7Reload);
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
                source.PlayOneShot(m249Reload);
                m249CurrentClipRight = m249Clip;
                m249Ammo = m249Ammo - m249Clip;
            }
            else if ((m249Ammo - m249Clip) < 0)
            {
                source.PlayOneShot(m249Reload);
                m249CurrentClipRight = m249Ammo;
                m249Ammo = 0;
            }
        }

        if (bennelliM4Right.activeInHierarchy == true)
        {
            if ((bennelliM4Ammo - bennelliM4Clip) >= 0)
            {
                source.PlayOneShot(bennelliM4Reload);
                bennelliM4CurrentClipRight = bennelliM4Clip;
                bennelliM4Ammo = bennelliM4Ammo - bennelliM4Clip;
            }
            else if ((bennelliM4Ammo - bennelliM4Clip) < 0)
            {
                source.PlayOneShot(bennelliM4Reload);
                bennelliM4CurrentClipRight = bennelliM4Ammo;
                bennelliM4Ammo = 0;
            }
        }

        if (rPG7Right.activeInHierarchy == true)
        {
            if ((rPG7Ammo - rPG7Clip) >= 0)
            {
                source.PlayOneShot(rPG7Reload);
                rPG7CurrentClipRight = rPG7Clip;
                rPG7Ammo = rPG7Ammo - rPG7Clip;
            }
            else if ((rPG7Ammo - rPG7Clip) < 0)
            {
                source.PlayOneShot(rPG7Reload);
                rPG7CurrentClipRight = rPG7Ammo;
                rPG7Ammo = 0;
            }
        }

        isReloadingRight = false;
    }

    void M249FireLeft()
    {
        source.PlayOneShot(m249);
        GameObject PlayerBullet1 = Instantiate(m249Bullet, m249SpawnLeft.transform.position, m249SpawnLeft.transform.rotation);
        Rigidbody bulletRB = PlayerBullet1.GetComponent<Rigidbody>();
        Vector3 directionVector = hit.point - m249SpawnLeft.transform.position;
        bulletRB.velocity = directionVector.normalized * bulletSpeed;
        m249CurrentClipLeft--;
    }

    void M249FireRight()
    {
        source.PlayOneShot(m249);
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

    public void OnLeftShootButtonPressed()
    {
        BulletFactory bulletFactory = new BulletFactory(this);
        if (m107Left.activeInHierarchy == true && isReloadingLeft == false)
            {
                if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
                {
                    //source.PlayOneShot(cannonShootSFX, 0.5f);
                    Bullet bullet = bulletFactory.createBullet("m107");
                    bullet.FireLeft();
                }
                isReloadingLeft = true;
                Invoke("IsReloadingLeft", 0.5f);
            }
            else if (bennelliM4Left.activeInHierarchy == true && isReloadingLeft == false && isBennelliM4DelayLeft == false)
            {
                if (bennelliM4CurrentClipLeft != 0)
                {
                    if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
                    {
                        Bullet bullet = bulletFactory.createBullet("bennelliM4");
                        bullet.FireLeft();
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
                        Bullet bullet = bulletFactory.createBullet("rPG7");
                        bullet.FireLeft();
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

    public void OnRightShootButtonPressed()
    {
        BulletFactory bulletFactory = new BulletFactory(this);
        if (m107Right.activeInHierarchy == true && isReloadingRight == false)
        {
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
            {
                    //source.PlayOneShot(cannonShootSFX, 0.5f);
                    Bullet bullet = bulletFactory.createBullet("m107");
                    bullet.FireRight();
                }
            isReloadingRight = true;
            Invoke("IsReloadingRight", 0.5f);
        }
        else if (bennelliM4Right.activeInHierarchy == true && isReloadingRight == false && isBennelliM4DelayRight == false)
        {
            if (bennelliM4CurrentClipRight != 0)
            {
                if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
                {
                    Bullet bullet = bulletFactory.createBullet("bennelliM4");
                    bullet.FireRight();
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
                    Bullet bullet = bulletFactory.createBullet("rPG7");
                    bullet.FireRight();
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

    public void OnBothShootButtonPressed()
    {
        OnRightShootButtonPressed();
        OnLeftShootButtonPressed();

    }
}
