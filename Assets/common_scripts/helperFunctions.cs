using System.Collections;
using System.Collections.Generic;
using System;

public static class helperFunctions {
	public static float geoCoordsToMeters(float lat1, float lon1, float height1, float lat2, float lon2, float height2){  // generally used geo measurement function
		var R = 6378.137f; // Radius of earth in KM
		var dLat = lat2 * Math.PI / 180.0f - lat1 * Math.PI / 180.0f;
		var dLon = lon2 * Math.PI / 180.0f - lon1 * Math.PI / 180.0f;
		var a = Math.Sin(dLat/2.0f) * Math.Sin(dLat/2.0f) +
			Math.Cos(lat1 * Math.PI / 180.0f) * Math.Cos(lat2 * Math.PI / 180.0f) *
			Math.Sin(dLon/2.0f) * Math.Sin(dLon/2.0f);
		var c = 2.0f * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0-a));
		var d = R * c;
		return (float)(d * 1000.0f); // meters
	}

	public static float latitudeToMeters(float latitudeDiff, float currentLatitude){
		return (float)(111132.92f - 559.82f * Math.Cos (2.0f * currentLatitude) + 1.175f * Math.Cos (4.0f * currentLatitude) - 0.0023f * Math.Cos (6.0f * currentLatitude));
	}

	public static float longtitudeToMeters(float longtitudeDiff, float currentLatitude){
		return (float)(111412.84f * Math.Cos (currentLatitude) - 93.5f * Math.Cos (3.0f * currentLatitude) + 0.118f * Math.Cos (5.0f * currentLatitude));
	}
}
