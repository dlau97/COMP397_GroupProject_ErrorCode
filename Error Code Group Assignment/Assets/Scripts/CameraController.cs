using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Controls")]
    public Joystick joystick;

    public float horizontalSensitivity;
    public float verticalSensitivity;


    public float mouseSensitivity = 500.0f;
    public Transform playerBody;

    private float XRotation = 0.0f;

    private bool shaking = false;
	private float shakeDuration = 1f;
	public float shakeIntensity = 0.5f;
	public AnimationCurve decreaseFactor;
	private float startTime;
	public Transform cameraTransform;
	private Vector3 original_pos;

    private Vector3 initialPos; 



    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        initialPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        float mouseX = joystick.Horizontal;
        float mouseY = joystick.Vertical;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(XRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);

        //Screenshake Code

        original_pos = cameraTransform.position;
		if (shaking == true) {

			float t = (Time.time - startTime) / shakeDuration; 
			float amount = shakeIntensity * decreaseFactor.Evaluate (t);
			Vector3 randomVector = Random.insideUnitCircle;

			cameraTransform.position = original_pos + randomVector * amount;

			if(Time.time >= startTime + shakeDuration){
				cameraTransform.localPosition = initialPos;
				shaking = false;
			}

		}
    }
    public void ShakeScreen(float time){
        if(shaking == false){
            shakeDuration = time;
            startTime = Time.time;
            shaking = true;
        }
    }
}
