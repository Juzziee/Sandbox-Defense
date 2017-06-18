using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


public class Inventory : MonoBehaviour {

	ItemDatabase database;
	GameObject invetoryPanel;
	GameObject slotPanel;
	public GameObject inventorySlot;
	public GameObject inventoryItem;
	public GameObject player;

	public FirstPersonController fpController;


	int slotAmount;
	public List<ItemDatabase.Item> items = new List<ItemDatabase.Item> ();
	public List<GameObject> slots = new List<GameObject>();

	void Start(){
		database = GetComponent<ItemDatabase> ();

		fpController.m_MouseLook.SetCursorLock (true);
		fpController.m_MouseLook.SetRotLock (false);

		slotAmount = 16;

		invetoryPanel = this.transform.Find ("Inventory Panel").gameObject;
		slotPanel = invetoryPanel.transform.Find ("Slot Panel").gameObject;
		fpController = this.GetComponent<FirstPersonController> ();


		for (int i = 0; i < slotAmount; i++) {
			items.Add (new ItemDatabase.Item());
			slots.Add (Instantiate (inventorySlot));
			slots [i].GetComponent<ItemSlot> ().id = i;
			slots[i].transform.SetParent(slotPanel.transform);
		}
		invetoryPanel.GetComponent<Canvas> ().enabled = true;
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
		}
			
	}

	void ToggleInventory(){
		if(invetoryPanel.GetComponent<Canvas>().enabled == true){
			// turn off inv
			fpController.m_MouseLook.SetCursorLock (true);
			fpController.m_MouseLook.SetRotLock (false);
			invetoryPanel.GetComponent<Canvas>().enabled = false;

		} else {
			// turn on inv
			fpController.m_MouseLook.SetCursorLock (false);
			fpController.m_MouseLook.SetRotLock (true);
			invetoryPanel.GetComponent<Canvas> ().enabled = true;
		}

	}
	
}
