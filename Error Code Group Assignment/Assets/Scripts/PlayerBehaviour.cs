using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Controls")]
    public Joystick joystick;
    public float horizontalSensitivity;
    public float verticalSensitivity;
    public bool isJumpPressed;

    [Header("Player Move")]
    public CharacterController controller;
    public bool isGrounded, isDashing, isAlarmOn, isWalking, isLanding, isOutOFFuel;
    public float groundRadius = 0.5f;
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 4.0f;
    public float dashForce = 30.0f;
    public float flightForce = 0.2f;
    public float health = 100.0f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Vector3 velocity;
    public Vector3 move;
    public Vector3 dashDirection;

    [Header("Audio Clip")]
    public AudioSource audioSource;
    public AudioClip mechStartup;
    public AudioClip mechDash;
    public AudioClip mechFlight;
    public AudioClip mechJump;
    public AudioClip mechLanding;
    public AudioClip mechLowHealth;
    public AudioClip mechOutOfFuel;
    public AudioClip mechStep;

    [Header("Health Bar setting")]
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    [Header("Fuel Bar setting")]
    public FuelBar fuelBar;
    public float maxFlightFuel = 2000.0f;
    public float flightFuel;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject moveJoystick = GameObject.FindWithTag("MoveStick");
        //joystick = moveJoystick.GetComponent<Joystick>();
        audioSource.PlayOneShot(mechStartup, 0.5f);
        controller = GetComponent<CharacterController>();
        
        //HealthBar setting
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //Fuel setting
        flightFuel = maxFlightFuel;
        fuelBar.SetMaxFuel(maxFlightFuel);

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
            audioSource.PlayOneShot(mechLanding, 0.5f);
        }

        if (isGrounded && flightFuel <= (maxFlightFuel - 1))
        {
            flightFuel++;
            fuelBar.SetFuel(flightFuel);
            isOutOFFuel = false;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        //Input for WebGL and Desktop
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");

        
        float x = joystick.Horizontal;
        float z = joystick.Vertical;


        if (z > 0 && isWalking == false && isGrounded == true)
        {
            Walk();
        }
        else if (x < 0 && isWalking == false && isGrounded == true)
        {
            Walk();
        }
        else if (z < 0 && isWalking == false && isGrounded == true)
        {
            Walk();
        }
        else if (x > 0 && isWalking == false && isGrounded == true)
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
            audioSource.PlayOneShot(mechDash, 0.5f);
            dashDirection = move;
            Dash();

            Invoke("Dash", 0.25f);
        }

        if (isDashing == true)
        {
            controller.Move(dashDirection * dashForce * Time.deltaTime);
        }    
        //if(Input.GetButtonDown("Jump")){
        //    if(isGrounded){
        //        Jump();
        //    }
        //    else if(!isGrounded && flightFuel != 0.0f){
        //        audioSource.PlayOneShot(mechFlight, 0.6f);
        //    }

        //}    


        //if (Input.GetButton("Jump") && isGrounded == false && flightFuel != 0.0f)
        //{
        //    velocity.y += flightForce;
        //    flightFuel--;
        //    fuelBar.SetFuel(flightFuel);
        //}

        if (flightFuel == 0.0f && isOutOFFuel == false)
        {
            isOutOFFuel = true;
            audioSource.PlayOneShot(mechOutOfFuel, 0.5f);
        }

        if (isAlarmOn == false && health <= 25)
        {
            audioSource.PlayOneShot(mechLowHealth, 0.5f);
            isAlarmOn = true;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        audioSource.PlayOneShot(mechJump, 0.6f);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
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
        GameObject.Find("Sound Controller").SendMessage("PlayMetalImpactSFX");
        healthBar.SetHealth(currentHealth);
        FindObjectOfType<CameraController>().ShakeScreen(0.3f);
        if(currentHealth <= 0f){
            Debug.Log("Player Dead");
            SceneManager.LoadScene("Game Over Screen");
        }
    }
    
    public void OnJumpButtonPressed()
    {
        if (isGrounded)
        {
            Jump();
        }
        else if (!isGrounded && flightFuel != 0.0f)
        {
            audioSource.PlayOneShot(mechFlight, 0.6f);
        }

        if (isGrounded == false && flightFuel != 0.0f)
        {
            velocity.y += flightForce;
            flightFuel--;
            fuelBar.SetFuel(flightFuel);
        }
    }

    }
