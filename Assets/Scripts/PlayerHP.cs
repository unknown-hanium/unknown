using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {
    public float maxHP = 100f;
    public float currentHP = 0f;

    public GameObject HPBar;
    public GameObject Reward;

    private void Start()
    {
        currentHP = maxHP;
    }

    private void Update()
    {
        if (PlayerController.instance.isCollide == true && currentHP > 0)
        {
			if (GetComponent<Invincible> ().Invin == false) {
				currentHP -= 10f;
				float calHP = currentHP / maxHP;
				SetHPBar (calHP);
				PlayerController.instance.isCollide = false;
			}
        }
        else if(currentHP <= 0)
        {
            GameOver();
        }
	}

    public void SetHPBar(float myHP)
    {
        HPBar.transform.localScale = new Vector2(HPBar.transform.localScale.x, myHP);
    }

    void GameOver()
    {
        Reward.SetActive(true);
    }
}