using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    private Texture2D itemIcon;
    private string id;
    private string stats;
    private string description;
    private int count;

    public Item(string name, string stats)
    {
        this.name = name;
        this.stats = stats;
    }
}
