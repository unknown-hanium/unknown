﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAttack : MonoBehaviour {

    public float timeLeft;
    public bool stop = false;

    float minutes;
    float seconds;

    public Text text;

    private void Start()
    {
        timeLeft = 420.0f;
    }

    private void Update()
    {
        if (stop)
            return;

        minutes = Mathf.Floor(timeLeft / 60);
        seconds = timeLeft % 60;

        if (seconds > 59)
            seconds = 59;
        if(minutes < 0)
        {
            stop = true;
            minutes = 0;
            seconds = 0;
        }

        timeLeft -= Time.deltaTime;

        StartCoroutine(updateCoroutine());
    }

    IEnumerator updateCoroutine()
    {
        while (!stop)
        {
            text.text = string.Format("{0:0}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
