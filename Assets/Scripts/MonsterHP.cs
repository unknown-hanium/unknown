using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHP : MonoBehaviour {
    float maxHP = 100f;
    public float currentHP = 0f;
    public float SP = 1f;

    public static MonsterHP instance;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        currentHP = maxHP;
    }
}
