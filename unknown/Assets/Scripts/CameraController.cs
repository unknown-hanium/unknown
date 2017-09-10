using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject Player;
    public float xLimit; 
    public float yLimit;

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(Screen.width, Screen.height, true);
    }

    void LateUpdate() {
        Vector3 pos = new Vector3(0, 0 ,-1);

        pos.x = Mathf.Clamp(Player.transform.position.x, -xLimit, xLimit);
        pos.y = Mathf.Clamp(Player.transform.position.y, -yLimit, yLimit);

        transform.position = pos;
	}
}
