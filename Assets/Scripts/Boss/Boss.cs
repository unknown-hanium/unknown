using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    enum Pattern { None,Hail,MeteorFall,Earthquake}
    
    public GameObject bolt;
    public GameObject meteor;
    public GameObject bigMeteor;
    public GameObject earthquake;
    
    public int meteorCount;
    public int timeForDestroy;
    // entry time for patternChange() method
    public float waitForPattern;
    public float idleForShoot;
    // when pattern was end, wait time for next pattern 
    public float waitForNextPattern;
    public float termForHail;

    private GameObject target;
    private Animator anim;
    private Pattern pattern;
    private bool alive;// TODO after apply health system, if boss have not hp then alive convert to false

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        pattern = Pattern.None;
        alive = true;
        target= GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Attack());
	}


    IEnumerator Attack()
    {
        while (alive)
        {
            int shootCount = Random.Range(10, 15);
            StartCoroutine(shoot(shootCount));
            // Shoot and then waiting time for next pattern
            yield return new WaitForSeconds(waitForPattern);
            patternChange();
            switch (pattern)
            {
                case Pattern.Hail:
                    for (int i = 0; i < meteorCount; i++)
                    {
                        hail();
                        yield return new WaitForSeconds(termForHail);
                    }
                    break;
                case Pattern.MeteorFall:
                    meteorFall();
                    break;
                case Pattern.Earthquake:
                    Earthquake();
                    break;
            }
            yield return new WaitForSeconds(waitForNextPattern);
        }
    } 

    void patternChange()
    {
        pattern = (Pattern)Random.Range(1, 4);
    }

    IEnumerator shoot(int count)
    {
        anim.SetBool("isTriggeredHail", false);
        anim.SetBool("isTriggeredFallMeteor", false);
        anim.SetBool("isTriggeredEarthquake", false);
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(idleForShoot);
            // calculate direction to target
            Vector3 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Call Animation Energy Bolt to player
            GameObject bullet = Instantiate(bolt);
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            Destroy(bullet, 6);
        } 
    }
    void hail()
    {
        // get Random position for rain
        Vector3 RandomPos = new Vector3(Random.Range(-41, 31), Random.Range(-17, 42), 0);
        // Call Hail and repeat 20 per 0.1f to random position
        GameObject hail = Instantiate(meteor);
        hail.transform.position = RandomPos;
        //Debug.Log("is Hailing");
        anim.SetBool("isTriggeredHail", true);
        // TODO add a falling hail animation
            // after animation apply collision

        Destroy(hail, timeForDestroy);
    }

    void meteorFall()
    {
        GameObject BigMeteor = Instantiate(bigMeteor);
        BigMeteor.transform.position = target.transform.position;
        anim.SetBool("isTriggeredFallMeteor", true);
        // TODO add a falling meteor animation
            // after animation apply collision
        Destroy(BigMeteor, timeForDestroy);
    }

    void Earthquake()
    {
        GameObject earth = Instantiate(earthquake);
        earth.transform.position = new Vector2(transform.position.x, transform.position.y - 3.0f);
        anim.SetBool("isTriggeredEarthquake", true);
        // TODO add a split ground animation
            // after animation apply collision
        Destroy(earth, timeForDestroy);
    }

    
}
