using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Calculate RandomPos ->Clear
//Move To RandomPos
//Wait for Seconds 3f
//and then repeat
public class RandomEnemyAi : MonoBehaviour
{
    // Where is the player
    private Transform playerTransform;

    private Rigidbody2D rigid;


    // FSM related variables
    private Animator animator;
    bool chasing = false;
    bool waiting = false;
    private float distanceFromTarget;
    public bool inViewCone;

    // Where is it going and how fast?
    Vector3 direction;
    private float walkSpeed = 1f;
    private float rotateSpeed = 10f;
    private int currentTarget;
    

    private Vector3 RandomPos;
    private bool isMoving = false;
    // This runs when the zombie is added to the scene
    private void Awake()
    {
        // Get a reference to the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        rigid = GetComponent<Rigidbody2D>();
        // Get a reference to the FSM (animator)
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        // If chasing get the position of the player and point towards it
        if (chasing)
        {
            direction = playerTransform.position - transform.position;

            RotateEnemy();
        }     
    }

    private void FixedUpdate()
    {
        // Give the values to the FSM (animator)
        distanceFromTarget = Vector3.Distance(RandomPos, transform.position);
        animator.SetFloat("distanceFromWaypoint", distanceFromTarget);
        animator.SetBool("playerInSight", inViewCone);


        // Unless the zombie is waiting then move
        if (!waiting)
        {
            rigid.AddForce(direction * walkSpeed);
        }
    }

    private void CalculateRandomPos()
    {
        float PosX = transform.position.x + Random.Range(-5.5f, 5.5f);
        float PosY = transform.position.y + Random.Range(-5.5f, 5.5f);

        RandomPos = new Vector3(PosX, PosY, 0);
    }

    public void SetNextPoint()
    {
        // Pick a random waypoint 
        // But make sure it is not the same as the last one
       
        do
        {
            StartCoroutine("Idle");
        } while (RandomPos == transform.position);

        // Load the direction of the next waypoint
        direction = RandomPos - transform.position;
        RotateEnemy();
    }

    IEnumerator Idle()  
    {
        CalculateRandomPos();
        
        yield return new WaitForSeconds(3);
    }

    public void StopChasing()
    {
        inViewCone = false;
        chasing = false;
    }

    private void RotateEnemy()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion tempRotate = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        
        transform.rotation= Quaternion.Slerp(transform.rotation, tempRotate, rotateSpeed * Time.deltaTime);
        
    }

    public void StartChasing()
    {
        inViewCone = true;
        chasing = true;
    }

    public void ToggleWaiting()
    {
        waiting = !waiting;
    }
}