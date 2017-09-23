﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
	public string itemName;
	public int itemID;
	public string itemDesc;
	public Texture2D itemIcon;
	public int itemPower;
	public int itemSpeed;
	public ItemType itemType;

	public enum ItemType 
	{
		Weapon, 
		Usable,
		Consumable,
		Chest,
		Head,
		Cloves,
		Legs,
		Shoes,
		Quest
	}

	public Item(string name, int id, string desc, int power, int speed, ItemType type)
	{
		itemName = name;
		itemID = id;
		itemDesc = desc;
		itemIcon = Resources.Load<Texture2D> ("Item Icons/" + name);
		itemPower = power;
		itemSpeed = speed;
		itemType = type;
	}

	public Item()
	{
		itemID = -1;
	}


}
