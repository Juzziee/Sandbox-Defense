using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enable_Axe : MonoBehaviour {

	void OnEnable(){
		transform.root.GetComponent<Axe> ().enabled = true;
	}

	void OnDisable(){
		transform.root.GetComponent<Axe> ().enabled = false;
	}
}
