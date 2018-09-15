using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class DroneController : MonoBehaviour {


	public float pilotControlSpeed = 0.01f;

	public bool overriddenControl;

	public PilotData pilotData;


	bool collisionDetection(){
		return false;
	}

	void collisionAvoidance(){

	}

	public string getDroneJson(){
		Vector3 drone_position = this.transform.position;
		Aircraft drone = new Aircraft();
		drone.alt = (int)drone_position.y;
		drone.icao = "FFFFFF";
		drone.lat = pilotData.lantitude + helperFunctions.metersToLatitude (drone_position.x, pilotData.lantitude);
		drone.lon = pilotData.longtitude + helperFunctions.metersToLongtitude (drone_position.z, pilotData.longtitude);
		drone.speed = 0.0f;
		drone.last_update = "2018-09-14T22:42:25.898475+00:00";
		drone.heading = 0.0f;

		return JsonUtility.ToJson (drone);

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
