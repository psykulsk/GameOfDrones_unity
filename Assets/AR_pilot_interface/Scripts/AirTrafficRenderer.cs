using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirTrafficRenderer : MonoBehaviour {

	string testData = "{\"Items\": [{\"icao\": \"4B18E8\", \"lat\": 47.0128287463, \"lon\": 7.71295242012, \"alt\": 4876, \"speed\": 146.61654000000001, \"heading\": 51.71, \"last_update\": \"2018-09-14T22:53:51.613492+00:00\"}, {\"icao\": \"4CA2AA\", \"lat\": 47.581281783, \"lon\": 5.98600569174, \"alt\": 10972, \"speed\": 196.517608, \"heading\": -66.44, \"last_update\": \"2018-09-14T22:53:51.620481+00:00\"}, {\"icao\": \"484C5A\", \"lat\": 48.5388486949, \"lon\": 6.34890241338, \"alt\": 11582, \"speed\": 225.840916, \"heading\": -13.45, \"last_update\": \"2018-09-14T22:53:51.622557+00:00\"}, {\"icao\": \"3C6664\", \"lat\": 46.5590154727, \"lon\": 10.039639348, \"alt\": 10058, \"speed\": 237.158684, \"heading\": 142.7, \"last_update\": \"2018-09-14T22:53:51.629038+00:00\"}, {\"icao\": \"44082B\", \"lat\": 47.4009459149, \"lon\": 9.23782945082, \"alt\": 10418, \"speed\": 251.563116, \"heading\": 79.06, \"last_update\": \"2018-09-14T22:53:51.644333+00:00\"}, {\"icao\": \"3964EB\", \"lat\": 46.1151586361, \"lon\": 5.29412588289, \"alt\": 10363, \"speed\": 215.03759200000002, \"heading\": -35.34, \"last_update\": \"2018-09-14T22:53:51.644627+00:00\"}, {\"icao\": \"4CAA63\", \"lat\": 46.2310850782, \"lon\": 6.10396360321, \"alt\": 383, \"speed\": 5.14444, \"heading\": 152.2, \"last_update\": \"2018-09-14T22:53:51.677045+00:00\"}, {\"icao\": \"396663\", \"lat\": 47.5936405559, \"lon\": 6.92248004683, \"alt\": 12190, \"speed\": 202.690936, \"heading\": -74.01, \"last_update\": \"2018-09-14T22:53:51.677661+00:00\"}, {\"icao\": \"4B17E3\", \"lat\": 47.5614132797, \"lon\": 8.14512641184, \"alt\": 2616, \"speed\": 115.7499, \"heading\": -32.74, \"last_update\": \"2018-09-14T22:53:51.677926+00:00\"}, {\"icao\": \"4CA813\", \"lat\": 46.6949772883, \"lon\": 4.77253274168, \"alt\": 10363, \"speed\": 207.320932, \"heading\": -28.1, \"last_update\": \"2018-09-14T22:53:51.679576+00:00\"}, {\"icao\": \"3C66AE\", \"lat\": 47.6522400706, \"lon\": 8.57595062743, \"alt\": 10365, \"speed\": 243.332012, \"heading\": 26.28, \"last_update\": \"2018-09-14T22:53:51.710793+00:00\"}, {\"icao\": \"3415CE\", \"lat\": 47.5941812419, \"lon\": 9.70652416479, \"alt\": 8777, \"speed\": 262.36644, \"heading\": 73.7, \"last_update\": \"2018-09-14T22:53:51.712908+00:00\"}, {\"icao\": \"4408EC\", \"lat\": 46.0713103911, \"lon\": 8.63916876992, \"alt\": 11273, \"speed\": 219.153144, \"heading\": -168.8, \"last_update\": \"2018-09-14T22:53:51.748836+00:00\"}, {\"icao\": \"4B1A1C\", \"lat\": 45.1338319717, \"lon\": 7.32793683862, \"alt\": 9146, \"speed\": 240.759792, \"heading\": 126.7, \"last_update\": \"2018-09-14T22:53:51.749182+00:00\"}, {\"icao\": \"4CA51F\", \"lat\": 45.5686044968, \"lon\": 6.92056106063, \"alt\": 9441, \"speed\": 216.06648, \"heading\": 135.3, \"last_update\": \"2018-09-14T22:53:51.787603+00:00\"}, {\"icao\": \"44A98F\", \"lat\": 47.6912641399, \"lon\": 6.25386020052, \"alt\": 6163, \"speed\": 217.609812, \"heading\": 4.09, \"last_update\": \"2018-09-14T22:53:51.787931+00:00\"}, {\"icao\": \"44058F\", \"lat\": 47.2812110135, \"lon\": 9.46342675581, \"alt\": 8229, \"speed\": 182.62762, \"heading\": 106.6, \"last_update\": \"2018-09-14T22:53:51.788184+00:00\"}, {\"icao\": \"400AFF\", \"lat\": 45.206821325, \"lon\": 7.46668663392, \"alt\": 10058, \"speed\": 239.730904, \"heading\": 138.2, \"last_update\": \"2018-09-14T22:53:51.788429+00:00\"}, {\"icao\": \"3991E5\", \"lat\": 47.3362713888, \"lon\": 7.40452995843, \"alt\": 10955, \"speed\": 207.835376, \"heading\": -83.0, \"last_update\": \"2018-09-14T22:53:51.828883+00:00\"}]}";

	public Aircraft[] aircrafts;

	public List<GameObject> aircraftOverlays;

	public Dictionary<string, GameObject> aircraftsAndObjects;

	public PilotData pilotData;

	public GameObject aircraftOverlayPrefab;

	public float distanceThreshold = 1000000.0f;

	private void drawAircraftOverlays(){
	// destroy old overlays
		foreach (var item in aircraftOverlays) {
			Destroy (item.gameObject);
		}
		foreach (var aircraft in aircrafts) {
			float xDiff = helperFunctions.latitudeToMeters (aircraft.lat - pilotData.lantitude, pilotData.lantitude);
			float zDiff = helperFunctions.longtitudeToMeters (aircraft.lon - pilotData.longtitude, pilotData.lantitude);
			float heightDiff = aircraft.alt - pilotData.altitude;
			float distance = Mathf.Sqrt ((float)(zDiff * zDiff + xDiff * xDiff));
			if (distance < distanceThreshold) {
				float scale = distance / 6000.0f;
				GameObject newOverlay = (GameObject)Instantiate (aircraftOverlayPrefab, this.gameObject.transform); 
				newOverlay.GetComponent<RectTransform> ().localPosition = new Vector3 (xDiff, heightDiff, zDiff); 
				newOverlay.GetComponent<RectTransform> ().localScale = new Vector3 (scale, scale);
				aircraftOverlays.Add (newOverlay);
				newOverlay.GetComponent<OverlayController> ().setAircraftData (aircraft);
			}
		}
	}

	// Use this for initialization
	void Start () {
	}

	public void renderNewOverlaysFromJson(string json){
		aircrafts = AirTrafficParser.parseAirTrafficJSON (json);
		drawAircraftOverlays ();
	}

	// Update is called once per frame
	void Update () {
		
	}


}
