using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class ItemData : NetworkBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {

	public ItemDatabase.Item item;
	public Tooltip tooltip;
	public int amount;
	public int slot;

	private Inventory inv;


	void Start(){
		inv = transform.root.GetComponent<Inventory> ();
		tooltip = transform.root.GetComponent<Tooltip> ();
	}

	public void OnBeginDrag(PointerEventData eventData){
		if (item != null) {
			this.transform.SetParent (this.transform.parent.parent);
			this.transform.position = eventData.position;
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}
	}

	public void OnDrag(PointerEventData eventData){
		if (item != null) {
			
			this.transform.position = eventData.position;

		}
	}

	public void OnEndDrag(PointerEventData eventData){
		Debug.Log (this.transform.parent);
		this.transform.SetParent (inv.slots[slot].transform);
		this.transform.position = inv.slots[slot].transform.position;
		this.GetComponent<CanvasGroup> ().blocksRaycasts = true;

	}

	public void OnPointerEnter(PointerEventData eventData){
		tooltip.Activate (item.ID);
	}

	public void OnPointerExit(PointerEventData eventData){
		tooltip.Deactivate ();
	}

}
