using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class DroneController : MonoBehaviour {


	public float pilotControlSpeed = 0.01f;

	public bool overriddenControl;



	bool collisionDetection(){
		return false;
	}

	void collisionAvoidance(){

	}

	void pilotControl(){
		Vector3 pos = this.transform.position;
		if (CrossPlatformInputManager.GetAxis ("Horizontal") < 0) {
			pos.x -= pilotControlSpeed;
		}
		if (CrossPlatformInputManager.GetAxis ("Horizontal") > 0) {
			pos.x += pilotControlSpeed;
		}
		if (CrossPlatformInputManager.GetAxis ("Vertical") < 0) {
			pos.z -= pilotControlSpeed;
		}
		if (CrossPlatformInputManager.GetAxis ("Vertical") > 0) {
			pos.z += pilotControlSpeed;
		}
		pos.y += CrossPlatformInputManager.GetAxis ("Lift")*0.1f;
		this.transform.position = pos;
	}

	// Update is called once per frame
	void Update () {
		overriddenControl = collisionDetection ();
		if (!overriddenControl)
			pilotControl ();
		else
			collisionAvoidance ();
	}
}
