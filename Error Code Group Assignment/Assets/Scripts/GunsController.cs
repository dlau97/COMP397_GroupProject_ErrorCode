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
	public int bennelliM4Clip = 999;
	public int bennelliM4CurrentClipLeft = 999;
	public int bennelliM4CurrentClipRight =999;
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

	public BulletPool bulletPool;
	public BulletFactory bulletFactory;

	public class BulletPool 
	{
		public List<GameObject> m107Pool = new List<GameObject>();
		public List<GameObject> bennelliM4Pool = new List<GameObject>();
		public List<GameObject> rPG7Pool = new List<GameObject>();

		public BulletPool(GameObject m107Bullet, GameObject bennelliM4Bullet, GameObject rPG7Bullet)
		{
			for (int i = 0; i < 10; i++)
			{
				GameObject m107 = Instantiate(m107Bullet);
				GameObject bennelliM4 = Instantiate(bennelliM4Bullet);
				GameObject rPG7 = Instantiate(rPG7Bullet);

				m107.SetActive(false);
				bennelliM4.SetActive(false);
				rPG7.SetActive(false);

				m107Pool.Add(m107);
				bennelliM4Pool.Add(bennelliM4);
				rPG7Pool.Add(rPG7);
			}
		}

		public GameObject getBulletFromPool(List<GameObject> pool)
		{
			for(int i = 0; i < pool.Count; i++)
			{
				if(!pool[i].activeInHierarchy)
				{
					return pool[i];
				}
			} 

			return null;
		}

		public GameObject getM107Bullet()
		{
			return getBulletFromPool(m107Pool);	
		}

		public GameObject getBennelliM4Bullet()
		{
			return getBulletFromPool(bennelliM4Pool);
		}

		public GameObject getrPG7Bullet()
		{
			return getBulletFromPool(rPG7Pool);
		}

	}

	public abstract class Bullet
	{
		public GameObject bullet;
		public float speed;
		public AudioSource audioSource;
		public AudioClip bulletSound;


		public Bullet(
			GameObject bullet, 
			float speed,
			AudioSource audioSource,
			AudioClip bulletSound
		)
		{
			this.bullet = bullet;
			this.speed = speed;
			this.audioSource = audioSource;
			this.bulletSound = bulletSound;
		}

		public void PlaySound()
		{
			audioSource.PlayOneShot(bulletSound);
		}

		protected void Fire(Vector3 position, Quaternion rotation, Vector3 targetPosition)
		{
			if (bullet == null) return;
			bullet.SetActive(true);
			PlaySound();
			bullet.transform.position = position;
			bullet.transform.rotation = rotation;
			Rigidbody rigidBody = bullet.GetComponent<Rigidbody>();
			Vector3 direction = targetPosition - position;
			rigidBody.velocity = direction.normalized * speed;
		}

		public abstract void FireRight();
		public abstract void FireLeft();
	}

	public class M107Bullet : Bullet
	{
		private GunsController gunsController;

		public M107Bullet(GunsController gunsController) : base(
			gunsController.bulletPool.getM107Bullet(),
			gunsController.bulletSpeed,
			gunsController.source,
			gunsController.m107
		)
		{
			this.gunsController = gunsController;
		}

		public override void FireLeft()
		{
			Fire(
				gunsController.m107SpawnLeft.transform.position,
				gunsController.m107SpawnLeft.transform.rotation,
				gunsController.hit.point
			);
		}
		public override void FireRight()
		{
			Fire(
				gunsController.m107SpawnRight.transform.position,
				gunsController.m107SpawnRight.transform.rotation,
				gunsController.hit.point
			);
		}
	}

	public class BennelliM4Bullet : Bullet
	{
		private GunsController gunsController;
		public BennelliM4Bullet(GunsController gunsController) : base(
			gunsController.bulletPool.getBennelliM4Bullet(),
			gunsController.bulletSpeed,
			gunsController.source,
			gunsController.bennelliM4
		)
		{ }

		public override void FireLeft()
		{
			Fire(
				gunsController.bennelliM4SpawnLeft.transform.position,
				gunsController.bennelliM4SpawnLeft.transform.rotation,
				gunsController.hit.point
			);
		}

		public override void FireRight()
		{
			Fire(
				gunsController.bennelliM4SpawnRight.transform.position,
				gunsController.bennelliM4SpawnRight.transform.rotation,
				gunsController.hit.point
			);
		}
	}

	public class RPG7Bullet : Bullet
	{
		private GunsController gunsController;
		public RPG7Bullet(GunsController gunsController) : base(
			gunsController.bulletPool.getrPG7Bullet(),
			gunsController.bulletSpeed,
			gunsController.source,
			gunsController.rPG7
		)
		{ }

		public override void FireLeft()
		{
			Fire(
				gunsController.rPG7SpawnLeft.transform.position,
				gunsController.rPG7SpawnLeft.transform.rotation,
				gunsController.hit.point
			);
		}

		public override void FireRight()
		{
			Fire(
				gunsController.rPG7SpawnRight.transform.position,
				gunsController.rPG7SpawnRight.transform.rotation,
				gunsController.hit.point
			);
		}
	}


	public class BulletFactory
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


	void Start()
	{
		bulletPool = new BulletPool(
			m107Bullet,
			bennelliM4Bullet,
			rPG7Bullet
		);
		bulletFactory = new BulletFactory(this);
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
