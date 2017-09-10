using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserInfo {
    public string nickname;
    public int gold;
    public List<Item> items; 

    public void LoadItem(Item item)
    {
        items.Add(item);
    }
}
