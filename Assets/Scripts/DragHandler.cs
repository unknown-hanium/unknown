using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject itemBeingDragged;
	Vector3 startPosition;
	Transform startParent;

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		if(itemBeingDragged.tag == "shopitem"){
			itemBeingDragged = Instantiate (itemBeingDragged,startParent);
		}
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		itemBeingDragged.transform.position = Input.mousePosition;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		if(itemBeingDragged.tag == "shopitem"){
			itemBeingDragged.tag = "item";
		}
		itemBeingDragged = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (transform.parent != null) {
			transform.position = startPosition;
		}
	}

	#endregion



}
