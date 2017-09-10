using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMover : MonoBehaviour {
    Transform target;

    public float speed;
    public float dest;
    // Use this for initialization
    void Start () {
        target = GetComponent<Transform>();
        target.Translate(0, 2, 0);
	}
	
	// Update is called once per frame
	void Update () {
        target.Translate(0, speed, 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            // TODO if col.gameObject.tag is player then apply damage to player
            Destroy(gameObject);
        }
        else if(col.gameObject.tag=="Background")
        {
            Destroy(gameObject);
        }
    }
}
