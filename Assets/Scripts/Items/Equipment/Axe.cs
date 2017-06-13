using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Axe : NetworkBehaviour {

	public Transform cameraTransform;
	private bool canSwing;

	// Use this for initialization
	void Start () {
		canSwing = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1") & canSwing) {
			canSwing = false;
			RaycastHit hit;

			Vector3 rayPos = cameraTransform.position + (1f * cameraTransform.forward);
			if (Physics.Raycast (rayPos, cameraTransform.forward, out hit, 50f)) {
				if (hit.transform.tag == "Tree") {
					CmdHit (hit.transform.gameObject);
				}
			}
			Invoke ("SwingReset", 1f);
		}
	}

	void SwingReset(){
		canSwing = true;
	}

	[Command]
	void CmdHit(GameObject hit){
		hit.GetComponent<Trees> ().RpcDamage ();
	}
}
