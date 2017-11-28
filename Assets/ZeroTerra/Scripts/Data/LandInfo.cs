using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LandConsts {
	public static double landsInDegree = 1800; // degree = 3600 seconds, land heigh/wight = 2 seconds, 3600 / 2 = 1800
	public static double maxLatitude = 90;
	public static double maxLongitude = 180;

	public static int maxLatitudeIndex = 324000;
	public static int maxLongitudeIndex = 647999;// 648000 - 1, because 0 = 360

	public static double oneSecond  = 0.00027777778;
	public static double twoSeconds = 0.00055555556;
}

public class LandInfo {

	private Vector3 offsetFromCenter = Vector3.zero;

	public string LandId { get; private set; }
	public double LatitudeIndex { get; private set; }
	public double LongitudeIndex { get; private set; }
	public double Latitude { get; private set; }
	public double Longitude { get; private set; }
	public Vector3 OffsetFromCenter { get { return offsetFromCenter; } } // offset for placing scene center

	public LandInfo() {
	}

	public LandInfo(string landId) {
		Init (landId);
	}

	public LandInfo(double lat, double lon) {
		Init (lat, lon);
	}

	public void Init(string landId) {
		string[] arr = landId.Split ('H');
		Init ( int.Parse(arr[0]), int.Parse(arr[1]));
	}

	public void Init(double lat, double lon) {
		LatitudeIndex = GetLatitudeIndex (lat);
		LongitudeIndex = GetLongitudeIndex (lon);

		LandId = LatitudeIndex.ToString() + "H" + LongitudeIndex.ToString();
		Latitude = LatitudeIndex / LandConsts.landsInDegree - LandConsts.maxLatitude;
		Longitude = LongitudeIndex / LandConsts.landsInDegree - LandConsts.maxLongitude;

		offsetFromCenter = GPSEncoder.GPSToUCS (new Vector2 ((float)lat, (float)lon)) -
			GPSEncoder.GPSToUCS (new Vector2 ((float)Latitude, (float)Longitude));
	}

	public bool Equls(LandInfo info) {
		return this.LatitudeIndex.Equals (info.LatitudeIndex) && this.LongitudeIndex.Equals (info.LongitudeIndex);
	}

	public bool Equls(double lat, double lon) {
		int latitudeIndex = GetLatitudeIndex (lat);
		int longitudeIndex = GetLongitudeIndex (lon);

		return this.LatitudeIndex.Equals (latitudeIndex) && this.LongitudeIndex.Equals (longitudeIndex);
	}

	public string ToString() {
		return "LandID = " + LandId + "\n" +
		"LatitudeIndex = " + LatitudeIndex.ToString () + "\n" +
		"LongitudeIndex = " + LongitudeIndex.ToString () + "\n" +
		"Latitude = " + Latitude.ToString () + "\n" +
		"Longitude = " + Longitude.ToString () + "\n" +
		"OffsetFromCenter = " + OffsetFromCenter.ToString ();
	}

	#region calculations

	private int GetLatitudeIndex(double lat) {
		int latIndex = Mathf.RoundToInt((float)((LandConsts.maxLatitude + lat) * LandConsts.landsInDegree));
		latIndex = Mathf.Clamp (latIndex, 0, LandConsts.maxLatitudeIndex);
		return latIndex;
	}

	private int GetLongitudeIndex(double lon) {
		int lonIndex = Mathf.RoundToInt((float)((LandConsts.maxLongitude + lon) * LandConsts.landsInDegree));

		if (lonIndex > LandConsts.maxLongitudeIndex) {
			lonIndex = 0;
		}

		lonIndex = Mathf.Clamp (lonIndex, 0, LandConsts.maxLongitudeIndex);
		return lonIndex;
	}

	#endregion
}

[Serializable]
public class LandScene {
	public string id;
	public GameObject scene;
}