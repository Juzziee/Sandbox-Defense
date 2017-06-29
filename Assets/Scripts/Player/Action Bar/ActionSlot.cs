using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class ActionSlot : NetworkBehaviour {

	public int id;
	private Inventory inv;

	void Start(){
		inv = transform.root.GetComponent<Inventory> ();
	}

	public void OnDrop(PointerEventData eventData){
		ActionData droppedItem = eventData.pointerDrag.GetComponent<ActionData> ();
		if (inv.items [id].ID == -1) {
			droppedItem.transform.SetParent (this.transform);
			droppedItem.transform.position = this.transform.position;
			inv.items [droppedItem.slot] = new ItemDatabase.Item ();
			inv.items [id] = droppedItem.item;
			droppedItem.slot = id;
		} else if(droppedItem.slot != id) {
			Transform item = this.transform.GetChild (0);
			item.GetComponent<ActionData> ().slot = droppedItem.slot;
			item.transform.SetParent (inv.actionSlots [droppedItem.slot].transform);
			item.transform.position = inv.actionSlots [droppedItem.slot].transform.position;

			droppedItem.slot = id;
			droppedItem.transform.SetParent (this.transform);
			droppedItem.transform.position = this.transform.position;

			inv.items [droppedItem.slot] = item.GetComponent<ItemData> ().item;
			inv.items [id] = droppedItem.item;
		}
	}
}
