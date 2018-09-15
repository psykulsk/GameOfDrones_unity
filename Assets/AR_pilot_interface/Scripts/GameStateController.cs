using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour {

	public AirTrafficRenderer airTrafficRenderer;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("updateAirTrafficData", 1.0f, 1.0f);
	}

	void updateAirTrafficData(){
		StartCoroutine (ServerQuery.getAirTrafficJson ((newJson) => {
		//	Debug.Log("new json: " + newJson);
			airTrafficRenderer.renderNewOverlaysFromJson(newJson);
		}));
	}
}
