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


	void Start()
	{
		rd2d = GetComponent<Rigidbody2D> ();
		movement = Vector2.zero;

	}
		
	void FixedUpdate()
	{
        float h = joystick.GetHorizontalValue();
        float v = joystick.GetVerticalValue();

        movement.x = Mathf.Clamp(h, -maxSpeed, maxSpeed);
        movement.y = Mathf.Clamp(v, -maxSpeed, maxSpeed);

        if(movement.x != 0 && movement.y != 0)
        {
            dirVector = movement;
        }

        transform.eulerAngles = new Vector3(0, 0, -(Mathf.Atan2(dirVector.x, dirVector.y) * Mathf.Rad2Deg));
        

        rd2d.AddForce (movement * speed);
	}
}