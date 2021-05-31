using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public CharacterController controller;
    public bool isGrounded;
    public bool isDashing;
    public float groundRadius = 0.5f;
    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 4.0f;
    public float dashForce = 30.0f;
    public float flightForce = 0.2f;
    public float maxFlightFuel = 2000.0f;
    public float flightFuel = 2000.0f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Vector3 velocity;
    public Vector3 move;
    public Vector3 dashDirection;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && flightFuel <= (maxFlightFuel-1))
        {
            flightFuel++;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (isDashing == false)
        {
            move = transform.right * x + transform.forward * z;
            controller.Move(move * maxSpeed * Time.deltaTime);
        }

        if (isDashing == false && Input.GetButtonUp("Left Shift") && isGrounded && move != new Vector3(0,0,0))
        {
            Debug.Log("Dash");
            dashDirection = move;
            Dash();
            Invoke("Dash", 1);
        }     

        if (isDashing == true)
        {
            controller.Move(dashDirection * dashForce * Time.deltaTime);            
        }

        if (Input.GetButtonUp("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        if (Input.GetButton("Jump") && isGrounded == false && flightFuel != 0.0f)
        {          
            velocity.y += flightForce;
            flightFuel--;
        }
       
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
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
}
