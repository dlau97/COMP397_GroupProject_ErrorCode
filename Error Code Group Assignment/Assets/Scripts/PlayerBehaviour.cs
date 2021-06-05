using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public CharacterController controller;
    public bool isGrounded, isDashing, isAlarmOn, isWalking, isLanding, isOutOFFuel;
    public float groundRadius = 0.5f;
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 4.0f;
    public float dashForce = 30.0f;
    public float flightForce = 0.2f;
    public float maxFlightFuel = 2000.0f;
    public float flightFuel = 2000.0f;
    public float health = 100.0f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Vector3 velocity;
    public Vector3 move;
    public Vector3 dashDirection;

    public AudioSource audioSource;
    public AudioClip mechStartup;
    public AudioClip mechDash;
    public AudioClip mechFlight;
    public AudioClip mechJump;
    public AudioClip mechLanding;
    public AudioClip mechLowHealth;
    public AudioClip mechOutOfFuel;
    public AudioClip mechStep;

    //HealthBar script
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(mechStartup);
        controller = GetComponent<CharacterController>();
        
        //HealthBar setting
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded == false)
        {
            isLanding = true;
        }

        if (isLanding == true && isGrounded == true)
        {
            isLanding = false;
            audioSource.PlayOneShot(mechLanding);
        }

        if (isGrounded && flightFuel <= (maxFlightFuel - 1))
        {
            flightFuel++;
            isOutOFFuel = false;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey("w") && isWalking == false && isGrounded == true)
        {
            Walk();
        }
        else if (Input.GetKey("a") && isWalking == false && isGrounded == true)
        {
            Walk();
        }
        else if (Input.GetKey("s") && isWalking == false && isGrounded == true)
        {
            Walk();
        }
        else if (Input.GetKey("d") && isWalking == false && isGrounded == true)
        {
            Walk();
        }

        if (isDashing == false)
        {
            move = transform.right * x + transform.forward * z;
            controller.Move(move * maxSpeed * Time.deltaTime);
        }

        
        if (isDashing == false && Input.GetKeyUp(KeyCode.LeftShift) && isGrounded && move != new Vector3(0, 0, 0))
        {
            audioSource.PlayOneShot(mechDash);
            dashDirection = move;
            Dash();

            Invoke("Dash", 0.25f);
        }

        if (isDashing == true)
        {
            controller.Move(dashDirection * dashForce * Time.deltaTime);
        }        
        
        if (Input.GetButtonUp("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            audioSource.PlayOneShot(mechJump);
        }

        if (Input.GetButtonDown("Jump") && isGrounded == false && flightFuel != 0.0f)
        {
            audioSource.PlayOneShot(mechFlight);

        }

        if (Input.GetButton("Jump") && isGrounded == false && flightFuel != 0.0f)
        {
            velocity.y += flightForce;
            flightFuel--;
        }

        if (flightFuel == 0.0f && isOutOFFuel == false)
        {
            isOutOFFuel = true;
            audioSource.PlayOneShot(mechOutOfFuel);
        }

        if (isAlarmOn == false && health <= 25)
        {
            audioSource.PlayOneShot(mechLowHealth);
            isAlarmOn = true;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Control HealthBar
        // if(Input.GetMouseButtonDown(0))
        // {
        //     TakeDamage(10);
        // }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            health--;
        }
    }

    void Dash()
    {
        if (isDashing == true)
        {
            isDashing = false;
        }
        else
        {
            isDashing = true;
        }
    }

    void Walk()
    {
        WalkStatus();
        audioSource.PlayOneShot(mechStep);
        Invoke("WalkStatus", 1.0f);
    }

    void WalkStatus()
    {
        if (isWalking == false)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    //Health bar control
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    
     
}
