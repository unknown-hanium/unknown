using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileMover : MonoBehaviour {
	Transform this_transform;
	public GameObject explosion;
    public GameObject HPpotion;
    public GameObject Weapon;
    public GameObject Coin;

    public float Speed;
	public float Dest;

    int MonsterCoin = 100;

    public bool isUnBeatTime = false;

    private int random;

    void Start(){
		this_transform = GetComponent<Transform>();
		this_transform.Translate (0, 2, 0);

		Destroy (this.gameObject, 2.0f);
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
            MonsterHP.instance.currentHP -= 20f * MonsterHP.instance.SP;
            Debug.Log(MonsterHP.instance.currentHP);

            if (MonsterHP.instance.currentHP <= 0f)
            {
                Destroy(col.gameObject);
                Destroy(gameObject);

                ItemDrop(col.transform.position);
            }
			Instantiate (explosion, col.transform.position, col.transform.rotation);
		}

		if (col.gameObject.tag == "Background") {
			Destroy (gameObject);
		}
	}
   
    void ItemDrop(Vector3 MonsterPos)
    {
        Vector3 BeforeUIPos = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 AfterUIPos = Camera.main.ViewportToWorldPoint(BeforeUIPos);
        Vector3 RealityUIPos = new Vector3(AfterUIPos.x, AfterUIPos.y, 0);

        Instantiate(Coin, RealityUIPos, Quaternion.identity);

        random = Random.Range(0, 100);

        Debug.Log(random);

        if (random % 2 == 0)
            Instantiate(HPpotion, MonsterPos, Quaternion.identity);
        else if (random % 2 == 1)
            Instantiate(Weapon, MonsterPos, Quaternion.identity);
    }
}