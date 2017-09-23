using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO Remove warning message
// make a collider
public class EnemyCreate : MonoBehaviour {

    public GameObject EnemyParent;
    public GameObject Enemy;
    public Transform[] EnemyCreatePoint;

    public List<GameObject> EnemyList = new List<GameObject>();

    public int EnemyMaxCount = 1;
    public int EnemyCount = 0;
    public float regenDelay;

    private float PosX = 0;
    private float PosY = 0;
    private float timeForRegen=0;
 
    void Awake()
    {
        EnemyCreateOnZone();
    }

    private void EnemyCreateOnZone()
    {
        for (int i = 0; i < EnemyCreatePoint.Length; i++)
        {
            for (int j = 0; j < EnemyMaxCount; j++)
            {
                GameObject EnemyObj = (GameObject)Instantiate(Enemy);
                RandomPos(i);
                EnemyObj.transform.position = new Vector3(PosX, PosY, 0);
                EnemyObj.transform.parent = EnemyParent.transform;
                EnemyObj.name = "Enemy_" + EnemyCount;
                EnemyObj.tag = "Enemy";

                EnemyList.Add(EnemyObj);
                EnemyCount++;
            }
        }
    }

    void Update()
    {
        if(EnemyCount<EnemyMaxCount)
        {
            timeForRegen += Time.deltaTime;
            if(timeForRegen>regenDelay)
            {
                EnemyCreateOnZone();
                regenDelay += timeForRegen;
            }
        }
    }

    void RandomPos(int _AreaCount)
    {
        PosX = EnemyCreatePoint[_AreaCount].transform.position.x + Random.Range(-5, 5);
        PosY = EnemyCreatePoint[_AreaCount].transform.position.y + Random.Range(-5, 5);
    }
}
