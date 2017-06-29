using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Tooltip : NetworkBehaviour {

	public ItemDatabase database;
	public GameObject tooltip;
	private string tooltipText;
	public ItemDatabase.Item item;


	public void Activate(int id){
		ItemDatabase.Item item = database.FetchItemByID (id);
		UpdateText (item);

		tooltip.SetActive (true);
	}

	public void Deactivate(){
		tooltip.SetActive (false);
	}

	public void UpdateText(ItemDatabase.Item item){
		tooltip.transform.GetChild (0).GetComponent<Text> ().text = tooltipText;
		tooltipText = "<b>" + item.Title + "</b>\n\n" + item.Description;

	}

}
