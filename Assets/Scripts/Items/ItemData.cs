using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public ItemDatabase.Item item;
	public int amount;

	public void OnBeginDrag(PointerEventData eventData){

		if (item != null) {
			Debug.Log (item.Title);
			this.transform.position = eventData.position;
		}
	}

	public void OnDrag(PointerEventData eventData){

	}

	public void OnEndDrag(PointerEventData eventData){

	}

}
