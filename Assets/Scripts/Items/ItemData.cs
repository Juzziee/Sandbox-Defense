using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {

	public ItemDatabase.Item item;
	public int amount;
	public int slot;

	private Inventory inv;


	void Start(){
		inv = transform.root.GetComponent<Inventory> ();
	}

	public void OnBeginDrag(PointerEventData eventData){

		Debug.Log ("Drag Begin");

		if (item != null) {
			Debug.Log ("hit");
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
		
		this.transform.SetParent (inv.slots[slot].transform);
		this.transform.position = inv.slots[slot].transform.position;
		this.GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	public void OnPointerEnter(PointerEventData eventData){
		Debug.Log ("Mouse enter");
	}

	public void OnPointerExit(PointerEventData eventData){
		//tooltip.Deactivate (item);
		Debug.Log("Mouse Exit");
	}

}
