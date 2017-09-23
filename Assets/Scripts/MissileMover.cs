using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMover : MonoBehaviour {
	Transform this_transform;
	public GameObject explosion;
    public GameObject creator;

    private EnemyCreate creatorManager;

	public float Speed;
	public float Dest;

	void Start(){
		this_transform = GetComponent<Transform>();
		this_transform.Translate (0, 2, 0);
        creatorManager = GameObject.FindGameObjectWithTag("EnemyCreator").GetComponent<EnemyCreate>();
	}

	void Update(){
		this_transform.Translate (0, Speed, 0);

		if (transform.position.z >= Dest) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy") {
			Destroy (col.gameObject);
			Destroy (gameObject);
            creatorManager.EnemyCount--;
			Instantiate (explosion, col.transform.position, col.transform.rotation);
		}

		if (col.gameObject.tag == "Background") {
			Destroy (gameObject);
		}
	}
}