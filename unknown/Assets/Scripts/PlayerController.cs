using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;

    public int speed;
    public float maxSpeed;

    public Vector2 dirVector;

    private Rigidbody2D rd2d;

    private Vector2 movement;

    bool isUnBeatTime = false;
    public bool isCollide = false;

    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        movement = Vector2.zero;

    }

    void FixedUpdate()
    {
        float h = joystick.GetHorizontalValue();
        float v = joystick.GetVerticalValue();

        movement.x = Mathf.Clamp(h, -maxSpeed, maxSpeed);
        movement.y = Mathf.Clamp(v, -maxSpeed, maxSpeed);

        if (movement.x != 0 && movement.y != 0)
        {
            dirVector = movement;
        }

        transform.eulerAngles = new Vector3(0, 0, -(Mathf.Atan2(dirVector.x, dirVector.y) * Mathf.Rad2Deg));

        rd2d.AddForce(movement * speed);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log("collision");

        if (col.gameObject.tag == "Enemy")
        {
            if (!isUnBeatTime)
            {
                isUnBeatTime = true;
                isCollide = true;
                StartCoroutine("UnBeatTime");
            }
        }

        if (col.gameObject.tag == "Potion")
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Charm")
        {
            Destroy(col.gameObject);
        }
    }

    IEnumerator UnBeatTime()
    {
        int countTime = 0;

        while (countTime < 6)
        {
            yield return new WaitForSeconds(0.2f);

            countTime++;
        }

        isUnBeatTime = false;

        yield return null;
    }
}