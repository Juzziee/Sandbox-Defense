﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour {

	ItemDatabase database;
	GameObject invetoryPanel;
	GameObject slotPanel;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	int slotAmount;
	public List<ItemDatabase.Item> items = new List<ItemDatabase.Item> ();
	public List<GameObject> slots = new List<GameObject>();

	void Start(){
		database = GetComponent<ItemDatabase> ();

		slotAmount = 16;

		invetoryPanel = this.transform.Find ("Inventory Panel").gameObject;
		slotPanel = invetoryPanel.transform.Find ("Slot Panel").gameObject;


		for (int i = 0; i < slotAmount; i++) {
			items.Add (new ItemDatabase.Item());
			slots.Add (Instantiate (inventorySlot));
			slots[i].transform.SetParent(slotPanel.transform);
		}


		AddItem (0);
		AddItem (0);
		AddItem (0);
		AddItem (0);
		AddItem (0);
		AddItem (0);
		AddItem (0);
		AddItem (0);
		AddItem (0);
		AddItem (0);
		AddItem (0);
		AddItem (0);
		AddItem (0);
		
		AddItem (1);
		AddItem (2);
		AddItem (3);
	}

	public void AddItem(int id){
		ItemDatabase.Item itemToAdd = database.FetchItemByID (id);
		if (itemToAdd.Stackable && itemExists (itemToAdd)) {
			for (int i = 0; i < items.Count; i++) {
				if (items [i].ID == id) {
					ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
					data.amount++;
					data.transform.GetChild (0).GetComponent<Text> ().text = data.amount.ToString ();
					//Debug.Log ("Item Name: " + itemToAdd.Title + " | data amount: " + data.amount);
					break;
				}
			}
		} else {

			for (int i = 0; i < items.Count; i++) {
				if (items [i].ID == -1) {
					items [i] = itemToAdd;
					GameObject itemObj = Instantiate (inventoryItem);
					itemObj.transform.SetParent (slots [i].transform);
					itemObj.GetComponent<Image> ().sprite = itemToAdd.Sprite;
					itemObj.transform.position = Vector2.zero;
					break;
				}
			}
		}
	}

	bool itemExists(ItemDatabase.Item item){
		for (int i = 0; i < items.Count; i++) {
			if (items [i].ID == item.ID) {
				ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
				if (data.amount >= item.stackSize) {
					//Increment I count
				} else {
					return true;
				}
			}
		}
		return false;
	}

	void Update(){

		if (Input.GetButtonDown ("Inventory")) {
			ToggleInventory ();
		}
	}

	void ToggleInventory(){
		
		if(invetoryPanel.GetComponent<Canvas>().enabled == true){
			// turn off inv
			invetoryPanel.GetComponent<Canvas>().enabled = false;

		} else {
			// turn on inv
			invetoryPanel.GetComponent<Canvas> ().enabled = true;
		}

	}
	
}
