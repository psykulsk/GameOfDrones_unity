using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour {

	public AirTrafficRenderer airTrafficRenderer;

	public string currentDroneJson;

	public DroneController droneController;


	// Use this for initialization
	void Start () {
		InvokeRepeating ("updateAirTrafficData", 0.5f, 0.5f);
		InvokeRepeating ("sendDroneData", 0.2f, 0.5f);
	}

	void updateAirTrafficData(){
		StartCoroutine (ServerQuery.getAirTrafficJson ((newJson) => {
		//	Debug.Log("new json: " + newJson);
			airTrafficRenderer.renderNewOverlaysFromJson(newJson);
		}));
	}

	void sendDroneData(){
		StartCoroutine(ServerQuery.postDronePosition (() => {
			return droneController.getDroneJson();
		}));
	}
}
