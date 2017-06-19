using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Trees : NetworkBehaviour {

	private int treeHealth = 5;
	public Rigidbody treeBody;


	void Damage(){
		treeHealth -= 1;
		if(treeHealth <= 0){
			TreeFall ();
		}
	}

	void TreeFall(){
		treeBody.isKinematic = false;
		treeBody.AddForce (transform.right * 80f);
		Invoke ("TreeDespawn", 15f);
	}

	void TreeDespawn(){
		Destroy (this.gameObject);
	}

	[ClientRpc]
	public void RpcDamage(){
		Damage ();
	}
}
