using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class slot : MonoBehaviour, IDropHandler {

	public GameObject item {
		get { 
			if (transform.childCount > 0) {
				return transform.GetChild (0).gameObject;
				}
			return null;
			}
	}

	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		if (!item) {
			DragHandler.itemBeingDragged.transform.SetParent (transform);	
		}

		else
		{
			Transform aux = DragHandler.itemBeingDragged.transform.parent;
			DragHandler.itemBeingDragged.transform.SetParent(transform);
			item.transform.SetParent(aux);
		}
	}

	#endregion
}
