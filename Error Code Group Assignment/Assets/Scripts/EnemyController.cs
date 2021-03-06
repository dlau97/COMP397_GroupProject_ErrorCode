using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Runtime.Serialization;

public class EnemyController : MonoBehaviour
{

    public enum Enemy
    {
        [EnumMember(Value = "Static")]
        Static,

        [EnumMember(Value = "Dynamic")]
        Dynamic,

        [EnumMember(Value = "Suicide")] 
        Suicide
    };
    public Enemy enemyType = Enemy.Static;

    private GameObject player;

    public GameObject visionEye;

    public float detectionRange = 15f;

    private bool playerInRange = false;
    public float rotationSpeed = 4f;

    public float health = 50f;



    //Static Enemy variables
    public Transform shootLocation;
    public GameObject enemyBullet;
    public float shootDelay = 1f;
    private float shootStartTime;

    public float bulletSpeed = 16f;

    public NavMeshAgent agent;
    public Animator animator;

    //Dynamic Enemy variables

    //Suicide Enemy variables
    public float suicideMoveSpeed = 5f;

    public GameObject explosionFX;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInRange = false;
        shootStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange == true){
            RaycastHit hit;
            Ray visionRay = new Ray(visionEye.transform.position, player.transform.position - visionEye.transform.position); //create raycast from enemy to player
            Debug.DrawLine (visionEye.transform.position,  player.transform.position, Color.red); //Visually draw a visible red raycast line from enemy to player
            if(Physics.Raycast (visionRay, out hit, detectionRange) && (hit.transform.gameObject == player)){
                
                if(enemyType == Enemy.Suicide){
                    SuicideMoveTowardsPlayer();
                    if(GetDistanceToPlayer() <= 2.5f){
                        player.SendMessage("TakeDamage", 20f);
                        GameObject.Find("Game Manager").SendMessage("EnemyKill");
                        Instantiate(explosionFX, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                        GameObject.Find("Sound Controller").SendMessage("PlayEnemyExplodeSFX");
                        Destroy(this.gameObject);
                    }
                }
                else if(enemyType == Enemy.Static){
                    LookTowardsPlayer();
                    ShootBullet();
                }
                else if(enemyType == Enemy.Dynamic){
                    LookTowardsPlayer();
                    ShootBullet();
                    DynamicMoveTowardPlayer();
                    if(GetDistanceToPlayer() <= 2.5f){
                        player.SendMessage("TakeDamage", 20f);
                        Destroy(this.gameObject);
                    }
                }
            }
            else{
                //Debug.Log(hit,gameObject);
                if(enemyType == Enemy.Suicide){
                    animator.SetBool("Running", false);
                }

            }
        }
    }

    private void ShootBullet(){
        if(Time.time >= shootStartTime + shootDelay){ //Ensure it only shoots after delay
            //Debug.Log("Shooting");
            GameObject.Find("Sound Controller").SendMessage("PlayEnemyShootSFX");
            GameObject bulletPrefab = Instantiate(enemyBullet, shootLocation.position, Quaternion.Euler (new Vector3 (90f, 0f, 0f)));
            Rigidbody bulletRB = bulletPrefab.GetComponent<Rigidbody>();
            Vector3 directionVector = player.transform.position - shootLocation.position;
            bulletRB.velocity = directionVector.normalized * bulletSpeed;
            shootStartTime = Time.time;

        }

    }

    private void LookTowardsPlayer(){
        Vector3 playerDir = player.transform.position - this.transform.position;
		//playerDir.y = 0f;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (playerDir), Time.deltaTime * rotationSpeed);
    }

    private void SuicideMoveTowardsPlayer(){
        animator.SetBool("Running", true);
        agent.SetDestination(player.transform.position);
    }

    private void DynamicMoveTowardPlayer(){
		//Debug.Log ("moving towards player");
		Vector3 moveLocation = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
		transform.position = Vector3.MoveTowards (transform.position, moveLocation, suicideMoveSpeed * Time.deltaTime);
	}

    private float GetDistanceToPlayer(){
        float distance = 0f;
        Vector3 playerLocation = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        distance = Vector3.Distance(playerLocation, this.transform.position);

        return Mathf.Abs(distance);
    }

    public void TakeDamage(float damage){
        health -= damage;
        GameObject.Find("Sound Controller").SendMessage("PlayMetalImpactSFX");
        if(health <= 0f){
            Instantiate(explosionFX, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            GameObject.Find("Sound Controller").SendMessage("PlayEnemyExplodeSFX");
            GameObject.Find("Game Manager").SendMessage("EnemyKill");
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject == player){
            playerInRange = true;
            //Debug.Log("Player In Range");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject == player){
            playerInRange = false;
            Debug.Log("Player Out of Range");
        }
    }

}
