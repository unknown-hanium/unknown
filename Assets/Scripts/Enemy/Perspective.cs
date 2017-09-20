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

    public GameObject bullet;
    public Transform bulletLocation;
    public float idleForShoot;

    private bool fireState = true;
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

    IEnumerator shoot(int count)
    {
        fireState = false;   
        for (int i = 0; i < count; i++)
        {
            // calculate direction to target
            Vector3 direction = playerTrans.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Call Animation Energy Bolt to player
            GameObject projectile = Instantiate(bullet,bulletLocation.position,bulletLocation.rotation);
            Physics2D.IgnoreCollision(projectile.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            Destroy(projectile, 6);
            yield return new WaitForSeconds(idleForShoot);
        }
        fireState = true;
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
                    enemyAi.RotateEnemy();
                    if (fireState)
                    {
                        StartCoroutine(shoot(1));
                    }
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
