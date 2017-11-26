using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public Vector2 dirVector;
    private Vector2 movement;
    private Rigidbody2D rd2d;
    public static PlayerController instance;

    public int speed;
    public float maxSpeed;

    public bool isUnBeatTime = false;
    public bool isCollide = false;

    public int CoinNum = 0;
    public int PotionNum = 0;
    public int CharmNum = 0;

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
            PotionNum++;
        }

        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
            CoinNum++;
        }

        if (col.gameObject.tag == "Charm")
        {
            Destroy(col.gameObject);
            CharmNum++;
        }

        if(col.gameObject.tag == "Portal")
        {
            SceneManager.LoadScene("Unkown_Boss");
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