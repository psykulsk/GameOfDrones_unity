using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System.Text;

public static class ServerQuery {

	public static string airTrafficEndpoint = "http://godbackend.scapp.io/full_data";
	public static string droneDataEndpoint = "http://godbackend.scapp.io/post_drone_position";

	public static IEnumerator getAirTrafficJson(System.Action<string> callback) {
		UnityWebRequest www = UnityWebRequest.Get(airTrafficEndpoint);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		}
		else {

		//	Debug.Log(www.downloadHandler.text);

			callback(www.downloadHandler.text);
		}
	}

	public static IEnumerator postDronePosition(System.Func<string> callback){
		string drone_data_json = callback ();	
		//Debug.Log (drone_data_json);
		byte[] bytes = Encoding.UTF8.GetBytes(drone_data_json);
		UnityWebRequest www = new UnityWebRequest (droneDataEndpoint, UnityWebRequest.kHttpVerbPOST);
		UploadHandlerRaw uploadHandler = new UploadHandlerRaw (bytes);
		www.uploadHandler = uploadHandler;
		www.SetRequestHeader ("Content-Type", "application/json");

		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		}
		else {
		}
	}

}
