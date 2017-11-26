using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour, IDropHandler {


	public static GameObject invenItem;


	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		invenItem = eventData.pointerDrag.gameObject;

		if (invenItem.tag == "item") {
			Destroy (invenItem);
			Debug.Log ("Plus 100 Gold");
		}


	}
	#endregion
}
