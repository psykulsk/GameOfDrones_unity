using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AirTrafficParser {

	public static Aircraft[] parseAirTrafficJSON(string json){
		return JsonHelper.FromJson<Aircraft> (json);
	}

}
