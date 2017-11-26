using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour {
    float maxHP = 100f;
    public float currentHP = 0f;
    public float SP = 1f;

    public static BossHP instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHP = maxHP;
    }
}
