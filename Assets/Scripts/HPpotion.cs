using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPpotion : MonoBehaviour
{
    public int HPpotionNum = 10;    // 포션이 10개가 있다고 가정

    float currentHP = 0f;
    float maxHP = 100f;

    public void UseHPpotion()
    {
        currentHP = GameObject.Find("Player").GetComponent<PlayerHP>().currentHP;
        maxHP = GameObject.Find("Player").GetComponent<PlayerHP>().maxHP;

        if (HPpotionNum > 0)
        {
            if (80 < currentHP && currentHP < 100)
            {
                currentHP = 100f;

                float calHP = currentHP / maxHP;
                GameObject.Find("Player").GetComponent<PlayerHP>().SetHPBar(calHP);
                HPpotionNum -= 1;
            }
            else if (GameObject.Find("Player").GetComponent<PlayerHP>().currentHP < 81)
            {
                currentHP += 20f;

                float calHP = currentHP / maxHP;
                GameObject.Find("Player").GetComponent<PlayerHP>().SetHPBar(calHP);
                HPpotionNum -= 1;
            }
        }
        GameObject.Find("Player").GetComponent<PlayerHP>().currentHP = currentHP;
        GameObject.Find("Player").GetComponent<PlayerHP>().maxHP = maxHP;
    }
}
