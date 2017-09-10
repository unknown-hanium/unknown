using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
	public GameObject PlayerMissile;    // 복제할 미사일 오브젝트
	public Transform MissileLocation;   // 미사일이 발사될 위치
	public float FireDelay;             // 미사일 발사 속도(미사일이 날라가는 속도x)
	private bool FireState;             // 미사일 발사 속도를 제어할 변수

	void Start (){
		FireState = true;
	}

	public void Shoot(){
		if (FireState) {
			StartCoroutine (FireCycleControl ());
			Instantiate (PlayerMissile, MissileLocation.position, MissileLocation.rotation);
		}
	}

	IEnumerator FireCycleControl(){
		FireState = false;
		yield return new WaitForSeconds(FireDelay);
		FireState = true;
	}
}