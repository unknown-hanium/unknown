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
    public GameObject Portal;

    public float Speed;
	public float Dest;

    int MonsterCoin = 100;

    public bool isUnBeatTime = false;
    public bool PortalOpen = false;

    private int random;

    Vector3 PortalPos = new Vector3(0, 13, 0);

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
                PortalOpen = true;
                Destroy(col.gameObject);
                Destroy(gameObject);

                ItemDrop(col.transform.position);
                OpenPortal(PortalOpen, PortalPos);
            }
			Instantiate (explosion, col.transform.position, col.transform.rotation);
		}

        if (col.gameObject.GetComponent<Boss>())
        {
            Debug.Log("collision");

            BossHP.instance.currentHP -= 20f * BossHP.instance.SP;
            Debug.Log(BossHP.instance.currentHP);

            if (BossHP.instance.currentHP <= 0f)
            {
                Destroy(col.gameObject);
                Destroy(gameObject);

                ItemDrop(col.transform.position);
            }
            Instantiate(explosion, col.transform.position, col.transform.rotation);
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

    void OpenPortal(bool open, Vector3 PortalPos)
    {
        if(open == true)
        {
            Instantiate(Portal, PortalPos, Quaternion.identity);
        }
    }
}