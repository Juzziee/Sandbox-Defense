using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Networking;


public class Inventory : NetworkBehaviour {

	ItemDatabase database;
	GameObject invetoryPanel;
	GameObject slotPanel;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpController;

	int invAmount;
	int actionAmount;

	public List<ItemDatabase.Item> items = new List<ItemDatabase.Item> ();
	public List<GameObject> slots = new List<GameObject>();

	GameObject actionPanel;
	GameObject skillPanel;
	public GameObject actionSlot;
	public GameObject actionItem;

	public List<GameObject> actionSlots = new List<GameObject>();
	public List<GameObject> actionbar = new List<GameObject>();

	void Start(){
		database = GetComponent<ItemDatabase> ();

		fpController.m_MouseLook.SetCursorLock (true);


		// Inventory slots
		invAmount = 16;
		actionAmount = 7;

		invetoryPanel = this.transform.Find ("Inventory Panel").gameObject;
		slotPanel = invetoryPanel.transform.Find ("Slot Panel").gameObject;


		for (int i = 0; i < invAmount; i++) {
			items.Add (new ItemDatabase.Item());
			slots.Add (Instantiate (inventorySlot));
			slots [i].gameObject.name = "InvSlot " + i;
			slots [i].GetComponent<ItemSlot> ().id = i;
			slots[i].transform.SetParent(slotPanel.transform);
		}
		invetoryPanel.GetComponent<Canvas> ().enabled = false;

		// Action Bar
		actionPanel = this.transform.Find ("Action Bar").gameObject;
		skillPanel = actionPanel.transform.Find ("Action Slot").gameObject;

		for (int i = 0; i < actionAmount; i++) {
			actionbar.Add (Instantiate (actionSlot));
			actionbar [i].gameObject.name = "Actionbar " + i;
			actionbar [i].GetComponent<ActionSlot> ().id = i;
			actionbar [i].transform.SetParent(skillPanel.transform);
		}
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
					itemObj.GetComponent<ItemData> ().item = itemToAdd;
					itemObj.GetComponent<ItemData> ().slot = i;
					itemObj.transform.SetParent (slots [i].transform);
					itemObj.GetComponent<Image> ().sprite = itemToAdd.Sprite;
					itemObj.transform.localPosition = Vector2.zero;

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

		if (Input.GetKeyDown (KeyCode.T)) {
			AddItem (0);
			AddItem (2);
			AddItem (1);
			AddItem (4);
		}
			
	}

	void ToggleInventory(){
		if(invetoryPanel.GetComponent<Canvas>().enabled == true){
			// turn off inv
			fpController.m_MouseLook.SetCursorLock (true);
			fpController.SetCameraLock (false);
			invetoryPanel.GetComponent<Canvas>().enabled = false;

		} else {
			// turn on inv
			fpController.m_MouseLook.SetCursorLock (false);
			fpController.SetCameraLock (true);
			invetoryPanel.GetComponent<Canvas> ().enabled = true;
		}

	}
	
}
