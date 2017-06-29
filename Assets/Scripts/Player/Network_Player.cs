using UnityEngine;
using UnityEngine.Networking;

public class Network_Player : NetworkBehaviour {

	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;
	public Camera fpsCamera;
	public AudioListener audioListener;
	public GameObject playerBody;
	public Inventory Inventory;


	public override void OnStartLocalPlayer(){
		fpsController.enabled = true;
		Inventory.enabled = true;
		fpsCamera.enabled = true;
		audioListener.enabled = true;
		playerBody.SetActive (false);



		gameObject.name = "Local player";

		base.OnStartLocalPlayer ();
	}


}
