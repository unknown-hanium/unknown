using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour 
{

	public List<Item> items = new List<Item>();

	void Start()
	{
		items.Add (new Item ("Apple", 0, "체력 회복용 사과", 2, 0, Item.ItemType.Consumable));
		items.Add (new Item ("Axe", 1, "기본 도끼", 2, 0, Item.ItemType.Weapon));
		items.Add (new Item ("Power Potion", 2, "That potion that temporarily increase your power", 0, 0, Item.ItemType.Consumable));
	}

}
