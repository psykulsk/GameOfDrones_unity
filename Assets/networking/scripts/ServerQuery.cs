using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public static class ServerQuery {

	public static string airTrafficEndpoint = "http://godbackend.scapp.io/mirror_recorded";

	public static IEnumerator getAirTrafficJson(System.Action<string> callback) {
		UnityWebRequest www = UnityWebRequest.Get(airTrafficEndpoint);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log (www.error);
		}
		else {

			Debug.Log(www.downloadHandler.text);

			callback(www.downloadHandler.text);
		}
	}

}
