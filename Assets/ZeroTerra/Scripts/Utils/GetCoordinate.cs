using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GetCoordinate {
	public static readonly Vector2 CENTER_OF_THE_EARTH = new Vector2 (40.866667F, 34.566667F);

	public static float GetDistanceToCenter (double lat, double lon) {
		return GetDistanceFromLatLonInM ((double)CENTER_OF_THE_EARTH.x, (double)CENTER_OF_THE_EARTH.y, lat, lon);
	}

	public static Vector3 GetOffsetFromCenterEarth (double lat, double lon) {
		return GetVectorFromLatLonInM ((double)CENTER_OF_THE_EARTH.x, (double)CENTER_OF_THE_EARTH.y, lat, lon);
	}

	public static Vector3 GetVectorFromLatLonInM (double lat1, double lon1, double lat2, double lon2) {
		float zDistance = GetDistanceFromLatLonInM (lat1, lon1, lat1, lon2);
		float xDistance = GetDistanceFromLatLonInM (lat1, lon1, lat2, lon1);
		return new Vector3 (xDistance, 0f, zDistance);
	}

	public static float GetDistanceFromLatLonInM (double lat1, double lon1, double lat2, double lon2) {
		var R = 6371;
		var dLat = Deg2Rad (lat2 - lat1);
		var dLon = Deg2Rad (lon2 - lon1);
		var a = 
			Mathf.Sin ((float)(dLat / 2)) * Mathf.Sin ((float)(dLat / 2)) +
			Mathf.Cos ((float)Deg2Rad (lat1)) * Mathf.Cos ((float)Deg2Rad (lat2)) *
			Mathf.Sin ((float)dLon / 2F) * Mathf.Sin ((float)dLon / 2F);
		var c = 2 * Mathf.Atan2 (Mathf.Sqrt (a), Mathf.Sqrt (1 - a));
		var d = R * c * 1000;
		return d;
	}

	private static double Deg2Rad (double deg) {
		return deg * (Mathf.PI / 180);
	}
}
