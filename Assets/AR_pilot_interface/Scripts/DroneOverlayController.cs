using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneOverlayController : MonoBehaviour {

	public Text icoa;
	public Text longtitude;
	public Text latitude;
	public Text altitude;
	public Text speed;
	public Text heading;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = this.transform.position;
		float distance = (float)System.Math.Sqrt (pos.x * pos.x + pos.z * pos.z);
		float scale = distance / 6000.0f;
		this.GetComponent<RectTransform> ().rotation = Quaternion.LookRotation (Camera.main.transform.forward);
		this.GetComponent<RectTransform> ().localScale = new Vector3 (scale, scale);
	}

	public void setAircraftData(Aircraft aircraft){
		icoa.text = "ICOA: " + aircraft.icao;
		longtitude.text = "LON: " + aircraft.lon.ToString();
		latitude.text = "LAT: " + aircraft.lat.ToString();
		altitude.text = "ALT: " + aircraft.alt.ToString();
		speed.text = "SP: " + aircraft.speed.ToString();
		heading.text = "HEAD: " + aircraft.heading.ToString();
	}
}
