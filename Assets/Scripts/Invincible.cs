using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour {
	public bool Invin;
	public bool InvinTime;

    public static Invincible instancei;

    private void Awake()
    {
        instancei = this;
    }

    void Start(){
		Invin = false;
		InvinTime = false;
	}

	public void ClickInvin() {
		if (InvinTime == false) {
			Invin = true;
			Invoke ("FalseInvin", 2);	// 지속 시간 2초

			InvinTime = true;
			Invoke ("FalseInvinTime", 5);	// 쿨타임 3초 + 지속 시간 2초
		}
	}

	void FalseInvin()	{
		Invin = false;
	}

	void FalseInvinTime(){
		InvinTime = false;
	}
}