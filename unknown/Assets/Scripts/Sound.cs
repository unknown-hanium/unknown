using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour {
	Text te;
	public AudioSource au;

	void Start () {
		te = GetComponent<Text> ();
		te.text = "OFF";
	}

	public void OnClick () {
		if (te.text == "ON") {
			au.Play ();
			te.text = "OFF";
		} else if (te.text == "OFF") {
			au.Stop ();
			te.text = "ON";
		}
	}
}