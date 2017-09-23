using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public int slotsX, slotsY;
	public GUISkin skin;
	public List<Item> inventory = new List<Item>();
	public bool showInventory;
	//public List<Item> slots = new List<Item>();
	private ItemDatabase database;
	private bool showTooltip;
	private string tooltip;

	private bool draggingItem;
	private Item draggedItem;
	private int prevIndex;

	void Start()
	{
		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatabase> ();
		for(int i = 0; i < (slotsX * slotsY); i++)
		{
			inventory.Add (new Item ());
		}
	

		AddItem (1);		
		AddItem (0);
		AddItem (2);

		showInventory = !showInventory;
	}


	void OnGUI() // item Name GUI
	{
		/*
		if (GUI.Button (new Rect (80, 230, 100, 40), "Save"))
			SaveInventory ();
		if (GUI.Button (new Rect (80, 280, 100, 40), "Load"))
			LoadInventory ();
		*/
		tooltip = "";
		GUI.skin = skin;

		if(showInventory)
		{
			DrawInventory ();
			if (showTooltip) 
			{
				GUI.Box (new Rect (20, 100, 250, 200), tooltip, skin.GetStyle ("Tooltip"));
			}
		}

		if(draggingItem)
		{
			GUI.DrawTexture (new Rect (Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
		}

		/*
		for(int i = 0; i < inventory.Count; i++)
		{
			GUI.Label(new Rect(10, i * 20, 200, 50), inventory[i].itemName);

		}
		*/
	}

	void DrawInventory()
	{
		Event e = Event.current;
		int i = 0;
		for(int y = 0; y < slotsY; y++)
		{
			for(int x = 0; x < slotsX; x++)
			{
				Rect saveSlotRect = new Rect (80 + x * (35 + 10), 50 + y * (45 + 10), 40, 40);
				GUI.Box (saveSlotRect, "", skin.GetStyle("Slot"));	

				Item item = inventory [i];
				if (item.itemName != null) 
				{
					if (item.itemIcon != null)	
					{
						GUI.DrawTexture (saveSlotRect, inventory [i].itemIcon);
					
						if (saveSlotRect.Contains(e.mousePosition)) 
						{
							tooltip = CreateTooltip (inventory [i]);
							showTooltip = true;
						}

						if (e.button == 0 && e.type == EventType.mouseDrag && !draggingItem) { //left mouseButton
						draggingItem = true;
						prevIndex = i;
						draggedItem = inventory [i];
						inventory [i] = new Item ();

						}
						if (e.type == EventType.mouseUp && draggingItem) {
							inventory [prevIndex] = inventory [i];
							inventory [i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
						if (e.isMouse && e.type == EventType.mouseDown && e.button == 1) 
						{
							if (item.itemType == Item.ItemType.Consumable)
								UseConsumable (item, i, true);
						}
					}
				} 
				else
				{
					if(saveSlotRect.Contains (e.mousePosition))
					{
						if(e.type == EventType.mouseUp && draggingItem)
						{
							inventory [i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				}
				if(tooltip == "")
				{
					showTooltip = false;
				}
				i++;
			}
		}

	}

	string CreateTooltip(Item item)
	{
		tooltip = item.itemName + "\n\n" + item.itemDesc + "\n\n" + "power : "+ item.itemPower;
		return tooltip;
	}

	void RemoveItem(int id)
	{
		for (int i = 0; i < inventory.Count; i++) 
		{
			if(inventory[i].itemID == id)
			{
				inventory [i] = new Item ();
				break;
			}
		}
	}
	void AddItem(int id)
	{
		for(int i = 0; i < inventory.Count; i++)
		{
			if(inventory[i].itemName == null)
			{
				for(int j = 0; j < database.items.Count; j++)
				{
					if(database.items[j].itemID == id)
					{
						inventory [i] = database.items [j];						
					}
				}
					
				break;
			}
		}
	}

	bool InventoryContains(int id)
	{
		foreach (Item item in inventory)
			if (item.itemID == id)	return true;
		return false;
	}

	private void UseConsumable(Item item, int slot, bool delete)
	{
		switch (item.itemID) 
		{
		case 2: 
		{
				print ("Use Consumable: " + item.itemName);
				break;
		}
		}
	}



	void SaveInventory()
	{
		for(int i = 0; i < inventory.Count; i++)
		{
			PlayerPrefs.SetInt("Inventory " + i, inventory[i].itemID);
		}
	}

	void LoadInventory()
	{
		for (int i = 0; i < inventory.Count; i++) 
		{
			inventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >= 0 ? database.items[PlayerPrefs.GetInt("Inventory" + i)] : new Item() ;
		}
	}
}
