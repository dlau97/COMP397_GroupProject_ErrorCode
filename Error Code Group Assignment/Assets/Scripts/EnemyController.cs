using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public enum Enemy {Static, Dynamic, Suicide};
    public Enemy enemyType = Enemy.Static;

    private GameObject player;

    public GameObject visionEye;

    public float detectionRange = 15f;

    private bool playerInRange = false;
    public float rotationSpeed = 4f;



    //Static Enemy variables

    //Dynamic Enemy variables

    //Suicide Enemy variables
    public float suicideMoveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange == true){
            RaycastHit hit;
            Ray visionRay = new Ray(visionEye.transform.position, player.transform.position - visionEye.transform.position); //create raycast from enemy to player
            Debug.DrawLine (visionEye.transform.position,  player.transform.position, Color.red); //Visually draw a visible red raycast line from enemy to player
            if(Physics.Raycast (visionRay, out hit, detectionRange) && (hit.transform.gameObject == player)){
                LookTowardsPlayer();
                if(enemyType == Enemy.Suicide){
                    MoveTowardPlayer();
                }
                else if(enemyType == Enemy.Static){

                }
                else if(enemyType == Enemy.Dynamic){

                }
            }
            else{
                //Debug.Log(hit,gameObject);
            }
        }
    }

    private void LookTowardsPlayer(){
        Vector3 playerDir = player.transform.position - this.transform.position;
		playerDir.y = 0f;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (playerDir), Time.deltaTime * rotationSpeed);
    }

    private void MoveTowardPlayer(){
		Debug.Log ("moving towards player");
		Vector3 moveLocation = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
		transform.position = Vector3.MoveTowards (transform.position, moveLocation, suicideMoveSpeed * Time.deltaTime);
	
	}
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == player){
            playerInRange = true;
            Debug.Log("Player In Range");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject == player){
            playerInRange = false;
            Debug.Log("Player Out of Range");
        }
    }
}
