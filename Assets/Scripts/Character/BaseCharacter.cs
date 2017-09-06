using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour {
    private string nickname;
    private uint gold;
    private Item[] items;
    
    public BaseCharacter(string nickname, uint gold, Item[]items) 
    {
        this.nickname = nickname;
        this.gold = gold;
    }

    public void SetItem(Item item)
    {
        
    }
}
