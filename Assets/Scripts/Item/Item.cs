using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    private string name;
    private string stats;

    public Item(string name, string stats)
    {
        this.name = name;
        this.stats = stats;
    }
}
