using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoMap;
using GoShared;

public class GOMapExtension : GOMap {

	[SerializeField] private Material regionBorder;

	private Dictionary<Vector3, GameObject> regions = new Dictionary<Vector3, GameObject> ();
	private Coordinates lastCurrentLocation;
	private float vectorSizeRegion = -1;

	private void Start() {
		locationManager.onOriginSet.AddListener((Coordinates) => {OnOriginSet(Coordinates);});
		locationManager.onLocationChanged.AddListener((Coordinates) => {OnLocationChanged(Coordinates);});
	}

	private void OnOriginSet (Coordinates currentLocation) {
		lastCurrentLocation = currentLocation;
		UpdateRegions (currentLocation);
	}

	private void OnLocationChanged (Coordinates currentLocation) {
		lastCurrentLocation = currentLocation;
		UpdateRegions (currentLocation);
	}

	private void UpdateRegions(Coordinates location) {
		if (vectorSizeRegion < 0) {
			Coordinates offsetLocation = new Coordinates (location.latitude + 0.0005556, location.longitude + 0.0005556, location.altitude);
			vectorSizeRegion = Mathf.Abs (offsetLocation.convertCoordinateToVector ().x - location.convertCoordinateToVector ().x);
		}

		Vector3 startPos = location.convertCoordinateToVector ();

		for (float x = startPos.x - 600; x < startPos.x + 700; x += vectorSizeRegion) {
			for (float z = startPos.z - 300; z < startPos.z + 800; z += vectorSizeRegion) {
				Vector3 pos = new Vector3 (x, 0f, z);
				pos.x = Mathf.Floor (pos.x / (float)vectorSizeRegion) * (int)vectorSizeRegion;
				pos.z = Mathf.Floor (pos.z / (float)vectorSizeRegion) * (int)vectorSizeRegion;

				RenderRegion (pos, vectorSizeRegion);
			}
		}
	}

	private void RenderRegion (Vector3 pos, float vectorSizeRegion) {
		if (RegionIsRendered (pos))
			return;

		List<Vector3> figure = new List<Vector3> () { (pos + Vector3.up) };
		figure.Add (new Vector3 (pos.x + vectorSizeRegion, 1f, pos.z));
		figure.Add (new Vector3 (pos.x + vectorSizeRegion, 1f, pos.z - vectorSizeRegion));
		figure.Add (new Vector3 (pos.x, 1f, pos.z - vectorSizeRegion));

		GameObject line = new GameObject ("Line " + pos);

		GOLineMesh lineMesh = new GOLineMesh (figure);
		lineMesh.width = 0.3F;
		lineMesh.load (line);

		line.GetComponent<MeshRenderer> ().material = regionBorder;

		regions.Add (pos, null);
	}

	private bool RegionIsRendered (Vector3 pos) {
		return regions.ContainsKey (pos);
	}

	public Coordinates GetCoordinateCurrentRegionID () {
		Vector3 startPos = lastCurrentLocation.convertCoordinateToVector ();

		Vector3 pos = new Vector3 (startPos.x, 0f, startPos.z);
		pos.x = Mathf.Floor (pos.x / (float)vectorSizeRegion) * (int)vectorSizeRegion;
		pos.z = Mathf.Floor (pos.z / (float)vectorSizeRegion) * (int)vectorSizeRegion;

		pos.x += vectorSizeRegion / 2F;
		pos.z += vectorSizeRegion / 2F;

		return Coordinates.convertVectorToCoordinates (pos);
	}

	public Vector3 GetOffsetFromCenterCurrentRegion () {
		Vector3 startPos = lastCurrentLocation.convertCoordinateToVector ();

		Vector3 pos = new Vector3 (startPos.x, 0f, startPos.z);
		pos.x = Mathf.Floor (pos.x / (float)vectorSizeRegion) * (int)vectorSizeRegion;
		pos.z = Mathf.Floor (pos.z / (float)vectorSizeRegion) * (int)vectorSizeRegion;

		pos.x += vectorSizeRegion / 2F;
		pos.z += vectorSizeRegion / 2F;

		Coordinates cor = Coordinates.convertVectorToCoordinates (pos);
		float latitudeOffset = 0f; //z
		float longitudeOffset = 0f; //x

		latitudeOffset = GetCoordinate.GetDistanceFromLatLonInM (cor.latitude, cor.longitude, lastCurrentLocation.latitude, cor.longitude);
		longitudeOffset = GetCoordinate.GetDistanceFromLatLonInM (cor.latitude, cor.longitude, cor.latitude, lastCurrentLocation.longitude);

		if (lastCurrentLocation.latitude < cor.latitude)
			latitudeOffset *= -1F;
		if (lastCurrentLocation.longitude < cor.longitude)
			longitudeOffset *= -1F;

		return new Vector3 (longitudeOffset, 0f, latitudeOffset);
	}
}
