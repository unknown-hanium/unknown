using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour {

    private float timeAfterAppear;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Invincible.instancei.Invin == false)
            {
                PlayerHP.instancep.currentHP -= 10f;
                float calHP = PlayerHP.instancep.currentHP / PlayerHP.instancep.maxHP;
                PlayerHP.instancep.SetHPBar(calHP);
                PlayerController.instance.isCollide = false;
            }
        }
    }
}
