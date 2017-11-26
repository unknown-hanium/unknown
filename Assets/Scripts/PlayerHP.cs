using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {
    public float maxHP = 100f;
    public float currentHP = 0f;

    public GameObject HPBar;
    public GameObject Reward;

    public static PlayerHP instancep;

    private void Awake()
    {
        instancep = this;
    }

    private void Start()
    {
        currentHP = maxHP;
    }

    public void Update()
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
        if (currentHP >= 0)
            HPBar.transform.localScale = new Vector2(HPBar.transform.localScale.x, myHP);
    }

    public void GameOver()
    {
        Reward.SetActive(true);
    }
}