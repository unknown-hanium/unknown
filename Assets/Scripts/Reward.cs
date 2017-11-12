using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward : MonoBehaviour
{
    void Update()
    {
        Text t;

        // 코인
        t = GameObject.Find("CoinText").GetComponent<Text>();
        t.text = GameObject.Find("Player").GetComponent<PlayerController>().CoinNum.ToString();

        // 포션
        t = GameObject.Find("PotionText").GetComponent<Text>();
        t.text = GameObject.Find("Player").GetComponent<PlayerController>().PotionNum.ToString();

        // 부적
        t = GameObject.Find("CharmText").GetComponent<Text>();
        t.text = GameObject.Find("Player").GetComponent<PlayerController>().CharmNum.ToString();

        // 시간
        GameObject.Find("Time").GetComponent<TimeAttack>().stop = true;
        t = GameObject.Find("Timetext").GetComponent<Text>();
        t.text = string.Format("{0:0}:{1:00}", GameObject.Find("Time").GetComponent<TimeAttack>().minutes, GameObject.Find("Time").GetComponent<TimeAttack>().seconds);
    }
}
