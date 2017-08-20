using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "trap")
        {
            System.Random r = new System.Random();
            float a = r.Next(0, 10);
            float b = r.Next(0, 10);
            gameObject.transform.position = GameObject.Find("Player").transform.position;
            gameObject.transform.position = new Vector3(a, 0, b);
        }
    }
}
