using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : Sense
{
    public int FieldOfView = 45;
    public int ViewDistance = 100;

    public float maxRange;

    private Transform playerTrans;
    private Vector2 rayDirection;

    private RandomEnemyAi enemyAi;
    private Animator animator;

    protected override void Initialize()
    {
        animator = gameObject.GetComponent<Animator>();

        enemyAi = animator.gameObject.GetComponent<RandomEnemyAi>();

        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void UpdateSense()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= detectionRate)
            DetectAspect();
    }



    void DetectAspect()
    {
        rayDirection = playerTrans.position - transform.position;

        //Player와 Enemy 현재 방향 간의 각도 검사
        if ((Vector2.Angle(rayDirection, transform.up)) < FieldOfView)
        {
            Ray2D ray = new Ray2D(transform.position, rayDirection);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, maxRange, LayerMask.GetMask("Targets"));

            if (hit.collider != null)
            {
                if(hit.collider.name == "Player")
                {
                    //Debug.Log("enemy attack!!");
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        enemyAi.StartChasing();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        enemyAi.StopChasing();
    }
}
