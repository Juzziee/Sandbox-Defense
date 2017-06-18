﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler {

	public int id;
	private Inventory inv;

	void Start(){
		inv = transform.root.GetComponent<Inventory> ();
	}
	public void OnDrop(PointerEventData eventData){
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData> ();
		if (inv.items [id].ID == -1) {
			droppedItem.transform.SetParent (this.transform);
			droppedItem.transform.position = this.transform.position;
			inv.items [droppedItem.slot] = new ItemDatabase.Item ();
			inv.items [id] = droppedItem.item;
			droppedItem.slot = id;
		} else {
			Transform item = this.transform.GetChild (0);
			item.GetComponent<ItemData> ().slot = droppedItem.slot;
			item.transform.SetParent (inv.slots [droppedItem.slot].transform);
			item.transform.position = inv.slots [droppedItem.slot].transform.position;

			droppedItem.slot = id;
			droppedItem.transform.SetParent (this.transform);
			droppedItem.transform.position = this.transform.position;

			inv.items [droppedItem.slot] = item.GetComponent<ItemData> ().item;
			inv.items [id] = droppedItem.item;
		}
	}
}
