using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Equip_Item : NetworkBehaviour {

	//public Transform equiptItem;
	public Transform Player;
	public Transform EquiptmentHolder;
	public int currentEquipt = 0;


	void Start (){

	}

	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer) {
			return;
		}

		if(Input.GetButtonDown("Action1")){
			CmdEquipt(1);
		}

		if(Input.GetButtonDown("Action2")){
			CmdEquipt(2);
		}

		if(Input.GetButtonDown("Action3")){
			CmdEquipt(3);
		}
	}
		
	private void Equipt(int selectedTool) {

		int i = 0;
	

		foreach (Transform tools in EquiptmentHolder.transform) {
			if (i == selectedTool) {
				currentEquipt = i;
				tools.gameObject.SetActive (true);
			} else {
				tools.gameObject.SetActive (false);
			}
			i++;
		}
	}

	[Command]
	void CmdEquipt(int selectedTool){

		Equipt (selectedTool);
		RpcEquipt (selectedTool);
	
	}

	[ClientRpc]
	void RpcEquipt(int selectedTool){
		Equipt (selectedTool);
	}
		
}
