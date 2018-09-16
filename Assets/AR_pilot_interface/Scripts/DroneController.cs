using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class DroneController : MonoBehaviour {


	public float pilotControlSpeed;
	public float rotationSpeed;

	public bool overriddenControl;

	public PilotData pilotData;

	public List<GameObject> overlays;
	public List<float> distances;

	public AirTrafficRenderer airTrafficRenderer;

	public GameObject collisionText;

	public float criticalDistance = 10000.0f;

	public Vector3 initPos;
	public Quaternion initRot;

	void Start(){
		initPos = this.transform.position;
		initRot = this.transform.rotation;
	}

	public void droneReset(){
		if (this.isActiveAndEnabled) {
			this.gameObject.SetActive (false);
			collisionText.SetActive (false);
		} else {
			this.transform.position = initPos;
			this.transform.rotation = initRot;
			this.gameObject.SetActive (true);
		}
	}

	bool collisionDetection(List<float> new_distances){
		foreach (var item in new_distances) {
			//float dist = System.Math.Abs(Vector3.Distance (item.transform.position, this.GetComponent<RectTransform>().position));
			if (item < criticalDistance) {
			//	Debug.Log ("Collision dist: " + item);
				this.GetComponentInChildren<Image> ().color = Color.yellow;
				collisionText.SetActive (true);
				return true;
			}
		}
		this.GetComponentInChildren<Image> ().color = Color.green;
		collisionText.SetActive (false);
		return false;
	}

	void collisionAvoidance(){

	}

	Aircraft getDroneData(){
		Vector3 drone_position = this.transform.position;
		Aircraft drone = new Aircraft();
		drone.alt = (int)System.Math.Round(drone_position.y + pilotData.altitude);
		drone.icao = "FFFFFF";
		drone.lat = pilotData.lantitude + helperFunctions.metersToLatitude (drone_position.x, pilotData.lantitude);
		drone.lon = pilotData.longtitude + helperFunctions.metersToLongtitude (drone_position.z, pilotData.longtitude);
		drone.speed = 0.0f;
		drone.last_update = "2018-09-14T22:42:25.898475+00:00";
		drone.heading = this.transform.rotation.eulerAngles.y;
		return drone;
	}

	public string getDroneJson(){
		Aircraft drone = getDroneData ();
		return JsonUtility.ToJson (drone);

	}

	public void updateDroneOverlay(){
		Aircraft drone = getDroneData ();
		this.GetComponentInChildren<OverlayController> ().setAircraftData (drone);
	}

	void pilotControl(){
		Vector3 pos = this.transform.localPosition;
		Quaternion rot = this.transform.localRotation;
		Vector3 angles = rot.eulerAngles;
		float multiplier = 1.0f;
//		if (CrossPlatformInputManager.GetAxis ("Horizontal") == 0.0f && CrossPlatformInputManager.GetAxis ("Vertical") == 0.0f)
//			multiplier = 1.0f / Mathf.Sqrt (2);
		if (CrossPlatformInputManager.GetAxis ("Horizontal") < 0) {
			angles.y -= multiplier * rotationSpeed;
		}	
		if (CrossPlatformInputManager.GetAxis ("Horizontal") > 0) {
			angles.y += multiplier * rotationSpeed;
		}
		if (CrossPlatformInputManager.GetAxis ("Vertical") < 0) {
			this.transform.position -= this.transform.forward * pilotControlSpeed;
			this.transform.Translate(Vector3.forward * Time.deltaTime*pilotControlSpeed);
		}
		if (CrossPlatformInputManager.GetAxis ("Vertical") > 0) {
			this.transform.position += this.transform.forward * pilotControlSpeed;
			this.transform.Translate(-Vector3.forward * Time.deltaTime*pilotControlSpeed);
		}
	//	pos.y += CrossPlatformInputManager.GetAxis ("Lift")*0.06f;
		this.transform.Translate(Vector3.up*Time.deltaTime*CrossPlatformInputManager.GetAxis("Lift")*1.2f);
		//this.transform.localPosition = pos;
		rot.eulerAngles = angles;
		this.transform.localRotation = rot;
	}

	// Update is called once per frame
	void Update () {
		// Update airplane data
	//	overlays = new List<GameObject>(airTrafficRenderer.aircraftOverlays);
		updateDroneOverlay();
		distances = new List<float>(airTrafficRenderer.distances);	
		overriddenControl = collisionDetection (distances);
		if (!overriddenControl)
			pilotControl ();
		else
			collisionAvoidance ();
	}
}
