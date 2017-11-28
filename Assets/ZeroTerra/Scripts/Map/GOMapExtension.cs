using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoMap;
using GoShared;
using TMPro;

public class GOMapExtension : GOMap {

	[SerializeField] private GameObject landIdTextPrefab;

	[SerializeField] private Material regionBorder;
	[SerializeField] private Material regionBorderHighLight;

	[SerializeField] private TextMeshProUGUI landId;
	[SerializeField] private TextMeshProUGUI landCoords;
	[SerializeField] private TextMeshProUGUI playerCoords;
	[SerializeField] private TextMeshProUGUI centerOffset;

	private Dictionary<string, GameObject> regions = new Dictionary<string, GameObject> ();

	private void Start() {
		locationManager.onOriginSet.AddListener((Coordinates) => {OnOriginSet(Coordinates);});
		locationManager.onLocationChanged.AddListener((Coordinates) => {OnLocationChanged(Coordinates);});
	}

	private void OnOriginSet (Coordinates currentLocation) {
		UpdateRegions (currentLocation);
	}

	private void OnLocationChanged (Coordinates currentLocation) {
		UpdateRegions (currentLocation);
	}

	private void UpdateRegions(Coordinates location) {

		UserData.Instance.LandInfo.Init (location.latitude, location.longitude);

		landId.text = UserData.Instance.LandInfo.LandId;
		landCoords.text = UserData.Instance.LandInfo.Latitude.ToString ("###.000000") + ", " + UserData.Instance.LandInfo.Longitude.ToString ("###.000000");
		playerCoords.text = location.latitude.ToString ("###.000000") + ", " + location.longitude.ToString ("###.000000");
		centerOffset.text = (UserData.Instance.LandInfo.OffsetFromCenter * -1f).ToString ();

		LandInfo li = new LandInfo ();
		Vector3 pos = Vector3.zero;
		Vector3 borderOffset = Vector3.zero;
		Vector3 landOffset = Vector3.zero;

		for (int indLat = -8; indLat <= 8; ++indLat) {
			for (int indLon = -8; indLon <= 8; ++indLon) {

				li.Init (location.latitude + indLat * LandConsts.twoSeconds, location.longitude + indLon * LandConsts.twoSeconds );
				pos = GPSEncoder.GPSToUCS (new Vector2 ((float)li.Latitude, (float)li.Longitude));
				borderOffset = GPSEncoder.GPSToUCS (new Vector2 ((float)(li.Latitude + LandConsts.oneSecond), (float)(li.Longitude + LandConsts.oneSecond)));
				borderOffset -= pos;

				RenderRegion (li.LandId, pos, borderOffset, indLat == 0 && indLon == 0);
			}
		}
	}

	private void RenderRegion(string landId, Vector3 pos, Vector3 borderOffset, bool isSelected) {

		GameObject line = null;

		if (!regions.ContainsKey (landId)) {
			List<Vector3> figure = new List<Vector3> ();
			figure.Add (new Vector3 (pos.x + borderOffset.x, 1f, pos.z - borderOffset.z));
			figure.Add (new Vector3 (pos.x - borderOffset.x, 1f, pos.z - borderOffset.z));
			figure.Add (new Vector3 (pos.x - borderOffset.x, 1f, pos.z + borderOffset.z));
			figure.Add (new Vector3 (pos.x + borderOffset.x, 1f, pos.z + borderOffset.z));
			figure.Add (new Vector3 (pos.x + borderOffset.x, 1f, pos.z - borderOffset.z));

			line = new GameObject ("Line " + pos);

			GOLineMesh lineMesh = new GOLineMesh (figure);
			lineMesh.width = 0.6f;
			lineMesh.load (line);

			GameObject textObj = Instantiate (landIdTextPrefab, new Vector3 (pos.x, 1f, pos.z + borderOffset.z - 3f),
				Quaternion.Euler(90f, 0f, 0f), line.transform) as GameObject;

			textObj.GetComponent<TextMeshPro>().text = landId;

			regions.Add (landId, line);
		}
		else {
			line = regions[landId];
		}

		if (isSelected) {
			line.GetComponent<MeshRenderer> ().material = regionBorderHighLight;
		} else {
			line.GetComponent<MeshRenderer> ().material = regionBorder;
		}

		if (isSelected && line.transform.position.y < 0.1f) {
			line.transform.Translate (new Vector3(0, 0.1f, 0));
		}
		else if (!isSelected && line.transform.position.y >= 0.1f) {
			line.transform.Translate (new Vector3(0, -0.1f, 0));
		}
	}
}
