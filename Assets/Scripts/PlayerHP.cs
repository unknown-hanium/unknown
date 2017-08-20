using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {
    public float maxHP = 100f;
    public float currentHP = 0f;
    public GameObject HPBar;

    private void Start()
    {
        currentHP = maxHP;
        InvokeRepeating("decreaseHP", 1f, 1f);
    }

    private void Update()
    {
        
    }

    void decreaseHP()
    {
        currentHP -= 10f;
        float calHP = currentHP / maxHP;
        SetHPBar(calHP);
    }

    public void SetHPBar(float myHP)
    {
        HPBar.transform.localScale = new Vector2(myHP, HPBar.transform.localScale.y);
    }
}
