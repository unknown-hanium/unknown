using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class shopslot : MonoBehaviour, IDropHandler {

	public GameObject item {
		get { 
			if (transform.childCount > 0) {
				return transform.GetChild (1).gameObject;
			}
			return null;
		}
	}

	#region IDropHandler implementation

	public static GameObject invenItem;

	public void OnDrop (PointerEventData eventData)
	{
		if (!item) {
			DragHandler.itemBeingDragged.transform.SetParent (transform);	
		}

		Destroy (item);
		


	}

	#endregion
}
