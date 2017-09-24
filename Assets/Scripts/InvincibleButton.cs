using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvincibleButton : MonoBehaviour {
	Button but;
	ColorBlock cb;

	void Start(){
		but = GetComponent<Button> ();
		cb = but.colors;
	}

	void Update(){
		if (GameObject.Find ("Player").GetComponent<Invincible> ().Invin == true) {
			InvinState ();
		} else {
			if (GameObject.Find ("Player").GetComponent<Invincible> ().InvinTime == true) {
				InvinTimeState ();
			} else {
				NormalState ();			
			}
		}
	}

	void NormalState(){	// 보통 상태
		Color c = Color.white;
		cb.normalColor = c;
		but.colors = cb;
	}

	void InvinState(){	// 무적 상태
		Color c = Color.blue;
		cb.normalColor = c;
		but.colors = cb;
	}

	void InvinTimeState(){	// 쿨타임 상태
		Color c = Color.red;
		cb.normalColor = c;
		but.colors = cb;
	}
}