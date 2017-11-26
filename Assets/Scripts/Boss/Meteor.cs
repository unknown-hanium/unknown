using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Invincible.instancei.Invin == false)
            {
                PlayerHP.instancep.currentHP -= 1f;
                float calHP = PlayerHP.instancep.currentHP / PlayerHP.instancep.maxHP;
                PlayerHP.instancep.SetHPBar(calHP);
                PlayerController.instance.isCollide = false;
            }
        }
    }
}
