using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPpotion : MonoBehaviour {
    Button bt;
    ColorBlock cb;
    public bool CooldownTime;
    public bool IncreaseSP;

    public int SPpotionNum = 10;

    void Start()
    {
        bt = GetComponent<Button>();
        cb = bt.colors;
        CooldownTime = false;
        IncreaseSP = false;
    }

    void Update()
    {
        if (IncreaseSP == true)
        {
            IncreaseSPState();
        }
        else
        {
            if (CooldownTime == true)
            {
                CooldownTimeState();
            }
            else
            {
                NormalState();
            }
        }
    }

    public void ClickButton()
    {
        if (CooldownTime == false)
        {
            IncreaseSP = true;
            Invoke("NotIncreaseSP", 2);

            CooldownTime = true;
            Invoke("NotCooldownTime", 5);
        }
    }

    void NotIncreaseSP()
    {
        IncreaseSP = false;
    }

    void NotCooldownTime()
    {
        CooldownTime = false;
    }

    void NormalState()
    {
        Color c = Color.white;
        cb.normalColor = c;
        bt.colors = cb;
    }

    void IncreaseSPState()
    {
        MonsterHP.instance.SP = 1.5f;

        Color c = Color.blue;
        cb.normalColor = c;
        bt.colors = cb;
    }

    void CooldownTimeState()
    {
        MonsterHP.instance.SP = 1f;

        Color c = Color.red;
        cb.normalColor = c;
        bt.colors = cb;
    }
}
